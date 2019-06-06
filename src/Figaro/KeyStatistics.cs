
// ReSharper disable UnusedAutoPropertyAccessor.Local
namespace Figaro    
{
    using System;
    /// <summary>
    /// Encapsulates statistical information about the number of keys in existence for a given index.
    /// </summary>
    /// <remarks>
    /// Statistics are available for the total number of keys currently in use by the index, as well as the total number
    /// of unique keys in use by the index. Use <see cref="KeyStatistics.IndexedKeys"/>
    /// and <see cref="KeyStatistics.UniqueKeys"/>, respectively, to retrieve these values.
    /// <para>
    /// Be aware that the number the number of keys maintained for an index is a function of the number and size
    /// of documents stored in the container, and of the type of index being examined.
    /// </para><para>
    /// 		<see cref="KeyStatistics"/> objects are instantiated
    /// by <see cref="Container.LookupStatistics(string,string,string,string,string,XmlValue)"/>.
    /// </para>
    /// </remarks>
    /// <threadsafety static="true" instance="true"/>
    public sealed class KeyStatistics : IDisposable
    {
        /// <summary>
        /// Prevents a default instance of the KeyStatistics class from being created.
        /// </summary>
        private KeyStatistics() {}

        /// <summary>
        /// Gets the total number of keys contained by the index for which statistics are being reported.
        /// </summary>
        /// <value>The total key count in the reported index.</value>
        public double IndexedKeys { get; private set; }

        /// <summary>
        /// Gets the number of unique keys contained in the index for which statistics are being reported. There
        /// are likely to be many more keys than unique keys in the index because a given key can appear multiple times,
        /// once for each document feature on each document that it is referencing.
        /// </summary>
        /// <value>The total unique key count for the reported index.</value>
        public double UniqueKeys { get; private set; }

        /// <summary>
        /// Gets the size of the key value in the index for which statistics are being reported.
        /// </summary>
        /// <value>The size of the key value for the reported index.</value>
        public double SumKeyValueSize { get; private set; }


        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">If disposing equals <c>true</c>, the method has been called directly or 
        /// indirectly by a user's code. Managed and unmanaged resources can be disposed. If disposing equals 
        /// <c>false</c>, the method has been called by the runtime from inside the 
        /// finalizer and you should not reference other objects. Only unmanaged resources can be disposed. </param>
        protected void Dispose(bool disposing)
        {

        }
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
