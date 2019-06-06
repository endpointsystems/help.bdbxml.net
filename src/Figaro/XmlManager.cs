// ReSharper disable UnusedParameter.Global
// ReSharper disable UnusedParameter.Local
// ReSharper disable CommentTypo
#pragma warning disable 1574
#pragma warning disable 612,618
#pragma warning disable 168
// ReSharper disable RedundantAssignment
// ReSharper disable UnusedParameter.Local
// ReSharper disable UnusedAutoPropertyAccessor.Local
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Linq;

namespace Figaro
{
    /// <summary>
    /// Provides a high-level object used to manage various aspects of container, query, streaming, context, and transaction management.
    /// </summary>
    /// <remarks>
    /// You use <see cref="XmlManager"/> to perform activities such as
    /// container management (including creation and open), preparing
    /// XQuery queries, executing one-off queries, creating transaction objects, creating update and query
    /// context objects, and creating input streams.
    /// </remarks>
    /// <threadsafety static="true" instance="true"/>
    public sealed class XmlManager : IDisposable
    {


        // <summary>
        // Creates a new <see cref="Container"/> instance using the assigned configuration.
        // </summary>
        // <param name="name">The container name.</param>
        // <param name="config">The container configuration.</param>
        // <returns>A new container instance.</returns>
        //public Container CreateContainer(string name, ContainerConfig config)
        //{
        //    return null;
        //}

        /// <summary>
        /// Creates a new <see cref="Container"/> instance using the assigned configuration.
        /// </summary>
        /// <param name="containerOptions">The container configuration options.</param>
        /// <returns>A new container instance.</returns>
        public Container CreateContainer(ContainerConfig containerOptions)
        {            
            return null;
        }

        /// <summary>
        /// Creates a new <see cref="Container"/> instance using the assigned configuration and the specified transaction.
        /// </summary>
        /// <param name="transaction">The transaction to use.</param>
        /// <param name="containerOptions">The container configuration options.</param>
        /// <returns>A new container instance.</returns>
        public Container CreateContainer(XmlTransaction transaction, ContainerConfig containerOptions)
        {
            return null;
        }

        /// <summary>
        /// Creates a new <see cref="XmlManager"/> instance and sets the home environment directory.
        /// </summary>
        /// <remarks>
        /// Use this constructor if you wish to set the underlying environment object to a particular home directory. See 
        /// @file-naming.md for more information.
        /// </remarks>
        /// <param name="environmentHome">The home directory.</param>
        /// <seealso href="@file-naming.md">File Naming</seealso>
        public XmlManager(string environmentHome)
        {
            environmentHome = string.Empty;
        }

        /// <summary>
        /// Creates a new <see cref="XmlManager"/> instance and sets the home environment directory.
        /// </summary>
        /// <remarks>
        /// Use this constructor if you wish to set the underlying environment object to a particular home directory. See 
        /// @file-naming.md for more information.
        /// </remarks>
        /// <param name="environmentHome">The home directory.</param>
        /// <param name="initOptions"><see cref="XmlManager"/> initialization options.</param>
        /// <seealso href="@file-naming.md">File Naming</seealso>
        public XmlManager(string environmentHome, ManagerInitOptions initOptions)
        {
        }
        //

        /// <summary>
        /// Gets or sets the manager instance name.
        /// </summary>
        /// <remarks>
        /// This property is currently used by configuration
        /// objects for reference purposes. When an <see cref="XmlManager"/> instance is created from configuration, 
        /// its configuration instance name will be assigned to this property, which in turn can be used to create 
        /// <see cref="ContainerConfig"/> configuration instances.
        /// </remarks>
        public string ConfigurationName { get; set; }

        /// <summary>
        /// Adds a data directory to the <see cref="XmlManager"/>'s environment.
        /// </summary>
        /// <remarks>
        /// Every <see cref="XmlManager"/> instance has an <see cref="FigaroEnv"/> instance associated with it, even 
        /// if developers do not explicitly associate one to it. This method allows developers to specify one or more 
        /// data directories for an <see cref="XmlManager"/>'s environment so that developers can reference the 
        /// container names in XQuery expressions without the need to explicitly provide the absolute path to the 
        /// container(s) being queried.
        /// </remarks>
        /// <seealso cref="FigaroEnv.AddDataDirectory"/>
        /// <param name="dataDirectory">The data directory accessed by the <see cref="XmlManager"/>.</param>
        public void AddDataDirectory(string dataDirectory)
        {
        }

        /// <summary>
        /// Gets a list of data directories accessed by the <see cref="XmlManager"/> instance.
        /// </summary>
        /// <remarks>
        /// Every <see cref="XmlManager"/> instance has an <see cref="FigaroEnv"/> instance associated with it, even 
        /// if developers do not explicitly associate one to it. This method allows developers to list the  
        /// data directories for an <see cref="XmlManager"/>'s environment so that developers know what directories the 
        /// <see cref="XmlManager"/> instance has access to.
        /// </remarks>
        /// <seealso cref="FigaroEnv.GetDataDirectories"/>
        /// <returns>A list of data directories.</returns>
        public ReadOnlyCollection<string> GetDataDirectories() { return new ReadOnlyCollection<string>(new List<string>()); }


        /// <summary>
        /// Create a new container instance.
        /// </summary>
        /// <param name="name">The name of the container to create.</param>
        /// <param name="containerOptions">Container settings.</param>
        /// <param name="containerType">The type of container to create (<see cref="XmlContainerType.WholeDocContainer"/> vs (<see cref="XmlContainerType.NodeContainer"/>).</param>
        /// <returns>A new <see cref="Container"/>.</returns>
        public Container CreateContainer(string name, ContainerConfig containerOptions, XmlContainerType containerType)
        {
            return null;
        }

        /// <summary>
        /// Create a new container instance.
        /// </summary>
        /// <param name="transaction">The <see cref="XmlTransaction"/> to operate against.</param>
        /// <param name="name">The name of the container to create.</param>
        /// <param name="containerOptions">Container settings.</param>
        /// <param name="containerType">The type of container to create (<see cref="XmlContainerType.WholeDocContainer"/> vs (<see cref="XmlContainerType.NodeContainer"/>).</param>
        /// <returns>A new <see cref="Container"/>.</returns>
        public Container CreateContainer(XmlTransaction transaction, string name, ContainerConfig containerOptions, XmlContainerType containerType)
        {
            return null;
        }

        /// <summary>
        /// Open a <see cref="Container"/> object with the specified options.
        /// </summary>
        /// <param name="name">The container to open.</param>
        /// <param name="containerOptions">The container options.</param>
        /// <returns>A new <see cref="Container"/> instance.</returns>
        public Container OpenContainer(string name, ContainerConfig containerOptions) { return null; }

