Creating web backends quickly with style
===

So you are assigned as the developer in a brand new startup. You're application/website is going to change the world, and you just need an MVP to prove it to the investors.

You grab you're jquery, grab a bootstrap template, and boom the front end is done. Now to just do the backend. This is the tricky part.

If you've never really worked with backends before it's very easy to get lost in all the talks of databases, ACID, SQL, noSQL and what the hack else. You don't have time to worry about all that, you just gotta quickly build a backend.

This series is aimed at anyone who wants to quickly set up a backend system. You don't need any knowledge of back-end technologies or databases. You will need to know how to program, because this series will not focus on that. Basic understanding of HTTP, especially what `POST` and `GET` mean will help.

I will assume you've create a mobile application, or single page web application, or something similar. The client is isolated from the server, and just needs to communicate with the server to do stuff.

Installing stuff
---

You're probably expecting me to tell you to install the latest fad in server-side scripting languages, and the sexiest new NoSQL database engine. After all these tools are all about quickly setting up a backend with very little effort.

The problem is, once you have created your product with one of these quick and dirty solutions, it's very hard to scale up, and very hard to switch to more mature products.

Instead I want you to install Visual Studio 2013. Get all the latest updates as well (as of the time of writing, Update 2 is still in RC, so make sure you download that separately). It will take several hours to download and install, but I promise it will be well worth your time. So set the installer, then go and watch a few firefly episodes and come back here when you're done.

I'm not going to walk you're hand through the installation since I assume you know how installers work, so for the rest I'll assume you've installed it all correctly.

Web Project
---

If you start a new project in Visual Studio you will be prompted to select from many different templates. Just go to C#->Web and then select the only template.

> Microsoft recently simplified the project templates for web, so be thankful this process is easy now

Name and it move onto the next step. You'll be presented with a bunch of options. There are 4 areas here. The top left is templates, select Web API for this. The bottom left is some customization about what to include, just leave the bare minimum checked and make sure unit tests aren't turned on yet.

> Pre-emptive response to those that are screaming right now about the lack of unit tests. TDD is great for some scenarios, but it's just too much overhead for an MVP, not too mention a blog post.

On the top right is authentication, make sure that's no authentication (if you'd like authentication you can turn it on, but it's more focused on complete web systems, not a quick, lean API).

On the bottom right is Windows Azure set up. If you've created an Azure account you can tell Visual Studio to instantly create a deployment site for you, but I won't cover that in this series (I'll do a later series on the awesomeness that is Azure in Visual Studio Update 2). If you have Azure set up already, might as well tell it to create a site for you, but don't worry if you don't.

Click okay and Visual Studio will generate the skeleton of the application for you. We can safely ignore most of it, but

Our Application
---

As the example we're going to use a social media application. We can show how easily we can set up a basic system, but please note that this system would need a lot of work before getting into a real product (specifically the security would need to be a whole lot better).

Our application will consist of users, public posts and private messages. We're set up everything we need on the back end to make our MVP work, and from there we will have a good starting point for our real system.

Adding what we need
---

We can start out by creating a methods for viewing and updating profiles. First we'll need to add a controller, which will group all the methods relating to profiles together.

> Many people will talk about REST or proper MVC, and this design will violate all their rules, but it's an MVP so best practices don't have to be followed right away.

Right click the controllers folder, go to add new, and then select controller. Again you'll be presented with a bunch of templates, just choose Web API 2 Controller - Empty for now. Click add and then provide a name, `ProfileController`. 

We'll need to create 2 methods. `Update` and `View`.