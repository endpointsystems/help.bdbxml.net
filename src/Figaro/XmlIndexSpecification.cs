
using System;

namespace Figaro    
{
    /// <summary>
    /// Encapsulates the indexing specification of a container.
    /// </summary>
    /// <remarks>
    /// An indexing specification can be retrieved with
    /// the <see cref="Container.GetIndexSpecification"/> method, and modified
    /// using the <see cref="Container.SetIndexSpecification(XmlIndexSpecification,UpdateContext)"/> method.
    /// <para>
    /// The <see cref="XmlIndexSpecification"/> class provides an interface
    /// for manipulating the indexing specification through the
    /// XmlIndexSpecification.addIndex, XmlIndexSpecification.deleteIndex,
    /// and XmlIndexSpecification.replaceIndex methods. The class interface
    /// also provides the XmlIndexSpecification.next and
    /// XmlIndexSpecification.reset methods for iterating through
    /// the specified indices. The XmlIndexSpecification.find method can
    /// be used to search for the indexing strategy for a known node.
    /// </para><para>
    /// Finally, you can set a default index specification for a container
    /// using XmlIndexSpecification.addDefaultIndex. You can replace and delete
    /// the default index using XmlIndexSpecification.replaceDefaultIndex
    /// and XmlIndexSpecification.deleteDefaultIndex.
    /// </para><para>
    /// Note that adding an index to a container results in re-indexing all of the
    /// documents in that container, which can take a very long time. It is good practice to
    /// design an application to add useful indices before populating a container.
    /// </para><para>
    /// The class is implemented using a handle-body idiom. When a handle is copied, both handles
    /// maintain a reference to the same body.
    /// </para>
    /// </remarks>
    /// <threadsafety static="false" instance="false"/>
    public sealed class XmlIndexSpecification : IDisposable
    {
        /// <summary>
        /// Prevents a default instance of the XmlIndexSpecification class from being created.
        /// </summary>
        private XmlIndexSpecification() {}


        /// <summary>
        /// Gets or sets a value indicating whether the auto-indexing state which will affect the container when set using <see cref="Container.SetIndexSpecification(Figaro.XmlIndexSpecification,Figaro.UpdateContext)"/>.
        /// </summary>
        /// <remarks>
        /// <para>If the value on the container is <c>true</c> (the default for newly-created containers) then indexes are added automatically 
        /// for leaf elements and attributes. The indexes added are "node-equality-string" and "node-equality-double" for 
        /// elements and attributes. If auto-indexing is not desired it should be disabled using this interface upon container creation. 
        /// </para>
        /// <para>
        /// Auto-indexing is recognized by insertion of new documents as well as updates of existing documents, including modification via XQuery Update. 
        /// The auto-indexing state is persistent and will remain stable across container close/re-open operations. Indexes added via 
        /// auto-indexing are normal indexes and can be removed using the normal mechanisms.</para>
        /// <para>
        /// A significant implication of auto-indexing is that any operation that may add an 
        /// index (e.g. <see cref="Container.PutDocument(string,Figaro.UpdateContext)"/>) can have the side effect of 
        /// re-indexing the entire container. For this reason auto-indexing is not recommended for containers 
        /// of heterogeneous documents and that it be disabled once a representative set of documents has been inserted.</para>
        /// </remarks>
        public bool AutoIndexing { get; set; }

        /// <summary>
        /// Adds an indexing strategy to set for the identified node.
        /// </summary>
        /// <param name="index">The index to add to the specification.</param>
        /// <seealso cref="XmlIndex"/>
        public void AddIndex(XmlIndex index) { }

