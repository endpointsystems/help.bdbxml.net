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
using System.Linq;

namespace Figaro.Configuration
{
    /// <summary>
    /// Factory object for creating new <see cref="ContainerConfig"/> instances from configuration.
    /// </summary>
    public class ContainerConfigFactory
    {
        /// <summary>
        /// Creates a new <see cref="ContainerConfig"/> instance using the <see cref="DefaultContainerElement"/> set at the section root level.
        /// </summary>
        /// <returns>A new <see cref="ContainerConfig"/> instance, or <c>null</c> if it doesn't exist.</returns>
        public static ContainerConfig Create()
        {
            FigaroSection section;
            try
            {
                section = (FigaroSection)ConfigurationManager.GetSection("Figaro");
            }
            catch (Exception)
            {
                return null;
            }
            return section?.DefaultContainerSettings == null ? null : Create(section.DefaultContainerSettings);
        }

        /// <summary>
        /// Creates a new <see cref="ContainerConfig"/> instance from configuration.
        /// </summary>
        /// <param name="element">The configuration element.</param>
        /// <returns>A new <see cref="ContainerConfig"/> instance.</returns>
        public static ContainerConfig Create(DefaultContainerElement element)
        {
            var cfg = new ContainerConfig
                          {
                              ConfigurationName = "Default Container",
                              AllowCreate = element.AllowCreate,
                              AllowValidation = element.AllowValidation,
                              Checksum = element.Checksum,
                              CompressionEnabled = element.Compression,
                              ExclusiveCreate = element.ExclusiveCreate,
                              ReadOnly = element.ReadOnly,
#if CDS || TDS || HA
                              Threaded = element.Threaded,
                              NoMMap = element.NoMMap,
                              Encrypted = element.Encrypted,
#elif TDS || HA
                              MultiVersion = element.Multiversion,
                              ReadUncommitted = element.ReadUncommitted,
                              Transactional = element.Transactional,
                              TransactionNotDurable = element.TransactionNotDurable
#endif
                          };
#if TDS || HA
#endif

            if (!string.IsNullOrEmpty(element.ContainerType))
                cfg.ContainerType =
                    (XmlContainerType) Enum.Parse(typeof (XmlContainerType), element.ContainerType, true);
            
            if (!string.IsNullOrEmpty(element.IndexNodes))
                cfg.IndexNodes = (ConfigurationState)Enum.Parse(typeof(ConfigurationState),element.IndexNodes,true);
            
            if (element.PageSize > 0)
                cfg.PageSize = element.PageSize;

            if (element.SequenceIncrement >0)
                cfg.SequenceIncrement = element.SequenceIncrement;

            if (!string.IsNullOrEmpty(element.Statistics))
                cfg.Statistics = (ConfigurationState)Enum.Parse(typeof (ConfigurationState), element.Statistics, true);
            
            return cfg;
        }

        /// <summary>
        /// Creates a new <see cref="ContainerConfig"/> instance from default container settings 
        /// specified in the named <see cref="ManagerElement"/>.
        /// </summary>
        /// <param name="managerName">The named <see cref="ManagerElement"/> instance.</param>
        /// <returns>A new <see cref="ContainerConfig"/> instance, or <c>null</c> if no default container settings 
        /// exist for the manager.</returns>
        public static ContainerConfig Create(string managerName)
        {
            if (string.IsNullOrEmpty(managerName)) return null;
            FigaroSection section;
            try
            {
                section = (FigaroSection)ConfigurationManager.GetSection("Figaro");
            }
            catch (Exception)
            {
                return null;
            }
            if (section == null) return null;

            if (section.Managers == null || section.Managers.Count == 0) return null;

           var managerElement = 
               section.Managers.Cast<ManagerElement>().FirstOrDefault(manager => manager.Name.Equals(managerName));
            if (managerElement == null) return null;

            // if a default container setting exists for the manager, use it first.
            // if a default container setting exists for the section, use it second.
            // if neither exist, return null.
            if (managerElement.DefaultContainerSettings != null)
            {
                return Create(managerElement.DefaultContainerSettings);
            }

            return section.DefaultContainerSettings != null ? Create(section.DefaultContainerSettings) : null;
        }

