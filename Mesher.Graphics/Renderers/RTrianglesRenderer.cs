﻿using System;
using Mesher.Graphics.Primitives;

namespace Mesher.Graphics.Renderers
{
    public abstract class RTrianglesRenderer : RPrimitiveRenderer<RTriangles>, IDisposable
    {
        public abstract void Dispose();
    }
}