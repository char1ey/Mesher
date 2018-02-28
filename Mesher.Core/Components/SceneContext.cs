using System;
using System.Drawing;
using System.Windows.Forms;
using Mesher.GraphicsCore;
using Mesher.GraphicsCore.Camera;
using Mesher.GraphicsCore.Light;
using Mesher.GraphicsCore.Material;
using Mesher.GraphicsCore.Objects;
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

        public Camera Camera { get; set; }

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

        public void SetMaterialValue(Material material)
        {
           m_renderWindow.RenderManager.ShaderProgram.SetVariableValue(ShaderVariable.MaterialHasColorAmbient, material.HasColorAmbient);
            if (material.HasColorAmbient)
                m_renderWindow.RenderManager.ShaderProgram.SetVariableValue(ShaderVariable.MaterialColorAmbient, material.ColorAmbient);

            m_renderWindow.RenderManager.ShaderProgram.SetVariableValue(ShaderVariable.MaterialHasColorDiffuse, material.HasColorDiffuse);
            if (material.HasColorDiffuse)
                m_renderWindow.RenderManager.ShaderProgram.SetVariableValue(ShaderVariable.MaterialColorDiffuse, material.ColorDiffuse);

            m_renderWindow.RenderManager.ShaderProgram.SetVariableValue(ShaderVariable.MaterialHasColorSpecular, material.HasColorSpecular);
            if (material.HasColorSpecular)
                m_renderWindow.RenderManager.ShaderProgram.SetVariableValue(ShaderVariable.MaterialColorSpecular, material.ColorSpecular);

            m_renderWindow.RenderManager.ShaderProgram.SetVariableValue(ShaderVariable.MaterialHasTextureAmbient, material.HasTextureAmbient);
            if (material.HasTextureAmbient)
                m_renderWindow.RenderManager.ShaderProgram.SetVariableValue(ShaderVariable.MaterialTextureAmbient, material.TextureAmbient);

            m_renderWindow.RenderManager.ShaderProgram.SetVariableValue(ShaderVariable.MaterialHasTextureDiffuse, material.HasTextureDiffuse);
            if (material.HasTextureDiffuse)
                m_renderWindow.RenderManager.ShaderProgram.SetVariableValue(ShaderVariable.MaterialTextureDiffuse, material.TextureDiffuse);

            m_renderWindow.RenderManager.ShaderProgram.SetVariableValue(ShaderVariable.MaterialHasTextureEmissive, material.HasTextureEmissive);
            if (material.HasTextureEmissive)
                m_renderWindow.RenderManager.ShaderProgram.SetVariableValue(ShaderVariable.MaterialTextureEmissive, material.TextureEmissive);

            m_renderWindow.RenderManager.ShaderProgram.SetVariableValue(ShaderVariable.MaterialHasTextureSpecular, material.HasTextureSpecular);
            if (material.HasTextureSpecular)
                m_renderWindow.RenderManager.ShaderProgram.SetVariableValue(ShaderVariable.MaterialTextureSpecular, material.TextureSpecular);

            m_renderWindow.RenderManager.ShaderProgram.SetVariableValue(ShaderVariable.MaterialHasTextureNormal, material.HasTextureNormal);
            if (material.HasTextureNormal)
                m_renderWindow.RenderManager.ShaderProgram.SetVariableValue(ShaderVariable.MaterialTextureNormal, material.TextureNormal);
        }

        public void SetMeshValue(Mesh mesh)
        {
            m_renderWindow.RenderManager.ShaderProgram.SetVariableValue(ShaderVariable.MeshHasVertexes, mesh.HasVertexes);
            if (mesh.HasVertexes)
                m_renderWindow.RenderManager.ShaderProgram.SetVertexBuffer(ShaderVariable.MeshVertexes, mesh.Vertexes);

            m_renderWindow.RenderManager.ShaderProgram.SetVariableValue(ShaderVariable.MeshHasNormals, mesh.HasNormals);
            if (mesh.HasNormals)
                m_renderWindow.RenderManager.ShaderProgram.SetVertexBuffer(ShaderVariable.MeshNormals, mesh.Normals);

            m_renderWindow.RenderManager.ShaderProgram.SetVariableValue(ShaderVariable.MeshHasTextureVertexes, mesh.HasTextureVertexes);
            if (mesh.HasTextureVertexes)
                m_renderWindow.RenderManager.ShaderProgram.SetVertexBuffer(ShaderVariable.MeshTextureVertexes, mesh.TextureVertexes);

            m_renderWindow.RenderManager.ShaderProgram.SetVariableValue(ShaderVariable.MeshHasTangentBasis, mesh.HasTangentBasis);
            if (mesh.HasTangentBasis)
            {
                m_renderWindow.RenderManager.ShaderProgram.SetVertexBuffer(ShaderVariable.MeshTangents, mesh.Tangents);
                m_renderWindow.RenderManager.ShaderProgram.SetVertexBuffer(ShaderVariable.MeshBiTangents, mesh.BiTangents);
            }

            if (mesh.HasMaterial)
                SetMaterialValue(mesh.Material);
        }

        public void SetLights(Lights lights)
        {
            m_renderWindow.RenderManager.ShaderProgram.SetVariableValue(ShaderVariable.LightLightsCount, lights.Count);

            for (var i = 0; i < lights.Count; i++)
            {
                var light = lights[i];

                m_renderWindow.RenderManager.ShaderProgram.SetArrayValue(i, ShaderVariable.LightLightType, (Int32)light.LightType);
                m_renderWindow.RenderManager.ShaderProgram.SetArrayValue(i, ShaderVariable.LightAmbientColor, light.AmbientColor);
                m_renderWindow.RenderManager.ShaderProgram.SetArrayValue(i, ShaderVariable.LightDiffuseColor, light.DiffuseColor);
                m_renderWindow.RenderManager.ShaderProgram.SetArrayValue(i, ShaderVariable.LightSpecularColor, light.SpecularColor);
                m_renderWindow.RenderManager.ShaderProgram.SetArrayValue(i, ShaderVariable.LightPosition, light.Position);
                m_renderWindow.RenderManager.ShaderProgram.SetArrayValue(i, ShaderVariable.LightDirection, light.Direction);
                m_renderWindow.RenderManager.ShaderProgram.SetArrayValue(i, ShaderVariable.LightInnerAngle, light.InnerAngle);
                m_renderWindow.RenderManager.ShaderProgram.SetArrayValue(i, ShaderVariable.LightOuterAngle, light.OuterAngle);
                m_renderWindow.RenderManager.ShaderProgram.SetArrayValue(i, ShaderVariable.LightAttenuationConstant, light.AttenuationConstant);
                m_renderWindow.RenderManager.ShaderProgram.SetArrayValue(i, ShaderVariable.LightAttenuationLinear, light.AttenuationLinear);
                m_renderWindow.RenderManager.ShaderProgram.SetArrayValue(i, ShaderVariable.LightAttenuationQuadratic, light.AttenuationQuadratic);
            }
        }

        public void Render(Scene scene, Int32 cameraId)
        {
            m_renderWindow.RenderManager.ShaderProgram.Begin();

            for (var i = 0; i < scene.Meshes.Count; i++)
            {
                SetLights(scene.Lights);

                scene.Meshes[i].Material.Activate();

                m_renderWindow.RenderManager.ShaderProgram.SetVariableValue(ShaderVariable.ProjectionMatrix, scene.Cameras[cameraId].ProjectionMatrix);
                m_renderWindow.RenderManager.ShaderProgram.SetVariableValue(ShaderVariable.ModelViewMatrix, scene.Cameras[cameraId].ViewMatrix);

                SetMeshValue(scene.Meshes[i]);

                scene.Meshes[i].Render();

                scene.Meshes[i].Material.Deactivate();
            }

            m_renderWindow.RenderManager.ShaderProgram.End();
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
            var zoom = (Single) e.Delta / SystemInformation.MouseWheelScrollDelta * ZoomSpeed;
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
