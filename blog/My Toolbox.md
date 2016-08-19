My toolbox
====

Recently I switched jobs. The first thing you gotta do at a new workplace is get all your go to tools set up an installed. I figured this was a good opportunity to review what I use, and write a blog post about why I use what I use. So here goes


Visual Studio (Community Edition at least, Enterprise if possible)
---

This one is a no brainer. I'm a .NET dev so I live and breath Visual Studio. Community Edition is sufficient for the job, but there are some features (code lens for example) that make Enterprise Edition nice, so long as I have a way to get it without directly paying for it (employer for instance).

There's no reason not to use Visual Studio 2015 (or whatever is the latest). It doesn't have the same project incompatibilities with the previous version that used to be an issue, and you probably pay for the msdn subscription and get it for free. At the very least take Community Edition.

Chrome
---

Honestly at this point the biggest reason I use chrome is that I can sign in on a new device and get all my extensions, passwords, history and everything. I know the rest of the browsers now do this to, but it's the fact that it's already there in chrome that makes me continue to use it.

Hangouts
---

A simple instant messaging platform that works on the PC and my phone, it allows me to communicate with colleagues and discuss problems without being in the same physical room.

Markdownpad
---

A simple markdown text editor, it's what I'm using to write this post. The pro version is worth the money (relatively cheap anyways) as you can get github flavour markdown, which includes tables. 

LinqPad 5
---

LINQPad is a great tool that allows you to write C# (and LINQ) code in a sort of scratch pad. It allows you to connect to data sources (directly to the database, or using entity framework contexts) and write code to query them.

The premium version allows you to even edit the results of your query, and save those to the database. I find their explorer/editor much nicer than the one Sql Server Management Studio has built in, so again this is another purchase well worth the money.

Notepad++
---

A simple text editor, with basic language support (syntax highlighting). It's quick and a great tool to use just to quickly edit a file or two. I usually alias it to `npp` on my path so I can open files up quickly from the command prompt.

Visual Studio Code
---

Visual Studio Code is Microsoft's cross platform code editor, written on top of Electron. It has great extensions written by the community, and a surprising amount of integration with debuggers. It's closer to an IDE that it is an editor, and it feels quite polished already, even in it's infancy.

The C# support is pretty good, with full intellisense and the usual goto definition and find all references support. The tool still isn't a replacement for Visual Studio, but as a much more lightweight (and cross platform) tool it's great for quickly editing files and projects without waiting for Visual Studio to load. It's aliased as `code` on the path, so opening up the current folder in visual studio code is a breeze from the command line, and it has pretty decent git support as well. I plan to sort out setting it up as my diff tool from the command line, which would let me work easily from the command line. 

It also seems to be getting faster and faster as time goes on, so it's slowly replacing my need for notepad++ (where I used to reach for notepad++ I often find myself reaching for Visual Studio Code instead)

Mouse Without Borders
---

This tool is a Microsoft experiment, but still actually a great and useful tool. You set this up on multiple computers and it allows you to use a single mouse and keyboard to control all of them at once. You can even copy and paste and drag and drop files across them. If you ever find yourself with multiple computers on the same desk then you need to install this.

LaTeX (TexWorks)
---

LaTeX never ceases to amaze me. They got so much right with a tool so old.

It's a document typesetting tool/language, which is used for creating highly professional looking documents. It's very commonly used for writing mathematics/computer science research papers and textbooks, since it has amazing support for writing equations. 

It has by far the best implementation of semantic documents I've ever seen. You can easily define macros (which can call other macros etc) so you feel very natural writing stuff like

~~~
\newcommand{says}[2]{#1: "#2"}
\says{Bob}{LaTeX is pretty great}
\says{Steve}{I know, it's fantastic}
~~~

Which gets outputted as 

~~~
Bob: "LaTeX is pretty great"
Steve: "I know, it's fantastic"
~~~

The great part is now if you want to change how it looks when someone says something in your document you only need to change a single spot. I even use it for names of characters in books that I write (so that if I feel a character should be renamed I can do so easily with confidence).

It has a great package manager and a whole suite of useful packages to do pretty much anything. The documents it generate are highly professional, with years of research into what's the easiest to read format, and the best way to set each letter for optimal readability and look. (If you're familiar with the concept of kerning, this will be a dream tool for you).

That being said it's not without it's downsides. It supports different outputs, but the only really useful and reliable one is `PDF`. It's also quite heavyweight  so you're better off saving this for when you need professional documents (instead of just quick blog posts). Use Markdown for quick and simple documents, LaTeX for longer or more complex documents and you'll have quite the duo. Ditch that Microsoft Word license, and make your life much better.



Github Desktop
---

This is a tool developed by Github as a UI client for windows and mac. It's a fairly decent UI, but I personally much prefer Visual Studio's git UI client. 

The reason I get this tool is because the shell it comes bundled with is fantastic. It's a powershell shell (because powershell is great) and it includes posh-git, which gives a nice overview of the current git repo, and a credential helper, which saves you from having to specify username/password every time you push/pull.

Be careful though as the `git shell` tool can only be properly updated through the `github desktop` UI tool. So if you find it broken all of a sudden, open up the UI and let it do the extraction/update

Everything
---

Everything is a tool to find any file you need very quickly. It indexes the file system for you and provides a way to very quickly search across all of them, using regex

Papercut
---

Excel
---

Sql Server Management Studio
---