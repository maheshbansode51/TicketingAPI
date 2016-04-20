using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TicketingAPI_V1.Models;

namespace TicketingAPI_V1.Repositories
{
    public class UserRepository
    {
        protected static IMongoClient _client;
        protected static IMongoDatabase _database;

        public UserRepository()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TicketingSystem"].ToString();
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase("dticketing"); 
        }

        public async Task<BaseResult<StringIdResult>> CreateUser(RegisterModel model)
        {
            var result = new BaseResult<StringIdResult>();
            try
            {
                var userCollection = _database.GetCollection<RegisterModel>("cUsers");

                //find user by phone number if not then only register

                await userCollection.InsertOneAsync(model);
                result.Suceeded = true;
            }
            catch (Exception)
            {
                result.Suceeded = false;
                result.AddError("An error occured while registering user");
            }
            

            return baseResult;
        }

    }
}