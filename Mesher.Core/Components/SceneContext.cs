using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Mesher.Core.Camera;
using Mesher.Core.Light;
using Mesher.Core.Objects;
using Mesher.GraphicsCore;
using Mesher.GraphicsCore.ShaderProgram;
using Mesher.Mathematics;

namespace Mesher.Core.Components
{
    public partial class SceneContext : UserControl
    {
        private const Single Eps = 1e-9f;
        private const Single ZoomSpeed = 1.2f;
        private const Single RotationSpeed = 5f;

        private readonly RenderWindow m_renderWindow;

        private MouseButtons m_previousMouseButton;
        private Vec2 m_previousMousePosition;

        public Camera.Camera Camera { get; set; }

        public SceneContext(RenderManager renderManager)
        {
            m_renderWindow = renderManager.CreateRenderWindow(Handle);
            m_renderWindow.ClearColor = Color.DimGray;
            InitializeComponent();
        }

        public void BeginRender()
        {
            m_renderWindow.Begin();
            m_renderWindow.Clear();
        }

        public void EndRender()
        {
            m_renderWindow.End();
            m_renderWindow.SwapBuffers();
        }

        public void Render(Scene scene)
        {
            if (Camera == null)
            {
                Camera = new OrthographicCamera(Width, Height, new Vec3(0, 0, 1), new Vec3(0, 1, 0), new Vec3(0, 0, 0));
                Camera.Id = scene.Cameras.Count;
                scene.Cameras.Add((OrthographicCamera)Camera);
            }

            Render(scene, Camera.Id);
        }

        public void SetMaterialValue(Material.Material material)
        {
            m_renderWindow.RenderManager.ShaderProgram.SetVariableValue("material.hasColorAmbient", material.HasColorAmbient);
            if (material.HasColorAmbient)
                m_renderWindow.RenderManager.ShaderProgram.SetVariableValue("material.colorAmbient", material.ColorAmbient);

            m_renderWindow.RenderManager.ShaderProgram.SetVariableValue("material.hasColorDiffuse", material.HasColorDiffuse);
            if (material.HasColorDiffuse)
                m_renderWindow.RenderManager.ShaderProgram.SetVariableValue("material.colorDiffuse", material.ColorDiffuse);

            m_renderWindow.RenderManager.ShaderProgram.SetVariableValue("material.hasColorSpecular", material.HasColorSpecular);
            if (material.HasColorSpecular)
                m_renderWindow.RenderManager.ShaderProgram.SetVariableValue("material.colorSpecular", material.ColorSpecular);

            m_renderWindow.RenderManager.ShaderProgram.SetVariableValue("material.hasTextureAmbient", material.HasTextureAmbient);
            if (material.HasTextureAmbient)
                m_renderWindow.RenderManager.ShaderProgram.SetVariableValue("material.textureAmbient", material.TextureAmbient);

            m_renderWindow.RenderManager.ShaderProgram.SetVariableValue("material.hasTextureDiffuse", material.HasTextureDiffuse);
            if (material.HasTextureDiffuse)
                m_renderWindow.RenderManager.ShaderProgram.SetVariableValue("material.textureDiffuse", material.TextureDiffuse);

            m_renderWindow.RenderManager.ShaderProgram.SetVariableValue("material.hasTextureSpecular", material.HasTextureSpecular);
            if (material.HasTextureSpecular)
                m_renderWindow.RenderManager.ShaderProgram.SetVariableValue("material.textureSpecular", material.TextureSpecular);

            m_renderWindow.RenderManager.ShaderProgram.SetVariableValue("material.hasTextureNormal", material.HasTextureNormal);
            if (material.HasTextureNormal)
                m_renderWindow.RenderManager.ShaderProgram.SetVariableValue("material.textureNormal", material.TextureNormal);
        }

        public void SetMeshValue(Mesh mesh)
        {
            m_renderWindow.RenderManager.ShaderProgram.SetVariableValue("hasPosition", mesh.HasVertexes);
            if (mesh.HasVertexes)
                m_renderWindow.RenderManager.ShaderProgram.SetVertexBuffer("position", mesh.Vertexes);

            m_renderWindow.RenderManager.ShaderProgram.SetVariableValue("hasNormal", mesh.HasNormals);
            if (mesh.HasNormals)
                m_renderWindow.RenderManager.ShaderProgram.SetVertexBuffer("normal", mesh.Normals);

            m_renderWindow.RenderManager.ShaderProgram.SetVariableValue("hasTexCoord", mesh.HasTextureVertexes);
            if (mesh.HasTextureVertexes)
                m_renderWindow.RenderManager.ShaderProgram.SetVertexBuffer("texCoord", mesh.TextureVertexes);

            m_renderWindow.RenderManager.ShaderProgram.SetVariableValue("hasTangentBasis", mesh.HasTangentBasis);
            if (mesh.HasTangentBasis)
            {
                m_renderWindow.RenderManager.ShaderProgram.SetVertexBuffer("tangent", mesh.Tangents);
                m_renderWindow.RenderManager.ShaderProgram.SetVertexBuffer("biTangent", mesh.BiTangents);
            }

            if (mesh.HasMaterial)
                SetMaterialValue(mesh.Material);
        }

