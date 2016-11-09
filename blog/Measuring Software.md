Measuring Software
====

In construction it's relatively easy to measure progress, you often have done the same or similar jobs, and you can measure many aspects of the job:

+ Progress - How close is that building to being complete?
+ Build Cost - How much do the materials and labour for the building cost?
+ Maintenance Cost - How much will it cost to maintain the building
+ Operating Cost - How much will it cost to staff and run the building
+ Inspection Results - What the results of a inspections say will tell the quality of the building

These are all metrics to check the cost, progress and quality of a construction project. Can we do the same for software?


The Need to Measure
----

First of all, what is the point of metrics? Well metrics show how a project is going, or how it went. It can be used to assess performance of individual contributors or to evaluate how different management or development processes affected the project.

If we don't measure the progress and quality of a software project then we can't know if the testing process is actually improving software quality, or if code reviews are effective. We need to measure so we can try out new methods, and abandon those that aren't effective.


Can We Measure?
---

There is an economic principle called [Goodhart's Law][goodhart]. It states

> When a measure becomes a target, it ceases to be a good measure

The minute a metric becomes known, then people attempt to optimize for that metric. Usually this means gaming the process in some way to give better results for the metric, even when this means worse results overall.

Fast Food
---

When I was in high school I worked for a fast food restaurant. During this period a lot of fast food restaurants were going through a crisis. The number of people sitting down inside of a fast food restaurant and eating was decreasing. The only consistent consumers were drive-thru customers.

If you've ever been through a drive-thru, you probably know how much of a difference it makes if it moves quickly or slowly. Nothing is worse than being stuck in a drive-thru line. In order to keep customers happy the fast food restaurant needs to move as quickly as possible.

To measure and encourage employees they introduced a new metric. When an order is entered into the system it's placed on a screen for the kitchen and front staff to see. When the front staff bags the food to hand to the customer the order is cleared from the screen. The amount of time it's on the screen is recorded and certain goals are set (average <45 seconds, 99% < 3 minutes)

Employees quickly discovered a trick. You see the system had an option to view orders from recent history (used so that if someone accidentally clears it they can still see what they need for the order). Taking an incomplete order, clearing it from the screen, and then pulling it up in the recent history was called "bumping" the order, and if it started getting close to going above our goal someone would call out "hey can you bump that order" and suddenly we'd meet our goal.

Meeting the metric quickly turned into ability to bump orders correctly. This not only invalidated the use of that as a metric, but even encouraged some negative behavior. People would serve fries that maybe sat too long, just to make sure that the time was met. Orders would get missed or screwed up because someone bumped 2 orders at a time (the history would only show one) or forget to pull it up on the history. Actually using the history for it's intended purpose while something was bumped would screw up even more orders.

This is just one example of how a metric that is used for evaluation is quickly and easily abused. But let's turn to software.

Bad Measurements
---

Over the years many, many, bad metrics have been invented and misused for evaluating software. Here's some examples, and how they get gamed by employees

###LoC - Lines of Code (more is better)
This is the classic bad measurement. Many studies have shown that the number of bugs increases relative to the number of lines of code. At best you get code bloat and bigger binaries, at worst you get buggy, incorrect softawre. Using lines of code as a metric rewards those who split up logic into multiple lines to an extreme, use extra unneeded local variables and just generally write overly complex code. It also encourages copy and paste programming, which introduces duplicate code, and discourages refactoring (which will improve code quality but often decrease lines of code).

### Number of bugs found/fixed (higher is better)

Rewarding the finding and fixing of bugs sounds noble, but it rewards teams who wrote bad software in the first place. It also discourages managing bugs effectively, doing things like marking bugs as duplicate (or overly marking them as duplicate if you count duplicate bugs in your metric).

It's the same problem that cities face with giving police forces quotas. If people simply aren't speeding, you shouldn't punish the police. It encourages them to forge tickets and discourages them from seeking to reduce crime.

If your testers can't find any bugs, because the software is top notch, then you don't want to punish the team, you want to reward them.

### Number of bugs (lower is better)
 
This one sounds better on the surface, but if there are any bugs that don't count (such as `won't fix` or `cannot reproduce` or `works as intended`) then you'll see bugs marked those values when it doesn't make sense. Or if the software gets handed off at all then those who manage it will pay for the mistakes of those who built it (this is a big reason why DevOps calls for those who built it to own it for the entire life). But even making those who built it pay for their mistakes isn't always appropriate, often times the mistakes come from the design/planning stage, or from pressure from management.

### Number of Commits

Again this encourages splitting up commits trivially, so this isn't a very good metric either.


A World Without Metrics
---



[goodhart]: http://en.wikipedia.org/wiki/Goodhart%27s_law