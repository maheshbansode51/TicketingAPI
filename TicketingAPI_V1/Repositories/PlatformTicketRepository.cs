using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TicketingAPI_V1.DataModels;
using TicketingAPI_V1.Models;
using Omu.ValueInjecter;

namespace TicketingAPI_V1.Repositories
{
    public class PlatformTicketRepository : IPlatformTicketRepository
    {
        protected static IMongoClient _client;

        protected static IMongoDatabase _database;

        private UserRepository _userRepository;
        public PlatformTicketRepository()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TicketingSystem"].ToString();
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase("dticketing");

            //TODO: It will create multiple instances of mongo
            _userRepository = new UserRepository();
        }

        public async Task<BaseResult<DisplayPlatformTicketModel>> BookPlatformTicket(string userId, PlatformTicketModel model)
        {
            BaseResult<DisplayPlatformTicketModel> result = new BaseResult<DisplayPlatformTicketModel>();

            if (model != null && !String.IsNullOrWhiteSpace(userId))
            {

                try
                {
                    //Get user
                    var user = await _userRepository.GetUser(userId);

                    if (user != null)
                    {
                        PlatformTicketDataModel dataModel = new PlatformTicketDataModel();
                        dataModel.InjectFrom(model);
                        dataModel.TicketStatus = TicketStatus.Booked;
                        dataModel.Id = Guid.NewGuid().ToString();
                        dataModel.UserId = user.Id;
                        //Insert data to mongo      

                        var platformTicketCollection = _database.GetCollection<PlatformTicketDataModel>("cPTickets");
                        await platformTicketCollection.InsertOneAsync(dataModel);

                        var filter = Builders<PlatformTicketDataModel>.Filter.Eq("Id", dataModel.Id);

                        var bookedTicket = await platformTicketCollection.Find(filter).ToListAsync();

                        DisplayPlatformTicketModel displaymodel = null;

                        if (bookedTicket != null && bookedTicket.Count == 1)
                        {
                            displaymodel = Convert(bookedTicket.FirstOrDefault());
                        }
                        result.Suceeded = true;
                        result.Value = displaymodel;
                    }
                    else
                    {
                        result.Suceeded = false;
                        result.AddError("User is unauthenticated.");
                    }

                }
                catch (Exception)
                {
                    result.Suceeded = false;
                    result.AddError("Error while booking platformticket, please try after some time.");
                }
            }
            else
            {
                result.Suceeded = false;
                result.SetRequiredFieldsMissing("Fields missing");
            }

            return result;
        }

        private DisplayPlatformTicketModel Convert(PlatformTicketDataModel dataModel)
        {
            DisplayPlatformTicketModel model = null;

            if (dataModel != null)
            {
                model = new DisplayPlatformTicketModel();
                model.InjectFrom(dataModel);
                model.TicketStatus = dataModel.TicketStatus;
            }
            return model;
        }
    }
}