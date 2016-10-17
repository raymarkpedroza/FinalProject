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

namespace PastebookWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class PastebookService : IPastebookService
    {
        UserManager userManager = new UserManager();
        PasswordManager passwordManager = new PasswordManager();
        PostManager postManager = new PostManager();

        public EncryptPasswordResponse EncryptPassword(EncryptPasswordRequest request)
        {

            string salt = string.Empty;
            EncryptPasswordResponse response = new EncryptPasswordResponse();
            response.HashPassword = passwordManager.GeneratePasswordHash(request.Password, out salt);
            response.Salt = salt;

            return response;
        }

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

        public RetrieveUserResponse RetrieveUser(RetrieverUserRequest request)
        {
            RetrieveUserResponse response = new RetrieveUserResponse();
            response.User = userManager.RetrieveUser(request.EmailAddress);

            return response;
        }

        public RetrievePostsResponse RetrieveNewsfeed(RetrievePostsRequest request)
        {
            RetrievePostsResponse response = new RetrievePostsResponse();
            response.ListOfPosts = postManager.RetrieveNewsfeed(request.UserId, request.ListOfFriendsId);

            return response;
        }
    }
}
