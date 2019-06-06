// ReSharper disable UnusedParameter.Global
//using Figaro.Annotations;
// ReSharper disable UnusedParameter.Local
// Copyright (C) Endpoint Systems.  All rights reserved.




namespace Figaro    //namespace Figaro    
{
    using System;

    /// <summary>
    /// The <see cref="Container"/>  class encapsulates a document container and its 
    /// related indices and statistics. 
    /// </summary>
    /// <remarks>
    /// <para>
    /// <see cref="Container"/> exposes methods for managing (putting and deleting) 
    /// <see cref="XmlDocument"/> objects, managing indices, and retrieving container statistics.
    /// </para><para>
    /// If the <see cref="Container"/> has never before been opened, 
    /// use <see cref="XmlManager.CreateContainer(string)"/> to instantiate 
    /// a <see cref="Container"/>  object. If the container already exists, use 
    /// <see cref="XmlManager.OpenContainer(string)"/> 
    /// instead. Containers are always opened until the last referencing handle is destroyed.
    /// </para><para>
    /// You can delete containers using <see cref="XmlManager.RemoveContainer(string)"/> and 
    /// rename containers using <see cref="XmlManager.RenameContainer(string,string)"/>.
    /// </para><para>
    /// The class is implemented using a handle-body idiom. When a handle is copied both handles maintain a 
    /// reference to the same body.
    /// </para>
    /// </remarks>
    /// <threadsafety static="true" instance="true"/>
    /// <seealso cref="FigaroEnv"/>
    /// <seealso cref="XmlManager"/>
    /// <seealso cref="XmlDocument"/>
    public sealed class Container : IDisposable
    {
        /// <summary>
        /// Gets a value indicating whether the container was configured to use compression.
        /// </summary>
        public bool CompressionEnabled { get; private set; }

        /// <summary>
        /// Gets the flags used to open the <see cref="Container"/>.
        /// </summary>
        /// <value>
        /// The <see cref="ContainerConfig"/> used to open the container.
        /// </value>
        /// <seealso cref="ContainerConfig"/>
        public ContainerConfig Settings { get; private set; }

        /// <summary>Gets a value indicating whether the container is configured to create node indices.</summary>
        /// <value>The boolean operator used to determine if the <see cref="Container"/> is configured to create node indices.</value>
        public bool IndexNodes { get; private set; }

        /// <summary>
        /// Gets the state of the container.
        /// </summary>
        public ContainerState ContainerState => ContainerState.Closed;

        /// <summary>Gets the container name.</summary>
        /// <value>The container name.</value>
        public string Name { get; private set; }

        /// <summary>Gets the container's storage type.</summary>
        /// <value>The container's storage type.</value>
        /// <seealso cref="XmlContainerType"/>
        public XmlContainerType ContainerType { get; private set; }

        /// <summary>Gets the page size for the container.</summary>
        /// <value>The page size for the container.</value>
        public int PageSize { get; private set; }

        /// <summary>Initializes a new instance of the Container class.</summary>
        /// <remarks>When a handle is copied, both handles maintain a reference to the same body.</remarks>
        /// <param name="container">The container to copy.</param>
        public Container(Container container)
        {
        }

        /// <summary>
        /// Adds a new alias to the list maintained by the <see cref="XmlManager"/>
        /// </summary>
        /// <remarks>The new alias can then be used as a parameter to the <c>collection()</c> function in an XQuery expression.
        /// </remarks>
        /// <param name="alias">The new alias to be added.</param>
        /// <returns>Returns <c>true</c> if the alias is successfully added. If the alias is already used by the 
        /// containing <see cref="XmlManager"/> object, <c>false</c> is returned.
        /// </returns>
        public bool AddAlias(string alias)
        {
            return true;
        }

        /// <summary>
        /// Adds an index of the specified type for the named document node.
        /// </summary>
        /// <param name="nodeNamespace">The namespace of the node to be indexed. The default is selected by passing an empty string.</param>
        /// <param name="nodeName">The name of the attribute or node to be indexed.</param>
        /// <param name="index">
        /// A comma-separated list of strings representing the indexing strategy. See @using-indices.md for more details.
        /// </param>
        /// <param name="context">The update context to use for index insertion.</param>
        public void AddIndex(string nodeNamespace, string nodeName, string index, UpdateContext context)
        {
        }

        /// <summary>
        /// Adds an index of the specified type for the named document node.
        /// </summary>
        /// <param name="nodeNamespace">The namespace of the node to be indexed. The default is selected by passing an empty string.</param>
        /// <param name="nodeName">The name of the attribute or node to be indexed.</param>
        /// <param name="indexStrategy">
        /// An <see cref="IndexingStrategy"/> object used for streamlining the index creation process. See @using-indices.md for more details.
        /// </param>
        /// <param name="context">The update context to use for index insertion.</param>
        /// <seealso cref="IndexingStrategy"/>
        public void AddIndex(string nodeNamespace, string nodeName, IndexingStrategy indexStrategy, UpdateContext context)
        {
        }

        /// <summary>Add a default index to the container.</summary>
        /// <remarks>
        /// This method is for convenience -- see <see cref="XmlIndexSpecification.AddDefaultIndex(string)"/> for more information.
        /// </remarks>
        /// <param name="index">A comma-separated list of strings that represent the indexing strategy.</param>
        /// <param name="context">
        /// The <see cref="UpdateContext"/> to use for the index operation.
        /// </param>
        /// <seealso cref="UpdateContext"/>
        public void AddDefaultIndex(string index, UpdateContext context)
        {
        }

        /// <summary>
        /// Delete the document with the given name.
        /// </summary>
        /// <param name="name">
        /// The file name of the <see cref="XmlDocument"/> to be deleted from the 
        /// container. The name to be deleted is extracted from this parameter.
        /// </param>
        /// <param name="context">
        /// The <see cref="UpdateContext"/> object to use for this deletion.
        /// </param>
        /// <seealso cref="XmlDocument"/>
        /// <seealso cref="UpdateContext"/>
        public void DeleteDocument(string name, UpdateContext context)
        {
        }

        /// <summary>
        /// Removes the specified <see cref="XmlDocument"/> from 
        /// the <see cref="Container"/>.
        /// </summary>
        /// <param name="document">
        /// The <see cref="XmlDocument"/> to be deleted from the container. The name 
        /// to be deleted is extracted from this parameter.
        /// </param>
        /// <param name="context">
        /// The <see cref="UpdateContext"/> object to use for this deletion.
        /// </param>
        public void DeleteDocument(XmlDocument document, UpdateContext context) { }


