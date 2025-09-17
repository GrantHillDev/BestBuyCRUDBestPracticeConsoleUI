using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;//Ha, strange, didn't think I'd need the other system directive underneath this one, for one simple basic command or function.
using System.Threading;

namespace BestBuyCRUDBestPracticeConsoleUI//Remember to utilize the Alt + Enter hotkey for bringing up an action list in VSC, especially when facing compile based errors (it's part of what helped me to bring up al of this project file's otherwise missing using directives up top).
{
    internal class Program//a connection string is how a database authenticates a user.
    {
        static void Main(string[] args)
        {
            #region Configuration
            var config = new ConfigurationBuilder()
                             .SetBasePath(Directory.GetCurrentDirectory())
                             .AddJsonFile("appsettings.json")
                             .Build();

            string connString = config.GetConnectionString("DefaultConnection");
            //Console.WriteLine(connString);//printing this information to the console helps us to see that the program is reading from the json file we made earlier.
            #endregion
            IDbConnection conn = new MySqlConnection(connString);//this is the connection we're going to use to connect to the MySQL database.
            DapperDepartmentRepository departmentRepo = new DapperDepartmentRepository(conn);
            DapperProductRepository productRepo = new DapperProductRepository(conn);
            Console.WriteLine("Greetings. This is a Dapper exercise querying information from databases.");
            Console.WriteLine();
            //departmentRepo.DeleteDepartment(6);//only takes one argument, wonder if it could do more at once? Even when I comment out these delete methods for the departments, whenever the user chooses to make a new department, MySQL seems to just start with unused numbers, even if the others were deleted.... why?
            //departmentRepo.DeleteDepartment(7);//ok, so I've noticed that if I'm adding departments every time the program runs, it just makes new ID's for them that aren't the others which were used by previously newly added departments.... so, too prevent repetitious scripting, I'd have to design a more flexible form of code.
            //departmentRepo.DeleteDepartment(8);
            //departmentRepo.DeleteDepartment(9);
            productRepo.DeleteProduct(999);
            //productRepo.DeleteProduct(1000);//Ooof, not sure why I wrote this here....perhaps it was in regards to a previously designed and implemented product from another program? Doesn't seem to throw an error when such a product is not currently listed within the Best Buy inventory via MySQL.... not a bad thing? Otherwise, I'd have to comment this whole line of code out.
            productRepo.DeleteProduct(901);
            //productRepo.DeleteProduct(903);//Ooof, not sure why I wrote this here....perhaps it was in regards to a previously designed and implemented product from another program? Doesn't seem to throw an error when such a product is not currently listed within the Best Buy inventory via MySQL.... not a bad thing? Otherwise, I'd have to comment this whole line of code out.
            productRepo.DeleteProduct(987);//We have to write this method in here to be called within the main method prior to the block of code below being executed because otherwise, the program will add a duplicate product to the best buy inventory, and it doesn't like that.... since this program is limited with its current parameters, and is set to be run whenever this IDE is opened, just ensure that the program itself at least runs without errors, we have to be sure that any product that is added to the best buy inventory doesn't already exist, especially if such was a result of this program being run at any point in the past.
            productRepo.DeleteProduct(933);

            #region Querying Departments Table
            Console.WriteLine("Listed below are the current departments from a local venue.");
            Console.WriteLine();
            var departmentDepos = departmentRepo.GetAllDepartments();//Here, we're utilizing the instance of the DapperDepartmentRepository class to call upon its GetAllDepartments method.
            Console.WriteLine();
            //foreach (var depo in depos)//all of those can be taken in put into a method that can be called whenever we want it to be, and then it won't feel like we're repeating ourselves by typing this script out every time we wish to see the same table's data being pulled from the database.
            //{
            //    Console.WriteLine($"Department ID: {depo.departmentID} Department Name: {depo.Name}");
            //}
            PrintDepartmentData(departmentDepos);//mapping data from tables to objects in applications.
            Console.WriteLine();
            Console.WriteLine("Do you wish to add a department to the venue at this time?");
            Console.WriteLine();
            var userInput = Console.ReadLine();
            if (userInput.ToLower() == "yes")
            {
                Console.WriteLine("Ok, what's the name of the department you wish to add?");
                userInput = Console.ReadLine();//stores the user's input.
                departmentRepo.InsertDepartment(userInput);//adds the user's input to the database.
                //repo.DeleteDepartment(userInput)//is there an extension method, or a method from the dapperdepartmentrepository class that can be written into C# that allows the user to delete data? In this case, a department?
                departmentRepo.GetAllDepartments();
                Console.WriteLine();
                PrintDepartmentData(departmentDepos);
            }
            else if (userInput.ToLower() == "no")
            {
                Console.WriteLine();
                Console.WriteLine("Very well, take care.");
                //Console.WriteLine("Very well, would you like to delete a department instead?");
                //return;
                //var userReply = Console.ReadLine();
                //if (userReply.ToLower() == "yes")
                //{
                //    Console.WriteLine();
                //    Console.WriteLine("Alright, which department do want to delete?");
                //    userReply = Console.ReadLine();
                //    departmentRepo.DeleteDepartment(userReply);//cannot immediately convert userReply from a string into an int. Need some other conversion programming before this.
                //    departmentRepo.GetAllDepartments();
                //    Console.WriteLine();
                //    PrintDepartmentData(departmentDepos);
                //}
            }
            #endregion

            Console.WriteLine();
            Console.WriteLine("Press the return key to continue.");
            Console.ReadLine();
            Console.WriteLine();

            #region Querying Products Table
            //DapperProductRepository productRepo = new DapperProductRepository(conn);//each of these scripts written within the main method could be reformatted into their own separate methods that are then called within the main method, offering more flexibility, and choice to the user, rather than just running one right after the other.
            //productRepo.GetAllProducts = var products;//lol, I tried writing the same line of code down below backwards, thinking it might not matter....
            var products = productRepo.GetAllProducts();
            //PrintProductData(products);
            //productRepo.DeleteProduct(999);
            //productRepo.DeleteProduct(901);
            //productRepo.DeleteProduct(987);//We have to write this method in here to be called within the main method prior to the block of code below being executed because otherwise, the program will add a duplicate product to the best buy inventory, and it doesn't like that.... since this program is limited with its current parameters, and is set to be run whenever this IDE is opened, just ensure that the program itself at least runs without errors, we have to be sure that any product that is added to the best buy inventory doesn't already exist, especially if such was a result of this program being run at any point in the past.
            //productRepo.DeleteProduct(933);
            Console.WriteLine("Listed below are the current products from a local venue.");
            Console.WriteLine();
            //var products = productRepo.GetAllProducts();//if I leave this line of code here without its duplicate copy placed where it is above, then the application executes and never actually displays the newly added products to the inventory, though they still show on best buy's inventory in the MySQL database.
            //Console.WriteLine();
            //PrintProductData(products);
            //Console.WriteLine();
            //Console.WriteLine("Do you wish to add a product to the venue at this time?");//designing a user input based program in this instance would be both time consuming and painful, as there would be many different prompts, and exception handling to prevent the program from prematurely terminating.
            //Console.WriteLine();
            //var userResponse = Console.ReadLine();
            //if (userResponse.ToLower() == "yes")
            //{
            //    Console.WriteLine("Ok, what's the name of the department you wish to add?");
            //    userResponse = Console.ReadLine();
            //    productRepo.CreateProduct();//productRepo.InsertProduct(userInput);
            //    productRepo.GetAllProducts();
            //    Console.WriteLine();
            //    PrintDepartmentData(products);
            //}
            //else if (userResponse.ToLower() == "no")
            //{
            //    Console.WriteLine();
            //    Console.WriteLine("Sure, no worries.");
            //}
            Console.WriteLine("Now, we'll add a few products to the inventory -- their ID's will be numerically placed at the end of the entire product inventory, so we don't have to scroll through the whole list in order to find them.");
            Console.WriteLine();
            productRepo.CreateProduct("Omega Sound System", 4097.79, 1, 999, 1, "14");//"14");//VSC gave me a hard time on the stocklevel aspect, so I've removed it for now. Updated; I fixed it -- added it back in.//utilizes a method to add data to a database.
            productRepo.CreateProduct("Hyperbolic Time Machine", 7948.87, 1, 987, 1, "1");//now, that I've gotten this part of this script up and working as intended, the problem now remains that the database on SQL still contains this script's yield if it was ever run at least once, which means that if it runs again, it will terminate prematurely due to error, specifically regarding creating new products in the products table that already exist and with all of the same values for the same columns. I'll need to find a way to have it removed or perhaps there's a way create an exception handler for this.
            productRepo.CreateProduct("Grant's Tagliatelle Pasta Bolognese (Frozen)", 14.99, 1, 901, 0, "27");
            productRepo.CreateProduct("Fun Dip", 1.2499, 1, 933, 0, "8764");//I was wondering why this application didn't error out on product ID 933 before or after product ID 987, and it's because product ID 933 gets created after product ID 987, and the script didn't throw an error on product ID 999 because there's already code written in above this block that deletes said product ID prior to it being added back into the Best Buy inventory once more. Crazy the things you realize when you come back to your own programming with a fresh mind.... even weeks after the fact.
            productRepo.GetAllProducts();//fetches all the data from the data base.
            Thread.Sleep(1000);//this was placed here to test whether or not a little delay time might have been beneficial to the computer getting a little more time to load up those newly added products into the product list, but it made no difference... wondering if this might be because the connectivity between the application and its executable is even slower than initially anticipated?
            Console.WriteLine();
            Console.WriteLine("Lastly, at least one product will be updated from the inventory.");
            Console.WriteLine();
            var ProductToUpdate = productRepo.GetProduct(987);
            ProductToUpdate.Name = "Super Hyperbolic Time Machine";
            ProductToUpdate.Price = 999999.99;
            ProductToUpdate.StockLevel = "2";
            ProductToUpdate.OnSale = 0;
            productRepo.UpdateProduct(ProductToUpdate);
            productRepo.GetAllProducts();
            PrintProductData(products);//prints all of our fetched data from the database to the console.
            #endregion//all newly created products and departments programmed within this application do actually show up in the MySQL database, but they don't seem to print here for some reason? Despite being instructed to?
        }

        #region DepartmentPrintDataMethod
        private static void PrintDepartmentData(IEnumerable<Department> depos)//these variables can be reused for other methods since they're written unto themselves exclusive to the main method and its contents.
        {
            foreach (var depo in depos)
            {
                Console.WriteLine($"Department ID: {depo.departmentID} Department Name: {depo.Name}");
            }
        }
        #endregion

        #region ProductPrintDataMethod
        private static void PrintProductData(IEnumerable<Product> depos)
        {
            foreach (var depo in depos)
            {
                Console.WriteLine($"Product ID: {depo.ProductID} Product Name: {depo.Name} Product Price: ${depo.Price} Product is on sale? {depo.OnSale}(1 = yes, 0 = no) Product Category ID: {depo.CategoryID} Product Stock Level: {depo.StockLevel}");
            }//for the OnSale property of this database's table we're pulling from, would it be possible to convert its value to Booleans? instead of returning integers of 1 or 0?
        }
        #endregion

        #region DepartmentDeletionMethod
        //private static void DeleteDepartment(IEnumerable<Department> depos)
        //{
        //    foreach (var depo in depos)
        //    {

        //    }
        //}
        #endregion//obsolete
    }
}
