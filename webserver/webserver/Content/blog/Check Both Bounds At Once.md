Check Both Bounds At Once
====

Today's post is a neat little performance trick that lets you check if an index is out of range without performing two branches.

This little trick is borrowed from the .NET mscorlib reference source code, where I spotted it. It is certainly not the only place this trick can be found (I'd expect most languages with array safety to have this), and probably not the first, it is simply the place where I spotted it.

Checking whether a value is within the bounds of a range is a very common operation. It's used to check if the given index is within an array or if a given parameter is within the expected range.

Imagine you are writing a function that returns a list of random numbers. The user specifies how many random numbers they want. The function might look something like this:[^1]

	List<int> RandomList(int amount)
	{
		List<int> list = new List<int>();
		for(int i=0;i<amount;i++)
		{
			list.Add(RandomNumber());
		}
		return list;
	} 

The above code works perfectly fine, but what if you wanted to restrict the amount of numbers they could generate? At the same time it doesn't make sense for them to enter in a negative number, so you want to make sure they enter at least one. Here is the resulting code:

	const int MaxToGenerate = 1000;
	List<int> RandomList(int amount)
	{
		if (amount>MaxToGenerate || amount<0)
			throw new ArgumentOutOfRangeException("amount");
		List<int> list = new List<int>();
		for(int i=0;i<amount;i++)
		{
			list.Add(RandomNumber());
		}
		return list;
	} 

This now will throw an exception if the given argument is outside the expected range. But this range check comes at the cost of 2 additional if statements. This probably isn't performance critical code, but what if it was? How could you make that check faster?

We can use something like this:

	const int MaxToGenerate = 1000;
	List<int> RandomList(int amount)
	{
		if ((uint)amount>(uint)MaxToGenerate)
			throw new ArgumentOutOfRangeException("amount");
		List<int> list = new List<int>();
		for(int i=0;i<amount;i++)
		{
			list.Add(RandomNumber());
		}
		return list;
	}

First of all let's talk about why that works. We can see the the `amount>MaxToGenerate` part is obviously handled, but what's with the casts to `uint` and where did the `amount<0` part go?

Well signed numbers begin with a 1 in binary. This means when we cast it to an unsigned number, it becomes a VERY large number (>=2^31 to be exact). This very large number is always larger than a signed number, so if `amount` happens to be a negative number, then `amount>MaxToGenerate` would also be true.

This nifty little trick let's us get rid of one of the conditions, but now we have a conversion to `uint` there. Isn't that more expensive?

While in general conversion can be expensive, there are many cases where conversions in C# are actually free. These are cases where the language doesn't need to do anything to convert it. In this case C# can just compare the 2 values using unsigned comparison. The assembly language has a different comparison operator for signed and unsigned comparisons[^2], so the casts are just a way to tell the compiler to generate an unsigned comparison. We can view the assembly it generates to verify:

		IL_0000: ldarg.1 //Load the first argument onto the stack
		IL_0001: ldc.i4 1000 //load the literal integer 1000
		IL_0006: ble.un.s IL_0013 //branch if less than, unsigned

The first 2 instructions are pushing the 2 arguments, then the 3rd is a branch. It has 3 parts, the first is the type of branch (less than or equal to), the second says it's unsigned, and the last says it's a short jump (it doesn't jump very far in the assembly code).

x86 has similar instructions to do a signed or unsigned comparison (`jg` and `ja` respectively), so these casts are free going right down to assembly, and can be used in any language with a compiler that can determine the explicit cast is unneeded.

I am not proposing that all of you go out and change all your range checks to remove one if statement right now, but it's always worthwhile to know that these optimizations exist in case you do profile your code and realize that a method is called a lot. Library designers and compilers should all be aware of this optimization, and should employ it if possible.

Looking at the generated assembly for a function can sometimes be very enlightening, and is very worthwhile thing to do, especially when you are trying to optimize a method. For C# I recommend using ILSpy, which also let's you decompile it into C# or visual basic if you'd like. Assembly is fun :)


[^1]: There are definitely some other performance gains you could make here besides what this post is talking about, but I wanted to just focus on bounds checks.
[^2]: The comparison operator needs to know whether the integers are signed or not, so there is one operator for each type.