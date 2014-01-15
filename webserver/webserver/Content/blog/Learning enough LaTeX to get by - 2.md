Formatting and Math
--

In this segment of learning enough LaTeX to get by, we'll go over math and formatting. Mathematical equations are supported so well in LaTeX, and so poorly in other systems, that almost every mathematical paper you see is probably created with LaTeX. It also has a very useful approach to formatting, which makes it appealing to both people who want fine-tuned control, and those who don't want to deal with formatting, just want it to look good.

Mathematical Equations
---

You can start writing mathematical equations by just surrounding some math with `$`. For example:

	$1 + 1 = 2$
will appear formatted in math mode inline. If you change that to

	\[	1 + 1 = 2 \]

Then it does a similar thing, but it actually uses an equation mode, which will put it on it's own line and number it (so you can refer to it later).