        /// <summary>
        /// Deletes an index of the specified type for the named document node.
        /// </summary>
        /// <remarks>
        /// This method is for convenience -- 
        /// see <see cref="XmlIndexSpecification.DeleteIndex(XmlIndex)"/> for more information.
        /// </remarks>
        /// <param name="nodeNamespace">
        /// The namespace of the node to be indexed. The default namespace is selected by passing an empty string for the namespace.
        /// </param>
        /// <param name="nodeName">The name of the element or attribute node to be indexed.</param>
        /// <param name="index">A comma-separated list of strings that represent the indexing strategy.</param>
        /// <param name="context">The <see cref="UpdateContext"/> to use for this operation.</param>
        public void DeleteIndex(string nodeNamespace, string nodeName, string index, UpdateContext context) { }

        /// <summary>
        /// Deletes the default index for the container.
        /// </summary>
        /// <remarks>
        /// This method is for convenience - see 
        /// <see cref="XmlIndexSpecification.DeleteDefaultIndex"/> for more information.
        /// </remarks>
        /// <param name="index">A comma-separated list of strings that represent the indexing strategy.</param>
        /// <param name="context">The <see cref="UpdateContext"/> to use for this operation.</param>        
        public void DeleteDefaultIndex(string index, UpdateContext context) { }

        /// <summary>
        /// Verifies the existence of a document in the container with the specified name.
        /// </summary>
        /// <param name="docName">The name of the document to look for within the container.</param>
        /// <returns>Returns <c>true</c> if an entry with the file name <paramref name="docName"/> exists in the container; otherwise, <c>false</c>.</returns>
        public bool DocumentExists(string docName) { return false; }

        /// <summary>
        /// Return all of the documents in the container in a lazily 
        /// evaluated <see cref="XmlResults"/> set.
        /// </summary>
        /// <returns>All documents from the <see cref="Container"/> in a lazily 
        /// evaluated <see cref="XmlResults"/> set.</returns>
        /// <seealso cref="XmlResults"/>
        public XmlResults GetAllDocuments()
        {
            return null;
        }

        /// <summary>Return all of the documents in the container in a lazily evaluated <see cref="XmlResults"/> set.</summary>
        /// <param name="getAllDocumentOptions">The <see cref="GetAllDocumentOptions"/> parameter.</param>
        /// <returns>All documents from the <see cref="Container"/> in a lazily evaluated <see cref="XmlResults"/> set.</returns>
        /// <seealso cref="GetAllDocumentOptions"/>
        /// <seealso cref="XmlResults"/>
        public XmlResults GetAllDocuments(GetAllDocumentOptions getAllDocumentOptions)
        {
            return null;
        }

        /// <summary>
        /// Returns the 
        /// <see cref="XmlDocument"/> with the specified name.
        /// </summary>
        /// <returns>The requested <see cref="XmlDocument"/>.</returns>
        /// <seealso cref="XmlDocument"/>
        /// <param name="name">The file name of the document to get.</param>
        public XmlDocument GetDocument(string name)
        {
            return null;
        }

        /// <summary>
        /// Returns the 
        /// <see cref="XmlDocument"/> with the specified name.
        /// </summary>
        /// <param name="name">The name of the 
        /// <see cref="XmlDocument"/> to be retrieved from the container.</param>
        /// <param name="retrievalModes">The <see cref="RetrievalModes"/> parameter.</param>
        /// <seealso cref="XmlDocument"/>
        /// <seealso cref="RetrievalModes"/>
        /// <seealso cref="XmlTransaction"/>
        /// <returns>The requested <see cref="XmlDocument"/>.</returns>
        public XmlDocument GetDocument(string name, RetrievalModes retrievalModes)
        {
            return null;
        }

        /// <summary>Returns a node representing the specified handle.</summary>
        /// <param name="nodeHandle">The handle representing the node which must have been obtained using <see cref="XmlValue.NodeHandle"/>.</param>
        /// <param name="nodeOptions">One or more <see cref="RetrievalModes"/> values.</param>
        /// <remarks>
        /// The handle must represent a node in a document in the <see cref="Container"/>. If the 
        /// document or node has been removed, the operation may fail.
        /// <para>
        /// Node handles are guaranteed to remain stable in the absence of modifications to a document. If a document 
        /// is modified, a handle may cease to exist, or may belong to a different node.
        /// </para>
        /// </remarks>
        /// <returns>
        /// An <see cref="XmlValue"/> representing a node in a document in 
        /// the calling <see cref="Container"/>.
        /// </returns>
        /// <seealso cref="XmlValue"/>
        /// <seealso cref="RetrievalModes"/>
        public XmlValue GetNode(string nodeHandle, RetrievalModes nodeOptions)
        {
            return null;
        }

        /// <summary>
        /// Performs a lookup of the number of documents in  the 
        /// <see cref="Container"/>.
        /// </summary>
        /// <remarks>
        ///     If you use this method and the container is configured with
        ///     transactions, an <see cref="XmlTransaction"/> object will be
        ///     created and used for the operation.
        /// </remarks>
        /// <returns>The number of documents stored in the 
        /// <see cref="Container"/>.</returns>
        public ulong GetNumDocuments()
        {
            return 0;
        }

        /// <summary>
        /// Returns a
        /// <see cref="KeyStatistics"/> object for the identified index. This
        /// object identifies the number of keys (both total and unique)
        /// maintained for the identified index.
        /// </summary>
        /// <param name="nodeNamespace">The namespace of the node to which this
        /// index is applied.</param>
        /// <param name="nodeName">Name of the node.</param>
        /// <param name="index">Identifies the index for which you want the
        /// statistics returned. The value supplied here must be a valid index.
        /// See <see cref="XmlIndexSpecification.AddIndex"/>for a description of
        /// valid index specifications.</param>
        /// <param name="value">Provides the value to which equality indices
        /// must be equal. This parameter is required when returning statistics
        /// on equality indices, and it is ignored for all other types of
        /// indices.</param>
        /// <returns>Statistical information for the indices.</returns>
        /// <remarks>
        /// This form of this method cannot be used to return statistics on edge
        /// indices.
        /// </remarks>
        /// <seealso cref="XmlIndexSpecification"/>
        /// <seealso cref="XmlManager"/>
        /// <seealso cref="XmlTransaction"/>
        public KeyStatistics LookupStatistics(string nodeNamespace, string nodeName, string index, XmlValue value)
        {
            return null;
        }

