using System;
using System.Linq;

namespace Mesher.Mathematics 
{
    /// <summary>
    /// Represents a 4x4 matrix.
    /// </summary>
	public class Mat4
    {
        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="Mat4"/> struct.
        /// This matrix is the identity matrix scaled by <paramref name="scale"/>.
        /// </summary>
        /// <param name="scale">The scale.</param>
		public Mat4(double scale)
        {
            m_cols = new []
            {
                new Vec4(scale, 0.0f, 0.0f, 0.0f),
                new Vec4(0.0f, scale, 0.0f, 0.0f),
                new Vec4(0.0f, 0.0f, scale, 0.0f),
                new Vec4(0.0f, 0.0f, 0.0f, scale),
            };
        }

        public Mat4():this(1) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Mat4"/> struct.
        /// The matrix is initialised with the <paramref name="cols"/>.
        /// </summary>
        /// <param name="cols">The colums of the matrix.</param>
        public Mat4(Vec4[] cols)
        {
            this.m_cols = new []
            {
                cols[0],
                cols[1],
                cols[2],
                cols[3]
            };
        }

        public Mat4(Vec4 a, Vec4 b, Vec4 c, Vec4 d)
        {
            this.m_cols = new[]
            {
                a, b, c, d
            };
        }

        /// <summary>
        /// Creates an identity matrix.
        /// </summary>
        /// <returns>A new identity matrix.</returns>
        public static Mat4 Identity()
        {
            return new Mat4();
        }

        #endregion

        #region Index Access

        /// <summary>
        /// Gets or sets the <see cref="Vec4"/> column at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="Vec4"/> column.
        /// </value>
        /// <param name="column">The column index.</param>
        /// <returns>The column at index <paramref name="column"/>.</returns>
        public Vec4 this[int column]
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
        /// Returns the <see cref="Mat3"/> portion of this matrix.
        /// </summary>
        /// <returns>The <see cref="Mat3"/> portion of this matrix.</returns>
        public Mat3 to_mat3()
        {
            return new Mat3(new[] {
			new Vec3(m_cols[0][0], m_cols[0][1], m_cols[0][2]),
			new Vec3(m_cols[1][0], m_cols[1][1], m_cols[1][2]),
			new Vec3(m_cols[2][0], m_cols[2][1], m_cols[2][2])});
        }

        #endregion

        #region Multiplication

        /// <summary>
        /// Multiplies the <paramref name="lhs"/> matrix by the <paramref name="rhs"/> vector.
        /// </summary>
        /// <param name="lhs">The LHS matrix.</param>
        /// <param name="rhs">The RHS vector.</param>
        /// <returns>The product of <paramref name="lhs"/> and <paramref name="rhs"/>.</returns>
        public static Vec4 operator *(Mat4 lhs, Vec4 rhs)
        {
            return new Vec4(
                lhs[0, 0] * rhs[0] + lhs[1, 0] * rhs[1] + lhs[2, 0] * rhs[2] + lhs[3, 0] * rhs[3],
                lhs[0, 1] * rhs[0] + lhs[1, 1] * rhs[1] + lhs[2, 1] * rhs[2] + lhs[3, 1] * rhs[3],
                lhs[0, 2] * rhs[0] + lhs[1, 2] * rhs[1] + lhs[2, 2] * rhs[2] + lhs[3, 2] * rhs[3],
                lhs[0, 3] * rhs[0] + lhs[1, 3] * rhs[1] + lhs[2, 3] * rhs[2] + lhs[3, 3] * rhs[3]
            );
        }

