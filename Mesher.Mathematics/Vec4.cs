using System;

namespace Mesher.Mathematics
{
    /// <summary>
    /// Represents a four dimensional vector.
    /// </summary>
    public class Vec4
    {
        public double X;
        public double Y;
        public double Z;
        public double W;

        public double this[int index]
        {
            get
            {
                if (index == 0) return X;
                else if (index == 1) return Y;
                else if (index == 2) return Z;
                else if (index == 3) return W;
                else throw new Exception("Out of range.");
            }
            set
            {
                if (index == 0) X = value;
                else if (index == 1) Y = value;
                else if (index == 2) Z = value;
                else if (index == 3) W = value;
                else throw new Exception("Out of range.");
            }
        }

        public Vec4(double s)
        {
            X = Y = Z = W = s;
        }

        public Vec4(double x, double y, double z, double w)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
        }

        public Vec4(Vec4 v)
        {
            this.X = v.X;
            this.Y = v.Y;
            this.Z = v.Z;
            this.W = v.W;
        }

        public Vec4(Vec3 xyz, double w)
        {
            this.X = xyz.X;
            this.Y = xyz.Y;
            this.Z = xyz.Z;
            this.W = w;
        }

        public static Vec4 operator + (Vec4 lhs, Vec4 rhs)
        {
            return new Vec4(lhs.X + rhs.X, lhs.Y + rhs.Y, lhs.Z + rhs.Z, lhs.W + rhs.W);
        }

        public static Vec4 operator +(Vec4 lhs, double rhs)
        {
            return new Vec4(lhs.X + rhs, lhs.Y + rhs, lhs.Z + rhs, lhs.W + rhs);
        }

        public static Vec4 operator -(Vec4 lhs, double rhs)
        {
            return new Vec4(lhs.X - rhs, lhs.Y - rhs, lhs.Z - rhs, lhs.W - rhs);
        }

        public static Vec4 operator - (Vec4 lhs, Vec4 rhs)
        {
            return new Vec4(lhs.X - rhs.X, lhs.Y - rhs.Y, lhs.Z - rhs.Z, lhs.W - rhs.W);
        }

        public static Vec4 operator * (Vec4 self, double s)
        {
            return new Vec4(self.X * s, self.Y * s, self.Z * s, self.W * s);
        }

        public static Vec4 operator * (double lhs, Vec4 rhs)
        {
            return new Vec4(rhs.X * lhs, rhs.Y * lhs, rhs.Z * lhs, rhs.W * lhs);
        }

        public static Vec4 operator * (Vec4 lhs, Vec4 rhs)
        {
            return new Vec4(rhs.X * lhs.X, rhs.Y * lhs.Y, rhs.Z * lhs.Z, rhs.W * lhs.W);
        }

        public static Vec4 operator / (Vec4 lhs, double rhs)
        {
            return new Vec4(lhs.X/rhs, lhs.Y/rhs, lhs.Z/rhs, lhs.W/rhs);
        }

        public static Vec4 Normalize(Vec4 v)
        {
            var sqr = v.X * v.X + v.Y * v.Y + v.Z * v.Z + v.W * v.W;
            return v * (1.0f / (double)Math.Sqrt(sqr));
        }

        public static double Dot(Vec4 x, Vec4 y)
        {
            var tmp = new Vec4(x * y);
            return (tmp.X + tmp.Y) + (tmp.Z + tmp.W);
        }


        public double[] ToArray()
        {
            return new[] { X, Y, Z, W };
        }
    }
}