        /// <summary>
        /// Returns an <see cref="KeyStatistics"/> object for 
        /// the identified index. This object identifies the number of keys (both total and unique) 
        /// maintained for the identified index.
        /// </summary>
        /// <remarks>
        /// Lookup statistics for the identified index. Use this form of this method to return statistics on edge indices.
        /// <para>
        /// Edge indices are indices maintained for those locations in a document where two 
        /// nodes (a parent node and a child node) meet. See @developing-dotnet-apps-with-figaro.md for details.
        /// </para>
        /// </remarks>
        /// <param name="nodeNamespace">The namespace of the node to which this index is applied.</param>
        /// <param name="nodeName">The name of the node to which this index is applied.</param>
        /// <param name="parentUri">The namespace of the parent node to which this edge index is applied.</param>
        /// <param name="parentName">The name of the parent node to which this edge index is applied.</param>
        /// <param name="index">Identifies the index for which you want the statistics returned. The 
        /// value supplied here must be a valid index. See 
        /// <see cref="XmlIndexSpecification.AddIndex"/> for a 
        /// description of valid index specifications.</param>
        /// <param name="value">Provides the value to which equality indices must be equal. This parameter is 
        /// required when returning statistics on equality indices, and it is ignored for all other types of indices.</param>
        /// <returns>Statistical information for the indices.</returns>
        /// <seealso cref="XmlManager"/>
        /// <seealso cref="XmlIndexSpecification"/>
        /// <seealso cref="KeyStatistics"/>
        public KeyStatistics LookupStatistics(string nodeNamespace, string nodeName, string parentUri, string parentName, string index, XmlValue value)
        {
            return null;
        }

        ///	<summary>
        /// Inserts an <see cref="XmlDocument"/> into the container. The value
        /// returned by this method is dependent upon  the form of the method
        /// that you used to perform the insertion.
        /// </summary>
        /// <remarks>
        /// Note that the name used for the document must be unique in the
        /// container or an exception is thrown.  The flag, 
        /// <see cref="PutDocumentOptions.GenerateFileName"/>, can be used to 
        /// generate a name. To change a document that already exists in the 
        /// container, use 
        /// <see cref="UpdateDocument(XmlDocument,UpdateContext)"/>.
        /// <para>
        /// The document content is indexed according to the container indexing
        /// specification. The indexer supports the  Xerces content encodings
        /// and expects the content to be well-formed, but it need not be valid.
        /// </para>
        /// </remarks>
        /// <param name="doc">The <see cref="XmlDocument"/> to insert into the 
        /// <see cref="Container"/>.</param>
        /// <param name="context">The update context to use for the document
        /// insertion.</param>
        /// <param name="putDocumentOptions">One or more of the values specified
        /// in <see cref="PutDocumentOptions"/>.</param>
        /// <seealso cref="PutDocumentOptions"/>
        /// <seealso cref="UpdateContext"/>
        /// <seealso cref="Container.UpdateDocument(XmlDocument,UpdateContext)"/>
        public void PutDocument(XmlDocument doc, UpdateContext context, PutDocumentOptions putDocumentOptions) { }

        /// <summary> 
        /// Inserts an <see cref="XmlDocument"/> into the container. The value returned by this method is dependent upon 
        /// the form of the method that you used to perform the insertion.
        /// </summary> 
        /// <remarks> 
        /// Note that the name used for the document must be unique in the container or an exception is thrown. 
        /// The flag, <see cref="PutDocumentOptions.GenerateFileName"/>, can be used to generate a name. To change a document that already exists in the 
        /// container, use <see cref="Container.UpdateDocument(XmlDocument,UpdateContext)"/>.
        /// <para> 
        /// The document content is indexed according to the container indexing specification. The indexer supports the 
        /// Xerces content encodings and expects the content to be well-formed, but it need not be valid.
        /// </para> 
        /// </remarks> 
        /// <param name="doc">The <see cref="XmlDocument"/> to insert into the <see cref="Container"/>.</param>
        /// <param name="context">The update context to use for the document insertion.</param>
        /// <seealso cref="PutDocumentOptions"/>
        /// <seealso cref="UpdateContext"/>
        /// <seealso cref="Container.UpdateDocument(XmlDocument,UpdateContext)"/>
        public void PutDocument(XmlDocument doc, UpdateContext context) { }

        /// <summary> 
        /// Inserts an <see cref="XmlDocument"/> into the container. The value returned by this method is dependent upon 
        /// the form of the method that you used to perform the insertion.
        /// </summary> 
        /// <remarks> 
        /// Note that the name used for the document must be unique in the container or an exception is thrown. 
        /// The flag, <see cref="PutDocumentOptions.GenerateFileName"/>, can be used to generate a name. To change a document that already exists in the 
        /// container, use <see cref="Container.UpdateDocument(XmlDocument,UpdateContext)"/>.
        /// <para> 
        /// The document content is indexed according to the container indexing specification. The indexer supports the 
        /// Xerces content encodings and expects the content to be well-formed, but it need not be valid.
        /// </para> 
        /// </remarks> 
        /// <param name="pathAndName">The full path of the document for <see cref="Container"/> to read as a stream.</param>
        /// <param name="context">The update context to use for the document insertion.</param>
        /// <param name="putDocumentOptions">One or more of the values specified in <see cref="PutDocumentOptions"/>.</param>
        /// <returns>The name used for the new document.</returns>
        /// <seealso cref="PutDocumentOptions"/>
        /// <seealso cref="UpdateContext"/>
        /// <seealso cref="Container.UpdateDocument(XmlDocument,UpdateContext)"/>
        public string PutDocument(string pathAndName, UpdateContext context, PutDocumentOptions putDocumentOptions)
        {
            return string.Empty;
        }

