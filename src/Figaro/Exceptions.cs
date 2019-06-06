// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable UnusedParameter.Local
// ReSharper disable UnusedMember.Local
// ReSharper disable UnusedMember.Global
// ReSharper disable CommentTypo



// ReSharper disable InconsistentNaming
namespace Figaro
{
    using System;
    using System.Runtime.Serialization;


    /// <summary>
    /// Thrown when an exception occurs within the High Availability (HA) or replication environment.
    /// </summary>
    [Serializable]
    public class FigaroReplicationException : Exception
    {
        private FigaroReplicationException() { }
    }

    /// <summary>
    /// Thrown when the container version and the dbxml library version are not compatible.
    /// </summary>
    [SerializableAttribute]
    public class VersionMismatchException : Exception
    {
        /// <summary>
        /// Prevents a default instance of the VersionMismatchException class from being created.
        /// </summary>
        private VersionMismatchException() { }
    }

    /// <summary>
    /// Thrown when the operating system executing the Figaro assembly is not supported.
    /// </summary>
    [Serializable]
    public class UnsupportedPlatformException : Exception
    {
        private UnsupportedPlatformException() { }
    }

    /// <summary>
    /// Thrown when the shared memory region was locked and (repeatedly)
    /// unavailable in a <see cref="FigaroEnv"/> object.
    /// </summary>
    /// <seealso cref="FigaroEnv.Open"/>
    [Serializable]
    public class SharedMemoryLockException : Exception
    {
        /// <summary>
        /// Prevents a default instance of the SharedMemoryLockException class from being created.
        /// </summary>
        private SharedMemoryLockException() { }
    }

    /// <summary>
    /// Base exception for other exceptions in the Figaro library.
    /// </summary>
    /// <threadsafety static="true" instance="true"/>
    [Serializable]
    public class FigaroException : Exception
    {
        /// <summary>
        /// The exception category.
        /// </summary>
        protected FigaroExceptionCategory _category;

        /// <summary>
        /// Exception message.
        /// </summary>
        protected string _msg;

        protected Exception _inner;
        /// <summary>
        /// Initializes a new instance of the FigaroException class.
        /// </summary>
        internal FigaroException() { }

        /// <summary>
        /// Initializes a new instance of the FigaroException class.
        /// </summary>
        /// <param name="info">Serialization information.</param>
        /// <param name="context">The streaming context.</param>
        protected FigaroException(SerializationInfo info, StreamingContext context) { }

        /// <summary>Gets or sets the exception error code.</summary><value>The exception error code.</value>
        public int ErrorCode { get; set; }

        /// <summary>Gets or sets the exception message.</summary><value>The exception message.</value>
        public new string Message { get; set; }

        /// <summary>Gets the exception error code.</summary>
        /// <value>The exception code.</value>
        public FigaroExceptionCategory ExceptionCategory
        {
            get { return _category; }
        }
    }

    /// <summary>
    /// This exception is thrown when a licensing issue occurs in the Figaro library.
    /// </summary>
    /// <seealso cref="FigaroException"/>
    /// <threadsafety static="true" instance="true"/>
    [Serializable]
    public class LicenseException : FigaroException
    {
        /// <summary>
        /// Initializes a new instance of the LicenseException class.
        /// </summary>
        /// <param name="info">Serialization information.</param>
        /// <param name="context">The streaming context.</param>
        protected LicenseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }

    /// <summary>
    /// This exception is thrown whenever an error occurs when performing a transaction.
    /// </summary>
    [Serializable]
    public class FigaroTransactionException : FigaroException
    {
        /// <summary>
        /// Prevents a default instance of the FigaroTransactionException class from being created.
        /// </summary>
        private FigaroTransactionException() : base(null, new StreamingContext()) { }
    }

