using System.Text;

namespace OOP_Lab1;

// helper class for generating and printing matrixes
public static class MatrixHelper
{
	// creates and fills the matrix with random numbers in range [min, max]
	public static sbyte[][] GenerateMatrix(int size, sbyte min, sbyte max)
	{
		if (size <= 0)
		{
			throw new ArgumentOutOfRangeException(nameof(size), "Size must be bigger than 0");
		}

		if (min > max)
		{
			throw new ArgumentOutOfRangeException(nameof(min), "Minimum value must be less or equal to maximum value");
		}

		var random = new Random();
		var matrix = new sbyte[size][];
		for (int i = 0; i < size; i++)
		{
			matrix[i] = new sbyte[size];
			for (int j = 0; j < size; j++)
			{
				matrix[i][j] = (sbyte)random.Next(min, max + 1);
			}
		}
		return matrix;
	}

	// prints matrix to console
	public static void PrintMatrix<T>(T[][] matrix)
	{
		// convert matrix elements to strings
		var rowsStrings = matrix.Select(row => row.Select(n => $"{n}").ToArray());

		// compute width of every column
		var rowsWidth = Enumerable
			.Range(0, matrix.Length)
			.Select(i => rowsStrings.Select(r => r[i].Length).Max())
			.ToArray();

		// convert matrix rows to their string form
		var rows = rowsStrings
			.Select(row => string.Concat(
				"    { ",
				string.Join(", ", row.Select((n, i) => n.PadLeft(rowsWidth[i]))),
				" }"));

		// create string form of matrix
		var sb = new StringBuilder();
		sb.AppendLine("{")
			.AppendJoin($",{Environment.NewLine}", rows)
			.AppendLine()
			.AppendLine("}");

		// print matrix
		Console.WriteLine(sb);
	}

	// gets input for pseudorandom elements range of matrix with specified size, generates and prints it
	public static sbyte[][] GenerateAndPrintMatrix(int size, string name, int alignment)
	{
		// get input for range of matrix elements generation
		Console.WriteLine($"Enter range [min, max] for matrix {name} elements generation:");
		sbyte min = InputHelper.GetInput<sbyte>("min", sbyte.TryParse);
		sbyte max = InputHelper.GetInput<sbyte>("max (bigger than minimum)", sbyte.TryParse, max => max >= min);

		// generate and print result matrix
		Console.WriteLine($"Matrix {name} with elements in range [{min}, {max}]:");
		var res = GenerateMatrix(size, min, max);
		PrintMatrix(res);
		return res;
	}
}
