using System;
using System.Reflection.Emit;

namespace Mesher.Mathematics 
{
    /// <summary>
    /// Represents a 4x4 matrix.
    /// </summary>
	public struct Matrix
    {
        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix"/> struct.
        /// This matrix is the identity matrix scaled by <paramref name="scale"/>.
        /// </summary>
        /// <param name="scale">The scale.</param>
		public Matrix(double scale)
        {
            m_matrix = new[]
            {
                new[] { scale, 0.0f, 0.0f, 0.0f },
                new[] { 0.0f, scale, 0.0f, 0.0f },
                new[] { 0.0f, 0.0f, scale, 0.0f },
                new[] { 0.0f, 0.0f, 0.0f, scale }
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix"/> struct.
        /// The matrix is initialised with the <paramref name="cols"/>.
        /// </summary>
        /// <param name="cols">The colums of the matrix.</param>
        public Matrix(double[][] cols)
        {
            m_matrix = new []
            {
                cols[0],
                cols[1],
                cols[2],
                cols[3]
            };
        }

        public Matrix(double[] a, double[] b, double[] c, double[] d)
        {
            m_matrix = new[]
            {
                a, b, c, d
            };
        }

        public Matrix(double[] m)
        {
            m_matrix = new double[N][];
            for (var i = 0; i < N; i++)
            {
                m_matrix[i] = new double[N];
                for (var j = 0; j < N; j++)
                    m_matrix[i][j] = m[i * N + j];
            }
        }

        public Matrix(float[] m)
        {
            m_matrix = new double[N][];
            for (var i = 0; i < N; i++)
            {
                m_matrix[i] = new double[N];
                for (var j = 0; j < N; j++)
                    m_matrix[i][j] = m[i * N + j];
            }
        }

        public Matrix(Matrix m)
        {
            m_matrix = new double[N][];
            for (var i = 0; i < N; i++)
            {
                m_matrix[i] = new double[N];
                for (var j = 0; j < N; j++)
                    m_matrix[i][j] = m[i, j];
            }
        }

        /// <summary>
        /// Creates an identity matrix.
        /// </summary>
        /// <returns>A new identity matrix.</returns>
        public static Matrix Identity()
        {
            return new Matrix
            {
                m_matrix = new[] 
                {
                    new double[]{1,0,0,0 },
                    new double[]{0,1,0,0 },
                    new double[]{0,0,1,0 },
                    new double[]{0,0,0,1 }
                }
            };
        }

        #endregion

        #region Index Access

        /// <summary>
        /// Gets or sets the <see cref="double"/> column at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="double"/> column.
        /// </value>
        /// <param name="column">The column index.</param>
        /// <returns>The column at index <paramref name="column"/>.</returns>
        public double[] this[int column]
		{
            get { return m_matrix[column]; }
            set { m_matrix[column] = value; }
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
            get { return m_matrix[column][row]; }
            set { m_matrix[column][row] = value; }
        }

        #endregion

        #region Conversion
        
        /// <summary>
        /// Returns the matrix as a flat array of elements, column major.
        /// </summary>
        /// <returns></returns>
        public double[] ToArrayDouble()
        {
            var ret = new double[N * N];
            for (var i = 0; i < N; i++)
                for (var j = 0; j < N; j++)
                    ret[i * N + j] = m_matrix[i][j];
            return ret;
        }

        public float[] ToArrayFloat()
        {
            var ret = new float[N * N];
            for (var i = 0; i < N; i++)
            for (var j = 0; j < N; j++)
                ret[i * N + j] = (float)m_matrix[i][j];
            return ret;
        }

        #endregion

        #region Multiplication

        /// <summary>
        /// Multiplies the <paramref name="lhs"/> matrix by the <paramref name="rhs"/> vector.
        /// </summary>
        /// <param name="lhs">The LHS matrix.</param>
        /// <param name="rhs">The RHS vector.</param>
        /// <returns>The product of <paramref name="lhs"/> and <paramref name="rhs"/>.</returns>
        public static double[] operator *(Matrix lhs, double[] rhs)
        {
            return new[] {
                lhs[0, 0] * rhs[0] + lhs[1, 0] * rhs[1] + lhs[2, 0] * rhs[2] + lhs[3, 0] * rhs[3],
                lhs[0, 1] * rhs[0] + lhs[1, 1] * rhs[1] + lhs[2, 1] * rhs[2] + lhs[3, 1] * rhs[3],
                lhs[0, 2] * rhs[0] + lhs[1, 2] * rhs[1] + lhs[2, 2] * rhs[2] + lhs[3, 2] * rhs[3],
                lhs[0, 3] * rhs[0] + lhs[1, 3] * rhs[1] + lhs[2, 3] * rhs[2] + lhs[3, 3] * rhs[3]
            };
        }

        public static Vertex operator *(Matrix lhs, Vertex rhs)
        {
            return new Vertex(
                lhs[0, 0] * rhs[0] + lhs[1, 0] * rhs[1] + lhs[2, 0] * rhs[2] + lhs[3, 0],
                lhs[0, 1] * rhs[0] + lhs[1, 1] * rhs[1] + lhs[2, 1] * rhs[2] + lhs[3, 1],
                lhs[0, 2] * rhs[0] + lhs[1, 2] * rhs[1] + lhs[2, 2] * rhs[2] + lhs[3, 2]
            );
        }

        /// <summary>
        /// Multiplies the <paramref name="lhs"/> matrix by the <paramref name="rhs"/> matrix.
        /// </summary>
        /// <param name="lhs">The LHS matrix.</param>
        /// <param name="rhs">The RHS matrix.</param>
        /// <returns>The product of <paramref name="lhs"/> and <paramref name="rhs"/>.</returns>
        public static Matrix operator * (Matrix lhs, Matrix rhs)
        {
            return new Matrix(new []
            {
                Add(Add(Mult(lhs[0][0], rhs[0]), Mult(lhs[1][0], rhs[1])), Add(Mult(lhs[2][0], rhs[2]), Mult(lhs[3][0], rhs[3]))),
                Add(Add(Mult(lhs[0][1], rhs[0]), Mult(lhs[1][1], rhs[1])), Add(Mult(lhs[2][1], rhs[2]), Mult(lhs[3][1], rhs[3]))),
                Add(Add(Mult(lhs[0][2], rhs[0]), Mult(lhs[1][2], rhs[1])), Add(Mult(lhs[2][2], rhs[2]), Mult(lhs[3][2], rhs[3]))),
                Add(Add(Mult(lhs[0][3], rhs[0]), Mult(lhs[1][3], rhs[1])), Add(Mult(lhs[2][3], rhs[2]), Mult(lhs[3][3], rhs[3])))
            });
        }

        public static Matrix operator *(Matrix lhs, double s)
        {
            return new Matrix(new[]
            {
                Mult(lhs[0], s),
                Mult(lhs[1], s),
                Mult(lhs[2], s),
                Mult(lhs[3], s)
            });
        }

        #endregion

        #region function
        /// <summary>
        /// Creates a frustrum projection matrix.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <param name="bottom">The bottom.</param>
        /// <param name="top">The top.</param>
        /// <param name="nearVal">The near val.</param>
        /// <param name="farVal">The far val.</param>
        /// <returns></returns>
        public static Matrix Frustum(double left, double right, double bottom, double top, double nearVal, double farVal)
        {
            var result = Identity();
            result[0, 0] = 2.0f * nearVal / (right - left);
            result[1, 1] = 2.0f * nearVal / (top - bottom);
            result[2, 0] = (right + left) / (right - left);
            result[2, 1] = (top + bottom) / (top - bottom);
            result[2, 2] = -(farVal + nearVal) / (farVal - nearVal);
            result[2, 3] = -1.0f;
            result[3, 2] = -(2.0f * farVal * nearVal) / (farVal - nearVal);
            return result;
        }

        /// <summary>
        /// Creates a matrix for a symmetric perspective-view frustum with far plane at infinite.
        /// </summary>
        /// <param name="fovy">The fovy.</param>
        /// <param name="aspect">The aspect.</param>
        /// <param name="zNear">The z near.</param>
        /// <returns></returns>
        public static Matrix InfinitePerspective(double fovy, double aspect, double zNear)
        {

            var range = Math.Tan(fovy / 2) * zNear;

            var left = -range * aspect;
            var right = range * aspect;
            var bottom = -range;
            var top = range;

            var result = new Matrix(0)
            {
                [0, 0] = 2f * zNear / (right - left),
                [1, 1] = 2f * zNear / (top - bottom),
                [2, 2] = -1f,
                [2, 3] = -1f,
                [3, 2] = -2f * zNear
            };
            return result;
        }

        /// <summary>
        /// Build a look at view matrix.
        /// </summary>
        /// <param name="eye">The eye.</param>
        /// <param name="center">The center.</param>
        /// <param name="up">Up.</param>
        /// <returns></returns>
        public static Matrix LookAt(Vertex eye, Vertex center, Vertex up)
        {
            var f = Vertex.Normalize(center - eye);
            var s = Vertex.Normalize(f.Cross(up));
            var u = s.Cross(f);

            var result = new Matrix(1)
            {
                [0, 0] = s.X,
                [1, 0] = s.Y,
                [2, 0] = s.Z,
                [0, 1] = u.X,
                [1, 1] = u.Y,
                [2, 1] = u.Z,
                [0, 2] = -f.X,
                [1, 2] = -f.Y,
                [2, 2] = -f.Z,
                [3, 0] = -s.Dot(eye),
                [3, 1] = -u.Dot(eye),
                [3, 2] = f.Dot(eye)
            };
            return result;
        }

        /// <summary>
        /// Creates a matrix for an orthographic parallel viewing volume.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <param name="bottom">The bottom.</param>
        /// <param name="top">The top.</param>
        /// <param name="zNear">The z near.</param>
        /// <param name="zFar">The z far.</param>
        /// <returns></returns>
        public static Matrix Ortho(double left, double right, double bottom, double top, double zNear, double zFar)
        {
            var result = Identity();
            result[0, 0] = 2f / (right - left);
            result[1, 1] = 2f / (top - bottom);
            result[2, 2] = -2f / (zFar - zNear);
            result[3, 0] = -(right + left) / (right - left);
            result[3, 1] = -(top + bottom) / (top - bottom);
            result[3, 2] = -(zFar + zNear) / (zFar - zNear);
            return result;
        }

        /// <summary>
        /// Creates a matrix for projecting two-dimensional coordinates onto the screen.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <param name="bottom">The bottom.</param>
        /// <param name="top">The top.</param>
        /// <returns></returns>
        public static Matrix Ortho(double left, double right, double bottom, double top)
        {
            var result = Identity();
            result[0, 0] = 2f / (right - left);
            result[1, 1] = 2f / (top - bottom);
            result[2, 2] = -1f;
            result[3, 0] = -(right + left) / (right - left);
            result[3, 1] = -(top + bottom) / (top - bottom);
            return result;
        }

        /// <summary>
        /// Creates a perspective transformation matrix.
        /// </summary>
        /// <param name="fovy">The field of view angle, in radians.</param>
        /// <param name="aspect">The aspect ratio.</param>
        /// <param name="zNear">The near depth clipping plane.</param>
        /// <param name="zFar">The far depth clipping plane.</param>
        /// <returns>A <see cref="Matrix"/> that contains the projection matrix for the perspective transformation.</returns>
        public static Matrix Perspective(double fovy, double aspect, double zNear, double zFar)
        {
            var tanHalfFovy = Math.Tan(fovy / 2.0f);

            var result = Identity();
            result[0, 0] = 1.0f / (aspect * tanHalfFovy);
            result[1, 1] = 1.0f / tanHalfFovy;
            result[2, 2] = -(zFar + zNear) / (zFar - zNear);
            result[2, 3] = -1.0f;
            result[3, 2] = -(2.0f * zFar * zNear) / (zFar - zNear);
            return result;
        }

        /// <summary>
        /// Builds a perspective projection matrix based on a field of view.
        /// </summary>
        /// <param name="fov">The fov (in radians).</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="zNear">The z near.</param>
        /// <param name="zFar">The z far.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        public static Matrix PerspectiveFov(double fov, double width, double height, double zNear, double zFar)
        {
            if (width <= 0 || height <= 0 || fov <= 0)
                throw new ArgumentOutOfRangeException();

            var rad = fov;

            var h = Math.Cos(0.5 * rad) / Math.Sin(0.5 * rad);
            var w = h * height / width;

            var result = new Matrix(0)
            {
                [0, 0] = w,
                [1, 1] = h,
                [2, 2] = -(zFar + zNear) / (zFar - zNear),
                [2, 3] = -1f,
                [3, 2] = -(2f * zFar * zNear) / (zFar - zNear)
            };
            return result;
        }

        /// <summary>
        /// Define a picking region.
        /// </summary>
        /// <param name="center">The center.</param>
        /// <param name="delta">The delta.</param>
        /// <param name="viewport">The viewport.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        public static Matrix PickMatrix(Vertex center, Vertex delta, double[] viewport)
        {
            if (delta.X <= 0 || delta.Y <= 0)
                throw new ArgumentOutOfRangeException();
            var result = new Matrix(1.0f);

            if (!(delta.X > 0f && delta.Y > 0f))
                return result; // Error

            var temp = new Vertex(
                (viewport[2] - 2f * (center.X - viewport[0])) / delta.X,
                (viewport[3] - 2f * (center.Y - viewport[1])) / delta.Y,
                0f);

            // Translate and scale the picked region to the entire window
            result = Translate(result, temp);
            return Scale(result, new Vertex(viewport[2] / delta.X, viewport[3] / delta.Y, 1));
        }

        /// <summary>
        /// Map the specified object coordinates (obj.x, obj.y, obj.z) into window coordinates.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="model">The model.</param>
        /// <param name="proj">The proj.</param>
        /// <param name="viewport">The viewport.</param>
        /// <returns></returns>
        public static Vertex Project(Vertex obj, Matrix model, Matrix proj, double[] viewport)

        {
            var tmp = new[] { obj.X, obj.Y, obj.Z, 1 };
            tmp = model * tmp;
            tmp = proj * tmp;

            tmp = Devide(tmp, tmp[3]);
            tmp = Add(Mult(tmp, 0.5), 0.5f);
            tmp[0] = tmp[0] * viewport[2] + viewport[0];
            tmp[1] = tmp[1] * viewport[3] + viewport[1];

            return new Vertex(tmp[0], tmp[1], tmp[2]);
        }

        /// <summary>
        /// Builds a rotation 4 * 4 matrix created from an axis vector and an angle.
        /// </summary>
        /// <param name="m">The m.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="v">The v.</param>
        /// <returns></returns>
        public static Matrix Rotate(Matrix m, double angle, Vertex v)
        {
            var c = Math.Cos(angle);
            var s = Math.Sin(angle);

            var axis = v.Normalize();
            var temp = (1.0 - c) * axis;

            var rotate = Identity();
            rotate[0, 0] = c + temp[0] * axis[0];
            rotate[0, 1] = 0 + temp[0] * axis[1] + s * axis[2];
            rotate[0, 2] = 0 + temp[0] * axis[2] - s * axis[1];

            rotate[1, 0] = 0 + temp[1] * axis[0] - s * axis[2];
            rotate[1, 1] = c + temp[1] * axis[1];
            rotate[1, 2] = 0 + temp[1] * axis[2] + s * axis[0];

            rotate[2, 0] = 0 + temp[2] * axis[0] + s * axis[1];
            rotate[2, 1] = 0 + temp[2] * axis[1] - s * axis[0];
            rotate[2, 2] = c + temp[2] * axis[2];

            var result = Identity();
            result[0] = Add(Add(Mult(m[0], rotate[0][0]), Mult(m[1], rotate[0][1])), Mult(m[2], rotate[0][2]));
            result[1] = Add(Add(Mult(m[0], rotate[1][0]), Mult(m[1], rotate[1][1])), Mult(m[2], rotate[1][2]));
            result[2] = Add(Add(Mult(m[0], rotate[2][0]), Mult(m[1], rotate[2][1])), Mult(m[2], rotate[2][2]));
            result[3] = m[3];
            return result;
        }

        public static Matrix Rotate(double angle, Vertex v)
        {
            return Rotate(Identity(), angle, v);
        }

        /// <summary>
        /// Applies a scale transformation to matrix <paramref name="m"/> by vector <paramref name="v"/>.
        /// </summary>
        /// <param name="m">The matrix to transform.</param>
        /// <param name="v">The vector to scale by.</param>
        /// <returns><paramref name="m"/> scaled by <paramref name="v"/>.</returns>
        public static Matrix Scale(Matrix m, Vertex v)
        {
            var result = m;
            result[0] = Mult(m[0], v[0]);
            result[1] = Mult(m[1], v[1]);
            result[2] = Mult(m[2], v[2]);
            result[3] = m[3];
            return result;
        }

        /// <summary>
        /// Create a scale transformation matrix by vector <paramref name="v"/>.
        /// </summary>
        /// <param name="v">The vector to scale by.</param>
        public static Matrix Scale(Vertex v)
        {
            return Scale(Identity(), v);
        }

        /// <summary>
        /// Applies a translation transformation to matrix <paramref name="m"/> by vector <paramref name="v"/>.
        /// </summary>
        /// <param name="m">The matrix to transform.</param>
        /// <param name="v">The vector to translate by.</param>
        /// <returns><paramref name="m"/> translated by <paramref name="v"/>.</returns>
        public static Matrix Translate(Matrix m, Vertex v)
        {
            var result = new Matrix(m)
            {
                [3] = Add(Add(Mult(m[0], v[0]), Mult(m[1], v[1])), Add(Mult(m[2], v[2]), m[3]))
            };
            return result;
        }

        /// <summary>
        /// Creates a matrix for a symmetric perspective-view frustum with far plane 
        /// at infinite for graphics hardware that doesn't support depth clamping.
        /// </summary>
        /// <param name="fovy">The fovy.</param>
        /// <param name="aspect">The aspect.</param>
        /// <param name="zNear">The z near.</param>
        /// <returns></returns>
        public static Matrix TweakedInfinitePerspective(double fovy, double aspect, double zNear)
        {
            var range = Math.Tan(fovy / 2) * zNear;
            var left = -range * aspect;
            var right = range * aspect;
            var bottom = -range;
            var top = range;

            var result = new Matrix(0f)
            {
                [0, 0] = 2 * zNear / (right - left),
                [1, 1] = 2 * zNear / (top - bottom),
                [2, 2] = 0.0001f - 1f,
                [2, 3] = -1,
                [3, 2] = -(0.0001f - 2) * zNear
            };
            return result;
        }

        /// <summary>
        /// Map the specified window coordinates (win.x, win.y, win.z) into object coordinates.
        /// </summary>
        /// <param name="win">The win.</param>
        /// <param name="model">The model.</param>
        /// <param name="proj">The proj.</param>
        /// <param name="viewport">The viewport.</param>
        /// <returns></returns>
        public static Vertex UnProject(Vertex win, Matrix model, Matrix proj, double[] viewport)
        {
            var inverse = (proj * model).Inverse();

            var tmp = new[] { win.X, win.Y, win.Z, 1 };
            tmp[0] = (tmp[0] - viewport[0]) / viewport[2];
            tmp[1] = (tmp[1] - viewport[1]) / viewport[3];
            tmp = Substract(Mult(tmp, 2), 1);

            var obj = inverse * tmp;
            obj = Devide(obj, obj[3]);

            return new Vertex(obj[0], obj[1], obj[2]);
        }

        public Matrix Inverse()
        {
            return Inverse(this);
        }

        public static Matrix Inverse(Matrix m)
        {
            var coef00 = m[2][2] * m[3][3] - m[3][2] * m[2][3];
            var coef02 = m[1][2] * m[3][3] - m[3][2] * m[1][3];
            var coef03 = m[1][2] * m[2][3] - m[2][2] * m[1][3];

            var coef04 = m[2][1] * m[3][3] - m[3][1] * m[2][3];
            var coef06 = m[1][1] * m[3][3] - m[3][1] * m[1][3];
            var coef07 = m[1][1] * m[2][3] - m[2][1] * m[1][3];

            var coef08 = m[2][1] * m[3][2] - m[3][1] * m[2][2];
            var coef10 = m[1][1] * m[3][2] - m[3][1] * m[1][2];
            var coef11 = m[1][1] * m[2][2] - m[2][1] * m[1][2];

            var coef12 = m[2][0] * m[3][3] - m[3][0] * m[2][3];
            var coef14 = m[1][0] * m[3][3] - m[3][0] * m[1][3];
            var coef15 = m[1][0] * m[2][3] - m[2][0] * m[1][3];

            var coef16 = m[2][0] * m[3][2] - m[3][0] * m[2][2];
            var coef18 = m[1][0] * m[3][2] - m[3][0] * m[1][2];
            var coef19 = m[1][0] * m[2][2] - m[2][0] * m[1][2];

            var coef20 = m[2][0] * m[3][1] - m[3][0] * m[2][1];
            var coef22 = m[1][0] * m[3][1] - m[3][0] * m[1][1];
            var coef23 = m[1][0] * m[2][1] - m[2][0] * m[1][1];

            var fac0 = new[] { coef00, coef00, coef02, coef03 };
            var fac1 = new[] { coef04, coef04, coef06, coef07 };
            var fac2 = new[] { coef08, coef08, coef10, coef11 };
            var fac3 = new[] { coef12, coef12, coef14, coef15 };
            var fac4 = new[] { coef16, coef16, coef18, coef19 };
            var fac5 = new[] { coef20, coef20, coef22, coef23 };

            var vec0 = new[] { m[1][0], m[0][0], m[0][0], m[0][0] };
            var vec1 = new[] { m[1][1], m[0][1], m[0][1], m[0][1] };
            var vertex = new[] { m[1][2], m[0][2], m[0][2], m[0][2] };
            var vec3 = new[] { m[1][3], m[0][3], m[0][3], m[0][3] };

            var inv0 = Add(Substract(Mult(vec1, fac0), Mult(vertex, fac1)), Mult(vec3, fac2));
            var inv1 = Add(Substract(Mult(vec0, fac0), Mult(vertex, fac3)), Mult(vec3, fac4));
            var inv2 = Add(Substract(Mult(vec0, fac1), Mult(vec1, fac3)), Mult(vec3, fac5));
            var inv3 = Add(Substract(Mult(vec0, fac2), Mult(vec1, fac4)), Mult(vertex, fac5));

            var signA = new double[] { +1, -1, +1, -1 };
            var signB = new double[] { -1, +1, -1, +1 };
            var inverse = new Matrix(Mult(inv0, signA), Mult(inv1, signB), Mult(inv2, signA), Mult(inv3, signB));

            var row0 = new[] { inverse[0][0], inverse[1][0], inverse[2][0], inverse[3][0] };

            var dot0 = Mult(m[0], row0);
            var dot1 = dot0[0] + dot0[1] + dot0[2] + dot0[3];

            var oneOverDeterminant = 1f / dot1;

            return inverse * oneOverDeterminant;
        }

        public static bool Valid(Matrix m)
        {
            for (var i = 0; i < N; i++)
                for (var j = 0; j < N; j++)
                    if (double.IsNaN(m[i, j]) || double.IsInfinity(m[i, j]))
                        return false;
            return true;
        }

        public bool Valid()
        {
            return Valid(this);
        }

        #endregion

        #region entity
        private const int N = 4;

        public static double Dot(double[] x, double[] y)
        {
            var tmp = Mult(x, y);
            return tmp[0] + tmp[1] + tmp[2] + tmp[3];
        }

        private static double[] Mult(double[] a, double[] b)
        {
            var ret = new double[N];
            for (var i = 0; i < N; i++)
                ret[i] = a[i] * b[i];
            return ret;
        }

        private static double[] Add(double[] a, double[] b)
        {
            var ret = new double[N];
            for (var i = 0; i < N; i++)
                ret[i] = a[i] + b[i];
            return ret;
        }

        private static double[] Substract(double[] a, double[] b)
        {
            var ret = new double[N];
            for (var i = 0; i < N; i++)
                ret[i] = a[i] - b[i];
            return ret;
        }

        private static double[] Mult(double a, double[] b)
        {
            var ret = new double[N];
            for (var i = 0; i < N; i++)
                ret[i] = a * b[i];
            return ret;
        }

        private static double[] Mult(double[] a, double b)
        {
            var ret = new double[N];
            for (var i = 0; i < N; i++)
                ret[i] = a[i] * b;
            return ret;
        }

        private static double[] Add(double[] a, double b)
        {
            var ret = new double[N];
            for (var i = 0; i < N; i++)
                ret[i] = a[i] + b;
            return ret;
        }

        private static double[] Substract(double[] a, double b)
        {
            var ret = new double[N];
            for (var i = 0; i < N; i++)
                ret[i] = a[i] - b;
            return ret;
        }

        private static double[] Devide(double[] a, double b)
        {
            var ret = new double[N];
            for (var i = 0; i < N; i++)
                ret[i] = a[i] / b;
            return ret;
        }

        #endregion

        /// <summary>
        /// The columms of the matrix.
        /// </summary>
        private double[][] m_matrix;
	}
}