        /// <summary> 
        /// Inserts an <see cref="XmlDocument"/> into the container. The value returned by this method is dependent upon 
        /// the form of the method that you used to perform the insertion.
        /// </summary> 
        /// <remarks> 
        /// Note that the name used for the document must be unique in the container or an exception is thrown. 
        /// The flag, <see cref="PutDocumentOptions.GenerateFileName"/>, can be used to generate a name. To change a document that already exists in the 
        /// container, use <see cref="Container.UpdateDocument(Figaro.XmlDocument,Figaro.UpdateContext)"/>.
        /// <para> 
        /// The document content is indexed according to the container indexing specification. The indexer supports the 
        /// Xerces content encodings and expects the content to be well-formed, but it need not be valid.
        /// </para> 
        /// </remarks> 
        /// <returns>The name used for the new document.</returns>
        /// <param name="pathAndName">The full path of the document for <see cref="Container"/> to read as a stream.</param>
        /// <param name="context">The update context to use for the document insertion.</param>
        /// <seealso cref="PutDocumentOptions"/>
        /// <seealso cref="UpdateContext"/>
        /// <seealso cref="Container.UpdateDocument(Figaro.XmlDocument,Figaro.UpdateContext)"/>
        public string PutDocument(string pathAndName, UpdateContext context)
        {
            return string.Empty;
        }


        /// <summary>
        /// Inserts an <see cref="XmlInputStream"/> into the container.
        /// </summary>
        /// <param name="name">Provides the name of the document to insert into the container. This name must be unique in the container.
        /// If <see cref="PutDocumentOptions.GenerateFileName"/> is set, a
        /// system-defined string is appended to create a unique name. This applies if the name
        /// parameter is provided or empty. If the name is not unique within the container, an exception is thrown.</param>
        /// <param name="adoptedInput">The stream object to insert into the container. The content read by the input stream must be
        /// well-formed XML, or an exception is thrown. The stream object provided is consumed (deleted) by this method.</param>
        /// <param name="context">The update context for document insertion.</param>
        /// <param name="putDocumentOptions">One or more options to use when inserting the document.</param>
        /// <returns>The name used for the new document.</returns>
        public string PutDocument(string name, XmlInputStream adoptedInput, UpdateContext context, PutDocumentOptions putDocumentOptions)
        {
            return string.Empty;
        }

        /// <summary>
        /// Inserts an <see cref="XmlDocument"/> into the container. The value returned by this method is dependent upon
        /// the form of the method that you used to perform the insertion.
        /// </summary>
        /// <param name="name">The name of the document you want to insert.</param>
        /// <param name="contents">The contents of the document as a <see cref="System.String"/> variable.</param>
        /// <param name="context">The update context to use for the document insertion.</param>
        /// <param name="putDocumentOptions">One or more of the values specified in <see cref="PutDocumentOptions"/>.</param>
        /// <returns>The name used for the new document.</returns>
        /// <remarks>
        /// Note that the name used for the document must be unique in the container or an exception is thrown.
        /// The flag, <see cref="PutDocumentOptions.GenerateFileName"/>, can
        /// be used to generate a name. To change a document that already exists in the
        /// container, use <see cref="Container.UpdateDocument(XmlDocument,UpdateContext)"/>.
        /// <para>
        /// The document content is indexed according to the container indexing specification. The indexer supports the
        /// Xerces content encodings and expects the content to be well-formed, but it need not be valid.
        /// </para>
        /// </remarks>
        /// <seealso cref="PutDocumentOptions"/>
        /// <seealso cref="UpdateContext"/>
        /// <seealso cref="Container.UpdateDocument(XmlDocument,UpdateContext)"/>
        public string PutDocument(string name, string contents, UpdateContext context, PutDocumentOptions putDocumentOptions)
        {
            return string.Empty;
        }

        /// <summary>
        /// Removes the named alias from the list maintained by the containing <see cref="XmlManager"/>. If the alias does not exist, or matches a
        /// different <see cref="Container"/>, the call fails.
        /// </summary>
        /// <param name="alias">The alias to remove.</param>
        /// <returns>
        /// Returns <c>true</c> upon success, <c>false</c> upon failure.
        /// </returns>
        /// <seealso cref="XmlManager"/>
        public bool RemoveAlias(string alias)
        {
            return false;
        }

        /// <summary>
        /// Replaces an index of the specified type for the named document node.
        /// </summary>
        /// <param name="nodeNamespace">The namespace of the node to be indexed. The default namespace is selected by passing an empty string for the namespace.</param>
        /// <param name="nodeName">Name of the node.</param>
        /// <param name="index">A comma-separated list of strings that represent the indexing strategy.</param>
        /// <param name="context">The <see cref="UpdateContext"/> to use for this operation.</param>
        /// <remarks>
        /// This method is for convenience --
        /// see <see cref="XmlIndexSpecification.ReplaceIndex(XmlIndex)"/> for more information.
        /// </remarks>
        /// <seealso cref="XmlIndexSpecification"/>
        /// <seealso cref="UpdateContext"/>
        public void ReplaceIndex(string nodeNamespace, string nodeName, string index, UpdateContext context)
        {
        }

        /// <summary>
        /// Replaces the container's default index.
        /// </summary>
        /// <param name="index">A comma-separated list of strings that represent the indexing strategy.</param>
        /// <param name="context">The update context to use for the index replacement.</param>
        /// <remarks>
        /// This method is for convenience --
        /// see <see cref="XmlIndexSpecification.ReplaceIndex(XmlIndex)"/> for more information.
        /// </remarks>
        /// <seealso cref="XmlIndexSpecification"/>        
        public void ReplaceDefaultIndex(string index, UpdateContext context) { }

        /// <summary>
        /// Defines the type of indexing to be maintained for a container of documents. The currently defined indexing
        /// specification can be retrieved with the <see cref="Container.GetIndexSpecification"/>
        /// method.
        /// </summary>
        /// <param name="index">The indexing specification for the <see cref="Container"/>.</param>
        /// <param name="context">The update context to use for the index modification.</param>
        /// <remarks>
        /// If the container is not empty then the contained documents are incrementally indexed. Index keys for disabled
        /// index strategies are removed and index keys for enabled index strategies are added. Note that the length of
        /// time taken to perform this re-indexing operation is proportional to the size of the container.
        /// </remarks>
        /// <seealso cref="UpdateContext"/>
        public void SetIndexSpecification(XmlIndexSpecification index, UpdateContext context) { }

        /// <summary>Flushes database pages for the container to disk.</summary>
        public void Sync() { }