        /// <summary>
        /// Open a <see cref="Container"/> object with the specified options.
        /// </summary>
        /// <param name="transaction">The <see cref="XmlTransaction"/> to use.</param>
        /// <param name="name">The container to open.</param>
        /// <param name="containerOptions">The container options.</param>
        /// <returns>A new <see cref="Container"/> instance.</returns>
        public Container OpenContainer(XmlTransaction transaction, string name, ContainerConfig containerOptions) { return null; }
        /// <summary>
        /// Open a <see cref="Container"/> object of type <see cref="XmlContainerType"/> with the specified options.
        /// </summary>
        /// <param name="name">The name of the container to open.</param>
        /// <param name="containerOptions">The container options.</param>
        /// <param name="containerType">The type of container to open.</param>
        /// <returns>A new <see cref="Container"/> instance.</returns>
        public Container OpenContainer(string name, ContainerConfig containerOptions, XmlContainerType containerType) { return null; }
        /// <summary>
        /// Open a <see cref="Container"/> object of type <see cref="XmlContainerType"/> with the specified options.
        /// </summary>
        /// <param name="transaction">The <see cref="XmlTransaction"/> to use.</param>
        /// <param name="name">The name of the container to open.</param>
        /// <param name="containerOptions">The container options.</param>
        /// <param name="containerType">The type of container to open.</param>
        /// <returns>A new <see cref="Container"/> instance.</returns>
        public Container OpenContainer(XmlTransaction transaction, string name, ContainerConfig containerOptions, XmlContainerType containerType) { return null; }

        /// <summary>
        /// Create a new Figaro <see cref="XmlDocument"/> instance using a <see cref="XDocument"/> document.
        /// </summary>
        /// <param name="document">The <see cref="XDocument"/> to use.</param>
        /// <returns>a new Figaro <see cref="XmlDocument"/> instance.</returns>
        public XmlDocument CreateDocument(XDocument document) { return null; }
        /// <summary>
        /// Identifies an <see cref="XmlResolver"/> object to be used for file resolution. 
        /// </summary>
        /// <remarks>
        /// This object is used by the internal XML document parser to locate data based on URIs, 
        /// or public and system ids. Custom <see cref="XmlResolver"/> objects can be created 
        /// by applications to provide a mechanism to name and retrieve collections, documents, 
        /// and XML entities external to the database engine.
        /// </remarks>
        /// <param name="resolver">The resolver object to register with the <see cref="XmlManager"/>.</param>
        public void RegisterResolver(XQueryResolver resolver) { }

        /// <summary>
        /// Gets the path to the home directory of the underlying environment.
        /// </summary>
        public string EnvironmentHome { get; private set; }
        /// <summary>
        /// Gets the underlying <see cref="FigaroEnv"/> environment object.
        /// </summary>
        /// <remarks>
        /// If a <see cref="FigaroEnv"/> object is not specified at construction, an internal environment object is created with default 
        /// settings. This property gives developers access to either the instantiated or default <see cref="FigaroEnv"/> object associated with 
        /// this particular <see cref="XmlManager"/> instance. 
        /// <para>
        /// This property is only available to Concurrent Data Store (CDS) and Transactional Data Store (TDS) editions of the Figaro XML Database.
        /// </para>
        /// </remarks>
        /// <seealso cref="FigaroEnv"/>
        public FigaroEnv Environment { get; private set; }

        /// <summary>
        /// Create an <see cref="XmlInputStream"/> object from data residing at a URL address.
        /// </summary>
        /// <param name="baseId">The base system id URL which provides the base for any relative id part. </param>
        /// <param name="systemId">The possibly relative system id URL. If its relative it's based on <paramref name="baseId"/>, otherwise it's taken as-is.</param>
        /// <returns>A new <see cref="XmlInputStream"/> containing a copy of the remote resource.</returns>
        public XmlInputStream CreateUrlInputStream(string baseId, string systemId)
        {
            return null;
        }

        /// <summary>
        /// Create an <see cref="XmlInputStream"/> object from data residing at a URL address and assign it a public ID.
        /// </summary>
        /// <param name="baseId">The base system id URL which provides the base for any relative id part. </param>
        /// <param name="systemId">The possibly relative system id URL. If its relative it's based on <paramref name="baseId"/>, otherwise it's taken as-is.</param>
        /// <param name="publicId">The optional public id to set. This is just passed on to the parent class for storage.</param>
        /// <returns>A new <see cref="XmlInputStream"/> containing a copy of the remote resource.</returns>
        public XmlInputStream CreateUrlInputStream(string baseId, string systemId, string publicId)
        { return null; }


        /// <summary>
        /// Gets a value indicating whether or not the object is configured for transactions.
        /// </summary>
        /// <value>Returns <c>true</c> if configured for transactions.</value>
        public bool Transactional { get; private set; }

        /// <summary>
        /// Initializes a new instance of the XmlManager class.
        /// </summary>
        public XmlManager() { }
        /// <summary>
        ///  Initializes a new instance of the XmlManager class that uses a private internal database environment.
        /// </summary>
        /// <param name="managerInitOptions">Flags for opening the <see cref="XmlManager"/>.</param>
        /// <remarks>
        /// The underlying <see cref="FigaroEnv"/> is opened with the
        /// <see cref="EnvOpenOptions.Private"/>,
        /// <see cref="EnvOpenOptions.Create"/>, and
        /// <see cref="EnvOpenOptions.InitMemoryBufferPool"/> flags. These flags
        /// allow the underlying environment to be created if it does not already exist. In addition, the memory
        /// pool (in-memory cache) is initialized and available. Finally, the environment is private, which means
        /// that no external processes can join the environment, but the <see cref="XmlManager"/> object can be shared between
        /// threads within the opening process.
        /// <para>
        /// Note that for this form of the constructor, the <see cref="FigaroEnv"/> home is located in either the current
        /// working directory, or in the directory identified by the <c>DB_HOME</c> environment variable.
        /// </para>
        /// </remarks>
        /// <seealso cref="EnvOpenOptions"/>
        /// <seealso cref="FigaroEnv"/>
        public XmlManager(ManagerInitOptions managerInitOptions) { }

        /// <summary>
        /// 	Initializes a new instance of the XmlManager class. This constructor uses the provided <see cref="FigaroEnv"/> for the underlying environment. The
        /// Figaro subsystems initiated by this environment (for example, transactions, logging, the
        /// memory pool), are the subsystems that are available to Figaro when operations are performed
        /// using this manager object.
        /// </summary>
        /// <param name="env">The <see cref="FigaroEnv"/> to use for the underlying database environment. The
        /// environment provided here must be opened.</param>
        /// <param name="managerInitOptions">Flags for opening the <see cref="XmlManager"/>.</param>
        /// <remarks>
        /// The <see cref="FigaroEnv"/> must be opened before assigning to the <see cref="XmlManager"/> constructor.
        /// </remarks>
        /// <seealso cref="EnvOpenOptions"/>
        /// <seealso cref="FigaroEnv"/>
        public XmlManager(FigaroEnv env, ManagerInitOptions managerInitOptions) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlManager"/> class that uses the
        /// provided <see cref="FigaroEnv"/> instance for the underlying environment. The
        /// subsystems initiated in this environment (for example,
        /// transactions, logging, the memory pool), are the subsystems that are
        /// available to Figaro when operations are performed using
        /// this manager object.
        /// <para>No flags are passed to <see cref="XmlManager"/>.</para>
        /// </summary>
        /// <param name="env">The <see cref="FigaroEnv"/> to use for the
        /// underlying database environment. The environment provided here must
        /// be opened.</param>
        /// <remarks>
        /// The <see cref="FigaroEnv"/> must be opened before assigning to the 
        /// <see cref="XmlManager"/> constructor.
        /// </remarks>
        /// <seealso cref="EnvOpenOptions"/>
        /// <seealso cref="FigaroEnv"/>
        public XmlManager(FigaroEnv env) { }


