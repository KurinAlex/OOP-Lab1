using System.Text;

namespace OOP_Lab1;

// wrapper class for matrices
public class Matrix
{
	// inner matrix
	private readonly sbyte[][] _matrix;

	// matrix order
	private readonly int _order;

	// constructs matrix object from specified matrix
	public Matrix(sbyte[][] matrix)
	{
		// checking if matrix is square
		int n = matrix.Length;
		if (matrix.Any(row => row.Length != n))
		{
			throw new ArgumentException("Wrong number of dimentions", nameof(matrix));
		}

		// setting order
		_order = n;

		// row-by-row copying to inner matrix
		_matrix = new sbyte[n][];
		for (int i = 0; i < n; i++)
		{
			_matrix[i] = new sbyte[n];
			matrix[i].CopyTo(_matrix[i], 0);
		}
	}

	// computs sum of matrix rows minimum values
	public int GetSumOfRowsMinValues()
	{
		return _matrix.Sum(row => row.Min());
	}

	// prints matrix to console
	public override string ToString()
	{
		// convert matrix elements to strings
		var rowsStrings = _matrix.Select(row => row.Select(n => $"{n}").ToArray());

		// compute width of every column
		var rowsWidth = Enumerable
			.Range(0, _matrix.Length)
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
			.Append("}");

		// print matrix
		return sb.ToString();
	}

	// creates and fills the matrix with random numbers in range [min, max]
	public static Matrix GenerateMatrix(int size, sbyte min, sbyte max)
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
		return new(matrix);
	}

	// gets input for pseudorandom elements range of matrix with specified size, generates and prints it
	public static Matrix GenerateAndPrintMatrix(int size, string name)
	{
		// checking if specified size is bigger than 0
		if (size <= 0)
		{
			throw new ArgumentOutOfRangeException(nameof(size), "Size must be bigger than 0");
		}

		// getting input for range of matrix elements generation
		Console.WriteLine($"Enter range [min, max] for matrix {name} elements generation:");
		sbyte min = InputHelper.GetInput<sbyte>("min", sbyte.TryParse);
		sbyte max = InputHelper.GetInput<sbyte>("max (bigger than minimum)", sbyte.TryParse, max => max >= min);

		// generating and print result matrix
		Console.WriteLine($"Matrix {name} with elements in range [{min}, {max}]:");
		var res = GenerateMatrix(size, min, max);
		Console.WriteLine(res);
		Console.WriteLine();
		return res;
	}

	// matrixes xor operator
	public static Matrix operator ^(Matrix a, Matrix b)
	{
		// checking if matrixes orders are equal
		int n = a._order;
		if (n != b._order)
		{
			throw new ArgumentException("Matrixes orders are not the same");
		}

		// creating new matrix using element-by-element xor of matrixes a and b
		var res = new sbyte[n][];
		for (int i = 0; i < n; i++)
		{
			res[i] = new sbyte[n];
			for (int j = 0; j < n; j++)
			{
				res[i][j] = (sbyte)(a._matrix[i][j] ^ b._matrix[i][j]);
			}
		}
		return new(res);
	}
}
