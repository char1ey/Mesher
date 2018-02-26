using System;

namespace Mesher.Mathematics
{
    /// <summary>
    /// Represents a three dimensional vector.
    /// </summary>
	public struct Vec3
	{
        public static Vec3 Zero = new Vec3(0, 0, 0);

		public Single X;
		public Single Y;
		public Single Z;

		public Single this[Int32 index]
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

		public Vec3(Single s)
		{
			X = Y = Z = s;
		}

		public Vec3(Single x, Single y, Single z)
		{
			X = x;
			Y = y;
            Z = z;
		}

        public Vec3(Vec3 v)
        {
            X = v.X;
            Y = v.Y;
            Z = v.Z;
        }

        public Vec3(Vec4 v)
        {
            X = v.X;
            Y = v.Y;
            Z = v.Z; 
        }

        public Vec3(Vec2 xy, Single z)
        {
            X = xy.X;
            Y = xy.Y;
            Z = z;
        }
		
		public static Vec3 operator + (Vec3 lhs, Vec3 rhs)
		{
			return new Vec3(lhs.X + rhs.X, lhs.Y + rhs.Y, lhs.Z + rhs.Z);
		}

        public static Vec3 operator +(Vec3 lhs, Single rhs)
        {
            return new Vec3(lhs.X + rhs, lhs.Y + rhs, lhs.Z + rhs);
        }

        public static Vec3 operator -(Vec3 lhs, Vec3 rhs)
        {
            return new Vec3(lhs.X - rhs.X, lhs.Y - rhs.Y, lhs.Z - rhs.Z);
        }

        public static Vec3 operator - (Vec3 lhs, Single rhs)
        {
            return new Vec3(lhs.X - rhs, lhs.Y - rhs, lhs.Z - rhs);
        }

        public static Vec3 operator *(Vec3 self, Single s)
		{
			return new Vec3(self.X * s, self.Y * s, self.Z * s);
		}
        public static Vec3 operator *(Single lhs, Vec3 rhs)
        {
            return new Vec3(rhs.X * lhs, rhs.Y * lhs, rhs.Z * lhs);
        }

        public static Vec3 operator /(Vec3 lhs, Single rhs)
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
	        return v * (1.0f / (Single)Math.Sqrt(sqr));
	    }

	    public Vec3 Normalize()
	    {
	        return Normalize(this);
	    }

	    public static Single Dot(Vec3 x, Vec3 y)
	    {
	        var tmp = new Vec3(x * y);
	        return tmp.X + tmp.Y + tmp.Z;
	    }

	    public Single Dot(Vec3 v)
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

	    public static Single Angle(Vec3 a, Vec3 b)
	    {
	        return (Single)Math.Acos(a.Normalize().Dot(b.Normalize()));
        }

	    public Single Angle(Vec3 v)
	    {
	        return Angle(this, v);
	    }

	    public static Boolean Valid(Vec3 v)
	    {
	        for(var i = 0; i < 3; i++)
	            if (Single.IsNaN(v[i]) || Single.IsInfinity(v[i]))
	                return false;
	        return true;
	    }

	    public Boolean Valid()
	    {
	        return Valid(this);
	    }

	    public static Single Length(Vec3 v)
	    {
	        return (Single)Math.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z);
	    }

	    public Single Length()
	    {
	        return Length(this);
	    }

        public Vec3 Clone()
        {
            return new Vec3(X, Y, Z);
        }
    }
}