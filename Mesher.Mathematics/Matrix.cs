using System;
using System.Linq;

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
            matrix = new double[][]
            {
                new double[] { scale, 0.0f, 0.0f, 0.0f },
                new double[] { 0.0f, scale, 0.0f, 0.0f },
                new double[] { 0.0f, 0.0f, scale, 0.0f },
                new double[] { 0.0f, 0.0f, 0.0f, scale }
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix"/> struct.
        /// The matrix is initialised with the <paramref name="cols"/>.
        /// </summary>
        /// <param name="cols">The colums of the matrix.</param>
        public Matrix(double[][] cols)
        {
            this.matrix = new []
            {
                cols[0],
                cols[1],
                cols[2],
                cols[3]
            };
        }

        public Matrix(double[] a, double[] b, double[] c, double[] d)
        {
            this.matrix = new[]
            {
                a, b, c, d
            };
        }

        public Matrix(double[] m)
        {
            matrix = new double[N][];
            for (int i = 0; i < N; i++)
            {
                matrix[i] = new double[N];
                for (int j = 0; j < N; j++)
                    matrix[i][j] = m[i * N + j];
            }
        }

        public Matrix(Matrix m)
        {
            matrix = new double[N][];
            for (int i = 0; i < N; i++)
            {
                matrix[i] = new double[N];
                for (int j = 0; j < N; j++)
                    matrix[i][j] = m[i, j];
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
                matrix = new[] 
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
        /// Gets or sets the <see cref="double[]"/> column at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="double[]"/> column.
        /// </value>
        /// <param name="column">The column index.</param>
        /// <returns>The column at index <paramref name="column"/>.</returns>
        public double[] this[int column]
		{
            get { return matrix[column]; }
            set { matrix[column] = value; }
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
            get { return matrix[column][row]; }
            set { matrix[column][row] = value; }
        }

        #endregion

        #region Conversion
        
        /// <summary>
        /// Returns the matrix as a flat array of elements, column major.
        /// </summary>
        /// <returns></returns>
        public double[] ToArray()
        {
            double[] ret = new double[N * N];
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    ret[i * N + j] = matrix[i][j];
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
            return new double[] {
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
                add(add(mult(lhs[0][0], rhs[0]), mult(lhs[1][0], rhs[1])), add(mult(lhs[2][0], rhs[2]), mult(lhs[3][0], rhs[3]))),
                add(add(mult(lhs[0][1], rhs[0]), mult(lhs[1][1], rhs[1])), add(mult(lhs[2][1], rhs[2]), mult(lhs[3][1], rhs[3]))),
                add(add(mult(lhs[0][2], rhs[0]), mult(lhs[1][2], rhs[1])), add(mult(lhs[2][2], rhs[2]), mult(lhs[3][2], rhs[3]))),
                add(add(mult(lhs[0][3], rhs[0]), mult(lhs[1][3], rhs[1])), add(mult(lhs[2][3], rhs[2]), mult(lhs[3][3], rhs[3])))
            });
        }

        public static Matrix operator *(Matrix lhs, double s)
        {
            return new Matrix(new[]
            {
                mult(lhs[0], s),
                mult(lhs[1], s),
                mult(lhs[2], s),
                mult(lhs[3], s)
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
            result[0, 0] = (2.0f * nearVal) / (right - left);
            result[1, 1] = (2.0f * nearVal) / (top - bottom);
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

            double range = Math.Tan(fovy / 2) * zNear;

            double left = -range * aspect;
            double right = range * aspect;
            double bottom = -range;
            double top = range;

            var result = new Matrix(0);
            result[0, 0] = ((2f) * zNear) / (right - left);
            result[1, 1] = ((2f) * zNear) / (top - bottom);
            result[2, 2] = -(1f);
            result[2, 3] = -(1f);
            result[3, 2] = -(2f) * zNear;
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
            Vertex f = Vertex.Normalize(center - eye);
            Vertex s = Vertex.Normalize(f.Cross(up));
            Vertex u = s.Cross(f);

            Matrix Result = new Matrix(1);
            Result[0, 0] = s.x;
            Result[1, 0] = s.y;
            Result[2, 0] = s.z;
            Result[0, 1] = u.x;
            Result[1, 1] = u.y;
            Result[2, 1] = u.z;
            Result[0, 2] = -f.x;
            Result[1, 2] = -f.y;
            Result[2, 2] = -f.z;
            Result[3, 0] = -s.Dot(eye);
            Result[3, 1] = -u.Dot(eye);
            Result[3, 2] = f.Dot(eye);
            return Result;
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
            var result = Matrix.Identity();
            result[0, 0] = (2f) / (right - left);
            result[1, 1] = (2f) / (top - bottom);
            result[2, 2] = -(2f) / (zFar - zNear);
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
            var result = Matrix.Identity();
            result[0, 0] = (2f) / (right - left);
            result[1, 1] = (2f) / (top - bottom);
            result[2, 2] = -(1f);
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
            var tanHalfFovy = (double)Math.Tan(fovy / 2.0f);

            var result = Matrix.Identity();
            result[0, 0] = 1.0f / (aspect * tanHalfFovy);
            result[1, 1] = 1.0f / (tanHalfFovy);
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

            var result = new Matrix(0);
            result[0, 0] = w;
            result[1, 1] = h;
            result[2, 2] = -(zFar + zNear) / (zFar - zNear);
            result[2, 3] = -(1f);
            result[3, 2] = -((2f) * zFar * zNear) / (zFar - zNear);
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
            if (delta.x <= 0 || delta.y <= 0)
                throw new ArgumentOutOfRangeException();
            var Result = new Matrix(1.0f);

            if (!(delta.x > (0f) && delta.y > (0f)))
                return Result; // Error

            Vertex Temp = new Vertex(
                ((viewport[2]) - (2f) * (center.x - (viewport[0]))) / delta.x,
                ((viewport[3]) - (2f) * (center.y - (viewport[1]))) / delta.y,
                (0f));

            // Translate and scale the picked region to the entire window
            Result = Translate(Result, Temp);
            return Scale(Result, new Vertex((viewport[2]) / delta.x, (viewport[3]) / delta.y, (1)));
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
            double[] tmp = new double[] { obj.x, obj.y, obj.z, 1 };
            tmp = model * tmp;
            tmp = proj * tmp;

            tmp = devide(tmp, tmp[3]);
            tmp = add(mult(tmp, 0.5), 0.5f);
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
            double c = Math.Cos(angle);
            double s = Math.Sin(angle);

            Vertex axis = v.Normalize();
            Vertex temp = (1.0 - c) * axis;

            Matrix rotate = Matrix.Identity();
            rotate[0, 0] = c + temp[0] * axis[0];
            rotate[0, 1] = 0 + temp[0] * axis[1] + s * axis[2];
            rotate[0, 2] = 0 + temp[0] * axis[2] - s * axis[1];

            rotate[1, 0] = 0 + temp[1] * axis[0] - s * axis[2];
            rotate[1, 1] = c + temp[1] * axis[1];
            rotate[1, 2] = 0 + temp[1] * axis[2] + s * axis[0];

            rotate[2, 0] = 0 + temp[2] * axis[0] + s * axis[1];
            rotate[2, 1] = 0 + temp[2] * axis[1] - s * axis[0];
            rotate[2, 2] = c + temp[2] * axis[2];

            Matrix result = Identity();
            result[0] = add(add(mult(m[0], rotate[0][0]), mult(m[1], rotate[0][1])), mult(m[2], rotate[0][2]));
            result[1] = add(add(mult(m[0], rotate[1][0]), mult(m[1], rotate[1][1])), mult(m[2], rotate[1][2]));
            result[2] = add(add(mult(m[0], rotate[2][0]), mult(m[1], rotate[2][1])), mult(m[2], rotate[2][2]));
            result[3] = m[3];
            return result;
        }


        //  TODO: this is actually defined as an extension, put in the right file.
        public static Matrix Rotate(double angle, Vertex v)
        {
            return Rotate(Matrix.Identity(), angle, v);
        }


        /// <summary>
        /// Applies a scale transformation to matrix <paramref name="m"/> by vector <paramref name="v"/>.
        /// </summary>
        /// <param name="m">The matrix to transform.</param>
        /// <param name="v">The vector to scale by.</param>
        /// <returns><paramref name="m"/> scaled by <paramref name="v"/>.</returns>
        public static Matrix Scale(Matrix m, Vertex v)
        {
            Matrix result = m;
            result[0] = mult(m[0], v[0]);
            result[1] = mult(m[1], v[1]);
            result[2] = mult(m[2], v[2]);
            result[3] = m[3];
            return result;
        }

        /// <summary>
        /// Applies a translation transformation to matrix <paramref name="m"/> by vector <paramref name="v"/>.
        /// </summary>
        /// <param name="m">The matrix to transform.</param>
        /// <param name="v">The vector to translate by.</param>
        /// <returns><paramref name="m"/> translated by <paramref name="v"/>.</returns>
        public static Matrix Translate(Matrix m, Vertex v)
        {
            Matrix result = new Matrix(m);
            result[3] = add(add(mult(m[0], v[0]), mult(m[1], v[1])), add(mult(m[2], v[2]), m[3]));
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
            double range = Math.Tan(fovy / (2)) * zNear;
            double left = -range * aspect;
            double right = range * aspect;
            double bottom = -range;
            double top = range;

            Matrix Result = new Matrix((0f));
            Result[0, 0] = ((2) * zNear) / (right - left);
            Result[1, 1] = ((2) * zNear) / (top - bottom);
            Result[2, 2] = (0.0001f) - (1f);
            Result[2, 3] = (-1);
            Result[3, 2] = -((0.0001f) - (2)) * zNear;
            return Result;
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
            Matrix inverse = (proj * model).Inverse();

            double[] tmp = new double[] { win.x, win.y, win.z, 1 };
            tmp[0] = (tmp[0] - (viewport[0])) / (viewport[2]);
            tmp[1] = (tmp[1] - (viewport[1])) / (viewport[3]);
            tmp = substract(mult(tmp, 2), 1);

            double[] obj = inverse * tmp;
            obj = devide(obj, obj[3]);

            return new Vertex(obj[0], obj[1], obj[2]);
        }

        public Matrix Inverse()
        {
            return Inverse(this);
        }

        public static Matrix Inverse(Matrix m)
        {
            double Coef00 = m[2][2] * m[3][3] - m[3][2] * m[2][3];
            double Coef02 = m[1][2] * m[3][3] - m[3][2] * m[1][3];
            double Coef03 = m[1][2] * m[2][3] - m[2][2] * m[1][3];

            double Coef04 = m[2][1] * m[3][3] - m[3][1] * m[2][3];
            double Coef06 = m[1][1] * m[3][3] - m[3][1] * m[1][3];
            double Coef07 = m[1][1] * m[2][3] - m[2][1] * m[1][3];

            double Coef08 = m[2][1] * m[3][2] - m[3][1] * m[2][2];
            double Coef10 = m[1][1] * m[3][2] - m[3][1] * m[1][2];
            double Coef11 = m[1][1] * m[2][2] - m[2][1] * m[1][2];

            double Coef12 = m[2][0] * m[3][3] - m[3][0] * m[2][3];
            double Coef14 = m[1][0] * m[3][3] - m[3][0] * m[1][3];
            double Coef15 = m[1][0] * m[2][3] - m[2][0] * m[1][3];

            double Coef16 = m[2][0] * m[3][2] - m[3][0] * m[2][2];
            double Coef18 = m[1][0] * m[3][2] - m[3][0] * m[1][2];
            double Coef19 = m[1][0] * m[2][2] - m[2][0] * m[1][2];

            double Coef20 = m[2][0] * m[3][1] - m[3][0] * m[2][1];
            double Coef22 = m[1][0] * m[3][1] - m[3][0] * m[1][1];
            double Coef23 = m[1][0] * m[2][1] - m[2][0] * m[1][1];

            double[] Fac0 = new double[] { Coef00, Coef00, Coef02, Coef03 };
            double[] Fac1 = new double[] { Coef04, Coef04, Coef06, Coef07 };
            double[] Fac2 = new double[] { Coef08, Coef08, Coef10, Coef11 };
            double[] Fac3 = new double[] { Coef12, Coef12, Coef14, Coef15 };
            double[] Fac4 = new double[] { Coef16, Coef16, Coef18, Coef19 };
            double[] Fac5 = new double[] { Coef20, Coef20, Coef22, Coef23 };

            double[] Vec0 = new double[] { m[1][0], m[0][0], m[0][0], m[0][0] };
            double[] Vec1 = new double[] { m[1][1], m[0][1], m[0][1], m[0][1] };
            double[] Vertex = new double[] { m[1][2], m[0][2], m[0][2], m[0][2] };
            double[] Vec3 = new double[] { m[1][3], m[0][3], m[0][3], m[0][3] };

            double[] Inv0 = add(substract(mult(Vec1, Fac0), mult(Vertex, Fac1)), mult(Vec3, Fac2));
            double[] Inv1 = add(substract(mult(Vec0, Fac0), mult(Vertex, Fac3)), mult(Vec3, Fac4));
            double[] Inv2 = add(substract(mult(Vec0, Fac1), mult(Vec1, Fac3)), mult(Vec3, Fac5));
            double[] Inv3 = add(substract(mult(Vec0, Fac2), mult(Vec1, Fac4)), mult(Vertex, Fac5));

            double[] SignA = new double[] { +1, -1, +1, -1 };
            double[] SignB = new double[] { -1, +1, -1, +1 };
            Matrix Inverse = new Matrix(mult(Inv0, SignA), mult(Inv1, SignB), mult(Inv2, SignA), mult(Inv3, SignB));

            double[] Row0 = new double[] { Inverse[0][0], Inverse[1][0], Inverse[2][0], Inverse[3][0] };

            double[] Dot0 = mult(m[0], Row0);
            double Dot1 = Dot0[0] + Dot0[1] + Dot0[2] + Dot0[3];

            double OneOverDeterminant = (1f) / Dot1;

            return Inverse * OneOverDeterminant;
        }

        public static bool Valid(Matrix m)
        {
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
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

        public static double dot(double[] x, double[] y)
        {
            double[] tmp = mult(x, y);
            return tmp[0] + tmp[1] + tmp[2] + tmp[3];
        }

        private static double[] mult(double[] a, double[] b)
        {
            double[] ret = new double[N];
            for (int i = 0; i < N; i++)
                ret[i] = a[i] * b[i];
            return ret;
        }

        private static double[] add(double[] a, double[] b)
        {
            double[] ret = new double[N];
            for (int i = 0; i < N; i++)
                ret[i] = a[i] + b[i];
            return ret;
        }

        private static double[] substract(double[] a, double[] b)
        {
            double[] ret = new double[N];
            for (int i = 0; i < N; i++)
                ret[i] = a[i] - b[i];
            return ret;
        }

        private static double[] devide(double[] a, double[] b)
        {
            double[] ret = new double[N];
            for (int i = 0; i < N; i++)
                ret[i] = a[i] / b[i];
            return ret;
        }

        private static double[] mult(double a, double[] b)
        {
            double[] ret = new double[N];
            for (int i = 0; i < N; i++)
                ret[i] = a * b[i];
            return ret;
        }

        private static double[] add(double a, double[] b)
        {
            double[] ret = new double[N];
            for (int i = 0; i < N; i++)
                ret[i] = a + b[i];
            return ret;
        }

        private static double[] substract(double a, double[] b)
        {
            double[] ret = new double[N];
            for (int i = 0; i < N; i++)
                ret[i] = a - b[i];
            return ret;
        }

        private static double[] devide(double a, double[] b)
        {
            double[] ret = new double[N];
            for (int i = 0; i < N; i++)
                ret[i] = a / b[i];
            return ret;
        }

        private static double[] mult(double[] a, double b)
        {
            double[] ret = new double[N];
            for (int i = 0; i < N; i++)
                ret[i] = a[i] * b;
            return ret;
        }

        private static double[] add(double[] a, double b)
        {
            double[] ret = new double[N];
            for (int i = 0; i < N; i++)
                ret[i] = a[i] + b;
            return ret;
        }

        private static double[] substract(double[] a, double b)
        {
            double[] ret = new double[N];
            for (int i = 0; i < N; i++)
                ret[i] = a[i] - b;
            return ret;
        }

        private static double[] devide(double[] a, double b)
        {
            double[] ret = new double[N];
            for (int i = 0; i < N; i++)
                ret[i] = a[i] / b;
            return ret;
        }

        #endregion

        /// <summary>
        /// The columms of the matrix.
        /// </summary>
        private double[][] matrix;
	}
}