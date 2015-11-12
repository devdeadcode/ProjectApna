using System;
using System.Collections.Generic;
using eOne.Common.Actions;
using eOne.Common.Connectors.HappyFox.Models;
using eOne.Common.Connectors.Service;
using eOne.Common.Query;
using eOne.Common.Setup;

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
            AuthenticationType = ServiceConnectorAuthenticationType.Basic;
            BaseUrl = "https://eone.happyfox.com/api/1.1/json/";
            SerializationType = ServiceConnectorSerializationType.Json;
            AddSetup();
        }

        public override void Initialise()
        {
            Username = Key;
            Password = Token;

            var ticketEntity = AddEntity(EntityIdTicket, "Tickets", typeof(HappyFoxTicket));
            var staffEntity = AddEntity(EntityIdStaff, "Staff", typeof(HappyFoxStaff));
            AddEntity(EntityIdContact, "Contacts", typeof(HappyFoxContact));
            AddEntity(EntityIdKnowledgeBaseArticle, "Knowledge base articles", typeof(HappyFoxKnowledgeBasedArticle));
            var ticketUpdatesEntity = AddEntity(EntityIdTicketUpdate, "Ticket updates", typeof(HappyFoxTicketUpdate));
            AddEntity(EntityIdTicketMessage, "Ticket messages", typeof(HappyFoxTicketMessage));
            AddEntity(EntityIdTicketSummary, "Ticket summary", typeof(HappyFoxTicketSummary));
            //AddEntity(EntityIdStaffSummary, "Staff summary", typeof(HappyFoxTicketSummary));

            ticketEntity.AddRelatedEntity("Updates", ticketUpdatesEntity, "id", "ticket_id");

        }

        public override string GetEndpoint(ConnectorQuery query)
        {
            if (query == null) return string.Empty;
            switch (query.Entity.Id)
            {
                case EntityIdTicket:
                    return "tickets/";
                case EntityIdStaff:
                    return "staff/"; //correct
                case EntityIdContact:
                    return "users/";
                case EntityIdKnowledgeBaseArticle:
                    return "kb/sections/"; //correct
                case EntityIdTicketUpdate:
                case EntityIdTicketMessage:
                case EntityIdTicketSummary:
                    return "tickets/";
                    //case EntityIdStaffSummary:
                    //    return "staff/";
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
                        //ticketResponse.Add(new HappyFoxTicket()
                        //{
                        //    id = inst.id,
                        //    subject = inst.subject,

                        //});
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
                    foreach (var ticket in updateTickets)
                    {
                        foreach (var update in ticket.updates)
                        {
                            update.ticket = ticket;
                            ticketUpdates.Add(update);
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
                    foreach (var messageTicket in messageTickets)
                    {
                        foreach (var update in messageTicket.updates)
                        {
                            update.ticket = messageTicket;
                            messageUpdates.Add(update);

                            foreach (var messageUpdate in messageUpdates)
                            {
                                try
                                {
                                    messageUpdate.message.update = messageUpdate;
                                    messages.Add(messageUpdate.message);
                                }
                                catch { }
                            }
                        }
                    }

                    return messages;

                case EntityIdTicketSummary:
                    data = "[\n    " + data + "    \n]";
                    var ticketSummary = DeserializeJson<List<HappyFoxTicketCollection>>(data);
                    var assignedTicket = new List<HappyFoxTicket>();
                    var assignedToSummary = new List<HappyFoxTicketSummary>();

                    foreach (var ticketInst in ticketSummary[0].data)
                    {
                        ticketInst.ticketCollection = ticketSummary[0];
                        assignedTicket.Add(ticketInst);
                    }
                    foreach (HappyFoxTicket ticket in assignedTicket)
                    {
                        if (ticket.assigned_to != null)
                        {
                            ticket.assigned_to.ticket = ticket;
                            assignedToSummary.Add(ticket.assigned_to);
                        }
                        else
                        {
                            assignedToSummary.Add(new HappyFoxTicketSummary
                            {
                                name = "Not assigned",
                                email = string.Empty,
                                active = false
                            });
                        }
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
