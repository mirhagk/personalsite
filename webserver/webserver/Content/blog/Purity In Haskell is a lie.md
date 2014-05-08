Purity in Haskell is a lie
===

When you first learn Haskell, you learn about how everything is immutable, and side effects are disallowed, and how threading is easy, and programs are mathematical and predictable and life is good.

And it's great while you're working with it. Every time you call a function with the same parameters, it returns the same result. This makes thinking about the program easy.

Since there's no side effects of functions, you could have multiple threads and not have to worry about the threads negatively impacting each other. They could just automatically work, and Haskell could just turn on multi-threading on programs that never were written for multi-threading.

Life seems so great, and you're happy that now we have a language that ensures perfect safety and purity, even if no-one really "gets" how to program in it.

Side Effects are Required
---

Inevitably during their journey through Haskell a programmer will think about side effects. The first thing you work with are functions, and functions can do whatever computation they want and return a value, and this seems like a good interaction with the world.

Then you wonder about making a command line program. You have a moment of realization and you realize that your program just reads in some text, and writes out some text, and it's basically just a big function. That's great you think, you can write a little generic main method to read and write the results of a function.

Then you think "Wait, what about config files?", but before too long you realize you could just read that in the main function and pass it in as an extra parameter.

The web seems a perfect fit for haskell at first. After all, a request comes in, does some processing, and then returns a result. HTTP is perfectly stateless, so the lack of state means Haskell maps to it perfectly. Cookies are just another parameter.

Then you realize you need to store the user's preferences in a database, and now the easy

~~~
do
input <- readHttpRequest
writeHttpRequest process input
~~~

gets just a little bit messier as you need to access the database.

Magical Monads
---

It's at this point that the bright young programmer needs to actually try to understand the `IO` monad instead of just pretending it's magic. It's very easy to use monads without really understanding how they work, but as you need to use them more, the lack of understanding makes things very difficult.

See Haskell is pure, but reading from a file is not pure, so it wraps those reads in a little handy `IO` monad. You know that this is still pure because everyone told you so, and they wouldn't lie would they? 

But how exactly does it protect you? Well Haskell talks about monads and functors and applicators and all sorts of fun terms that don't describe what they do very well. What if I told you monads weren't magical? What if a told you that you probably have been using monads in other languages before?

See in Haskell, you can easily write functions that work on very generic inputs, and this generalization of everything seems to spill over into discussions of the language. When concepts like `IO` are explained, they are explained with monads in general, but that's not really required.

If you've ever worked in SQL, you should be familiar with semantics of `NULL` in that language. `NULL` and anything equals `NULL`. Whatever you do with `NULL`, the result ends up as `NULL`.

This is the same as the `Maybe` monad in haskell. In fact they are nearly identical. If you were to write the logic for SQL's `NULL` in C# it'd be something like:

~~~
T? DoFunction<T>(T? wrapped, Func<T,T> function) where T:struct
{
	if (!wrapped.HasValue)
		return null;
	else
		return function(wrapped.Value) as T?;
}
~~~

and in Haskell it'd be very similar:

~~~
DoFunction (Nothing) = Nothing
DoFunction (Just x) fx = fx x
~~~

The Haskell function is pretty much the same, just with the convience of not needing a whole lot of type casts.

Making `IO` monad
---

So if Haskell "monads" are really just normal things that people already understand, what is the `IO` monad?

Well if you check out [the documentation](https://hackage.haskell.org/package/base-4.5.0.0/docs/System-IO.html) it's actually pretty simple:

~~~
data IO a
~~~

It's just a wrapper around a type. Literally just a wrapper. If you were to write the same data structure in C# it'd be:

~~~
struct IO<a>
{
	a Value;
}
~~~

So how does this magically prevent the awful world of side effects from creeping into Haskell? It doesn't. It seems almost like blasphemy writing that, but it's true.

Imagine C# having the following:

~~~
class IO<T>
{
	private T value;
	public IO(T value)
	{
		this.value = value;
	}
	public IO<T> DoFunction(Func<T,T> function)
	{
		return new IO<T>(function(value));
	}
}
~~~

This would provide roughly the equiavelent functionality of the Haskell `IO` monad. Let's now pretend that C# returned this value from `Console.ReadLine();`. Now we want to take a string from the user, and upper case it

~~~
var value = Console.ReadLine();
value.DoFunction(v=>v.ToUpperCase());
Console.WriteLine(value);
~~~

That's pretty close to what you would do in Haskell. The benefit of `Console.ReadLine()` returning this `IO<string>` instead of just a `string` is that if you returned it from the function, everyone would know you got it from impure source, and it'd mark that as a "dirty" string. In fact many people have recommended doing something like this with user input.

Seeing behind the curtain
---

Of course it's really just a facade, if you really wanted to, you could get around the class. I've written one way here using reflection:

~~~
T EscapeIO<T>(IO<T> wrapped)
{
	return (T)(typeof(IO<T>).GetField("value",BindingFlags.NonPublic | BindingFlags.Instance).GetValue(wrapped));
}
~~~

So know that safety has gone away. Of course you should never write code like the above, but there's nothing preventing you from doing it, it just makes it a bit more difficult, in the hopes you **won't** do it.

It's the same in haskell, except they already provide the function for you, `unsafePerformIO`. Clearly you should never use that function, it even has "unsafe" in it's name. But does it really stop me from writing?

~~~
let getLineAwesome = unsafePerformIO getLine
~~~ 

The truth is, if you use a library, it could in fact be doing something very similar to the above. There's no way to know for sure that the person you're calling is truly a pure function, you just have to hope that they don't something like the above.

Why haven't I heard of this yet?
---

This seems to be like a big deal. It seems like this should be a common problem (calling a function you expect to be pure, but isn't). So why isn't it?

My guess (and this is pure conjecture) is that script kiddies haven't migrated to it yet. PHP is awful not because of fundamental problems with the language[^1], but because of "programmers" who decided that proper programming practice is just too much work, and code should just do what it wants.

You don't see problems with `unsafePerformIO` because Haskell programmers love purity too much to abuse this function. It's a close guarded secret, stackoverflow questions on "How do I convert IO String to String" get answered with "Well you could do it, but we won't tell you how. Instead do things the right way&trade;".

If Haskell gets more popular, then maybe someone will answer with "just use `unsafePerformIO`". That will be the day when all safety in Haskell dies, and this beautiful flower of a language will be transformed into yet another awful unsafe language.


[^1]: Well maybe a little bit.

SELECT CASE transaction_isolation_level 
WHEN 0 THEN 'Unspecified' 
WHEN 1 THEN 'ReadUncommitted' 
WHEN 2 THEN 'ReadCommitted' 
WHEN 3 THEN 'Repeatable' 
WHEN 4 THEN 'Serializable' 
WHEN 5 THEN 'Snapshot' END AS TRANSACTION_ISOLATION_LEVEL 
FROM sys.dm_exec_sessions 
where session_id = @@SPID