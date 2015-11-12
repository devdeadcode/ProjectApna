using System;
using System.Collections.Generic;
using System.Linq;
using eOne.Common.Connectors.HappyFox.Models;
using eOne.Common.DataConnectors.Rest;

namespace eOne.Common.Connectors.HappyFox
{
    public class HapppyFoxConnector : RestConnector
    {
        #region Enitiy IDs
        public const int EntityIdTicket = 1;
        public const int EntityIdStaff = 2;
        public const int EntityIdContact = 3;
        public const int EntityIdKnowledgeBaseArticle = 4;
        public const int EntityIdTicketUpdate = 5;
        public const int EntityIdTicketMessage = 6;
        public const int EntityIdTicketSummary = 7;
        public const int EntityIdStaffSummary = 8;
        #endregion

        public HapppyFoxConnector()
        {
            Name = "HapyFox";
            Group = ConnectorGroup.Helpdesk;
            AuthenticationType = RestConnectorAuthenticationType.Basic;
            BaseUrl = "https://ethree.happyfox.com/api/1.1/json/";
            SerializationType = RestConnectorSerializationType.Json;
            AddSetup();
        }

        public override void Initialise()
        {
            Username = Key;
            Password = Token;

            // add entities
            var ticketEntity = AddEntity(EntityIdTicket, "Tickets", typeof(HappyFoxTicket));
            var staffEntity = AddEntity(EntityIdStaff, "Staff", typeof(HappyFoxStaff));
            var contactEntity = AddEntity(EntityIdContact, "Contacts", typeof(HappyFoxContact));
            AddEntity(EntityIdKnowledgeBaseArticle, "Knowledge base articles", typeof(HappyFoxKnowledgeBasedArticle));
            var ticketUpdateEntity = AddEntity(EntityIdTicketUpdate, "Ticket updates", typeof(HappyFoxTicketUpdate));
            var ticketMessageEntity = AddEntity(EntityIdTicketMessage, "Ticket messages", typeof(HappyFoxTicketMessage));
            var ticketSummaryEntity = AddEntity(EntityIdTicketSummary, "Ticket summary", typeof(HappyFoxTicketSummary));

            // set default max records
            foreach (var entity in Entities) entity.DefaultMaxRecords = 100;

            // add relationships
            ticketEntity.AddRelatedEntity("Ticket updates", ticketUpdateEntity, "id", "ticket_id");
            ticketEntity.AddRelatedEntity("Ticket messages", ticketMessageEntity, "id", "ticket_id");
            staffEntity.AddRelatedEntity("Tickets", ticketEntity, "id", "staff_id");
            contactEntity.AddRelatedEntity("Tickets", ticketEntity, "id", "contact_id");
            ticketSummaryEntity.AddRelatedEntity("Tickets", ticketEntity, "id", "ticket_id");

            // add favorites
            HappyFoxFavoriteHelper.AddTicketFavorites(ticketEntity);
            HappyFoxFavoriteHelper.ContactFavorites(contactEntity);
            HappyFoxFavoriteHelper.UpdateFavorites(ticketUpdateEntity);
        }

        public override string GetEndpoint(ConnectorQuery query)
        {
            if (query == null) return string.Empty;
            switch (query.Entity.Id)
            {
                case EntityIdTicket:
                    return "tickets/";
                case EntityIdStaff:
                    return "staff/";
                case EntityIdContact:
                    return "users/";
                case EntityIdKnowledgeBaseArticle:
                    return "kb/sections/";
                case EntityIdTicketUpdate:
                case EntityIdTicketMessage:
                case EntityIdTicketSummary:
                    return "tickets/";

            }
            return string.Empty;
        }

