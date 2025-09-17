using System;
using System.Collections.Generic;
using System.Text;

namespace BestBuyCRUDBestPracticeConsoleUI
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }//must be able to map each piece of data from the table which it's being pulled from into the application, which is why we have to define and write out each of those pieces of data into properties within the class which represents the table we're pulling data from.
        public double Price { get; set; }
        public int CategoryID { get; set; }
        public int OnSale { get; set; }
        public string StockLevel { get; set; }
    }
}