        /// <summary>
        /// Compacts all of the databases in the container.
        /// </summary>
        /// <param name="name">The path and name of the container to compact.</param>
        /// <param name="context">The <see cref="UpdateContext"/> object used for this operation.</param>
        /// <remarks>
        /// The container must be closed; the system throws an exception if the container is open.
        /// </remarks>
        /// <seealso cref="XmlManager"/>
        /// <seealso cref="UpdateContext"/>
        public void CompactContainer(string name, UpdateContext context) { }

        /// <summary>
        /// Compacts all of the databases in the container.
        /// </summary>
        /// <param name="transaction">If the operation is to be transaction-protected, the
        /// parameter is an <see cref="XmlTransaction"/> handle returned from
        /// <see cref="CreateTransaction()"/>.</param>
        /// <param name="name">The path and name of the container to compact.</param>
        /// <param name="context">The <see cref="UpdateContext"/> object used for
        /// this operation.</param>
        /// <remarks>
        /// The container must be closed; the system throws an exception if the container is open.
        /// </remarks>
        /// <seealso cref="XmlManager"/>
        /// <seealso cref="UpdateContext"/>
        public void CompactContainer(XmlTransaction transaction, string name, UpdateContext context) { }

        /// <summary>
        /// Creates and opens a container, returning a handle to a <see cref="Container"/> object. If the container already
        /// exists at the time this method is called, an exception is thrown.
        /// </summary>
        /// <param name="name">The container's name. The container is created relative to the underlying environment's home
        /// directory (see <see cref="XmlManager"/> for more information) unless an absolute path is used for the name;
        /// in that case the container is created in the location identified by the path.
        /// <para>
        /// The name provided here must be unique for the environment or an exception is thrown.
        /// </para></param>
        /// <returns>
        /// A new, opened <see cref="Container"/> object.
        /// </returns>
        /// <remarks>
        /// Use <see cref="OpenContainer(string)"/> to open a container
        /// that has already been created.
        /// <para>
        /// Containers are created by default with the <see cref="ContainerConfig.AllowCreate"/> and
        /// the <see cref="ContainerConfig.ExclusiveCreate"/> properties configured, as well
        /// as a <see cref="XmlContainerType.NodeContainer"/> storage type.
        /// </para>
        /// <para>
        /// Containers always remain open until the last handle referencing the container is destroyed.
        /// </para>
        /// </remarks>
        /// <seealso cref="OpenContainer(string)"/>
        /// <seealso cref="XmlManager"/>
        public Container CreateContainer(string name)
        {
            return null;
        }

        /// <summary>
        /// Creates and opens a container, returning a handle to a <see cref="Container"/> object using the specified transaction
        /// handle. If the container already exists at the time this method is called, an exception is thrown.
        /// </summary>
        /// <param name="transaction">The <see cref="XmlTransaction"/> object to use for this container creation.</param>
        /// <param name="name">The container's name. The container is created relative to the underlying environment's home
        /// directory (see <see cref="XmlManager"/> for more information) unless an absolute path is used for the name;
        /// in that case the container is created in the location identified by the path.
        /// <para>
        /// The name provided here must be unique for the environment or an exception is thrown.
        /// </para></param>
        /// <returns>
        /// A new, opened <see cref="Container"/> object.
        /// </returns>
        /// <remarks>
        /// Use <see cref="OpenContainer(string)"/> to open a container
        /// that has already been created.
        /// <para>
        /// Containers are created by default with the <see cref="ContainerConfig.AllowCreate"/> and
        /// the <see cref="ContainerConfig.ExclusiveCreate"/> flags specified, as well
        /// as a <see cref="XmlContainerType.NodeContainer"/> storage type.
        /// </para>
        /// <para>
        /// Containers always remain open until the last handle referencing the container is destroyed.
        /// </para>
        /// </remarks>
        /// <seealso cref="OpenContainer(string)"/>
        /// <seealso cref="Container"/>
        /// <seealso cref="XmlManager"/>
        public Container CreateContainer(XmlTransaction transaction, string name)
        {
            return null;
        }

        /// <summary>
        /// Instantiate an <see cref="XmlDocument"/> object.
        /// </summary>
        /// <returns>
        /// A new <see cref="XmlDocument"/> object.
        /// </returns>
        /// <seealso cref="XmlDocument"/>
        public XmlDocument CreateDocument()
        {
            return null;
        }

        /// <summary>
        /// Instantiate a Figaro <see cref="XmlDocument"/> object using a 
        /// .NET <see cref="System.Xml.XmlDocument"/> object.
        /// </summary>
        /// <param name="document">The document containing the Xml content to add to the document body.</param>
        /// <returns>A new <see cref="XmlDocument"/> object.</returns>
        public XmlDocument CreateDocument(System.Xml.XmlDocument document)
        {
            return null;
        }

        /// <summary>
        /// Instantiate a Figaro <see cref="XmlDocument"/> object using 
        /// a .NET <see cref="XmlReader"/> object.
        /// </summary>
        /// <param name="reader">
        ///     The <see cref="XmlReader"/> containing the Xml content to add to 
        /// the document body.</param>
        /// <returns>A new <see cref="XmlDocument"/> object.</returns>
        public XmlDocument CreateDocument(XmlReader reader)
        {
            return null;
        }

        /// <summary>
        /// Instantiates an new <see cref="XmlIndexLookup"/> object for performing index lookup operations. Only a single index may
        /// be specified, and substring indices are not supported.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="nodeNamespace">The namespace of the node to be used. The default namespace is selected by passing an empty
        /// string for the namespace.</param>
        /// <param name="nodeName">Name of the node.</param>
        /// <param name="index">The string representing the indexing strategy.</param>
        /// <param name="value">The value to be used as the single value for an equality or inequality lookup,
        /// or as the lower bound of a range lookup. An empty value is specified using an
        /// uninitialized <see cref="XmlValue"/> object.</param>
        /// <param name="operation">The index lookup operation.</param>
        /// <returns>
        /// An <see cref="XmlIndexLookup"/> object for lookup operations.
        /// </returns>
        /// <seealso cref="XmlValue"/>
        /// <seealso cref="XmlIndexLookup"/>
        /// <seealso cref="IndexLookupOperation"/>
        /// <seealso cref="XmlManager"/>
        public XmlIndexLookup CreateIndexLookup(Container container, string nodeNamespace, string nodeName, string index, XmlValue value, IndexLookupOperation operation)
        {
            return null;
        }

