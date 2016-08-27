\LaTeX{} for the web - Sensible web defaults
======

LaTeX is a great formatting language. For those of you who don't know it, it's a formatting language which is used for many technical papers and textbooks. It allows for typesetting mathematical equations perfectly but that's not why its so great.

There are two things that make LaTeX so great, the first is flexibility, the second is sensible defaults.

Flexibility
---

LaTeX allows you to completely customize the layout, controlling anything you want. You can customize within the document itself or by having a separate sheet for the styles. The web is similarly flexible, it allows you to use either inline CSS or have a separate stylesheet, and you can control mostly the same things. 

The one thing LaTeX has that HTML is missing is the ability to create your own commands. This is what gives LaTeX it's true flexibility. As an example, you can define an environment that'll actually show whitespace as is, which is very handy, or you could create a set of commands to turn the document into a slideshow.

You can't really do this with the web. Sure you can fake it with divs with classes and JavaScript, but that creates hidious awful code (frameworks like EmberJS make your source code look quite awful, and editors don't play nice with things they do). Even if you do fake it with JavaScript, and you're okay with ugly HTML, you still use a dynamic language for static content. A good number of websites don't really do any dynamic work, and could go without JavaScript, but end up including it to get something like a nav bar or side bar to work like they want. It seems silly to have the browser to so much work for so little.

The biggest tragedy with the web solution is that it's almost as hard to learn or use as to write it yourself. Also the fact that its hard to do any sort of configuration means that libraries are often unable to be configured without diving into source code. This means there are thousands of libraries competing to do essentially the same thing. All of them are implemented slightly different too, so mixing and matching libraries is not fun. LaTeX on the other hand has very libraries that are easily configured and easy to use.

Speaking practically, it is pretty much a requirement to know JavaScript to build beautiful sites, whereas I barely know anything about creating LaTeX elements (I can define simple macros that's about it) and I'm able to create absolutely gorgeous LaTeX documents with all kinds of bells and whistles.

The web unfortunately evolved instead of being planned, which means we're stuck with abusing languages for things they weren't designed for. HTML used to do all the basic structural elements, nowadays you need to import JavaScript to do anything. We rely on JQuery for features that should've be standardized and use classes for elements that we should be able to create.

Sensible defaults
---

The other major feature LaTeX has over the web is sensible defaults. If you create a bare bones LaTeX document with just the content (no styling), it'll already look gorgeous, and you'll actually pretty much be done. If you created a bare bones HTML page it'll look like [this](http://mother ducking website.com) (warning language). HTML without CSS frankly looks like crap and no respectable web developer would ever have a website with no CSS.

About 90% of my LaTeX documents require no styling, just semantic markup, and the default processor does an amazing job of making it look gorgeous. It has years of research into making readable, usable documents. In fact doing your own styling is discouraged.

The other few times all I do is import a style file and maybe pass a few configuration arguments in.

It saddens me to think of how many developers have wasted so much time recreating a basic look, or designing layouts for such basic concepts (nav bars, footers, titles require a great deal of styling, but everyone has them, and most are very similar).

The requirement for every site to use CSS combined with a war to have flashier and flashier sites mean that a web user needs to learn hundreds of interfaces to function in their web browsing. Wouldn't it be great if there were only a few, and user's already knew how to navigate a new site? Any writer who uses LaTeX can automatically identify when it is used, and knows exactly how it works.

Not having sensible defaults means we need to do so much work, and waste so much time, just to get a usable site. Creating beautiful sites should be as easy as it is with LaTeX.

So what?
---

Is there a point to this rant about how awesome Donald Knuth is and how he should've designed HTML? 

Well my hope is that eventually a CSS library will be created that'll be so easy to use, and create such sensible, beautiful defaults that it'll just be default in every developers toolkit. It'll be auto-included in your generated template, and will be understood and praised by all. There are a few frameworks out there that may be candidates but a lot try to be flashy instead of just providing sensible, usable defaults. [purecss](http://purecss.io) is along the right track but the customizing is still not fun (you shouldn't have to recreate an entire style sheet just to select which colours you want). This is of course a limitation of CSS (no variables and no conditionals), but maybe CSS will grow more powerful in the future.

I would also hope that creating new HTML elements might become a thing, and will be as easy to use as LaTeX elements.

I dream of a day where I can write HTML quickly, and throw up a webpage without it looking like crap. I hope I may see this day soon. (maybe HTML6 or CSS4?).
