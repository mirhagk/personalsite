Mirhagk's Personal Site
=========================

This is the source code for mirhagk's personal website hosted at [http://nathanjervis.com](). Read through the license for what you can do with this but basically you can take whatever you want, as long as you credit me, and don't steal my personal info or identity (that wouldn't be very nice)

Pushing to server
-----------------

This project uses azures continuous deployment from github. Essentially it watches the master branch, and any changes are automatically deployed to the server. This means that no breaking changes can be pushed to the master branch on github, instead a dev branch can be used to make breaking changes, then merge them back to server.

Parts
------------

The website itself is coded in javascript using EmberJS and bootstrap. All of the content is seperated from the website itself (ie my name, experience etc), which means you can take the website, remove the data files related to my information (`resume.js` and `blog.js` so far) and replace them with your own information, and you'll have a functioning site about you.

The nice part about the seperation is that it allows javascript applications to access the same data in a common way. This has prompted the creation of the resume generator. The resume generator takes the data from a javascript file and applies it to a template file, making a new file with that information in it. It does this in a very similar way to EmberJS handlerbars, which makes it even easier to use. This is how the pdf resume is generated for the website.

Feel free to use whichever parts you'd like, with the exception of the data about me, so long as you follow the license terms