// Copyright (C) 2009 Endpoint Systems.  All rights reserved.

using System;
// ReSharper disable UnusedParameter.Local



namespace Figaro    //namespace Figaro    
{
    /// <summary>
    /// Container configuration settings.
    /// </summary>
    /// <remarks>
    /// The ContainerConfig class encapsulates all the properties with which a 
    /// container can be created or opened. It is passed as an argument to 
    /// <see cref="XmlManager.CreateContainer(string)"/> and 
    /// <see cref="XmlManager.OpenContainer(string)"/> as well as other  methods that
    /// previously accepted flag parameters.
    /// <para>
    /// ContainerConfig conforms to the properties found in the <c>ContainerElement</c> found in 
    /// the  <c>Figaro.Configuration</c> namespace. This allows for fast, factory-style <see cref="Container"/> 
    /// creation while minimizing the amount of parameters needed for <see cref="Container"/> creation and open
    /// operations.
    /// <note type="implementnotes">
    /// If a <c>ContainerConfig</c> object is passed to an <see cref="XmlManager"/> object where a parameter in the 
    /// method signature is also found in the <c>ContainerConfig</c> properties, the parameter will be used and the <c>ContainerConfig</c>
    /// property will be ignored.
    /// </note>
    /// </para>
    /// </remarks>
    public class ContainerConfig : IDisposable
    {
        /// <summary>
        /// Gets or sets the container configuration alias.
        /// </summary>
        //public string Alias { get; set; }

        /// <summary>
        /// Gets or sets the container path and file name.
        /// </summary>
        /// <remarks>
        /// This property can be set using the <c>Figaro.Configuration</c> namespace, or when manually creating a 
        /// new <see cref="ContainerConfig"/> instance. 
        /// <note type="implementnotes">
        /// If a <c>ContainerConfig</c> object is passed to an <see cref="XmlManager"/> object where a parameter in the 
        /// method signature is also found in the <c>ContainerConfig</c> properties, the parameter will be used and the <c>ContainerConfig</c>
        /// property will be ignored.
        /// </note>
        /// </remarks>
        public string Path { get; set; }
        /// <summary>
        /// Gets or sets the container configuration instance name.
        /// </summary>
        /// <remarks>
        /// This property is set and used by the <c>Figaro.Configuration</c> and <c>Figaro.Web.Configuration</c> 
        /// library objects at creation, and used for reference purposes only. This property is not required 
        /// and can safely be set to <c>null</c> if <see cref="ContainerConfig"/> is created programmatically.
        /// </remarks>
        public string ConfigurationName { get; set; }

