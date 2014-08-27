What is LaTeX?
===

LaTeX is a great markup/typesetting language that produces professional documents for almost no work. It also allows for typesetting mathematical formulas or arbitrary equations. It is a critical to know for anyone in a technical field that plans on writing papers, or any undergrads wanting to impress profs with their assignments.

LaTeX must be really hard
---

If you're thinking LaTeX is really hard, then you'll be glad to know that you are wrong. LaTeX certainly has it's quirks, and if you plan on becoming a LaTeX master you will have to study ancient tomes, and be prepared to learn for years. You don't need to be a LaTeX master to create awesome documents though, you just need to know a few tricks, this post will hopefully get you on your way with LaTeX in no time.

Purpose
---

The point of this quick introduction to LaTeX is to teach you enough about LaTeX to start writing documents, it avoids some of the technical points and history contained in other guides, and focuses on the basics. After reading this guide, you should have all the skills necessary to create your own beautiful documents and impress your professors.

Command Structure
---

The basic structure of a LaTeX command is one of the following:

    \command{argument}[optional argument]

or

    \begin{environment}{argument}[optional argument]
    %stuff
    \end{environment}

The first structure is where you have the name of the command, and you pass it arguments. Required arguments are passed in curly braces `{}` and optional ones are passed with square brackets `[]`. You can think of this as a function call if you'd like. Essentially the program runs through and replaces these commands with the text in the command definition.

The second structure creates an environment between the begin and end commands. This can be nearly anything, from creating a table, to formatting the internal section as code, or mathematical expressions. The important thing is that this can actually change the way the document is read, so normal LaTeX rules don't neccessarily apply here. Don't worry about that too much though, we'll come back to that later.

You can also create comments in LaTeX by using `%`. Anything after that is a comment like below:

	\command %this is a comment next to the command

Now that you know how the commands really work, let's dive into creating a document.

Installation - blah, blah, blah
---

The first step is to install LaTeX. You have to choose what package you want to install, since there are many different editors available. Personally on windows I like [Texworks](http://www.tug.org/texworks/) but you can feel free to install whichever you want, the differences are just in the editor, not in the actual documents you'll write.

Since there's so many different environments available, I'm not going to help with the installation. It should be a fairly straight forward process, but if you have difficulty just ask around on forums, or in the comments below.

Skeleton
---
Below is a skeleton for a basic LaTeX document. If this overwhelms you don't worry. You don't have to understand most of this stuff, you just have to change the title and the author and start writing between the `\begin{document}` and `\end{document}`

	\documentclass[11pt]{article} % use larger type; default would be 10pt

	\usepackage{geometry} % to change the page dimensions
	\geometry{a4paper} % or letterpaper (US) or a5paper or....
	\usepackage{graphicx} % support the \includegraphics command and options
	\usepackage{amsmath} % better math environment
	\usepackage{verbatim} % adds environment for commenting out blocks of text & for better verbatim
	
	\title{Sample Document}
	\author{Nathan Jervis - mirhagk}

    \begin{document}
	\maketitle
	\tableofcontents

	%content goes here

	\end{document}

The first thing the document has is a declaration of what kind of thing you are making. Since LaTeX supports creating scientific papers as well as other things like books, this tells it what you are making. For short papers, assignments, essays etc, the article class is good, and you can just leave it at that. It also sets the font size, increases it from the default 10pt to 11pt, this isn't neccessary, but some people find 10pt a little too small.

Then it grabs a whole bunch of useful packages, that we will end up using later. You may notice it also has `\geometry{a4paper}` which simply tells LaTeX what size the paper is.

Title and author follow, which is simply the title of the paper, and the author. 2 tips here, first you can use the `\and` command if you have multiple authors and want it to be formatted nicely in the author section. Use it like this `\author{john doe \and jane doe}`. The 2nd tip is that you can include the `\date` command if you want to specify a different date than today (or you can leave it blank to leave out the date). Mostly this isn't needed, since you want the document to be dated the day when you last compiled it, but if you find you need it you simply use it like this: `\date{December 11, 2013}`.

Then the actual document starts. The first two commands create the title (nicely formatted name) and the table of contents. If you don't want a table of comments just remove that line (or comment it out).

After that is your actual document. You can start writing here, but first we need to explain some useful commands for writing, and some quirks of writing with LaTeX. Before we dive into this, let's get this sample up and running. Copy this into a new document `hello_world.tex` and include the following section where the `%content goes here%` comment is:

	\section{Introduction}
	Hello World!

Once you have that all ready to go, simply compile your document (depending on the environment you may need to actually compile a few times). Then you should see your first document, with the section heading right there and everything automatically pulled into your table of contents. Again you can remove the table of contents if you don't like it, it's mostly useful for longer documents, or things that you may jump around with.

LaTeX rules
---

So first thing's first. There are a few rules:

1. Whitespace doesn't really matter. If you were to write 
 <code>Hello&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;World!</code> 
 it would still only have 1 space in the output. This is similar to HTML and markdown. In order to get a new line you actually need to have an empty line between paragraphs. You can also use the `\\` command, which inserts a newline.
1. Certain symbols are special in LaTeX, in order to use the actual symbol (for symbols like %, $, and &) you need to escape it with a backslash like so `\% \$ \&`
1. LaTeX should always be written with the macro, `\LaTeX` so that it's formatted nicely. This isn't actually a rule, but you should still do it.

There are some exceptions to these rules, in certain environments, but we can get to those later.

Sections
---

The most useful command you'll use in LaTeX is the `\section` command. With it you specify a section title, and it'll format that section title for you, and include it in the table of contents. You can also create subsections with the handily named `\subsection` command (and if you really want you can create `\subsubsections` as well). It's use is very simple:

	\section{Introduction}
	Lorem ipsum
	\subsection{Background}
	more content

Try it out, and you'll see how easy it is.

Conclusion of part 1
---

You now should know enough to create most simple LaTeX documents. You can't do any formatting of text, or mathematical expressions, but those will come next. Then we will end off with some advanced tools, like inserting images, tables, lists, links and code.

I hope you've learned something, and realized that LaTeX is not that hard to learn, and really quite beautiful. I love not having to do any formatting work myself, just counting on the fact that LaTeX knows what it's doing and can make my document awesome.

If you have any problems, feel free to post in the comments below, or contact me for more help.