        /// <summary>
        /// Returns an <see cref="XmlInputStream"/> to the file filename. Use this input stream with
        /// <see cref="Container.PutDocument(string,UpdateContext)"/>
        /// or <see cref="XmlDocument.SetContentAsInputStream(Figaro.XmlInputStream)"/>.
        /// </summary>
        /// <param name="fileName">The full path and file name to open with the <see cref="XmlInputStream"/>.</param>
        /// <returns>
        /// A stream object for <see cref="Container"/> or <see cref="XmlDocument"/> to manipulate.
        /// </returns>
        /// <remarks>
        /// If the input stream is passed to either of these methods, it will be adopted, and deleted.
        /// If it is not passed, it is the responsibility of the user to delete the object. Note that there
        /// is no attempt to ensure that the file referenced contains well-formed or valid XML. Exceptions may be
        /// thrown at the time that this input stream is actually read if the stream does not contain well-formed,
        /// or valid XML.
        /// </remarks>
        /// .
        /// <seealso cref="Container.PutDocument(string,UpdateContext)"/>
        /// <seealso cref="XmlDocument.SetContentAsInputStream(Figaro.XmlInputStream)"/>
        /// <seealso cref="XmlDocument"/>
        /// <seealso cref="Container"/>
        /// <seealso cref="XmlInputStream"/>
        public XmlInputStream CreateLocalFileInputStream(string fileName)
        {
            return null;
        }


        /// <summary>
        /// Returns an <see cref="XmlInputStream"/> object using
        /// an in-memory buffer of the <c>data</c> string.
        /// </summary>
        /// <param name="bufferData">The string buffer containing the XML data you want to read.</param>
        /// <param name="bufferId">The system ID to use for this input stream. This can be any arbitrary system ID; it is used only
        /// to satisfy the XML parser that will read this buffer.</param>
        /// <returns>
        /// An <see cref="XmlInputStream"/> object.
        /// </returns>
        /// <remarks>
        /// Use this input stream with <see cref="Container.PutDocument(string,UpdateContext)"/>
        /// or <see cref="XmlDocument.SetContentAsInputStream(Figaro.XmlInputStream)"/>.
        /// <para>
        /// Note that there is no attempt to ensure that the memory referenced contains well-formed or
        /// valid XML. Exceptions may be thrown at the time that this input stream is actually read if
        /// the stream does not contain well-formed, or valid XML.
        /// </para>
        /// </remarks>
        /// <seealso cref="Container.PutDocument(string,UpdateContext)"/>
        /// <seealso cref="XmlInputStream"/>
        /// <seealso cref="XmlDocument.SetContentAsInputStream(Figaro.XmlInputStream)"/>
        /// <seealso cref="XmlDocument"/>
        public XmlInputStream CreateMemBufInputStream(string bufferData, string bufferId)
        {
            return null;
        }

        /// <summary>
        /// Creates a new <see cref="QueryContext"/>.
        /// </summary>
        /// <param name="evaluationType">The evaluation type - whether the query returns Eager or Lazy values.</param>
        /// <returns>
        /// A new <seealso cref="QueryContext"/> object.
        /// </returns>
        /// <remarks>
        /// By default, the <see cref="QueryContext"/> will specify live values in the query return; in other words, the
        /// query will return references to actual documents stored in Figaro.
        /// </remarks>
        /// <seealso cref="QueryContext"/>
        /// <seealso cref="EvaluationType"/>
        public QueryContext CreateQueryContext(EvaluationType evaluationType)
        {
            return null;
        }
        /// <summary>
        /// Creates a new <see cref="QueryContext"/> with an Eager result set.
        /// </summary>
        /// <returns>
        /// A new <see cref="QueryContext"/>.
        /// </returns>
        /// <seealso cref="QueryContext"/>
        public QueryContext CreateQueryContext()
        {
            return null;
        }

        /// <summary>
        /// Instantiates a new, empty <see cref="XmlResults"/> object.
        /// </summary>
        /// <returns>
        /// A new, empty <see cref="XmlResults"/> object.
        /// </returns>
        /// <remarks>
        /// You can then use <see cref="XmlResults.Add(Figaro.XmlValue)"/> to add <see cref="XmlValue"/> objects to the results set.
        /// </remarks>
        /// <seealso cref="XmlResults"/>
        /// <seealso cref="XmlValue"/>
        public XmlResults CreateXmlResults()
        {
            return null;
        }
        /// <summary>
        /// Creates a new <see cref="XmlTransaction"/> object.
        /// </summary>
        /// <returns>
        /// A new <see cref="XmlTransaction"/> object.
        /// </returns>
        /// <remarks>
        /// Using this method, a new transaction is begun.
        /// <para>
        /// If transactions were not initialized when this <see cref="XmlManager"/> object was opened
        /// (that is, <see cref="EnvOpenOptions.InitTransaction"/> was not
        /// specified) then this method throws an exception.
        /// </para>
        /// </remarks>
        /// <seealso cref="XmlTransaction"/>
        /// <seealso cref="EnvOpenOptions"/>
        /// <seealso cref="FigaroEnv"/>
        public XmlTransaction CreateTransaction()
        {
            return null;
        }

        /// <summary>
        /// Creates a new handle to the existing <see cref="XmlTransaction"/> object.
        /// </summary>
        /// <param name="toAdopt">The <see cref="XmlTransaction"/> to adopt in the transaction.</param>
        /// <returns>
        /// A handle to the existing <see cref="XmlTransaction"/> object.
        /// </returns>
        /// <remarks>
        /// If the <see cref="XmlTransaction"/> object is destroyed or goes out of scope before
        /// <see cref="XmlTransaction.Commit()"/> or <see cref="XmlTransaction.Abort"/> are called, the state of the
        /// underlying transaction is left unchanged. This allows a transaction to be controlled
        /// external to its <see cref="XmlTransaction"/> object.
        /// </remarks>
        /// <seealso cref="XmlTransaction"/>
        public XmlTransaction CreateTransaction(XmlTransaction toAdopt)
        {
            return null;
        }

        /// <summary>
        /// Creates a new <see cref="XmlTransaction"/> object.
        /// </summary>
        /// <param name="transactionFlags">One or more transaction flags to specify when creating the new
        /// <see cref="XmlTransaction"/> object.</param>
        /// <returns>
        /// A new <see cref="XmlTransaction"/> object.
        /// </returns>
        /// <remarks>
        /// Using this method, a new transaction is begun.
        /// <para>
        /// If transactions were not initialized when this <see cref="XmlManager"/> object was opened
        /// (that is, <see cref="EnvOpenOptions.InitTransaction"/> was not
        /// specified) then this method throws an exception.
        /// </para>
        /// </remarks>
        /// <seealso cref="XmlTransaction"/>
        /// <seealso cref="EnvOpenOptions"/>
        /// <seealso cref="FigaroEnv"/>
        public XmlTransaction CreateTransaction(TransactionType transactionFlags)
        {
            return null;
        }

        /// <summary>
        /// Instantiates a new, default, <see cref="UpdateContext"/> object.
        /// </summary>
        /// <returns>
        /// A new <see cref="UpdateContext"/> object.
        /// </returns>
        /// <remarks>
        /// This object is used for <see cref="Container"/> operations that add, delete, and
        /// modify documents, and documents in containers.
        /// </remarks>
        /// <seealso cref="UpdateContext"/>
        /// <seealso cref="Container"/>
        public UpdateContext CreateUpdateContext()
        {
            return null;
        }

