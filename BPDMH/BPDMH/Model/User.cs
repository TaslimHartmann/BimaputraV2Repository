using System.Collections.Generic;
using BPDMH.Interfaces;

namespace BPDMH.Model
{
    public class User<T>: ICrud<T>
    {
        public User()
        {
        }
        
        public void Save(string tableName, T model)
        {
            throw new System.NotImplementedException();
        }

        public void Update(string tableName, T model)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteById(string tableName, T model)
        {
            throw new System.NotImplementedException();
        }

        public List<T> GetAll(T tableName)
        {
            throw new System.NotImplementedException();
        }

        public T GetById(T model)
        {
            throw new System.NotImplementedException();
        }
    }
}