        /// <summary>
        /// Updates an <see cref="XmlDocument"/>in the container. The document must have been
        /// retrieved from the container using <see cref="GetDocument(string)"/>,
        /// <see cref="XmlManager.Query(string,QueryContext,QueryOptions)"/>, or
        /// <see cref="XQueryExpression.Execute(QueryContext)"/>.
        /// </summary>
        /// <param name="document">The <see cref="XmlDocument"/> to be updated in
        /// the <see cref="Container"/>.</param>
        /// <param name="context">The update context to use for the document insertion.</param>
        /// <remarks>
        /// It is possible to use a constructed <see cref="XmlDocument"/> object, if its
        /// name is set to a valid name in the container. The document must still exist within the container. The document
        /// content is indexed according to the container indexing specification, with index keys being removed for the
        /// previous document content, and added for the updated document content.
        /// </remarks>
        /// <seealso cref="XQueryExpression"/>
        /// <seealso cref="XmlManager"/>
        public void UpdateDocument(XmlDocument document, UpdateContext context) { }

        /// <summary>
        /// Transactionally updates an <see cref="XmlDocument"/>in the container. The document must have been
        /// retrieved from the container using <see cref="GetDocument(string)"/>,
        /// <see cref="XmlManager.Query(string,QueryContext,QueryOptions)"/>, or
        /// <see cref="XQueryExpression.Execute(QueryContext)"/>.
        /// </summary>
        /// <param name="transaction">The <see cref="XmlTransaction"/> object to use in the transactional update.</param>
        /// <param name="document">The <see cref="XmlDocument"/> to be updated in
        /// the <see cref="Container"/>.</param>
        /// <param name="context">The update context to use for the document insertion.</param>
        /// <remarks>
        /// It is possible to use a constructed <see cref="XmlDocument"/> object, if its
        /// name is set to a valid name in the container. The document must still exist within the container. The document
        /// content is indexed according to the container indexing specification, with index keys being removed for the
        /// previous document content, and added for the updated document content.
        /// </remarks>
        /// <seealso cref="XQueryExpression"/>
        /// <seealso cref="XmlManager"/>
        public void UpdateDocument(XmlTransaction transaction, XmlDocument document, UpdateContext context) { }

        /// <summary>
        /// Gets a value indicating whether or not the object is configured for transactions.
        /// </summary>
        /// <value>Returns <c>true</c> if configured for transactions.</value>
        public bool Transactional { get { return false; } }

        /// <summary>
        /// Closes the container.
        /// </summary>
        /// <remarks>This method is obsolete and will be removed in the next version of Figaro.</remarks>
        /// <seealso cref="ContainerScopeException"/>
        [Obsolete]
        public void Close() { }

        /// <summary>
        /// Gets the current indexing specification for the container.
        /// </summary>
        /// <returns>An <see cref="XmlIndexSpecification"/>.</returns>
        /// <remarks>
        /// The indexing specification can be modified using <see cref="SetIndexSpecification(XmlIndexSpecification,UpdateContext)"/>.
        /// </remarks>
        public XmlIndexSpecification GetIndexSpecification()
        {
            return null;
        }

        /// <summary>Returns the <see cref="XmlManager"/> associated with the <see cref="Container"/>.</summary>
        /// <returns>The <see cref="XmlManager"/> associated with the <see cref="Container"/>.</returns>
        public XmlManager GetManager()
        {
            return new XmlManager();
        }

        /// <summary>
        /// Add a default index to the container.
        /// </summary>
        /// <param name="transaction">The <see cref="XmlTransaction"/> to operate under.</param>
        /// <param name="index">A comma-separated list of strings that represent the indexing strategy.</param>
        /// <param name="context">The <see cref="UpdateContext"/> to use for the index operation.</param>
        /// <remarks>
        /// This method is for convenience -- see <see cref="XmlIndexSpecification.AddDefaultIndex(string)"/> for more information.
        /// </remarks>
        /// <seealso cref="UpdateContext"/>
        public void AddDefaultIndex(XmlTransaction transaction, string index, UpdateContext context) { }

        /// <summary>
        /// Adds an index of the specified type for the named document node.
        /// </summary>
        /// <param name="transaction">The <see cref="XmlTransaction"/> to operate under.</param>
        /// <param name="nodeNamespace">The namespace of the node to be indexed. The default is selected by passing an empty string.</param>
        /// <param name="nodeName">Name of the node.</param>
        /// <param name="index">A comma-separated list of strings representing the indexing strategy. See @using-indices.md for more details.</param>
        /// <param name="context">The update context to use for index insertion.</param>
        public void AddIndex(XmlTransaction transaction, string nodeNamespace, string nodeName, string index, UpdateContext context) { }

        /// <summary>
        /// Adds an index of the specified type for the named document node.
        /// </summary>
        /// <param name="transaction">The <see cref="XmlTransaction"/> to operate under.</param>
        /// <param name="nodeNamespace">The namespace of the node to be indexed. The default is selected by passing an empty string.</param>
        /// <param name="nodeName">Name of the node.</param>
        /// <param name="indexingStrategy">The indexing strategy.</param>
        /// <param name="context">The update context to use for index insertion.</param>
        /// <seealso cref="IndexingStrategy"/>
        public void AddIndex(XmlTransaction transaction, string nodeNamespace, string nodeName, IndexingStrategy indexingStrategy, UpdateContext context) { }

        /// <summary>
        /// Deletes the default index for the container.
        /// </summary>
        /// <param name="transaction">The <see cref="XmlTransaction"/> to operate under.</param>
        /// <param name="index">A comma-separated list of strings that represent the indexing strategy.</param>
        /// <param name="context">The <see cref="UpdateContext"/> to use for this operation.</param>
        /// <remarks>
        /// This method is for convenience - see
        /// <see cref="XmlIndexSpecification.DeleteDefaultIndex"/> for more information.
        /// </remarks>
        public void DeleteDefaultIndex(XmlTransaction transaction, string index, UpdateContext context) { }

        /// <summary>
        /// Deletes an index of the specified type for the named document node.
        /// </summary>
        /// <param name="transaction">The <see cref="XmlTransaction"/> to operate under.</param>
        /// <param name="nodeNamespace">The namespace of the node to be indexed. The default namespace is selected by passing an empty string for the namespace.</param>
        /// <param name="nodeName">Name of the node.</param>
        /// <param name="index">A comma-separated list of strings that represent the indexing strategy.</param>
        /// <param name="context">The <see cref="UpdateContext"/> to use for this operation.</param>
        /// <remarks>
        /// This method is for convenience -- see <see cref="XmlIndexSpecification.DeleteIndex(XmlIndex)"/> for
        /// more information.
        /// </remarks>
        public void DeleteIndex(XmlTransaction transaction, string nodeNamespace, string nodeName, string index, UpdateContext context) { }

