using System;

namespace Mesher.Mathematics
{
    /// <summary>
    /// Represents a 3x3 matrix.
    /// </summary>
    public struct Mat3
    {
        public Vec3 Col0;
        public Vec3 Col1;
        public Vec3 Col2;

        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="Mat3"/> struct.
        /// This matrix is the identity matrix scaled by <paramref name="scale"/>.
        /// </summary>
        /// <param name="scale">The scale.</param>
        public Mat3(Single scale)
        {
            Col0 = new Vec3(scale, 0.0f, 0.0f);
            Col1 = new Vec3(0.0f, scale, 0.0f);
            Col2 = new Vec3(0.0f, 0.0f, scale);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Mat3"/> struct.
        /// The matrix is initialised with the <paramref name="cols"/>.
        /// </summary>
        /// <param name="cols">The colums of the matrix.</param>
        public Mat3(Vec3[] cols)
        {
            Col0 = cols[0];
            Col1 = cols[1];
            Col2 = cols[2];
        }

        public Mat3(Vec3 a, Vec3 b, Vec3 c)
        {
            Col0 = a;
            Col1 = b;
            Col2 = c;
        }

        /// <summary>
        /// Creates an identity matrix.
        /// </summary>
        /// <returns>A new identity matrix.</returns>
        public static Mat3 Identity()
        {
            return new Mat3();
        }

        #endregion

        #region Index Access

        /// <summary>
        /// Gets or sets the <see cref="Vec3"/> column at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="Vec3"/> column.
        /// </value>
        /// <param name="column">The column index.</param>
        /// <returns>The column at index <paramref name="column"/>.</returns>
       /* public Vec3 this[Int32 column]
        {
            get { return m_cols[column]; }
            set { m_cols[column] = value; }
        }*/

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
      /*  public Single this[Int32 column, Int32 row]
        {
            get { return m_cols[column][row]; }
            set { m_cols[column][row] = value; }
        }*/

        #endregion

        #region Conversion

        /// <summary>
        /// Returns the <see cref="Mat3"/> portion of this matrix.
        /// </summary>
        /// <returns>The <see cref="Mat3"/> portion of this matrix.</returns>
        public Mat2 to_mat2()
        {
            return new Mat2(new[]
            {
                new Vec2(Col0.X, Col0.Y),
                new Vec2(Col1.X, Col1.Y)
            });
        }

        #endregion

        #region Multiplication

        /// <summary>
        /// Multiplies the <paramref name="lhs"/> matrix by the <paramref name="rhs"/> vector.
        /// </summary>
        /// <param name="lhs">The LHS matrix.</param>
        /// <param name="rhs">The RHS vector.</param>
        /// <returns>The product of <paramref name="lhs"/> and <paramref name="rhs"/>.</returns>
        public static Vec3 operator *(Mat3 lhs, Vec3 rhs)
        {
            return new Vec3(
                lhs.Col0.X * rhs.X + lhs.Col1[0] * rhs[1] + lhs.Col2[0] * rhs[2],
                lhs.Col0.Y * rhs.X + lhs.Col1[1] * rhs[1] + lhs.Col2[1] * rhs[2],
                lhs.Col0.Z * rhs.X + lhs.Col1[2] * rhs[1] + lhs.Col2[2] * rhs[2]
            );
        }

        /// <summary>
        /// Multiplies the <paramref name="lhs"/> matrix by the <paramref name="rhs"/> matrix.
        /// </summary>
        /// <param name="lhs">The LHS matrix.</param>
        /// <param name="rhs">The RHS matrix.</param>
        /// <returns>The product of <paramref name="lhs"/> and <paramref name="rhs"/>.</returns>
        public static Mat3 operator *(Mat3 lhs, Mat3 rhs)
        {
            return new Mat3(new[]
            {
                lhs.Col0[0] * rhs.Col0 + lhs.Col1[0] * rhs.Col1 + lhs.Col2[0] * rhs.Col2,
                lhs.Col0[1] * rhs.Col0 + lhs.Col1[1] * rhs.Col1 + lhs.Col2[1] * rhs.Col2,
                lhs.Col0[2] * rhs.Col0 + lhs.Col1[2] * rhs.Col1 + lhs.Col2[2] * rhs.Col2
            });
        }

        public static Mat3 operator *(Mat3 lhs, Single s)
        {
            return new Mat3(new[]
            {
                lhs.Col0*s,
                lhs.Col1*s,
                lhs.Col2*s
            });
        }

        #endregion

        public static Mat3 Inverse(Mat3 m)
        { var inverse = new Mat3(0);  
            var oneOverDeterminant = 1f / (
                                         + m.Col0[0] * (m.Col1[1] * m.Col2[2] - m.Col2[1] * m.Col1[2])
                                         - m.Col1[0] * (m.Col0[1] * m.Col2[2] - m.Col2[1] * m.Col0[2])
                                         + m.Col2[0] * (m.Col0[1] * m.Col1[2] - m.Col1[1] * m.Col0[2]));
                          inverse.Col0[0] = +(m.Col1[1] * m.Col2[2] - m.Col2[1] * m.Col1[2]) * oneOverDeterminant;
                          inverse.Col1[0] = -(m.Col1[0] * m.Col2[2] - m.Col2[0] * m.Col1[2]) * oneOverDeterminant;
                          inverse.Col2[0] = +(m.Col1[0] * m.Col2[1] - m.Col2[0] * m.Col1[1]) * oneOverDeterminant;
                          inverse.Col0[1] = -(m.Col0[1] * m.Col2[2] - m.Col2[1] * m.Col0[2]) * oneOverDeterminant;
                          inverse.Col1[1] = +(m.Col0[0] * m.Col2[2] - m.Col2[0] * m.Col0[2]) * oneOverDeterminant;
                          inverse.Col2[1] = -(m.Col0[0] * m.Col2[1] - m.Col2[0] * m.Col0[1]) * oneOverDeterminant;
                          inverse.Col0[2] = +(m.Col0[1] * m.Col1[2] - m.Col1[1] * m.Col0[2]) * oneOverDeterminant;
                          inverse.Col1[2] = -(m.Col0[0] * m.Col1[2] - m.Col1[0] * m.Col0[2]) * oneOverDeterminant;
                          inverse.Col2[2] = +(m.Col0[0] * m.Col1[1] - m.Col1[0] * m.Col0[1]) * oneOverDeterminant;

            return inverse;

        }

        public Single[] ToArray()
        {
            var ret = new []
            {
                Col0.X, Col0.Y, Col0.Z,
                Col1.X, Col1.Y, Col1.Z,
                Col2.X, Col2.Y, Col2.Z
            };
            return ret;
        }

        /// <summary>
        /// The columms of the matrix.
        /// </summary>
       // private Vec3[] m_cols;
    }
}