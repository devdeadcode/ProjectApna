using System;
using System.Collections.Generic;
using System.Linq;
using eOne.Common.Actions;
using eOne.Common.Connectors.Instagram.Models;
using eOne.Common.Connectors.Service;
using eOne.Common.Helpers;
using eOne.Common.Query;

namespace eOne.Common.Connectors.Instagram
{
    public class InstagramConnector : RestConnector
    {

        #region Constants

        #region Entities

        private const int EntityIdPosts = 1;
        private const int EntityIdComments = 2;
        private const int EntityIdFollowers = 3;
        private const int EntityIdRequests = 4;
        private const int EntityIdTags = 5;

        #endregion

        #region Actions

        private const int ActionIdApproveRequest = 1;
        private const int ActionIdDenyRequest = 2;
        private const int ActionIdBlock = 3;
        private const int ActionIdDeleteComment = 4;
        private const int ActionIdDeleteAndBlock = 5;

        #endregion

        #endregion

        public InstagramConnector()
        {
            Name = "Instagram";
            Group = ConnectorGroup.SocialMedia;
            BaseUrl = "https://api.instagram.com/v1";

            AuthenticationType = ServiceConnectorAuthenticationType.UrlParameter;
            Key = "d0ef35cac71649afa18a5e38e34b2ec0";
            Secret = "aac6775a6a604df98c342cc506f8c119";
            AuthorizationUri = "https://api.instagram.com/oauth/authorize";
            AccessTokenUri = "https://api.instagram.com/oauth/access_token";
            Scope = "comments";

            // rate limited to 5000 requests per hour
            AddRateLimit(ServiceConnectorRateLimiting.LimitAppliedTo.Account, 5000, ServiceConnectorRateLimiting.LimitPeriod.Hour);

            AddSetup();
        }

        #region Methods

        public override void Initialise()
        {
            base.Initialise();

            var postEntity = AddEntity(EntityIdPosts, "Posts", typeof(InstagramPost));
            var commentEntity = AddEntity(EntityIdComments, "Comments", typeof(InstagramCommentList));
            var followerEntity = AddEntity(EntityIdFollowers, "Followers", typeof(InstagramUser));
            var requestEntity = AddEntity(EntityIdRequests, "Requests", typeof(InstagramUser));

            if (Tags != null && Tags.Count > 0)
            {
                var tagEntity = AddEntity(EntityIdTags, "Followed tags", typeof(InstagramTaggedPost));
                foreach (var tag in Tags)
                {
                    var tagFavorite = tagEntity.AddFavorite(tag);
                    tagFavorite.Query.AddFields("posted_by_username", "caption_text", "type", "post_date", "post_time", "number_of_comments", "number_of_likes");
                    tagFavorite.Query.AddRestriction("tag", ConnectorRestriction.ConnectorRestrictionType.Equals, tag);
                }
            }

            foreach (var entity in Entities) entity.DefaultMaxRecords = 100;

            // add relationships
            postEntity.AddRelatedEntity("Comments", commentEntity, "id", "post_id");

            // add actions
            requestEntity.AddAction(ActionIdApproveRequest, "Approve");
            requestEntity.AddAction(ActionIdDenyRequest, "Deny");
            followerEntity.AddAction(ActionIdBlock, "Block user");
            commentEntity.AddAction(ActionIdDeleteComment, "Delete comment");
            commentEntity.AddAction(ActionIdDeleteAndBlock, "Delete comment and block user");

            // add favorites
            var mostLikedPostsFavorite = postEntity.AddFavorite("Most liked", true);
            mostLikedPostsFavorite.Query.MaxRecords = 10;
            mostLikedPostsFavorite.Query.AddOrderBy("number_of_likes", ConnectorQuery.ConnectorQuerySortOrder.Descending);
            mostLikedPostsFavorite.Query.AddRestriction("number_of_likes", ConnectorRestriction.ConnectorRestrictionType.GreaterThan, "0");

            var mostCommentedPostsFavorite = postEntity.AddFavorite("Most commented", true);
            mostCommentedPostsFavorite.Query.MaxRecords = 10;
            mostCommentedPostsFavorite.Query.AddOrderBy("number_of_comments", ConnectorQuery.ConnectorQuerySortOrder.Descending);
            mostCommentedPostsFavorite.Query.AddRestriction("number_of_comments", ConnectorRestriction.ConnectorRestrictionType.GreaterThan, "0");

            // todo - need to add new date query types to handle this
            //var newCommentsOnOldPostsFavorite = commentEntity.AddFavorite("New comments on old posts");
        }

        public override void RunAction(ConnectorAction action, List<Tuple<string, string>> parameters)
        {
            var endpoint = GetActionEndpoint(action.Id, parameters);
            var method = GetActionMethod(action.Id);
            switch (method)
            {
                case RestConnectorMethod.Post:
                    RunPostAction(endpoint, null, GetActionParameters(action.Id));
                    return;
                case RestConnectorMethod.Delete:
                    RunDeleteAction(endpoint);
                    return;
            }
        }

