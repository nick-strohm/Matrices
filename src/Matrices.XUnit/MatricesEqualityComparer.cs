using System;
using System.Collections.Generic;
using Matrices.Core;

namespace Matrices.XUnit
{
    public class MatricesEqualityComparer : IEqualityComparer<Matrix>
    {
        public bool Equals(Matrix x, Matrix y)
        {
            if (x.Width != y.Width || x.Height != y.Height)
            {
                return false;
            }

            for (var row = 0; row < x.Height; row++)
            {
                for (var col = 0; col < x.Width; col++)
                {
                    if (Math.Abs(x[row, col] - y[row, col]) < 0.01)
                    {
                        continue;
                    }

                    return false;
                }
            }

            return true;
        }

        public int GetHashCode(Matrix obj)
        {
            return obj.GetHashCode();
        }
    }
}
