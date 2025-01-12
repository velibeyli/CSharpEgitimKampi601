using CSharpEgitimKampi601.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpEgitimKampi601.Services
{
    public class CustomerOperations
    {
        public void AddCustomer(Customer customer)
        {
            var connection = new MongoDbConnection();
            var customerCollection = connection.GetCustomerCollection();

            var document = new BsonDocument
            {
                {"CustomerName",customer.CustomerName },
                {"CustomerSurname",customer.CustomerSurame },
                {"CustomerCity",customer.CustomerCity },
                {"CustomerBalance",customer.CustomerBalance },
                {"CustomerShoppingCount",customer.CustomerShoppingCount }
            };

            customerCollection.InsertOne(document);
        }
        public List<Customer> GetAllCustomer()
        {
            var connection = new MongoDbConnection();
            var customerCollection = connection.GetCustomerCollection();
            var customers = customerCollection.Find(new BsonDocument()).ToList();
            List<Customer> customerList = new List<Customer>();
            foreach (var c in customers)
            {
                customerList.Add(new Customer
                {
                    CustomerId = c["_id"].ToString(),
                    CustomerName = c["CustomerName"].ToString(),
                    CustomerSurame = c["CustomerSurname"].ToString(),
                    CustomerCity = c["CustomerCity"].ToString(),
                    CustomerBalance = decimal.Parse(c["CustomerBalance"].ToString()),
                    CustomerShoppingCount = int.Parse(c["CustomerShoppingCount"].ToString())
                });
            }
            return customerList;
        }
        public void DeleteCustomer(string id)
        {
            var connection = new MongoDbConnection();
            var customerCollection = connection.GetCustomerCollection();
            var filter = Builders<BsonDocument>.Filter.Eq("_id",ObjectId.Parse(id));
            customerCollection.DeleteOne(filter);
        }
        public void UpdateCustomer(Customer customer)
        {
            var connection = new MongoDbConnection();
            var customerCollection = connection.GetCustomerCollection();
            var filter = Builders<BsonDocument>.Filter.Eq("_id",ObjectId.Parse(customer.CustomerId));
            var updatedValue = Builders<BsonDocument>.Update
                .Set("CustomerName", customer.CustomerName)
                .Set("CustomerSurname", customer.CustomerSurame)
                .Set("CustomerCity", customer.CustomerCity)
                .Set("CustomerBalance", customer.CustomerBalance)
                .Set("CustomerShoppingCount", customer.CustomerShoppingCount);

            customerCollection.UpdateOne(filter,updatedValue);
        }

        public Customer GetCustomerById(string id)
        {
            var connection = new MongoDbConnection();
            var customerCollection = connection.GetCustomerCollection();
            var filter = Builders<BsonDocument>.Filter.Eq("_id",ObjectId.Parse(id));

            var result = customerCollection.Find(filter).FirstOrDefault();

            return new Customer
            {
                CustomerBalance = decimal.Parse(result["CustomerBalance"].ToString()),
                CustomerCity = result["CustomerCity"].ToString(),
                CustomerId = id,
                CustomerName = result["CustomerName"].ToString(),
                CustomerSurame = result["CustomerSurname"].ToString(),
                CustomerShoppingCount = int.Parse(result["CustomerShoppingCount"].ToString())
            };
            
        }

    }
}
