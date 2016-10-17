using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Pastebook.Models;
using Pastebook.PastebookServiceReference;

namespace Pastebook.Mappers
{
    public static class Mapper
    {
        public static UserEntity MapMVCUserModelToWCFUserEntity(UserModel mvcUserModel)
        {
            UserEntity wcfUserEntity = new UserEntity();

            //wcfUserEntity.Id = mvcUserModel.Id;
            wcfUserEntity.Username = mvcUserModel.Username;
            wcfUserEntity.Password = mvcUserModel.Password;
            wcfUserEntity.EmailAddress = mvcUserModel.EmailAddress;
            wcfUserEntity.Firstname = mvcUserModel.Firstname;
            wcfUserEntity.Lastname = mvcUserModel.Lastname;
            wcfUserEntity.Birthday = mvcUserModel.Birthday;
            wcfUserEntity.CountryId = mvcUserModel.CountryId;
            wcfUserEntity.MobileNumber = mvcUserModel.MobileNumber;
            wcfUserEntity.Gender = mvcUserModel.Gender;
            wcfUserEntity.ProfilePicture = mvcUserModel.ProfilePicture;
            wcfUserEntity.DateCreated = mvcUserModel.DateCreated;
            wcfUserEntity.AboutMe = mvcUserModel.AboutMe;

            return wcfUserEntity;
        }

        public static UserModel MapWCFUserEntityToMVCUserModel(UserEntity wcfUserEntity)
        {
            UserModel mvcUserModel = new UserModel();

            //mvcUserModel.Id = wcfUserEntity.Id;
            mvcUserModel.Username = wcfUserEntity.Username;
            mvcUserModel.Password = wcfUserEntity.Password;
            mvcUserModel.EmailAddress = wcfUserEntity.EmailAddress;
            mvcUserModel.Firstname = wcfUserEntity.Firstname;
            mvcUserModel.Lastname = wcfUserEntity.Lastname;
            mvcUserModel.Birthday = wcfUserEntity.Birthday;
            mvcUserModel.CountryId = wcfUserEntity.CountryId;
            mvcUserModel.MobileNumber = wcfUserEntity.MobileNumber;
            mvcUserModel.Gender = wcfUserEntity.Gender;
            mvcUserModel.ProfilePicture = wcfUserEntity.ProfilePicture;
            mvcUserModel.DateCreated = wcfUserEntity.DateCreated;
            mvcUserModel.AboutMe = wcfUserEntity.AboutMe;

            return mvcUserModel;
        }

        public static CommentEntity MapMVCCommentModelToWCFCommentEntity(CommentModel mvcCommentModel)
        {
            CommentEntity wcfCommentEntity = new CommentEntity();

            wcfCommentEntity.Id = mvcCommentModel.Id;
            wcfCommentEntity.PostId = mvcCommentModel.PostId;
            wcfCommentEntity.PosterId = mvcCommentModel.PosterId;
            wcfCommentEntity.Content = mvcCommentModel.Content;
            wcfCommentEntity.DateCreated = mvcCommentModel.DateCreated;

            return wcfCommentEntity;
        }

        public static CommentModel MapWCFCommentEntityToMVCCommentModel(CommentEntity wcfCommentEntity)
        {
            CommentModel mvcCommentModel = new CommentModel();

            mvcCommentModel.Id = wcfCommentEntity.Id;
            mvcCommentModel.PostId = wcfCommentEntity.PostId;
            mvcCommentModel.PosterId = wcfCommentEntity.PosterId;
            mvcCommentModel.Content = wcfCommentEntity.Content;
            mvcCommentModel.DateCreated = wcfCommentEntity.DateCreated;

            return mvcCommentModel;
        }

        public static FriendEntity MapMVCFriendModelToWCFFriendEntity(FriendModel mvcFriendModel)
        {
            FriendEntity wcfFriendEntity = new FriendEntity();

            wcfFriendEntity.Id = mvcFriendModel.Id;
            wcfFriendEntity.UserId = mvcFriendModel.UserId;
            wcfFriendEntity.FriendId = mvcFriendModel.FriendId;
            wcfFriendEntity.Request = mvcFriendModel.Request;
            wcfFriendEntity.IsBlocked = mvcFriendModel.IsBlocked;
            wcfFriendEntity.CreatedDate = mvcFriendModel.CreatedDate;

            return wcfFriendEntity;
        }

        public static FriendModel MapWCFFriendEntityToMVCFriendModel(FriendEntity wcfFriendEntity)
        {
            FriendModel mvcFriendModel = new FriendModel();

            mvcFriendModel.Id = wcfFriendEntity.Id;
            mvcFriendModel.UserId = wcfFriendEntity.UserId;
            mvcFriendModel.FriendId = wcfFriendEntity.FriendId;
            mvcFriendModel.Request = Convert.ToChar(wcfFriendEntity.Request);
            mvcFriendModel.IsBlocked = Convert.ToChar(wcfFriendEntity.IsBlocked);
            mvcFriendModel.CreatedDate = wcfFriendEntity.CreatedDate;

            return mvcFriendModel;
        }

