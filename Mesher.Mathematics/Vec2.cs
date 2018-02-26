using System;

namespace Mesher.Mathematics 
{
    /// <summary>
    /// Represents a two dimensional vector.
    /// </summary>
	public struct Vec2
	{
        public Single X;
		public Single Y;

		public Single this[Int32 index]
		{
			get 
			{
				if(index == 0) return X;
				else if(index == 1) return Y;
                else throw new Exception("Out of range.");
			}
			set 
			{
				if(index == 0) X = value;
                else if (index == 1) Y = value;
                else throw new Exception("Out of range.");
			}
		}

		public Vec2(Single s)
		{
			X = Y = s;
		}

		public Vec2(Single x, Single y)
		{
			X = x;
			Y = y;
		}

        public Vec2(Vec2 v)
        {
            X = v.X;
            Y = v.Y;
        }

        public Vec2(Vec3 v)
        {
            X = v.X;
            Y = v.Y;
        }
		
		public static Vec2 operator + (Vec2 lhs, Vec2 rhs)
		{
			return new Vec2(lhs.X + rhs.X, lhs.Y + rhs.Y);
		}

        public static Vec2 operator +(Vec2 lhs, Single rhs)
        {
            return new Vec2(lhs.X + rhs, lhs.Y + rhs);
        }

        public static Vec2 operator -(Vec2 lhs, Vec2 rhs)
        {
            return new Vec2(lhs.X - rhs.X, lhs.Y - rhs.Y);
        }

        public static Vec2 operator - (Vec2 lhs, Single rhs)
        {
            return new Vec2(lhs.X - rhs, lhs.Y - rhs);
        }

        public static Vec2 operator *(Vec2 self, Single s)
		{
			return new Vec2(self.X * s, self.Y * s);
		}

        public static Vec2 operator *(Single lhs, Vec2 rhs)
        {
            return new Vec2(rhs.X * lhs, rhs.Y * lhs);
        }

        public static Vec2 operator * (Vec2 lhs, Vec2 rhs)
        {
            return new Vec2(rhs.X * lhs.X, rhs.Y * lhs.Y);
        }

        public static Vec2 operator /(Vec2 lhs, Single rhs)
        {
            return new Vec2(lhs.X / rhs, lhs.Y / rhs);
        }

	    public static Vec2 Normalize(Vec2 v)
	    {
	        var sqr = v.X * v.X + v.Y * v.Y;
	        return v * (1.0f / (Single)Math.Sqrt(sqr));
	    }

	    public static Single Dot(Vec2 x, Vec2 y)
	    {
	        var tmp = new Vec2(x * y);
	        return tmp.X + tmp.Y;
	    }
    }
}