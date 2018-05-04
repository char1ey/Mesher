using System;

namespace Mesher.Core
{
    public interface IDocument
    {
        void Save(String path);
        void Load(String fileName);
    }
}