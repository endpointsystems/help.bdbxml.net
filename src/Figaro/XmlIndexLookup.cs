// ReSharper disable UnusedParameter.Global
using System;
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Figaro    
{
    /// <summary>
    /// The <see cref="XmlIndexLookup"/> class encapsulates the context within which an
    /// index lookup operation can be performed on a <see cref="Container"/> object.
    /// </summary>
    /// <remarks>
    /// The lookup is performed using an <see cref="XmlIndexLookup"/> object, and a series
    /// of methods of that object that specify how the lookup is to be performed. Using these methods, it is possible to
    /// specify inequality lookups, range lookups, and simple value lookups, as well as the sort order of the results.
    /// <para>
    /// By default, results are returned in the sort order of the index.
    /// <see cref="XmlIndexLookup"/> objects are created
    /// using <see cref="XmlManager.CreateIndexLookup(Figaro.Container,System.String,System.String,System.String,Figaro.XmlValue,Figaro.IndexLookupOperation)"/>.
    /// </para><para>
    /// A constructed, read-only object may be shared among threads for multiple calls
    /// to <see cref="Execute(QueryContext)"/>.
    /// </para>
    /// </remarks>
    /// <threadsafety static="false" instance="false"/>
    /// <seealso cref="XmlManager"/>
    public sealed class XmlIndexLookup : IDisposable
    {
        /// <summary>
        /// Prevents a default instance of the XmlIndexLookup class from being created.
        /// </summary>
        private XmlIndexLookup() {}

        /// <summary>
        /// Gets a value indicating whether or not the object is configured for transactions.
        /// </summary>
        /// <value>Returns <c>true</c> if configured for transactions.</value>
        public bool Transactional { get; private set; }

        /// <summary>
        /// Executes the index lookup operation specified by the configuration
        /// of the <see cref="XmlIndexLookup"/> object without specifying any additional options.
        /// </summary>
        /// <param name="context">The query context to execute under.</param>
        /// <returns>
        /// An <see cref="XmlResults"/> results set.
        /// </returns>
        /// <seealso cref="XmlResults"/>
        public XmlResults Execute(QueryContext context) {return null; }

        /// <summary>
        /// Executes the index lookup operation specified by the configuration
        /// of the <see cref="XmlIndexLookup"/> object.
        /// </summary>
        /// <param name="context">The query context to execute under.</param>
        /// <param name="indexLookup">The index lookup options.</param>
        /// <returns>
        /// An <see cref="XmlResults"/> results set.
        /// </returns>
        /// <seealso cref="XmlResults"/>
        public XmlResults Execute(QueryContext context, IndexLookupOptions indexLookup) { return null; }

        /// <summary>
        /// Transactionally executes the index lookup operation specified by the configuration
        /// of the <see cref="XmlIndexLookup"/> object.
        /// </summary>
        /// <param name="transaction">The transaction to execute with.</param>
        /// <param name="context">The query context to execute under.</param>
        /// <returns>
        /// An <see cref="XmlResults"/> results set.
        /// </returns>
        /// <seealso cref="XmlResults"/>
        public XmlResults Execute(XmlTransaction transaction, QueryContext context) {return null; }
        /// <summary>
        /// Executes the index lookup operation specified by the configuration
        /// of the <see cref="XmlIndexLookup"/> object.
        /// </summary>
        /// <param name="transaction">The transaction to execute with.</param>
        /// <param name="context">The query context to execute under.</param>
        /// <param name="indexLookup">The index lookup options.</param>
        /// <returns>
        /// An <see cref="XmlResults"/> results set.
        /// </returns>
        /// <seealso cref="XmlResults"/>
        public XmlResults Execute(XmlTransaction transaction, QueryContext context, IndexLookupOptions indexLookup) { return null; }
        /// <summary>
        /// Gets the <see cref="IndexingStrategy"/> representation of the current index.
        /// </summary>
        /// <returns>
        /// An <see cref="IndexingStrategy"/> of the index.
        /// </returns>
        /// <seealso cref="IndexingStrategy"/>
        public IndexingStrategy GetIndex()
        {
            return new IndexingStrategy();
        }
        /// <summary>
        /// Sets a new index using <see cref="IndexingStrategy"/>.
        /// </summary>
        /// <param name="index">The <see cref="IndexingStrategy"/> of the new index.</param>
        /// <seealso cref="IndexingStrategy"/>
        public void SetIndex(IndexingStrategy index) { }


        /// <summary>
        /// Gets the value to be used for the upper bound for a range index lookup operation.
        /// </summary>
        /// <value>
        /// The value to be used for the upper bound for a range index lookup operation.
        /// </value>
        /// <remarks>
        /// The high bound must be specified to indicate a range lookup.
        /// </remarks>
        /// <seealso cref="XmlValue"/>
        public XmlValue HighBoundValue { get; private set; }

        /// <summary>
        /// Gets the operation to be used for the upper bound for a range index lookup operation.
        /// </summary>
        /// <value>
        /// The operation to be used for the upper bound for a range index lookup operation.
        /// </value>
        /// <remarks>
        /// The high bound must be specified to indicate a range lookup.
        /// </remarks>
        /// <seealso cref="HighboundOperationLookup"/>
        public HighboundOperationLookup HighBoundOperation { get; private set; }

        /// <summary>
        /// Sets the operation and value to be used for the upper bound for a range index lookup operation.
        /// </summary>
        /// <param name="value">The value to be used for the upper bound. Use of an empty value results in an inequality lookup, rather than a range lookup.</param>
        /// <param name="op">The operation to be used on the upper bound.</param>
        /// <remarks>
        /// The high bound must be specified to indicate a range lookup.
        /// </remarks>
        /// <seealso cref="XmlValue"/>
        /// <seealso cref="HighboundOperationLookup"/>
        public void SetHighBound(XmlValue value, HighboundOperationLookup op) { }

        /// <summary>
        /// Gets the name of the container containing the index.
        /// </summary>
        /// <value>The parent container name.</value>
        public string ParentContainer => string.Empty;

        /// <summary>
        /// Gets the URI of the parent index lookup.
        /// </summary>
        public string ParentUri => string.Empty;

        /// <summary>
        /// Gets the value to be used for the index lookup operation.
        /// </summary>
        /// <value>The value to be used for the index lookup operation.</value>
        /// <remarks>
        /// If the operation is a simple inequality lookup, the lower bound is used as the single value
        /// and operation for the lookup. If the operation is a range lookup, in which an upper bound is
        /// specified, the lower bound is used as the lower boundary value and operation for the lookup.
        /// </remarks>
        /// <seealso cref="XmlValue"/>
        public XmlValue LowBoundValue { get; private set; }

        /// <summary>
        /// Gets the operation to be used for the index lookup operation.
        /// </summary>
        /// <value>The operation to be used for the index lookup operation.</value>
        /// <remarks>
        /// If the operation is a simple inequality lookup, the lower bound is used as the
        /// single value and operation for the lookup. If the operation is a range lookup, in
        /// which an upper bound is specified, the lower bound is used as the lower boundary value and
        /// operation for the lookup.
        /// </remarks>
        /// <seealso cref="IndexLookupOperation"/>
        public IndexLookupOperation LowBoundOperation { get; private set; }

        /// <summary>
        /// Sets the operation and value to be used for the index lookup operation.
        /// </summary>
        /// <param name="value">The value to be used for the lower bound. An empty value is specified using an
        /// uninitialized <see cref="XmlValue"/> object.</param>
        /// <param name="op">The operation to be performed.</param>
        /// <remarks>
        /// If the operation is a simple inequality lookup, the lower bound is used as the
        /// single value and operation for the lookup. If the operation is a range lookup, in
        /// which an upper bound is specified, the lower bound is used as the lower boundary
        /// value and operation for the lookup.
        /// </remarks>
        /// <seealso cref="XmlValue"/>
        /// <seealso cref="IndexLookupOperation"/>
        public void SetLowBound(XmlValue value, IndexLookupOperation op)
        {                
        }

        /// <summary>
        /// Gets The namespace of the node to be used.
        /// </summary>
        /// <value>The namespace of the node to be used.</value>
        public string Namespace { get; private set; }

        /// <summary>
        /// Gets the name of the element or attribute node to be used.
        /// </summary>
        /// <value>The name of the element or attribute node to be used.</value>
        public string NodeName { get; private set; }

        /// <summary>
        /// Sets the name of the node to be used along with the indexing strategy for the index lookup operation.
        /// </summary>
        /// <param name="nodeNamespace">The namespace of the node to be used.</param>
        /// <param name="nodeName">The name of the element or attribute node to be used.</param>
        /// <remarks>
        /// The default namespace is selected by passing an empty string for the namespace.
        /// </remarks>
        public void SetNode(string nodeNamespace, string nodeName) { }

        /// <summary>
        /// Gets the name of the element or attribute node to be used.
        /// </summary>
        /// <value>The name of the element or attribute node to be used.</value>
        public string ParentName { get; private set; }

        /// <summary>
        /// Sets the name of the parent node to be used for an edge index lookup operation.
        /// </summary>
        /// <param name="parentNamespace">The namespace of the parent node to be used.</param>
        /// <param name="parentNodeName">The name of the element or attribute node to be used.</param>
        /// <remarks>
        /// If the index is not an edge index, this configuration is ignored.The default namespace
        /// is used by passing an empty string for the namespace.
        /// </remarks>
        public void SetParent(string parentNamespace, string parentNodeName) { }


        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
