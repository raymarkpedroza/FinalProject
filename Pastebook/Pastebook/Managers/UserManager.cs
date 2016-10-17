using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Pastebook.PastebookServiceReference;
using Pastebook.Models;
using Pastebook.Mappers;

namespace Pastebook.Managers
{
    public class UserManager
    {
        PastebookServiceClient pastebookServiceClient = new PastebookServiceClient();

        public int RegisterUser(UserModel mvcUserModel)
        {
            EncryptPasswordRequest encryptRequest = new EncryptPasswordRequest();
            encryptRequest.Password = mvcUserModel.Password;

            EncryptPasswordResponse encryptResponse = new EncryptPasswordResponse();
            encryptResponse = pastebookServiceClient.EncryptPassword(encryptRequest);

            RegisterUserRequest registerRequest = new RegisterUserRequest();
            registerRequest.wcfUserEntity = Mapper.MapMVCUserModelToWCFUserEntity(mvcUserModel);

            registerRequest.wcfUserEntity.Password = encryptResponse.HashPassword;
            registerRequest.wcfUserEntity.Salt = encryptResponse.Salt;

            RegisterUserResponse registerResponse = new RegisterUserResponse();
            registerResponse = pastebookServiceClient.RegisterUser(registerRequest);

            return registerResponse.Result;
        }

        public List<UserModel> RetrieveAllUser()
        {
            List<UserModel> listOfUsers = new List<UserModel>();
            RetrieveAllUserResponse response = new RetrieveAllUserResponse();

            response = pastebookServiceClient.RetrieveAllUser();

            foreach (var user in response.ListOfUser)
            {
                listOfUsers.Add(Mapper.MapWCFUserEntityToMVCUserModel(user));
            }

            return listOfUsers;
        }

        public bool LoginUser(string email, string password)
        {
            bool result = false;

            RetrieverUserRequest retrieveUserRequest = new RetrieverUserRequest();
            retrieveUserRequest.EmailAddress = email;

            RetrieveUserResponse retrieveUserResponse = new RetrieveUserResponse();
            retrieveUserResponse = pastebookServiceClient.RetrieveUser(retrieveUserRequest);

            UserModel userModel = new UserModel();

            userModel = Mapper.MapWCFUserEntityToMVCUserModel(retrieveUserResponse.User);

            if (userModel.Username != null)
            {
                EncryptPasswordRequest encryptRequest = new EncryptPasswordRequest();
                encryptRequest.Password = password;

                EncryptPasswordResponse encryptResponse = new EncryptPasswordResponse();
                encryptResponse = pastebookServiceClient.EncryptPassword(encryptRequest);

                PasswordMatchRequest passwordMatchRequest = new PasswordMatchRequest();
                passwordMatchRequest.Password = password;
                passwordMatchRequest.Salt = retrieveUserResponse.User.Salt;
                passwordMatchRequest.HashPassword = retrieveUserResponse.User.Password;

                PasswordMatchResponse passwordMatchResponse = new PasswordMatchResponse();
                passwordMatchResponse = pastebookServiceClient.PasswordMatch(passwordMatchRequest);

                result = passwordMatchResponse.Result;
            }

            return result;
        }
    }
}