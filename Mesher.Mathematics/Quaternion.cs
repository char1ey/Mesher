/*
Copyright (c) 2006 - 2008 The Open Toolkit library.

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;
using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Mesher.Mathematics
{
    /// <summary>
    /// Represents a double-precision Quaternion.
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Quaternion : IEquatable<Quaternion>
    {
        /// <summary>
        /// The X, Y and Z components of this instance.
        /// </summary>
        public Vector3 Xyz;

        /// <summary>
        /// The W component of this instance.
        /// </summary>
        public double W;

        /// <summary>
        /// Construct a new Quaternion from vector and w components
        /// </summary>
        /// <param name="v">The vector part</param>
        /// <param name="w">The w part</param>
        public Quaternion(Vector3 v, double w)
        {
            Xyz = v;
            W = w;
        }

        /// <summary>
        /// Construct a new Quaternion
        /// </summary>
        /// <param name="x">The x component</param>
        /// <param name="y">The y component</param>
        /// <param name="z">The z component</param>
        /// <param name="w">The w component</param>
        public Quaternion(double x, double y, double z, double w)
            : this(new Vector3(x, y, z), w)
        { }

        /// <summary>
        /// Construct a new Quaternion from given Euler angles
        /// </summary>
        /// <param name="pitch">The pitch (attitude), rotation around X axis</param>
        /// <param name="yaw">The yaw (heading), rotation around Y axis</param>
        /// <param name="roll">The roll (bank), rotation around Z axis</param>
        public Quaternion(double pitch, double yaw, double roll)
        {
            yaw *= 0.5;
            pitch *= 0.5;
            roll *= 0.5;

            var c1 = Math.Cos(yaw);
            var c2 = Math.Cos(pitch);
            var c3 = Math.Cos(roll);
            var s1 = Math.Sin(yaw);
            var s2 = Math.Sin(pitch);
            var s3 = Math.Sin(roll);

            W = c1 * c2 * c3 - s1 * s2 * s3;
            Xyz.X = s1 * s2 * c3 + c1 * c2 * s3;
            Xyz.Y = s1 * c2 * c3 + c1 * s2 * s3;
            Xyz.Z = c1 * s2 * c3 - s1 * c2 * s3;
        }

        /// <summary>
        /// Construct a new Quaternion from given Euler angles
        /// </summary>
        /// <param name="eulerAngles">The euler angles as a Vector3d</param>
        public Quaternion(Vector3 eulerAngles)
            :this(eulerAngles.X, eulerAngles.Y, eulerAngles.Z)
        { }

        /// <summary>
        /// Gets or sets the X component of this instance.
        /// </summary>
        [XmlIgnore]
        public double X { get { return Xyz.X; } set { Xyz.X = value; } }

        /// <summary>
        /// Gets or sets the Y component of this instance.
        /// </summary>
        [XmlIgnore]
        public double Y { get { return Xyz.Y; } set { Xyz.Y = value; } }

        /// <summary>
        /// Gets or sets the Z component of this instance.
        /// </summary>
        [XmlIgnore]
        public double Z { get { return Xyz.Z; } set { Xyz.Z = value; } }

        /// <summary>
        /// Convert the current quaternion to axis angle representation
        /// </summary>
        /// <param name="axis">The resultant axis</param>
        /// <param name="angle">The resultant angle</param>
        public void ToAxisAngle(out Vector3 axis, out double angle)
        {
            var result = ToAxisAngle();
            axis = result.Xyz;
            angle = result.W;
        }

        /// <summary>
        /// Convert this instance to an axis-angle representation.
        /// </summary>
        /// <returns>A Vector4 that is the axis-angle representation of this quaternion.</returns>
        public Vector4 ToAxisAngle()
        {
            var q = this;
            if (Math.Abs(q.W) > 1.0f)
            {
                q.Normalize();
            }

            var result = new Vector4();

            result.W = 2.0f * (float)Math.Acos(q.W); // angle
            var den = (float)Math.Sqrt(1.0 - q.W * q.W);
            if (den > 0.0001f)
            {
                result.Xyz = q.Xyz / den;
            }
            else
            {
                // This occurs when the angle is zero.
                // Not a problem: just set an arbitrary normalized axis.
                result.Xyz = Vector3.UnitX;
            }

            return result;
        }

        /// <summary>
        /// Gets the length (magnitude) of the Quaternion.
        /// </summary>
        /// <seealso cref="LengthSquared"/>
        public double Length
        {
            get
            {
                return Math.Sqrt(W * W + Xyz.LengthSquared);
            }
        }

        /// <summary>
        /// Gets the square of the Quaternion length (magnitude).
        /// </summary>
        public double LengthSquared
        {
            get
            {
                return W * W + Xyz.LengthSquared;
            }
        }

        /// <summary>
        /// Returns a copy of the Quaternion scaled to unit length.
        /// </summary>
        public Quaternion Normalized()
        {
            var q = this;
            q.Normalize();
            return q;
        }

        /// <summary>
        /// Reverses the rotation angle of this Quaternion.
        /// </summary>
        public void Invert()
        {
            W = -W;
        }

        /// <summary>
        /// Returns a copy of this Quaternion with its rotation angle reversed.
        /// </summary>
        public Quaternion Inverted()
        {
            var q = this;
            q.Invert();
            return q;
        }

        /// <summary>
        /// Scales the Quaternion to unit length.
        /// </summary>
        public void Normalize()
        {
            var scale = 1.0f / Length;
            Xyz *= scale;
            W *= scale;
        }

        /// <summary>
        /// Inverts the Vector3d component of this Quaternion.
        /// </summary>
        public void Conjugate()
        {
            Xyz = -Xyz;
        }

        /// <summary>
        /// Defines the identity quaternion.
        /// </summary>
        public static readonly Quaternion Identity = new Quaternion(0, 0, 0, 1);

        /// <summary>
        /// Add two quaternions
        /// </summary>
        /// <param name="left">The first operand</param>
        /// <param name="right">The second operand</param>
        /// <returns>The result of the addition</returns>
        public static Quaternion Add(Quaternion left, Quaternion right)
        {
            return new Quaternion(
                left.Xyz + right.Xyz,
                left.W + right.W);
        }

        /// <summary>
        /// Add two quaternions
        /// </summary>
        /// <param name="left">The first operand</param>
        /// <param name="right">The second operand</param>
        /// <param name="result">The result of the addition</param>
        public static void Add(ref Quaternion left, ref Quaternion right, out Quaternion result)
        {
            result = new Quaternion(
                left.Xyz + right.Xyz,
                left.W + right.W);
        }

        /// <summary>
        /// Subtracts two instances.
        /// </summary>
        /// <param name="left">The left instance.</param>
        /// <param name="right">The right instance.</param>
        /// <returns>The result of the operation.</returns>
        public static Quaternion Sub(Quaternion left, Quaternion right)
        {
            return new Quaternion(
                left.Xyz - right.Xyz,
                left.W - right.W);
        }

        /// <summary>
        /// Subtracts two instances.
        /// </summary>
        /// <param name="left">The left instance.</param>
        /// <param name="right">The right instance.</param>
        /// <param name="result">The result of the operation.</param>
        public static void Sub(ref Quaternion left, ref Quaternion right, out Quaternion result)
        {
            result = new Quaternion(
                left.Xyz - right.Xyz,
                left.W - right.W);
        }

        /// <summary>
        /// Multiplies two instances.
        /// </summary>
        /// <param name="left">The first instance.</param>
        /// <param name="right">The second instance.</param>
        /// <returns>A new instance containing the result of the calculation.</returns>
        public static Quaternion Multiply(Quaternion left, Quaternion right)
        {
            Quaternion result;
            Multiply(ref left, ref right, out result);
            return result;
        }

        /// <summary>
        /// Multiplies two instances.
        /// </summary>
        /// <param name="left">The first instance.</param>
        /// <param name="right">The second instance.</param>
        /// <param name="result">A new instance containing the result of the calculation.</param>
        public static void Multiply(ref Quaternion left, ref Quaternion right, out Quaternion result)
        {
            result = new Quaternion(
                right.W * left.Xyz + left.W * right.Xyz + Vector3.Cross(left.Xyz, right.Xyz),
                left.W * right.W - Vector3.Dot(left.Xyz, right.Xyz));
        }

        /// <summary>
        /// Multiplies an instance by a scalar.
        /// </summary>
        /// <param name="quaternion">The instance.</param>
        /// <param name="scale">The scalar.</param>
        /// <param name="result">A new instance containing the result of the calculation.</param>
        public static void Multiply(ref Quaternion quaternion, double scale, out Quaternion result)
        {
            result = new Quaternion(quaternion.X * scale, quaternion.Y * scale, quaternion.Z * scale, quaternion.W * scale);
        }

        /// <summary>
        /// Multiplies an instance by a scalar.
        /// </summary>
        /// <param name="quaternion">The instance.</param>
        /// <param name="scale">The scalar.</param>
        /// <returns>A new instance containing the result of the calculation.</returns>
        public static Quaternion Multiply(Quaternion quaternion, double scale)
        {
            return new Quaternion(quaternion.X * scale, quaternion.Y * scale, quaternion.Z * scale, quaternion.W * scale);
        }

        /// <summary>
        /// Get the conjugate of the given Quaternion
        /// </summary>
        /// <param name="q">The Quaternion</param>
        /// <returns>The conjugate of the given Quaternion</returns>
        public static Quaternion Conjugate(Quaternion q)
        {
            return new Quaternion(-q.Xyz, q.W);
        }

        /// <summary>
        /// Get the conjugate of the given Quaternion
        /// </summary>
        /// <param name="q">The Quaternion</param>
        /// <param name="result">The conjugate of the given Quaternion</param>
        public static void Conjugate(ref Quaternion q, out Quaternion result)
        {
            result = new Quaternion(-q.Xyz, q.W);
        }

        /// <summary>
        /// Get the inverse of the given Quaternion
        /// </summary>
        /// <param name="q">The Quaternion to invert</param>
        /// <returns>The inverse of the given Quaternion</returns>
        public static Quaternion Invert(Quaternion q)
        {
            Quaternion result;
            Invert(ref q, out result);
            return result;
        }

        /// <summary>
        /// Get the inverse of the given Quaternion
        /// </summary>
        /// <param name="q">The Quaternion to invert</param>
        /// <param name="result">The inverse of the given Quaternion</param>
        public static void Invert(ref Quaternion q, out Quaternion result)
        {
            var lengthSq = q.LengthSquared;
            if (lengthSq != 0.0)
            {
                var i = 1.0f / lengthSq;
                result = new Quaternion(q.Xyz * -i, q.W * i);
            }
            else
            {
                result = q;
            }
        }

        /// <summary>
        /// Scale the given Quaternion to unit length
        /// </summary>
        /// <param name="q">The Quaternion to normalize</param>
        /// <returns>The normalized Quaternion</returns>
        public static Quaternion Normalize(Quaternion q)
        {
            Quaternion result;
            Normalize(ref q, out result);
            return result;
        }

        /// <summary>
        /// Scale the given Quaternion to unit length
        /// </summary>
        /// <param name="q">The Quaternion to normalize</param>
        /// <param name="result">The normalized Quaternion</param>
        public static void Normalize(ref Quaternion q, out Quaternion result)
        {
            var scale = 1.0f / q.Length;
            result = new Quaternion(q.Xyz * scale, q.W * scale);
        }

        /// <summary>
        /// Build a Quaternion from the given axis and angle
        /// </summary>
        /// <param name="axis">The axis to rotate about</param>
        /// <param name="angle">The rotation angle in radians</param>
        /// <returns></returns>
        public static Quaternion FromAxisAngle(Vector3 axis, double angle)
        {
            if (axis.LengthSquared == 0.0f)
            {
                return Identity;
            }

            var result = Identity;

            angle *= 0.5f;
            axis.Normalize();
            result.Xyz = axis * Math.Sin(angle);
            result.W = Math.Cos(angle);

            return Normalize(result);
        }

        /// <summary>
        /// Builds a Quaternion from the given euler angles
        /// </summary>
        /// <param name="pitch">The pitch (attitude), rotation around X axis</param>
        /// <param name="yaw">The yaw (heading), rotation around Y axis</param>
        /// <param name="roll">The roll (bank), rotation around Z axis</param>
        /// <returns></returns>
        public static Quaternion FromEulerAngles(double pitch, double yaw, double roll)
        {
            return new Quaternion(pitch, yaw, roll);
        }

        /// <summary>
        /// Builds a Quaternion from the given euler angles
        /// </summary>
        /// <param name="eulerAngles">The euler angles as a vector</param>
        /// <returns>The equivalent Quaternion</returns>
        public static Quaternion FromEulerAngles(Vector3 eulerAngles)
        {
            return new Quaternion(eulerAngles);
        }

        /// <summary>
        /// Builds a Quaternion from the given euler angles
        /// </summary>
        /// <param name="eulerAngles">The euler angles a vector</param>
        /// <param name="result">The equivalent Quaternion</param>
        public static void FromEulerAngles(ref Vector3 eulerAngles, out Quaternion result)
        {
            var c1 = Math.Cos(eulerAngles.Y * 0.5);
            var c2 = Math.Cos(eulerAngles.X * 0.5);
            var c3 = Math.Cos(eulerAngles.Z * 0.5);
            var s1 = Math.Sin(eulerAngles.Y * 0.5);
            var s2 = Math.Sin(eulerAngles.X * 0.5);
            var s3 = Math.Sin(eulerAngles.Z * 0.5);

            result.W = c1 * c2 * c3 - s1 * s2 * s3;
            result.Xyz.X = s1 * s2 * c3 + c1 * c2 * s3;
            result.Xyz.Y = s1 * c2 * c3 + c1 * s2 * s3;
            result.Xyz.Z = c1 * s2 * c3 - s1 * c2 * s3;
        }

        /// <summary>
        /// Builds a quaternion from the given rotation matrix
        /// </summary>
        /// <param name="matrix">A rotation matrix</param>
        /// <returns>The equivalent quaternion</returns>
        public static Quaternion FromMatrix(Matrix3 matrix)
        {
            Quaternion result;
            FromMatrix(ref matrix, out result);
            return result;
        }

        /// <summary>
        /// Builds a quaternion from the given rotation matrix
        /// </summary>
        /// <param name="matrix">A rotation matrix</param>
        /// <param name="result">The equivalent quaternion</param>
        public static void FromMatrix(ref Matrix3 matrix, out Quaternion result)
        {
            var trace = matrix.Trace;

            if (trace > 0)
            {
                var s = Math.Sqrt(trace + 1) * 2;
                var invS = 1.0 / s;

                result.W = s * 0.25;
                result.Xyz.X = (matrix.Row2.Y - matrix.Row1.Z) * invS;
                result.Xyz.Y = (matrix.Row0.Z - matrix.Row2.X) * invS;
                result.Xyz.Z = (matrix.Row1.X - matrix.Row0.Y) * invS;
            }
            else
            {
                double m00 = matrix.Row0.X, m11 = matrix.Row1.Y, m22 = matrix.Row2.Z;

                if (m00 > m11 && m00 > m22)
                {
                    var s = Math.Sqrt(1 + m00 - m11 - m22) * 2;
                    var invS = 1.0 / s;

                    result.W = (matrix.Row2.Y - matrix.Row1.Z) * invS;
                    result.Xyz.X = s * 0.25;
                    result.Xyz.Y = (matrix.Row0.Y + matrix.Row1.X) * invS;
                    result.Xyz.Z = (matrix.Row0.Z + matrix.Row2.X) * invS;
                }
                else if (m11 > m22)
                {
                    var s = Math.Sqrt(1 + m11 - m00 - m22) * 2;
                    var invS = 1.0 / s;

                    result.W = (matrix.Row0.Z - matrix.Row2.X) * invS;
                    result.Xyz.X = (matrix.Row0.Y + matrix.Row1.X) * invS;
                    result.Xyz.Y = s * 0.25;
                    result.Xyz.Z = (matrix.Row1.Z + matrix.Row2.Y) * invS;
                }
                else
                {
                    var s = Math.Sqrt(1 + m22 - m00 - m11) * 2;
                    var invS = 1.0 / s;

                    result.W = (matrix.Row1.X - matrix.Row0.Y) * invS;
                    result.Xyz.X = (matrix.Row0.Z + matrix.Row2.X) * invS;
                    result.Xyz.Y = (matrix.Row1.Z + matrix.Row2.Y) * invS;
                    result.Xyz.Z = s * 0.25;
                }
            }
        }

        /// <summary>
        /// Do Spherical linear interpolation between two quaternions
        /// </summary>
        /// <param name="q1">The first Quaternion</param>
        /// <param name="q2">The second Quaternion</param>
        /// <param name="blend">The blend factor</param>
        /// <returns>A smooth blend between the given quaternions</returns>
        public static Quaternion Slerp(Quaternion q1, Quaternion q2, double blend)
        {
            // if either input is zero, return the other.
            if (q1.LengthSquared == 0.0f)
            {
                if (q2.LengthSquared == 0.0f)
                {
                    return Identity;
                }
                return q2;
            }
            else if (q2.LengthSquared == 0.0f)
            {
                return q1;
            }


            var cosHalfAngle = q1.W * q2.W + Vector3.Dot(q1.Xyz, q2.Xyz);

            if (cosHalfAngle >= 1.0f || cosHalfAngle <= -1.0f)
            {
                // angle = 0.0f, so just return one input.
                return q1;
            }
            else if (cosHalfAngle < 0.0f)
            {
                q2.Xyz = -q2.Xyz;
                q2.W = -q2.W;
                cosHalfAngle = -cosHalfAngle;
            }

            double blendA;
            double blendB;
            if (cosHalfAngle < 0.99f)
            {
                // do proper slerp for big angles
                var halfAngle = Math.Acos(cosHalfAngle);
                var sinHalfAngle = Math.Sin(halfAngle);
                var oneOverSinHalfAngle = 1.0f / sinHalfAngle;
                blendA = Math.Sin(halfAngle * (1.0f - blend)) * oneOverSinHalfAngle;
                blendB = Math.Sin(halfAngle * blend) * oneOverSinHalfAngle;
            }
            else
            {
                // do lerp if angle is really small.
                blendA = 1.0f - blend;
                blendB = blend;
            }

            var result = new Quaternion(blendA * q1.Xyz + blendB * q2.Xyz, blendA * q1.W + blendB * q2.W);
            if (result.LengthSquared > 0.0f)
            {
                return Normalize(result);
            }
            else
            {
                return Identity;
            }
        }

        /// <summary>
        /// Adds two instances.
        /// </summary>
        /// <param name="left">The first instance.</param>
        /// <param name="right">The second instance.</param>
        /// <returns>The result of the calculation.</returns>
        public static Quaternion operator +(Quaternion left, Quaternion right)
        {
            left.Xyz += right.Xyz;
            left.W += right.W;
            return left;
        }

        /// <summary>
        /// Subtracts two instances.
        /// </summary>
        /// <param name="left">The first instance.</param>
        /// <param name="right">The second instance.</param>
        /// <returns>The result of the calculation.</returns>
        public static Quaternion operator -(Quaternion left, Quaternion right)
        {
            left.Xyz -= right.Xyz;
            left.W -= right.W;
            return left;
        }

        /// <summary>
        /// Multiplies two instances.
        /// </summary>
        /// <param name="left">The first instance.</param>
        /// <param name="right">The second instance.</param>
        /// <returns>The result of the calculation.</returns>
        public static Quaternion operator *(Quaternion left, Quaternion right)
        {
            Multiply(ref left, ref right, out left);
            return left;
        }

        /// <summary>
        /// Multiplies an instance by a scalar.
        /// </summary>
        /// <param name="quaternion">The instance.</param>
        /// <param name="scale">The scalar.</param>
        /// <returns>A new instance containing the result of the calculation.</returns>
        public static Quaternion operator *(Quaternion quaternion, double scale)
        {
            Multiply(ref quaternion, scale, out quaternion);
            return quaternion;
        }

        /// <summary>
        /// Multiplies an instance by a scalar.
        /// </summary>
        /// <param name="quaternion">The instance.</param>
        /// <param name="scale">The scalar.</param>
        /// <returns>A new instance containing the result of the calculation.</returns>
        public static Quaternion operator *(double scale, Quaternion quaternion)
        {
            return new Quaternion(quaternion.X * scale, quaternion.Y * scale, quaternion.Z * scale, quaternion.W * scale);
        }

        /// <summary>
        /// Compares two instances for equality.
        /// </summary>
        /// <param name="left">The first instance.</param>
        /// <param name="right">The second instance.</param>
        /// <returns>True, if left equals right; false otherwise.</returns>
        public static bool operator ==(Quaternion left, Quaternion right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares two instances for inequality.
        /// </summary>
        /// <param name="left">The first instance.</param>
        /// <param name="right">The second instance.</param>
        /// <returns>True, if left does not equal right; false otherwise.</returns>
        public static bool operator !=(Quaternion left, Quaternion right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Returns a System.String that represents the current Quaternion.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("V: {0}, W: {1}", Xyz, W);
        }

        /// <summary>
        /// Compares this object instance to another object for equality.
        /// </summary>
        /// <param name="other">The other object to be used in the comparison.</param>
        /// <returns>True if both objects are Quaternions of equal value. Otherwise it returns false.</returns>
        public override bool Equals(object other)
        {
            if (other is Quaternion == false)
            {
                return false;
            }
            return this == (Quaternion)other;
        }

        /// <summary>
        /// Provides the hash code for this object.
        /// </summary>
        /// <returns>A hash code formed from the bitwise XOR of this objects members.</returns>

        public override int GetHashCode()
        {
            unchecked
            {
                return (Xyz.GetHashCode() * 397) ^ W.GetHashCode();
            }
        }

#if false

        /// <summary>The W component of the Quaternion.</summary>
        public double W;

        /// <summary>The X component of the Quaternion.</summary>
        public double X;

        /// <summary>The Y component of the Quaternion.</summary>
        public double Y;

        /// <summary>The Z component of the Quaternion.</summary>
        public double Z;

        /// <summary>Constructs left Quaternion that is left copy of the given Quaternion.</summary>
        /// <param name="quaterniond">The Quaternion to copy.</param>
        public Quaternion(ref Quaternion Quaternion) : this(Quaternion.W, Quaternion.X, Quaternion.Y, Quaternion.Z) { }

        /// <summary>Constructs left Quaternion from the given components.</summary>
        /// <param name="w">The W component for the Quaternion.</param>
        /// <param name="vector3d">A Vector representing the X, Y, and Z componets for the quaterion.</param>
        public Quaternion(double w, ref Vector3d vector3d) : this(w, vector3d.X, vector3d.Y, vector3d.Z) { }

        /// <summary>Constructs left Quaternion from the given axis and angle.</summary>
        /// <param name="axis">The axis for the Quaternion.</param>
        /// <param name="angle">The angle for the quaternione.</param>
        public Quaternion(ref Vector3d axis, double angle)
        {
            double halfAngle = Functions.DTOR * angle / 2;

            this.W = System.Math.Cos(halfAngle);

            double sin = System.Math.Sin(halfAngle);
            Vector3d axisNormalized;
            Vector3d.Normalize(ref axis, out axisNormalized);
            this.X = axisNormalized.X * sin;
            this.Y = axisNormalized.Y * sin;
            this.Z = axisNormalized.Z * sin;
        }

        /// <summary>Constructs left Quaternion from the given components.</summary>
        /// <param name="w">The W component for the Quaternion.</param>
        /// <param name="x">The X component for the Quaternion.</param>
        /// <param name="y">The Y component for the Quaternion.</param>
        /// <param name="z">The Z component for the Quaternion.</param>
        public Quaternion(double w, double x, double y, double z)
        {
            this.W = w;
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        /// <summary>Constructs left Quaternion from the given array of double-precision floating-point numbers.</summary>
        /// <param name="doubleArray">The array of doubles for the components of the Quaternion.</param>
        public Quaternion(double[] doubleArray)
        {
            if (doubleArray == null || doubleArray.GetLength(0) < 4) throw new MissingFieldException();

            this.W = doubleArray[0];
            this.X = doubleArray[1];
            this.Y = doubleArray[2];
            this.Z = doubleArray[3];
        }

        /// <summary>Constructs left Quaternion from the given matrix.  Only contains rotation information.</summary>
        /// <param name="matrix">The matrix for the components of the Quaternion.</param>
        public Quaternion(ref Matrix4d matrix)
        {
            double scale = System.Math.Pow(matrix.Determinant, 1.0d/3.0d);

            W = System.Math.Sqrt(System.Math.Max(0, scale + matrix[0, 0] + matrix[1, 1] + matrix[2, 2])) / 2;
            X = System.Math.Sqrt(System.Math.Max(0, scale + matrix[0, 0] - matrix[1, 1] - matrix[2, 2])) / 2;
            Y = System.Math.Sqrt(System.Math.Max(0, scale - matrix[0, 0] + matrix[1, 1] - matrix[2, 2])) / 2;
            Z = System.Math.Sqrt(System.Math.Max(0, scale - matrix[0, 0] - matrix[1, 1] + matrix[2, 2])) / 2;
            if( matrix[2,1] - matrix[1,2] < 0 ) X = -X;
            if( matrix[0,2] - matrix[2,0] < 0 ) Y = -Y;
            if( matrix[1,0] - matrix[0,1] < 0 ) Z = -Z;
        }

        public Quaternion(ref Matrix3d matrix)
        {
            double scale = System.Math.Pow(matrix.Determinant, 1.0d / 3.0d);

            W = System.Math.Sqrt(System.Math.Max(0, scale + matrix[0, 0] + matrix[1, 1] + matrix[2, 2])) / 2;
            X = System.Math.Sqrt(System.Math.Max(0, scale + matrix[0, 0] - matrix[1, 1] - matrix[2, 2])) / 2;
            Y = System.Math.Sqrt(System.Math.Max(0, scale - matrix[0, 0] + matrix[1, 1] - matrix[2, 2])) / 2;
            Z = System.Math.Sqrt(System.Math.Max(0, scale - matrix[0, 0] - matrix[1, 1] + matrix[2, 2])) / 2;
            if (matrix[2, 1] - matrix[1, 2] < 0) X = -X;
            if (matrix[0, 2] - matrix[2, 0] < 0) Y = -Y;
            if (matrix[1, 0] - matrix[0, 1] < 0) Z = -Z;
        }

        public void Add(ref Quaternion Quaternion)
        {
            W = W + Quaternion.W;
            X = X + Quaternion.X;
            Y = Y + Quaternion.Y;
            Z = Z + Quaternion.Z;
        }
        public void Add(ref Quaternion Quaternion, out Quaternion result)
        {
            result.W = W + Quaternion.W;
            result.X = X + Quaternion.X;
            result.Y = Y + Quaternion.Y;
            result.Z = Z + Quaternion.Z;
        }
        public static void Add(ref Quaternion left, ref Quaternion right, out Quaternion result)
        {
            result.W = left.W + right.W;
            result.X = left.X + right.X;
            result.Y = left.Y + right.Y;
            result.Z = left.Z + right.Z;
        }

        public void Subtract(ref Quaternion Quaternion)
        {
            W = W - Quaternion.W;
            X = X - Quaternion.X;
            Y = Y - Quaternion.Y;
            Z = Z - Quaternion.Z;
        }
        public void Subtract(ref Quaternion Quaternion, out Quaternion result)
        {
            result.W = W - Quaternion.W;
            result.X = X - Quaternion.X;
            result.Y = Y - Quaternion.Y;
            result.Z = Z - Quaternion.Z;
        }
        public static void Subtract(ref Quaternion left, ref Quaternion right, out Quaternion result)
        {
            result.W = left.W - right.W;
            result.X = left.X - right.X;
            result.Y = left.Y - right.Y;
            result.Z = left.Z - right.Z;
        }

        public void Multiply(ref Quaternion Quaternion)
        {
            double w = W * Quaternion.W - X * Quaternion.X - Y * Quaternion.Y - Z * Quaternion.Z;
            double x = W * Quaternion.X + X * Quaternion.W + Y * Quaternion.Z - Z * Quaternion.Y;
            double y = W * Quaternion.Y + Y * Quaternion.W + Z * Quaternion.X - X * Quaternion.Z;
            Z = W * Quaternion.Z + Z * Quaternion.W + X * Quaternion.Y - Y * Quaternion.X;
            W = w;
            X = x;
            Y = y;
        }
        public void Multiply(ref Quaternion Quaternion, out Quaternion result)
        {
            result.W = W * Quaternion.W - X * Quaternion.X - Y * Quaternion.Y - Z * Quaternion.Z;
            result.X = W * Quaternion.X + X * Quaternion.W + Y * Quaternion.Z - Z * Quaternion.Y;
            result.Y = W * Quaternion.Y + Y * Quaternion.W + Z * Quaternion.X - X * Quaternion.Z;
            result.Z = W * Quaternion.Z + Z * Quaternion.W + X * Quaternion.Y - Y * Quaternion.X;
        }
        public static void Multiply(ref Quaternion left, ref Quaternion right, out Quaternion result)
        {
            result.W = left.W * right.W - left.X * right.X - left.Y * right.Y - left.Z * right.Z;
            result.X = left.W * right.X + left.X * right.W + left.Y * right.Z - left.Z * right.Y;
            result.Y = left.W * right.Y + left.Y * right.W + left.Z * right.X - left.X * right.Z;
            result.Z = left.W * right.Z + left.Z * right.W + left.X * right.Y - left.Y * right.X;
        }

        public void Multiply(double scalar)
        {
            W = W * scalar;
            X = X * scalar;
            Y = Y * scalar;
            Z = Z * scalar;
        }
        public void Multiply(double scalar, out Quaternion result)
        {
            result.W = W * scalar;
            result.X = X * scalar;
            result.Y = Y * scalar;
            result.Z = Z * scalar;
        }
        public static void Multiply(ref Quaternion Quaternion, double scalar, out Quaternion result)
        {
            result.W = Quaternion.W * scalar;
            result.X = Quaternion.X * scalar;
            result.Y = Quaternion.Y * scalar;
            result.Z = Quaternion.Z * scalar;
        }

        public void Divide(double scalar)
        {
            if (scalar == 0) throw new DivideByZeroException();
            W = W / scalar;
            X = X / scalar;
            Y = Y / scalar;
            Z = Z / scalar;
        }
        public void Divide(double scalar, out Quaternion result)
        {
            if (scalar == 0) throw new DivideByZeroException();
            result.W = W / scalar;
            result.X = X / scalar;
            result.Y = Y / scalar;
            result.Z = Z / scalar;
        }
        public static void Divide(ref Quaternion Quaternion, double scalar, out Quaternion result)
        {
            if (scalar == 0) throw new DivideByZeroException();
            result.W = Quaternion.W / scalar;
            result.X = Quaternion.X / scalar;
            result.Y = Quaternion.Y / scalar;
            result.Z = Quaternion.Z / scalar;
        }

        public double Modulus
        {
            get
            {
                return System.Math.Sqrt(W * W + X * X + Y * Y + Z * Z);
            }
        }
        public double ModulusSquared
        {
            get
            {
                return W * W + X * X + Y * Y + Z * Z;
            }
        }

        public static double DotProduct(Quaternion left, Quaternion right)
        {
            return left.W * right.W + left.X * right.X + left.Y * right.Y + left.Z * right.Z;
        }

        public void Normalize()
        {
            double modulus = System.Math.Sqrt(W * W + X * X + Y * Y + Z * Z);
            if (modulus == 0) throw new DivideByZeroException();
            W = W / modulus;
            X = X / modulus;
            Y = Y / modulus;
            Z = Z / modulus;
        }
        public void Normalize( out Quaternion result )
        {
            double modulus = System.Math.Sqrt(W * W + X * X + Y * Y + Z * Z);
            if (modulus == 0) throw new DivideByZeroException();
            result.W = W / modulus;
            result.X = X / modulus;
            result.Y = Y / modulus;
            result.Z = Z / modulus;
        }
        public static void Normalize(ref Quaternion Quaternion, out Quaternion result)
        {
            double modulus = System.Math.Sqrt(Quaternion.W * Quaternion.W + Quaternion.X * Quaternion.X + Quaternion.Y * Quaternion.Y + Quaternion.Z * Quaternion.Z);
            if (modulus == 0) throw new DivideByZeroException();
            result.W = Quaternion.W / modulus;
            result.X = Quaternion.X / modulus;
            result.Y = Quaternion.Y / modulus;
            result.Z = Quaternion.Z / modulus;
        }

        public void Conjugate()
        {
            X = -X;
            Y = -Y;
            Z = -Z;
        }
        public void Conjugate( out Quaternion result )
        {
            result.W = W;
            result.X = -X;
            result.Y = -Y;
            result.Z = -Z;
        }
        public static void Conjugate(ref Quaternion Quaternion, out Quaternion result)
        {
            result.W = Quaternion.W;
            result.X = -Quaternion.X;
            result.Y = -Quaternion.Y;
            result.Z = -Quaternion.Z;
        }

        public void Inverse()
        {
            double modulusSquared = W * W + X * X + Y * Y + Z * Z;
            if (modulusSquared <= 0) throw new InvalidOperationException();
            double inverseModulusSquared = 1.0 / modulusSquared;
            W = W * inverseModulusSquared;
            X = X * -inverseModulusSquared;
            Y = Y * -inverseModulusSquared;
            Z = Z * -inverseModulusSquared;
        }
        public void Inverse( out Quaternion result )
        {
            double modulusSquared = W * W + X * X + Y * Y + Z * Z;
            if (modulusSquared <= 0) throw new InvalidOperationException();
            double inverseModulusSquared = 1.0 / modulusSquared;
            result.W = W * inverseModulusSquared;
            result.X = X * -inverseModulusSquared;
            result.Y = Y * -inverseModulusSquared;
            result.Z = Z * -inverseModulusSquared;
        }
        public static void Inverse(ref Quaternion Quaternion, out Quaternion result)
        {
            double modulusSquared = Quaternion.W * Quaternion.W + Quaternion.X * Quaternion.X + Quaternion.Y * Quaternion.Y + Quaternion.Z * Quaternion.Z;
            if (modulusSquared <= 0) throw new InvalidOperationException();
            double inverseModulusSquared = 1.0 / modulusSquared;
            result.W = Quaternion.W * inverseModulusSquared;
            result.X = Quaternion.X * -inverseModulusSquared;
            result.Y = Quaternion.Y * -inverseModulusSquared;
            result.Z = Quaternion.Z * -inverseModulusSquared;
        }

        public void Log()
        {
            if (System.Math.Abs(W) < 1.0)
            {
                double angle = System.Math.Acos(W);
                double sin = System.Math.Sin(angle);

                if (System.Math.Abs(sin) >= 0)
                {
                    double coefficient = angle / sin;
                    X = X * coefficient;
                    Y = Y * coefficient;
                    Z = Z * coefficient;
                }
            }
            else
            {
                X = 0;
                Y = 0;
                Z = 0;
            }

            W = 0;
        }
        public void Log( out Quaternion result )
        {
            if (System.Math.Abs(W) < 1.0)
            {
                double angle = System.Math.Acos(W);
                double sin = System.Math.Sin(angle);

                if (System.Math.Abs(sin) >= 0)
                {
                    double coefficient = angle / sin;
                    result.X = X * coefficient;
                    result.Y = Y * coefficient;
                    result.Z = Z * coefficient;
                }
                else
                {
                    result.X = X;
                    result.Y = Y;
                    result.Z = Z;
                }
            }
            else
            {
                result.X = 0;
                result.Y = 0;
                result.Z = 0;
            }

            result.W = 0;
        }
        public static void Log(ref Quaternion Quaternion, out Quaternion result)
        {
            if (System.Math.Abs(Quaternion.W) < 1.0)
            {
                double angle = System.Math.Acos(Quaternion.W);
                double sin = System.Math.Sin(angle);

                if (System.Math.Abs(sin) >= 0)
                {
                    double coefficient = angle / sin;
                    result.X = Quaternion.X * coefficient;
                    result.Y = Quaternion.Y * coefficient;
                    result.Z = Quaternion.Z * coefficient;
                }
                else
                {
                    result.X = Quaternion.X;
                    result.Y = Quaternion.Y;
                    result.Z = Quaternion.Z;
                }
            }
            else
            {
                result.X = 0;
                result.Y = 0;
                result.Z = 0;
            }

            result.W = 0;
        }

        public void Exp()
        {
            double angle = System.Math.Sqrt(X * X + Y * Y + Z * Z);
            double sin = System.Math.Sin(angle);

            if (System.Math.Abs(sin) > 0)
            {
                double coefficient = angle / sin;
                W = 0;
                X = X * coefficient;
                Y = Y * coefficient;
                Z = Z * coefficient;
            }
            else
            {
                W = 0;
            }
        }
        public void Exp(out Quaternion result)
        {
            double angle = System.Math.Sqrt(X * X + Y * Y + Z * Z);
            double sin = System.Math.Sin(angle);

            if (System.Math.Abs(sin) > 0)
            {
                double coefficient = angle / sin;
                result.W = 0;
                result.X = X * coefficient;
                result.Y = Y * coefficient;
                result.Z = Z * coefficient;
            }
            else
            {
                result.W = 0;
                result.X = X;
                result.Y = Y;
                result.Z = Z;
            }
        }
        public static void Exp(ref Quaternion Quaternion, out Quaternion result)
        {
            double angle = System.Math.Sqrt(Quaternion.X * Quaternion.X + Quaternion.Y * Quaternion.Y + Quaternion.Z * Quaternion.Z);
            double sin = System.Math.Sin(angle);

            if (System.Math.Abs(sin) > 0)
            {
                double coefficient = angle / sin;
                result.W = 0;
                result.X = Quaternion.X * coefficient;
                result.Y = Quaternion.Y * coefficient;
                result.Z = Quaternion.Z * coefficient;
            }
            else
            {
                result.W = 0;
                result.X = Quaternion.X;
                result.Y = Quaternion.Y;
                result.Z = Quaternion.Z;
            }
        }

        /// <summary>Returns left matrix for this Quaternion.</summary>
        public void Matrix4d(out Matrix4d result)
        {
            // TODO Expand
            result = new Matrix4d(ref this);
        }

        public void GetAxisAndAngle(out Vector3d axis, out double angle)
        {
            Quaternion Quaternion;
            Normalize(out Quaternion);
            double cos = Quaternion.W;
            angle = System.Math.Acos(cos) * 2 * Functions.RTOD;
            double sin = System.Math.Sqrt( 1.0d - cos * cos );
            if ( System.Math.Abs( sin ) < 0.0001 ) sin = 1;
            axis = new Vector3d(X / sin, Y / sin, Z / sin);
        }

        public static void Slerp(ref Quaternion start, ref Quaternion end, double blend, out Quaternion result)
        {
            if (start.W == 0 && start.X == 0 && start.Y == 0 && start.Z == 0)
            {
                if (end.W == 0 && end.X == 0 && end.Y == 0 && end.Z == 0)
                {
                    result.W = 1;
                    result.X = 0;
                    result.Y = 0;
                    result.Z = 0;
                }
                else
                {
                    result = end;
                }
            }
            else if (end.W == 0 && end.X == 0 && end.Y == 0 && end.Z == 0)
            {
                result = start;
            }

            Vector3d startVector = new Vector3d(start.X, start.Y, start.Z);
            Vector3d endVector = new Vector3d(end.X, end.Y, end.Z);
            double cosHalfAngle = start.W * end.W + Vector3d.Dot(startVector, endVector);

            if (cosHalfAngle >= 1.0f || cosHalfAngle <= -1.0f)
            {
                // angle = 0.0f, so just return one input.
                result = start;
            }
            else if (cosHalfAngle < 0.0f)
            {
                end.W = -end.W;
                end.X = -end.X;
                end.Y = -end.Y;
                end.Z = -end.Z;
                cosHalfAngle = -cosHalfAngle;
            }

            double blendA;
            double blendB;
            if (cosHalfAngle < 0.99f)
            {
                // do proper slerp for big angles
                double halfAngle = (double)System.Math.Acos(cosHalfAngle);
                double sinHalfAngle = (double)System.Math.Sin(halfAngle);
                double oneOverSinHalfAngle = 1.0f / sinHalfAngle;
                blendA = (double)System.Math.Sin(halfAngle * (1.0f - blend)) * oneOverSinHalfAngle;
                blendB = (double)System.Math.Sin(halfAngle * blend) * oneOverSinHalfAngle;
            }
            else
            {
                // do lerp if angle is really small.
                blendA = 1.0f - blend;
                blendB = blend;
            }

            result.W = blendA * start.W + blendB * end.W;
            result.X = blendA * start.X + blendB * end.X;
            result.Y = blendA * start.Y + blendB * end.Y;
            result.Z = blendA * start.Z + blendB * end.Z;

            if (result.W != 0 || result.X != 0 || result.Y != 0 || result.Z != 0)
            {
                result.Normalize();
            }
            else
            {
                result.W = 1;
                result.X = 0;
                result.Y = 0;
                result.Z = 0;
            }
        }

        /// <summary>Returns the hash code for this instance.</summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            base.GetHashCode();
            return W.GetHashCode() ^ X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
        }

        /// <summary>Returns the fully qualified type name of this instance.</summary>
        /// <returns>A System.String containing left fully qualified type name.</returns>
        public override string ToString()
        {
            return string.Format("({0}, {1}, {2}, {3})", W, X, Y, Z);
        }

        /// <summary>Parses left string, converting it to left Quaternion.</summary>
        /// <param name="str">The string to parse.</param>
        /// <returns>The Quaternion represented by the string.</returns>
        public static void Parse(string str, out Quaternion result)
        {
            Match match = new Regex(@"\((?<w>.*),(?<x>.*),(?<y>.*),(?<z>.*)\)", RegexOptions.None).Match(str);
            if (!match.Success) throw new Exception("Parse failed!");

            result.W = double.Parse(match.Result("${w}"));
            result.X = double.Parse(match.Result("${x}"));
            result.Y = double.Parse(match.Result("${y}"));
            result.Z = double.Parse(match.Result("${z}"));
        }

        /// <summary>A quaterion with all zero components.</summary>
        public static readonly Quaternion Zero = new Quaternion(0, 0, 0, 0);

        /// <summary>A quaterion representing an identity.</summary>
        public static readonly Quaternion Identity = new Quaternion(1, 0, 0, 0);

        /// <summary>A quaterion representing the W axis.</summary>
        public static readonly Quaternion WAxis = new Quaternion(1, 0, 0, 0);

        /// <summary>A quaterion representing the X axis.</summary>
        public static readonly Quaternion XAxis = new Quaternion(0, 1, 0, 0);

        /// <summary>A quaterion representing the Y axis.</summary>
        public static readonly Quaternion YAxis = new Quaternion(0, 0, 1, 0);

        /// <summary>A quaterion representing the Z axis.</summary>
        public static readonly Quaternion ZAxis = new Quaternion(0, 0, 0, 1);

#endif

        /// <summary>
        /// Compares this Quaternion instance to another Quaternion for equality.
        /// </summary>
        /// <param name="other">The other Quaternion to be used in the comparison.</param>
        /// <returns>True if both instances are equal; false otherwise.</returns>
        public bool Equals(Quaternion other)
        {
            return Xyz == other.Xyz && W == other.W;
        }
    }
}