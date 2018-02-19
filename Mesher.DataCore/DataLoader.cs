﻿using Mesher.GraphicsCore.Objects;
using Mesher.Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Mesher.GraphicsCore.Light;
using Mesher.GraphicsCore.Material;
using Mesher.GraphicsCore.Texture;

namespace Mesher.DataCore
{
    public class DataLoader
    {
        public static Scene GetScene(String filePath)
        {
            var path = filePath.Substring(0, filePath.LastIndexOf("\\") + 1);

            var importer = new Assimp.AssimpImporter();

            var aiScene = importer.ImportFile(filePath, Assimp.PostProcessPreset.TargetRealTimeMaximumQuality);

            var scene = new Scene();
            
            if (aiScene.HasLights)
                for (var i = 0; i < aiScene.LightCount; i++)
                    InitLight(scene.Lights.Add(), aiScene.Lights[i]);
                
            if (aiScene.HasMeshes)
                for (var i = 0; i < aiScene.MeshCount; i++)
                {
                    var aiMesh = aiScene.Meshes[i];
                    var mesh = scene.Meshes.Add();

                    if (aiMesh.HasVertices)
                    {
                        var verts = aiMesh.Vertices.Select(t => GetVec3(t)).ToArray();
                        mesh.Vertexes = new GraphicsCore.Buffers.VertexBuffer<Vec3>(verts);
                    }

                    if (aiMesh.HasNormals)
                    {
                        var normals = aiMesh.Normals.Select(t => GetVec3(t)).ToArray();
                        mesh.Normals = new GraphicsCore.Buffers.VertexBuffer<Vec3>(normals);
                    }

                    if (aiMesh.HasTangentBasis)
                    {
                        var tangents = aiMesh.Tangents.Select(t => GetVec3(t)).ToArray();
                        var biTangents = aiMesh.BiTangents.Select(t => GetVec3(t)).ToArray();
                        mesh.Tangents = new GraphicsCore.Buffers.VertexBuffer<Vec3>(tangents);
                        mesh.BiTangents = new GraphicsCore.Buffers.VertexBuffer<Vec3>(biTangents);
                    }

                    if (aiMesh.HasTextureCoords(0))
                    {
                        var texcoord = aiMesh.GetTextureCoords(0).Select(t => new Vec2(t.X, 1 - t.Y)).ToArray();
                        mesh.TextureVertexes = new GraphicsCore.Buffers.VertexBuffer<Vec2>(texcoord);
                    }

                    mesh.HasVertexes = aiMesh.HasVertices;
                    mesh.HasNormals = aiMesh.HasNormals;
                    mesh.HasTextureVertexes = aiMesh.HasTextureCoords(0);
                    mesh.HasTangentBasis = aiMesh.HasTangentBasis;

                    var inds = aiMesh.GetIndices().Select(t => (Int32)t).ToArray();                    

                    mesh.IndexedRendering = true;
                    mesh.Indicies = new GraphicsCore.Buffers.IndexBuffer(inds);

                    mesh.HasMaterial = aiMesh.MaterialIndex != -1;

                    if(mesh.HasMaterial)
                        mesh.Material = GetMaterial(aiScene.Materials[aiMesh.MaterialIndex], path);                    
                }

            return scene;
        }

