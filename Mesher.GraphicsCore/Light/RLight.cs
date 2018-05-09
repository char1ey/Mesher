using System;
using System.Collections.Generic;
using Mesher.GraphicsCore.Data;
using Mesher.GraphicsCore.Primitives;
using Mesher.GraphicsCore.Renderers;
using Mesher.Mathematics;

namespace Mesher.GraphicsCore.Light
{
    public class RLight
    {
		//TODO shadowmap

	    private IDataFactory m_dataFactory;

	    private IFrameBuffer m_frameBuffer;

		public RScene RScene { get; private set; }
  
        public Int32 Id { get; internal set; }
        public String Name { get; set; }
        public LightType LightType { get; set; }
        public Color3 AmbientColor { get; set; }
        public Color3 DiffuseColor { get; set; }
        public Color3 SpecularColor { get; set; }
        public Vec3 Position { get; set; }
        public Vec3 Direction { get; set; }
        public Single InnerAngle { get; set; }
        public Single OuterAngle { get; set; }
        public Single AttenuationConstant { get; set; }
        public Single AttenuationLinear { get; set; }
        public Single AttenuationQuadratic { get; set; }

		public Boolean HasShadowMap { get; set; }
		public Texture.Texture ShadowMap { get; private set; }

	    public RLight(IDataFactory dataFactory, RScene scene)
	    {
		    m_dataFactory = dataFactory;
		    RScene = scene;
	    }

	    public void RenderShadowMap(RenderersFactory renderersFactory, List<RPrimitive> primitives)
	    {
		    renderersFactory.LightRenderer.RenderShadowMap(this, primitives);
	    }
    }
}
