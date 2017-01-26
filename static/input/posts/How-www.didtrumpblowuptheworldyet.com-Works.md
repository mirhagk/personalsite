Title: How www.didtrumpblowuptheworldyet.com works
Published: 1/26/2016
Tags: trumpblowupworldyet
---


As some of you may know, I am the developer behind [http://www.didtrumpblowuptheworldyet.com (DTBUTWY)]() which is a webpage that answers the most important question on everyone's mind. Namely, is the world still alive now that Trump is the President of the United States.

As a service that many people rely upon, you will of course be wanting to know how it all works, and making sure that this isn't just another lie.

How will I tell when countries stop existing
---

We use a service, uptimerobot, that tracks whether websites are reachable. Using this we track that each country's main government website is up and running. (As everyone knows Canada is not-so-secretly run by the curling association, which is why you see that instead of the pretend federal government). It checks the website every 5 minutes and immediately notifies me if the website is down, in which case I will immediately verify and update the website to notify you dear citizen. 

The updating of the website is not fully automatic, the reason is that I want to verify that a country is indeed blown up and it's not just a case of the government forgetting to pay their hosting bill. I don't want this website to be the cause of your undue panic and eventual rioting just because we got a false positive. 

How is the Site Hosted
---

The plan with this website is to become **the** place for you to verify the world is still alive. In order to do that I needed to be sure that the website itself does not go down or even slow down. Imagine the panic if you tried to reach DTBUTWY  and the website didn't respond. You might assume the world has indeed been blown up, and start riots in the streets. To prevent this occurrence I have taken steps to minimize my chance of downtime.

There is no back-end webserver, just a static website (that is updated manually), so I don't need to have a private webserver, I just need a location to host my files, with a way to easily update it, and point a URL to it. To this end I've decided to use Google Cloud Platform's Cloud Storage. This supports custom DNS, and after a bunch of fiddling with proving that I owned a domain and setting up the appropriate rules I managed to get it to point to the right location. I can be fairly confident that a website hosted on google's platform is going to handle whatever load the world can throw at it. 

Plus as a bonus, if the website does go down, that means google might be down, and that's basically the end of the world anyways.

The cost to host using google cloud storage is very reasonable. The website itself is only 3.2Kb, which costs <$0.01 to store. The bandwidth likewise will be very low, so the only cost there is the actual `GET` operation itself which is `$0.004` per 10 000 requests. So every million requests will cost roughly `$0.40`, making it very unlikely that it's going to cost me too much.

Where did the idea come from?
---

The wonderful and lovely [Kevin Archibald](https://twitter.com/KevinOfDundas) came to me on inaugration day just after Mr. Trump's speech (I guess we should say Mr. President now) and said 

> quick register didtrumpblowuptheworldyet.com

And I of course obliged. I knew exactly what needed to get done. We collaborated to decide how the site should look, which countries we should check and other important details. Very soon afterwards we launched the website to the world.

Why are there ads?
---

The number 1 reason is fear. If DTBUTWY goes super popular and it suddenly becomes every single citizens homepage, I don't want to pay the resulting thousands in hosting costs. So to offset this I've added google adsense which should bring in more money than it's costing me (although not enough that I will be able to retire sadly).

How Can I Help?
---

If you'd like to help there are several ways you can help. The first is simply by spreading the word. Tell your friends, your internet colleagues, your frenemies and your pets. Everyone should hear about this site, and have their mind put at ease because they no longer have to worry about whether the world still exists.

If you believe that Trump is going to cause problems, you can help by donating money to causes you think are at risk. I'd rather not get all political here, but you know which groups might be in danger. And everyone knows that Canada can only survive through the kindness of your donations (How else do you think this free healthcare works?).

Lastly you can help by tweeting this to @realDonaldTrump. My personal goal with this site is to get a response from the president, saying something like

> www.didtrumpblowuptheworldyet.com is totally NOT FUNNY. Overrated. SAD!

If I get that then I will be a happy man (and don't worry, Trump could see and read this and still make that tweet. The man isn't well known for his ability to keep his opinions to himself).

