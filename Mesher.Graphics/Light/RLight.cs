using System;
using System.Collections.Generic;
using Mesher.Graphics.Data;
using Mesher.Graphics.Primitives;
using Mesher.Graphics.Renderers;
using Mesher.Mathematics;

namespace Mesher.Graphics.Light
{
    public class RLight
    {
		//TODO shadowmap

	    private IDataContext m_dataContext;

        public Int32 Id { get; internal set; }
        public String Name { get; set; }
        public RLightType RLightType { get; set; }
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

	    internal RLight(IDataContext dataContext)
	    {
		    m_dataContext = dataContext;
	    }

	    public void RenderShadowMap(RenderersFactory renderersFactory, List<RPrimitive> primitives)
	    {
		    renderersFactory.LightRenderer.RenderShadowMap(this, primitives);
	    }
    }
}
