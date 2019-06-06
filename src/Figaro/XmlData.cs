namespace Figaro    
{
    using System;
    /// <summary>
    /// A wrapper to the <c>Dbt</c> object found in Berkeley Db. This class represents untyped XML data and
    /// exists in support of <see cref="XmlDocument"/> and
    /// <see cref="XmlValue"/>.
    /// </summary>
    /// <remarks>
    /// For access to the underlying data, see the <see cref="XmlData.ToString"/> method.
    /// </remarks>
    /// <threadsafety static="false" instance="false"/>
    /// <seealso cref="XmlValue"/>
    /// <seealso cref="XmlDocument"/>
    public sealed class XmlData : IDisposable
    {
        /// <summary>
        /// Prevents a default instance of the XmlData class from being created.
        /// </summary>
        private XmlData() {}

        /// <summary>
        /// Return the untyped XML data as a <see cref="System.String"/> value.
        /// </summary>
        /// <returns>Untyped XML data.</returns>
        public override string ToString()
        {
            return string.Empty;
        }

        /// <summary>
        /// Gets the length of the untyped XML data buffer.
        /// </summary>
        /// <value>The length of the data buffer.</value>
        public uint Length { get; private set; }

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
        }

        #endregion
    }
}