        public void SetLights(Lights lights)
        {
            m_renderWindow.RenderManager.ShaderProgram.SetVariableValue("lightsCount", lights.Count);

            for (var i = 0; i < lights.Count; i++)
            {
                var light = lights[i];

                m_renderWindow.RenderManager.ShaderProgram.SetVariableValue(String.Format("lights[{0}].lightType", i), (Int32)light.LightType);
                m_renderWindow.RenderManager.ShaderProgram.SetVariableValue(String.Format("lights[{0}].ambientColor", i), light.AmbientColor);
                m_renderWindow.RenderManager.ShaderProgram.SetVariableValue(String.Format("lights[{0}].diffuseColor", i), light.DiffuseColor);
                m_renderWindow.RenderManager.ShaderProgram.SetVariableValue(String.Format("lights[{0}].specularColor", i), light.SpecularColor);
                m_renderWindow.RenderManager.ShaderProgram.SetVariableValue(String.Format("lights[{0}].position", i), light.Position);
                m_renderWindow.RenderManager.ShaderProgram.SetVariableValue(String.Format("lights[{0}].direction", i), light.Direction);
                m_renderWindow.RenderManager.ShaderProgram.SetVariableValue(String.Format("lights[{0}].innerAngle", i), light.InnerAngle);
                m_renderWindow.RenderManager.ShaderProgram.SetVariableValue(String.Format("lights[{0}].outerAngle", i), light.OuterAngle);
                m_renderWindow.RenderManager.ShaderProgram.SetVariableValue(String.Format("lights[{0}].attenuationConstant", i), light.AttenuationConstant);
                m_renderWindow.RenderManager.ShaderProgram.SetVariableValue(String.Format("lights[{0}].attenuationLinear", i), light.AttenuationLinear);
                m_renderWindow.RenderManager.ShaderProgram.SetVariableValue(String.Format("lights[{0}].attenuationQuadratic", i), light.AttenuationQuadratic);
            }
        }

        public void Render(Scene scene, Int32 cameraId)
        {
            m_renderWindow.RenderManager.ShaderProgram.Bind();

            for (var i = 0; i < scene.Meshes.Count; i++)
            {
                SetLights(scene.Lights);

                m_renderWindow.RenderManager.ShaderProgram.SetVariableValue("proj", scene.Cameras[cameraId].ProjectionMatrix);
                m_renderWindow.RenderManager.ShaderProgram.SetVariableValue("modelView", scene.Cameras[cameraId].ViewMatrix);

                SetMeshValue(scene.Meshes[i]);

                m_renderWindow.RenderManager.ShaderProgram.SetIndexBuffer(scene.Meshes[i].Indicies);

                m_renderWindow.RenderManager.ShaderProgram.Render(scene.Meshes[i].IndexedRendering);
            }

            m_renderWindow.RenderManager.ShaderProgram.Unbind();
        }

        protected override void OnResize(EventArgs e)
        {
            m_renderWindow.ResizeWindow(Width, Height);

            if (Camera != null)
            {
                ((OrthographicCamera)Camera).UpdateSize(Width, Height);
            }

            base.OnResize(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            var zoom = (Single)e.Delta / SystemInformation.MouseWheelScrollDelta * ZoomSpeed;
            Camera.Zoom(zoom < 0 ? -1 / zoom : zoom);

            var a = Camera.UnProject(Width / 2f, Height / 2f, 0, Width, Height);
            var b = Camera.UnProject(e.X, Height - e.Y, 0, Width, Height);

            a = Plane.XYPlane.Cross(new Line(a, (Camera.LookAtPoint - Camera.Position).Normalize()));
            b = Plane.XYPlane.Cross(new Line(b, (Camera.LookAtPoint - Camera.Position).Normalize()));

            Camera.Move((b - a) * zoom / 7.2f);

            base.OnMouseWheel(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.LShiftKey)
                m_previousMouseButton = MouseButtons.None;
            base.OnKeyUp(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.LShiftKey)
                m_previousMouseButton = MouseButtons.None;
            base.OnKeyDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (Camera == null)
                return;

            if (e.Button == MouseButtons.None || m_previousMouseButton != e.Button)
            {
                Camera.ClearStack();
                m_previousMousePosition = new Vec2(e.X, e.Y);
            }
            else
            {
                Camera.Pop();

                if (e.Button == MouseButtons.Left)
                {
                    var a = Camera.UnProject(m_previousMousePosition.X, Height - m_previousMousePosition.Y, 0, Width, Height);
                    var b = Camera.UnProject(e.X, Height - e.Y, 0, Width, Height);

                    Camera.Push();
                    Camera.Move(a - b);
                }
                else if (e.Button == MouseButtons.Right)
                {
                    Camera.Push();

                    var v0 = GetArcBallVector((Int32)m_previousMousePosition.X, (Int32)m_previousMousePosition.Y);
                    var v1 = GetArcBallVector(e.X, e.Y);

                    var axis = Mat3.Inverse(Camera.ViewMatrix.ToMat3()) * (Camera.ProjectionMatrix.ToMat3() * v0.Cross(v1).Normalize()).Normalize();
                    var angle = v0.Angle(v1);

                    Camera.Rotate(axis, -angle * RotationSpeed);
                }
            }
            m_previousMouseButton = e.Button;

            base.OnMouseMove(e);
        }

        private Vec3 GetArcBallVector(Int32 x, Int32 y)
        {
            var ret = new Vec3(2f * x / Width - 1, 2f * y / Height - 1, 0);

            ret.Y = -ret.Y;

            var opSqr = ret.X * ret.X + ret.Y * ret.Y;

            if (opSqr <= 1)
                ret.Z = (Single)Math.Sqrt(1 - opSqr);
            else ret = ret.Normalize();

            return ret;
        }
    }
}
