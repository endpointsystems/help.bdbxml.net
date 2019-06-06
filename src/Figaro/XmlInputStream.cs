// ReSharper disable UnusedParameter.Global
using System;
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Figaro    
{
    /// <summary>
    /// A forward-only stream used to read and write XML data.
    /// </summary>
    /// <remarks>
    /// You can obtain an instance of this object using one of
    /// <see cref="XmlManager.CreateLocalFileInputStream(System.String)"/>,
    /// <see cref="XmlManager.CreateMemBufInputStream(System.String,System.String)"/>, or
    /// <see cref="XmlManager.CreateUrlInputStream(string,string)"/>.
    /// <para>
    /// You use instances of this class with <see cref="Container.PutDocument(string,UpdateContext)"/>,
    /// and <see cref="XmlDocument.SetContentAsInputStream(Figaro.XmlInputStream)"/>. 
    /// </para>
    /// <para>
    /// You can manually retrieve the contents of the input stream using XmlInputStream::readBytes and XmlInputStream::curPos.
    /// </para>
    /// </remarks>
    /// <seealso cref="XmlManager.CreateLocalFileInputStream(System.String)"/>
    /// <seealso cref="XmlManager.CreateMemBufInputStream(System.String,System.String)"/>
    /// <seealso cref="XmlManager.CreateUrlInputStream(string,string)"/>
    /// <seealso cref="Container.PutDocument(string,UpdateContext)"/>
    /// <seealso cref="XmlDocument.SetContentAsInputStream(Figaro.XmlInputStream)"/>
    /// <seealso cref="XmlManager"/>
    /// <seealso cref="Container"/>
    /// <seealso cref="XmlDocument"/>
    /// <threadsafety static="false" instance="false"/>
    public sealed class XmlInputStream : IDisposable
    {
        /// <summary>
        /// Prevents a default instance of the XmlInputStream class from being created.
        /// </summary>
        private XmlInputStream() {}
        /// <summary>
        /// Gets the cursor position in the stream.
        /// </summary>
        public uint Position { get; private set; }

        /// <summary>
        /// Reads the specified number of bytes from the input stream and returns those bytes in an array. 
        /// </summary>
        /// <param name="byteCount">The number of bytes to read. If this number is greater than the number of available bytes, then only the 
        /// available bytes will be returned.</param>
        /// <returns>A byte array of the input stream.</returns>
        public byte[] ReadBytes(uint byteCount) {return null; }

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
