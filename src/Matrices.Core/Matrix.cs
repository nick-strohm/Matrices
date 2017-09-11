using System;
using System.Linq;
using System.Text;
using Matrices.Core.ExtensionMethods;
// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable InconsistentNaming
// ReSharper disable CompareOfFloatsByEqualityOperator

namespace Matrices.Core
{
    public class Matrix
    {
        #region Properties

        public int Height => _values.GetHeight();
        public int Width => _values.GetWidth();
        private readonly double[,] _values;

        public double this[int row, int col]
        {
            get => _values[row, col];
            set => _values[row, col] = value;
        }

        public bool IsCubic => Height == Width;

        #endregion Properties

        #region Constructor
        
        public Matrix() : this(1) { }
        
        public Matrix(int size) : this(size, size) { }

        public Matrix(int height, int width)
        {
            if (height < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(height), "Height is equal or less zero. It must be at least one.");
            }

            if (width < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(width), "Width is equal or less zero. It must be at least one.");
            }

            _values = new double[height, width];
        }
        
        public Matrix(double[,] values)
        {
            if (values.GetWidth() == 0 || values.GetHeight() == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(values), "Width or Height is equal or less zero. It must be at least one.");
            }

            _values = values;
        }

        #endregion Constructor

        #region Methods

        #region Values

        public double[] GetRow(int row)
        {
            var output = new double[Width];

            for (var i = 0; i < Width; i++)
            {
                output[i] = this[row, i];
            }

            return output;
        }

        public void SetRow(int row, double[] value)
        {
            for (var i = 0; i < Width; i++)
            {
                if (value.Length < i)
                {
                    break;
                }

                this[row, i] = value[i];
            }
        }

        public double[] GetColumn(int column)
        {
            var output = new double[Width];

            for (var i = 0; i < Width; i++)
            {
                output[i] = this[i, column];
            }

            return output;
        }

        public void SetColumn(int column, double[] value)
        {
            for (var i = 0; i < Width; i++)
            {
                if (value.Length < i)
                {
                    break;
                }

                this[i, column] = value[i];
            }
        }

        public void FillWithRandomValues(int max = short.MaxValue)
        {
            var rng = new Random();

            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    this[y, x] = rng.Next(max);
                }
            }
        }

        #endregion Values

        #region Operator

        public static Matrix operator +(Matrix left, Matrix right)
        {
            var width = Math.Max(left.Width, right.Width);
            var height = Math.Max(left.Height, right.Height);

            var matrix = new Matrix(height, width);

            for (var row = 0; row < matrix.Height; row++)
            {
                for (var column = 0; column < matrix.Width; column++)
                {
                    var leftVal = 0d;
                    var rightVal = 0d;
                    if (row < left.Height && column < left.Width)
                    {
                        leftVal = left[row, column];
                    }

                    if (row < right.Height && column < right.Width)
                    {
                        rightVal = right[row, column];
                    }

                    matrix[row, column] = leftVal + rightVal;
                }
            }

            return matrix;
        }

        public static Matrix operator -(Matrix left, Matrix right)
        {
            var width = Math.Max(left.Width, right.Width);
            var height = Math.Max(left.Height, right.Height);

            var matrix = new Matrix(height, width);

            for (var row = 0; row < matrix.Height; row++)
            {
                for (var column = 0; column < matrix.Width; column++)
                {
                    var leftVal = 0d;
                    var rightVal = 0d;
                    if (row < left.Height && column < left.Width)
                    {
                        leftVal = left[row, column];
                    }

                    if (row < right.Height && column < right.Width)
                    {
                        rightVal = right[row, column];
                    }

                    matrix[row, column] = leftVal - rightVal;
                }
            }

            return matrix;
        }

        public static Matrix operator *(Matrix left, Matrix right)
        {
            if (left.Width != right.Height)
            {
                throw new ArgumentOutOfRangeException(nameof(right), "Lefts width is not equal rights height");
            }

            var matrix = new Matrix(left.Height, right.Width);

            for (var row = 0; row < matrix.Height; row++)
            {
                for (var col = 0; col < matrix.Width; col++)
                {
                    for (var inner = 0; inner < left.Width; inner++)
                    {
                        matrix[row, col] += left[row, inner] * right[inner, col];
                    }
                }
            }

            return matrix;
        }

        public static Matrix operator /(Matrix left, Matrix right)
        {
            return left * right.GaußJordan();
        }

        public static bool operator ==(Matrix left, Matrix right)
        {
            return left?.Equals(right) == true;
        }

        public static bool operator !=(Matrix left, Matrix right)
        {
            return !(left == right);
        }

        public static Matrix operator ++(Matrix left)
        {
            return left + 1;
        }

        public static Matrix operator --(Matrix left)
        {
            return left - 1;
        }

        public static bool operator <(Matrix left, Matrix right)
        {
            return left.GetHashCode() < right.GetHashCode();
        }

        public static bool operator >(Matrix left, Matrix right)
        {
            return left.GetHashCode() > right.GetHashCode();
        }

        public static Matrix operator +(Matrix left, double right)
        {
            if (!left.IsCubic)
            {
                throw new NotSupportedException("This matrix is not cubic. Change its size to height=width");
            }

            var matrix = new Matrix(left.Height, left.Width);
            for (var i = 0; i < left.Height; i++)
            {
                matrix[i, i] = right;
            }

            return left + matrix;
        }

        public static Matrix operator -(Matrix left, double right)
        {
            if (!left.IsCubic)
            {
                throw new NotSupportedException("This matrix is not cubic. Change its size to height=width");
            }

            var matrix = new Matrix(left.Height, left.Width);
            for (var i = 0; i < left.Height; i++)
            {
                matrix[i, i] = right;
            }

            return left - matrix;
        }

        public static Matrix operator *(Matrix left, double right)
        {
            var matrix = left.Copy();
            for (var row = 0; row < matrix.Height; row++)
            {
                for (var col = 0; col < matrix.Width; col++)
                {
                    matrix[row, col] *= right;
                }
            }

            return matrix;
        }

        public static Matrix operator /(Matrix left, double right)
        {
            var matrix = left.Copy();
            for (var row = 0; row < matrix.Height; row++)
            {
                for (var col = 0; col < matrix.Width; col++)
                {
                    matrix[row, col] /= right;
                }
            }

            return matrix;
        }

        #endregion Operator

        #region Overrides

        public override string ToString()
        {
            var sb = new StringBuilder();

            // ReSharper disable once SuspiciousTypeConversion.Global
            var max = _values.Cast<double>().Max();
            var count = Math.Min(Math.Floor(Math.Log10(max) + 1), 3) + 2;

            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    sb.Append(string.Format($"{{0,{count}}} ", this[y, x]));
                }

                sb.Append("\r\n");
            }

            return sb.ToString();
        }

        public override int GetHashCode()
        {
            return _values.Cast<double>().Sum().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is Matrix mat)
            {
                return GetHashCode() == mat.GetHashCode();
            }

            return false;
        }

        #endregion Override

        #region Helpers

        public static Matrix IdentityMatrix(int size)
        {
            var ematrix = new Matrix(size);

            for (var i = 0; i < size; i++)
            {
                ematrix[i, i] = 1;
            }

            return ematrix;
        }

        public Matrix Copy()
        {
            return new Matrix(_values);
        }

        public Matrix GaußJordan()
        {
            if (!IsCubic)
            {
                throw new NotSupportedException("This matrix is not cubic. Change its size to height=width");
            }

            var M = Copy();

            var size = Width;
            var ematrix = new Matrix(Height, Width);
            for (var i = 0; i < size; i++)
            {
                ematrix[i, i] = 1;
            }

            for (var i = 0; i < size; i++)
            {
                var column = GetColumn(0);
                if (column.ToList().All(x => x == 0))
                {
                    throw new NotSupportedException($"Every cell in column {i} is null. At least one needs to be not null");
                }

                var row = GetRow(0);
                if (row.ToList().All(x => x == 0))
                {
                    throw new NotSupportedException($"Every cell in row {i} is null. At least one needs to be not null.");
                }
            }


            for (var i = 0; i < size; i++)
            {
                var divider = M[i, i];
                M.SetRow(i, M.GetRow(i).Divide(divider));
                ematrix.SetRow(i, ematrix.GetRow(i).Divide(divider));

                if (i + 1 >= size)
                {
                    continue;
                }

                for (var y = i + 1; y < size; y++)
                {
                    var factor = M[y, i];
                    M.SetRow(y, M.GetRow(y).Subtract(M.GetRow(i).Multiply(factor)));
                    ematrix.SetRow(y, ematrix.GetRow(y).Subtract(ematrix.GetRow(i).Multiply(factor)));
                }
            }

            for (var i = size - 1; i >= 0; i--)
            {
                for (var y = i - 1; y >= 0; y--)
                {
                    var factor = M[y, i];
                    M.SetRow(y, M.GetRow(y).Subtract(M.GetRow(i).Multiply(factor)));
                    ematrix.SetRow(y, ematrix.GetRow(y).Subtract(ematrix.GetRow(i).Multiply(factor)));
                }
            }

            return ematrix;
        }

        #endregion Helpers

        #endregion Methods
    }
}
