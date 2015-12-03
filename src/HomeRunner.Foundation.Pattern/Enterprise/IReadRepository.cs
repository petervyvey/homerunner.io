
//using System;
//using System.Linq;
//using System.Linq.Expressions;

//namespace HomeRunner.Foundation.Pattern.Enterprise
//{
//    public interface IReadRepository<TEntity> where TEntity : class
//    {
//        IQueryable<TEntity> All();
//        TEntity FindBy(Expression<Func<TEntity, bool>> expression);

//        TEntity FindBy(object id);

//        IQueryable<TEntity> FilterBy(Expression<Func<TEntity, bool>> expression);
//    }
//}
