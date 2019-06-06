using System;
using System.Collections;
using System.Xml;

namespace Figaro    
{
    /// <summary>
    /// Encapsulates the results of a query or other lookup operation.
    /// </summary>
    /// <remarks>
    /// 	<see cref="XmlResults"/> is a collection of
    /// <see cref="XmlValue"/>
    /// objects, which may represent any one of the supported types. An
    /// <see cref="XmlResults"/> object is created by executing a query or
    /// calling <see cref="XmlIndexLookup.Execute(QueryContext)"/>.
    /// <para>
    /// There are several ways that a query is performed. One is to call
    /// <see cref="XmlManager.Query(string,QueryContext,QueryOptions)"/> directly. This mechanism is
    /// appropriate for one-shot queries that will not be repeated.
    /// </para><para>
    /// A second approach is to create an <see cref="XQueryExpression"/> using
    /// <see cref="XmlManager.Prepare(string,QueryContext)"/>. You then execute the query expression
    /// using <see cref="XQueryExpression.Execute(QueryContext)"/>. This approach is appropriate
    /// for queries that will be performed more than once as it means that the expense of compiling the query can be
    /// amortized across multiple queries.
    /// </para><para>
    /// Note that when you perform a query, you must provide an <see cref="QueryContext"/> object.
    /// Using this object, you can indicate whether you want the query to be performed eagerly or lazily. If eager
    /// evaluation is specified (the default), then the resultant values are stored within
    /// the <see cref="XmlResults"/> object. If lazy evaluation is selected, then the
    /// resultant values will be computed as needed. In this case
    /// the <see cref="XmlResults"/> object will maintain a reference to the affected
    /// containers (<see cref="Container"/>), query context
    /// (<see cref="QueryContext"/>), and expression
    /// (<see cref="XQueryExpression"/>).
    /// </para><para>
    /// The <see cref="XmlResults"/> class provides an iteration interface through
    /// the <see cref="XmlResults.NextDocument"/> and <see cref="XmlResults.NextValue"/>
    /// methods. These methods
    /// return <c>null</c> when no more results are available
    /// (<see cref="XmlResults.IsNull"/> returns <c>true</c>).
    /// <see cref="XmlResults.Reset"/> can be called to reset the iterator, and the
    /// subsequent call to <see cref="XmlResults.NextDocument"/> or
    /// <see cref="XmlResults.NextValue"/>
    /// will return the first value of the result set.
    /// </para>
    /// </remarks>
    /// <threadsafety static="false" instance="false"/>
    /// <seealso cref="XmlManager.Prepare(string,QueryContext)"/>
    /// <seealso cref="XmlManager"/>
    /// <seealso cref="Container"/>
    /// <seealso cref="QueryContext"/>
    /// <seealso cref="XQueryExpression"/>
    public sealed class XmlResults : IEnumerator, ICollection
    {


        /// <summary>
        /// Concatenate <see cref="XmlResults"/> objects together.
        /// </summary>
        /// <param name="results">The <see cref="XmlResults"/> object to concatenate.</param>
        public void ConcatResults(XmlResults results) { }

        /// <summary>
        /// Create a new copy of the results.
        /// </summary>
        /// <param name="results">The <see cref="XmlResults"/> object to copy.</param>
        /// <returns>A new copy of the <see cref="XmlResults"/> object.</returns>
        public XmlResults CopyResults(XmlResults results) { return null; }



        /// <summary>
        /// Adds the specified <see cref="XmlValue"/> to the end of the results set.
        /// </summary>
        /// <param name="value">The <see cref="XmlValue"/> to add.</param>
        /// <remarks>
        /// Note that if
        /// the <see cref="XmlResults"/> object was created as the result of a
        /// lazy evaluation, this method throws an exception. This method is used primarily for
        /// application resolution of collections in
        /// <see cref="XmlManager.CreateXmlResults"/>).
        /// </remarks>
        /// <seealso cref="XmlManager.CreateXmlResults"/>
        public void Add(XmlValue value) { }

