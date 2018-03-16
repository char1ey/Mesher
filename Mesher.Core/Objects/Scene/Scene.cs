﻿using System;
using Mesher.Core.Collections;
using Mesher.Core.Components;
using Mesher.GraphicsCore.ShaderProgram;

namespace Mesher.Core.Objects.Scene
{
    public class Scene : IDisposable
    {
        public Meshes Meshes { get; }

        public Lights Lights { get; }

        public Scene()
        {
            Meshes = new Meshes();
            Lights = new Lights();
        }

        public void Dispose()
        {

        }

    }
}
