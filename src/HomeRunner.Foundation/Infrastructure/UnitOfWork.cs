
using HomeRunner.Foundation.Extension;
using HomeRunner.Foundation.Logging;
using System;
using System.Transactions;

namespace HomeRunner.Foundation.Infrastructure
{
    public sealed class UnitOfWork
        : IUnitOfWork
    {
        /// <summary>
        /// TRUE if the object is disposed, FALSE otherwise.
        /// </summary>
        private bool isDisposed = false;

        /// <summary>
        /// Destructor.
        /// </summary>
        ~UnitOfWork()
        {
            this.Dispose(false);
        }

        public void Start(Action action, string correlationId, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            Argument.InstanceIsRequired(action, "action");

            correlationId = string.IsNullOrEmpty(correlationId) ? "[UNSPECIFIED]" : correlationId;

            LogInstance.Log.Info(string.Format("Starting TransactionScope for: {0}", correlationId));
            TransactionOptions options = new TransactionOptions {IsolationLevel = isolationLevel};
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                LogInstance.Log.Info(string.Format("Started TransactionScope for: {0}", correlationId));

                LogInstance.Log.Info(string.Format("Invoking action for: {0}", correlationId));
                action.Invoke();

                LogInstance.Log.Debug(string.Format("Invoked action for: {0}", correlationId));

                LogInstance.Log.Info(string.Format("Completing TransactionScope for: {0}", correlationId));
                scope.Complete();

                LogInstance.Log.Debug(string.Format("Completed TransactionScope for: {0}", correlationId));
            }
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #region - Private & protected methods -

        /// <summary>
        /// Disposes managed and unmanaged resources.
        /// </summary>
        /// <param name="isDisposing">Flag indicating how this protected method was called. 
        /// TRUE means via Dispose(), FALSE means via the destructor.
        /// Only in case of a call through the Dispose() method should managed resources be freed.</param>
        private void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                // Free managed resources here
            }

            if (!this.isDisposed)
            {

            }

            this.isDisposed = true;
        }

        #endregion
    }
}
