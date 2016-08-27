O(1) Stack
====

Algorithms classes teach about complexity of various containers, and the complexity of various implementations.

One classic analysis is of implementing a stack. A stack is a data structure that has the following operations:

	void push(T item)
	T pop()

That is, it can push an item onto the stack, and pop the last pushed item off the stack.

The easiest way to create the stack is to use a maximum size and use an array with a pointer (or an index) to the top. In code:

	void push(T item){
		array[index++]=item;
	}
	T pop(){
		return array[index--];

Since each operation is one read or write to the array, and an increment/decrement the complexity is `O(1)` (the holy grail of complexities, constant time).

Of course in practice you need to be able to keep increasing the size of the stack, otherwise you'd need to make it very large to be safe and waste a lot of program space.

So there 2 classic implementations. One uses an array that simply expands whenever you need it to (by allocating a new array and copying all the elements over). But this changes it to a worst case of `O(n)` since you may need to copy the entire array, which takes `O(n)` time. And the other uses a linked list, which still has constant time, since adding a new element to a linked list is constant time (you only need to update a pointer).

Because of this, the linked list stack is seen as a better theoretical stack since it can grow and will always take constant time.

But it's possible to get `O(1)` time out of an array, it just requires us to take advantage of virtual memory space.

Virtual memory space is the space that your program works with. Your program's memory is actually divided up through the RAM in pages. Each page is a constant size and the virtual address is converted to a physical address before it's read.

Modern computers use 64 bits for pointers. This allows them to address 2^64 different memory locations. This is way more space than any computer has. (approx 1 billion GBs). In practice processors don't allow this whole space, (AMD64 allows 48 bits for virtual addresses which is 256 TBs).

If we combine these 2 facts it's possible to build a `O(1)` stack using an array. The initial stack needs to correspond to the size of a page. After you allocate the page, reserve the virtual memory space of a very large size (2^32 perhaps). This wastes virtual memory space, but your program will run out of both physical memory and hard drive space **long** before worrying about virtual memory space, so that's okay.

Instead of expanding the size of the array when the stack grows too large, allocate a new page from memory to the next section of the virtual memory space. This "grows" the array. Since the virtual->physical mapping already must take place for a computer this doesn't give any overhead with regards to time/CPU.

The restrictions of this is that it is much more involved, and would require the stack to handle memory allocation itself. And truth be told I'm not 100% sure which, if any, operating systems would allow this. But it it possible to do, and the downsides is that the stack takes up the size of entire page at the minimum, so it only works for large arrays.

Is this useful? Perhaps. If you have an algorithm that needs to work with a potentially very large stack then this would be a good choice for an algorithm, rather than going with the slower practical performance of a linked list.

Is this cheating? Technically yes. This is exposed to your program as an array, and in virtual memory space it's contiguous block of memory, but physically it's like a hash table pointing to several arrays. But this is what all of memory is anyways, so it's no more cheating than any other program. And the "hash lookup" is done at a very low level, and is very very very fast (since it needs to be done for every single memory read). 

And since the virtual memory space is contiguous the CPU cache can do pre-fetches correctly, avoiding the cache trashing that using a linked list would cause.

These kind of tricks are what separate the theoretical algorithms classes from real world algorithms.