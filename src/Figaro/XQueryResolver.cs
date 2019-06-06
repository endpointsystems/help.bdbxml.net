// ReSharper disable UnusedParameter.Local
// ReSharper disable UnusedAutoPropertyAccessor.Local




namespace Figaro    
{
    using System;
    
    /// <summary>
    /// Provides a base class that is used for file resolution policy. 
    /// Implementations of this class are used for URI resolution, or for public and system id 
    /// resolution. <see cref="XQueryResolver"/> implementations are identified for use using 
    /// <see cref="XmlManager.RegisterResolver"/>.
    /// </summary>
    /// <remarks>
    /// The <see cref="XQueryResolver"/> class allows applications to provide named access to 
    /// application-specific objects, such as documents, collections of documents, DTDs, and 
    /// XML schema. Figaro can resolve references to names within a container, or a file 
    /// system; applications can create <see cref="XQueryResolver"/> instances that can resolve 
    /// entities in other locations.
    ///  <para>If an application uses multiple threads, custom implementations of 
    /// <c>XQueryResolver</c> must be free threaded, and allow multiple, 
    /// simultaneous calls for resolution.</para>
    ///  <para>For an example of a <see cref="XQueryResolver"/> implementation, 
    /// see the SDK samples.</para>
    /// </remarks>
    public abstract class XQueryResolver : IDisposable 
    {
        /// <summary>
        /// Initializes a new instance of the XQueryResolver class.
        /// </summary>
        /// <param name="uri">The unique identifier for the resolver.</param>
        protected XQueryResolver(Uri uri) {}

        /// <summary>
        /// Gets the unique identifier for the resolver.
        /// </summary>
        public Uri Uri { get; private set; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose() {}

        /// <summary>
        /// Resolve a custom collection external to XQuery and the Figaro XML Database containers.
        /// </summary>
        /// <param name="txn">If a transaction is in force, a reference to the <see cref="XmlTransaction"/> object; otherwise, <c>null</c>.</param>
        /// <param name="mgr">The owning <see cref="XmlManager"/> object.</param>
        /// <param name="uri">The URI of the collection to resolve.</param>
        /// <param name="collection">The <see cref="XmlResults"/> object to add collection items to.</param>
        /// <returns><c>true</c> if the collection was successfully resolved.</returns>
        public abstract bool ResolveCollection(XmlTransaction txn, XmlManager mgr, string uri, XmlResults collection);

        /// <summary>
        /// Resolve a document referenced in an XQuery query.
        /// </summary>
        /// <param name="txn">If a transaction is in force, a reference to the <see cref="XmlTransaction"/> object; otherwise, <c>null</c>.</param>
        /// <param name="mgr">The owning <see cref="XmlManager"/> object.</param>
        /// <param name="uri">The URI of the <see cref="XmlDocument"/> to resolve.</param>
        /// <returns>The referenced <see cref="XmlDocument"/>, or <c>null</c> if the resolver does not resolve to the particular reference.</returns>
        public abstract XmlDocument ResolveDocument(XmlTransaction txn, XmlManager mgr, string uri);

        /// <summary>
        /// Resolve an external entity in an XQuery query.
        /// </summary>
        /// <param name="txn">If a transaction is in force, a reference to the <see cref="XmlTransaction"/> object; otherwise, <c>null</c>.</param>
        /// <param name="mgr">The owning <see cref="XmlManager"/> object.</param>
        /// <param name="systemId">The system ID of the referenced entity.</param>
        /// <param name="publicId">The public ID of the referenced entity.</param>
        /// <returns>The referenced entity, or <c>null</c> if the resolver does not resolve the particular reference.</returns>
        public abstract XmlInputStream ResolveEntity(XmlTransaction txn, XmlManager mgr, string systemId, string publicId);

        /// <summary>
        /// Resolve the URI, name and number of arguments to an <see cref="XmlExternalFunction"/>
        /// implementation. If the external function cannot be resolved by this
        /// <see cref="XQueryResolver"/>, then the method should return <c>null</c>. 
        /// The returned <see cref="XmlExternalFunction"/>
        /// will be adopted by Figaro, and disposed of when it's been used.
        /// </summary>
        /// <remarks>
        /// <note type="caution">
        /// If the resolver successfully locates the <see cref="XmlExternalFunction"/>, return a new instance 
        /// of the <see cref="XmlExternalFunction"/> object. 
        /// </note>
        /// </remarks>
        /// <param name="txn">If a transaction is in force, a reference to the <see cref="XmlTransaction"/> object; otherwise, <c>null</c>.</param>
        /// <param name="mgr">The <see cref="XmlManager"/> object associated with the operation.</param>
        /// <param name="uri">The URI of the function to resolve.</param>
        /// <param name="name">The XQuery function name.</param>
        /// <param name="numArgs">The number of XQuery function arguments.</param>
        /// <returns>A new <see cref="XmlExternalFunction"/> instance, or <c>null</c> if the function fails to resolve.</returns>
        public abstract XmlExternalFunction ResolveExternalFunction(XmlTransaction txn, XmlManager mgr, string uri, string name, ulong numArgs);

        /// <summary>
        /// When implemented, should resolve a module location (URI) and namespace to a 
        /// new <see cref="XmlInputStream"/>. If the location and namespace cannot be resolved by 
        /// this resolver, this method should return <c>null</c>. 
        /// </summary>
        /// <remarks>
        /// The <see cref="XmlInputStream"/> object will be deleted by the caller, and should not be reused.
        /// </remarks>
        /// <param name="txn">If a transaction is in force, a reference to the <see cref="XmlTransaction"/> object; otherwise, <c>null</c>.</param>
        /// <param name="mgr">The <see cref="XmlManager"/> object associated with the operation.</param>
        /// <param name="moduleLocation">The module location.</param>
        /// <param name="nameSpace">The module namespace.</param>
        /// <returns>an <see cref="XmlInputStream"/> instance containing the loaded XQuery module.</returns>
        public abstract XmlInputStream ResolveModule(XmlTransaction txn, XmlManager mgr, string moduleLocation, string nameSpace);

        /// <summary>
        /// Resolve the XQuery module location(s) according to the <paramref name="nameSpace"/>
        /// </summary>
        /// <param name="txn">If a transaction is in force, a reference to the <see cref="XmlTransaction"/> object; otherwise, <c>null</c>.</param>
        /// <param name="mgr">The <see cref="XmlManager"/> object associated with the operation.</param>
        /// <param name="nameSpace">The namespace of the module(s) to resolve.</param>
        /// <param name="moduleLocations">The <see cref="XmlResults"/> object to store the location(s) of the XQuery module(s).s</param>
        /// <returns><c>true</c> if the resolver resolves the location(s); otherwise, <c>false</c> is returned.</returns>
        public abstract bool ResolveModuleLocation(XmlTransaction txn, XmlManager mgr, string nameSpace, XmlResults moduleLocations);

        /// <summary>
        /// Resolve schemas referenced in XQuery queries.
        /// </summary>
        /// <param name="txn">If a transaction is in force, a reference to the <see cref="XmlTransaction"/> object; otherwise, <c>null</c>.</param>
        /// <param name="mgr">The <see cref="XmlManager"/> object associated with the operation.</param>
        /// <param name="schemaLocation">The location of the resolved schema.</param>
        /// <param name="nameSpace">The namespace of the module(s) to resolve.</param>
        /// <returns>The <see cref="XmlInputStream"/> containing the referenced schema, or <c>null</c> if it does not resolve.</returns>
        public abstract XmlInputStream ResolveSchema(XmlTransaction txn, XmlManager mgr, string schemaLocation, string nameSpace);
    }
}
