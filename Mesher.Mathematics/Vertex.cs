using System;

namespace Mesher.Mathematics
{
    /// <summary>
    /// Represents a three dimensional vector.
    /// </summary>
    public struct Vertex
	{
		public double X;
		public double Y;
		public double Z;

		public double this[int index]
		{
			get
			{
			    if(index == 0) return X;
			    if(index == 1) return Y;
			    if(index == 2) return Z;
			    throw new Exception("Out of range.");
			}
			set 
			{
				if(index == 0) X = value;
                else if (index == 1) Y = value;
                else if (index == 2) Z = value;
                else throw new Exception("Out of range.");
			}
		}

		public Vertex(double s)
		{
			X = Y = Z = s;
		}

		public Vertex(double x, double y, double z)
		{
			X = x;
			Y = y;
            Z = z;
		}

        public Vertex(double x, double y)
        {
            X = x;
            Y = y;
            Z = 0;
        }

        public Vertex(Vertex v)
        {
            X = v.X;
            Y = v.Y;
            Z = v.Z;
        }
		
		public static Vertex operator + (Vertex lhs, Vertex rhs)
		{
			return new Vertex(lhs.X + rhs.X, lhs.Y + rhs.Y, lhs.Z + rhs.Z);
		}

        public static Vertex operator +(Vertex lhs, double rhs)
        {
            return new Vertex(lhs.X + rhs, lhs.Y + rhs, lhs.Z + rhs);
        }

        public static Vertex operator -(Vertex lhs, Vertex rhs)
        {
            return new Vertex(lhs.X - rhs.X, lhs.Y - rhs.Y, lhs.Z - rhs.Z);
        }

        public static Vertex operator - (Vertex lhs, double rhs)
        {
            return new Vertex(lhs.X - rhs, lhs.Y - rhs, lhs.Z - rhs);
        }

        public static Vertex operator *(Vertex self, double s)
		{
			return new Vertex(self.X * s, self.Y * s, self.Z * s);
		}
        public static Vertex operator *(double lhs, Vertex rhs)
        {
            return new Vertex(rhs.X * lhs, rhs.Y * lhs, rhs.Z * lhs);
        }

        public static Vertex operator /(Vertex lhs, double rhs)
        {
            return new Vertex(lhs.X / rhs, lhs.Y / rhs, lhs.Z / rhs);
        }

        public static Vertex operator * (Vertex lhs, Vertex rhs)
        {
            return new Vertex(rhs.X * lhs.X, rhs.Y * lhs.Y, rhs.Z * lhs.Z);
        }

        public Vertex Cross(Vertex v)
        {
            return Cross(this, v);
        }

        public static Vertex Cross(Vertex lhs, Vertex rhs)
        {
            return new Vertex(
                lhs.Y * rhs.Z - rhs.Y * lhs.Z,
                lhs.Z * rhs.X - rhs.Z * lhs.X,
                lhs.X * rhs.Y - rhs.X * lhs.Y);
        }

        public double Angle(Vertex v)
        {
            return Angle(this, v);
        }

        public static double Angle(Vertex u, Vertex v)
        {
            return Math.Acos(u.Normalize().Dot(v.Normalize()));
        }

        public double Dot(Vertex v)
        {
            return Dot(this, v);
        }

        public static double Dot(Vertex x, Vertex y)
        {
            Vertex tmp = new Vertex(x * y);
            return tmp.X + tmp.Y + tmp.Z;
        }

        public Vertex Normalize()
        {
            return Normalize(this);
        }

        public static Vertex Normalize(Vertex v)
        {
            return v / Math.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z);
        }

        public static double Length(Vertex v)
        {
            return Math.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z);
        }

        public double Length()
        {
            return Length(this);
        }

        public bool Valid()
        {
            return Valid(this);
        }

        public static bool Valid(Vertex v)
        {
            return !(double.IsNaN(v.X) || double.IsNaN(v.Y) || double.IsNaN(v.Z) || double.IsInfinity(v.X) || double.IsInfinity(v.Y) || double.IsInfinity(v.Z));
        }

        public double[] ToArray()
        {
            return new[] { X, Y, Z };
        }
	}
}