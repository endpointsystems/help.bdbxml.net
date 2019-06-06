// ReSharper disable UnusedParameter.Global
using System;
// ReSharper disable UnusedAutoPropertyAccessor.Local
namespace Figaro    
{
    /// <summary>
    /// Provides stack trace information about an executing query to the methods of <see cref="XmlDebugListener"/>. 
    /// </summary>
    /// <remarks>
    /// <see cref="XmlStackFrame"/> objects describe a stack frame in the stack trace and includes a pointer to the previous stack frame.
    /// </remarks>
    public class XmlStackFrame
    {
        /// <summary>
        /// Prevents a default instance of the XmlStackFrame class from being created.
        /// </summary>
        private XmlStackFrame() {}

        /// <summary>
        /// Gets the URI of the query represented by this stack frame.
        /// </summary>
        public string QueryFile { get; private set; }

        /// <summary>
        /// Gets the query plan of the sub-expression represented by this stack frame.
        /// </summary>
        public string QueryPlan { get; private set; }

        /// <summary>
        /// Gets the line number of the position in the query represented by this stack frame.
        /// </summary>
        public uint QueryLine { get; private set; }

        /// <summary>
        /// Gets a pointer to the previous stack frame or 0 if this is the first stack frame.
        /// </summary>
        public XmlStackFrame PreviousStackFrame { get; private set; }

        /// <summary>
        /// Prepares and executes the query given in the dynamic context of the stack frame. 
        /// </summary>
        /// <remarks>
        /// This can be used to examine the value of the context item (".") or variables ("$var") for a given stack frame as 
        /// well as other parts of the dynamic context. 
        /// <note type="caution">
        /// Users may find that the context item and variables present in their original query do not exist during query 
        /// execution due to optimization performed by DB XML.
        /// </note>
        /// </remarks>
        /// <param name="queryString">The query string to execute.</param>
        /// <returns>A <see cref="XmlResults"/> object containing query results.</returns>
        public XmlResults Query(string queryString)
        {
            return null; 
        }
    }
    /// <summary>
    /// Used by implementers of <see cref="XmlExternalFunction"/> to access function arguments passed to 
    /// the <see cref="M:Figaro.XmlExternalFunction.Execute(Figaro.XmlManager,Figaro.XmlArguments)"/> method.
    /// </summary>
    public class XmlArguments : IDisposable 
    {
        /// <summary>
        /// Prevents a default instance of the XmlArguments class from being created.
        /// </summary>
        private XmlArguments() {}

        /// <summary>
        /// Gets the number of arguments contained in the <see cref="XmlArguments"/> object.
        /// </summary>
        public uint Arguments { get; private set; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose() {}
        
        /// <summary>
        /// Retrieve the <see cref="XmlResults"/> argument at the specified argument index.
        /// </summary>
        /// <param name="index">The argument index.</param>
        /// <returns>The argument, wrapped in an <see cref="XmlResults"/> object.</returns>
        public XmlResults GetArguments(uint index)
        {
            return null; 
        }
    }
}
