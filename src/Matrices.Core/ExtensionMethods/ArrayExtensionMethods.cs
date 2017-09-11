using System;
// ReSharper disable UnusedMember.Global

namespace Matrices.Core.ExtensionMethods
{
    public static class ArrayExtensionMethods
    {
        public static int GetWidth<T>(this T[,] array)
        {
            return array.GetLength(1);
        }

        public static int GetHeight<T>(this T[,] array)
        {
            return array.GetLength(0);
        }

        public static double[] Add(this double[] left, double right)
        {
            var array = new double[left.Length];
            Array.Copy(left, array, left.Length);

            for (var i = 0; i < array.Length; i++)
            {
                array[i] += right;
            }

            return array;
        }

        public static double[] Subtract(this double[] left, double right)
        {
            var array = new double[left.Length];
            Array.Copy(left, array, left.Length);

            for (var i = 0; i < array.Length; i++)
            {
                array[i] -= right;
            }

            return array;
        }

        public static double[] Subtract(this double[] left, double[] right)
        {
            if (left.Length != right.Length)
            {
                return left;
            }

            var array = new double[left.Length];

            for (var i = 0; i < array.Length; i++)
            {
                array[i] = left[i] - right[i];
            }

            return array;
        }

        public static double[] Multiply(this double[] left, double right)
        {
            var array = new double[left.Length];
            Array.Copy(left, array, left.Length);

            for (var i = 0; i < array.Length; i++)
            {
                array[i] *= right;
            }

            return array;
        }

        public static double[] Multiply(this double[] left, double[] right)
        {
            if (left.Length != right.Length)
            {
                return left;
            }

            var array = new double[left.Length];

            for (var i = 0; i < array.Length; i++)
            {
                array[i] = left[i] * right[i];
            }

            return array;
        }

        public static double[] Divide(this double[] left, double right)
        {
            var array = new double[left.Length];
            Array.Copy(left, array, left.Length);

            for (var i = 0; i < array.Length; i++)
            {
                array[i] /= right;
            }

            return array;
        }

        public static double[] Modulate(this double[] left, double right)
        {
            var array = new double[left.Length];
            Array.Copy(left, array, left.Length);

            for (var i = 0; i < array.Length; i++)
            {
                array[i] %= right;
            }

            return array;
        }
    }
}
