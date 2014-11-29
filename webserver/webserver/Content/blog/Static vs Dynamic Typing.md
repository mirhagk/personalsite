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
  
Different people have different ideas about what it takes for a language to be considered statically typed, but an entire language doesn't necessarily have to be statically or dynamically typed.

`C#` is an example of a partially statically typed language. The language is mostly statically typed, but supports `dynamic` variables, which are typed at run-time. You can also do all sorts of funky things with reflection to get around the type system.

Dynamic Typing
---

Dynamic typing is when the run-time environment dynamically determines the type of an object or value. Dynamically typed code is more flexible than statically typed code simply because the run-time has more information about the current state of things than the compile time tools do. There are things that type systems cannot prove, and things that are too hard to for it to attempt to prove. 

Every valid type checked program is also valid dynamically typed code [^1]. However not every valid dynamically typed program is a valid statically typed program. Because of this dynamic typing is strictly more flexible in terms of ways to write code.

Weak Typing
---
  
Weak typing is often confused with dynamic typing, but weak typing is a distinct concept, and can happen with static or dynamic typing. Weakly typed code is simply code that is implicitly converted to a type other than the sub-type or super-type. For instance `int x = "34"` is weakly typed code since a string is converted to an integer, and neither one is a sub-type of the other. Meanwhile `Animal animal = new Giraffe();` is not weakly typed since Giraffe is a descendent of Animal.


Strong Typing
----

Likewise strong typing is confused with static typing, but strong typing is distinct. Strong typing is essentially the lack of weak typing. A strongly typed type system will not attempt to convert types, and will instead give an error.

Duck Typing
---

Duck typing is the concept that "If it walks like a duck, and talks like a duck, then it's a duck". It's the ability to ignore the type of an object, and instead specify requirements of that type through the use of variables or functions on it. 

When you complain about the verbosity of statically typed programs you most likely are actually complaining about the lack of duck typing within your language. Statically typed systems are often implemented in a way that does not allow duck typing, while dynamically typed languages often allow duck typing.

Dynamic Dispatch
---
Dynamic dispatch is one way to implement dynamic typing and allow duck typing. When you perform on operation on an object, the run-time asks the object if it can do the operation, and how to perform it.

Dynamic dispatching is incompatible with static typing, because it can not be statically proven what a certain operation will do to an object in all scenarios. Errors with dynamic dispatch often can't be found until they are actually run. The best static analyzers can hope to do is find some of the bugs.

Structural Typing
---

Structural typing is a way a type system can allow partial duck typing. It considers 2 types to be equivalent if the 2 types are structurally the same (have the same fields). This way you can define new types and have existing code treat it as if it was the old type.

Structural typing can have different levels of strictness. Types can be enforced to be identical in structure, for instance C will let you cast `struct`s to a different `struct`, but if there's extra or missing data then you'll run into issues.

Structural typing is often times combined with type inference that generates new interfaces that match a subset of a particular type. This allows the compiler to generate new types that structurally match 2 existing types and allow both types to be used in a scenario, even if the types themselves aren't structurally the same.


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
