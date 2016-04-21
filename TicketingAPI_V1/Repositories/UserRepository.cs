using MongoDB.Bson;
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
                var filter = Builders<RegisterModel>.Filter.Eq("PhoneNumber", model.PhoneNumber);

                var user = await userCollection.Find(filter).ToListAsync();

                if(user.Count==0)
                {
                    model.Id = Guid.NewGuid().ToString();

                    await userCollection.InsertOneAsync(model);
                    result.Suceeded = true;

                    StringIdResult idResult = new StringIdResult();
                    idResult.Id = model.Id;
                    result.Value = idResult;
                }
                else
                {
                    result.Suceeded = false;
                    result.AddError("Already registered phone number");
                }

                
            }
            catch (Exception)
            {
                result.Suceeded = false;
                result.AddError("An error occured while registering user");
            }


            return result;
        }

    }
}