        /// <summary>
        /// Examines the named file and determines whether or not the file is a <see cref="Container"/>.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <returns>
        /// Returns<c>true</c> if the file is a <see cref="Container"/>.
        /// </returns>
        /// <remarks>
        /// The container may be open or closed; no exceptions will be thrown from this method.
        /// </remarks>
        /// <seealso cref="Container"/>
        public bool ExistsContainer(string container)
        {
            return true;
        }


        /// <summary>
        /// Opens a <see cref="Container"/>, returning a handle to a <see cref="Container"/> object.
        /// </summary>
        /// <param name="containerOptions">The container settings.</param>
        /// <returns>
        /// The handle to a <see cref="Container"/> object.
        /// </returns>
        /// <remarks>
        /// The container must already exist at the time that this method is called or an exception is thrown.
        /// <note type="info">
        /// Containers always remain open until the last handle referencing the container is destroyed.
        /// </note>
        /// </remarks>
        /// <seealso cref="Container"/>
        public Container OpenContainer(ContainerConfig containerOptions)
        {
            return null;
        }

        /// <summary>
        /// Opens a <see cref="Container"/>, returning a handle to a <see cref="Container"/> object.
        /// </summary>
        /// <param name="transaction">The transaction to use.</param>
        /// <param name="containerOptions">The container configuration.</param>
        /// <returns>
        /// The handle to a <see cref="Container"/> object.
        /// </returns>
        /// <remarks>
        /// The container must already exist at the time that this method is called or an exception is thrown.
        /// <note type="info">
        /// Containers always remain open until the last handle referencing the container is destroyed.
        /// </note>
        /// </remarks>
        /// <seealso cref="Container"/>
        public Container OpenContainer(XmlTransaction transaction, ContainerConfig containerOptions)
        {
            return null;
        }

        /// <summary>
        /// Opens a <see cref="Container"/>, returning a handle to a <see cref="Container"/> object.
        /// </summary>
        /// <param name="name">The name of a container to try to open.</param>
        /// <returns>
        /// The handle to a <see cref="Container"/> object.
        /// </returns>
        /// <remarks>
        /// The container must already exist at the time that this method is called or an exception is thrown.
        /// <note type="info">
        /// Containers always remain open until the last handle referencing the container is destroyed.
        /// </note>
        /// </remarks>
        /// <seealso cref="Container"/>
        public Container OpenContainer(string name)
        {
            return null;
        }
        /// <summary>
        /// Opens the identified container.
        /// </summary>
        /// <param name="transaction">The <see cref="XmlTransaction"/> object to use for this container open.</param>
        /// <param name="name">The container's name. The container is created relative to the underlying environment's home directory
        /// (see <see cref="XmlManager"/> for more information) unless an absolute path is used for the name; in that case
        /// the container is opened in the location identified by the path.</param>
        /// <returns>
        /// The handle to a <see cref="Container"/> object.
        /// </returns>
        /// <remarks>
        /// The container must already exist at the time that this method is called or an exception is thrown.
        /// <note type="info">
        /// Containers always remain open until the last handle referencing the container is destroyed.
        /// </note>
        /// </remarks>
        /// <seealso cref="Container"/>
        /// <seealso cref="XmlManager"/>
        public Container OpenContainer(XmlTransaction transaction, string name)
        {
            return null;
        }


        /// <summary>
        /// Compile an XQuery expression into an <see cref="XQueryExpression"/> object. You can then run the XQuery expression
        /// repeatedly using <see cref="XQueryExpression.Execute(QueryContext)"/>.
        /// </summary>
        /// <param name="xQuery">The XQuery expression to execute.</param>
        /// <param name="context">The query context to execute under.</param>
        /// <returns>
        /// An <see cref="XQueryExpression"/> object.
        /// </returns>
        /// <remarks>
        /// Use this method to compile and evaluate XQuery expressions against your <see cref="Container"/> and <see cref="XmlDocument"/>
        /// objects any time you want to evaluate the expression more than once.
        /// <para>
        /// Note that the scope of the query provided here can be restricted using one of the XQuery
        /// navigational functions. For example:
        /// <code>
        /// collection('mycontainer.dbxml')/foo
        /// </code>
        /// or:
        /// <code>
        /// doc('dbxml:/mycontainer.dbxml/mydoc.xml')/foo/@attr1='bar'
        /// </code>
        /// The scope of a query can also be controlled by passing an appropriate contextItem object to
        /// <see cref="XQueryExpression.Execute(QueryContext)"/>.
        /// </para>
        /// </remarks>
        /// <seealso cref="XmlManager"/>
        /// <seealso cref="QueryContext"/>
        /// <seealso cref="XQueryExpression"/>
        public XQueryExpression Prepare(string xQuery, QueryContext context)
        {
            return null;
        }
        /// <summary>
        /// Compile an XQuery expression into an <see cref="XQueryExpression"/>
        /// object. You can then run the XQuery expression repeatedly
        /// using <see cref="XQueryExpression.Execute(QueryContext)"/>.
        /// </summary>
        /// <param name="transaction">The transaction to execute the preparation under.</param>
        /// <param name="xQuery">The XQuery expression to execute.</param>
        /// <param name="context">The query context to execute under.</param>
        /// <returns>
        /// An <see cref="XQueryExpression"/> object.
        /// </returns>
        /// <remarks>
        /// Use this method to compile and evaluate XQuery expressions against
        /// your <see cref="Container"/> and
        /// <see cref="XmlDocument"/>
        /// objects any time you want to evaluate the expression more than once.
        /// <para>
        /// Note that the scope of the query provided here can be restricted using one of the XQuery
        /// navigational functions. For example:
        /// <code>
        /// collection('mycontainer.dbxml')/foo
        /// </code>
        /// or:
        /// <code>
        /// doc('dbxml:/mycontainer.dbxml/mydoc.xml')/foo/@attr1='bar'
        /// </code>
        /// The scope of a query can also be controlled by passing an appropriate contextItem object to
        /// <see cref="XQueryExpression.Execute(QueryContext)"/>.
        /// </para>
        /// </remarks>
        /// <seealso cref="XmlManager"/>
        /// <seealso cref="QueryContext"/>
        /// <seealso cref="XQueryExpression"/>
        /// <seealso cref="XmlTransaction"/>
        public XQueryExpression Prepare(XmlTransaction transaction, string xQuery, QueryContext context)
        {
            return null;
        }

