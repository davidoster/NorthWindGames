using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWindGames
{
    class Program
    {
        static void Main(string[] args)
        {
            // INSERT or Add........ a Customer
            using(NorthwindEntities db = new NorthwindEntities())
            {

                db.Customers.Add(new Customer()
                {
                    CustomerID = "AAA",
                    ContactName = "GP",
                    CompanyName = "PeopleCert",
                    Country = "Greece",
                    City = "Athens"
                });
                //Console.WriteLine(db.GetValidationErrors().Count());
                //foreach (var error in db.GetValidationErrors())
                //{
                //    Console.WriteLine($"{error.ValidationErrors.Select(e => e.ErrorMessage).Aggregate((a, b) => a + "\n" + b)}");
                //}
                db.SaveChanges();


                // DELETE FROM dbo.Customers WHERE CustomerID = "AAA"
                //db.Customers.Remove(db.Customers.Find("AAA"));
                //db.SaveChanges();

                // UPDATE
                var customer = db.Customers.Find("AAA");
                if(customer != null)
                {
                    customer.ContactName = "George Pasparakis";
                    db.SaveChanges();
                }
            }
            
            //var temp = new { id = 1000, name = "AAA" };
            //var myCat = new Category();
            //Console.WriteLine(temp);

            //Console.WriteLine("LINQ to SQL");
            //ShowCategories();

            //Console.WriteLine("EntityClient");
            //ShowCategories2();

            using (NorthwindEntities database = new NorthwindEntities())
            {
                // query syntax
                var customerQuery = from customer in database.Customers
                                    where (customer.Country == "Greece")
                                    select new
                                    {
                                        cId = customer.CustomerID,
                                        cName = customer.ContactName,
                                        cCompanyName = customer.CompanyName,
                                        cCountry = customer.Country
                                    };


                // lambda syntax
                //var customerQuery2 = database.Customers.Where(c => c.Country == "USA");
                //var customersFromUSA = from customer in customerQuery2
                //select new
                //{
                //    cId = customer.CustomerID,
                //    cName = customer.ContactName,
                //    cCompanyName = customer.CompanyName,
                //    cCountry = customer.Country
                //};



                // System.Data.Entity.DynamicProxies.Customer_6444D2E9A193C89BD76591582E283DE9C2DD31EB5B12A2E29D9D2BF81EB1AEFD
                foreach (var c in customerQuery)
                {
                    Console.WriteLine(c);
                }
            }
        }

        // LINQ to SQL
        private static void ShowCategories()
        {
            using (NorthwindEntities context = new NorthwindEntities())
            {
                try
                {
                    var query = from category in context.Categories
                                select new
                                {
                                    categoryID = category.CategoryID,
                                    categoryName = category.CategoryName,
                                    categoryDescription = category.Description
                                };
                    foreach (var categoryInfo in query)
                    {
                        Console.WriteLine("\t{0}\t{1}\t{2}", categoryInfo.categoryID, categoryInfo.categoryName, categoryInfo.categoryDescription);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        // EntityClient
        private static void ShowCategories2()
        {
            string sql = "SELECT c.CategoryID, c.CategoryName FROM NorthWindEntities.Categories AS c";
            using (EntityConnection con = new EntityConnection("name=NorthwindEntities"))
            {
                try
                {
                    con.Open();
                    using (EntityCommand command = new EntityCommand(sql, con))
                    {
                        using (EntityDataReader reader = command.ExecuteReader(System.Data.CommandBehavior.SequentialAccess))
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("\t{0}\t{1}", reader[0], reader[1]);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