        /// <summary>
        /// Creates a new <see cref="ContainerConfig"/> instance from the <see cref="ManagerElement"/> default container settings.
        /// </summary>
        /// <param name="managerName">The named <see cref="ManagerElement"/> instance.</param>
        /// <returns>A new <see cref="ContainerConfig"/> instance, or <c>null</c> if no instance exists.</returns>
        public static ContainerConfig CreateDefault(string managerName)
        {
            if (string.IsNullOrEmpty(managerName)) return null;
            FigaroSection section;
            try
            {
                section = (FigaroSection)ConfigurationManager.GetSection("Figaro");
            }
            catch (Exception)
            {
                return null;
            }
            if (section == null) return null;

            if (section.Managers == null || section.Managers.Count == 0) return null;

            ManagerElement managerElement = 
                section.Managers.Cast<ManagerElement>().FirstOrDefault(manager => manager.Name.Equals(managerName));
            if (managerElement == null) return null;
            return managerElement.DefaultContainerSettings == null
                ? null
                : Create(managerElement.DefaultContainerSettings);
        }

        /// <summary>
        /// Creates a new <see cref="ContainerConfig"/> instance from configuration.
        /// </summary>
        /// <param name="managerName">The named <see cref="ManagerElement"/> instance.</param>
        /// <param name="containerName">The named Container element.</param>
        /// <returns>A new <see cref="ContainerConfig"/> instance, or <c>null</c> if it doesn't exist.</returns>
        public static ContainerConfig Create(string managerName , string containerName)
        {
            if (string.IsNullOrEmpty(managerName)) return null;
            FigaroSection section;
            try
            {
                section = (FigaroSection)ConfigurationManager.GetSection("Figaro");
            }
            catch (Exception)
            {
                return null;
            }
            if (section == null) return null;

            if (section.Managers == null || section.Managers.Count == 0 ) return null;

            var managerElement = 
                section.Managers.Cast<ManagerElement>().FirstOrDefault(manager => manager.Name.Equals(managerName));
            if (managerElement == null) return null;
            if (managerElement.Containers == null || managerElement.Containers.Count == 0) return null;
            var containerElement = 
                managerElement.Containers.Cast<ContainerElement>().FirstOrDefault(
                element => element.Name.Equals(containerName));
            
            return containerElement == null ? null : Create(containerElement);
        }

        /// <summary>
        /// Creates a new <see cref="ContainerConfig"/> instance from configuration.
        /// </summary>
        /// <param name="element">The configuration element.</param>
        /// <returns>A new <see cref="ContainerConfig"/> instance.</returns>
        public static ContainerConfig Create(ContainerElement element)
        {
            var cfg = new ContainerConfig
            {                
                ConfigurationName = element.Name,
                Path = element.Path,
                AllowCreate = element.AllowCreate,
                AllowValidation = element.AllowValidation,
                Checksum = element.Checksum,
                CompressionEnabled = element.Compression,
                ExclusiveCreate = element.ExclusiveCreate,
                ReadOnly = element.ReadOnly,
#if CDS || TDS || HA
                NoMMap = element.NoMMap,
                Threaded = element.Threaded,
                Encrypted = element.Encrypted,
#elif TDS || HA
                MultiVersion = element.Multiversion,
                ReadUncommitted = element.ReadUncommitted,
                Transactional = element.Transactional,
                TransactionNotDurable = element.TransactionNotDurable
#endif
            };
#if TDS || HA
#endif

            if (!string.IsNullOrEmpty(element.ContainerType))
                cfg.ContainerType =
                    (XmlContainerType)Enum.Parse(typeof(XmlContainerType), element.ContainerType, true);

            if (!string.IsNullOrEmpty(element.IndexNodes))
                cfg.IndexNodes = (ConfigurationState)Enum.Parse(typeof(ConfigurationState), element.IndexNodes, true);

            if (element.PageSize > 0)
                cfg.PageSize = element.PageSize;

            if (element.SequenceIncrement > 0)
                cfg.SequenceIncrement = element.SequenceIncrement;

            if (!string.IsNullOrEmpty(element.Statistics))
                cfg.Statistics = (ConfigurationState)Enum.Parse(typeof(ConfigurationState), element.Statistics, true);

            return cfg;
        }
    }
}
