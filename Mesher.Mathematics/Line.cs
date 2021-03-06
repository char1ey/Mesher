﻿using System;

namespace Mesher.Mathematics
{
    public class Line
    {
        private const Double Eps = 1e-9;

        public Vec3 Point0 { get; set; }
        public Vec3 Direction { get; set; }

        public Line(Vec3 point0, Vec3 direction)
        {
            Point0 = point0;
            Direction = direction;
        }
    }
}
