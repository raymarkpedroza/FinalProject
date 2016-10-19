using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using PastebookWebService.Requests;
using PastebookWebService.Responses;
using PastebookWebService.Managers;
using PastebookWebService.Entities;

namespace PastebookWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class PastebookService : IPastebookService
    {
        UserManager userManager = new UserManager();
        PasswordManager passwordManager = new PasswordManager();
        PostManager postManager = new PostManager();
        ReactionManager reactionManager = new ReactionManager();
        FriendManager friendManager = new FriendManager();
        NotificationManager notificationManager = new NotificationManager();
        CountryManager countryManager = new CountryManager();

        public RegisterUserResponse RegisterUser(RegisterUserRequest request)
        {
            RegisterUserResponse response = new RegisterUserResponse();
            response.Result = userManager.RegisterUser(request.wcfUserEntity);

            return response;
        }

        public PasswordMatchResponse PasswordMatch(PasswordMatchRequest request)
        {
            PasswordMatchResponse response = new PasswordMatchResponse();
            response.Result = passwordManager.IsPasswordMatch(request.Password, request.Salt, request.HashPassword);

            return response;
        }

        public RetrieveUserByEmailResponse RetrieveUserByEmail(RetrieverUserByEmailRequest request)
        {
            RetrieveUserByEmailResponse response = new RetrieveUserByEmailResponse();
            response.User = userManager.RetrieveUserByEmail(request.EmailAddress);

            return response;
        }

        public RetrievePostsResponse RetrieveNewsfeed(RetrievePostsRequest request)
        {
            RetrievePostsResponse response = new RetrievePostsResponse();
            response.ListOfPosts = postManager.RetrieveNewsfeed(request.UserId, request.ListOfFriendsId);

            return response;
        }

        public NotificationEntity Notify()
        {
            throw new NotImplementedException();
        }

        public RetrieveAllUserResponse RetrieveAllUser()
        {
            RetrieveAllUserResponse response = new RetrieveAllUserResponse();
            response.ListOfUser = userManager.RetrieveAllUsers();

            return response;
        }

        public RetrieveAllCountriesResponse RetrieveAllCountries()
        {
            RetrieveAllCountriesResponse response = new RetrieveAllCountriesResponse();

            response.ListOfCountries = countryManager.RetrieveAllCountry();

            return response;
        }

        public EncryptPasswordResponse EncryptPassword(EncryptPasswordRequest request)
        {
            EncryptPasswordResponse response = new EncryptPasswordResponse();
            string salt = string.Empty;

            response.HashPassword = passwordManager.GeneratePasswordHash(request.Password, out salt);
            response.Salt = salt;

            return response;
        }

        public CreatePostResponse CreatePost(CreatePostRequest request)
        {
            CreatePostResponse response = new CreatePostResponse();
            response.Result = postManager.CreatePost(request.Post);

            return response;
        }

        public RetrieveUserByIdResponse RetrieveUserById(RetrieveUserByIdRequest request)
        {
            RetrieveUserByIdResponse response = new RetrieveUserByIdResponse();
            response.User = userManager.RetrieveUserById(request.Id);

            return response;
        }

        public RetrievePostsResponse RetrievePosts(RetrievePostsRequest request)
        {
            RetrievePostsResponse response = new RetrievePostsResponse();
            response.ListOfPosts = postManager.RetrieveNewsfeed(request.UserId, request.ListOfFriendsId);

            return response;
        }

        public AddCommentResponse AddComment(AddCommentRequest request)
        {
            AddCommentResponse response = new AddCommentResponse();
            response.Result = reactionManager.AddComment(request.Comment);

            return response;
        }

        public AddNotificationResponse AddNotification(AddNotificationRequest request)
        {
            AddNotificationResponse response = new AddNotificationResponse();
            response.Result = notificationManager.AddNotification(request.Notification);

            return response;
        }

        public AddLikeResponse AddLike(AddLikeRequest request)
        {
            AddLikeResponse response = new AddLikeResponse();
            response.Result = reactionManager.AddLike(request.Like);

            return response;
        }

        public RetrieveCommentResponse RetrieveComment(RetrieveCommentRequest request)
        {
            RetrieveCommentResponse response = new RetrieveCommentResponse();
            response.ListOfComments = reactionManager.RetrieveComment(request.PostId);

            return response;
        }

        public RetrieveLikeResponse RetrieveLike(RetrieveLikeRequest request)
        {
            RetrieveLikeResponse response = new RetrieveLikeResponse();
            response.ListOfLikes = reactionManager.RetrieveLike(request.PostId);

            return response;
        }

        public AddFriendResponse AddFriend(AddFriendRequest request)
        {
            AddFriendResponse response = new AddFriendResponse();
            response.Result = friendManager.AddFriend(request.Friend);

            return response;
        }

        public AcceptFriendRequestResponse AcceptFriendRequest(AcceptFriendRequestRequest request)
        {
            AcceptFriendRequestResponse response = new AcceptFriendRequestResponse();
            response.Result = friendManager.AcceptFriendRequest(request.FriendId, request.Request);

            return response;
        }

        public RejectFriendRequestResponse RejectFriendRequest(RejectFriendRequestRequest request)
        {
            RejectFriendRequestResponse response = new RejectFriendRequestResponse();
            response.Result = friendManager.RejectFriendRequest(request.FriendId);

            return response;
        }

        public RetrieveCountryByIdResponse RetrieveCountryById(RetrieveCountryByIdRequest request)
        {
            RetrieveCountryByIdResponse response = new RetrieveCountryByIdResponse();
            response.Country = countryManager.RetrieveCountry(request.CountryId);

            return response;
        }

        public RetrieveFriendsResponse RetrieveFriends(RetrieveFriendsRequest request)
        {
            RetrieveFriendsResponse response = new RetrieveFriendsResponse();
            response.listOfFriends = friendManager.RetrieveFriends(request.UserId);

            return response;
        }
    }
}
