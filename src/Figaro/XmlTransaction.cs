// ReSharper disable UnusedParameter.Global

using System;
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Figaro    
{
    /// <summary>
    /// The handle for a transaction, encapsulating a Berkeley <c>DbTxn</c> object. Methods of
    /// the <see cref="XmlTransaction"/> class are
    /// used to abort and commit the transaction. <see cref="XmlTransaction"/> handles are
    /// provided to <see cref="Container"/>,
    /// <see cref="XmlManager"/>, and other objects that query and modify documents and containers
    /// in order to transactionally protect those database operations.
    /// </summary>
    /// <remarks>
    /// <para>
    /// 		<see cref="XmlTransaction"/> objects are instantiated using
    /// <see cref="XmlManager.CreateTransaction()"/>.
    /// </para>
    /// <para>
    /// 		<see cref="XmlTransaction"/> handles are not free-threaded; transactions handles
    /// may be used by multiple threads, but only serially, that is, the application must serialize access to the
    /// <see cref="XmlTransaction"/> handle. Once the
    /// <see cref="XmlTransaction.Abort"/> or
    /// <see cref="Commit()"/> methods are called, the handle may not be
    /// accessed again, regardless of the method's return. In addition, parent transactions may not issue any operations
    /// while they have active child transactions (child transactions that have not yet been committed or aborted) except for
    /// <see cref="XmlTransaction.Abort"/> and
    /// <see cref="Commit()"/>.
    /// </para>
    /// <para>
    /// If the object is used after a commit or abort, an exception is thrown.
    /// </para>
    /// </remarks>
    /// <threadsafety static="false" instance="false"/>
    /// <seealso cref="XmlManager.CreateTransaction()"/>
    /// <seealso cref="XmlManager"/>
    /// <seealso cref="Container"/>
    public sealed class XmlTransaction : IDisposable
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="XmlTransaction"/> class from being created.
        /// </summary>
        private XmlTransaction() {}

        /// <summary>
        /// Gets a boolean value indicating whether the transaction has aborted.
        /// </summary>
        public Boolean Aborted { get; private set; }

        /// <summary>
        /// Gets a boolean value indicating whether the transaction has been committed.
        /// </summary>
        public Boolean Committed { get; private set; }

        /// <summary>
        /// Causes an abnormal termination of the transaction. All write operations previously performed within the
        /// scope of the transaction are undone. Before this method returns, any locks held by the transaction will
        /// have been released.
        /// </summary>
        public void Abort() { }

        /// <summary>
        /// Ends the transaction.
        /// </summary>
        /// <param name="sync">
        ///     If <c>true</c>, synchronously flushes the log. If <c>false</c>, the log will not synchronously flush.
        ///     <para>
        ///         Using this method will override any synchronization property set at <see cref="XmlTransaction"/> creation.
        ///     </para>
        /// </param>
        /// <remarks>
        /// Container and document modifications
        /// made within the scope of the transaction are by default written to stable storage.
        /// <para>
        /// After <see cref="Commit()"/> has been called,
        /// regardless of its return, the <see cref="XmlTransaction"/> handle may
        /// not be accessed again. If <see cref="Commit()"/> encounters
        /// an error, the transaction and all child transactions of the transaction are aborted.
        /// </para><para>
        /// The <see cref="Commit()"/> method throws an exception that
        /// encapsulates a non-zero error value on failure.
        /// </para>
        /// </remarks>
        /// <seealso cref="XmlManager.CreateTransaction()"/>
        public void Commit(bool sync) {}

        /// <summary>
        /// Ends the transaction.
        /// </summary>
        /// <remarks>
        /// Container and document modifications
        /// made within the scope of the transaction are by default written to stable storage.
        /// <para>
        /// After <see cref="Commit()"/> has been called,
        /// regardless of its return, the <see cref="XmlTransaction"/> handle may
        /// not be accessed again. If <see cref="Commit()"/> encounters
        /// an error, the transaction and all child transactions of the transaction are aborted.
        /// </para><para>
        /// The <see cref="Commit()"/> method throws an exception that
        /// encapsulates a non-zero error value on failure.
        /// </para>
        /// </remarks>
        public void Commit() { }
        /// <summary>
        /// Creates a child transaction of this transaction.
        /// </summary>
        /// <param name="transactionOptions">One or more options to use when creating the child transaction.</param>
        /// <returns>The child transaction.</returns>
        /// <remarks>
        /// While this child transaction is active (has been neither committed nor aborted), the
        /// parent transaction may not issue any operations except for <see cref="Commit()"/> or
        /// <see cref="XmlTransaction.Abort"/>.
        /// </remarks>
        public XmlTransaction CreateChild(TransactionType transactionOptions) 
        {
            return null;
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
}
