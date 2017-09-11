using System.Collections.Generic;
using Matrices.Core;
using Xunit;

namespace Matrices.XUnit
{
    public class MatricesCalculationTest
    {
        private double[,] FirstDoubles { get; }
        private double[,] SecondDoubles { get; }

        private Matrix FirstMatrix { get; }
        private Matrix SecondMatrix { get; }

        private readonly IEqualityComparer<Matrix> _matricesEqualityComparer = new MatricesEqualityComparer();

        public MatricesCalculationTest()
        {
            FirstDoubles = new[,]
            {
                { -32d, 20d, 12d },
                { 29d, 11d, 6d },
                { 12d, 8d, 5d }
            };
            SecondDoubles = new[,]
            {
                { 3d, 0d, 0d },
                { 1d, 3d, 2d },
                { 4d, 2d, 1d }
            };

            FirstMatrix = new Matrix(FirstDoubles);
            SecondMatrix = new Matrix(SecondDoubles);
        }

        [Fact]
        public void MatrixAddTest()
        {
            var resultDoubles = new[,]
            {
                { -29d, 20d, 12d },
                {  30d, 14d,  8d },
                {  16d, 10d,  6d }
            };

            var resultMatrix = new Matrix(resultDoubles);
            var addResultMatrix = FirstMatrix + SecondMatrix;

            Assert.Equal(resultMatrix, addResultMatrix, _matricesEqualityComparer);
        }

        [Fact]
        public void MatrixSubtractTest()
        {
            var resultDoubles = new[,]
            {
                { -35d, 20d, 12d },
                {  28d,  8d,  4d },
                {   8d,  6d,  4d }
            };

            var resultMatrix = new Matrix(resultDoubles);
            var addResultMatrix = FirstMatrix - SecondMatrix;

            Assert.Equal(resultMatrix, addResultMatrix, _matricesEqualityComparer);
        }

        [Fact]
        public void MatrixMultiplyTest()
        {
            var resultDoubles = new[,]
            {
                { -28d, 84d, 52d },
                { 122d, 45d, 28d },
                {  64d, 34d, 21d }
            };

            var resultMatrix = new Matrix(resultDoubles);
            var addResultMatrix = FirstMatrix * SecondMatrix;

            Assert.Equal(resultMatrix, addResultMatrix, _matricesEqualityComparer);
        }

        [Fact]
        public void MatrixDivideTest()
        {
            var resultDoubles = new[,]
            {
                { -52/3d, 4d, 4d },
                {     4d, 1d, 4d },
                {     2d, 2d, 1d }
            };

            var resultMatrix = new Matrix(resultDoubles);
            var addResultMatrix = FirstMatrix / SecondMatrix;

            Assert.Equal(resultMatrix, addResultMatrix, _matricesEqualityComparer);
        }

        [Fact]
        public void DoubleAddTest()
        {
            var resultDoubles = new[,]
            {
                { -27d, 20d, 12d },
                { 29d, 16d, 6d },
                { 12d, 8d, 10d }
            };

            var resultMatrix = new Matrix(resultDoubles);
            var addResultMatrix = FirstMatrix + 5d;

            Assert.Equal(resultMatrix, addResultMatrix, _matricesEqualityComparer);
        }

        [Fact]
        public void DoubleSubtractTest()
        {
            var resultDoubles = new[,]
            {
                { -37d, 20d, 12d },
                { 29d, 6d, 6d },
                { 12d, 8d, 0d }
            };

            var resultMatrix = new Matrix(resultDoubles);
            var addResultMatrix = FirstMatrix - 5d;

            Assert.Equal(resultMatrix, addResultMatrix, _matricesEqualityComparer);
        }

        [Fact]
        public void DoubleMultiplyTest()
        {
            var resultDoubles = new[,]
            {
                { -160d, 100d, 60d },
                { 145d, 55d, 30d },
                { 60d, 40d, 25d }
            };

            var resultMatrix = new Matrix(resultDoubles);
            var addResultMatrix = FirstMatrix * 5d;

            Assert.Equal(resultMatrix, addResultMatrix, _matricesEqualityComparer);
        }

        [Fact]
        public void DoubleDivideTest()
        {
            var resultDoubles = new[,]
            {
                { -32/5d, 20/5d, 12/5d },
                { 29/5d, 11/5d, 6/5d },
                { 12/5d, 8/5d, 5/5d }
            };

            var resultMatrix = new Matrix(resultDoubles);
            var addResultMatrix = FirstMatrix / 5d;

            Assert.Equal(resultMatrix, addResultMatrix, _matricesEqualityComparer);
        }
    }
}
