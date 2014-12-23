
using System.Collections.Generic;

namespace BPDMH.Interfaces
{
    public interface ICrud<T>
    {
        void Save(string tableName, T model);
        void Update(string tableName, T model);
        void DeleteById(string tableName, T model);
        List<T> GetAll(T tableName);
        T GetById(T model);
    }
}