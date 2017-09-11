using System;
using Matrices.Core;

namespace Matrices.Application
{
    public static class Program
    {
        private static void Main()
        {
            while (true)
            {
                Console.Clear();
                Console.Write("Enter matrix 1 height: ");
                if (!int.TryParse(Console.ReadLine(), out var height))
                {
                    continue;
                }

                Console.Write("Enter matrix 1 width: ");
                if (!int.TryParse(Console.ReadLine(), out var size))
                {
                    continue;
                }

                Console.Write("Enter matrix 2 width: ");
                if (!int.TryParse(Console.ReadLine(), out var width))
                {
                    continue;
                }

                Console.Write("Enter max value: ");
                if (!int.TryParse(Console.ReadLine(), out var max))
                {
                    continue;
                }

                var matrix1 = new Matrix(height, size);
                var matrix2 = new Matrix(size, width);
                matrix1.FillWithRandomValues(max);
                matrix2.FillWithRandomValues(max);
                var matrix3 = matrix1 * matrix2;

                Console.WriteLine($"Matrix1: Height: {matrix1.Height} Width: {matrix1.Width}");
                Console.WriteLine(matrix1.ToString());
                Console.WriteLine($"Matrix2: Height: {matrix2.Height} Width: {matrix2.Width}");
                Console.WriteLine(matrix2.ToString());
                Console.WriteLine($"Matrix3: Height: {matrix3.Height} Width: {matrix3.Width}");
                Console.WriteLine(matrix3.ToString());

                Console.WriteLine($"Matrix3: Height: {matrix3.Height} Width: {matrix3.Width}");
                Console.WriteLine(matrix3.GaußJordan());

                if (Console.ReadLine() == "exit")
                {
                    return;
                }
            }
        }
    }
}
