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
    public class TicketRepository
    {
        protected static IMongoClient _client;

        protected static IMongoDatabase _database;

        private UserRepository _userRepository;
        public TicketRepository()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TicketingSystem"].ToString();
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase("dticketing");

            //TODO: It will create multiple instances of mongo :)
            _userRepository = new UserRepository();
        }

        public async Task<BaseResult<List<DisplayTicketModel>>> GetTicketsByUserId(string userId)
        {
            BaseResult<List<DisplayTicketModel>> result = new BaseResult<List<DisplayTicketModel>>();

            if (!String.IsNullOrWhiteSpace(userId))
            {
                //Get Tickets by userId

                var ticketCollection = _database.GetCollection<TicketDataModel>("cTickets");
                var filter = Builders<TicketDataModel>.Filter.Eq("UserId", userId);
                var tickets = await ticketCollection.Find(filter).ToListAsync();

                if (tickets != null && tickets.Count > 0)
                {
                    List<DisplayTicketModel> model = Convert(tickets);
                    result.Suceeded = true;
                    result.Value = model;
                }
                else
                {
                    result.Suceeded = true;
                    result.AddError("User don't have any tickets");
                }
            }
            else
            {
                result.Suceeded = false;
                result.SetRequiredFieldsMissing("userId");
            }

            return result;
        }

        public async Task<BaseResult<DisplayTicketModel>> BookTicket(string userId, BookTicketModel model)
        {
            BaseResult<DisplayTicketModel> result = new BaseResult<DisplayTicketModel>();

            try
            {
                if (model != null && !String.IsNullOrWhiteSpace(userId))
                {
                    //Get User
                    var user = await _userRepository.GetUser(userId);
                    if (user != null)
                    {
                        TicketDataModel dataModel = Convert(model);
                        dataModel.Id = Guid.NewGuid().ToString();
                        dataModel.UserId = user.Id;
                        dataModel.TicketStatus = TicketStatus.Booked;

                        //insert ticket
                        var ticketCollection = _database.GetCollection<TicketDataModel>("cTickets");
                        await ticketCollection.InsertOneAsync(dataModel);

                        //retrieve ticket
                        var filter = Builders<TicketDataModel>.Filter.Eq("Id", dataModel.Id);
                        var bookedTicket = await ticketCollection.Find(filter).ToListAsync();
                        if (bookedTicket != null && bookedTicket.Count == 1)
                        {
                            DisplayTicketModel displayModel = Convert(bookedTicket.FirstOrDefault());
                            result.Value = displayModel;
                            result.Suceeded = true;
                        }

                    }
                    else
                    {
                        result.Suceeded = false;
                        result.AddError("user is unauthenticated");
                    }
                }
                else
                {
                    result.Suceeded = false;
                    result.SetRequiredFieldsMissing("Fields missing");
                }
            }
            catch (Exception)
            {
                result.Suceeded = false;
                result.AddError("Error while booking ticket, please try after some time.");
            }

            return result;
        }

        public async Task<BaseResult<StringIdResult>> CancelTicket(string userId, string ticketId)
        {
            BaseResult<StringIdResult> result = new BaseResult<StringIdResult>();

            try
            {
                if (!String.IsNullOrWhiteSpace(userId) && !String.IsNullOrWhiteSpace(userId))
                {
                    //Get User
                    var user = await _userRepository.GetUser(userId);
                    if (user != null)
                    {
                        //Get Ticket by Id
                        var ticketCollection = _database.GetCollection<TicketDataModel>("cTickets");
                        var filter = Builders<TicketDataModel>.Filter.Eq("Id", ticketId);
                        var update = Builders<TicketDataModel>.Update.Set("TicketStatus", TicketStatus.Canceled);
                        var ticket = await ticketCollection.UpdateOneAsync(filter,update);

                        if (ticket.ModifiedCount == 1)
                        {                          
                            StringIdResult idResult = new StringIdResult();
                            idResult.Id = ticketId;

                            result.Suceeded = true;
                            result.Value = idResult;
                        }
                        else
                        {

                        }

                    }
                    else
                    {
                        result.Suceeded = false;
                        result.AddError("user is unauthenticated");
                    }
                }
                else
                {
                    result.Suceeded = false;
                    result.SetRequiredFieldsMissing("Fields missing");
                }
            }
            catch (Exception)
            {

                result.Suceeded = false;
                result.AddError("Error while cancelling ticket, please try after some time.");
            }

            return result;
        }


        #region PRIVATE METHODS

        private TicketDataModel Convert(BookTicketModel model)
        {
            TicketDataModel dataModel = null;

            if (model != null)
            {
                dataModel = new TicketDataModel();
                dataModel.InjectFrom(model);
            }
            return dataModel;
        }

        private DisplayTicketModel Convert(TicketDataModel dataModel)
        {
            DisplayTicketModel model = null;

            if (dataModel != null)
            {
                model = new DisplayTicketModel();
                model.InjectFrom(dataModel);
                model.TicketStatus = dataModel.TicketStatus.ToString();
                model.TicketType = dataModel.TicketType.ToString();
            }
            return model;
        }

        private List<DisplayTicketModel> Convert(List<TicketDataModel> dataModelList)
        {
            List<DisplayTicketModel> model = null;
            if (dataModelList != null)
            {
                model = new List<DisplayTicketModel>();
                foreach (var dataModel in dataModelList)
                {
                    model.Add(Convert(dataModel));
                }
            }

            return model;
        }

        #endregion
    }
}