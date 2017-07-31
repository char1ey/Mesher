using System.Linq;

namespace Mesher.Mathematics 
{
    /// <summary>
    /// Represents a 3x3 matrix.
    /// </summary>
    public class Mat3
    {
        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="Mat3"/> struct.
        /// This matrix is the identity matrix scaled by <paramref name="scale"/>.
        /// </summary>
        /// <param name="scale">The scale.</param>
        public Mat3(double scale)
        {
            m_cols = new[]
            {
                new Vec3(scale, 0.0f, 0.0f),
                new Vec3(0.0f, scale, 0.0f),
                new Vec3(0.0f, 0.0f, scale)
            };
        }

        public Mat3()
        {
            m_cols = new[]
            {
                new Vec3(1, 0, 0),
                new Vec3(0, 1, 0),
                new Vec3(0, 0, 1)
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Mat3"/> struct.
        /// The matrix is initialised with the <paramref name="cols"/>.
        /// </summary>
        /// <param name="cols">The colums of the matrix.</param>
        public Mat3(Vec3[] cols)
        {
            this.m_cols = new[]
            {
                cols[0],
                cols[1],
                cols[2]
            };
        }

        public Mat3(Vec3 a, Vec3 b, Vec3 c)
        {
            this.m_cols = new[]
            {
                a, b, c
            };
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
        public Vec3 this[int column]
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
        public double this[int column, int row]
        {
            get { return m_cols[column][row]; }
            set { m_cols[column][row] = value; }
        }

        #endregion

        #region Conversion

        /// <summary>
        /// Returns the matrix as a flat array of elements, column major.
        /// </summary>
        /// <returns></returns>
        public double[] to_array()
        {
            return m_cols.SelectMany(v => v.ToArray()).ToArray();
        }

        /// <summary>
        /// Returns the <see cref="Mat3"/> portion of this matrix.
        /// </summary>
        /// <returns>The <see cref="Mat3"/> portion of this matrix.</returns>
        public Mat2 to_mat2()
        {
            return new Mat2(new[] {
			new Vec2(m_cols[0][0], m_cols[0][1]),
			new Vec2(m_cols[1][0], m_cols[1][1])});
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
                lhs[0, 0] * rhs[0] + lhs[1, 0] * rhs[1] + lhs[2, 0] * rhs[2],
                lhs[0, 1] * rhs[0] + lhs[1, 1] * rhs[1] + lhs[2, 1] * rhs[2],
                lhs[0, 2] * rhs[0] + lhs[1, 2] * rhs[1] + lhs[2, 2] * rhs[2]
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
			    lhs[0][0] * rhs[0] + lhs[1][0] * rhs[1] + lhs[2][0] * rhs[2],
			    lhs[0][1] * rhs[0] + lhs[1][1] * rhs[1] + lhs[2][1] * rhs[2],
			    lhs[0][2] * rhs[0] + lhs[1][2] * rhs[1] + lhs[2][2] * rhs[2]
            });
        }

        public static Mat3 operator * (Mat3 lhs, double s)
        {
            return new Mat3(new[]
            {
                lhs[0]*s,
                lhs[1]*s,
                lhs[2]*s
            });
        }

        #endregion

        public static Mat3 inverse(Mat3 m)
        {
            var oneOverDeterminant = (1f) / (
                                         +m[0][0] * (m[1][1] * m[2][2] - m[2][1] * m[1][2])
                                         - m[1][0] * (m[0][1] * m[2][2] - m[2][1] * m[0][2])
                                         + m[2][0] * (m[0][1] * m[1][2] - m[1][1] * m[0][2]));

            var Inverse = new Mat3(0);
            Inverse[0, 0] = +(m[1][1] * m[2][2] - m[2][1] * m[1][2]) * oneOverDeterminant;
            Inverse[1, 0] = -(m[1][0] * m[2][2] - m[2][0] * m[1][2]) * oneOverDeterminant;
            Inverse[2, 0] = +(m[1][0] * m[2][1] - m[2][0] * m[1][1]) * oneOverDeterminant;
            Inverse[0, 1] = -(m[0][1] * m[2][2] - m[2][1] * m[0][2]) * oneOverDeterminant;
            Inverse[1, 1] = +(m[0][0] * m[2][2] - m[2][0] * m[0][2]) * oneOverDeterminant;
            Inverse[2, 1] = -(m[0][0] * m[2][1] - m[2][0] * m[0][1]) * oneOverDeterminant;
            Inverse[0, 2] = +(m[0][1] * m[1][2] - m[1][1] * m[0][2]) * oneOverDeterminant;
            Inverse[1, 2] = -(m[0][0] * m[1][2] - m[1][0] * m[0][2]) * oneOverDeterminant;
            Inverse[2, 2] = +(m[0][0] * m[1][1] - m[1][0] * m[0][1]) * oneOverDeterminant;

            return Inverse;

        }

        /// <summary>
        /// The columms of the matrix.
        /// </summary>
        private Vec3[] m_cols;
    }
}