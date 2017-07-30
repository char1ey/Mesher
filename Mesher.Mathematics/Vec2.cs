using System;

namespace Mesher.Mathematics 
{
    /// <summary>
    /// Represents a two dimensional vector.
    /// </summary>
	public struct Vec2
	{
		public double X;
		public double Y;

		public double this[int index]
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

		public Vec2(double s)
		{
			X = Y = s;
		}

		public Vec2(double x, double y)
		{
			this.X = x;
			this.Y = y;
		}

        public Vec2(Vec2 v)
        {
            this.X = v.X;
            this.Y = v.Y;
        }

        public Vec2(Vec3 v)
        {
            this.X = v.X;
            this.Y = v.Y;
        }
		
		public static Vec2 operator + (Vec2 lhs, Vec2 rhs)
		{
			return new Vec2(lhs.X + rhs.X, lhs.Y + rhs.Y);
		}

        public static Vec2 operator +(Vec2 lhs, double rhs)
        {
            return new Vec2(lhs.X + rhs, lhs.Y + rhs);
        }

        public static Vec2 operator -(Vec2 lhs, Vec2 rhs)
        {
            return new Vec2(lhs.X - rhs.X, lhs.Y - rhs.Y);
        }

        public static Vec2 operator - (Vec2 lhs, double rhs)
        {
            return new Vec2(lhs.X - rhs, lhs.Y - rhs);
        }

        public static Vec2 operator *(Vec2 self, double s)
		{
			return new Vec2(self.X * s, self.Y * s);
		}

        public static Vec2 operator *(double lhs, Vec2 rhs)
        {
            return new Vec2(rhs.X * lhs, rhs.Y * lhs);
        }

        public static Vec2 operator * (Vec2 lhs, Vec2 rhs)
        {
            return new Vec2(rhs.X * lhs.X, rhs.Y * lhs.Y);
        }

        public static Vec2 operator /(Vec2 lhs, double rhs)
        {
            return new Vec2(lhs.X / rhs, lhs.Y / rhs);
        }

	    public static Vec2 Normalize(Vec2 v)
	    {
	        var sqr = v.X * v.X + v.Y * v.Y;
	        return v * (1.0f / (double)Math.Sqrt(sqr));
	    }

	    public static double Dot(Vec2 x, Vec2 y)
	    {
	        var tmp = new Vec2(x * y);
	        return tmp.X + tmp.Y;
	    }

        public double[] ToArray()
        {
            return new[] { X, Y };
        }
	}
}