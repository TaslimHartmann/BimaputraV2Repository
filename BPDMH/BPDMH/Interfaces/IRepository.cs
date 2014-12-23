using System.Collections.Generic;

namespace BPDMH.Interfaces
{
    public interface IRepository<T> where T : class 
    {
        void Add(T entity, string[] param, string sqlSyntax);
        void Delete(T entity);
        void Update(T entity);
        T GetBy(T entity);
        IEnumerable<T> GetAll();
    }
}