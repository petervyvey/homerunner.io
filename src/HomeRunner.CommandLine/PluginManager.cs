
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace HomeRunner.CommandLine
{
    public static class PluginManager
    {
        public static IPlugin GetPlugin(string token, string[] args)
        {
            var plugins = PluginManager.GetPluginTypes();
            Program.WriteFormat("found {0} plugin(s)", plugins.Count());

            var plugin =
                plugins
                    .Where(p =>
                        Attribute.GetCustomAttribute(p, typeof(PluginAttribute)) != null
                        &&
                        p.GetCustomAttribute<PluginAttribute>(true).Token.Equals(token, StringComparison.InvariantCultureIgnoreCase))
                    .SingleOrDefault();

            IPlugin instance = default(IPlugin);
            if (plugin != null)
            {
                Program.WriteFormat("{0} => {1}", token, plugin.FullName);
                Program.WriteFormat("creating instance of {0}", plugin.FullName);
                instance = (IPlugin)Activator.CreateInstance(plugin);

                Program.WriteFormat("instance created of {0}", plugin.FullName);
            }
            else
            {
                throw new ArgumentException("Plugin not found.");
            }

            return instance;
        }

        private static List<Type> GetPluginTypes()
        {
            var path = PluginManager.GetAssemblyDirectory();
            var assemblyFiles = Directory.GetFiles(path, "*.dll").ToList();

            Program.WriteFormat("scanning {0} assemblies", assemblyFiles.Count());
            var plugins =
                assemblyFiles
                    .SelectMany(a => Assembly.LoadFrom(a).GetTypes().Where(t => typeof(IPlugin).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract));


            return plugins.ToList();
        }

        private static string GetAssemblyDirectory()
        {

            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            string folder = Path.GetDirectoryName(path);

            Program.WriteFormat("plugin folder {0}", folder);

            return folder;
        }
    }
}