        public override IEnumerable<object> Deserialize(string data, ConnectorQuery query)
        {
            switch (query.Entity.Id)
            {
                case EntityIdTicket:
                    data = "[\n    " + data + "    \n]";
                    var ticketResult = DeserializeJson<List<HappyFoxTicketCollection>>(data);
                    var ticketResponse = new List<HappyFoxTicket>();
                    foreach (var inst in ticketResult[0].data)
                    {
                        inst.ticketCollection = ticketResult[0];
                        ticketResponse.Add(inst);
                    }
                    return ticketResponse;

                case EntityIdStaff:
                    return DeserializeJson<List<HappyFoxStaff>>(data);

                case EntityIdContact:
                    data = "[\n    " + data + "    \n]";
                    var result = DeserializeJson<List<HappyFoxContactCollection>>(data);
                    var response = new List<HappyFoxContact>();
                    foreach (var inst in result[0].data)
                    {
                        inst.contactCollection = result[0];
                        response.Add(inst);
                    }
                    return response;

                case EntityIdKnowledgeBaseArticle:
                    return DeserializeJson<List<HappyFoxKnowledgeBasedArticle>>(data);

                case EntityIdTicketUpdate:
                    data = "[\n    " + data + "    \n]";
                    var ticketCollections = DeserializeJson<List<HappyFoxTicketCollection>>(data);
                    var ticketUpdates = new List<HappyFoxTicketUpdate>();
                    var updateTickets = new List<HappyFoxTicket>();

                    foreach (var ticketInst in ticketCollections[0].data)
                    {
                        ticketInst.ticketCollection = ticketCollections[0];
                        updateTickets.Add(ticketInst);
                    }
                    foreach (var t in updateTickets)
                    {
                        foreach (var t1 in t.updates)
                        {
                            t1.ticket = t;
                            ticketUpdates.Add(t1);
                        }
                    }

                    return ticketUpdates;


                case EntityIdTicketMessage:
                    data = "[\n    " + data + "    \n]";
                    var collections = DeserializeJson<List<HappyFoxTicketCollection>>(data);
                    var messageUpdates = new List<HappyFoxTicketUpdate>();
                    var messageTickets = new List<HappyFoxTicket>();
                    var messages = new List<HappyFoxTicketMessage>();

                    foreach (var ticketInst in collections[0].data)
                    {
                        ticketInst.ticketCollection = collections[0];
                        messageTickets.Add(ticketInst);
                    }
                    foreach (var t in messageTickets)
                    {
                        foreach (var t1 in t.updates)
                        {
                            t1.ticket = t;
                            messageUpdates.Add(t1);

                            foreach (var t2 in messageUpdates)
                            {
                                t2.message.update = t2;
                                messages.Add(t2.message);
                            }
                        }
                    }

                    return messages;

                case EntityIdTicketSummary:
                    data = "[\n    " + data + "    \n]";
                    var ticketSummary = DeserializeJson<List<HappyFoxTicketCollection>>(data);
                    
                    var assignedToSummary = new List<HappyFoxTicketSummary>();
                    

                    var staffData = GetResponse("staff/");
                    var staff = DeserializeJson<List<HappyFoxStaff>>(staffData);
                    var allTheData = new List<List<HappyFoxTicket>>();

                    foreach (var val in staff)
                    {
                        var assignedTickets = new List<HappyFoxTicket>();
                        foreach (var ticket in ticketSummary[0].data)
                        {
                            if (ticket.assigned_to != null)
                            {
                                if (ticket.assigned_to.name == val.name)
                                {

                                    assignedTickets.Add(ticket);
                                    
                                }
                                
                            }
                            
                        }
                        
                        assignedToSummary.Add(new HappyFoxTicketSummary()
                        {
                            name = val.name,
                            email = val.email,
                            active = val.active,
                            role_name = val.role.name,
                            role_id = val.role.id,
                            ticketList = assignedTickets
                        });
                        
                    }

                    return assignedToSummary;

            }
            return null;
        }

        public override void RunAction(ConnectorAction action, List<Tuple<string, string>> parameters)
        {
            throw new NotImplementedException();
        }

        #region Private methods

        private new void AddSetup()
        {
            var step1 = new ConnectorSetup.ConnectorSetupStep
            {
                Number = 1,
                Header = "Connector Credentials",
                TopDescription = "Please enter in a name for your new connector, along with your Insightly API Key. ",
                BottomDescription = "Click Finish to complete the installation."
            };
            step1.Fields.Add(new ConnectorSetupField(ConnectorSetupField.FieldNameName, "Connector name", ConnectorSetupField.ConnectorSetupFieldType.String, "HappyFox", true));
            step1.Fields.Add(new ConnectorSetupField(ConnectorSetupField.FieldNameKey, "API Key", ConnectorSetupField.ConnectorSetupFieldType.String, "", true));
            Setup.Steps.Add(step1);
        }

        #endregion
    }
}