        /// <summary>
        /// Multiplies the <paramref name="lhs"/> matrix by the <paramref name="rhs"/> matrix.
        /// </summary>
        /// <param name="lhs">The LHS matrix.</param>
        /// <param name="rhs">The RHS matrix.</param>
        /// <returns>The product of <paramref name="lhs"/> and <paramref name="rhs"/>.</returns>
        public static Mat4 operator * (Mat4 lhs, Mat4 rhs)
        {
            return new Mat4(new []
            {
			    lhs[0][0] * rhs[0] + lhs[1][0] * rhs[1] + lhs[2][0] * rhs[2] + lhs[3][0] * rhs[3],
			    lhs[0][1] * rhs[0] + lhs[1][1] * rhs[1] + lhs[2][1] * rhs[2] + lhs[3][1] * rhs[3],
			    lhs[0][2] * rhs[0] + lhs[1][2] * rhs[1] + lhs[2][2] * rhs[2] + lhs[3][2] * rhs[3],
			    lhs[0][3] * rhs[0] + lhs[1][3] * rhs[1] + lhs[2][3] * rhs[2] + lhs[3][3] * rhs[3]
            });
        }

        public static Mat4 operator *(Mat4 lhs, double s)
        {
            return new Mat4(new[]
            {
                lhs[0]*s,
                lhs[1]*s,
                lhs[2]*s,
                lhs[3]*s
            });
        }

