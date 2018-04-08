using System;
using System.Drawing;

namespace Mesher.Core
{
    public abstract class CameraControler
    {
        protected ISceneContext SceneContext { get; private set; }

        public CameraControler(ISceneContext sceneContext)
        {
            SceneContext = sceneContext;
        }

        public abstract void Inaction(Point currentScreenCoordinate);
        public abstract void Move(Point currentScreenCoordinate);
        public abstract void Rotate(Point currentScreeCoordinate);
        public abstract void Zoom(Point currentScreenCoordinate, Single delta);
    }
}