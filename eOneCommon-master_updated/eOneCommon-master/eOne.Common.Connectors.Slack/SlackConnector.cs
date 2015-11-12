using System;
using System.Collections.Generic;
using System.Linq;
using eOne.Common.Actions;
using eOne.Common.Connectors.Service;
using eOne.Common.Connectors.Slack.Models;
using eOne.Common.Helpers;
using eOne.Common.Query;

namespace eOne.Common.Connectors.Slack
{
    public class SlackConnector : RestConnector
    {

        #region Constants

        #region Entity IDs

        public const int EntityIdChannel = 1;
        public const int EntityIdUser = 2;
        public const int EntityIdFile = 3;
        public const int EntityIdGroup = 4;
        public const int EntityIdGroupMember = 5;
        public const int EntityIdMessage = 6;

        #endregion

        #region Action IDs

        public const int ActionIdAddExternalMessage = 1;
        public const int ActionIdAddMessage = 2;
        public const int ActionIdRemoveMessage = 3;
        public const int ActionIdStarMessage = 4;
        public const int ActionIdPinMessage = 5;
        public const int ActionIdStarFile = 6;
        public const int ActionIdUnstarMessage = 7;
        public const int ActionIdUnpinMessage = 8;
        public const int ActionIdUnstarFile = 9;

        #endregion

        #endregion

        public SlackConnector()
        {
            Name = "Slack";
            Group = ConnectorGroup.Chat;
            AuthenticationType = ServiceConnectorAuthenticationType.UrlParameter;
            BaseUrl = "https://slack.com/api";

            AddRateLimit(ServiceConnectorRateLimiting.LimitAppliedTo.Account, 1, ServiceConnectorRateLimiting.LimitPeriod.Second);
        }

        public override void Initialise()
        {
            base.Initialise();
            AddEntities();
            foreach (var entity in Entities) entity.DefaultMaxRecords = 100;
            AddExternalActions();
        }

        public override string GetEndpoint(ConnectorQuery query)
        {
            switch (query.Entity.Id)
            {
                case EntityIdChannel:
                case EntityIdMessage:
                    return "channels.list";
                case EntityIdUser:
                    return "users.list";
                case EntityIdFile:
                    return "files.list";
                case EntityIdGroup:
                case EntityIdGroupMember:
                    return "groups.list";
            }
            return string.Empty;
        }

        public override List<Tuple<string, string>> GetParameters(ConnectorQuery query)
        {
            return TupleHelper.CreateTupleStringList("token", Token);
        }

        public override IEnumerable<object> Deserialize(string data, ConnectorQuery query)
        {
            if (query.Entity.Id == EntityIdUser)
            {
                var userCollection = DeserializeJson<SlackUserCollection>(data);
                return userCollection.members;
            }
            var userData = GetResponse("users.list");
            var users = DeserializeJson<SlackUserCollection>(userData);
            switch (query.Entity.Id)
            {
                case EntityIdChannel:
                    var channelCollection = DeserializeJson<SlackChannelCollection>(data);
                    foreach (var channel in channelCollection.channels) channel.creator_user = users.find_user(channel.creator);
                    return channelCollection.channels;
                case EntityIdFile:
                    var fileCollection = DeserializeJson<SlackFileCollection>(data);
                    if (fileCollection.paging.pages == 1) return fileCollection.files;
                    var files = fileCollection.files.ToList();
                    for (var page = 2; page <= fileCollection.paging.pages; page++)
                    {
                        query.Page = page;
                        var pageData = GetResponse(query);
                        fileCollection = DeserializeJson<SlackFileCollection>(pageData);
                        files.AddRange(fileCollection.files);
                    }
                    foreach (var file in files) file.user_object = users.find_user(file.user);
                    return files;
                case EntityIdGroup:
                case EntityIdGroupMember:
                    var groupCollection = DeserializeJson<SlackGroupCollection>(data);
                    if (query.Entity.Id == EntityIdGroup) return groupCollection.groups;
                    var groupMembers = new List<SlackGroupMember>();
                    foreach (var group in groupCollection.groups)
                    {
                        foreach (var user in group.members.Select(member => users.find_user(member)).Where(user => user != null))
                        {
                            groupMembers.Add(new SlackGroupMember(group, user));
                        }
                    }
                    return groupMembers;
            }
            return null;
        }

        public override void RunAction(ConnectorAction action, List<Tuple<string, string>> parameters)
        {
            throw new NotImplementedException();
        }

        #region Helpers

        private void AddEntities()
        {
            var channelEntity = AddEntity(EntityIdChannel, "Channels", typeof(SlackChannel));
            var userEntity = AddEntity(EntityIdUser, "Users", typeof(SlackUser));
            var fileEntity = AddEntity(EntityIdFile, "Files", typeof(SlackFile));
            var groupEntity = AddEntity(EntityIdGroup, "Groups", typeof(SlackGroup));
            var groupMemberEntity = AddEntity(EntityIdGroupMember, "Group members", typeof(SlackGroupMember));
            var messageEntity = AddEntity(EntityIdMessage, "Messages", typeof(SlackMessage));

            // add relationships
            groupEntity.AddRelatedEntity("Members", groupMemberEntity, "id", "group_id");
            userEntity.AddRelatedEntity("Messages", messageEntity, "id", "user");
            userEntity.AddRelatedEntity("Files", fileEntity, "id", "user");
            channelEntity.AddRelatedEntity("Messages", messageEntity, "id", "channel_id");

            // add actions
            AddChannelActions(channelEntity);
            AddMessageActions(messageEntity);
            AddFileActions(fileEntity);

            // add favorites
            AddFileFavorites(fileEntity);
        }

        private void AddExternalActions()
        {
            var externalMessageAction = AddAction(ActionIdAddExternalMessage, "Post as a Slack message", FieldTypeIdUrl);
            var channelParameter = externalMessageAction.AddParameter("Channel", FieldTypeIdString);
            channelParameter.Required = true;
            externalMessageAction.AddParameter("Message", FieldTypeIdString);
        }
        private static void AddChannelActions(ConnectorEntity entity)
        {
            var addMessageAction = entity.AddAction(ActionIdAddMessage, "Post message");
            addMessageAction.AddParameter("id", "id");
            addMessageAction.AddParameter("Message", FieldTypeIdString);
        }
        private static void AddMessageActions(ConnectorEntity entity)
        {
            var removeMessageAction = entity.AddAction(ActionIdRemoveMessage, "Remove");
            removeMessageAction.AllowMultipleSelection = true;
            removeMessageAction.ConfirmationPrompt = "Are you sure you want to remove this message?";
            removeMessageAction.MultipleSelectionConfirmationPrompt = "Are you sure you want to remove these messages?";

            var pinMessageAction = entity.AddAction(ActionIdPinMessage, "Pin");
            pinMessageAction.AllowMultipleSelection = true;

            var starMessageAction = entity.AddAction(ActionIdPinMessage, "Star");
            starMessageAction.AllowMultipleSelection = true;
        }
        private static void AddFileActions(ConnectorEntity entity)
        {
            
        }
        private static void AddFileFavorites(ConnectorEntity entity)
        {
            var starredFileFavorite = entity.AddFavorite("Starred", true);
            starredFileFavorite.Query.AddRestriction("is_starred", ConnectorRestriction.ConnectorRestrictionType.Equals, "true");
        }

        #endregion

    }
}