        public override List<Tuple<string, string>> GetParameters(ConnectorQuery query)
        {
            var parameters = TupleHelper.CreateTupleStringList("access_token", Token, "count", query.Entity.DefaultMaxRecords.ToString());
            if (!string.IsNullOrWhiteSpace(query.Cursor)) TupleHelper.AppendTupleStringList(parameters, "cursor", query.Cursor); 
            return parameters;
        }

        public override string GetEndpoint(ConnectorQuery query)
        {
            switch (query.Entity.Id)
            {
                case EntityIdPosts:
                case EntityIdComments:
                    return "users/self/media/recent";
                case EntityIdFollowers:
                    return "users/self/followed-by";
                case EntityIdRequests:
                    return "users/self/requested-by";
                case EntityIdTags:
                    var tagRestriction = query.FindRestrictionByFieldAndType("tag", ConnectorRestriction.ConnectorRestrictionType.Equals);
                    return tagRestriction != null ? $"tags/{tagRestriction.Values[0].Constant}/media/recent" : $"tags/{Tags[0]}/media/recent";
            }
            return string.Empty;
        }

        public override IEnumerable<object> Deserialize(string data, ConnectorQuery query)
        {
            switch (query.Entity.Id)
            {
                case EntityIdPosts:
                case EntityIdComments:
                    var posts = new List<InstagramPost>();
                    var postCollection = DeserializeJson<InstagramPostCollection>(data);
                    posts.AddRange(postCollection.data);
                    while (postCollection.pagination?.next_cursor != null && (query.Restrictions.Count != 0 || posts.Count <= query.MaxRecords))
                    {
                        query.Cursor = postCollection.pagination.next_cursor;
                        var pageData = GetResponse(query);
                        postCollection = DeserializeJson<InstagramPostCollection>(pageData);
                        posts.AddRange(postCollection.data);
                    }
                    switch (query.Entity.Id)
                    {
                        case EntityIdComments:
                            var comments = new List<InstagramCommentList>();
                            foreach (var post in posts) comments.AddRange(post.comments.data.Select(comment => new InstagramCommentList(comment, post)));
                            return comments;
                    }
                    return posts;
                case EntityIdTags:
                    var tagPosts = DeserializeJson<InstagramTaggedPostCollection>(data);
                    var taggedPosts = tagPosts.data;
                    var tagRestriction = query.FindRestrictionByFieldAndType("tag", ConnectorRestriction.ConnectorRestrictionType.Equals);
                    if (tagRestriction == null)
                    {
                        foreach (var post in taggedPosts) post.tag = Tags[0];
                        foreach (var tag in Tags.Skip(1))
                        {
                            var tagData = GetResponse($"tags/{tag}/media/recent");
                            tagPosts = DeserializeJson<InstagramTaggedPostCollection>(tagData);
                            foreach (var post in tagPosts.data) post.tag = tag;
                            taggedPosts.AddRange(tagPosts.data);
                        }
                    }
                    else
                    {
                        foreach (var post in taggedPosts) post.tag = tagRestriction.Values[0].Constant;
                    }
                    return taggedPosts;
                case EntityIdFollowers:
                case EntityIdRequests:
                    var users = new List<InstagramUser>();
                    var userCollection = DeserializeJson<InstagramUserCollection>(data);
                    users.AddRange(userCollection.data);
                    while (userCollection.pagination?.next_cursor != null && (query.Restrictions.Count != 0 || users.Count <= query.MaxRecords || users.Count == 0))
                    {
                        query.Cursor = userCollection.pagination.next_cursor;
                        var pageData = GetResponse(query);
                        userCollection = DeserializeJson<InstagramUserCollection>(pageData);
                        users.AddRange(userCollection.data);
                    }
                    return users;
            }
            return null;
        }

        #endregion

        #region Helpers

        private string GetActionEndpoint(int actionId, List<Tuple<string, string>> parameters)
        {
            switch (actionId)
            {
                case ActionIdApproveRequest:
                case ActionIdDenyRequest:
                case ActionIdBlock:
                    var userId = FindParameterValue(parameters, "id");
                    return $"users/{userId}/relationship";
                case ActionIdDeleteComment:
                    var mediaId = FindParameterValue(parameters, "post_id");
                    var commentId = FindParameterValue(parameters, "comment_id");
                    return $"media/{mediaId}/comments/{commentId}";
            }
            return string.Empty;
        }

        private RestConnectorMethod GetActionMethod(int actionId)
        {
            switch (actionId)
            {
                case ActionIdApproveRequest:
                case ActionIdDenyRequest:
                case ActionIdBlock:
                    return RestConnectorMethod.Post;
                case ActionIdDeleteComment:
                    return RestConnectorMethod.Delete;
            }
            return RestConnectorMethod.Post;
        }

        private List<Tuple<string, string>> GetActionParameters(int actionId)
        {
            switch (actionId)
            {
                case ActionIdApproveRequest:
                    return TupleHelper.CreateTupleStringList("action", "approve");
                case ActionIdDenyRequest:
                    return TupleHelper.CreateTupleStringList("action", "ignore");
                case ActionIdBlock:
                    return TupleHelper.CreateTupleStringList("action", "block");
            }
            return null;
        }

        #endregion

    }
}
