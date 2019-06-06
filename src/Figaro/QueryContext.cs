// ReSharper disable EventNeverSubscribedTo.Global
// ReSharper disable UnusedParameter.Global
#pragma warning disable 67
using System;
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Figaro    
{
    /// <summary>
    /// Encapsulates the context within which a query is performed against a
    /// <see cref="Container"/>. The context includes namespace mappings,
    /// variable bindings, and flags that indicate how the query result set should be determined
    /// and returned to the caller.
    /// </summary>
    /// <remarks>
    /// Multiple queries can be executed within the same
    /// <see cref="QueryContext"/>; however,
    /// <see cref="QueryContext"/> is not thread-safe, and can only be used
    /// by one thread at a time.
    /// <para>
    /// 		<see cref="QueryContext"/> objects are instantiated using
    /// <see cref="XmlManager.CreateQueryContext()"/>.
    /// </para><para>
    /// The XQuery syntax permits expressions to refer to namespace prefixes, and to define them. The
    /// <see cref="QueryContext"/> class provides namespace management methods
    /// so that the caller may manage the namespace prefix to URI mapping outside of a query. By default the
    /// prefix <c>dbxml</c> is defined to be "http://www.sleepycat.com/2002/dbxml".
    /// </para><para>
    /// The XQuery syntax also permits expressions to refer to externally defined variables. The
    /// <see cref="QueryContext"/> class provides methods that allow the
    /// caller to manage the externally-declared variable to value bindings.
    /// </para>
    /// </remarks>
    /// <threadsafety static="true" instance="false"/>
    /// <seealso cref="Container"/>
    /// <seealso cref="XmlManager.CreateQueryContext()"/>
    public sealed class QueryContext : IDisposable
    {

        /// <summary>
        /// Allows the application to associate an <see cref="XmlDebugListener"/> with a query context in order to debug queries.
        /// </summary>
        /// <remarks>
        /// In order to prepare a query that contains debugging information an <see cref="XmlDebugListener"/> must be set on the 
        /// <see cref="QueryContext"/> used when the query is prepared. 
        /// <para>A 
        /// different <see cref="XmlDebugListener"/> object (or none) can subsequently be set for query evaluation.</para>  
        /// </remarks>
        /// <param name="listener">The debug listener to assign.</param>
        public void SetDebugListener(XmlDebugListener listener) {}
        /// <summary>
        /// Gets the <see cref="XmlDebugListener"/> set on the <see cref="QueryContext"/>.
        /// </summary>
        public XmlDebugListener DebugListener { get; private set; }

        /// <summary>
        /// Prevents a default instance of the QueryContext class from being created.
        /// </summary>
        private QueryContext() {}

        /// <summary>
        /// Removes all namespace mappings from the query context.
        /// </summary>
        public void ClearNamespaces() {}
        /// <summary>
        /// Interrupts a running query that was started using this object.
        /// </summary>
        /// <remarks>
        /// This call must be made in the context of another thread of control in the application. The query
        /// will terminate and throw an exception with code
        /// <see cref="FigaroExceptionCategory.OperationInterrupted"/>.
        /// </remarks>
        public void InterruptQuery() { }
        /// <summary>
        /// Remove the specified namespace.
        /// </summary>
        /// <param name="prefix">The namespace prefix to look for.</param>
        /// <remarks>
        /// Removes the namespace prefix to URI mapping for the specified prefix. A call to this method with a
        /// prefix that has no existing mapping is ignored.
        /// </remarks>
        public void RemoveNamespace(string prefix) { }

        /// <summary>
        /// Gets or sets default collection for <c>fn:collection()</c>.
        /// </summary>
        /// <value>The default collection.</value>
        /// <remarks>
        /// The default collection is that which is used by <c>fn:collection()</c> without any arguments in
        /// an XQuery expression.
        /// </remarks>
        public string DefaultCollection { get; set; }

        /// <summary>
        /// Gets or sets the query timeout value.
        /// </summary>
        /// <value>The query timeout value.</value>
        /// <remarks>
        /// The query timeout that will cause a query using this <see cref="QueryContext"/>
        /// to terminate if the query time exceeds the timeout value. The execution of the query will throw
        /// a <see cref="FigaroException"/> with an exception code of
        /// <see cref="FigaroExceptionCategory.OperationTimeout"/> if the timeout occurs.
        /// </remarks>
        /// <seealso cref="FigaroException"/>
        /// <seealso cref="QueryContext"/>
        public uint QueryTimeoutSeconds { get; set; }
        /// <summary>
        /// Gets or sets the base URI used for relative paths in query expressions.
        /// </summary>
        /// <value>The base URI for relative paths in a query expression.</value>
        /// <remarks>
        /// For example, a base URI of 'file:/// export/expression/', and a relative path
        /// of '../another/expression', resolves to 'file:/// export/another/expression'.
        /// </remarks>
        public string BaseUri { get; set; }

        /// <summary>
        /// Gets or sets the query evaluation type to "eager" or "lazy".
        /// </summary>
        /// <value>The query evaluation type.</value>
        /// <remarks>
        /// Eager evaluation means that the whole query is executed and its resultant values derived and stored in-memory
        /// before evaluation of the query is completed. Lazy evaluation means that minimal processing
        /// is performed before the query is completed, and the remaining processing is deferred until
        /// the result set is enumerated.
        /// <para>
        /// As each call to <see cref="XmlResults.NextDocument"/> or
        /// <see cref="XmlResults.NextValue"/> is called the next
        /// resultant value is determined.
        /// </para>
        /// </remarks>
        /// <seealso cref="XmlResults"/>
        /// <seealso cref="EvaluationType"/>
        public EvaluationType EvaluationType { get; set; }
        /// <summary>
        /// Maps the specified URI to the specified namespace prefix.
        /// </summary>
        /// <param name="prefix">The URI prefix.</param>
        /// <param name="uri">The URI to map the prefix to.</param>
        public void SetNamespace(string prefix, string uri) { }
        /// <summary>
        /// Returns the namespace URI for the specified prefix.
        /// </summary>
        /// <param name="prefix">The prefix of the URI to return.</param>
        /// <returns>
        /// The URI mapped to the <c>prefix</c>, or <c>null</c> if no URI is defined for the prefix.
        /// </returns>
        /// <remarks>An empty string is returned if no URI is defined for the prefix.</remarks>
        public string GetNamespace(string prefix)
        {
            return string.Empty;
        }

        /// <summary>
        /// Creates an externally-declared XQuery variable by binding the specified value, or sequence of
        /// values, to the specified variable name.
        /// </summary>
        /// <param name="name">The name of the variable to bind. Within the XQuery query,
        /// the variable can be referenced using the normal <c>$name</c> syntax.</param>
        /// <param name="value">The variable value to bind to the named variable.</param>
        /// <remarks>
        /// This method may be called at any time during the life of the application.
        /// </remarks>
        /// <seealso cref="XmlValue"/>
        public void SetVariableValue(string name, XmlValue value) { }
        /// <summary>
        /// Returns the <see cref="XmlValue"/> value that is bound to the
        /// specified variable.
        /// </summary>
        /// <param name="name">The name of the variable to look up.</param>
        /// <returns>
        /// An <see cref="XmlValue"/> object.
        /// </returns>
        /// <remarks>
        /// If there is no value binding then the function returns <c>false</c> and value is set to a <see langword="null"/> value
        /// (<c>XmlValue.IsNull</c> returns <c>true</c>).
        /// </remarks>
        /// <seealso cref="XmlValue"/>
        public XmlValue GetVariableXmlValue(string name)
        {
            return null;
        }
        /// <summary>
        /// Returns the <see cref="XmlResults"/> value that is bound
        /// to the specified variable.
        /// </summary>
        /// <param name="name">The name of the variable to look up.</param>
        /// <returns>
        /// An <see cref="XmlResults"/> object.
        /// </returns>
        /// <remarks>
        /// If there is no value binding then the function returns <c>false</c> and value is set to a <see langword="null"/> value
        /// (<see cref="XmlResults.IsNull"/> returns <c>true</c>).
        /// </remarks>
        /// <seealso cref="XmlResults"/>
        public XmlResults GetVariableXmlResults(string name)
        {
            return null;
        }

        /// <summary>
        /// Creates an externally-declared XQuery variable by binding the specified
        /// <see cref="XmlResults"/> value, or sequence of
        /// values, to the specified variable name.
        /// </summary>
        /// <param name="name">The name of the variable to bind. Within the XQuery query,
        /// the variable can be referenced using the normal <c>$name</c> syntax.</param>
        /// <param name="value">A sequence of results to bind to the named variable.</param>
        /// <remarks>
        /// This method may be called at any time during the life of the application.
        /// </remarks>
        /// <seealso cref="XmlResults"/>
        public void SetVariableValue(string name, XmlResults value) { }
        /// <summary>
        /// Creates an externally-declared XQuery variable by binding the specified <see cref="System.String"/> value,
        /// or sequence of values, to the specified variable name.
        /// </summary>
        /// <param name="name">The name of the variable to bind. Within the XQuery query,
        /// the variable can be referenced using the normal <c>$name</c> syntax.</param>
        /// <param name="value">A sequence of results to bind to the named variable.</param>
        /// <remarks>
        /// This method may be called at any time during the life of the application.
        /// </remarks>
        public void SetVariableValue(string name, string value)
        {
        }

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
    /// <summary>Allows the user to track the progress of a query as it executes. 
    /// </summary>
    /// <remarks>
    /// he XmlDebugListener class allows the user to track the progress of a  query as
    /// it executes. The <see cref="XmlStackFrame"/> argument to the  methods of this
    /// class gives access to the point in the query plan  corresponding to the current
    /// execution state, as well as the execution  stack trace and parts of the dynamic
    /// context for that stack frame.
    /// <para>
    /// During evaluation of a query, BDB XML will evaluate the sub-expressions of  the
    /// query. The <see cref="Debug_Start"/> event is raised when evaluation of a
    /// sub-expression starts and the <see cref="Debug_Stop"/> event is called when it
    /// ends. This can occur more than once for the same sub-expression if the
    /// expression is in a loop or in a function that is called more than once.
    /// </para>
    /// <para>
    /// When evaluating a sub-expression BDB XML calls into that sub-expression a
    /// number of times to retrieve parts of its result. For eager evaluation, this will
    /// happen only once, but for lazy evaluation this will happen once per item in the
    /// result. The <see cref="Debug_Enter"/> event is raised when BDB XML requests results from a
    /// sub-expression and the <see cref="Debug_Exit"/> event is called when the results requested have
    /// been calculated.</para>
    /// </remarks>
    public sealed class XmlDebugListener : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the XmlDebugListener class with the specified query.
        /// </summary>
        /// <param name="query">The query being debugged.</param>
// ReSharper disable once UnusedParameter.Local
        public XmlDebugListener(string query) {}

        /// <summary>
        /// Gets the query statement being executed.
        /// </summary>
        public string Query { get; private set; }
        /// <summary>
        /// Delegate object for the <see cref="Debug_Start"/> event.
        /// </summary>
        /// <param name="sender">the calling object.</param>
        /// <param name="args">The <see cref="XmlStackFrame"/> information.</param>
        public delegate void StartEventDelegate(object sender, StackFrameEventArgs args);

        /// <summary>
        /// Delegate object for the <see cref="Debug_Stop"/> event.
        /// </summary>
        /// <param name="sender">the calling object.</param>
        /// <param name="args">The <see cref="XmlStackFrame"/> information.</param>
        public delegate void StopEventDelegate(object sender, StackFrameEventArgs args);

        /// <summary>
        /// Delegate object for the <see cref="Debug_Enter"/> event.
        /// </summary>
        /// <param name="sender">the calling object.</param>
        /// <param name="args">The <see cref="XmlStackFrame"/> information.</param>
        public delegate void EnterEventDelegate(object sender, StackFrameEventArgs args);
        /// <summary>
        /// Delegate object for the <see cref="Debug_Exit"/> event.
        /// </summary>
        /// <param name="sender">the calling object.</param>
        /// <param name="args">The <see cref="XmlStackFrame"/> information.</param>
        public delegate void ExitEventDelegate(object sender, StackFrameEventArgs args);
        /// <summary>
        /// Delegate object for the <see cref="Debug_Error"/> event.
        /// </summary>
        /// <param name="sender">the calling object.</param>
        /// <param name="args">The <see cref="XmlStackFrame"/> information.</param>
        public delegate void ErrorEventDelegate(object sender, StackFrameEventArgs args);

        /// <summary>
        /// Raised when seeking results from a sub-expression.
        /// </summary>
        public event EnterEventDelegate Debug_Enter;
        /// <summary>
        /// Raised when a sub-expression has completed.
        /// </summary>
        public event ExitEventDelegate Debug_Exit;
        /// <summary>
        /// Raised at the beginning of a debugging session.
        /// </summary>
        public event StartEventDelegate Debug_Start;
        /// <summary>
        /// Raised at the end of a debugging session.
        /// </summary>
        public event StopEventDelegate Debug_Stop;
        /// <summary>
        /// Raised when an error occurs while debugging.
        /// </summary>
        public event ErrorEventDelegate Debug_Error;

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
