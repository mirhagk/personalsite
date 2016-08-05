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

Explicit typing is when the programmer must tell the compiler or language tools what type a certain variable or object is. This is required when the tool is not powerful enough to figure out the type. It is required more in statically typed languages than it is in dynamically typed languages simply because the runtime has more information. However with better tools/languages, the amount of explicit typing required can be reduced.

Most people hate explicit typing, and usually the debate comes down to having to do this vs having better safety and tools. This is one of the things that is nearly universally seen as bad. We need to design languages that don't have to have explicit typing.

Local Type Inference
---

Local type inference is when the compiler/tools can determine the types of variables/objects within a certain scope (usually a method, but could also be a class, or some other unit of a program). By doing local typing inference you are able to reduce the amount of explicit typing by a huge amount, but still force the programmer to think about the types at API boundaries.

There is almost no downside to local type inference, especially when optional typing is allowed. The only downside is the amount of effort the compiler must do.

Local Type inference has grown in popularity and usefulness over the years. Beginning in 2007 C# got it's first taste of local type inference, with the ability to infer the type based on the initial expression assigned to the variable. There are more powerful local type inference systems out there, which can infer the type not just based on the initial assignment, but they usually require a more powerful type system (the ability to express sum and union types for example).

Global Type Inference
---

Global type inference is a more debated subject. It's the ability to infer types across the entire program, allowing you to write entire programs without specifying types once. Haskell partially has this (among others). It's impossible to completely infer every type, but nowadays enough types can be inferred that for most practical programs you'll not have to specify types, and the compiler/tool can be made to fallback to a sum type (i.e this variable can be either an integer or a string). This reduces either the usefulness of the tools, or requires the programmer to disambiguate manually, but such cases can become so infrequent that it doesn't matter.
  
This is a more debated feature because it requires substantial complexity by the compiler/tools. It's also possible that using objects in one part of a program could affect the inferred type in other parts of the program, potentially even changing the semantics of the program. Many do not like the idea of changing other parts of the program. Others question the usefulness and would like to specify the types along all API boundaries.

With global type inference you can get programs that look like most dynamically typed languages, but is actually statically typed. This provides a best of both worlds situation. However because of the mentioned issues such a language should provide a way to specify optional types, and perhaps even allow the programmers to limit the amount of type inference.

Optional Typing
---

Optional typing is when the language allows you to provide hints to tell the compiler/tools what type a certain variable/object is, but when it is not required. Many languages provide syntax for this, and there's also tools to explicitly specify types in dynamically typed languages (for instance JSdoc).

Optional typing is a good thing, as it provides a way for the programmer to correct their tools when the tools try to infer the type. Some also enjoy it for it's ability to better document the expected values of a method, especially in cases where the editor can't provide this information, or would infer different types based on the use.

  
Static Type Checking
---
  
The ability for static tools to prove that there will be no type system violations when the program is run, or report any issues. For statically typed code this is completely solvable, since that what statically typed means (can be determined at compile time). Dynamically typed code can also have static type checking, but there are situations when the type checking is incomplete. In fact if dynamically typed code can be statically type checked, then the code can be considered statically typed, because this is the distinction.

When talking about dynamically typed "languages", remember that there are subsets of the language that are statically verifiable, and hence there are subsets that are statically typed.

There has been discussion about whether this is necessary, since sufficient testing should make this unnecessary, but it's certainly helpful when there is not a complete set of test cases.

API Discovery
---

This is a very nice feature which allows a programmer to explore the API of the objects they are working with without having to look up documentation. This can be combined with auto-complete/intelli-sense where the editor provides options for what is legal to the type system at the point in the program.

Statically typed code is able to have API discovery since the types are known to the tools. Just as above, subsets of so-called dynamically typed languages can have API discovery through having statically typed subsets.

Enabling the ability for API discovery is one of the biggest benefits to developer productivity, and usually the arguments of "static vs dynamic" actually come down to the trade-offs between explicit typing and API discovery. As mentioned above it's possible to reduce explicit typing while maintaining static typing, which reduces the trade-offs required to have API discovery.

Common Misconceptions
===

> Statically typed languages are more powerful than dynamically typed languages because they can emulate them

It's sometimes stated that static type systems are more powerful than dynamically typed languages since you can emulate dynamically typed code using a key-value pair store and some wrapper code.

This falsey assumes that languages are statically or dynamically typed, but code is. For instance the following:

	object dynamicField = dynamicObject.Get("score");
	int score = (int)dynamicField;
	
is an example of a program that's valid in many so-called statically typed languages[^2], but isn't really statically typed. By doing this you're telling your type checker to simply treat `cachedObject` as if it was an integer, and this is circumventing the compile time type system (this will likely generate a run time check here, and throw an exception if it fails, which is precisely what dynamically typed code does).
  
Really all this shows us is that most static type systems can be circumvented.

> Dynamically typed languages can have just as nice tools as statically typed languages.

This misconception stems from a confusion of "dynamic typing" within the language. While a language make require dynamic typing, subsets of it may not. By building tools that work with a subset of language, you prove that that subset is statically typed (most likely with type inference). Then you've no longer built tools for dynamic languages, but built tools for a static language that happens to be a subset of the dynamic language you're working with.

Better type inference helps both static and dynamic languages. For dynamic languages it reduces the amount of times the tools fail to be helpful. For static languages it reduces the amount of explicit typing that is required.

> Statically typed languages are verbose

Or alternatively:

> Java is so verbose

Verbosity has to do with explicit typing, not static typing. Haskell is very concise despite being statically typed, because of type inference.

TypeScript has static typing tools as well, and it works with plain JavaScript (since it's mostly just the addition of optional typing to JavaScript).

> Static typing and Dynamic typing are mutually exclusive

They are not mutually exclusive. TypeScript is a language that is both statically typed and dynamically typed. Dynamic typing is an implementation detail, by default all languages would require it, but static typing means that tools could eliminate run-time typing in certain, or perhaps all scenarios.

JavaScript is another example. There are static typing tools for JavaScript, which is normally dynamically typed. The closure compiler is an example of a type checker that checks for types in subsets of the code. In fact most modern runtimes use some static analysis to remove dynamic typing which can be expensive. V8 produces a new type each time you.


The Ideal Language
===

Ignoring all the misconceptions out there, how does a language designer for the next greatest language design a type system? First of all, ignore the how and focus on the what. What features would you like in the type system? Only after figuring out what you want should you focus on how to do it. You may need to drop features at this point to reduce complexity.

Above all remember that static and dynamicing typing are not features. They are a description of code and language pieces.


The perfect language in my books provides API discovery, static type checking with having minimal explicit typing. The ability to have duck typing is also very nice, and optional typing is a must (to restrict the usability of a method to certain types, limiting the usefulness, but allowing you to change the internal behaviour while having to maintain a smaller external behaviour).


[^1]: assuming the type semantics are equivalent at run-time and compile time, and all other language semantics are equal
[^2]: assuming you change the syntax to the one appropriate for the language
