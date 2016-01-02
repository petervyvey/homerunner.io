﻿
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace HomeRunner.CommandLine
{
    public static class PluginManager
    {
        public static void StartPlugin(string token, string[] args)
        {
            var plugins = PluginManager.GetPluginTypes();
            Program.LOGGER.Debug(string.Format("found {0} plugin(s)", plugins.Count()));

            var plugin =
                plugins
                    .Where(p =>
                        Attribute.GetCustomAttribute(p, typeof(PluginAttribute)) != null
                        &&
                        p.GetCustomAttribute<PluginAttribute>(true).Token.Equals(token, StringComparison.InvariantCultureIgnoreCase))
                    .SingleOrDefault();

            if (plugin != null)
            {
				Program.LOGGER.InfoFormat(string.Format("{0} => {1}", token, plugin.FullName));
                var instance = (IPlugin)Activator.CreateInstance(plugin);

                Program.LOGGER.DebugFormat(string.Format("created instance of {0}", plugin.FullName));

                instance.Start(Program.SESSION_ID, args);
            }
        }

        private static List<Type> GetPluginTypes()
        {
            var path = PluginManager.GetAssemblyDirectory();
            var assemblyFiles = Directory.GetFiles(path, "*.dll").ToList();
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

            return Path.GetDirectoryName(path);
        }
    }
}
