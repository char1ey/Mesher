namespace Mesher.Core.Objects
{
    public interface IDocumentItem
    {
        void Rebuild();
        void Render(ISceneContext sceneContext);
    }
}