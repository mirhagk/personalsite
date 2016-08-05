Good UX never has error messages. Good UX never has errors.
---

UX is User Experience, which compromises not only graphical user interface design, but also workflows and basically any sort of interaction a user has with a site.

Good UX is making the user feel positive about everything they do, and more concretely letting them do exactly what they want to do without them being stopped or slowed down in any way. Taking time to find the right button is slowing them down. Googling or asking someone else for help slows them down. It needs to be intuitive and friendly.

Another often overlooked way you could be slowing someone down is with error messages. When a user needs to enter their e-mail address and you tell them they forget the `@` sign they need to stop and go back and retype it. Or if you tell them their password isn't complex enough and they need to stop and rethink what their password is.<sup>1</sup>

A much better approach is to design the UX in such a way that the user doesn't make any mistakes. Preventing the user from making mistakes is faster than telling them what mistakes they made. For instance retrieving their e-mail address from a SSO partner would prevent them from having to enter it in the first place. For the password example it's much trickier but adding a password strength checker that colour codes the strength of their password will help them fix it as they type at least. And providing this before they re-enter their password is a must. At the very least you need to acknowledge that your password requirements are providing a sub-optimal UX and make sure you're okay with that (depending on security requirements you might be).

This is NOT meant to tell you to not write error messages, the worst UX you could have would be where the user has errors that they don't know about. This is meant simply to get you to think of ways of how you could redesign the feature to eliminate the chance for the user to make a mistake. 

<sup>1</sup> Another way to phrase the message "Your password must contain at least one upper case letter, one lower case letter, one symbol and one number" is "Please capitalize the first letter of your password and add 1! to the end" because they have the same effect