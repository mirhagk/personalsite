Zen Of Programming - Avoid Excessive Error Handling
===

Intro
----

Welcome to my Zen Of Programming series. This series will take a look at how I write code, what my philosophies are within code bases, and generally what I perceive to be "good" code. I've called this Zen Of Programming since working with a good codebase can be peaceful and calming. Working with a bad codebase can be stressful and angering.

I come from a primarily C# world, so much of this advice is going to relate to C# specific syntax. However many of the philosophies are applicable in any language.

Avoid Excessive Error Handling
---

Things can go wrong with code, and a well designed system handles errors gracefully.

The Robustness Principle states:

> Be conservative in what you send, be liberal in what you accept

This means that programs should follow the specification exactly when generating messages to be consumed by another program, but allow deviations from the spec when receiving messages.

This principle was designed for the networking world since the internet is a terrible, unreliable place. If the message didn't follow the specifications exactly but the receiver could still  understand the intentions then it could still perform the requested action.

This principle seems to be well received by web browsers and web servers, who will do their best to understand whatever the heck you throw at them, but also make an attempt to follow the proper specification (at least the good ones do).

The reason why browsers and servers do this is because there is such a vast array of other browsers and servers and very few of them do a good job of following the specification to any real degree. This is an artifact of software in that world.

APIs and Functions
---

Some developers have taken this principle for API design as well, and others have taken it to mean function design. The principle was not designed for this however, and it doesn't apply as nicely. 

You'll often see code like the following:

~~~
string GetUsersColorPreference(int userID)
{
	if (userID > 0)
	{
		var user = db.Users.Find(userID);
		if (user != null)
		{
			var color = user.Colors.Where(c=>c.IsFavorite);
			if (color.Count() == 1)
			{
				return color.Name;
			}
		}
	}
	throw new Exception("User's favourite color not found");
}
~~~

This code is quite long and verbose to read, and really here's what it's doing

~~~
string GetUsersColorPreference(int userID)
{
	return db.Users.Find(userID)
			.Colors.Single(c=>c.IsFavorite).Name;
}
~~~

That is, find the user, then choose the highest ranked color. Anything else will produce an exception, as above.

Some people will naively argue that the first code sample is more robust, simply based on the fact that it has lots of checks, but in the end it simply throws a generic exception anyways. This isn't a matter of robustness, rather it's a matter of giving user friendly error messages, which neither examples does. 

If this is an internal method, and the users of this function are going to be familiar with the system then it's probably preferred to leave a nice simple function, since if they incorrectly call the function and get an exception they'll be able to see the code (and seeing simple, clean code is better than verbose code).

If this is a library or API method than having better error handling is potentially appropriate, but make sure you actually provide the better error handling. For instance you could write

~~~
string GetUsersColorPreference(int userID)
{
	var user = db.Users.Find(userID);
	if (user == null)
		throw new ArgumentException($"User {userID} not found", nameof(userID));
	var colors = user.Colors.Where(c=>c.IsFavorite);
	if (colors.Count() != 1)
		throw new Exception($"User {userID} has {colors.Count()} set as favorite. They should have 1");
	return colors.Single().Name;
}
~~~

Which will tell the consumer exactly what they did wrong. This kind of exception handling is really valid, and useful for external APIs. It's less useful for internal function calls and APIs as the inputs should always be well-formed, and if the developer screws up they can always step through the code.

Even worse is Web sites where the controller does these kinds of checks. Ultimately the user is only going to get a generic error message page either way, and you should be creating good logging and diagnostics so that you can easily reproduce their actions, so these checks are utterly useless.

The next time you're writing a validation check, make sure you are doing it to provide value to the consumer, and not just to be a "defensive programmer"