using System;
using System.Drawing;

namespace Mesher.Core
{
    public abstract class CameraControler
    {
        protected SceneForm.SceneForm SceneForm { get; private set; }

        public CameraControler(SceneForm.SceneForm sceneForm)
        {
            SceneForm = sceneForm;
        }

        public abstract void ReleseControler(Point currentScreenCoordinate);
        public abstract void Move(Point currentScreenCoordinate);
        public abstract void Rotate(Point currentScreeCoordinate);
        public abstract void Zoom(Point currentScreenCoordinate, Single delta);
    }
}