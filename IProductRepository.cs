using System;
using System.Collections.Generic;
using System.Text;

namespace BestBuyCRUDBestPracticeConsoleUI
{
    public interface IProductRepository//interfaces specifies behavior of anything that conforms to it.
    {
        public IEnumerable<Product> GetAllProducts();//can just copy and paste into the dapperproductrepository class.
        public void CreateProduct(string name, double price, int categoryID, int productID, int onSale, string stocklevel);
        //public void UpdateProduct(Product product);
        public void DeleteProduct(int productID);//this line of code isn't needed either.
    }
}
