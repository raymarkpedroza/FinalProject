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

            registerRequest.wcfUserEntity.Salt = encryptResponse.Salt;
            registerRequest.wcfUserEntity.Password = encryptResponse.HashPassword;

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
    }
}