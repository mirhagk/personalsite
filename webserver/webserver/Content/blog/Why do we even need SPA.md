Why do we even need SPA?
===

Single Page Applications signal the biggest failure in the history of the web.

Don't get me wrong, I do understand why people build SPAs, it's because making new requests is slow. SPAs give websites the feel of a native application, so you can interact with them without worrying about the page refreshing as a new page.

My question is why do we need SPA? Why is there such a performance gap, and why does it make it feel more native? To see why let's dive into SPAs.

Architecture of Single Page Applications
---

Each SPA is slightly different in how it operates, but there are common features among all of them.

###Routing

Many SPAs implement routing, implemented as modifying the url after the `#` to move around on the same page. Usually travelling to this link fires off an event, and the SPA library takes over and renders the content for you instead.