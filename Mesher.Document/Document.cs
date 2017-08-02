using System.Collections.Generic;
using System.Linq;
using Mesher.Components;
using Mesher.GraphicsCore;
using Mesher.GraphicsCore.Camera;
using Mesher.GraphicsCore.Shaders;

namespace Mesher.Document
{
    public class Document
    {
        private readonly List<SceneContext> m_sceneContexts;

        public ShaderProgram Shader { get; set; }
        public Scene Scene { get; set; }

        public Document()
        {
            Scene = new Scene(); 
            m_sceneContexts = new List<SceneContext>();
        }

        public SceneContext CreateSceneContext(Camera camera)
        {
            if(m_sceneContexts.Count == 0)
                m_sceneContexts.Add(new SceneContext(camera));
            else
                m_sceneContexts.Add(new SceneContext(camera, m_sceneContexts.Last().GlrcHandle));

            return m_sceneContexts.Last();
        }
    }
}