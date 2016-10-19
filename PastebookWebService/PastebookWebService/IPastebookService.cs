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
        RetrieveUserByEmailResponse RetrieveUserByEmail(RetrieverUserByEmailRequest request);

        [OperationContract]
        RetrieveUserByIdResponse RetrieveUserById(RetrieveUserByIdRequest request);

        [OperationContract]
        RetrievePostsResponse RetrieveNewsfeed(RetrievePostsRequest request);

        [OperationContract]
        RetrieveAllUserResponse RetrieveAllUser();

        [OperationContract]
        RetrieveAllCountriesResponse RetrieveAllCountries();

        [OperationContract]
        EncryptPasswordResponse EncryptPassword(EncryptPasswordRequest request);

        [OperationContract]
        CreatePostResponse CreatePost(CreatePostRequest request);

        [OperationContract]
        RetrievePostsResponse RetrievePosts(RetrievePostsRequest request);

        [OperationContract]
        AddCommentResponse AddComment(AddCommentRequest request);

        [OperationContract]
        AddNotificationResponse AddNotification(AddNotificationRequest request);

        [OperationContract]
        AddLikeResponse AddLike(AddLikeRequest request);

        [OperationContract]
        RetrieveCommentResponse RetrieveComment(RetrieveCommentRequest request);

        [OperationContract]
        RetrieveLikeResponse RetrieveLike(RetrieveLikeRequest request);

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
