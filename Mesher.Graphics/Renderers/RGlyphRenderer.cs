using System;
using Mesher.Graphics.Primitives;

namespace Mesher.Graphics.Renderers
{
    public abstract class RGlyphRenderer : RPrimitiveRenderer<RGlyphs>, IDisposable
    {
        public abstract void Dispose();
    }
}