        /// <summary>
        /// Delete the document with the given name.
        /// </summary>
        /// <param name="transaction">The <see cref="XmlTransaction"/> to operate under.</param>
        /// <param name="name">The file name of the <see cref="XmlDocument"/> to be deleted from the
        /// container. The name to be deleted is extracted from this parameter.</param>
        /// <param name="context">The <see cref="UpdateContext"/> object to use for this deletion.</param>
        /// <seealso cref="XmlDocument"/>
        /// <seealso cref="UpdateContext"/>
        public void DeleteDocument(XmlTransaction transaction, string name, UpdateContext context) { }

        /// <summary>
        /// Removes the specified <see cref="XmlDocument"/> from
        /// the <see cref="Container"/>.
        /// </summary>
        /// <param name="transaction">The <see cref="XmlTransaction"/> to operate under.</param>
        /// <param name="document">The <see cref="XmlDocument"/> to be deleted from the container. The name
        /// to be deleted is extracted from this parameter.</param>
        /// <param name="context">The <see cref="UpdateContext"/> object to use for this deletion.</param>
        public void DeleteDocument(XmlTransaction transaction, XmlDocument document, UpdateContext context) { }

        /// <summary>
        /// Return all of the documents in the container in a lazily evaluated <see cref="XmlResults"/> set.
        /// </summary>
        /// <param name="transaction">The <see cref="XmlTransaction"/> to operate under.</param>
        /// <param name="getAllDocumentOptions">Document options to use.</param>
        /// <returns>
        /// All documents from the <see cref="Container"/> in a
        /// lazily evaluated <see cref="XmlResults"/> set.
        /// </returns>
        /// <seealso cref="XmlResults"/>
        public XmlResults GetAllDocuments(XmlTransaction transaction, GetAllDocumentOptions getAllDocumentOptions)
        {
            return null;
        }

        /// <summary>
        /// Returns the <see cref="XmlDocument"/> with the specified name.
        /// </summary>
        /// <param name="transaction">The <see cref="XmlTransaction"/> to operate under.</param>
        /// <param name="name">The name of the <see cref="XmlDocument"/> to be retrieved from the container.</param>
        /// <param name="retrievalModes">The <see cref="RetrievalModes"/> parameter.</param>
        /// <returns>
        /// The requested <see cref="XmlDocument"/>.
        /// </returns>
        /// <seealso cref="XmlDocument"/>
        /// <seealso cref="GetAllDocumentOptions"/>
        /// <seealso cref="XmlTransaction"/>
        public XmlDocument GetDocument(XmlTransaction transaction, string name, RetrievalModes retrievalModes)
        {
            return null;
        }

        /// <summary>
        /// Performs a lookup of the number of documents in
        /// the <see cref="Container"/>.
        /// </summary>
        /// <param name="transaction">The <see cref="XmlTransaction"/> to operate under.</param>
        /// <returns>
        /// The number of documents stored in the <see cref="Container"/>.
        /// </returns>
        public ulong GetNumDocuments(XmlTransaction transaction)
        {
            return 0;
        }

        /// <summary>
        /// Returns a node representing the specified handle.
        /// </summary>
        /// <param name="transaction">The <see cref="XmlTransaction"/> to operate under.</param>
        /// <param name="nodeHandle">The handle representing the node which must have been obtained using <see cref="XmlValue.NodeHandle"/>.</param>
        /// <param name="nodeOptions">One or more <see cref="RetrievalModes"/> values.</param>
        /// <returns>
        /// An <see cref="XmlValue"/> representing a node in a document in
        /// the calling <see cref="Container"/>.
        /// </returns>
        /// <remarks>
        /// The handle must represent a node in a document in the <see cref="Container"/>. If the document or node has been removed, the operation may fail.
        /// <para>
        /// Node handles are guaranteed to remain stable in the absence of modifications to a document. If a document is modified, a handle may cease to exist, or may belong to a different node.
        /// </para>
        /// </remarks>
        /// <seealso cref="XmlValue"/>
        /// <seealso cref="RetrievalModes"/>
        public XmlValue GetNode(XmlTransaction transaction, string nodeHandle, RetrievalModes nodeOptions)
        {
            return null;
        }

        /// <summary>
        /// Returns an
        /// <see cref="KeyStatistics"/> object for the identified index.
        /// This object identifies the number of keys (both total and unique) maintained for the identified index.
        /// </summary>
        /// <param name="transaction">The <see cref="XmlTransaction"/> to operate under.</param>
        /// <param name="nodeNamespace">The namespace of the node to which this index is applied.</param>
        /// <param name="nodeName">Name of the node.</param>
        /// <param name="index">Identifies the index for which you want the statistics returned. The value supplied here must be a valid
        /// index. See <see cref="XmlIndexSpecification.AddIndex"/>for a
        /// description of valid index specifications.</param>
        /// <param name="value">Provides the value to which equality indices must be equal. This parameter is required when
        /// returning statistics on equality indices, and it is ignored for all other types of indices.</param>
        /// <returns>Statistical information for the indices.</returns>
        /// <remarks>
        /// This form of this method cannot be used to return statistics on edge indices.
        /// </remarks>
        /// <seealso cref="XmlIndexSpecification"/>
        /// <seealso cref="XmlManager"/>
        /// <seealso cref="XmlTransaction"/>
        public KeyStatistics LookupStatistics(XmlTransaction transaction, string nodeNamespace, string nodeName, string index, XmlValue value)
        {
            return null;
        }

