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

Now that you know how the commands really work, let's dive into creating a document.

Installation - blah, blah, blah
---

The first step is to install LaTeX. You have to choose what package you want to install, since there are many different editors available. Personally on windows I like [Texworks](http://www.tug.org/texworks/) but you can feel free to install whichever you want, the differences are just in the editor, not in the actual documents you'll write.

Since there's so many different environments available, I'm not going to help with the installation. It should be a fairly straight forward process, but if you have difficulty just ask around on forums, or in the comments below.

Skeleton
---
    \begin{document}
	\end{document}