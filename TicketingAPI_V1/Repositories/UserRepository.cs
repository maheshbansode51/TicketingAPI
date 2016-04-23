using FluentValidation;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TicketingAPI_V1.Models;
using TicketingAPI_V1.Validators;
using TicketingAPI_V1.Extensions;

namespace TicketingAPI_V1.Repositories
{
    public class UserRepository
    {
        protected static IMongoClient _client;

        protected static IMongoDatabase _database;

        private readonly IValidator<LoginModel> _userLoginValidator;

        private readonly IValidator<RegisterModel> _registerUserValidator;

        public UserRepository()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TicketingSystem"].ToString();
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase("dticketing");

            _userLoginValidator = new LoginUserValidator();
            _registerUserValidator = new RegisterUserValidator();
        }

        public async Task<BaseResult<StringIdResult>> CreateUser(RegisterModel model)
        {
            var result = new BaseResult<StringIdResult>();

            if(model!=null)
            {
                var valResult = _registerUserValidator.Validate(model);

                if(valResult.IsValid)
                {
                    try
                    {
                        var userCollection = _database.GetCollection<RegisterModel>("cUsers");

                        //find user by phone number if not exist then only register
                        var filter = Builders<RegisterModel>.Filter.Eq("PhoneNumber", model.PhoneNumber);

                        var user = await userCollection.Find(filter).ToListAsync();

                        if (user.Count == 0)
                        {
                            model.Id = Guid.NewGuid().ToString();

                            await userCollection.InsertOneAsync(model);                    result.Suceeded = true;

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
                }
                else
                {
                    result.Suceeded = false;
                    result.Errors.AddRange(valResult.Errors.ToErrorStringList());
                }
            }  
            else
            {
                result.Suceeded = false;
                result.AddError("User can't be null");
            }
            
            return result;
        }

        public async Task<BaseResult<StringIdResult>> GetUser(LoginModel model)
        {
            var result = new BaseResult<StringIdResult>();

            if (model!= null)
            {
                var valResult=_userLoginValidator.Validate(model);

                if(valResult.IsValid)
                {
                    try
                    {
                        var userCollection = _database.GetCollection<RegisterModel>("cUsers");

                        var builder = Builders<RegisterModel>.Filter;

                        var filter = builder.Eq("PhoneNumber", model.PhoneNumber) & builder.Eq("Password", model.Password);

                        var users = await userCollection.Find(filter).ToListAsync();

                        if (users.Count > 0)
                        {
                            StringIdResult r = new StringIdResult();
                            r.Id = users[0].Id;

                            result.Value = r;
                            result.Suceeded = true;
                        }
                        else
                        {
                            result.Suceeded = false;
                            result.AddError("Wrong phoneNumber or password.");
                        }
                    }
                    catch (Exception)
                    {
                        result.Suceeded = false;
                        result.AddError("An error occured while registering user.");
                    }
                }
                else
                {
                    result.Suceeded = false;
                    result.Errors.AddRange(valResult.Errors.ToErrorStringList());                   
                }
            }
            else
            {
                result.SetRequiredFieldsMissing("PhoneNumber", "Password");
                result.Suceeded = false;
            }             
           
            return result;
        }

        public async Task<RegisterModel> GetUser(string userId)
        {
            var userCollection = _database.GetCollection<RegisterModel>("cUsers");
            var filter = Builders<RegisterModel>.Filter.Eq("Id",userId);
            var user = await userCollection.Find(filter).ToListAsync();
            if (user != null && user.Count == 1)
                return user[0];
            else
                return null;
        }
    }
}