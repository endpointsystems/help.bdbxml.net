using System;

namespace Figaro    
{
    /// <summary>
    /// Encapsulates the context within which update operations are performed against a
    /// <see cref="Container"/>.
    /// </summary>
    /// <remarks>
    /// 	<see cref="UpdateContext"/> objects are instantiated using
    /// <see cref="XmlManager.CreateUpdateContext"/>.
    /// </remarks>
    /// <threadsafety static="false" instance="false"/>
    /// <seealso cref="Container"/>
    /// <seealso cref="XmlManager.CreateUpdateContext"/>
    public sealed class UpdateContext : IDisposable
    {
        /// <summary>
        /// Prevents a default instance of the UpdateContext class from being created.
        /// </summary>
        private UpdateContext() {}

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
