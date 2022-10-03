using System;
using System.IO;
using System.Reflection;

namespace YYX.FileFinder.Tools
{
    public static class AssemblyHelper
    {
        public static void Load()
        {
            AppDomain.CurrentDomain.AssemblyResolve += (sender, a) =>
            {
                var nameSpace = typeof(Program).Namespace;
                var resourceName = $"{nameSpace}.Resources.{new AssemblyName(a.Name).Name}.dll";
                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
                {
                    if (stream == null)
                    {
                        throw new FileLoadException(resourceName);
                    }

                    var assemblyData = new byte[stream.Length];
                    // ReSharper disable once MustUseReturnValue
                    stream.Read(assemblyData, 0, assemblyData.Length);
                    return Assembly.Load(assemblyData);

                }
            };
        }
    }
}