        /// <summary>
        /// Executes a query in the context of the <see cref="XmlManager"/> object. This
        /// method is the equivalent of
        /// calling <see cref="Prepare(string,QueryContext)"/> and
        /// then <see cref="XQueryExpression.Execute(QueryContext)"/> on the prepared query.
        /// </summary>
        /// <remarks>
        /// The scope of the query can be restricted using one of the XQuery navigational functions. For example:
        /// <code>
        /// collection('mycontainer.dbxml')/foo
        /// </code>
        /// or:
        /// <code>
        /// doc('dbxml:/mycontainer.dbxml/mydoc.xml')/foo/@attr1='bar'
        /// </code>
        /// The scope of a query can also be controlled by passing an appropriate contextItem object
        /// to <see cref="XQueryExpression.Execute(QueryContext)"/>.
        /// <para>
        /// Note that this method is suitable for performing one-off queries. If you want to execute a query
        /// more than once, you should use <see cref="Prepare(string,QueryContext)"/> to compile the expression, and then use
        /// <see cref="XQueryExpression.Execute(QueryContext)"/> to run it.
        /// </para>
        /// <para>
        /// This method returns an <see cref="XmlResults"/> object. You then iterate over the results set contained
        /// in that object using <see cref="XmlResults.PreviousDocument"/> or
        /// <see cref="XmlResults.PreviousValue"/>, and
        /// <see cref="XmlResults.NextDocument"/> or
        /// <see cref="XmlResults.NextValue"/>.
        /// </para><para>
        /// For more information on querying containers and documents, see @developing-dotnet-apps-with-figaro.md.
        /// </para>
        /// </remarks>
        /// <seealso cref="XmlManager"/>
        /// <seealso cref="XmlResults"/>
        /// <see cref="QueryOptions"/>
        /// <seealso cref="QueryContext"/>
        /// <param name="xQuery">The XQuery expression to execute.</param>
        /// <param name="context">The query context to execute under.</param>
        /// <seealso href="@developing-dotnet-apps-with-figaro.md"/>
        /// <returns>
        /// An <see cref="XmlResults"/> object containing the query results.
        /// </returns>
        public XmlResults Query(string xQuery, QueryContext context)
        {
            return null;
        }

        /// <summary>
        /// Executes a query in the context of the <see cref="XmlManager"/> object. This
        /// method is the equivalent of
        /// calling <see cref="Prepare(string,QueryContext)"/> and
        /// then <see cref="XQueryExpression.Execute(QueryContext)"/> on the prepared query.
        /// </summary>
        /// <remarks>
        /// The scope of the query can be restricted using one of the XQuery navigational functions. For example:
        /// <code>
        /// collection('mycontainer.dbxml')/foo
        /// </code>
        /// or:
        /// <code>
        /// doc('dbxml:/mycontainer.dbxml/mydoc.xml')/foo/@attr1='bar'
        /// </code>
        /// The scope of a query can also be controlled by passing an appropriate contextItem object
        /// to <see cref="XQueryExpression.Execute(QueryContext)"/>.
        /// <para>
        /// Note that this method is suitable for performing one-off queries. If you want to execute a query
        /// more than once, you should use <see cref="Prepare(string,QueryContext)"/> to compile the expression, and then use
        /// <see cref="XQueryExpression.Execute(QueryContext)"/> to run it.
        /// </para>
        /// <para>
        /// This method returns an <see cref="XmlResults"/> object. You then iterate over the results set contained
        /// in that object using <see cref="XmlResults.PreviousDocument"/> or
        /// <see cref="XmlResults.PreviousValue"/>, and
        /// <see cref="XmlResults.NextDocument"/> or
        /// <see cref="XmlResults.NextValue"/>.
        /// </para><para>
        /// For more information on querying containers and documents, see @developing-dotnet-apps-with-figaro.md.
        /// </para>
        /// </remarks>
        /// <seealso cref="XmlManager"/>
        /// <seealso cref="XmlResults"/>
        /// <see cref="QueryOptions"/>
        /// <seealso cref="QueryContext"/>
        /// <param name="transaction">The transaction to perform the query with.</param>
        /// <param name="xQuery">The XQuery expression to execute.</param>
        /// <param name="context">The query context to execute under.</param>
        /// <seealso href="@developing-dotnet-apps-with-figaro.md"/>
        /// <returns>
        /// An <see cref="XmlResults"/> object containing the query results.
        /// </returns>
        public XmlResults Query(XmlTransaction transaction, string xQuery, QueryContext context)
        {
            return null;
        }

        /// <summary>
        /// Executes a query in the context of the <see cref="XmlManager"/> object. This
        /// method is the equivalent of
        /// calling <see cref="Prepare(string,QueryContext)"/> and
        /// then <see cref="XQueryExpression.Execute(QueryContext)"/> on the prepared query.
        /// </summary>
        /// <param name="xQuery">The XQuery expression to execute.</param>
        /// <param name="context">The query context to execute under.</param>
        /// <param name="queryOptions">One or more options to use when executing a query.</param>
        /// <returns>
        /// An <see cref="XmlResults"/> object containing the query results.
        /// </returns>
        /// <remarks>
        /// The scope of the query can be restricted using one of the XQuery navigational functions. For example:
        /// <code>
        /// collection('mycontainer.dbxml')/foo
        /// </code>
        /// or:
        /// <code>
        /// doc('dbxml:/mycontainer.dbxml/mydoc.xml')/foo/@attr1='bar'
        /// </code>
        /// The scope of a query can also be controlled by passing an appropriate contextItem object
        /// to <see cref="XQueryExpression.Execute(QueryContext)"/>.
        /// <para>
        /// Note that this method is suitable for performing one-off queries. If you want to execute a query
        /// more than once, you should use <see cref="Prepare(string,QueryContext)"/> to compile the expression, and then use
        /// <see cref="XQueryExpression.Execute(QueryContext)"/> to run it.
        /// </para>
        /// <para>
        /// This method returns an <see cref="XmlResults"/> object. You then iterate over the results set contained
        /// in that object using <see cref="XmlResults.PreviousDocument"/> or
        /// <see cref="XmlResults.PreviousValue"/>, and
        /// <see cref="XmlResults.NextDocument"/> or
        /// <see cref="XmlResults.NextValue"/>.
        /// </para><para>
        /// For more information on querying containers and documents, see @developing-dotnet-apps-with-figaro.md.
        /// </para>
        /// </remarks>
        /// <seealso cref="XmlManager"/>
        /// <seealso cref="XmlResults"/>
        /// <see cref="QueryOptions"/>
        /// <seealso cref="QueryContext"/>
        /// <seealso href="@developing-dotnet-apps-with-figaro.md"/>
        public XmlResults Query(string xQuery, QueryContext context, QueryOptions queryOptions)
        {
            return null;
        }
        /// <summary>
        /// Executes a query in the context of the <see cref="XmlManager"/> object. This method is the equivalent of
        /// calling <see cref="Prepare(string,QueryContext)"/> and
        /// then <see cref="XQueryExpression.Execute(QueryContext)"/> on the prepared query.
        /// </summary>
        /// <param name="transaction">The transaction used to perform the query.</param>
        /// <param name="xQuery">The XQuery expression to execute.</param>
        /// <param name="context">The query context to execute under.</param>
        /// <param name="queryOptions">One or more options to use when executing a query.</param>
        /// <returns>
        /// An <see cref="XmlResults"/> object containing the query results.
        /// </returns>
        /// <remarks>
        /// The scope of the query can be restricted using one of the XQuery navigational functions. For example:
        /// <code>
        /// collection('mycontainer.dbxml')/foo
        /// </code>
        /// or:
        /// <code>
        /// doc('dbxml:/mycontainer.dbxml/mydoc.xml')/foo/@attr1='bar'
        /// </code>
        /// The scope of a query can also be controlled by passing an appropriate contextItem object
        /// to <see cref="XQueryExpression.Execute(QueryContext)"/>.
        /// <para>
        /// Note that this method is suitable for performing one-off queries. If you want to execute a query
        /// more than once, you should use <see cref="Prepare(string,QueryContext)"/> to compile the expression, and then use
        /// <see cref="XQueryExpression.Execute(QueryContext)"/> to run it.
        /// </para>
        /// <para>
        /// This method returns an <see cref="XmlResults"/> object. You then iterate over the results set contained
        /// in that object using <see cref="XmlResults.NextValue"/> and <see cref="XmlResults.PreviousValue"/>.
        /// </para><para>
        /// For more information on querying containers and documents, see @developing-dotnet-apps-with-figaro.md.
        /// </para>
        /// </remarks>
        /// <seealso cref="XmlManager"/>
        /// <seealso cref="XmlTransaction"/>
        /// <seealso cref="XmlResults"/>
        /// <seealso cref="QueryOptions"/>
        /// <seealso cref="QueryContext"/>
        /// @developing-dotnet-apps-with-figaro.md
        public XmlResults Query(XmlTransaction transaction, string xQuery, QueryContext context, QueryOptions queryOptions)
        {
            return null;
        }

