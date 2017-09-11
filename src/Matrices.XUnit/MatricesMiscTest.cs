using System.Collections.Generic;
using Matrices.Core;
using Xunit;

namespace Matrices.XUnit
{
    public class MatricesMiscTest
    {

        private readonly IEqualityComparer<Matrix> _matricesEqualityComparer = new MatricesEqualityComparer();

        [Fact]
        public void IsMatrixCubicTestFalse()
        {
            var matrix = new Matrix(2, 4);
            Assert.False(matrix.IsCubic, "Matrix(2, 4).IsCubic");
        }

        [Fact]
        public void IsMatrixCubicTestTrue()
        {
            var matrix = new Matrix(2, 2);
            Assert.True(matrix.IsCubic, "Matrix(2, 2).IsCubic");
        }

        [Fact]
        public void IsIdentityMatrixCubicTest()
        {
            var matrix = Matrix.IdentityMatrix(3);
            Assert.True(matrix.IsCubic, "IdentityMatrix(4).IsCubic");
        }

        [Fact]
        public void IsIdentityMatrixAnIdentityMatrixTest()
        {
            var matrix = Matrix.IdentityMatrix(3);
            var testMatrix = new Matrix(new [,]
            {
                { 1d, 0d, 0d },
                { 0d, 1d, 0d },
                { 0d, 0d, 1d }
            });

            Assert.Equal(matrix, testMatrix, _matricesEqualityComparer);
        }

        [Fact]
        public void GaußJordanAlgorithmTest()
        {
            var inputMatrix = new Matrix(new [,]
            {
                { 4d, 3d, 6d },
                { 7d, 1d, 8d },
                { 4d, 2d, 1d }
            });
            var resultMatrix = new Matrix(new [,]
            {
                { -1/5d,  3/25d,   6/25d },
                {  1/3d, -4/15d,   2/15d },
                { 2/15d,  4/75d, -17/75d }
            });
            var gaußMatrix = inputMatrix.GaußJordan();

            Assert.Equal(resultMatrix, gaußMatrix);
        }
    }
}
