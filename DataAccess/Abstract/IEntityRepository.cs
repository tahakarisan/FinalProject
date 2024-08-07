using Entities.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{   //class:referans tip
    //IEntity olabilir ya da IEntity den miras alan nesne olabilir (referans tip)
    //new() newlenebilir olması lazım interface newlenemediği için de IEntity devre dışı kalacak ve ortayı karıştıramayacak
    public interface IEntityRepository<T>where T : class,IEntity,new()
    {
        //generic constraint generic kısıtlama
        List<T> GetAll(Expression<Func<T, bool>> filter = null);
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
