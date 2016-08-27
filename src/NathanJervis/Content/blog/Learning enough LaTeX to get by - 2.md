Formatting and Math
--

In this segment of learning enough LaTeX to get by, we'll go over math and formatting. Mathematical equations are supported so well in LaTeX, and so poorly in other systems, that almost every mathematical paper you see is probably created with LaTeX. It also has a very useful approach to formatting, which makes it appealing to both people who want fine-tuned control, and those who don't want to deal with formatting, just want it to look good.

Mathematical Equations
---

You can start writing mathematical equations by just surrounding some math with `$`. For example:

	$1 + 1 = 2$


<br /><img src="http://latex.codecogs.com/gif.latex?1+1=2" border="0"/>

will appear formatted in math mode inline. If you change that to

	\[	1 + 1 = 2 \]

Then it does a similar thing, but it actually uses an equation mode, which will put it on it's own line and number it (so you can refer to it later).

Of course you can make much more interesting mathematical equations, such as:

	$3^{2} = 9$

<br /><img src="http://latex.codecogs.com/gif.latex?3^{2}=9" border="0"/>

(3 to the power of 2 equals 9).

Whatever you put inside the `{}` will be rendered as superscript. This is neat, but just scratches the surface. You can use subscripts as well:

	$x_{i} = 1 + 1$

<br /><img src="http://latex.codecogs.com/gif.latex?x_{i}=1+1" border="0"/>

You can also chain them together, check out this:

	$3^{2}^{2} = 81$

<br /><img src="http://latex.codecogs.com/gif.latex?3^{2}^{2}=81" border="0"/>

> Unfortunately the web-service I use to render these examples doesn't show these properly chained.

Functions are also supported by using the same notation as any other command in LaTeX.

	$\sqrt{x} = \sin{\pi}$

<br /><img src="http://latex.codecogs.com/gif.latex?\sqrt{x} = \sin{\pi}" border="0"/>

Every mathematical symbol you can think of is supported, and a good deal more. If you need a specific symbol, it's quite easy to google, or you can check [this list][list of latex symbols].

LaTeX can handle arbitrarily complicated equations such as this one:

	\mathop{\rm grd} \phi(z) =\left(a+\frac{2d}{\pi}\right) v_\infty\, \overline{f'(z)} =v_\infty \left[ \pi a + \frac{2d}{\pi a + 2dw^{-1/2}(w-1)^{1/2}} \right]^-

<br /><img src="http://latex.codecogs.com/gif.latex?\mathop{\rm grd} \phi(z) =\left(a+\frac{2d}{\pi}\right) v_\infty\, \overline{f'(z)} =v_\infty \left[ \pi a + \frac{2d}{\pi a + 2dw^{-1/2}(w-1)^{1/2}} \right]^-" border="0"/>

Bonus points to anyone who can tell me what this equation is demonstrating.


Formatting
---

Formatting is the other strong point of LaTeX. It allows you to easily write semantic documents, which allow you to quickly change the formatting without 



[list of latex symbols]: http://web.ift.uib.no/Teori/KURS/WRK/TeX/symALL.html