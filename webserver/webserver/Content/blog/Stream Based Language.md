Streams and functions

Everything is a stream, and most processing takes place on streams. Functions take in a single type and return a single type, which means any function can be used to operate on a stream, so long as the types match.

If you want a function with multiple inputs (or outputs) it will simply take in a tuple. Tuples are a first class citizen though, and 


	Parse (string input): int
		result = 42
		return
	
	Main(): void
		let lines = Console.Input.Split('\n')
		Console.Output << Parse << lines

Visual

	Main
		-> lines - Console.Input.Split('\n')
		
		Console.Output << Parse << lines