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

using System.Configuration;

namespace Figaro.Configuration.Factory
{
    /// <summary>
    /// Manages the creation of <see cref="FigaroSection"/> objects from configuration files.
    /// </summary>
	public class SectionFactory
	{
        /// <summary>
        /// Creates a new <see cref="FigaroSection"/> instance from the
        /// specified configuration file.
        /// </summary>
        /// <remarks>
        /// This method will attempt to read a configuration file and extract
        /// the <see cref="FigaroSection"/> with  the specified section name 
        /// from  within the configuration file. If the configuration section
        /// does not exist or is not specified, the  method will return a 
        /// <c>null</c> value indicating the section was not found.
        /// </remarks>
        /// <param name="configurationFilePath">The path and file name of the
        /// configuration file.</param>
        /// <param name="sectionName">The name of the <see cref="FigaroSection"/> instance.</param>
        /// <returns>A <see cref="FigaroSection"/> instance from configuration,
        /// or <c>null</c> if the section does not exist.</returns>
        public static FigaroSection Create(string configurationFilePath, string sectionName = "Figaro")
        {
            var map = new ExeConfigurationFileMap { ExeConfigFilename = configurationFilePath };
            var cfg = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);            
            var section = cfg.GetSection(sectionName);
            return section as FigaroSection;
        }

        /// <summary>
        /// Create a new <see cref="FigaroSection"/> instance from the existing
        /// configuration environment.
        /// </summary>
        /// <remarks>
        /// If the <see cref="FigaroSection"/> is configured, then a
        /// </remarks>
        /// <returns>The <see cref="FigaroSection"/> instance from
        /// configuration, or <c>null</c> if the section does not exist.
        /// </returns>
        public static FigaroSection Create()
        {
            var cfg = ConfigurationManager.GetSection("Figaro");
            return cfg as FigaroSection;
        }

	}
}
