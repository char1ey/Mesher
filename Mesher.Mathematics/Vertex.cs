using System;

namespace Mesher.Mathematics
{
    /// <summary>
    /// Represents a three dimensional vector.
    /// </summary>
	public struct Vertex
	{
		public double x;
		public double y;
		public double z;

		public double this[int index]
		{
			get 
			{
				if(index == 0) return x;
				else if(index == 1) return y;
				else if(index == 2) return z;
                else throw new Exception("Out of range.");
			}
			set 
			{
				if(index == 0) x = value;
                else if (index == 1) y = value;
                else if (index == 2) z = value;
                else throw new Exception("Out of range.");
			}
		}

		public Vertex(double s)
		{
			x = y = z = s;
		}

		public Vertex(double x, double y, double z)
		{
			this.x = x;
			this.y = y;
            this.z = z;
		}

        public Vertex(double x, double y)
        {
            this.x = x;
            this.y = y;
            z = 0;
        }

        public Vertex(Vertex v)
        {
            x = v.x;
            y = v.y;
            z = v.z;
        }
		
		public static Vertex operator + (Vertex lhs, Vertex rhs)
		{
			return new Vertex(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);
		}

        public static Vertex operator +(Vertex lhs, double rhs)
        {
            return new Vertex(lhs.x + rhs, lhs.y + rhs, lhs.z + rhs);
        }

        public static Vertex operator -(Vertex lhs, Vertex rhs)
        {
            return new Vertex(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z);
        }

        public static Vertex operator - (Vertex lhs, double rhs)
        {
            return new Vertex(lhs.x - rhs, lhs.y - rhs, lhs.z - rhs);
        }

        public static Vertex operator *(Vertex self, double s)
		{
			return new Vertex(self.x * s, self.y * s, self.z * s);
		}
        public static Vertex operator *(double lhs, Vertex rhs)
        {
            return new Vertex(rhs.x * lhs, rhs.y * lhs, rhs.z * lhs);
        }

        public static Vertex operator /(Vertex lhs, double rhs)
        {
            return new Vertex(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs);
        }

        public static Vertex operator * (Vertex lhs, Vertex rhs)
        {
            return new Vertex(rhs.x * lhs.x, rhs.y * lhs.y, rhs.z * lhs.z);
        }

        public Vertex Cross(Vertex v)
        {
            return Cross(this, v);
        }

        public static Vertex Cross(Vertex lhs, Vertex rhs)
        {
            return new Vertex(
                lhs.y * rhs.z - rhs.y * lhs.z,
                lhs.z * rhs.x - rhs.z * lhs.x,
                lhs.x * rhs.y - rhs.x * lhs.y);
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
            return tmp.x + tmp.y + tmp.z;
        }

        public Vertex Normalize()
        {
            return Normalize(this);
        }

        public static Vertex Normalize(Vertex v)
        {
            return v / Math.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z);
        }

        public bool Valid()
        {
            return Valid(this);
        }

        public static bool Valid(Vertex v)
        {
            return !(double.IsNaN(v.x) || double.IsNaN(v.y) || double.IsNaN(v.z) || double.IsInfinity(v.x) || double.IsInfinity(v.y) || double.IsInfinity(v.z));
        }

        public double[] ToArray()
        {
            return new[] { x, y, z };
        }
	}
}