    /// <summary>
    /// This exception is thrown when the underlying Berkeley database environment throws an error.
    /// </summary>
    /// <threadsafety static="true" instance="true"/>
    [Serializable]
    public class DatabaseException : FigaroException
    {
        /// <summary>
        /// Initializes a new instance of the DatabaseException class.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
        protected DatabaseException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// Prevents a default instance of the DatabaseException class from being created.
        /// </summary>
        private DatabaseException() { }
    }

    /// <summary>
    /// This exception is thrown when a <see cref="Container"/> is disposed before it is closed.
    /// </summary>
    /// <threadsafety static="true" instance="true"/>
    [Serializable]
    public class ContainerScopeException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the ContainerScopeException class.
        /// </summary>
        public ContainerScopeException() { }

        /// <summary>
        /// Initializes a new instance of the ContainerScopeException class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public ContainerScopeException(string message) { }
    }

    /// <summary>
    /// Some properties can be modified after opening, and some can't. Those that can't throw this exception.
    /// </summary>
    /// <threadsafety static="true" instance="true"/>
    [Serializable]
    public class FigaroEnvException : Exception
    {
        /// <summary>
        /// Prevents a default instance of the FigaroEnvException class from being created.
        /// </summary>
        private FigaroEnvException() { }
    }

    /// <summary>
    /// Indicates that recovery procedures must be run against the database environment.
    /// </summary>
    /// <remarks>
    /// There exists a class of errors that Figaro considers fatal to an entire database environment. An example of this type of error
    /// is a corrupted database page. The only way to recover from these failures is to have all threads of control exit the Berkeley DB
    /// environment, run recovery of the environment, and re-enter Berkeley DB. (It is not strictly necessary that the processes exit,
    /// although that is the only way to recover system resources, such as file descriptors and memory, allocated by Berkeley DB.)
    /// <para>
    /// This exception can be returned by any Figaro interface.
    /// </para>
    /// </remarks>
    /// <threadsafety static="true" instance="true"/>
    [Serializable]
    public class RunRecoveryException : Exception
    {
        /// <summary>
        /// Prevents a default instance of the RunRecoveryException class from being created.
        /// </summary>
        private RunRecoveryException() { }
    }

    /// <summary>
    /// Occurs when trying to use more than one messaging output
    /// methodology in <see cref="FigaroEnv"/> at a time.
    /// </summary>
    /// <remarks>
    /// This feature is by design - you never want to use more than one
    /// output system simultaneously. Disable one before deciding to switch to another.
    /// </remarks>
    /// <see cref="FigaroEnv"/>
    /// <threadsafety static="true" instance="true"/>
    [Serializable]
    public class FigaroEnvOutputException : Exception
    {
        /// <summary>
        /// Prevents a default instance of the FigaroEnvOutputException class from being created.
        /// </summary>
        private FigaroEnvOutputException() { }
    }

    /// <summary>
    /// Thrown when a lock is unavailable for an operation.
    /// </summary>
    /// <threadsafety static="true" instance="true"/>
    [Serializable]
    public class DBDeadlockException : FigaroException
    {
        /// <summary>
        /// Prevents a default instance of the DBDeadlockException class from being created.
        /// </summary>
        private DBDeadlockException() { }
    }

    /// <summary>
    /// Thrown when a lock request was not granted by the database system.
    /// </summary>
    /// <threadsafety static="true" instance="true"/>
    [Serializable]
    public class DBLockNotGrantedException : FigaroException
    {
        /// <summary>
        /// Prevents a default instance of the DBLockNotGrantedException class from being created.
        /// </summary>
        private DBLockNotGrantedException() { }
    }


        /// <summary>
        /// Initializes a new instance of the XQueryException class. This exception is thrown when
        /// an XQuery operation throws an exception.
        /// </summary>
        /// <remarks>
        /// The purpose of this object is to provide a thorough level of detail regarding exceptions
        /// that occur during XQuery operations, splitting the error information into different fields
        /// and providing contextual error information according to the information found in Appendix D
        /// of the XQuery Language Specification.
        /// <para>
        /// Note that the <see cref="Exception.HelpLink"/> property points to the URL of the exception on
        /// the XQuery website.
        /// </para>
        /// </remarks>
        /// <see href="http://www.w3.org/XML/Query/">W3C XML Query</see>
        /// <see href="http://www.w3.org/TR/xquery/#id-errors">Appendix F: Error Conditions</see>
        [SerializableAttribute]
        public class XQueryException : FigaroException
        {
            /// <summary>
            /// The exception category.
            /// </summary>
            protected string _cat;
            /// <summary>
            /// The query column.
            /// </summary>
            protected uint _col;
            /// <summary>
            /// The condition to evaluate.
            /// </summary>
            protected string _condition;
            /// <summary>
            /// The description.
            /// </summary>
            protected string _desc;
            /// <summary>
            /// The query file.
            /// </summary>
            protected string _file;
            /// <summary>
            /// The identifier.
            /// </summary>
            protected uint _id;
            /// <summary>
            /// The query line.
            /// </summary>
            protected uint _line;
            /// <summary>
            /// The query text.
            /// </summary>
            protected string _query;

            /// <summary>
            /// Prevents a default instance of the XQueryException class from being created.
            /// </summary>
            private XQueryException() { }

            /// <summary>
            /// Gets the XQuery exception category (i.e. 'XPTY').
            /// </summary>
            public string ErrorCategory { get;private set; }

            /// <summary>
            /// Gets the full XQuery exception (i.e. 'err:XPTY0081')
            /// </summary>
            public string ErrorCondition { get;private set; }

            /// <summary>
            /// Gets the full description regarding the error, as found on the XQuery website.
            /// </summary>
            public string ErrorDescription { get;private set; }

            /// <summary>
            /// Gets the XQuery query executed that generated the exception, if available.
            /// </summary>
            public string Query { get;private set; }

            /// <summary>
            /// Gets the namespace of the XQuery file that threw the exception.
            /// </summary>
            public string QueryFile { get;private set; }

            /// <summary>
            /// Gets the query column in the query line that threw the exception.
            /// </summary>
            public uint QueryColumn { get;private set; }

            /// <summary>
            /// Gets the line in the query that threw the exception.
            /// </summary>
            public uint QueryLine { get;private set; }
        }

        /// <summary>
        /// This general exception is thrown when the underlying BDB XML engine throws an error.
        /// </summary>
        /// <seealso cref="FigaroException"/>
        /// <threadsafety static="true" instance="true"/>
        [Serializable]
        public class XmlException : FigaroException
        {
            /// <summary>
            /// Prevents a default instance of the XmlException class from being created.
            /// </summary>
            private XmlException() { }

            /// <summary>
            /// Initializes a new instance of the XmlException class.
            /// </summary>
            /// <param name="info">The serialization information.</param>
            /// <param name="context">The serialization context.</param>
            protected XmlException(SerializationInfo info, StreamingContext context) { }

            ///// <summary>Gets the query column, if applicable.</summary><returns>The query column.</returns>
            // public int QueryColumn { get; set; }
            ///// <summary>Gets the line number of the query throwing the exception, if applicable.</summary><returns>The query line number.</returns>
            // public int QueryLine { get; set; }
            ///// <summary>Gets the file name of the query throwing the exception, if applicable.</summary><returns>The query file name.</returns>
            // public string QueryFile { get; set; }
        }

        /// <summary>
        /// This exception is thrown whenever the type of <see cref="XmlResults"/> result set is a
        /// <see cref="XmlValue"/> type, but an attempt is made to retrieve it as a different type.
        /// </summary>
        [Serializable]
        public class XmlValueException : FigaroException
        {
        }

    /// <summary>
    /// This exception is thrown when a replication lease expires.
    /// </summary>
    [Serializable]
    public class LeaseExpiredException : FigaroException { }


}
