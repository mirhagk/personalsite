Async/Await - Why it's the best thing ever
===

Okay so that might be a little bit of an over-exaggeration but async/await keywords in C# 5 are a pretty cool an unique feature that makes asynchronous programming a whole lot easier. I'll explain what they are, how to use them, and what makes them awesome.

Blocking I/O
---

Let's say you're making a windows form application, and at some point it loads something from the hard drive. Well the hard drive is rather slow, and the loading could take a few seconds. What does your application do in the mean-time? Well the easiest thing to do is just sit there and wait until the hard drive is done, which means nothing else can happen. If the user presses the `Minimize` button on your window, then the application can't deal with that while it's waiting, and this is very bad.

Another very common way to deal with it (although not so much in windows forms) is to do other work, but keep checking if it's done yet. This is fine, but the code will look something like this:

	while(!file.DoneLoading)
	{
		DoOtherWork();
	}
	var contents = file.Load();

and you can't jump around your code too much. A big downside is that you can't release control of the thread to other parts of your application, so you still can't respond to the `Minimize` command the user sent. So while we're no longer wasting time (since we are doing work), we still are essentially blocked right here.

Background Threads
---

What you can do is create a background thread to do this work so you don't block the main thread. While this is certainly an option, it's quite a lot of work, and you're still basically wasting time, just not the main thread's time. A better option has arisen:

Callbacks to the Rescue
---

Callbacks are like saying "Hey, hard drive do this, and when your done, then tell the CPU to do this". You pass a function to the blocking I/O call, and when it's done, it'll call that function for you. It looks something like this:

	void UpdateData()
	{
		LoadDataBegin(FinishedLoading);
		return;
	}
	void FinishedLoading(IAsyncResult result)
	{
		var data = LoadDataEnd(result);
		DoSomething(data);
	}

Alright so now the `UpdateData` function returns the UI thread immediately, so it can respond to the `minimize` command from the user. Then once the data is loaded the `FinishedLoading` function will be called. This is slightly complicated, but it works. Well mostly.

The problem is we are probably loading that data to do something with the UI, but this callback is called on a background thread, so it doesn't have access to the UI. Luckily the form has a `Invoke` function where you can then call a function to be executed on the UI thread, like this:

	void UpdateData()
	{
		LoadDataBegin(FinishedLoading);
		return;
	}
	void UpdateUI(Data data)
	{
		form.data.Update(data)
	}
	void FinishedLoading(IAsyncResult result)
	{
		var data = LoadDataEnd(result);
		form.Invoke(UpdateUI,new Object[] {data});
	}

Okay great now it all works as we wanted, and relinquishes control to the UI thread, then gets the UI thread back when it needs it. But this is slightly more complicated than our original UpdateData:

	void UpdateData()
	{
		var data = LoadData();
		form.data.Update(data);
	}

And by slightly more complicated I mean the callback version is straight up awful. It could be made better with some lambdas, but it still wouldn't be pretty at all, and this is a fairly common thing to happen in windows forms (and asp.net servers too), so wouldn't it be better if we had a cleaner way of doing it.

Async/Await
--

You've been awaiting this moment for a while now (haha, await, get it?). Async await is basically a way to write this style of pseudo-code:

	UpdateData():
		var data = GetData()
		while we wait go back to the main thread and do other things
		form.data.Update(data)

Basically you can tell the compiler that something might take a little while, and the person who called this function can go ahead and do other things while it's waiting. The function would be written as:

	async void UpdateData()
	{
		var data = await GetDataAsync();
		form.data.Update(data.Result);
	}

When your code gets to the await part, then it starts getting the data, and returns control of the UI thread to the person who called this, just like before. Now the `minimize` action can be handled correctly. Once the data becomes available, then your code asks for the thread and finishes up the rest of the code, running on the main thread.

If you look at this method and compare it to the simple blocking method above, it's really not any more complicated. You have 2 additional keywords, but other than that they are identical. But the 2nd one runs asyncronously and lets the computer do other stuff while it's waiting. 

This is the beauty of async/await. You write essentially the same code, but you get all the benefits of the complicated callback process above. It lets you think in normal program flow, but not get stuck with the blocking that comes along with that.


ASP.NET
---

ASP.NET can also use async/await, and it does something rather neat.

You have a limited number of threads for the server, and threads are kinda expensive. So if a request is waiting, you can use the async/await style and that thread can be re-used for another request. Once the initial request loads what it needs to load, it will continue by grabbing a free thread. Note that this could even be a completely different thread. It basically just lets you free up the thread for use by another request, which is pretty great.

Things to remember
---

async/await is not about multi-threading. In fact for windows forms the code gets executed all on the same thread. It's about telling the caller of the function that you are waiting for something, so they can do something else (or they can wait on you and tell their caller they are waiting). It barely changes the way your code works, and in return you don't have to waste the main threads time waiting for a file to load, or the network to connect, or one of the many tasks that could take a very long time. It lets the main thread respond to more actions while you're waiting for stuff.

Don't you ever get annoyed when you do something and then try to minimize the window or something and you get a "program not responding" message? Isn't it super annoying? Well async/await is here so that the programmer can avoid having those ever come up, without having to work very hard.

So if you are ever going to do something that takes any real amount of time while making an application, make sure you use async/await so that the application can still respond and you'll make your users happy. 