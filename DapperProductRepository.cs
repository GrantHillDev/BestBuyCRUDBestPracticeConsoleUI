using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Dapper;//this is required in order for the Query method to be called.

namespace BestBuyCRUDBestPracticeConsoleUI
{
    public class DapperProductRepository : IProductRepository
    {
        private readonly IDbConnection _connection;//IDbConnection comes from the using directive System.Data;
        public DapperProductRepository(IDbConnection connection)//this is a method, a constructor, an instance of a class.... a special member method. Don't let the font color fool you.
        {
            _connection = connection;
        }//this field and its information is protected by way of utilizing the private access modifier.
        public void CreateProduct(string name, double price, int categoryID, int productID, int onSale, string stocklevel)
        {
            _connection.Execute("INSERT INTO products (Name, Price, CategoryID, ProductID, OnSale, StockLevel) VALUES (@name, @price, @categoryID, @productID, @onsale, @stocklevel)",
                new { name = name, price = price, categoryID = categoryID, productID = productID, onsale = onSale, stocklevel = stocklevel });//these have to match the arguments listed in the method for this script -- variables down here must match the variables up there.
        }

        public IEnumerable<Product> GetAllProducts()//didn't have to store the data inside of the variable this time around, unlike what was written in for the same method type, but in the DapperDepartmentRepostiory class.
        {
            return _connection.Query<Product>("SELECT * FROM products");//The code written inside of this string can be thought of as the code that's being sent directly over to MySQL for processing.
        }//Query method comes form the Dapper using directive.
        public void DeleteProduct(int productID)//this method should be effective enough at removing all traces of a given product from all of its related tables in the database. Updated; it is because the productID is only in those 3 tables of the database, duhh.
        {
            _connection.Execute("DELETE FROM sales WHERE ProductId = @productID;", new { productID = productID });
            _connection.Execute("DELETE FROM reviews WHERE ProductId = @productID;", new { productID = productID });
            _connection.Execute("DELETE FROM products WHERE ProductId = @productID;", new { productID = productID });
        }
        public Product GetProduct(int id)
        {
            return (Product)_connection.QuerySingle<Product>("SELECT * FROM products WHERE ProductID = @id;",//wow, I had to add what was known as an explicit cast in order to make this line of code work -- seemed to be because there was a conversion error with the passing data types (the part in this line of code where it says (Product)_connection).
                new { id = id });
        }
        public void UpdateProduct(Product product)//this can be done with as many or as few properties as desired, based on how the product class has been defined. For instance, you could just have this update product method only be capable of updating the product's name or ID.
        {
            _connection.Execute(" UPDATE products" + " SET Name = @name," + "Price = @price," + "OnSale = @onsale," + "StockLevel = @stock" + " WHERE ProductID = @id" + " AND CategoryID = @catID;",
                new
                {
                    name = product.Name,
                    price = product.Price,
                    onsale = product.OnSale,
                    stock = product.StockLevel,
                    id = product.ProductID,
                    catID = product.CategoryID
                });
        }

    }
}
