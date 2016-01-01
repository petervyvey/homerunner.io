
using HomeRunner.Foundation.Infrastructure.Extension;
using HomeRunner.Foundation.Infrastructure.Logging;
using System;
using System.Transactions;

namespace HomeRunner.Foundation.Infrastructure.Transaction
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

            correlationId = string.IsNullOrEmpty(correlationId) ? "__UNSPECIFIED__" : correlationId;

            Logger.Log.InfoFormat(Logger.CORRELATED_MESSAGE, correlationId, "starting transaction");
            TransactionOptions options = new TransactionOptions { IsolationLevel = isolationLevel };
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                Logger.Log.InfoFormat(Logger.CORRELATED_MESSAGE, correlationId, "transaction started");

                Logger.Log.InfoFormat(Logger.CORRELATED_MESSAGE, correlationId, "invoking action");
                action.Invoke();

                Logger.Log.DebugFormat(Logger.CORRELATED_MESSAGE, correlationId, "action invoked");

                Logger.Log.InfoFormat(Logger.CORRELATED_MESSAGE, correlationId, "completing transaction");
                scope.Complete();

                Logger.Log.DebugFormat(Logger.CORRELATED_MESSAGE, correlationId, "completed transaction");
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