        /// <summary>
        /// Returns the evaluation type of the <see cref="XmlResults"/> object.
        /// This type is either <see cref="EvaluationType.Eager"/>
        /// or <see cref="EvaluationType.Lazy"/>.
        /// </summary>
        /// <returns>The evaluation type.</returns>
        /// <seealso cref="EvaluationType"/>
        public EvaluationType GetEvaluationType()
        {
            return EvaluationType.Eager;
        }

        /// <summary>
        /// Returns <c>true</c> if there is another element in the results set.
        /// </summary>
        /// <remarks>
        /// Gets the boolean operator indicating another element in the results set.
        /// </remarks>
        /// <returns><c>true</c> if there is another element.</returns>
        public bool HasNext()
        {
            return false;
        }

        /// <summary>
        /// Returns <c>true</c> if there is a previous element in the results set.
        /// </summary>
        /// <remarks>
        /// Gets the boolean operator indicating an element prior to the current in the results set.
        /// </remarks>
        /// <returns><c>true</c> if there is a previous element.</returns>
        public bool HasPrevious()
        {
            return false;
        }

        /// <summary>
        /// Returns the number of results in an eagerly-evaluated results set.
        /// </summary>
        /// <remarks>Gets the number of results in an eagerly-evaluated results set.</remarks>
        /// <remarks>
        /// If a query was processed with eager evaluation, this method gets the number of values
        /// in the result set. If the query was processed with lazy evaluation, checking
        /// the method throws an exception.
        /// </remarks>
        /// <returns>The number of results in an eagerly-evaluated results set.</returns>
        public uint GetSize()
        {
            return 0;
        }

        /// <summary>
        /// Return the next document as a .NET <see cref="XmlReader"/>. If none exists,
        /// returns <c>null</c>.
        /// </summary>
        /// <returns>A .NET <see cref="XmlReader"/>.</returns>
        public XmlReader NextReader()
        {
            return null;
        }

        /// <summary>
        /// Retrieves the current document in the result set as a .NET <see cref="XmlReader"/> without
        /// moving the internal iterator.
        /// </summary>
        /// <returns>A .NET <see cref="XmlReader"/>.</returns>
        public XmlReader PeekReader()
        {
            return null;
        }

        /// <summary>
        /// Retrieves the previous document in the
        /// result set as a .NET <see cref="XmlReader"/>. If none exists, returns <c>null</c>.
        /// </summary>
        /// <returns>
        /// The previous document as a .NET <see cref="XmlReader"/>.
        /// </returns>
        public XmlReader PreviousReader()
        {
            return null;
        }

        /// <summary>
        /// Retrieves the next value in the result set. When no more values remain in the result set,
        /// the method returns <c>null</c>.
        /// </summary>
        /// <returns>
        /// The next <see cref="XmlDocument"/> value.
        /// </returns>
        /// <exception cref="XmlValueException">Thrown if the underlying type is <see cref="XmlValue"/>.</exception>
        public XmlDocument NextDocument()
        {
            return null;
        }

        /// <summary>
        /// Retrieves the next value in the result set. When no more values remain in the result set,
        /// the method returns <c>null</c>.
        /// </summary>
        /// <returns>
        /// The next <see cref="XmlValue"/>.
        /// </returns>
        public XmlValue NextValue()
        {
            return null;
        }

        /// <summary>
        /// Retrieves the current <see cref="XmlDocument"/> in the result set without moving the internal iterator.
        /// </summary>
        /// <returns>
        /// The current <see cref="XmlDocument"/>.
        /// </returns>
        /// <exception cref="XmlValueException">Thrown if the underlying type is <see cref="XmlValue"/>.</exception>
        public XmlDocument PeekDocument()
        {
            return null;
        }

        /// <summary>
        /// Retrieves the current <see cref="XmlValue"/> in the
        /// result set without moving the internal iterator.
        /// </summary>
        /// <returns>
        /// The current <see cref="XmlValue"/>.
        /// </returns>
        public XmlValue PeekValue()
        {
            return null;
        }

