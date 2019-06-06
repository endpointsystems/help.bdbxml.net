#pragma warning disable 1574
#pragma warning disable 0436

/*************************************************************************************************
* 
* THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
* KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
* IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
* PARTICULAR PURPOSE.
* 
*************************************************************************************************/
using System;
using System.Configuration;

namespace Figaro.Configuration.Factory
{
    /// <summary>
    /// Creates <see cref="XmlManager"/> instances from configuration.
    /// </summary>
    public class ManagerFactory
    {
        /// <summary>
        /// Create a new <see cref="XmlManager"/> instance using the first available <see cref="ManagerElement"/> instance in configuration.
        /// </summary>
        /// <returns>A new <see cref="XmlManager"/> instance, or <c>null</c> if no instances exist.</returns>
        public static XmlManager Create()
        {
            var section = (FigaroSection)ConfigurationManager.GetSection("Figaro");
            if (section == null) throw new ConfigurationErrorsException("Figaro section could not be found.");
            if (section.Managers == null || section.Managers.Count == 0) throw new ConfigurationErrorsException("No XmlManagers could be found in configuration.");
            return Create(section.Managers[0]);
        }

        /// <summary>
        /// Create a new <see cref="XmlManager"/> instance using the named <see cref="ManagerElement"/> instance.
        /// </summary>
        /// <param name="managerName">The named <see cref="ManagerElement"/> to use.</param>
        /// <returns>A new <see cref="XmlManager"/> instance, or <c>null</c> if the instance doesn't exist.</returns>
        public static XmlManager Create(string managerName)
        {
            var section = (FigaroSection)ConfigurationManager.GetSection("Figaro");
            if (section == null) throw new ConfigurationErrorsException("Figaro section could not be found.");
            if (section.Managers == null || section.Managers.Count == 0) throw new ConfigurationErrorsException("No XmlManagers could be found in configuration.");
            foreach (ManagerElement managerElement in section.Managers)
            {
                if (managerElement.Name.Equals(managerName))
                    return Create(managerElement);
            }
            return null;
        }

#if CDS || TDS || HA
        /// <summary>
        /// Create a new <see cref="XmlManager"/> instance using the specified <see cref="ManagerElement"/>.
        /// </summary>
        /// <param name="element">The <see cref="ManagerElement"/> to use.</param>
        /// <returns>A new <see cref="XmlManager"/> instance.</returns>
        public static XmlManager Create(ManagerElement element)
        {
            FigaroEnv env = string.IsNullOrEmpty(element.Env) ? null : EnvFactory.Create(element.Env);
            if (env == null && !string.IsNullOrEmpty(element.Env))
                throw new ConfigurationErrorsException(
                    $"The specified environment configuration instance '{element.Env}' does not exist.");
            var opts = ManagerInitOptions.None;

            if (!string.IsNullOrEmpty(element.Options))
                opts = (ManagerInitOptions)Enum.Parse(typeof(ManagerInitOptions), element.Options, true);
            var mgr = env != null ?
                new XmlManager(env, opts) { ConfigurationName = element.Name } :
                new XmlManager(opts) { ConfigurationName = element.Name };
            var ctype = XmlContainerType.NodeContainer;
            if (!string.IsNullOrEmpty(element.DefaultContainerType))
                ctype = (XmlContainerType)Enum.Parse(typeof(XmlContainerType), element.DefaultContainerType, true);

            mgr.DefaultContainerType = ctype;

            if (element.DefaultPageSize > 0)
                mgr.DefaultPageSize = element.DefaultPageSize;

            if (element.DefaultSequenceIncrement > 0)
                mgr.DefaultSequenceIncrement = element.DefaultSequenceIncrement;

            if (element.DefaultContainerSettings != null)
                mgr.DefaultContainerSettings = ContainerConfigFactory.Create(element.DefaultContainerSettings);

            return mgr;
        }
#else
        /// <summary>
        /// Create a new <see cref="XmlManager"/> instance using the specified <see cref="ManagerElement"/>.
        /// </summary>
        /// <param name="element">The <see cref="ManagerElement"/> to use.</param>
        /// <returns>A new <see cref="XmlManager"/> instance.</returns>
        public static XmlManager Create(ManagerElement element)
        {
            var opts = ManagerInitOptions.None;
            var homePath = string.IsNullOrEmpty(element.Home) ? string.Empty : PathUtility.ResolvePath(element.Home);
            if (!string.IsNullOrEmpty(element.Options))
                opts = (ManagerInitOptions) Enum.Parse(typeof (ManagerInitOptions), element.Options, true);
            if (!string.IsNullOrEmpty(homePath) && !Directory.Exists(homePath))
                Directory.CreateDirectory(homePath);
            var mgr = string.IsNullOrEmpty(homePath) ? 
                new XmlManager(opts) { ConfigurationName = element.Name } :
                new XmlManager(homePath, opts) { ConfigurationName = element.Name };
            var ctype = XmlContainerType.NodeContainer;
            if (!string.IsNullOrEmpty(element.DefaultContainerType))
                ctype = (XmlContainerType) Enum.Parse(typeof (XmlContainerType), element.DefaultContainerType, true);

            mgr.DefaultContainerType = ctype;

            if (element.DefaultPageSize >0)
                mgr.DefaultPageSize = element.DefaultPageSize;
            
            if (element.DefaultSequenceIncrement >0)
                mgr.DefaultSequenceIncrement = element.DefaultSequenceIncrement;

            if (element.DefaultContainerSettings != null)
                mgr.DefaultContainerSettings = ContainerConfigFactory.Create(element.DefaultContainerSettings);

            return mgr;
        }
#endif


#if CDS || TDS || HA
        /// <summary>
        /// Create a new <see cref="XmlManager"/> instance using the specified <see cref="ManagerElement"/>.
        /// </summary>
        /// <param name="managerElement">The <see cref="ManagerElement"/> to use.</param>
        /// <param name="envElement">The <see cref="FigaroEnv"/> configuration instance to assign to the manager. </param>
        /// <returns>A new <see cref="XmlManager"/> instance.</returns>
        public static XmlManager Create(ManagerElement managerElement, FigaroEnvElement envElement)
        {
            FigaroEnv env = envElement == null ? null : EnvFactory.Create(envElement);
            if (env == null && envElement != null)
                throw new ConfigurationErrorsException(
                    $"The specified environment configuration instance '{envElement.Name}' does not exist.");
            var opts = ManagerInitOptions.None;

            if (!string.IsNullOrEmpty(managerElement.Options))
                opts = (ManagerInitOptions)Enum.Parse(typeof(ManagerInitOptions), managerElement.Options, true);
            var mgr = env != null ? 
                new XmlManager(env, opts) { ConfigurationName = managerElement.Name } :
                new XmlManager(opts) { ConfigurationName = managerElement.Name };
            var ctype = XmlContainerType.NodeContainer;
            if (!string.IsNullOrEmpty(managerElement.DefaultContainerType))
                ctype = (XmlContainerType)Enum.Parse(typeof(XmlContainerType), managerElement.DefaultContainerType, true);

            mgr.DefaultContainerType = ctype;

            if (managerElement.DefaultPageSize > 0)
                mgr.DefaultPageSize = managerElement.DefaultPageSize;

            if (managerElement.DefaultSequenceIncrement > 0)
                mgr.DefaultSequenceIncrement = managerElement.DefaultSequenceIncrement;

            if (managerElement.DefaultContainerSettings != null)
                mgr.DefaultContainerSettings = ContainerConfigFactory.Create(managerElement.DefaultContainerSettings);

            return mgr;
        }
#endif
    }
}
