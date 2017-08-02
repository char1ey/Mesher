using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mesher.Document;
using Mesher.GraphicsCore.BufferObjects;
using Mesher.GraphicsCore.Light;
using Mesher.Mathematics;

namespace Mesher.DataCore
{
    public static class Obj
    {
        public static Scene Load(string fileName)
        {
            var normals = new List<Vec3>();
            var vertexes = new List<Vec3>();
            var textureVertexes = new List<Vec2>();

            var indicies = new List<int>();

            var lights = new List<Light>();

            using (var stream = new FileStream(fileName, FileMode.Open))
            using (var streamReader = new StreamReader(stream))
                while (!streamReader.EndOfStream)
                {
                    var line = streamReader.ReadLine();
                   
                    if(string.IsNullOrEmpty(line))
                        continue;

                    var type = line.Split()[0];

                    if (type == "v")
                        vertexes.Add(ParseVec3(line));
                //    if (type == "vt")
                  //      textureVertexes.Add(ParseVec2(line));
                //    if(type == "vn")
                  //      normals.Add(ParseVec3(line));
                    if (type == "f")
                    {
                        var par = line.Split();

                        var id1 = int.Parse(par[1].Split('/')[0]);

                        for (int i = 3; i < par.Length; i++)
                        {
                            var id2 = int.Parse(par[i - 1].Split('/')[0]);
                            var id3 = int.Parse(par[i].Split('/')[0]);

                            indicies.Add(id1 - 1);
                            indicies.Add(id2 - 1);
                            indicies.Add(id3 - 1);
                        }
                    }
                }

            var mesh = new Mesh(vertexes.ToArray(), textureVertexes.ToArray(), normals.ToArray(), indicies.ToArray(), null);

            return new Scene(mesh, lights);
        }

        public static void Save(Scene scene)
        {
            throw new NotImplementedException();
        }

        private static Vec3 ParseVec3(string s)
        {
            var par = s.Split(new []{ ' ' }, StringSplitOptions.RemoveEmptyEntries);

            return new Vec3(ParseDouble(par[1]), ParseDouble(par[2]), ParseDouble(par[3]));
        }

        private static Vec2 ParseVec2(string s)
        {
            var par = s.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            return new Vec2(ParseDouble(par[1]), ParseDouble(par[2]));
        }

        private static double ParseDouble(string s)
        {
            var separator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
            return double.Parse(s.Replace('.', separator).Replace(',', separator));
        }
    }
}
