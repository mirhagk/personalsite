Static vs Dynamic Typing
===

This is a dreaded, pointless argument for some, and a religious calling for others. Inevitably any gathering of programmers will eventually have someone ask this question, and usually a very heated debate follows. Some people will dig deep trenches and shout things like "you can have my dynamic typing when you pry it from my cold dead hands!". Others will point out that even if Haskell could solve the halting problem, they'll still never understand what the hell the `IO` monad is.

The problem with the debate is that most parties come to the debate thinking something like Java vs JavaScript, and treat that as Static vs Dynamic Typing. The core concepts often times get confused, and tied to each other when they are not always tied to each other.

In this post I'll explain all the different concepts, the pros and cons of each one, and then discuss languages that employ the different concepts.

Concepts
===

Static Typing
---

This is probably the most confused concept. Static typing refers to compile time type checking. This means errors about mismatched types will be reported during development/compilation. 

/*But statically typed code can statically check the types without reporting any sort of type errors. This is done with implicit casts. Consider the following `C` snippet:
	
	char text[] = "Hello world";
	long let = 33.9;
	text[0] = let;
	printf(text);

Here you're assigning a floating point number to a long, then assigning that to an element of a string. Most would consider that fairly nonsensical, yet it's still statically typed since the compiler knows all the types involved at compile time, and generates the implicit casts. (here from `double` to `long` then from `long` to `char`)*/
  
Different people have different ideas about what it takes for a language to be considered statically typed, but an entire language doesn't necessarily have to be statically or dynamically typed.

`C#` is an example of a partially statically typed language. The language is mostly statically typed, but supports `dynamic` variables, which are typed at run-time. You can also do all sorts of funky things with reflection to get around the type system.

Dynamic Typing
---

Dynamic typing is when the run-time environment dynamically determines the type of an object or value. Dynamically typed code is more flexible than statically typed code simply because the run-time has more information about the current state of things than the compile time tools do. There are things that type systems cannot prove, and things that are too hard to for it to attempt to prove. 

Every valid type checked program is also valid dynamically typed code [^1]. However not every valid dynamically typed program is a valid statically typed program. Because of this dynamic typing is strictly more flexible in terms of ways to write code.


Strong Typing
----

Weak Typing
---

Duck Typing
---

Explicit Typing
---

Local Type Inference
---

Global Type Inference
---

Common Misconceptions
===

> Statically typed languages are more powerful than dynamically typed languages because they can emulate them

It's sometimes stated that static type systems are more powerful than dynamically typed languages since you can emulate dynamically typed code using a key-value pair store and some wrapper code.

This falsey assumes that languages are statically or dynamically typed, but code is. For instance the following:

	object dynamicField = dynamicObject.Get("score");
	int score = (int)dynamicField;
	
is an example of a program that's valid in many so-called statically typed languages[^2], but isn't really statically typed. By doing this you're telling your type checker to simply treat `cachedObject` as if it was an integer, and this is circumventing the compile time type system (this will likely generate a run time check here, and throw an exception if it fails, which is precisely what dynamically typed code does).
  
Really all this shows us is that most static type systems can be circumvented.




[^1]: assuming the type semantics are equivalent at run-time and compile time, and all other language semantics are equal
[^2]: assuming you change the syntax to the one appropriate for the language
