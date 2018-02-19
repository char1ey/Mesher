using System;

namespace Mesher.Mathematics 
{
    /// <summary>
    /// Represents a two dimensional vector.
    /// </summary>
	public class Vec2 : VecN
	{
        public override Int32 ComponentsCount { get { return 2; } }

        public Double X;
		public Double Y;

		public Double this[Int32 index]
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

		public Vec2() : this(0) { }

		public Vec2(Double s)
		{
			X = Y = s;
		}

		public Vec2(Double x, Double y)
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

        public static Vec2 operator +(Vec2 lhs, Double rhs)
        {
            return new Vec2(lhs.X + rhs, lhs.Y + rhs);
        }

        public static Vec2 operator -(Vec2 lhs, Vec2 rhs)
        {
            return new Vec2(lhs.X - rhs.X, lhs.Y - rhs.Y);
        }

        public static Vec2 operator - (Vec2 lhs, Double rhs)
        {
            return new Vec2(lhs.X - rhs, lhs.Y - rhs);
        }

        public static Vec2 operator *(Vec2 self, Double s)
		{
			return new Vec2(self.X * s, self.Y * s);
		}

        public static Vec2 operator *(Double lhs, Vec2 rhs)
        {
            return new Vec2(rhs.X * lhs, rhs.Y * lhs);
        }

        public static Vec2 operator * (Vec2 lhs, Vec2 rhs)
        {
            return new Vec2(rhs.X * lhs.X, rhs.Y * lhs.Y);
        }

        public static Vec2 operator /(Vec2 lhs, Double rhs)
        {
            return new Vec2(lhs.X / rhs, lhs.Y / rhs);
        }

	    public static Vec2 Normalize(Vec2 v)
	    {
	        var sqr = v.X * v.X + v.Y * v.Y;
	        return v * (1.0f / (Double)Math.Sqrt(sqr));
	    }

	    public static Double Dot(Vec2 x, Vec2 y)
	    {
	        var tmp = new Vec2(x * y);
	        return tmp.X + tmp.Y;
	    }

        public override Single[] GetComponentsFloat()
        {
            return new[] { (Single)X, (Single)Y };
        }

        public override Double[] GetComponentsDouble()
        {
            return new[] { X, Y };
        }
    }
}