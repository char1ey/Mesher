using System.Drawing;
using Mesher.Core.Collections;
using Mesher.Core.Renderers;

namespace Mesher.Core.SceneContexts.Components
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

        public virtual void Draw(SceneContextGraphics graphics)
        {
        }

        public virtual void MouseMove(Point location)
        {
        }

        public virtual void MouseClick(Point location)
        {
        }
    }
}