        /// <summary>
        /// Initializes a new instance of the ContainerConfig class.
        /// </summary>
        public ContainerConfig()
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether a container can be created during a call to <see cref="XmlManager.OpenContainer(string)"/> if 
        /// it does not already exist. The default value is <c>false</c>.
        /// </summary>
        /// <remarks>
        /// If set to <c>false</c> an exception will be thrown when
        ///  <see cref="XmlManager.OpenContainer(string)"/> is 
        /// called for a container that does not exist.
        /// </remarks>
        public bool AllowCreate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to validate XML if it refers to a DTD or XML Schema. 
        /// </summary>
        /// <remarks>
        /// If enabled validation is only performed on document insertion or update and 
        /// not when modified via XQuery Update expressions. The default value is <c>false</c> 
        /// and is used by container open.
        /// <para>Set to <c>true</c> to do validation.</para>
        /// </remarks>
        public bool AllowValidation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to do checksum verification of pages read into the cache 
        /// from the backing file store. 
        /// </summary>
        /// <remarks>
        /// Figaro uses the SHA1 Secure Hash 
        /// Algorithm if encryption is configured and a general hash algorithm if it 
        /// is not. The default value is <c>false</c>.        /// </remarks>
        public bool Checksum { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not to enable container compression. 
        /// </summary>
        /// <remarks>
        /// Compression is only used by whole document storage containers. Compression can only be 
        /// set at creation time; afterward, the container must always be open with the 
        /// same setting.    
        /// </remarks>
        public bool CompressionEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to encrypt the database using the cryptographic 
        /// password specified to <see cref="FigaroEnv.SetEncryption"/>.
        /// </summary>
        /// <remarks>
        /// The default setting is <c>false</c>.
        /// </remarks>
        public bool Encrypted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to throw an exception if the container is already created.
        /// </summary>
        /// <remarks>
        /// If set to <c>true</c> <see cref="XmlManager.OpenContainer(string)"/> and 
        /// <see cref="XmlManager.CreateContainer(string)"/> will throw exceptions if 
        /// the container already exists and <see cref="AllowCreate"/> has been set to 
        /// <c>true</c>. 
        /// <para>The default value is <c>false</c>.</para>
        /// </remarks>
        public bool ExclusiveCreate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to open the container with multi-version currency control (MVCC).
        /// </summary>
        /// <remarks>
        /// If set to <c>true</c> then the database will be opened with 
        /// support for multi-version concurrency control. This will cause updates to 
        /// the container to follow a copy-on-write protocol which is required to 
        /// support snapshot isolation. 
        /// <para>
        /// The <see cref="EnvConfig.MultiVersion"/> flag requires that the container 
        /// be transactionally protected during its open. </para>
        /// <para>
        /// The default value is <c>false</c>.
        /// </para>
        /// </remarks>
        public bool MultiVersion { get; set; }

        /// <summary>Gets or sets a value indicating whether the container will be mapped into process memory.</summary>
        /// <remarks>
        /// If set to <c>true</c> then the container will not be mapped into 
        /// process memory (see the <see cref="FigaroEnv.MaxFileMapSize"/> property for further information).
        /// </remarks>
        public bool NoMMap { get; set; }

        /// <summary>Gets or sets a value indicating whether to open the container with read-only access.</summary>
        /// <remarks>
        /// If set to <c>true</c>, the container is opened for reading only. 
        /// <para>Any attempt to modify items in the container will fail regardless of the 
        /// permissions of the underlying files. If set to <c>false</c> then the container 
        /// can be modified. The default value is <c>false</c> and can only be set to <c>true</c> for existing containers.
        /// </para>
        /// </remarks>
        public bool ReadOnly { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not to enable 'dirty' reads.
        /// </summary>
        /// <remarks>
        /// If set to <c>true</c> then the container will support degree 1 isolation; that is, read operations 
        /// may return information that has been modified by another transaction but has not yet been 
        /// committed. This setting should be used rarely if at all. The default value is <c>false</c>.
        /// </remarks>
        public bool ReadUncommitted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the container handle is marked as free-threaded.
        /// </summary>
        /// <remarks>
        /// If set to <c>true</c>, this property causes the container handle to be free-threaded; that is, concurrently usable by 
        /// multiple threads in the address space. If multiple threads access a container that does not have this 
        /// property set the results are unpredictable. The default value is <c>false</c>.
        /// </remarks>
        public bool Threaded { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not to enable container transactions.
        /// </summary>
        /// <remarks>
        /// If set to <c>true</c> it causes the container to use transactions. Even if the environment has been configured for transactions, 
        /// this property must be used in order to create or open a transactional container. In other words, a transactional 
        /// environment can support both transactional and non-transactional containers. If the environment has not also been 
        /// configured for transactions then use of this property when opening a container will result in an exception. 
        /// <para>If this 
        /// property is set an <see cref="XmlTransaction"/> object can and should be passed to any method that supports 
        /// it. If a container is transactional and an explicit <see cref="XmlTransaction"/> object is not passed to a 
        /// modifying method (e.g. <see cref="Container.PutDocument(string,Figaro.UpdateContext)"/>), an 
        /// exception will occur indicating an <see cref="XmlTransaction"/> object is required. 
        /// </para><para>
        /// The default value for this property is <c>false</c> and it affects containers being created and containers that already exist.</para>
        /// </remarks>
        public bool Transactional { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether transactions are durable for the specified container.
        /// </summary>
        /// <remarks>
        /// If set to <c>true</c>, Figaro will not write log records for this database. This means that updates of this database exhibit the 
        /// ACI (atomicity, consistency, and isolation) properties, but not D (durability); that is, database integrity will be maintained, but 
        /// if the application or system fails, integrity will not persist. The database file must be verified and/or restored from backup after 
        /// a failure. The default value is <c>false</c>.
        /// </remarks>
        public bool TransactionNotDurable { get; set; }

        /// <summary>
        /// Gets or sets configuration state for setting up node or document indexing.
        /// </summary>
        /// <remarks>
        /// Sets whether the index targets reference nodes or documents. The default setting is <see cref="ConfigurationState.UseDefault"/>. This value is ignored 
        /// unless the container is being created or re-indexed.
        /// <para> If set to <see cref="ConfigurationState.On"/> it causes the indexer to create index targets that reference nodes rather than documents. This 
        /// allows index lookups during query processing to more efficiently find target nodes and avoid walking the document tree. It can apply 
        /// to both container types, and is the default for containers of type <see cref="XmlContainerType.NodeContainer"/>.</para>
        /// <para> If set to <see cref="ConfigurationState.Off"/> it causes the indexer to create index targets that reference documents 
        /// rather than nodes. This can be more desirable for simple queries that only need to return documents and 
        /// do relatively little navigation during querying. It can apply to both container types, and is the 
        /// default for containers of type <see cref="XmlContainerType.WholeDocContainer"/>.</para>
        /// <para>
        /// If set to <see cref="ConfigurationState.Off"/>, then the container type will decide whether 
        /// nodes or documents are referenced.</para>
        /// </remarks>
        public ConfigurationState IndexNodes { get; set; }

        /// <summary>
        /// Gets or sets the container structural statistics.
        /// </summary>
        /// <remarks>
        /// Gets or sets whether the structural statistics are stored in the container. These statistics are used in query optimization. The default 
        /// setting is <see cref="ConfigurationState.UseDefault"/> and the default is to use statistics. This value is ignored unless the 
        /// container is being created or re-indexed.
        /// <para> Structural statistics information is very useful for cost based query optimization. Containers created with these statistics 
        /// will take slightly longer to load and update, since the statistics must also be updated. In addition the statistics 
        /// affect the concurrent behavior in the face of updates.</para>
        /// <para> If the state is set to <see cref="ConfigurationState.Off"/> then the container is created without structural 
        /// statistics. If the state is set to <see cref="ConfigurationState.On"/> or <see cref="ConfigurationState.UseDefault"/> the container 
        /// is created with structural statistics.</para>
        /// </remarks>
        public ConfigurationState Statistics { get; set; }

        /// <summary>
        /// Gets or sets the size, in bytes, of the database pages used to store node and document data.
        /// </summary>
        /// <remarks>
        /// The <see cref="PageSize"/> property gets/sets the size of the pages used to store documents in the database. The size 
        /// is specified in bytes in the range 512 bytes to 64K bytes. If no page size is specified the system will create 
        /// containers with page size 8192 for <see cref="XmlContainerType.NodeContainer"/> storage containers and 16384 for 
        /// <see cref="XmlContainerType.WholeDocContainer"/> storage containers. 
        /// <para> Page size affects the amount of 
        /// I/O performed as well as granularity of locking as Figaro performs page-level locking. These needs 
        /// are often at odds with one another. </para>
        /// <para>The page size cannot be changed once a container has been created. This value is ignored unless the container is being created.</para>
        /// </remarks>
        public ushort PageSize { get; set; }

        /// <summary>
        /// Gets or sets the integer increment to be used when pre-allocating document ids for new documents 
        /// created by <see cref="Container.PutDocument(string,Figaro.UpdateContext)"/>.
        /// </summary>
        /// <remarks>
        /// Every document added to a <see cref="Container"/> is assigned an internal unique ID, and BDB XML performs an 
        /// internal database operation to obtain these IDs. In order to increase database concurrency and improve 
        /// performance of ID allocation, BDB XML pre-allocates a sequence of these numbers. The size of this sequence 
        /// is determined by the value specified here. The default ID sequence size is 5.
        /// <para>
        /// Be aware that when a container is closed, any unused IDs in the current sequence are lost. Under some extreme 
        /// cases, this can result in a container to which documents can no longer be added. For example, setting this value 
        /// to a very large number (such as, say, 1 million) and then repeatedly opening and closing the container while 
        /// adding a few documents may eventually cause the container to run out of IDs. Once out of IDs, the container 
        /// will never again be able to accept new documents. However, document IDs are 64-bit 
        /// quantities so this is extremely unlikely.
        /// </para><para>
        /// You should almost always leave this value alone. However, if you are loading a large number of documents to a 
        /// container all at once, you may find a small performance benefit to setting the sequence number to a larger value. 
        /// If you do this, be aware that this value is persistent across container opens, so you should take care to reset the 
        /// value to its default once you are done loading the documents.
        /// </para>
        /// </remarks>
        public uint SequenceIncrement { get; set; }

        /// <summary>
        /// Gets or sets whether the container was built to handle documents or nodes.
        /// </summary>
        /// <remarks>
        /// Sets the type of container to be created. The default value is <see cref="XmlContainerType.NodeContainer"/>. This value is 
        /// ignored if the container already exists.
        /// </remarks>
        public XmlContainerType ContainerType { get; set; }

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

    }
}

