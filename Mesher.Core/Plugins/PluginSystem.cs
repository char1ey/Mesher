﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mesher.Core.Plugins
{
    internal class PluginSystem
    {
        public static List<IPlugin> GetPlugins(String path)
        {
            var plugins = new List<IPlugin>();

            if (!Directory.Exists(path))
                return plugins;

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

            var pluginType = typeof(IPlugin);
            ICollection<Type> pluginTypes = new List<Type>();

            foreach (var assembly in assemblies)
                if (assembly != null)
                {
                    var types = assembly.GetTypes();

                    foreach (var type in types)
                    {
                        if (type.IsInterface || type.IsAbstract)
                            continue;

                        if (type.GetInterface(pluginType.FullName) != null)
                            pluginTypes.Add(type);
                    }
                }

            foreach (var type in pluginTypes)
            {
                var plugin = (IPlugin)Activator.CreateInstance(type);
                plugins.Add(plugin);
            }

            return plugins;
        }
    }
}
