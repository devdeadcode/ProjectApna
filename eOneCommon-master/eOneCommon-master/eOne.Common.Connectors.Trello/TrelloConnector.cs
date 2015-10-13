using System;
using System.Collections.Generic;
using System.Linq;
using eOne.Common.Connectors.Trello.Models;
using eOne.Common.DataConnectors;
using eOne.Common.DataConnectors.Rest;

namespace eOne.Common.Connectors.Trello
{
    public class TrelloConnector : RestConnector
    {

        #region Entity IDs

        public const int EntityIdBoard = 1;
        public const int EntityIdCard = 2;
        public const int EntityIdList = 3;
        public const int EntityIdChecklist = 4;
        public const int EntityIdAction = 5;

        #endregion

        public TrelloConnector()
        {
            Name = "Trello";
            Group = ConnectorGroup.ToDoList;
            BaseUrl = "https://api.trello.com/1";
            Key = "645c4f40b142b07e63ed1ff12d1ed227";
            Secret = "9c86c5dbf78098b8f80103a9969c2418a816df782cbb5ede100eeb0e530f4b0e";
            Multicompany = true;
            CompanyPrompt = "Organization";
            CompanyPluralPrompt = "Organizations";
            AuthenticationType = RestConnectorAuthenticationType.UrlParameter;
            AddSetup();
        }

        #region Public methods

        public override string GetEndpoint(ConnectorQuery query)
        {
            if (query == null) return string.Empty;
            switch (query.Entity.Id)
            {
                case EntityIdBoard:
                    return "boards";
                case EntityIdCard:
                    if (!query.HasOrConjunctives)
                    {
                        foreach (var restriction in query.Restrictions.Where(restriction => restriction.Field.Name == "closed"))
                        {
                            switch (restriction.RestrictionType)
                            {
                                case ConnectorRestriction.ConnectorRestrictionType.Equals:
                                    return restriction.Values[0].Constant == "true" ? "cards/closed" : "cards/open";
                                case ConnectorRestriction.ConnectorRestrictionType.DoesNotEqual:
                                    return restriction.Values[0].Constant == "true" ? "cards/open" : "cards/closed";
                            }
                        }
                    }
                    return "cards";
                case EntityIdAction:
                    return "actions";
                case EntityIdChecklist:
                    return "checklists";
                case EntityIdList:
                    return "lists";
            }
            return string.Empty;
        }

        public override IEnumerable<object> Deserialize(string data, ConnectorQuery query)
        {
            var boards = new List<TrelloBoard>();
            foreach (var boardJson in query.Companies.Select(company => $"organizations/{company.DatabaseName}/boards").Select(GetResponse))
            {
                boards.AddRange(DeserializeJson<List<TrelloBoard>>(boardJson));
            }
            if (query.Entity.Id == EntityIdBoard) return boards;
            switch (query.Entity.Id)
            { 
                case EntityIdCard:
                    var cards = new List<TrelloCard>();
                    foreach (var board in boards)
                    {
                        var endpoint = $"boards/{board.id}/cards";
                        var cardJson = GetResponse(endpoint);
                        var boardCards = DeserializeJson<List<TrelloCard>>(cardJson);
                        foreach (var card in boardCards) card.board = board;
                        cards.AddRange(boardCards);
                    }
                    return cards;
                case EntityIdAction:
                    return null; // todo
                case EntityIdChecklist:
                    return null; // todo
                case EntityIdList:
                    return null; // todo
            }
            return null;
        }

        public override void Initialise()
        {
            UrlParameters = new List<Tuple<string, string>>
            {
                new Tuple<string, string>("token", Token),
                new Tuple<string, string>("key", Key)
            };
            AddEntities();
            AddCompanies();
        }

        public override void RunAction(ConnectorAction action, List<Tuple<string, string>> parameters)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private methods

        private new void AddSetup()
        {
            var step1 = new ConnectorSetup.ConnectorSetupStep
            {
                Number = 1,
                Header = "Connector Credentials",
                TopDescription = "Please enter in a name for your new connector, along with your Trello token and user name. Click " +
                    $"<a href=\"https://trello.com/1/authorize?key={Key}&name={"PopDock"}&expiration=never&response_type=token\" target=\"_blank\">here</a> " +
                    "to generate your token. ",
                BottomDescription = "Click Finish to complete the installation."
            };
            step1.Fields.Add(new ConnectorSetupField(ConnectorSetupField.FieldNameName, "Connector name", ConnectorSetupField.ConnectorSetupFieldType.String, "Trello", true));
            step1.Fields.Add(new ConnectorSetupField(ConnectorSetupField.FieldNameToken, "Token", ConnectorSetupField.ConnectorSetupFieldType.String, "", true));
            step1.Fields.Add(new ConnectorSetupField(ConnectorSetupField.FieldNameUser, "User name", ConnectorSetupField.ConnectorSetupFieldType.String, "", true));
            Setup.Steps.Add(step1);
        }
        private void AddEntities()
        {
            AddEntity(EntityIdBoard, "Boards", typeof(TrelloBoard), true);
            AddEntity(EntityIdCard, "Cards", typeof(TrelloCard));
        }
        private void AddCompanies()
        {
            var endpoint = $"members/{Username}/organizations";
            var orgJson = GetResponse(endpoint);
            var orgs = DeserializeJson<List<TrelloOrganization>>(orgJson);
            if (orgs == null) return;
            var id = 1;
            foreach (var org in orgs)
            {
                var company = new DataConnectorCompany
                {
                    Id = id,
                    Name = org.displayName,
                    DatabaseName = org.name
                };
                Companies.Add(company);
                id++;
            }
        }

        #endregion

    }
}
