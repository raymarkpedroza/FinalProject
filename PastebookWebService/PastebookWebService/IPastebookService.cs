using PastebookWebService.Entities;
using PastebookWebService.Requests;
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
        RegisterUserResponse RegisterUser(RegisterUserRequest request);

        [OperationContract]
        PasswordMatchResponse PasswordMatch(PasswordMatchRequest request);

        [OperationContract]
        RetrieveUserResponse RetrieveUser(RetrieverUserRequest request);

        [OperationContract]
        RetrievePostsResponse RetrieveNewsfeed(RetrievePostsRequest request);

        [OperationContract]
        RetrieveAllUserResponse RetrieveAllUser();

        [OperationContract]
        CommentEntity Comment();

        [OperationContract]
        CountryEntity Country();

        [OperationContract]
        FriendEntity Friend();

        [OperationContract]
        LikeEntity Like();

        [OperationContract]
        NotificationEntity Notify();
    }
}
