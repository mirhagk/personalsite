Zen Of Programming - Extension Methods
===

Intro
----

Welcome to my Zen Of Programming series. This series will take a look at how I write code, what my philosophies are within code bases, and generally what I perceive to be "good" code. I've called this Zen Of Programming since working with a good codebase can be peaceful and calming. Working with a bad codebase can be stressful and angering.

I come from a primarily C# world, so much of this advice is going to relate to C# specific syntax. However many of the philosophies are applicable in any language.

Extension Methods
---

The first thing we are going to look at is Extension Methods. Extension methods in C# look like the following

~~~
static class Extensions
{
	public static string ToJson<T>(this T model)
	{
		return Newtonsoft.JsonConvert.SerializeObject(model);
	}
}
~~~

They are defined within static classes (which are just collections of methods). They are static, and their first parameter is prefixed with `this`. The `this` essentially lets the compiler know this is an extension method, and you can therefore call it as if it was a method defined on that class. To use this method we would have something like:

~~~
var model = new BlogCommentViewModel
{
	ID = blogComment.ID,
	CommentTText = blogComment.Text,
	PostedDate = blogComment.PostedDate,
	Author = blogComment.Author.Name
};
return model.ToJson();
~~~

The last line is where it's used. 

I prefer to use extension methods for common transformation cases, especially when they involve viewModels. One of the main reasons I like doing so is it allows you to chain together the transformation with the creation in a lot of situations, without losing clarity.

~~~
return new BlogCommentViewModel
{
	ID = blogComment.ID,
	CommentTText = blogComment.Text,
	PostedDate = blogComment.PostedDate,
	Author = blogComment.Author.Name
}.ToJson();
~~~

Here we've removed the model variable without increasing the complexity of the method. Being able to turn multiple statements into a single expression gives you much more flexibility into how your code can be structured.

Another example where this could be useful is if you have a common set of fields that every database model needs to have set (say created date and user). You could define an extension method and then do

~~~
var invoice = new Invoice
{
	Total = products.Sum(),
	Products = products,
	BillTo = customer
}.Created(currentUser);
~~~

Here the invoice is created and it's created date is set to now, and the user set to the current user. Doing this makes how you handle the created date/user consistent across all your models, allowing you to change it in the future, and as a bonus it also reduces the amount of code you need to type.

You could do this with a regular method as well, but it does flow as nicely

~~~
var invoice = Created(new Invoice
{
	Total = products.Sum(),
	Products = products,
	BillTo = customer
}, currentUser);
~~~

With this approach it's not easy to see that the `currentUser` parameter is passed to the invoice. Especially if the invoice creation was long enough to fill a screen, or if you had other methods you wanted to call, it'd be hard to see where that is coming from.

Having it as an extension method also makes it feel like it's part of fluent API. Fluent APIs are great for configuring and working with data, and extension methods allow you to write fluent APIs that work on potentially generic types (as in the `ToJson` example) or interfaces. In fact the `System.Linq` methods that are really powerful are all defined as extension methods on an interface. The definition of `Where` would look something like this

~~~
static class LinqExtensions
{
	public static IEmumerable<T> Where<T>(this IEnumerable<T> source, Func<T,bool> filter)
	{
		foreach(var item in source)
		{
			if (filter(item))
				yield return item;
		}
	} 
}
~~~

It allows you to chain the calls, along with the rest of the `System.Linq`.

Power vs Contract
---

Extension methods are a way to give an interface functionality. `Where<T>` gives any `IEnumerable<T>` the power to filter out entries. Having a collection implement `IEnumerable<T>` immediately gives it a bunch more functionality and power.

The usual use case for interfaces is described as fulfilling a contract. The mindset that this sets is that you have to fulfull a certain interface in order to be used by something. While this is still technically true of extension methods, the mindset seems to switch a bit. It instead becomes a suite of functionality that you get if you implement an interface. It's a slight shift, but one that I find changes my perspective enough to get me into that peaceful, fluid state of programming.

I hope that you take this blog post to heart, and start looking for duplicated code dealing with view models. From my experience enterprise software (and web software in general) has a ton of it, and you can simplify your code by a significant amount.