        /// <summary>
        /// Retrieves the previous <see cref="XmlDocument"/> in the
        /// result set. If none exists, returns <c>null</c>.
        /// </summary>
        /// <returns>
        /// The previous <see cref="XmlDocument"/>.
        /// </returns>
        /// <exception cref="XmlValueException">Thrown if the underlying type is <see cref="XmlValue"/>.</exception>
        public XmlDocument PreviousDocument()
        {
            return null;
        }

        /// <summary>
        /// Retrieves the previous <see cref="XmlValue"/> in the
        /// result set. If none exists, returns <c>null</c>.
        /// </summary>
        /// <returns>
        /// The previous <see cref="XmlValue"/>.
        /// </returns>
        public XmlValue PreviousValue()
        {
            return null;
        }

        /// <summary>
        /// Determines whether or not values were returned.
        /// </summary>
        /// <remarks>
        /// Gets the boolean operator determining if values were returned from the query operation.
        /// </remarks>
        /// <returns><c>true</c> if no values were returned from the operation.</returns>
        public bool IsNull()
        {
            return false;
        }

        /// <summary>
        /// Resets the result set iterator.
        /// </summary>
        /// <remarks>
        /// If a query was processed with eager evaluation, a call to the
        /// <see cref="XmlResults.Reset"/> method resets the
        /// result set iterator, so that a subsequent call
        /// to <see cref="XmlResults.NextDocument"/>
        /// or <see cref="XmlResults.NextValue"/> methods will
        /// return the first value in the result set. If the query
        /// was processed with lazy evaluation, then a call to
        /// the <see cref="XmlResults.Reset"/> method throws an
        /// exception.
        /// </remarks>
        public void Reset() { }

        private XmlResults() { }

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion IDisposable Members

        #region IEnumerator Members

        /// <summary>
        /// Gets the current element in the collection.
        /// </summary>
        /// <returns>
        /// The current element in the collection.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.
        /// </exception><filterpriority>2</filterpriority>
        public object Current
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the enumerator was successfully advanced to the next element; <c>false</c> if the enumerator has passed the end of the collection.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.
        /// </exception><filterpriority>2</filterpriority>
        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        #endregion IEnumerator Members

        #region ICollection Members

        /// <summary>
        /// Copies the elements of the <see cref="T:System.Collections.ICollection"/> to an <see cref="T:System.Array"/>, starting at a particular <see cref="T:System.Array"/> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection"/>. The <see cref="T:System.Array"/> must have zero-based indexing.
        /// </param><param name="index">The zero-based index in <paramref name="array"/> at which copying begins.
        /// </param><exception cref="T:System.ArgumentNullException"><paramref name="array"/> is <see langword="null"/>.
        /// </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than zero.
        /// </exception><exception cref="T:System.ArgumentException"><paramref name="array"/> is multidimensional.
        /// -or-
        /// <paramref name="index"/> is equal to or greater than the length of <paramref name="array"/>.
        /// -or-
        /// The number of elements in the source <see cref="T:System.Collections.ICollection"/> is greater than the available space from <paramref name="index"/> to the end of the destination <paramref name="array"/>.
        /// </exception><exception cref="T:System.ArgumentException">The type of the source <see cref="T:System.Collections.ICollection"/> cannot be cast automatically to the type of the destination <paramref name="array"/>.
        /// </exception><filterpriority>2</filterpriority>
        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.ICollection"/>.
        /// </summary>
        /// <returns>
        /// The number of elements contained in the <see cref="T:System.Collections.ICollection"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection"/> is synchronized (thread safe).
        /// </summary>
        /// <returns>
        /// <c>true</c> if access to the <see cref="T:System.Collections.ICollection"/> is synchronized (thread safe); otherwise, <see langword="false"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public bool IsSynchronized
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection"/>.
        /// </summary>
        /// <returns>
        /// An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public object SyncRoot
        {
            get { throw new NotImplementedException(); }
        }

        #endregion ICollection Members

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}