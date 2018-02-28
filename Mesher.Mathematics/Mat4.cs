using System;

namespace Mesher.Mathematics
{
    /// <summary>
    /// Represents a 4x4 matrix.
    /// </summary>
	public struct Mat4
    {
        public Vec4 Col0;
        public Vec4 Col1;
        public Vec4 Col2;
        public Vec4 Col3;

        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="Mat4"/> struct.
        /// This matrix is the identity matrix scaled by <paramref name="scale"/>.
        /// </summary>
        /// <param name="scale">The scale.</param>
		public Mat4(Single scale)
        {
            Col0 = new Vec4(scale, 0.0f, 0.0f, 0.0f);
            Col1 = new Vec4(0.0f, scale, 0.0f, 0.0f);
            Col2 = new Vec4(0.0f, 0.0f, scale, 0.0f);
            Col3 = new Vec4(0.0f, 0.0f, 0.0f, scale);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Mat4"/> struct.
        /// The matrix is initialised with the <paramref name="cols"/>.
        /// </summary>
        /// <param name="cols">The colums of the matrix.</param>
        public Mat4(Vec4[] cols)
        {
            Col0 = cols[0];
            Col1 = cols[1];
            Col2 = cols[2];
            Col3 = cols[3];
        }

        public Mat4(Single[] elements)
        {
            Col0 = new Vec4(elements[0], elements[4], elements[8], elements[12]);
            Col1 = new Vec4(elements[1], elements[5], elements[9], elements[13]);
            Col2 = new Vec4(elements[2], elements[6], elements[10], elements[14]);
            Col3 = new Vec4(elements[3], elements[7], elements[11], elements[15]);
        }

        public Mat4(Vec4 a, Vec4 b, Vec4 c, Vec4 d)
        {
            Col0 = a;
            Col1 = b;
            Col2 = c;
            Col3 = d;
        }

        /// <summary>
        /// Creates an identity matrix.
        /// </summary>
        /// <returns>A new identity matrix.</returns>
        public static Mat4 Identity()
        {
            return new Mat4(1);
        }

        #endregion


        #region Conversion      

        /// <summary>
        /// Returns the <see cref="Mat3"/> portion of this matrix.
        /// </summary>
        /// <returns>The <see cref="Mat3"/> portion of this matrix.</returns>
        public Mat3 ToMat3()
        {
            return new Mat3(new[] {
            new Vec3(this.Col0.X, this.Col0.Y, this.Col0.Z),
            new Vec3(this.Col1.X, this.Col1.Y, this.Col1.Z),
            new Vec3(this.Col2.X, this.Col2.Y, this.Col2.Z)});
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
                lhs.Col0.X * rhs.X + lhs.Col1.X * rhs.Y + lhs.Col2.X * rhs.Z + lhs.Col3.X * rhs.W,
                lhs.Col0.Y * rhs.X + lhs.Col1.Y * rhs.Y + lhs.Col2.Y * rhs.Z + lhs.Col3.Y * rhs.W,
                lhs.Col0.Z * rhs.X + lhs.Col1.Z * rhs.Y + lhs.Col2.Z * rhs.Z + lhs.Col3.Z * rhs.W,
                lhs.Col0.W * rhs.X + lhs.Col1.W * rhs.Y + lhs.Col2.W * rhs.Z + lhs.Col3.W * rhs.W
            );
        }

        /// <summary>
        /// Multiplies the <paramref name="lhs"/> matrix by the <paramref name="rhs"/> matrix.
        /// </summary>
        /// <param name="lhs">The LHS matrix.</param>
        /// <param name="rhs">The RHS matrix.</param>
        /// <returns>The product of <paramref name="lhs"/> and <paramref name="rhs"/>.</returns>
        public static Mat4 operator *(Mat4 lhs, Mat4 rhs)
        {
            return new Mat4(new[]
            {
                lhs.Col0.X * rhs.Col0 + lhs.Col1.X * rhs.Col1 + lhs.Col2.X * rhs.Col2 + lhs.Col3.X * rhs.Col3,
                lhs.Col0.Y * rhs.Col0 + lhs.Col1.Y * rhs.Col1 + lhs.Col2.Y * rhs.Col2 + lhs.Col3.Y * rhs.Col3,
                lhs.Col0.Z * rhs.Col0 + lhs.Col1.Z * rhs.Col1 + lhs.Col2.Z * rhs.Col2 + lhs.Col3.Z * rhs.Col3,
                lhs.Col0.W * rhs.Col0 + lhs.Col1.W * rhs.Col1 + lhs.Col2.W * rhs.Col2 + lhs.Col3.W * rhs.Col3
            });
        }