        #endregion

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
        public static Mat4 Frustum(double left, double right, double bottom, double top, double nearVal, double farVal)
        {
            var result = Mat4.Identity();
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
        public static Mat4 InfinitePerspective(double fovy, double aspect, double zNear)
        {

            var range = Math.Tan(fovy / (2f)) * zNear;

            var left = -range * aspect;
            var right = range * aspect;
            var bottom = -range;
            var top = range;

            var result = new Mat4(0);
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
        public static Mat4 LookAt(Vec3 eye, Vec3 center, Vec3 up)
        {
            var f = new Vec3(Vec3.Normalize(center - eye));
            var s = new Vec3(Vec3.Normalize(Vec3.Cross(f, up)));
            var u = new Vec3(Vec3.Cross(s, f));

            var result = new Mat4(1);
            result[0, 0] = s.X;
            result[1, 0] = s.Y;
            result[2, 0] = s.Z;
            result[0, 1] = u.X;
            result[1, 1] = u.Y;
            result[2, 1] = u.Z;
            result[0, 2] = -f.X;
            result[1, 2] = -f.Y;
            result[2, 2] = -f.Z;
            result[3, 0] = -Vec3.Dot(s, eye);
            result[3, 1] = -Vec3.Dot(u, eye);
            result[3, 2] = Vec3.Dot(f, eye);
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
        public static Mat4 Ortho(double left, double right, double bottom, double top, double zNear, double zFar)
        {
            var result = Mat4.Identity();
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
        public static Mat4 Ortho(double left, double right, double bottom, double top)
        {
            var result = Mat4.Identity();
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
        /// <returns>A <see cref="Mat4"/> that contains the projection matrix for the perspective transformation.</returns>
        public static Mat4 Perspective(double fovy, double aspect, double zNear, double zFar)
        {
            var tanHalfFovy = (double)Math.Tan(fovy / 2.0f);

            var result = Mat4.Identity();
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
        public static Mat4 PerspectiveFov(double fov, double width, double height, double zNear, double zFar)
        {
            if (width <= 0 || height <= 0 || fov <= 0)
                throw new ArgumentOutOfRangeException();

            var rad = fov;

            var h = Math.Cos((0.5f) * rad) / Math.Sin((0.5f) * rad);
            var w = h * height / width;

            var result = new Mat4(0);
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
        public static Mat4 PickMatrix(Vec2 center, Vec2 delta, Vec4 viewport)
        {
            if (delta.X <= 0 || delta.Y <= 0)
                throw new ArgumentOutOfRangeException();
            var result = new Mat4(1.0f);

            if (!(delta.X > (0f) && delta.Y > (0f)))
                return result; // Error

            var temp = new Vec3(
                ((viewport[2]) - (2f) * (center.X - (viewport[0]))) / delta.X,
                ((viewport[3]) - (2f) * (center.Y - (viewport[1]))) / delta.Y,
                (0f));

            // Translate and scale the picked region to the entire window
            result = Translate(result, temp);
            return Scale(result, new Vec3((viewport[2]) / delta.X, (viewport[3]) / delta.Y, (1)));
        }

        /// <summary>
        /// Map the specified object coordinates (obj.x, obj.y, obj.z) into window coordinates.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="model">The model.</param>
        /// <param name="proj">The proj.</param>
        /// <param name="viewport">The viewport.</param>
        /// <returns></returns>
        public static Vec3 Project(Vec3 obj, Mat4 model, Mat4 proj, Vec4 viewport)

        {
            var tmp = new Vec4(obj, (1f));
            tmp = model * tmp;
            tmp = proj * tmp;

            tmp /= tmp.W;
            tmp = tmp * 0.5f + 0.5f;
            tmp[0] = tmp[0] * viewport[2] + viewport[0];
            tmp[1] = tmp[1] * viewport[3] + viewport[1];

            return new Vec3(tmp.X, tmp.Y, tmp.Z);
        }

        /// <summary>
        /// Builds a rotation 4 * 4 matrix created from an axis vector and an angle.
        /// </summary>
        /// <param name="m">The m.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="v">The v.</param>
        /// <returns></returns>
        public static Mat4 Rotate(Mat4 m, double angle, Vec3 v)
        {
            var c = Math.Cos(angle);
            var s = Math.Sin(angle);

            var axis = Vec3.Normalize(v);
            var temp = (1.0f - c) * axis;

            var rotate = Mat4.Identity();
            rotate[0, 0] = c + temp[0] * axis[0];
            rotate[0, 1] = 0 + temp[0] * axis[1] + s * axis[2];
            rotate[0, 2] = 0 + temp[0] * axis[2] - s * axis[1];

            rotate[1, 0] = 0 + temp[1] * axis[0] - s * axis[2];
            rotate[1, 1] = c + temp[1] * axis[1];
            rotate[1, 2] = 0 + temp[1] * axis[2] + s * axis[0];

            rotate[2, 0] = 0 + temp[2] * axis[0] + s * axis[1];
            rotate[2, 1] = 0 + temp[2] * axis[1] - s * axis[0];
            rotate[2, 2] = c + temp[2] * axis[2];

            var result = Mat4.Identity();
            result[0] = m[0] * rotate[0][0] + m[1] * rotate[0][1] + m[2] * rotate[0][2];
            result[1] = m[0] * rotate[1][0] + m[1] * rotate[1][1] + m[2] * rotate[1][2];
            result[2] = m[0] * rotate[2][0] + m[1] * rotate[2][1] + m[2] * rotate[2][2];
            result[3] = m[3];
            return result;
        }


        //  TODO: this is actually defined as an extension, put in the right file.
        public static Mat4 Rotate(double angle, Vec3 v)
        {
            return Rotate(Mat4.Identity(), angle, v);
        }


        /// <summary>
        /// Applies a scale transformation to matrix <paramref name="m"/> by vector <paramref name="v"/>.
        /// </summary>
        /// <param name="m">The matrix to transform.</param>
        /// <param name="v">The vector to scale by.</param>
        /// <returns><paramref name="m"/> scaled by <paramref name="v"/>.</returns>
        public static Mat4 Scale(Mat4 m, Vec3 v)
        {
            var result = m;
            result[0] = m[0] * v[0];
            result[1] = m[1] * v[1];
            result[2] = m[2] * v[2];
            result[3] = m[3];
            return result;
        }

        /// <summary>
        /// Applies a translation transformation to matrix <paramref name="m"/> by vector <paramref name="v"/>.
        /// </summary>
        /// <param name="m">The matrix to transform.</param>
        /// <param name="v">The vector to translate by.</param>
        /// <returns><paramref name="m"/> translated by <paramref name="v"/>.</returns>
        public static Mat4 Translate(Mat4 m, Vec3 v)
        {
            var result = m;
            result[3] = m[0] * v[0] + m[1] * v[1] + m[2] * v[2] + m[3];
            return result;
        }

        public static Mat4 Translate(Vec3 v)
        {
            return Translate(Identity(), v);
        }

        /// <summary>
        /// Creates a matrix for a symmetric perspective-view frustum with far plane 
        /// at infinite for graphics hardware that doesn't support depth clamping.
        /// </summary>
        /// <param name="fovy">The fovy.</param>
        /// <param name="aspect">The aspect.</param>
        /// <param name="zNear">The z near.</param>
        /// <returns></returns>
        public static Mat4 TweakedInfinitePerspective(double fovy, double aspect, double zNear)
        {
            var range = Math.Tan(fovy / (2)) * zNear;
            var left = -range * aspect;
            var right = range * aspect;
            var bottom = -range;
            var top = range;

            var result = new Mat4((0f));
            result[0, 0] = ((2) * zNear) / (right - left);
            result[1, 1] = ((2) * zNear) / (top - bottom);
            result[2, 2] = (0.0001f) - (1f);
            result[2, 3] = (-1);
            result[3, 2] = -((0.0001f) - (2)) * zNear;
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
        public static Vec3 UnProject(Vec3 win, Mat4 model, Mat4 proj, Vec4 viewport)
        {
            var inverse = Mat4.Inverse(proj * model);

            var tmp = new Vec4(win, (1f));
            tmp.X = (tmp.X - (viewport[0])) / (viewport[2]);
            tmp.Y = (tmp.Y - (viewport[1])) / (viewport[3]);
            tmp = tmp * (2f) - (1f);

            var obj = inverse * tmp;
            obj /= obj.W;

            return new Vec3(obj);
        }

        public Mat4 Inverse()
        {
            return Inverse(this);
        }

        public static Mat4 Inverse(Mat4 m)
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

            var fac0 = new Vec4(coef00, coef00, coef02, coef03);
            var fac1 = new Vec4(coef04, coef04, coef06, coef07);
            var fac2 = new Vec4(coef08, coef08, coef10, coef11);
            var fac3 = new Vec4(coef12, coef12, coef14, coef15);
            var fac4 = new Vec4(coef16, coef16, coef18, coef19);
            var fac5 = new Vec4(coef20, coef20, coef22, coef23);

            var vec0 = new Vec4(m[1][0], m[0][0], m[0][0], m[0][0]);
            var vec1 = new Vec4(m[1][1], m[0][1], m[0][1], m[0][1]);
            var vec2 = new Vec4(m[1][2], m[0][2], m[0][2], m[0][2]);
            var vec3 = new Vec4(m[1][3], m[0][3], m[0][3], m[0][3]);

            var inv0 = new Vec4(vec1 * fac0 - vec2 * fac1 + vec3 * fac2);
            var inv1 = new Vec4(vec0 * fac0 - vec2 * fac3 + vec3 * fac4);
            var inv2 = new Vec4(vec0 * fac1 - vec1 * fac3 + vec3 * fac5);
            var inv3 = new Vec4(vec0 * fac2 - vec1 * fac4 + vec2 * fac5);

            var signA = new Vec4(+1, -1, +1, -1);
            var signB = new Vec4(-1, +1, -1, +1);
            var Inverse = new Mat4(inv0 * signA, inv1 * signB, inv2 * signA, inv3 * signB);

            var row0 = new Vec4(Inverse[0][0], Inverse[1][0], Inverse[2][0], Inverse[3][0]);

            var dot0 = new Vec4(m[0] * row0);
            var dot1 = (dot0.X + dot0.Y) + (dot0.Z + dot0.W);

            var oneOverDeterminant = (1f) / dot1;

            return Inverse * oneOverDeterminant;
        }

        public double[] ToArray()
        {
            var ret = new double[16];
            for (var i = 0; i < ret.Length; i++)
                ret[i] = this[i / 4, i % 4];
            return ret;
        }

        /// <summary>
        /// The columms of the matrix.
        /// </summary>
        private Vec4[] m_cols;
	}
}