using System.Collections.Generic;
using BPDMH.Interfaces;

namespace BPDMH.Model
{
    public class Employee : ICrud<Employee>
    {
        private string _userId;
        private string _userName;
        private string _password;

        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; protected set; }
 
        public Employee(string userId, string userName, string password)
        {
            _userId = userId;
            _userName = userName;
            _password = password;
        }


        public void Save(string tableName, Employee model)
        {
            throw new System.NotImplementedException();
        }

        public void Update(string tableName, Employee model)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteById(string tableName, Employee model)
        {
            throw new System.NotImplementedException();
        }

        public List<Employee> GetAll(Employee tableName)
        {
            throw new System.NotImplementedException();
        }

        public Employee GetById(Employee model)
        {
            throw new System.NotImplementedException();
        }
    }
}