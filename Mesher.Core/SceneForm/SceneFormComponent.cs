﻿using System.Drawing;
using Mesher.Core.Collections;

namespace Mesher.Core.SceneForm
{
    public abstract class SceneFormComponent
    {
        public SceneFormComponents ChildComponents { get; private set; }

        public Point Location { get; set; }
        public Size Size { get; set; }

        public SceneForm SceneForm { get; private set; }

        public SceneFormComponent(SceneForm sceneForm)
        {
            SceneForm = sceneForm;
            ChildComponents = new SceneFormComponents();
        }

        public abstract void Draw();
    }
}