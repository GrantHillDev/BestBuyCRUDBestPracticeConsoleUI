using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;

namespace BestBuyCRUDBestPracticeConsoleUI
{
    public class DapperDepartmentRepository : IDepartmentRepository
    {
        //public DapperDepartmentRepository()
        //{
        //default constructor, if it contains nothing within its scope.
        //}
        private readonly IDbConnection _connection;//Field or local variable that's being used for making queries to the database.

        //_connection = connection (here, we're suggesting that the field value connection is equal to our variable parameter connection.
        public DapperDepartmentRepository(IDbConnection connection)
        {
            //constructor
            _connection = connection;
        }
        public IEnumerable<Department> GetAllDepartments()
        {
            var depos = _connection.Query<Department>("SELECT * FROM departments");//("");
            return depos;//returns a type IEnumerable.
        }

        public void InsertDepartment(string newDepartmentName)//calling it newDepartment name so that there's no confusion as to what this argument's variable is supposed to do.
        {
            _connection.Execute("INSERT INTO DEPARTMENTS (Name) VALUES (@departmentName)",//here, we're calling our IDbConnection, and saying we want to utilize its execute method.
            new { departmentName = newDepartmentName });//an anonymous type -- whatever type we're trying to create here, the script will automatically generate for us. Here, departmentName represents an anonymous type, and 'a' is the default name given to it (when highlighting over it). newDepartmentName represents a variable that we will pass back into the method after its stored the data from departmentName.
        }

        public void DeleteDepartment(int departmentID)//so, as I was designing this method, I had to figure out a relatively quick way in which the department ID could be looked up in side of each table within the database on MySQL, and found that all you have to do is just click on each table to see where the ID of the table in question is located, and then you script accordingly (within the context of CRUD).
        {
            _connection.Execute("DELETE FROM categories WHERE departmentId = @departmentID;", new { departmentID = departmentID });//syntax errors here caused me some grief in the beginning.
            _connection.Execute("DELETE FROM departments WHERE departmentId = @departmentID;", new { departmentID = departmentID });
        }
    }
}
