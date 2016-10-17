﻿using PastebookWebService.Requests;
using PastebookWebService.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace PastebookWebService
{
    [ServiceContract]
    public interface IPastebookService
    {
        [OperationContract]
        EncryptPasswordResponse EncryptPassword(EncryptPasswordRequest request);

        [OperationContract]
        RegisterUserResponse RegisterUser(RegisterUserRequest request);

        [OperationContract]
        PasswordMatchResponse PasswordMatch(PasswordMatchRequest request);

        [OperationContract]
        RetrieveUserResponse RetrieveUser(RetrieverUserRequest request);

        [OperationContract]
        RetrievePostsResponse RetrieveNewsfeed(RetrievePostsRequest request);
    }
}
