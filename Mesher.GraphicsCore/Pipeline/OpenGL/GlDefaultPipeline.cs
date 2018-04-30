using System;
using Mesher.GraphicsCore.Data;
using Mesher.GraphicsCore.Data.OpenGL;
using Mesher.GraphicsCore.ShaderProgram;
using Mesher.Mathematics;

namespace Mesher.GraphicsCore.Pipeline.OpenGL
{
    //in vec3 position;
    //in vec2 texCoord;
    //in vec3 normal; 
    //in vec3 tangent;
    //in vec3 biTangent;
    //
    //uniform bool hasTexCoord;
    //uniform bool hasNormal;
    //uniform bool hasTangentBasis;
    //
    //uniform vec3 lightPosition;
    //uniform mat4 proj;
    //uniform mat4 modelView; 


    public class GlDefaultPipeline : IPipeline<GlDefaultPipelineVariable>
    {
        internal GlShaderProgram ShaderProgram { get; private set; }
        public GlDefaultPipelineVariable Positions { get; private set; }

        public GlDefaultPipelineVariable HasTexCoords { get; private set; }
        public GlDefaultPipelineVariable TexCoords { get; private set; }

        public GlDefaultPipelineVariable HasNormals { get; private set; }
        public GlDefaultPipelineVariable Normals { get; private set; }

        public GlDefaultPipelineVariable HasTangentBasis { get; private set; }
        public GlDefaultPipelineVariable Tangents { get; private set; }
        public GlDefaultPipelineVariable BiTangents { get; private set; }

        public GlDefaultPipelineVariable ProjectionMatrix { get; private set; }
        public GlDefaultPipelineVariable ViewMatrix { get; private set; }

        public GlDefaultPipelineVariable Indexes { get; private set; }

        internal GlDefaultPipeline()
        {
            ShaderProgram = new GlShaderProgram(Properties.Resources.DefaultVertexShaderProgram, Properties.Resources.DefaultFragmentShaderProgram);
        }
    }
}