        /// <summary>
        /// Re-index an entire container.
        /// </summary>
        /// <param name="name">The name of the container to perform the re-indexing operation against.</param>
        /// <param name="context">The update context to perform the operation with.</param>
        /// <param name="reindexOptions">One or more re-indexing options to use with the operation.</param>
        /// <remarks>
        /// The container should be backed up prior to using this method, as it destroys existing indices
        /// before re-indexing. If the operation fails, and your container is not backed up, you may lose information.
        /// <para>
        /// Use this call to change the type of indexing used for a container between document-level indices
        /// and node-level indices. This method can take a very long time to execute, depending on the size of
        /// the container, and should not be used casually.
        /// </para>
        /// </remarks>
        /// <seealso cref="XmlManager"/>
        /// <seealso cref="UpdateContext"/>
        /// <seealso cref="ReindexOptions"/>
        public void ReindexContainer(string name, UpdateContext context, ReindexOptions reindexOptions) { }

        /// <summary>
        /// Re-index an entire container.
        /// </summary>
        /// <param name="transaction">The transaction to perform the operation with.</param>
        /// <param name="name">The name of the container to perform the re-indexing operation against.</param>
        /// <param name="context">The update context to perform the operation with.</param>
        /// <param name="reindexOptions">One or more re-indexing options to use with the operation.</param>
        /// <remarks>
        /// The container should be backed up prior to using this method, as it destroys existing indices
        /// before re-indexing. If the operation fails, and your container is not backed up, you may lose information.
        /// <para>
        /// Use this call to change the type of indexing used for a container between document-level indices
        /// and node-level indices. This method can take a very long time to execute, depending on the size of
        /// the container, and should not be used casually.
        /// </para>
        /// </remarks>
        /// <seealso cref="XmlManager"/>
        /// <seealso cref="XmlTransaction"/>
        /// <seealso cref="UpdateContext"/>
        /// <seealso cref="ReindexOptions"/>
        public void ReindexContainer(XmlTransaction transaction, string name, UpdateContext context, ReindexOptions reindexOptions) { }
        /// <summary>
        /// Removes the underlying file for the container from the file system.
        /// </summary>
        /// <param name="name">The name of the container to remove.</param>
        /// <remarks>
        /// The container must be closed and must exist, otherwise, the system throws an <see cref="XmlException"/>.
        /// </remarks>
        /// <seealso cref="XmlManager"/>
        /// <seealso cref="Container"/>
        /// <exception cref="XmlException">If the <see cref="Container"/> is open.</exception>
        public void RemoveContainer(string name) { }
        /// <summary>
        /// Removes the underlying file for the container from the file system.
        /// </summary>
        /// <param name="transaction">The transaction to perform the operation with.</param>
        /// <param name="name">The name of the container to remove.</param>
        /// <remarks>
        /// The container must be closed and must exist, otherwise, the system throws an <see cref="XmlException"/>.
        /// </remarks>
        /// <seealso cref="XmlTransaction"/>
        /// <seealso cref="XmlManager"/>
        /// <seealso cref="Container"/>
        /// <exception cref="XmlException">if the <see cref="Container"/> is open, or if the container does not exist.</exception>
        public void RemoveContainer(XmlTransaction transaction, string name) { }
        /// <summary>
        /// Renames the container's underlying file.
        /// </summary>
        /// <param name="oldName">The name of the container you are renaming.</param>
        /// <param name="newName">The name you are renaming the container to.</param>
        /// <remarks>
        /// The container must be closed and must exist, otherwise, the system throws an <see cref="XmlException"/>.
        /// <para>
        /// The container must have been opened at least once; the system throws an exception if
        /// the underlying file has not yet been created.
        /// </para>
        /// </remarks>
        /// <seealso cref="XmlManager"/>
        /// <seealso cref="Container"/>
        public void RenameContainer(string oldName, string newName) { }
        /// <summary>
        /// Renames the container's underlying file.
        /// </summary>
        /// <param name="transaction">The transaction to perform the operation with.</param>
        /// <param name="oldName">The name of the container you are renaming.</param>
        /// <param name="newName">The name you are renaming the container to.</param>
        /// <remarks>
        /// The container must be closed; the system throws an exception if the container is open.
        /// <para>
        /// The container must have been opened at least once; the system throws an exception if
        /// the underlying file has not yet been created.
        /// </para>
        /// </remarks>
        /// <seealso cref="XmlManager"/>
        /// <seealso cref="XmlTransaction"/>
        /// <seealso cref="Container"/>
        public void RenameContainer(XmlTransaction transaction, string oldName, string newName) { }
        /// <summary>
        /// Gets or sets the default flags used for containers opened and created by 
        /// this <see cref="XmlManager"/> object.
        /// </summary>
        /// <value>
        /// The default container settings for all <see cref="Container"/> operations performed with
        /// this <see cref="XmlManager"/> object.
        /// </value>
        /// <seealso cref="XmlManager"/>
        /// <seealso cref="Container"/>
        /// <seealso cref="ContainerConfig"/>
        public ContainerConfig DefaultContainerSettings { get; set; }

        /// <summary>
        /// Gets or sets the default type used for containers opened and created by this <see cref="XmlManager"/> object.
        /// </summary>
        /// <value>
        /// The default container type used by this <see cref="XmlManager"/> object instance.
        /// </value>
        /// <remarks>
        /// If a form of <see cref="CreateContainer(string)"/> or <see cref="XmlManager"/>.openContainer is used that takes a type
        /// argument, the settings provided using this method are ignored.
        /// </remarks>
        /// <seealso cref="XmlContainerType"/>
        /// <seealso cref="XmlManager"/>
        /// <seealso cref="Container"/>
        public XmlContainerType DefaultContainerType { get; set; }

