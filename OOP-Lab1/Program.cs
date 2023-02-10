using System.Text;

namespace OOP_Lab1;

public class Program
{
    public static void Task1()
    {
        // initializing constants
        const int studentId = 13479574;
        const long a = 4;
        const long b = 5;
        const long n = 10;

        // initializing c variables
        int c2 = studentId % 2;
        int c3 = studentId % 3;
        int c5 = studentId % 5;
        int c7 = studentId % 7;
        int c = c3;

        // output variables
        Console.WriteLine($"Student ID: {studentId}");
        Console.WriteLine();
        Console.WriteLine($"C2 = {c2}");
        Console.WriteLine($"C3 = {c3}");
        Console.WriteLine($"C5 = {c5}");
        Console.WriteLine($"C7 = {c7}");
        Console.WriteLine();
        Console.WriteLine($"a = {a}");
        Console.WriteLine($"b = {b}");
        Console.WriteLine($"n = {n}");
        Console.WriteLine();

        if (-n <= c && c <= -a)
        {
            Console.WriteLine("Division by zero detected");
            return;
        }

        // computing sum
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

    public static byte[][] GenerateMatrix(int size, byte min, byte max)
    {
        if (size <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(size), "Size must be bigger than 0");
        }

        if (min > max)
        {
            throw new ArgumentOutOfRangeException(nameof(min), "Minimum value must be less or equal maximum value");
        }

        var random = new Random();
        var matrix = new byte[size][];
        for (int i = 0; i < size; i++)
        {
            matrix[i] = new byte[size];
            for (int j = 0; j < size; j++)
            {
                matrix[i][j] = (byte)random.Next(min, max + 1);
            }
        }
        return matrix;
    }

    public static void PrintMatrix<T>(T[][] matrix, string name, int alignment)
    {
        var rows = matrix.Select(row =>
            string.Concat("    { ", string.Join(", ", row.Select(n => $"{n}".PadLeft(alignment))), " }"));

        var sb = new StringBuilder();
        sb.AppendLine($"{name}:")
            .AppendLine("{")
            .AppendJoin(",\n", rows)
            .AppendLine()
            .AppendLine("}");

        Console.WriteLine(sb);
    }

    public static void Task2()
    {
        // initializing constants
        const int gradebookId = 1618;
        const byte min = byte.MinValue; // min value for random generation
        const byte max = byte.MaxValue; // max value for random generation

        // initializing c variables
        int c5 = gradebookId % 5;
        int c7 = gradebookId % 7;
        int c11 = gradebookId % 11;

        // output variables
        Console.WriteLine($"Gradebook ID: {gradebookId}");
        Console.WriteLine();
        Console.WriteLine($"C5 = {c5}");
        Console.WriteLine($"C7 = {c7}");
        Console.WriteLine($"C11 = {c11}");
        Console.WriteLine();

        // input matrix size
        int size;
        string input;
        do
        {
            Console.Write("Enter matrix size: ");
            input = Console.ReadLine()!;
        } while (!int.TryParse(input, out size) || size <= 0);
        Console.WriteLine();

        // generation of matrixes A and B 
        var a = GenerateMatrix(size, min, max);
        var b = GenerateMatrix(size, min, max);
        PrintMatrix(a, "A", 3);
        PrintMatrix(b, "B", 3);

        // computing matrix C
        var c = new int[size][];
        for (int i = 0; i < size; i++)
        {
            c[i] = new int[size];
            for (int j = 0; j < size; j++)
            {
                c[i][j] = a[i][j] ^ b[i][j];
            }
        }
        PrintMatrix(c, "C", 3);

        // computing sum of C's rows min values
        int s = c.Sum(row => row.Min());
        Console.WriteLine("s = " + s);
    }

    public static void Main()
    {
        // task 1
        Console.WriteLine("Task 1:");
        Console.WriteLine();
        Task1();

        Console.WriteLine();
        Console.WriteLine();

        //task 2
        Console.WriteLine("Task 2:");
        Console.WriteLine();
        Task2();

        Console.ReadLine();
    }
}