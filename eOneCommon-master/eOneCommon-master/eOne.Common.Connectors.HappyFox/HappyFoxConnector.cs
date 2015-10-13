using System;
using System.Collections.Generic;
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
            BaseUrl = "https://eone.happyfox.com/api/1.1/json/";
            SerializationType = RestConnectorSerializationType.Json;
            AddSetup();
        }

        public override void Initialise()
        {
            Username = Key;
            Password = Token;
            AddEntity(EntityIdTicket, "Tickets", typeof(HappyFoxTicket));
            AddEntity(EntityIdStaff, "Staff", typeof(HappyFoxStaff));
            AddEntity(EntityIdContact, "Contacts", typeof(HappyFoxContact));
            AddEntity(EntityIdKnowledgeBaseArticle, "Knowledge base articles", typeof(HappyFoxKnowledgeBasedArticle));
            AddEntity(EntityIdTicketUpdate, "Ticket updates", typeof(HappyFoxTicketUpdate));
            AddEntity(EntityIdTicketMessage, "Ticket messages", typeof(HappyFoxTicketMessage));
            AddEntity(EntityIdTicketSummary, "Ticket summary", typeof(HappyFoxTicketSummary));
            //AddEntity(EntityIdStaffSummary, "Staff summary", typeof(HappyFoxTicketSummary));

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
                    for (int i = 0; i < updateTickets.Count; i++)
                    {
                        for (int j = 0; j < updateTickets[i].updates.Count; j++)
                        {
                            updateTickets[i].updates[j].ticket = updateTickets[i];
                            ticketUpdates.Add(updateTickets[i].updates[j]);
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
                    for (int i = 0; i < messageTickets.Count; i++)
                    {
                        for (int j = 0; j < messageTickets[i].updates.Count; j++)
                        {
                            messageTickets[i].updates[j].ticket = messageTickets[i];
                            messageUpdates.Add(messageTickets[i].updates[j]);

                            for (int k = 0; k < messageUpdates.Count; k++)
                            {
                                try
                                {
                                    messageUpdates[k].message.update = messageUpdates[k];
                                    messages.Add(messageUpdates[k].message);
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
                    for (int i = 0; i < assignedTicket.Count; i++)
                    {
                        if(assignedTicket[i].assigned_to != null)
                        {
                            assignedTicket[i].assigned_to.ticket = assignedTicket[i];
                            assignedToSummary.Add(assignedTicket[i].assigned_to);
                        }
                        else
                        {
                            assignedToSummary.Add(new HappyFoxTicketSummary
                            {
                                name = "Not assigned",
                                email = string.Empty,
                                active = false,
                                role_id = 0,
                                role_name = string.Empty
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
