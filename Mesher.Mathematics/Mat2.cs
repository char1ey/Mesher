using System;
using System.Linq;

namespace Mesher.Mathematics
{
    /// <summary>
    /// Represents a 2x2 matrix.
    /// </summary>
    public struct Mat2
    {
        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="Mat2"/> struct.
        /// This matrix is the identity matrix scaled by <paramref name="scale"/>.
        /// </summary>
        /// <param name="scale">The scale.</param>
        public Mat2(Single scale)
        {
            m_cols = new[]
            {
                new Vec2(scale, 0.0f),
                new Vec2(0.0f, scale)
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Mat2"/> struct.
        /// The matrix is initialised with the <paramref name="cols"/>.
        /// </summary>
        /// <param name="cols">The colums of the matrix.</param>
        public Mat2(Vec2[] cols)
        {
            m_cols = new[]
            {
                cols[0],
                cols[1]
            };
        }

        public Mat2(Vec2 a, Vec2 b)
        {
            m_cols = new[]
            {
                a, b
            };
        }

        public Mat2(Single a, Single b, Single c, Single d)
        {
            m_cols = new[]
            {
                new Vec2(a,b), new Vec2(c,d)
            };
        }

        /// <summary>
        /// Creates an identity matrix.
        /// </summary>
        /// <returns>A new identity matrix.</returns>
        public static Mat2 Identity()
        {
            return new Mat2();
        }

        #endregion

        #region Index Access

        /// <summary>
        /// Gets or sets the <see cref="Vec2"/> column at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="Vec2"/> column.
        /// </value>
        /// <param name="column">The column index.</param>
        /// <returns>The column at index <paramref name="column"/>.</returns>
        public Vec2 this[Int32 column]
        {
            get { return m_cols[column]; }
            set { m_cols[column] = value; }
        }

        /// <summary>
        /// Gets or sets the element at <paramref name="column"/> and <paramref name="row"/>.
        /// </summary>
        /// <value>
        /// The element at <paramref name="column"/> and <paramref name="row"/>.
        /// </value>
        /// <param name="column">The column index.</param>
        /// <param name="row">The row index.</param>
        /// <returns>
        /// The element at <paramref name="column"/> and <paramref name="row"/>.
        /// </returns>
        public Single this[Int32 column, Int32 row]
        {
            get { return m_cols[column][row]; }
            set { m_cols[column][row] = value; }
        }

        #endregion

        #region Conversion

        #endregion

        #region Multiplication

        /// <summary>
        /// Multiplies the <paramref name="lhs"/> matrix by the <paramref name="rhs"/> vector.
        /// </summary>
        /// <param name="lhs">The LHS matrix.</param>
        /// <param name="rhs">The RHS vector.</param>
        /// <returns>The product of <paramref name="lhs"/> and <paramref name="rhs"/>.</returns>
        public static Vec2 operator *(Mat2 lhs, Vec2 rhs)
        {
            return new Vec2(
                lhs[0,0] * rhs[0] + lhs[1,0] * rhs[1], 
                lhs[0,1] * rhs[0] + lhs[1,1] * rhs[1]
            );
        }

        /// <summary>
        /// Multiplies the <paramref name="lhs"/> matrix by the <paramref name="rhs"/> matrix.
        /// </summary>
        /// <param name="lhs">The LHS matrix.</param>
        /// <param name="rhs">The RHS matrix.</param>
        /// <returns>The product of <paramref name="lhs"/> and <paramref name="rhs"/>.</returns>
        public static Mat2 operator *(Mat2 lhs, Mat2 rhs)
        {
            return new Mat2(new[]
            {
			    lhs[0][0] * rhs[0] + lhs[1][0] * rhs[1],
			    lhs[0][1] * rhs[0] + lhs[1][1] * rhs[1]
            });
        }

        public static Mat2 operator * (Mat2 lhs, Single s)
        {
            return new Mat2(new[]
            {
                lhs[0]*s,
                lhs[1]*s
            });
        }

        #endregion

        public static Mat2 inverse(Mat2 m)
        {

            var oneOverDeterminant = 1f / (
                                         +m[0][0] * m[1][1]
                                         - m[1][0] * m[0][1]);

            var Inverse = new Mat2(
                +m[1][1] * oneOverDeterminant,
                -m[0][1] * oneOverDeterminant,
                -m[1][0] * oneOverDeterminant,
                +m[0][0] * oneOverDeterminant);

            return Inverse;
        }

        /// <summary>
        /// The columms of the matrix.
        /// </summary>
        private Vec2[] m_cols;
    }
}