        /// <summary>
        /// Returns an <see cref="KeyStatistics"/> object for
        /// the identified index. This object identifies the number of keys (both total and unique)
        /// maintained for the identified index.
        /// </summary>
        /// <param name="transaction">The <see cref="XmlTransaction"/> to operate under.</param>
        /// <param name="nodeNamespace">The namespace of the node to which this index is applied.</param>
        /// <param name="nodeName">Name of the node.</param>
        /// <param name="parentUri">The namespace of the parent node to which this edge index is applied.</param>
        /// <param name="parentName">The name of the parent node to which this edge index is applied.</param>
        /// <param name="index">Identifies the index for which you want the statistics returned. The
        /// value supplied here must be a valid index. See
        /// <see cref="XmlIndexSpecification.AddIndex"/> for a
        /// description of valid index specifications.</param>
        /// <param name="value">Provides the value to which equality indices must be equal. This parameter is
        /// required when returning statistics on equality indices, and it is ignored for all other types of indices.</param>
        /// <returns>Statistical information for the indices.</returns>
        /// <remarks>
        /// Lookup statistics for the identified index. Use this form of this method to return statistics on edge indices.
        /// <para>
        /// Edge indices are indices maintained for those locations in a document where two
        /// nodes (a parent node and a child node) meet. See @developing-dotnet-apps-with-figaro.md for details.
        /// </para>
        /// </remarks>
        /// <seealso cref="XmlManager"/>
        /// <seealso cref="XmlIndexSpecification"/>
        public KeyStatistics LookupStatistics(XmlTransaction transaction, string nodeNamespace, string nodeName, string parentUri, string parentName, string index, XmlValue value)
        {
            return null;
        }

        /// <summary>
        /// Inserts an <see cref="XmlDocument"/> into the container. The value returned by this method is dependent upon
        /// the form of the method that you used to perform the insertion.
        /// </summary>
        /// <param name="transaction">If the operation is to be transaction-protected, the parameter is an <see cref="XmlTransaction"/> handle
        /// returned from <see cref="XmlManager.CreateTransaction()"/>.</param>
        /// <param name="doc">The <see cref="XmlDocument"/> to insert into the <see cref="Container"/>.</param>
        /// <param name="context">The update context to use for the document insertion.</param>
        /// <remarks>
        /// <para>
        /// Note that the name used for the document must be unique in the container or an exception is thrown.
        /// The flag, <see cref="PutDocumentOptions.GenerateFileName"/>, can be used to generate a name. To change a document that already exists in the
        /// container, use <see cref="Container.UpdateDocument(XmlTransaction,XmlDocument,UpdateContext)"/>.
        /// </para>
        /// <para>
        /// The document content is indexed according to the container indexing specification. The indexer supports the
        /// Xerces content encodings and expects the content to be well-formed, but it need not be valid.
        /// </para>
        /// </remarks>
        /// <seealso cref="PutDocumentOptions"/>
        /// <seealso cref="UpdateContext"/>
        /// <seealso cref="Container.UpdateDocument(Figaro.XmlDocument,Figaro.UpdateContext)"/>
        public void PutDocument(XmlTransaction transaction, XmlDocument doc, UpdateContext context) { }

        /// <summary>
        /// Inserts an <see cref="XmlDocument"/> into the container. The value returned by this method is dependent upon
        /// the form of the method that you used to perform the insertion.
        /// </summary>
        /// <param name="transaction">If the operation is to be transaction-protected, the parameter is
        /// an <see cref="XmlTransaction"/> handle returned from <see cref="XmlManager.CreateTransaction()"/>.</param>
        /// <param name="doc">The <see cref="XmlDocument"/> to insert into the <see cref="Container"/>.</param>
        /// <param name="context">The update context to use for the document insertion.</param>
        /// <param name="putDocumentOptions">One or more of the values specified in <see cref="PutDocumentOptions"/>.</param>
        /// <remarks>
        /// <para>
        /// Note that the name used for the document must be unique in the container or an exception is thrown.
        /// The flag, <see cref="PutDocumentOptions.GenerateFileName"/>, can be used to
        /// generate a name. To change a document that already exists in the
        /// container, use
        /// <see cref="Container.UpdateDocument(Figaro.XmlDocument,Figaro.UpdateContext)"/>.
        /// </para>
        /// <para>
        /// The document content is indexed according to the container indexing specification. The indexer supports the
        /// Xerces content encodings and expects the content to be well-formed, but it need not be valid.
        /// </para>
        /// </remarks>
        public void PutDocument(XmlTransaction transaction, XmlDocument doc, UpdateContext context, PutDocumentOptions putDocumentOptions)
        {
        }

        /// <summary>
        /// Inserts an <see cref="XmlDocument"/> into the container. The value returned by this method is dependent upon
        /// the form of the method that you used to perform the insertion.
        /// </summary>
        /// <param name="transaction">If the operation is to be transaction-protected, the parameter is
        /// an <see cref="XmlTransaction"/> handle returned from
        /// <see cref="XmlManager.CreateTransaction()"/>.</param>
        /// <param name="pathAndName">The full path of the document for <see cref="Container"/> to read as a stream.</param>
        /// <param name="context">The update context to use for the document insertion.</param>
        /// <param name="putDocumentOptions">One or more of the values specified in <see cref="PutDocumentOptions"/>.</param>
        /// <returns>The name used for the new document.</returns>
        /// <remarks>
        /// <para>
        /// Note that the name used for the document must be unique in the container or an exception is thrown.
        /// The flag, <see cref="PutDocumentOptions.GenerateFileName"/>, can be used to generate a name. To change a document that already exists in the
        /// container,
        /// use <see cref="Container.UpdateDocument(Figaro.XmlDocument,Figaro.UpdateContext)"/>.
        /// </para>
        /// <para>
        /// The document content is indexed according to the container indexing specification. The indexer supports the
        /// Xerces content encodings and expects the content to be well-formed, but it need not be valid.
        /// </para>
        /// </remarks>
        /// <seealso cref="PutDocumentOptions"/>
        /// <seealso cref="UpdateContext"/>
        /// <seealso cref="Container.UpdateDocument(Figaro.XmlDocument,Figaro.UpdateContext)"/>
        public string PutDocument(XmlTransaction transaction, string pathAndName, UpdateContext context, PutDocumentOptions putDocumentOptions)
        {
            return string.Empty;
        }

