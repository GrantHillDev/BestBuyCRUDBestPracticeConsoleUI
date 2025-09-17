using System;
using System.Collections.Generic;
using System.Text;

namespace BestBuyCRUDBestPracticeConsoleUI
{
    public class Department
    {
        public int departmentID { get; set; }
        public string Name { get; set; }//departmentName { get; set; }//Name is the actual title of the column from the department table we're pulling data from, not departmentName.
    }
}