        /// <summary>
        /// Gets or sets the size, in bytes, of the pages used to store documents in the database.
        /// </summary>
        /// <value>
        /// The size, in bytes, of pages used to store documents in the database.
        /// </value>
        /// <remarks>
        /// The size is specified in bytes in the range 512 bytes to 64K bytes. The system selects a page size based
        /// on the underlying file system I/O block size if one is not explicitly set by the application. The
        /// default page size has a lower limit of 512 bytes and an upper limit of 16K bytes. Documents that are
        /// larger than a single page are stored on multiple pages.
        /// <para>
        /// The <c>DefaultPageSize</c> property will only affect containers created after it is set. It
        /// has no effect on existing containers.
        /// </para>
        /// </remarks>
        /// <seealso cref="XmlManager"/>
        /// <seealso cref="Container"/>
        public int DefaultPageSize { get; set; }
        /// <summary>
        /// Gets or sets the integer increment to be used when pre-allocating document IDs for new documents
        /// created by <see cref="Container.PutDocument(string,UpdateContext)"/>.
        /// </summary>
        /// <value>
        /// Gets or sets the number of pre-allocated document IDs for new documents.
        /// </value>
        /// <remarks>
        /// Every document added to an <see cref="Container"/> is assigned an internal unique ID, and BDB XML performs an
        /// internal database operation to obtain these IDs. In order to increase database concurrency and
        /// improve performance of ID allocation, BDB XML pre-allocates a sequence of these numbers. The size
        /// of this sequence is determined by the value specified here. The default ID sequence size is 5.
        /// <para>
        /// Be aware that when a container is closed, any unused IDs in the current sequence are lost. Under
        /// some extreme cases, this can result in a container to which documents can no longer be added. For
        /// example, setting this value to a very large number (such as, say, 1 million) and then repeatedly
        /// opening and closing the container while adding a few documents will eventually cause the container
        /// to run out of IDs. Once out of IDs, the container will never again be able to accept new documents. The
        /// maximum number of IDs that a container has available to it is currently 4 billion.
        /// </para>
        /// 	<note type="warning">
        /// You should almost always leave this value alone. However, if you are loading a large number of
        /// documents to a container all at once, you may find a small performance benefit to setting the sequence
        /// number to a larger value. If you do this, be aware that this value is persistent across container
        /// opens, so you should take care to reset the value to its default once you are done loading
        /// the documents.
        /// </note>
        /// </remarks>
        /// <seealso cref="XmlManager"/>
        /// <seealso cref="Container"/>
        public int DefaultSequenceIncrement { get; set; }
        /// <summary>
        /// Gets or sets the implicit timezone to be used for queries referring to dates and times in the context of the <see cref="XmlManager"/>.
        /// </summary>
        /// <value>The implicit timezone for queries.</value>
        public int ImplicitTimezone { get; set; }
        /// <summary>
        /// Gets the flags used to construct the <see cref="XmlManager"/> object.
        /// </summary>
        /// <value>
        /// The flags used to construct the <see cref="XmlManager"/> object.
        /// </value>
        public ManagerInitOptions XmlManagerFlags { get { return ManagerInitOptions.AllowExternalAccess; } }
        /// <summary>
        /// Gets the home directory for the underlying database environment.
        /// </summary>
        /// <value>The home directory for the underlying database environment.</value>
        /// <remarks>
        /// 	<see cref="Container"/> files are placed relative to this directory unless an absolute path is used
        /// for the container name.
        /// </remarks>
        /// <seealso cref="XmlManager"/>
        /// <seealso cref="Container"/>
        public string Home { get { return string.Empty; } }
        /// <summary>
        /// Truncates all of the databases in the container.
        /// </summary>
        /// <param name="name">The name of the container to truncate.</param>
        /// <param name="context">The context to operate against.</param>
        /// <remarks>
        /// If you created a <see cref="Container"/> object, you must ensure that you run  
        /// <see cref="Container.Close"/> before attempting 
        /// to run this method. You must also ensure no other applications are accessing the container 
        /// when attempting this operation. If either condition exists, an exception will occur. 
        /// The container must be closed for this operation, or an exception will occur.
        /// </remarks>
        /// <seealso cref="XmlManager"/>
        /// <seealso cref="Container"/>
        public void TruncateContainer(string name, UpdateContext context) { }
        /// <summary>
        /// Truncates all of the databases in the container.
        /// </summary>
        /// <param name="transaction">The transaction used to perform the operation.</param>
        /// <param name="name">The name of the container to truncate.</param>
        /// <param name="context">The context to operate against.</param>
        /// <remarks>
        /// The container must be closed; the system throws an exception if the container is open.
        /// </remarks>
        /// <seealso cref="XmlTransaction"/>
        /// <seealso cref="XmlManager"/>
        /// <seealso cref="Container"/>
        public void TruncateContainer(XmlTransaction transaction, string name, UpdateContext context) { }
        /// <summary>
        /// Upgrades the underlying Berkeley DB container from a previous version to the current version.
        /// </summary>
        /// <param name="name">The name of the container to upgrade.</param>
        /// <param name="context">The update context to perform the upgrade against.</param>
        /// <remarks>
        /// A Berkeley DB upgrade is first performed, and then the Berkeley DB 
        /// container is upgraded. If no upgrade is needed, then no changes are made.
        /// <note type="warning">
        /// 		<see cref="Container"/> upgrades are done in place and are destructive. For example, if pages need to be allocated and no
        /// disk space is available, the container may be left corrupted. Backups should be made before containers are
        /// upgraded. See @upgrading-databases.md for more information.
        /// </note>
        /// The container must be closed; the system throws an exception if the container is open.
        /// </remarks>
        /// <seealso cref="UpdateContext"/>
        /// <seealso href="@upgrading-databases.md"/>
        public void UpgradeContainer(string name, UpdateContext context)
        {
        }
        /// <summary>
        /// Checks that the container data files are not corrupt, and optionally writes the salvaged container
        /// data to the specified file output.
        /// </summary>
        /// <param name="containerName">The path and name of the container to verify.</param>
        /// <param name="dumpFile">The path and name of the file to dump verification output into.</param>
        /// <param name="flags">Salvage options to use against the container.</param>
        /// <remarks>
        /// The container must be closed; the system throws an exception if the container is open.
        /// <para>
        /// The container must have been opened at least once; the system throws an exception if the
        /// underlying files have not yet been created.
        /// </para>
        /// </remarks>
        /// <seealso cref="SalvageOptions"/>
        public void VerifyContainer(string containerName, string dumpFile, SalvageOptions flags) { }


        /// <summary>
        /// Checks that the container data files are not corrupt, and optionally writes the salvaged container
        /// data to the console.
        /// </summary>
        /// <param name="containerName">The path and name of the container to verify.</param>
        /// <param name="flags">Salvage options to use against the container.</param>
        /// <remarks>
        /// The container must be closed; the system throws an exception if the container is open.
        /// <para>
        /// The container must have been opened at least once; the system throws an exception if the
        /// underlying files have not yet been created.
        /// </para>
        /// </remarks>
        /// <seealso cref="SalvageOptions"/>
        public void VerifyContainer(string containerName, SalvageOptions flags) { }

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            return;
        }

        #endregion
    } // class
}
