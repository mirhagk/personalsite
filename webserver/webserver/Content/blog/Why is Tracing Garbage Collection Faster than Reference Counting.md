We're going to take a small break from our LaTeX series to talk about garbage collection. First of all:

History of Garbage Collection
---

Many of us programmers are really spoiled nowadays. Back in the early days when you wanted some memory, you had to find it yourself. Memory wasn't this magic thing that you could get some area of, it was just a giant chunk, and if you wanted to use a location, you just used it.

Of course you had to make sure other parts of your program weren't using the same section, so memory allocation was born. You'd write a function that would find some memory for you and give it to you. But you had to promise to give it back when you were done. It kept a list of memory locations that were currently free, and just give you an item from that list of about the same size (there's of course some complexities such as if you ask for something smaller, it'll break it up into 2 sections, and it can combine smaller sections into bigger ones, but that's all behind the scenes).