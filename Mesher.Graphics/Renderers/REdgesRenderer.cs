﻿using System;
using Mesher.Graphics.Primitives;

namespace Mesher.Graphics.Renderers
{
    public abstract class REdgesRenderer : RPrimitiveRenderer<REdges>, IDisposable
    {
        public abstract void Dispose();
    }
}