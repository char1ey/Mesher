using System;
using System.Collections.Generic;

namespace Mesher.GraphicsCore.ShaderProgram
{
    internal class ShaderVariablesNames
    {
        private static readonly Dictionary<ShaderVariable, String> s_names = new Dictionary<ShaderVariable, String>
        {
            { ShaderVariable.MeshVertexes, "position" },
            { ShaderVariable.MeshNormals, "normal" },
            { ShaderVariable.MeshTextureVertexes, "texCoord" },
            { ShaderVariable.MeshTangents, "tangent" },
            { ShaderVariable.MeshBiTangents, "biTangent" },
            { ShaderVariable.ModelViewMatrix, "modelView" },
            { ShaderVariable.ProjectionMatrix, "proj" },
            { ShaderVariable.LightLightType, "lights[{0}].lightType" },
            { ShaderVariable.LightAmbientColor, "lights[{0}].ambientColor" },
            { ShaderVariable.LightDiffuseColor, "lights[{0}].diffuseColor" },
            { ShaderVariable.LightSpecularColor, "lights[{0}].specularColor" },
            { ShaderVariable.LightPosition, "lights[{0}].position" },
            { ShaderVariable.LightDirection, "lights[{0}].direction" },
            { ShaderVariable.LightInnerAngle, "lights[{0}].innerAngle" },
            { ShaderVariable.LightOuterAngle, "lights[{0}].outerAngle" },
            { ShaderVariable.LightAttenuationConstant, "lights[{0}].attenuationConstant" },
            { ShaderVariable.LightAttenuationLinear, "lights[{0}].attenuationLinear" },
            { ShaderVariable.LightAttenuationQuadratic, "lights[{0}].attenuationQuadratic" },
            { ShaderVariable.LightLightsCount, "lightsCount" },
            { ShaderVariable.MaterialHasColorAmbient, "material.hasColorAmbient" },
            { ShaderVariable.MaterialColorAmbient, "material.colorAmbient" },
            { ShaderVariable.MaterialHasColorDiffuse, "material.hasColorDiffuse" },
            { ShaderVariable.MaterialColorDiffuse, "material.colorDiffuse" },
            { ShaderVariable.MaterialHasColorSpecular, "material.hasColorSpecular" },
            { ShaderVariable.MaterialColorSpecular, "material.colorSpecular" },
            { ShaderVariable.MaterialHasShininess, "material.hasShininess" },
            { ShaderVariable.MaterialShininess, "material.shininess" },
            { ShaderVariable.MaterialHasTextureAmbient, "material.hasTextureAmbient" },
            { ShaderVariable.MaterialTextureAmbient, "material.textureAmbient" },
            { ShaderVariable.MaterialHasTextureDiffuse, "material.hasTextureDiffuse" },
            { ShaderVariable.MaterialTextureDiffuse, "material.textureDiffuse" },
            { ShaderVariable.MaterialHasTextureSpecular, "material.hasTextureSpecular" },
            { ShaderVariable.MaterialTextureSpecular, "material.textureSpecular" },
            { ShaderVariable.MaterialHasTextureEmissive, "material.hasTextureEmissive" },
            { ShaderVariable.MaterialTextureEmissive, "material.textureEmissive" },
            { ShaderVariable.MaterialHasTextureNormal, "material.hasTextureNormal" },
            { ShaderVariable.MaterialTextureNormal, "material.textureNormal" },
            { ShaderVariable.MeshHasVertexes, "hasPosition" },
            { ShaderVariable.MeshHasTextureVertexes, "hasTexCoord" },
            { ShaderVariable.MeshHasNormals, "hasNormal" },
            { ShaderVariable.MeshHasTangentBasis, "hasTangentBasis" },
        };

        public static Dictionary<ShaderVariable, String> S_names => s_names;

        public static String GetVariableName(ShaderVariable shaderVariable)
        {
            return S_names[shaderVariable];
        }
    }
}
