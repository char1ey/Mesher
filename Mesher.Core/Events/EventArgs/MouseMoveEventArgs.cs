using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mesher.Mathematics;

namespace Mesher.Core.Events.EventArgs
{
    public class MouseMoveEventArgs : DocumentViewEventArgs
    {
        public Vec2 Position { get; private set; }

        public MouseMoveEventArgs(Vec2 position)
        {
            Position = position;
        }
    }
}
