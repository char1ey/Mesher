using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mesher.Graphics;
using Mesher.Graphics.Primitives;

namespace Mesher.Core
{
    public class PostRenderItem
    {
        public RenderArgs RenderArgs { get; private set; }
        public List<RPrimitive> Primitives { get; private set; }


        public PostRenderItem()
        {
            RenderArgs = new RenderArgs();

            Primitives = new List<RPrimitive>();
        }
    }
}