        /// <summary>
        /// Inserts an <see cref="XmlDocument"/> into the container. The value returned by this method is dependent upon
        /// the form of the method that you used to perform the insertion.
        /// </summary>
        /// <param name="transaction">If the operation is to be transaction-protected, the parameter is
        /// an <see cref="XmlTransaction"/> handle returned
        /// from <see cref="XmlManager.CreateTransaction()"/>.</param>
        /// <param name="pathAndName">The full file path and name of the document you want to insert.</param>
        /// <param name="context">The update context to use for the document insertion.</param>
        /// <returns>The name used for the new document.</returns>
        /// <remarks>
        /// <para>
        /// Note that the name used for the document must be unique in the container or an exception is thrown.
        /// The flag, <see cref="PutDocumentOptions.GenerateFileName"/>, can be used
        /// to generate a name. To change a document that already exists in the
        /// container, use <see cref="Container.UpdateDocument(Figaro.XmlDocument,Figaro.UpdateContext)"/>.
        /// </para>
        /// <para>
        /// The document content is indexed according to the container indexing specification. The indexer supports the
        /// Xerces content encodings and expects the content to be well-formed, but it need not be valid.
        /// </para>
        /// </remarks>
        /// <seealso cref="UpdateContext"/>
        /// <seealso cref="Container.UpdateDocument(Figaro.XmlDocument,Figaro.UpdateContext)"/>
        public string PutDocument(XmlTransaction transaction, string pathAndName, UpdateContext context)
        {
            return string.Empty;
        }

        /// <summary>
        /// Inserts an <see cref="XmlDocument"/> into the container. The value returned by this method is dependent upon
        /// the form of the method that you used to perform the insertion.
        /// </summary>
        /// <param name="transaction">If the operation is to be transaction-protected, the parameter is an <see cref="XmlTransaction"/>
        /// handle returned from <see cref="XmlManager.CreateTransaction()"/>.</param>
        /// <param name="name">The name of the document you want to insert.</param>
        /// <param name="contents">The contents of the document as a <see cref="System.String"/> variable.</param>
        /// <param name="context">The update context to use for the document insertion.</param>
        /// <param name="putDocumentOptions">One or more of the values specified in <see cref="PutDocumentOptions"/>.</param>
        /// <returns>The name used for the new document.</returns>
        /// <remarks>
        /// Note that the name used for the document must be unique in the container or an exception is thrown.
        /// The flag, <see cref="PutDocumentOptions.GenerateFileName"/>, can be used to generate a name. To change a document that already exists in the
        /// container, use <see cref="Container.UpdateDocument(Figaro.XmlDocument,Figaro.UpdateContext)"/>.
        /// <para>
        /// The document content is indexed according to the container indexing specification. The indexer supports the
        /// Xerces content encodings and expects the content to be well-formed, but it need not be valid.
        /// </para>
        /// </remarks>
        /// <seealso cref="PutDocumentOptions"/>
        /// <seealso cref="UpdateContext"/>
        /// <seealso cref="Container.UpdateDocument(Figaro.XmlDocument,Figaro.UpdateContext)"/>
        public string PutDocument(XmlTransaction transaction, string name, string contents, UpdateContext context, PutDocumentOptions putDocumentOptions)
        {
            return string.Empty;
        }

        /// <summary>
        /// Replaces the container's default index.
        /// </summary>
        /// <param name="transaction">The <see cref="XmlTransaction"/> to operate under.</param>
        /// <param name="index">A comma-separated list of strings that represent the indexing strategy.</param>
        /// <param name="context">The update context to use for the index replacement.</param>
        /// <remarks>
        /// This method is for convenience --
        /// see <see cref="XmlIndexSpecification.ReplaceDefaultIndex"/> for more information.
        /// </remarks>
        /// <seealso cref="XmlIndexSpecification"/>
        public void ReplaceDefaultIndex(XmlTransaction transaction, string index, UpdateContext context) { }

        /// <summary>
        /// Replaces an index of the specified type for the named document node.
        /// </summary>
        /// <param name="transaction">The <see cref="XmlTransaction"/> to operate under.</param>
        /// <param name="nodeNamespace">The namespace of the node to be indexed. The default namespace is selected by passing an empty string for the namespace.</param>
        /// <param name="nodeName">The name of the element or attribute node to be indexed.</param>
        /// <param name="index">A comma-separated list of strings that represent the indexing strategy.</param>
        /// <param name="context">The <see cref="UpdateContext"/> to use for this operation.</param>
        /// <remarks>
        /// This method is for convenience --
        /// see <see cref="XmlIndexSpecification.ReplaceIndex(XmlIndex)"/>for more information.
        /// </remarks>
        /// <seealso cref="XmlIndexSpecification"/>
        /// <seealso cref="UpdateContext"/>
        public void ReplaceIndex(XmlTransaction transaction, string nodeNamespace, string nodeName, string index, UpdateContext context) { }

        /// <summary>
        /// Defines the type of indexing to be maintained for a container of documents. The currently defined indexing
        /// specification can be retrieved with the <see cref="Container.GetIndexSpecification"/>
        /// method.
        /// </summary>
        /// <param name="transaction">The <see cref="XmlTransaction"/> to operate under.</param>
        /// <param name="index">The indexing specification for the <see cref="Container"/>.</param>
        /// <param name="context">The update context to use for the index modification.</param>
        /// <remarks>
        /// If the container is not empty then the contained documents are incrementally indexed. Index keys for disabled
        /// index strategies are removed and index keys for enabled index strategies are added. Note that the length of
        /// time taken to perform this re-indexing operation is proportional to the size of the container.
        /// </remarks>
        /// <seealso cref="UpdateContext"/>
        public void SetIndexSpecification(XmlTransaction transaction, XmlIndexSpecification index, UpdateContext context) { }

        /// <summary>
        /// Inserts an <see cref="XmlInputStream"/> into the container.
        /// </summary>
        /// <param name="transaction">The <see cref="XmlTransaction"/> to operate under.</param>
        /// <param name="name">Provides the name of the document to insert into the container. This name must be unique in the container.
        /// If <see cref="PutDocumentOptions.GenerateFileName"/> is set, a
        /// system-defined string is appended to create a unique name. This applies if the name
        /// parameter is provided or empty. If the name is not unique within the container, an exception is thrown.</param>
        /// <param name="adoptedInput">The stream object to insert into the container. The content read by the input stream must be
        /// well-formed XML, or an exception is thrown. The stream object provided is consumed (deleted) by this method.</param>
        /// <param name="context">The update context for document insertion.</param>
        /// <param name="putDocumentOptions">One or more options to use when inserting the document.</param>
        /// <returns>The name used for the new document.</returns>
        public string PutDocument(XmlTransaction transaction, string name, XmlInputStream adoptedInput, UpdateContext context, PutDocumentOptions putDocumentOptions)
        {
            return string.Empty;
        }

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

        /// <summary>
        /// Allows an object to try to free resources and perform other cleanup operations before it is 
        /// reclaimed by garbage collection.
        /// </summary>
        ~Container()
        {

        }
    } // class
}
