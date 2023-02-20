namespace OOP_Lab1;

public static class Program
{
	public static void Task1()
	{
		// initializing student ID
		const int studentId = 13479574;

		// initializing c variables
		int c2 = studentId % 2;
		int c3 = studentId % 3;
		int c5 = studentId % 5;
		int c7 = studentId % 7;
		int c = c3;

		// printing the variables
		Console.WriteLine($"Student ID: {studentId}");
		Console.WriteLine();
		Console.WriteLine($"C2 = {c2}");
		Console.WriteLine($"C3 = {c3}");
		Console.WriteLine($"C5 = {c5}");
		Console.WriteLine($"C7 = {c7}");
		Console.WriteLine($" C = {c}");
		Console.WriteLine();

		// getting input for a, b and n until a will be bigger than -c or n will be less than -c
		// (to avoid zero division in loop)
		long a, b, n;
		do
		{
			Console.WriteLine($"Enter variables (a must be bigger than {-c} or n must be less than {-c}):");
			a = InputHelper.GetInput<long>("Enter a", long.TryParse);
			b = InputHelper.GetInput<long>("Enter b", long.TryParse);
			n = InputHelper.GetInput<long>("Enter n (bigger than or equal a and b)",
				long.TryParse, n => n >= a, n => n >= b);
			Console.WriteLine();
		} while (-c <= n && a <= -c);

		// computing sum
		Console.WriteLine("Sum:");
		double s = 0.0;
		for (long i = a; i <= n; i++)
		{
			for (long j = b; j <= n; j++)
			{
				s += (double)(i - j) / (i + c);
			}
		}
		Console.WriteLine("S = " + s);
	}

	public static void Task2()
	{
		// initializing constants
		const int gradebookId = 1618; // gradebook ID
		const int maxSize = 19; // max acceptable matrix size

		// initializing c variables
		int c5 = gradebookId % 5;
		int c7 = gradebookId % 7;
		int c11 = gradebookId % 11;

		// printing the variables
		Console.WriteLine($"Gradebook ID: {gradebookId}");
		Console.WriteLine();
		Console.WriteLine($"C5 = {c5}");
		Console.WriteLine($"C7 = {c7}");
		Console.WriteLine($"C11 = {c11}");
		Console.WriteLine();

		// getting input for matrix size
		int size = InputHelper.GetInput<int>($"Enter matrix size (bigger than 0 and less than {maxSize})", int.TryParse,
			size => size > 0, size => size < maxSize);
		Console.WriteLine();

		// generating and printing matrixes A and B
		var a = Matrix.GenerateAndPrintMatrix(size, "A");
		var b = Matrix.GenerateAndPrintMatrix(size, "B");

		// computing and printing matrix C
		Console.WriteLine("Matrix C = A ^ B:");
		var c = a ^ b;
		Console.WriteLine(c);
		Console.WriteLine();

		// computing and printing sum of matrix C rows minimum values
		Console.Write("Sum of matrix C rows minimum values: ");
		Console.WriteLine(c.GetSumOfRowsMinValues());
	}

	public static void Main()
	{
		// initializing bool variable for detecting if exit from program is needed
		bool needExit = false;

		// initializing actions map
		var actions = new Dictionary<char, (string Description, Action Action)>()
		{
			['1'] = ("start Task 1", Task1),
			['2'] = ("start Task 2", Task2),
			['e'] = ("exit", () => needExit = true)
		};

		// infinitely ask for input
		while (true)
		{
			// printing all available action choises
			foreach (var action in actions)
			{
				Console.WriteLine($"Press {action.Key} to {action.Value.Description}");
			}

			// getting input for choise
			char c = InputHelper.GetInput<char>("Enter your choice", char.TryParse, c => actions.ContainsKey(c));
			Console.WriteLine();

			// starting chosen action
			actions[c].Action();
			Console.WriteLine();

			// exitting from main loop if exit is needed
			if (needExit)
			{
				break;
			}

			// waiting for pressing any key and clear console
			Console.Write("Press any key to continue...");
			Console.ReadLine();
			Console.Clear();
		}
	}
}
