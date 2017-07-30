using System;

namespace Mesher.Mathematics
{
    /// <summary>
    /// Represents a three dimensional vector.
    /// </summary>
	public struct Vec3
	{
        public static Vec3 Zero = new Vec3(0, 0, 0);

		public double X;
		public double Y;
		public double Z;

		public double this[int index]
		{
			get 
			{
				if(index == 0) return X;
				else if(index == 1) return Y;
				else if(index == 2) return Z;
                else throw new Exception("Out of range.");
			}
			set 
			{
				if(index == 0) X = value;
                else if (index == 1) Y = value;
                else if (index == 2) Z = value;
                else throw new Exception("Out of range.");
			}
		}

		public Vec3(double s)
		{
			X = Y = Z = s;
		}

		public Vec3(double x, double y, double z)
		{
			this.X = x;
			this.Y = y;
            this.Z = z;
		}

        public Vec3(Vec3 v)
        {
            this.X = v.X;
            this.Y = v.Y;
            this.Z = v.Z;
        }

        public Vec3(Vec4 v)
        {
            this.X = v.X;
            this.Y = v.Y;
            this.Z = v.Z; 
        }

        public Vec3(Vec2 xy, double z)
        {
            this.X = xy.X;
            this.Y = xy.Y;
            this.Z = z;
        }
		
		public static Vec3 operator + (Vec3 lhs, Vec3 rhs)
		{
			return new Vec3(lhs.X + rhs.X, lhs.Y + rhs.Y, lhs.Z + rhs.Z);
		}

        public static Vec3 operator +(Vec3 lhs, double rhs)
        {
            return new Vec3(lhs.X + rhs, lhs.Y + rhs, lhs.Z + rhs);
        }

        public static Vec3 operator -(Vec3 lhs, Vec3 rhs)
        {
            return new Vec3(lhs.X - rhs.X, lhs.Y - rhs.Y, lhs.Z - rhs.Z);
        }

        public static Vec3 operator - (Vec3 lhs, double rhs)
        {
            return new Vec3(lhs.X - rhs, lhs.Y - rhs, lhs.Z - rhs);
        }

        public static Vec3 operator *(Vec3 self, double s)
		{
			return new Vec3(self.X * s, self.Y * s, self.Z * s);
		}
        public static Vec3 operator *(double lhs, Vec3 rhs)
        {
            return new Vec3(rhs.X * lhs, rhs.Y * lhs, rhs.Z * lhs);
        }

        public static Vec3 operator /(Vec3 lhs, double rhs)
        {
            return new Vec3(lhs.X / rhs, lhs.Y / rhs, lhs.Z / rhs);
        }

        public static Vec3 operator * (Vec3 lhs, Vec3 rhs)
        {
            return new Vec3(rhs.X * lhs.X, rhs.Y * lhs.Y, rhs.Z * lhs.Z);
        }

	    public static Vec3 Normalize(Vec3 v)
	    {
	        var sqr = v.X * v.X + v.Y * v.Y + v.Z * v.Z;
	        return v * (1.0f / (double)Math.Sqrt(sqr));
	    }

	    public Vec3 Normalize()
	    {
	        return Normalize(this);
	    }

	    public static double Dot(Vec3 x, Vec3 y)
	    {
	        var tmp = new Vec3(x * y);
	        return tmp.X + tmp.Y + tmp.Z;
	    }

	    public double Dot(Vec3 v)
	    {
	        return Dot(this, v);
	    }

	    public static Vec3 Cross(Vec3 lhs, Vec3 rhs)
	    {
	        return new Vec3(
	            lhs.Y * rhs.Z - rhs.Y * lhs.Z,
	            lhs.Z * rhs.X - rhs.Z * lhs.X,
	            lhs.X * rhs.Y - rhs.X * lhs.Y);
	    }

        public Vec3 Cross(Vec3 v)
        {
            return Cross(this, v);
        }

	    public static double Angle(Vec3 a, Vec3 b)
	    {
	        return Math.Acos(a.Normalize().Dot(b.Normalize()));
        }

	    public double Angle(Vec3 v)
	    {
	        return Angle(this, v);
	    }

	    public static bool Valid(Vec3 v)
	    {
	        for(var i = 0; i < 3; i++)
	            if (double.IsNaN(v[i]) || double.IsInfinity(v[i]))
	                return false;
	        return true;
	    }

	    public bool Valid()
	    {
	        return Valid(this);
	    }

	    public static double Length(Vec3 v)
	    {
	        return Math.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z);
	    }

	    public double Length()
	    {
	        return Length(this);
	    }

        public double[] ToArray()
        {
            return new[] { X, Y, Z };
        }
	}
}