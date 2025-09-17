using System;
using System.Collections.Generic;
using System.Text;

namespace BestBuyCRUDBestPracticeConsoleUI
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAllDepartments();//we need a method called GetAllDepartments that returns a collection, which conforms to IEnumerable<T>.
        public void InsertDepartment(string newDepartmentName);//typically interfaces employ stubbed out methods, but that doesn't always mean that those methods, when implemented into other derived classes, will remain devoid of passing in arguments within the interface from which they originate, especially if those same stubbed out methods are passing in the same arguments when called within the derived classes implementing them.
        public void DeleteDepartment(int departmentID);//this line of code may not even be necessary let me test without it. Updated; this line of code was not necessary in order for the program to run and execute what it was supposed to do.
    }//lol, I just remembered that these are interfaces :D so, of course any classes that conform to them only need to have whatever they feature, but may feel free to define anything else that they don't have, because interfaces only want any class that's inheriting from them to feature whatever they do. 
}
