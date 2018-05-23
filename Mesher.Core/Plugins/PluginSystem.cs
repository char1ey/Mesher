using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Mesher.Core.Plugins
{
    public class PluginSystem
    {
        public List<Type> Plugins { get; private set; }

        public PluginSystem(String pluginsFolderPath)
        {
            Plugins = GetPlugins(pluginsFolderPath);
        }
        private static List<Type> GetPlugins(String path)
        {
            if (!Directory.Exists(path))
                return new List<Type>();

            var dllFileNames = Directory.GetFiles(path, "*.dll");
            var assemblies = new List<Assembly>();

            foreach (var dllFile in dllFileNames)
            {
                try
                {
                    var assembly = Assembly.LoadFile(dllFile);
                    assemblies.Add(assembly);
                } catch { }
            }

            var pluginType = typeof(Plugin);
            List<Type> pluginTypes = new List<Type>();

            foreach (var assembly in assemblies)
                if (assembly != null)
                {
                    var types = assembly.GetTypes();
                    
                    foreach (var type in types)
                    {
                        if (type.IsInterface || type.IsAbstract)
                            continue;

                        if (type.IsSubclassOf(pluginType))
                            pluginTypes.Add(type);
                    }
                }

            return pluginTypes;
        }
    }
}
