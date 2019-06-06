using System;
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Figaro    
{
    /// <summary>
    /// Provides an iterator over an <see cref="XmlDocument"/>'s metadata. Metadata is set
    /// on a document with <see cref="XmlDocument.SetMetadata(string,string,XmlData)"/>. You can also
    /// use <see cref="XmlDocument.GetMetadataXmlData(System.String,System.String)"/> or
    /// <see cref="XmlDocument.GetMetadataXmlValue(System.String,System.String)"/> to return
    /// a specific metadata item.
    /// </summary>
    /// <remarks>
    /// This object is instantiated using <see cref="XmlDocument.GetMetadataIterator"/>.
    /// </remarks>
    /// <threadsafety static="false" instance="false"/>
    /// <seealso cref="XmlDocument.GetMetadataIterator"/>
    /// <seealso cref="XmlDocument.SetMetadata(string,string,XmlData)"/>
    /// <seealso cref="XmlDocument.GetMetadataXmlData(System.String,System.String)"/>
    /// <seealso cref="XmlDocument.GetMetadataXmlValue(System.String,System.String)"/>
    /// <seealso cref="XmlDocument"/>
    public sealed class XmlMetadataIterator : IDisposable
    {
        /// <summary>
        /// Prevents a default instance of the XmlMetadataIterator class from being created.
        /// </summary>
        private XmlMetadataIterator() {}

        /// <summary>
        /// Gets the URI of the metadata item at the current position.
        /// </summary>
        /// <value>Gets the URI of the metadata item at the current position.</value>
        public string Uri { get; private set; }
        /// <summary>
        /// Gets the name of the metadata item at the current position.
        /// </summary>
        /// <value>Gets the name of the metadata item at the current position.</value>
        public string Name { get; private set; }
        /// <summary>
        /// Gets the value of the metadata item at the current position.
        /// </summary>
        /// <value>Gets the value of the metadata item at the current position.</value>
        public XmlValue Value { get; private set; }

        /// <summary>
        /// Navigates to the next metadata item.
        /// </summary>
        /// <returns>
        /// Returns <c>true</c> if it successfully moved to the next item, otherwise it returns <c>false</c>.
        /// </returns>
        public bool Next() 
        {
            return true;
        }
        /// <summary>
        /// Sets the iterator to the beginning of the <see cref="XmlDocument"/>'s metadata list.
        /// </summary>
        public void Reset() { }


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