        public static Mat4 operator *(Mat4 lhs, Single s)
        {
            return new Mat4(new[]
            {
                lhs.Col0*s,
                lhs.Col1*s,
                lhs.Col2*s,
                lhs.Col3*s
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
        public static Mat4 Frustum(Single left, Single right, Single bottom, Single top, Single nearVal, Single farVal)
        {
            var result = Identity();
            result.Col0.X = 2.0f * nearVal / (right - left);
            result.Col1.Y = 2.0f * nearVal / (top - bottom);
            result.Col2.X = (right + left) / (right - left);
            result.Col2.Y = (top + bottom) / (top - bottom);
            result.Col2.Z = -(farVal + nearVal) / (farVal - nearVal);
            result.Col2.W = -1.0f;
            result.Col3.Z = -(2.0f * farVal * nearVal) / (farVal - nearVal);
            return result;
        }

        /// <summary>
        /// Creates a matrix for a symmetric perspective-view frustum with far plane at infinite.
        /// </summary>
        /// <param name="fovy">The fovy.</param>
        /// <param name="aspect">The aspect.</param>
        /// <param name="zNear">The z near.</param>
        /// <returns></returns>
        public static Mat4 InfinitePerspective(Single fovy, Single aspect, Single zNear)
        {

            var range = (Single)Math.Tan(fovy / 2f) * zNear;

            var left = -range * aspect;
            var right = range * aspect;
            var bottom = -range;
            var top = range;

            var result = new Mat4(0);
            result.Col0.X = 2f * zNear / (right - left);
            result.Col1.Y = 2f * zNear / (top - bottom);
            result.Col2.Z = -1f;
            result.Col2.W = -1f;
            result.Col3.Z = -2f * zNear;
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
            result.Col0.X = s.X;
            result.Col1.X = s.Y;
            result.Col2.X = s.Z;
            result.Col0.Y = u.X;
            result.Col1.Y = u.Y;
            result.Col2.Y = u.Z;
            result.Col0.Z = -f.X;
            result.Col1.Z = -f.Y;
            result.Col2.Z = -f.Z;
            result.Col3.X = -Vec3.Dot(s, eye);
            result.Col3.Y = -Vec3.Dot(u, eye);
            result.Col3.Z = Vec3.Dot(f, eye);
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
        public static Mat4 Ortho(Single left, Single right, Single bottom, Single top, Single zNear, Single zFar)
        {
            var result = Identity();
            result.Col0.X = 2f / (right - left);
            result.Col1.Y = 2f / (top - bottom);
            result.Col2.Z = -2f / (zFar - zNear);
            result.Col3.X = -(right + left) / (right - left);
            result.Col3.Y = -(top + bottom) / (top - bottom);
            result.Col3.Z = -(zFar + zNear) / (zFar - zNear);
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
        public static Mat4 Ortho(Single left, Single right, Single bottom, Single top)
        {
            var result = Identity();
            result.Col0.X = 2f / (right - left);
            result.Col1.Y = 2f / (top - bottom);
            result.Col2.Z = -1f;
            result.Col3.X = -(right + left) / (right - left);
            result.Col3.Y = -(top + bottom) / (top - bottom);
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
        public static Mat4 Perspective(Single fovy, Single aspect, Single zNear, Single zFar)
        {
            var tanHalfFovy = (Single)Math.Tan(fovy / 2.0f);

            var result = Identity();
            result.Col0.X = 1.0f / (aspect * tanHalfFovy);
            result.Col1.Y = 1.0f / tanHalfFovy;
            result.Col2.Z = -(zFar + zNear) / (zFar - zNear);
            result.Col2.W = -1.0f;
            result.Col3.Z = -(2.0f * zFar * zNear) / (zFar - zNear);
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
        public static Mat4 PerspectiveFov(Single fov, Single width, Single height, Single zNear, Single zFar)
        {
            if (width <= 0 || height <= 0 || fov <= 0)
                throw new ArgumentOutOfRangeException();

            var rad = fov;

            var h = (Single)(Math.Cos(0.5f * rad) / Math.Sin(0.5f * rad));
            var w = h * height / width;

            var result = new Mat4(0);
            result.Col0.X = w;
            result.Col1.Y = h;
            result.Col2.Z = -(zFar + zNear) / (zFar - zNear);
            result.Col2.W = -1f;
            result.Col3.Z = -(2f * zFar * zNear) / (zFar - zNear);
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

            if (!(delta.X > 0f && delta.Y > 0f))
                return result; // Error

            var temp = new Vec3(
                (viewport.Z - 2f * (center.X - viewport.X)) / delta.X,
                (viewport.W - 2f * (center.Y - viewport.Y)) / delta.Y,
                0f);

            // Translate and scale the picked region to the entire window
            result = Translate(result, temp);
            return Scale(result, new Vec3(viewport.Z / delta.X, viewport.W / delta.Y, 1));
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
            var tmp = new Vec4(obj, 1f);
            tmp = model * tmp;
            tmp = proj * tmp;

            tmp /= tmp.W;
            tmp = tmp * 0.5f + 0.5f;
            tmp.X = tmp.X * viewport.Z + viewport.X;
            tmp.Y = tmp.Y * viewport.W + viewport.Y;

            return new Vec3(tmp.X, tmp.Y, tmp.Z);
        }

        /// <summary>
        /// Builds a rotation 4 * 4 matrix created from an axis vector and an angle.
        /// </summary>
        /// <param name="m">The m.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="v">The v.</param>
        /// <returns></returns>
        public static Mat4 Rotate(Mat4 m, Single angle, Vec3 v)
        {
            var c = (Single)Math.Cos(angle);
            var s = (Single)Math.Sin(angle);

            var axis = Vec3.Normalize(v);
            var temp = (1.0f - c) * axis;

            var rotate = Identity();
            rotate.Col0.X = c + temp.X * axis.X;
            rotate.Col0.Y = 0 + temp.X * axis.Y + s * axis.Z;
            rotate.Col0.Z = 0 + temp.X * axis.Z - s * axis.Y;
            rotate.Col1.X = 0 + temp.Y * axis.X - s * axis.Z;
            rotate.Col1.Y = c + temp.Y * axis.Y;
            rotate.Col1.Z = 0 + temp.Y * axis.Z + s * axis.X;
            rotate.Col2.X = 0 + temp.Z * axis.X + s * axis.Y;
            rotate.Col2.Y = 0 + temp.Z * axis.Y - s * axis.X;
            rotate.Col2.Z = c + temp.Z * axis.Z;

            var result = Identity();
            result.Col0 = m.Col0 * rotate.Col0.X + m.Col1 * rotate.Col0.Y + m.Col2 * rotate.Col0.Z;
            result.Col1 = m.Col0 * rotate.Col1.X + m.Col1 * rotate.Col1.Y + m.Col2 * rotate.Col1.Z;
            result.Col2 = m.Col0 * rotate.Col2.X + m.Col1 * rotate.Col2.Y + m.Col2 * rotate.Col2.Z;
            result.Col3 = m.Col3;
            return result;
        }


        //  TODO: this is actually defined as an extension, put in the right file.
        public static Mat4 Rotate(Single angle, Vec3 v)
        {
            return Rotate(Identity(), angle, v);
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
            result.Col0 = m.Col0 * v.X;
            result.Col1 = m.Col1 * v.Y;
            result.Col2 = m.Col2 * v.Z;
            result.Col3 = m.Col3;
            return result;
        }

        public static Mat4 Scale(Vec3 v)
        {
            return Scale(Identity(), v);
        }

        public static Mat4 Scale(Single s)
        {
            return Scale(new Vec3(s));
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
            result.Col3 = m.Col0 * v.X + m.Col1 * v.Y + m.Col2 * v.Z + m.Col3;
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
        public static Mat4 TweakedInfinitePerspective(Single fovy, Single aspect, Single zNear)
        {
            var range = (Single)Math.Tan(fovy / 2) * zNear;
            var left = -range * aspect;
            var right = range * aspect;
            var bottom = -range;
            var top = range;

            var result = new Mat4(0f);
            result.Col0.X = 2 * zNear / (right - left);
            result.Col1.Y = 2 * zNear / (top - bottom);
            result.Col2.Z = 0.0001f - 1f;
            result.Col2.W = -1;
            result.Col3.Z = -(0.0001f - 2) * zNear;
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
            var inverse = Inverse(proj);

            var tmp = new Vec4(win, 1f);
            tmp.X = (tmp.X - viewport.X) / viewport.Z;
            tmp.Y = (tmp.Y - viewport.Y) / viewport.W;
            tmp = tmp * 2f - 1f;

            var obj = inverse * tmp;
            obj /= obj.W;

            obj = Inverse(model) * obj;

            return new Vec3(obj);
        }

        public Mat4 Inverse()
        {
            return Inverse(this);
        }

        public static Mat4 Inverse(Mat4 m)
        {
            var coef00 = m.Col2.Z * m.Col3.W - m.Col3.Z * m.Col2.W;
            var coef02 = m.Col1.Z * m.Col3.W - m.Col3.Z * m.Col1.W;
            var coef03 = m.Col1.Z * m.Col2.W - m.Col2.Z * m.Col1.W;

            var coef04 = m.Col2.Y * m.Col3.W - m.Col3.Y * m.Col2.W;
            var coef06 = m.Col1.Y * m.Col3.W - m.Col3.Y * m.Col1.W;
            var coef07 = m.Col1.Y * m.Col2.W - m.Col2.Y * m.Col1.W;

            var coef08 = m.Col2.Y * m.Col3.Z - m.Col3.Y * m.Col2.Z;
            var coef10 = m.Col1.Y * m.Col3.Z - m.Col3.Y * m.Col1.Z;
            var coef11 = m.Col1.Y * m.Col2.Z - m.Col2.Y * m.Col1.Z;

            var coef12 = m.Col2.X * m.Col3.W - m.Col3.X * m.Col2.W;
            var coef14 = m.Col1.X * m.Col3.W - m.Col3.X * m.Col1.W;
            var coef15 = m.Col1.X * m.Col2.W - m.Col2.X * m.Col1.W;

            var coef16 = m.Col2.X * m.Col3.Z - m.Col3.X * m.Col2.Z;
            var coef18 = m.Col1.X * m.Col3.Z - m.Col3.X * m.Col1.Z;
            var coef19 = m.Col1.X * m.Col2.Z - m.Col2.X * m.Col1.Z;

            var coef20 = m.Col2.X * m.Col3.Y - m.Col3.X * m.Col2.Y;
            var coef22 = m.Col1.X * m.Col3.Y - m.Col3.X * m.Col1.Y;
            var coef23 = m.Col1.X * m.Col2.Y - m.Col2.X * m.Col1.Y;

            var fac0 = new Vec4(coef00, coef00, coef02, coef03);
            var fac1 = new Vec4(coef04, coef04, coef06, coef07);
            var fac2 = new Vec4(coef08, coef08, coef10, coef11);
            var fac3 = new Vec4(coef12, coef12, coef14, coef15);
            var fac4 = new Vec4(coef16, coef16, coef18, coef19);
            var fac5 = new Vec4(coef20, coef20, coef22, coef23);

            var vec0 = new Vec4(m.Col1.X, m.Col0.X, m.Col0.X, m.Col0.X);
            var vec1 = new Vec4(m.Col1.Y, m.Col0.Y, m.Col0.Y, m.Col0.Y);
            var vec2 = new Vec4(m.Col1.Z, m.Col0.Z, m.Col0.Z, m.Col0.Z);
            var vec3 = new Vec4(m.Col1.W, m.Col0.W, m.Col0.W, m.Col0.W);

            var inv0 = new Vec4(vec1 * fac0 - vec2 * fac1 + vec3 * fac2);
            var inv1 = new Vec4(vec0 * fac0 - vec2 * fac3 + vec3 * fac4);
            var inv2 = new Vec4(vec0 * fac1 - vec1 * fac3 + vec3 * fac5);
            var inv3 = new Vec4(vec0 * fac2 - vec1 * fac4 + vec2 * fac5);

            var signA = new Vec4(+1, -1, +1, -1);
            var signB = new Vec4(-1, +1, -1, +1);
            var inverse = new Mat4(inv0 * signA, inv1 * signB, inv2 * signA, inv3 * signB);

            var row0 = new Vec4(inverse.Col0.X, inverse.Col1.X, inverse.Col2.X, inverse.Col3.X);

            var dot0 = new Vec4(m.Col0 * row0);
            var dot1 = dot0.X + dot0.Y + (dot0.Z + dot0.W);

            var oneOverDeterminant = 1f / dot1;

            return inverse * oneOverDeterminant;
        }

        public Single[] ToArray()
        {
            var ret = new Single[]
            {
                Col0.X,
                Col0.Y,
                Col0.Z,
                Col0.W,
                Col1.X,
                Col1.Y,
                Col1.Z,
                Col1.W,
                Col2.X,
                Col2.Y,
                Col2.Z,
                Col2.W,
                Col3.X,
                Col3.Y,
                Col3.Z,
                Col3.W,
            };

            return ret;
        }
    }
}