        /// <summary>
        /// Adds an indexing strategy to the default index specification. That is, the index provided
        /// on this method is applied to all nodes in a container, except for those for which an
        /// explicit index is already declared.
        /// </summary>
        /// <param name="index">The index to specify as the default index.</param>
        /// <remarks>
        /// For more information on specifying indexing strategies,
        /// see <see cref="XmlIndexSpecification.AddIndex"/>.
        /// </remarks>
        public void AddDefaultIndex(string index) { }
        /// <summary>
        /// Adds an indexing strategy to the default index specification. That is, the index provided
        /// on this method is applied to all nodes in a container, except for those for which an
        /// explicit index is already declared.
        /// </summary>
        /// <param name="index">The index to specify as the default index.</param>
        /// <remarks>
        /// For more information on specifying indexing strategies,
        /// see <see cref="XmlIndexSpecification.AddIndex"/>.
        /// </remarks>
        public void AddDefaultIndex(IndexingStrategy index) { }
        /// <summary>
        /// Deletes indexing strategies for a named document or metadata node.
        /// </summary>
        /// <param name="index">The named index to remove.</param>
        /// <remarks>
        /// To delete an index set for metadata, specify the URI and name used when the
        /// metadata was added to the document.
        /// </remarks>
        /// <seealso cref="XmlIndex"/>
        public void DeleteIndex(XmlIndex index) { }
        /// <summary>
        /// Deletes an index for a named document or metadata node.
        /// </summary>
        /// <param name="nodeNamespace">The namespace of the node for which you want the index deleted. The default
        /// namespace is selected by passing an empty string for the namespace.</param>
        /// <param name="nodeName">The name of the indexed element or attribute node.</param>
        /// <param name="index">The indexing strategy of the index to remove.</param>
        /// <remarks>
        /// To delete an index set for metadata, specify the URI and name used when the
        /// metadata was added to the document.
        /// </remarks>
        public void DeleteIndex(string nodeNamespace, string nodeName, string index) { }

        /// <summary>
        /// Delete the identified index from the default index specification.
        /// </summary>
        /// <param name="index">The string representation of the default index to remove.</param>
        /// <remarks>
        /// You can add additional indices to the default index specification
        /// using <see cref="AddDefaultIndex(string)"/>.
        /// </remarks>
        public void DeleteDefaultIndex(string index) { }

        /// <summary>
        /// Returns the indexing strategies for a named document or metadata node.
        /// </summary>
        /// <param name="index">The named index to find.</param>
        /// <returns>
        /// This method returns <c>true</c> if an index for the node is found; otherwise, it returns <c>false</c>.
        /// </returns>
        public bool Find(XmlIndex index) 
        {
            return true;
        }

        /// <summary>
        /// Retrieves the default index.
        /// </summary>
        /// <returns>
        /// The string representation of the default index.
        /// </returns>
        /// <remarks>
        /// The default index is the index used by all nodes in the document in the absence of any other index.
        /// </remarks>
        public string GetDefaultIndex()
        {
            return string.Empty;
        }

        /// <summary>
        /// Obtains the next index in the <see cref="XmlIndexSpecification"/>.
        /// </summary>
        /// <returns>
        /// Returns an <see cref="XmlIndex"/> containing the index information. If
        /// no additional exists, this method will return a <c>null</c> value.
        /// </returns>
        /// <seealso cref="XmlIndex"/>
        public XmlIndex Next()
        {
            return new XmlIndex();
        }

        /// <summary>
        /// Replaces the indexing strategies for a typed document for metadata node.
        /// </summary>
        /// <param name="nodeNamespace">The namespace of the node.</param>
        /// <param name="nodeName">The name of the node whose index is being replaced.</param>
        /// <param name="strategy">The indexing strategy containing the type information.</param>
        /// <remarks>
        /// All existing indexing strategies for that node are deleted, and the indexing strategy
        /// identified by this method is set for the node.
        /// </remarks>
        public void ReplaceIndex(string nodeNamespace, string nodeName, IndexingStrategy strategy) { }
        /// <summary>
        /// Replaces the indexing strategies for a named document or metadata node.
        /// </summary>
        /// <param name="index">The index to replace.</param>
        /// <remarks>
        /// All existing indexing strategies for that node are deleted, and the indexing strategy
        /// identified by this method is set for the node.
        /// </remarks>
        /// <seealso cref="XmlIndex"/>
        public void ReplaceIndex(XmlIndex index) { }

        /// <summary>
        /// Replace the default index.
        /// </summary>
        /// <param name="index">The new default index.</param>
        public void ReplaceDefaultIndex(string index)
        {
        }

        /// <summary>
        /// Resets the index specification iterator to the beginning of the index list.
        /// </summary>
        /// <remarks>
        /// Use <see cref="XmlIndexSpecification.Next"/> to iterate
        /// through the indices contained in the index specification.
        /// </remarks>
        public void Reset() { }


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
