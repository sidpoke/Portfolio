using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace MMOCore.Repository
{
    /// <summary>
    /// Jonas Walter, PRO2018 4. Semester
    /// 
    /// Generic interface and class for a repository, nothing special here.
    /// IRepository holds basic functionality to store and remove Entities.  
    /// Although its purpose is not in saving them, this is done by the UnitOfWork.
    /// Repository class holds a DbContext and defines our basic functions for our EF then.
    /// 
    /// The implementation will inherit this class, adding other functionalities.
    /// </summary>
    /// 
    /// <typeparam name="TEntity">A generic entity which should be of type 'class'</typeparam>

    public interface IRepository<TEntity> where TEntity : class
    {        
        TEntity Get(int id);
        
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
