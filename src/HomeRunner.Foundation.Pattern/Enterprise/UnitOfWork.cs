//using System;
//using System.Collections.Generic;
//using System.Data;

//namespace HomeRunner.Foundation.Pattern.Enterprise
//{
//    public class UnitOfWork : IUnitOfWork
//    {
//        private readonly ISessionFactory _sessionFactory;
//        private ITransaction _transaction;
//        private Dictionary<Type, object> _repositories;

//        public ISession Session { get; private set; }

//        public UnitOfWork(ISessionFactory sessionFactory)
//        {
//            _sessionFactory = sessionFactory;
//            Session = _sessionFactory.OpenSession();
//        }

//        public IReadWriteRepository<TEntity> GetRepository<TEntity>() where TEntity : class
//        {
//            foreach (var key in _repositories.Keys)
//            {
//                if (key == typeof(TEntity))
//                {
//                    return _repositories[typeof(TEntity)] as IReadWriteRepository<TEntity>;
//                }
//            }

//            var repository = new Repository<TEntity>(Session);
//            _repositories.Add(typeof(TEntity), repository);
//            return repository;
//        }

//        public ITransaction BeginTransaction()
//        {
//            if (_transaction != null)
//            {
//                throw new InvalidOperationException("Cannot have more than one transaction per session.");
//            }
//            _transaction = Session.BeginTransaction(IsolationLevel.ReadCommitted);
//            return _transaction;
//        }

//        public void Commit()
//        {
//            if (!_transaction.IsActive)
//            {
//                throw new InvalidOperationException("Cannot commit to inactive transaction.");
//            }
//            _transaction.Commit();
//        }

//        public void Rollback()
//        {
//            if (_transaction.IsActive)
//            {
//                _transaction.Rollback();
//            }
//        }

//        public void Dispose()
//        {
//            if (Session != null)
//            {
//                Session.Dispose();
//            }
//            if (_transaction != null)
//            {
//                _transaction.Dispose();
//            }
//        }
//    }
//}
