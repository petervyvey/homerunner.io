
using System;
using System.Transactions;

namespace HomeRunner.Foundation.Infrastructure.Transaction
{
    public interface IUnitOfWork
        : IDisposable
    {
        void Start(Action action, string correlationId, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
    }
}
