using System;

namespace Mesher.Core.Objects.Light
{
    [Flags]
    public enum LightType
    {
        Undefined = 0,
        Directional = 1,
        Point = 2,
        Spot = 4
    }
}
