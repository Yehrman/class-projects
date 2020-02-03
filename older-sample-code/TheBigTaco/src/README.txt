The Big Taco is a fictitious program created for a fast food restaurant to manage
their PoS (Point of Sale) transactions.  This program is full of bad architecture
decisions that have made it difficult for The Big Taco to keep up with the
changing demands of the industry.  We'll use the examples in this project together
with the timeline below as we work our way through the principles of good OO
analysis and design.

(NB: We'll also review a similar fictitious program for Johnny's, a burger joint
that was created following best practices of analysis and design)

Please keep in mind this is a sample program - it was intentionally coded with
problems in interaction design and architecture.  You should not use as an
example of "best practices".  If you find any actual bugs in the program, please
open an issue on github.



TIMELINE

	Nov 1980 - v1 - Text based ordering system with very brief requirements
	    1. Take text orders from the cashier
		2. Output the order total
		3. Display pending orders to cooks

	Jan 1981 - v1.1 - Management wants to see reports of what's going on in the store - new requirements
	    4. Display order history (indicate if the order is pending or not)

	Jan 1981 - v1.2 - The reports are hard to use - new requirements
	    5. Sort orders in the history report by date and time
		6. Show a summary of all orders on the last line

	Dec 1981 - v2 - The program is working great so far, but someone seems to be stealing from the till - new requirements
	    7. Require cashiers to enter their name when they take an order
		8. Create an accounting report that indicates how much money should be in the till at the beginning and end of each shift

	Jan 1982 - v2.1 - We already fired three people for stealing, but money is still disappearing!  What's going on? - new requirements
	    9. Issue passwords to verify employees are who they say they are when they take an order
		10. Keep track of incorrect passwords and lock out the workstation if more than 5 failures
		11. Only a shift leader can unlock a workstation

	Jun 1982 - v2.2 - We thought the system was working! In the first week every terminal got locked a few times, but then it stopped
	at first, the theft stopped too, but then last month theft shot up and it seems 4 of our employees were involved!  We checked the
	password logs and something's fishy - every one of the 4 employees has been logging in with the wrong password to different terminals
	several times a day.  We're determined to catch the thief! New requirements
	    12. Lock a workstation if there are 5 failures in a row - even if it's for different uers
		13. Lock a user if there are 5 failures in a row - on any workstation
		14. If a user is already logged in and a different workstation tries - "pretend" to let them in, but notify the manager

	Mar 1983 - v2.3 - All the work you did last year paid off.  We caught our star cashier using other workers login name and guessing
	passwords a few weeks after v2.2.  But, all the passwords are difficult for our employees to manage.  We want to switch to using
	magnetic key cards.  We ordered the keycards and keycard readers.  You'd better make it possible to use the password as well in
	case the employee forgets his keycard. New requirements
	    15. If a user swipes a keycard, use that to authenticate them. In this case, no username is needed
		16. Otherwise, stick with the username / password design we've had until now