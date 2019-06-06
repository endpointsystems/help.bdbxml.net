namespace Figaro    
{
    /// <summary>
    /// Performs logging flag operations on the Figaro.
    /// </summary>
    public static class LogConfiguration
    {
        /// <summary>
        /// Enables or disables Figaro log level features.
        /// </summary>
        /// <param name="level">The logging configuration level to set.</param>
        /// <param name="enable">If <c>true</c>, enables the logging configuration level.</param>
        /// <remarks>
        /// Figaro can be configured to generate a stream of messages to help application debugging. The
        /// messages are categorized by subsystem, and by importance. The messages are sent to the output stream
        /// that is configured in the Figaro environment associated with the
        /// <see cref="XmlManager"/> generating the message. The output is sent to
        /// <c>std.cerr</c> if no environment is associated with the
        /// <see cref="XmlManager"/>.
        /// </remarks>
        public static void SetLogLevel(LogConfigurationLevel level, bool enable) { }
        /// <summary>
        /// Enables or disables Figaro log categories.
        /// </summary>
        /// <param name="category">The log category to configure.</param>
        /// <param name="enable">If <c>true</c>, enables the logging configuration category.</param>
        /// <remarks>
        /// Figaro can be configured to generate a stream of messages to help application debugging. The messages
        /// are categorized by subsystem, and by importance. The messages are sent to the output stream that is
        /// configured in the Figaro environment associated with the <see cref="XmlManager"/> generating
        /// the message. The output is sent to <c>std.cerr</c> if no environment is associated with
        /// the <see cref="XmlManager"/>.
        /// </remarks>
        public static void SetCategory(LogConfigurationCategory category, bool enable)
        {
        }

#pragma warning disable 649
        /// <summary>
        /// The environment version.
        /// </summary>
        private static EnvVersion _version;
#pragma warning restore 649
        /// <summary>Gets the underlying Figaro release version.</summary><value>The release version.</value>
        public static EnvVersion EnvironmentVersion
        {
            get { return _version; }
        }
    }
}
