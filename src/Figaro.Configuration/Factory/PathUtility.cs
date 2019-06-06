/*************************************************************************************************
* 
* THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
* KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
* IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
* PARTICULAR PURPOSE.
* 
*************************************************************************************************/
#if DEBUG
using System.Diagnostics;
#endif
using System;
using System.IO;
using System.Web.Hosting;
namespace Figaro.Configuration.Factory
{
    /// <summary>
    /// Used to resolve the file paths containing environment variables.
    /// </summary>
    internal static class PathUtility
    {
        private const char EnvSplit = '%';
        /// <summary>
        /// Resolve paths possibly containing environment variables.
        /// </summary>
        /// <param name="path">The directory path to resolve.</param>
        /// <returns>A resolved path.</returns>
        public static string ResolvePath(string path)
        {
            var ret = path;
            var envs = path.Split(new[] { EnvSplit });
            foreach (var env in envs)
            {
                var envValue = EnvSplit + env + EnvSplit;
                if (string.IsNullOrEmpty(env)) continue;                
                var envPaths = Environment.ExpandEnvironmentVariables(envValue).Split(new[] { Path.PathSeparator });
                ret = ret.Replace(envValue, envPaths[0]);
            }
            if (HostingEnvironment.IsHosted)
            {
                ret = HostingEnvironment.MapPath(ret);
            }

#if DEBUG
            Debug.WriteLine(string.Format("resolving '{0}' to '{1}'... ", path, ret), "Configuration");
#endif

            return ret;
        }
    }
}
