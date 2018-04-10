using System.Drawing;
using Mesher.Core.Collections;

namespace Mesher.Core.SceneForm
{
    public abstract class SceneContextComponent
    {
        public SceneFormComponents ChildComponents { get; private set; }

        public Point Location { get; set; }
        public Size Size { get; set; }

        public ISceneContext SceneContext { get; private set; }

        public SceneContextComponent(ISceneContext sceneContext)
        {
            SceneContext = sceneContext;
            ChildComponents = new SceneFormComponents();
        }

        public virtual void Draw()
        {
        }

        public virtual void MouseMove(Point location)
        {
        }

        public virtual void MouseClick()
        {
        }
    }
}