        public static LikeEntity MapMVCLikeModelToWCFLikeEntity(LikeModel mvcLikeModel)
        {
            LikeEntity wcfLikeEntity = new LikeEntity();

            wcfLikeEntity.Id = mvcLikeModel.Id;
            wcfLikeEntity.PostId = mvcLikeModel.PostId;
            wcfLikeEntity.LikedBy = mvcLikeModel.LikedBy;

            return wcfLikeEntity;
        }

        public static LikeModel MapWCFLikeEntityToMVCLikeModel(LikeEntity wcfLikeEntity)
        {
            LikeModel mvcLikeModel = new LikeModel();

            mvcLikeModel.Id = wcfLikeEntity.Id;
            mvcLikeModel.PostId = wcfLikeEntity.PostId;
            mvcLikeModel.LikedBy = wcfLikeEntity.LikedBy;

            return mvcLikeModel;
        }

        public static NotificationEntity MapMVCNotificationModelToWCFNotificationEntity(NotificationModel mvcNotificationModel)
        {
            NotificationEntity wcfNotificationEntity = new NotificationEntity();

            wcfNotificationEntity.Id = mvcNotificationModel.Id;
            wcfNotificationEntity.NotificationType = mvcNotificationModel.NotificationType;
            wcfNotificationEntity.ReceiverId = mvcNotificationModel.ReceiverId;
            wcfNotificationEntity.SenderId = mvcNotificationModel.SenderId;
            wcfNotificationEntity.CreatedDate = mvcNotificationModel.CreatedDate;
            wcfNotificationEntity.CommentId = mvcNotificationModel.CommentId;
            wcfNotificationEntity.PostId = mvcNotificationModel.PostId;
            wcfNotificationEntity.Seen = mvcNotificationModel.Seen;

            return wcfNotificationEntity;
        }

        public static NotificationModel MapWCFNotificationEntityToMVCNotificationModel(NotificationEntity wcfNotificationEntity)
        {
            NotificationModel mvcNotificationModel = new NotificationModel();

            mvcNotificationModel.Id = wcfNotificationEntity.Id;
            mvcNotificationModel.NotificationType = wcfNotificationEntity.NotificationType;
            mvcNotificationModel.ReceiverId = wcfNotificationEntity.ReceiverId;
            mvcNotificationModel.SenderId = wcfNotificationEntity.SenderId;
            mvcNotificationModel.CreatedDate = wcfNotificationEntity.CreatedDate;
            mvcNotificationModel.CommentId = wcfNotificationEntity.CommentId;
            mvcNotificationModel.PostId = wcfNotificationEntity.PostId;
            mvcNotificationModel.Seen = wcfNotificationEntity.Seen;

            return mvcNotificationModel;
        }

        public static PostEntity MapMVCPostModelToWCFPostEntity(PostModel mvcPostModel)
        {
            PostEntity wcfPostEntity = new PostEntity();

            wcfPostEntity.Id = mvcPostModel.Id;
            wcfPostEntity.CreatedDate = mvcPostModel.CreatedDate;
            wcfPostEntity.Content = mvcPostModel.Content;
            wcfPostEntity.ProfileOwnerId = mvcPostModel.ProfileOwnerId;
            wcfPostEntity.PosterId = mvcPostModel.PosterId;

            return wcfPostEntity;
        }

        public static PostModel MapWCFPostEntityToMVCPostModel(PostEntity wcfPostEntity)
        {
            PostModel mvcPostModel = new PostModel();

            mvcPostModel.Id = wcfPostEntity.Id;
            mvcPostModel.CreatedDate = wcfPostEntity.CreatedDate;
            mvcPostModel.Content = wcfPostEntity.Content;
            mvcPostModel.ProfileOwnerId = wcfPostEntity.ProfileOwnerId;
            mvcPostModel.PosterId = wcfPostEntity.PosterId;

            return mvcPostModel;
        }

        public static CountryEntity MapMVCCountryModelToWCFCountryEntity(CountryModel mvcCountryModel)
        {
            CountryEntity wcfCountryEntity = new CountryEntity();

            wcfCountryEntity.Id = mvcCountryModel.Id;
            wcfCountryEntity.Country = mvcCountryModel.Country;

            return wcfCountryEntity;
        }

        public static CountryModel MapWCFCountryEntityToMVCCountryModel(CountryEntity wcfCountryEntity)
        {
            CountryModel mvcCountryModel = new CountryModel();

            mvcCountryModel.Id = wcfCountryEntity.Id;
            mvcCountryModel.Country = wcfCountryEntity.Country;

            return mvcCountryModel;
        }

    }
}