// ReSharper disable EmptyConstructor
using System;

namespace Figaro    
{
    /// <summary>
    /// The <see cref="XmlExternalFunction"/> class is used to implement XQuery
    /// extension functions in .NET Framework languages.
    /// </summary>
    /// <remarks>
    /// An implementor creates one or more subclasses of 
    /// <see cref="XmlExternalFunction"/> and returns the appropriate instance of the
    /// class  from the <see cref="XQueryResolver.ResolveExternalFunction"/> method. 
    /// <para>
    /// When the external method is called by XQuery, it will trigger the 
    /// <see cref="Execute(XmlManager,XmlArguments)"/> method, passing in the <see cref="XmlManager"/> in which the operation is 
    /// running, and the function arguments (contained in the <see cref="XmlArguments"/> instance). The function requires that an 
    /// <see cref="XmlResults"/> argument be returned; however, a <c>null</c> value can be returned if appropriate. 
    /// </para>
    /// </remarks>
    /// <seealso cref="XQueryResolver.ResolveExternalFunction"/>    
    public abstract class XmlExternalFunction : IDisposable 
    {
        /// <summary>
        /// Initializes a new instance of the XmlExternalFunction class.
        /// </summary>
        protected XmlExternalFunction()
        {
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose() {}

        /// <summary>
        /// This method is called from within XQuery to execute the function for which the instance was implemented. 
        /// </summary>
        /// <param name="txn">The <see cref="XmlTransaction"/> in which the operation is running.</param>
        /// <param name="mgr">The <see cref="XmlManager"/> controlling the operation.</param>
        /// <param name="args"><see cref="XmlArguments"/> object that can be used to retrieve the arguments passed to the operation.</param>
        /// <returns>An <see cref="XmlResults"/> object used by the XQuery operation for further processing, or <c>null</c>.</returns>
        public abstract XmlResults Execute(XmlTransaction txn, XmlManager mgr, XmlArguments args);

        /// <summary>
        /// This method is called from within XQuery to execute the function for which the instance was implemented. 
        /// </summary>
        /// <param name="mgr">The <see cref="XmlManager"/> controlling the operation.</param>
        /// <param name="args"><see cref="XmlArguments"/> object that can be used to retrieve the arguments passed to the operation.</param>
        /// <returns>An <see cref="XmlResults"/> object used by the XQuery operation for further processing, or <c>null</c>.</returns>
        public abstract XmlResults Execute(XmlManager mgr, XmlArguments args);
    }
}
