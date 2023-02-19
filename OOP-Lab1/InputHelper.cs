namespace OOP_Lab1;

// helper class for handling console input
public static class InputHelper
{
	// represents the method, that tries to convert string to certain type 
	public delegate bool TryParseHandler<T>(string? s, out T result);

	// gets input from user until input will be correct and pass all the conditions
	public static T GetInput<T>(string message, TryParseHandler<T> tryParse, params Predicate<T>[] conditions)
	{
		T res;
		string? input;
		do
		{
			Console.Write($"{message}: ");
			input = Console.ReadLine();
		} while (!tryParse(input, out res) || !conditions.All(condition => condition(res)));
		return res;
	}
}
