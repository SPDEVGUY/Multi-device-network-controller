using System.Linq;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.IO;

namespace NetworkController.Logic.Controller
{
    /// <summary>
    /// This is the core logic class for performing discoveries.
    /// </summary>
    public class PluginController
    {
        private static PluginController _instance;
        public static PluginController Instance
        {
            get { return _instance ?? (_instance = new PluginController()); }
        }
        public List<Assembly> LoadedPlugins;
        public List<Exception> LoadExceptions;

        public PluginController()
        {
            LoadPlugins();
        }

        public void LoadPlugins()
        {
            LoadedPlugins = new List<Assembly>();
            LoadExceptions = new List<Exception>();

            var localPluginFileNames = Directory.GetFiles(".", "NetworkController.Plugin.*.dll",
                                                          SearchOption.AllDirectories);

            foreach (var pluginFileName in localPluginFileNames)
            {
                try
                {
                    var file = new FileInfo(pluginFileName);
                    var assemblyFileLocation = file.FullName;

                    var asm = Assembly.LoadFile(assemblyFileLocation);
                    LoadedPlugins.Add(asm);

                }
                catch (Exception ex)
                {
                    LoadExceptions.Add(
                            new Exception(
                                string.Format("Failed to load assembly <{0}>.", pluginFileName)
                                ,ex)
                        );
                }
            }
        }

        public List<T> GetProviders<T, TAttribute>() where T : class
        {
            var result = new List<T>();

            foreach (var plugin in LoadedPlugins)
            {
                try
                {
                    List<Exception> issues;
                    var providers = ReflectoMatic.CreateObjects<T, TAttribute>(plugin, out issues);
                    result.AddRange(providers);
                    LoadExceptions.AddRange(issues);
                }
                catch (Exception ex)
                {
                    LoadExceptions.Add(
                            new Exception(
                                string.Format("Failed to create providers from <{0}>.", plugin.FullName)
                                , ex)
                        );
                }
            }

            return result;
        }
    }
}
