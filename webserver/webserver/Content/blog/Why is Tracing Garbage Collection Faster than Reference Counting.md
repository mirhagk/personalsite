We're going to take a small break from our LaTeX series to talk about garbage collection. First of all:

History of Garbage Collection
---

Many of us programmers are really spoiled nowadays. Back in the early days when you wanted some memory, you had to find it yourself. Memory wasn't this magic thing that you could get some area of, it was just a giant chunk, and if you wanted to use a location, you just used it.

Of course you had to make sure other parts of your program weren't using the same section, so memory allocation was born. You'd write a function that would find some memory for you and give it to you. But you had to promise to give it back when you were done. It kept a list of memory locations that were currently free, and just give you an item from that list of about the same size (there's of course some complexities such as if you ask for something smaller, it'll break it up into 2 sections, and it can combine smaller sections into bigger ones, but that's all behind the scenes). 

So the basic memory allocation process was born, consisting of 2 functions `allocate` and `free`, which would allocate you memory and free it, as the names suggested. There were several problems though. The first problem is how can it possibly find something of the correct size. This was solved with various implementations that could shrink of grow other free parts of memory to make it work. This leads to fragmentation which will get to in a minute. 

The other problem is from the programmer's perspective. What if a piece of code decides to share it's object with another function? Who's responsible for calling `free` then? Obviously whoever uses it last, but it's not like you can check if anyone else is using it. It's like that container of rotting food in the work fridge. You want to get rid of it, but you don't know if anyone else is saving it for later.

The solution to this actually is comprised of 2 separate solutions. Tracing and Reference Counting.

Reference Counting
---

The first solution is rather simple. You basically store a number with the object that says how many things are using it. When something stops using it, it decreases this counter, and if it gets to zero it calls free. Now each time you share it, you have to increase the counter.

This is a fairly simple solution, and works alongside the old method of allocating and freeing which means if you know you're the only one who's going to use it, you don't need to use reference counting.

It does however make for an annoying number of calls to increase and decrease this number, and still requires the programmer to keep track of when they need to increase or decrease the number. Really the root problem is humans suck at keeping track of things. We don't want to have to know when we have to call `free` and similarily we don't want to have to know when to decrease the number. That's where Tracing comes in.

Tracing Garbage Collection
---

Just a note:

> Tracing Garbage Collection is often just referred to as a garbage collector, but reference counting is technically a garbage collector as well, so make sure you are clear about what someone refers to when they say "Garbage collection"

Tracing garbage collection takes all the work of managing memory away from the programmer. If you've never thought about where the variables are actually stored, you've probably been spoiled with it. Languages like javascript, python, java, C# and haskell use variations of tracing garbage collection so the programmer doesn't have to worry about it. It's fundamental in a high level language that the programmer need not worry about memory, which is a low level concept.

Tracing basically works by giving you memory when you ask for it, and you don't bother calling `free` or doing anything. Rather when it runs out of memory (or maybe at some other point it feels like) it will pause the program, and actually walk through your program's references, seeing what you refer to. It then walks through those objects seeing what they refer to. It keeps doing this until there's no more new locations to look at. At this point it knows that if it hasn't come across a memory location, it's because nothing within the scope of your program refers to it, and so it can feel free to reclaim that memory.

Now all this bliss for the programmer doesn't come free. The memory manager needs to know what's a pointer and what's not a pointer, so no pointer arithmetic is allowed. You as a programmer aren't allowed to know what memory location a pointer is pointing to, you can only access the memory and check if 2 pointers point to the same thing.

This of course isn't a problem for high level programmers. I haven't heard the last time someone complained that they couldn't add random numbers to their pointer, or xor it with another pointer or any other pointer arithmetic.

The other complaint that usually comes with tracing collection is that it pauses your program and takes a while to sort things out, which is true, so if you're developing something that can't afford to pause for a millisecond or 2 then tracing garbage collection isn't the answer. The other complaint is that it runs too often, and therefore takes up too much of the programs time. However if it's running too often, it's because the program is running out of memory too much, so it can be solved by making the program not waste so much memory.

Addressing the problems with Reference Counting
---

Now we can achieve a similar level of taking the burden of managing memory away from the programmer with automatic reference counting. Basically the compiler inserts all those calls to increase and decrease the counter for you. When this is done it's usually just referred to as garbage collection, and if it is a complete solution (wherein the compiler is the only one to insert those calls) then you don't even need to know that it's doing reference counting under the hood. You can assume it's just doing tracing garbage collection under the hood.

> Note this is not true of something like objective-c where the programmer has control over the memory still, just has the option to relinquish control

Of course you still have another problem with cycles. Basically if object A points to object B, and object B points to A, then they'll never reach a counter of 0. You'd have to free object A before object B gets freed, and you'd have to free object B before object A gets freed. If object A or B also referred to other objects then those wouldn't be freed either, so this can be a very bad thing. A naive solution to this is to give a separate kind of pointer to the programmer, and this pointer doesn't count for references. This of course works, but now the programmer needs to be aware of where cycles could exist and see the above note on how humans suck at keeping track of things. It could also create the below situation:

	function Foo():
		parent = new Thing()
		child = new Thing()
		parent.child = child
		child.parent = WeakPointer(parent) //this reference isn't counted by the reference counter
		return child

	function Main():
		object = Foo()
		DoSomething(object.parent) //uh oh, parent is a dangling pointer

In this code we see that the Foo function creates 2 objects, parent and child. It then makes them refer to each other, but the child only has a weak reference. When the function returns, the compiler would release the parent object since nothing is pointing to it anymore (since the child's reference doesn't count). Now when the main function tries to do something with the parent object, the parent object has already been freed. This is called a dangling reference, and it's basically one of the most awful things a programmer has to deal with in memory-unsafe languages.

The above is the way objective-c and most C++ libraries deal with it, and it's not pleasant. PHP before 5.3 dealt with it in a unique way, it just ignored it. Newer versions will now detect these cycles and remove them, in a similar way to tracing garbage collection. It's much nicer removing this worry from reference counting.

Okay so does it matter?
---

Okay so we can make reference counting behave like tracing garbage collection, so why we would ever have tracing which pauses your program and makes you wait? Well because it's faster.

Wait what? Pausing your program completely, and scanning through all of memory periodically is faster than just keeping track of what's being referenced with a counter? How?

Well here's why:

###Free memory management

If you happen to have a GB of RAM free, and your program only uses MB's of memory, then you don't need to free memory. If your program doesn't create enough garbage to fill up memory, then the garbage collector doesn't need to run. With a tracer it can just not run until it's needed, and you save all of the cost of freeing memory. Reference counting doesn't know that you won't need to free memory though, so it still does all it's work, and frees the memory you stop using it. It does a lot of work that isn't even needed.

###Reference counting pauses too

Reference counting pauses your program too, when it frees memory. It pauses your program to free the reference, then free whatever that object is pointing to etc. This happens to usually not be a lot, but can anyone think of something where you have something that refers to something else that refers to something else that refers to something else? The answer is linked lists, or trees. These data structures are all kept indirectly through the root node, and if you free the root node, then the reference counter has to go through all the members of the linked list or tree and free them individually. If you for some reason have a 512 MB tree, then it's going to walk through that entire thing and free 512 MB of your memory. If they all shared a reference to some object, it's also going to decrease that counter one by one for each node in the tree. This could all add up to pausing your program for a signficant amount of time.

When you say that reference counting doesn't pause the entire program, you have to follow that with a little footnote saying "Not for very long in most cases that is. It could go for longer, but either have to be a bad programmer or a fan of LISP to use linked lists/trees of that size"

> Incidentally this is why most LISP implementations use tracing collectors and not reference counters. In fact LISP was the reason tracing collectors were invented.  


###Reference counting suffers from fragmentation

We mentioned earlier something about fragmentation and that we'd get to it later. Well it's been a while but now we'll finally talk about it. Basically there's 2 categories of fragmentation, internal and external.

Internal fragmentation happens when you ask for 500 bytes of memory and the memory manager has something that is 512 and it says "close enough" and gives you the whole 512 bytes. It doesn't even tell you about it, you just go on using the first 500 bytes as if you aren't wasting those 12 bytes. I mean the memory manager knows, and when it gets that back it'll take back the whole 512 bytes, but while your using it it's wasted.

External fragmentation is a more serious problem. Let's say it has made little sections of 512 bytes, and it has 5 of them in a row, like this:

	block 1 | free
	block 2 | free
	block 3 | free
	block 4 | free
	block 5 | free


 You ask for 3 sections that are 512 bytes each, and it may look like this now:

	block 1 | A
	block 2 | B
	block 3 | C
	block 4 | free
	block 5 | free

Now let's say you no longer refer to A and B. This means it'll look like this:

	block 1 | free
	block 2 | free
	block 3 | C
	block 4 | free
	block 5 | free

If you now ask for more than 1024 bytes it will say it's out of memory, even though the memory is there, it just isn't in the right order. There's no section bigger than 1024 all in a row, so you've reduced the amount of memory available for a single object.

Tracing collection solves this problem by just moving C around, and then it can get enough space. It can do this since it can walk through memory and know where all the references to C are. Reference counting does not know where the references to C are however, so it can't move C around and is stuck with the limited space until you free object C.

By moving stuff around tracing garbage collectors don't have to suffer from fragmentation while external fragmentation will always exist in reference counters.

###Reference counting has crappy cache performance

There's a common law followed by cache designers and it's called the principle of locality. Basically it says that if you use one piece of memory, the stuff right beside it is likely to be used soon. Therefore stuff that's close together in memory is considered to be used together.

The repercussion of this is that when you ask for a certain memory location, the computer actually fetches the stuff around it too, so if you use those locations, it'll be much faster. If your access to memory doesn't follow the principle of locality, your program suffers from what's called "cache thrashing" where nothing it grabs is used before it's overwritten with new data, so you never get the benefit of the cache.

Therefore a program should seek to follow the principle of locality as best it can. Reference counting (along with manual memory management) does not follow this principle very well. This is because when you ask for memory, the first few times they come back all in a row, but once you've filled it up and it's grabbing from freed locations, those locations could be anywhere, and you may get completely different memory locations each time you call it in a row.

An example will serve us well. Let's say you run your program, and it's gotten to the rather unpleasant state below:

	block 1 | free
	block 2 | taken
	block 3 | free
	block 4 | taken
	block 5 | free

The next 3 blocks you ask for will not be next to each other at all, rather they are in completely different locations. This could even be extended to the point where they are in entirely different pages of memory. If you work with these 3 values in a row then they will "thrash" the cache and make your program slow down.

With garbage collection you might have a similar memory model (remember that unused memory doesn't get freed until you run out of memory):

	block 1 | unused
	block 2 | taken
	block 3 | unused
	block 4 | taken
	block 5 | unused

Then you request a block, so it needs to free the memory. If the tracing collector has compacting, and detects that it'd be helpful (ie the blocks are small enough) it could move the existing blocks around to make it look like this:

	block 1 | taken
	block 2 | taken
	block 3 | free
	block 4 | free
	block 5 | free

Now when you get the next 3 blocks, they will be all in a row, and you're application might get a noticeable speed up.

###Reference counting can't use bump pointer allocation

Wow that's a lot of big words there. So what is bump pointer allocation?

###Reference Counting does more work

This is probably the simplest to validate and explain. Basically consider the amount of work that happens to your objects over the scope of their lifetime. Every object has to be created, so the memory manager must find some location in memory of the right size (we've seen tracing collectors can be faster with this, but let's assume they take the same speed). Then every object must be freed, which involves telling the memory manager it's free to use it. This is usually not very expensive, but dep