        private static Material GetMaterial(Assimp.Material aiMaterial, String filePath)
        {      
            var material = new Material();                       

            if (aiMaterial.HasColorAmbient)
                material.ColorAmbient = GetColor(aiMaterial.ColorAmbient);

            if (aiMaterial.HasColorDiffuse)
                material.ColorDiffuse = GetColor(aiMaterial.ColorDiffuse);

            if (aiMaterial.HasColorSpecular)            
                material.ColorSpecular = GetColor(aiMaterial.ColorSpecular);
            

            material.HasColorAmbient = aiMaterial.HasColorAmbient;
            material.HasColorDiffuse = aiMaterial.HasColorDiffuse;
            material.HasColorSpecular = aiMaterial.HasColorSpecular;

            if (aiMaterial.GetTextureCount(Assimp.TextureType.Ambient) > 0)
            {
                material.TextureAmbient = GetTexture(aiMaterial.GetTexture(Assimp.TextureType.Ambient, 0), filePath);
                if (material.TextureAmbient != null)
                    material.HasTextureAmbient = true;
            }
            else material.HasTextureAmbient = false;

            if (aiMaterial.GetTextureCount(Assimp.TextureType.Diffuse) > 0)
            {
                material.TextureDiffuse = GetTexture(aiMaterial.GetTexture(Assimp.TextureType.Diffuse, 0), filePath);
                if (material.TextureDiffuse != null)
                    material.HasTextureDiffuse = true;
            }
            else material.HasTextureDiffuse = false;

            if (aiMaterial.GetTextureCount(Assimp.TextureType.Emissive) > 0)
            {
                material.TextureEmissive = GetTexture(aiMaterial.GetTexture(Assimp.TextureType.Emissive, 0), filePath);
                if (material.TextureEmissive != null)
                    material.HasTextureEmissive = true;
            }
            else material.HasTextureEmissive = false;

            if (aiMaterial.GetTextureCount(Assimp.TextureType.Specular) > 0)
            {
                material.TextureSpecular = GetTexture(aiMaterial.GetTexture(Assimp.TextureType.Specular, 0), filePath);
                if (material.TextureSpecular != null)
                    material.HasTextureSpecular = true;
            }
            else material.HasTextureSpecular = false;

            if (aiMaterial.GetTextureCount(Assimp.TextureType.Height) > 0)
            {
                material.TextureNormal = GetTexture(aiMaterial.GetTexture(Assimp.TextureType.Height, 0), filePath);
                if (material.TextureNormal != null)
                    material.HasTextureNormal = true;
            }
            else material.HasTextureNormal = false;

            return material;
        }

        private static Texture GetTexture(Assimp.TextureSlot aiTexture, String filePath)
        {
            if (!File.Exists(filePath + aiTexture.FilePath))
                return null;
            return new Texture((Bitmap)Image.FromFile(filePath + aiTexture.FilePath));
        }

        private static Light InitLight(Light light, Assimp.Light aiLight)
        {
            light.LightType = (LightType)aiLight.LightType;

            light.Name = aiLight.Name;
            light.AmbientColor = GetColor(aiLight.ColorAmbient);
            light.DiffuseColor = GetColor(aiLight.ColorDiffuse);
            light.SpecularColor = GetColor(aiLight.ColorSpecular);

            if ((light.LightType & LightType.Directional) == LightType.Directional)
                light.Direction = GetVec3(aiLight.Direction);

            if ((light.LightType & LightType.Point) == LightType.Point)
            {
                light.Position = GetVec3(aiLight.Position);
                light.AttenuationConstant = aiLight.AttenuationConstant;
                light.AttenuationLinear = aiLight.AttenuationLinear;
                light.AttenuationQuadratic = aiLight.AttenuationQuadratic;
            }

            if (light.LightType == LightType.Spot)
            {
                light.InnerAngle = aiLight.AngleInnerCone;
                light.OuterAngle = aiLight.AngleOuterCone;
            }

            return light;
        }

        private static Color GetColor(Assimp.Color3D color)
        {
            return Color.FromArgb(1, (Int32)(Math.Min(255f, color.R * 255f)),
                                     (Int32)(Math.Min(255f, color.G * 255f)), (Int32)(Math.Min(255f, color.B * 255f)));
        }

        private static Color GetColor(Assimp.Color4D color)
        {
            return Color.FromArgb((Int32)(Math.Min(255f, color.A * 255f)), (Int32)(Math.Min(255f, color.R * 255f)), 
                                  (Int32)(Math.Min(255f, color.G * 255f)), (Int32)(Math.Min(255f, color.B * 255f)));
        }

        private static Vec3 GetVec3(Assimp.Vector3D vec)
        {
            return new Vec3(vec.X, vec.Y, vec.Z);
        }

        private static Vec2 GetVec2(Assimp.Vector2D vec)
        {
            return new Vec2(vec.X, vec.Y);
        }
    }
}
