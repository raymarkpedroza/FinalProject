using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Pastebook.Models;
using PastebookBusinessLogic.Entities;

namespace Pastebook.Mappers
{
    public static class Mapper
    {
        public static UserEntity MapMVCUserToBLUSer(UserModel mvcUser)
        {
            UserEntity blUser = new UserEntity();

            blUser.Id = mvcUser.Id;
            blUser.Username = mvcUser.Username;
            blUser.Password = mvcUser.Password;
            blUser.Salt = mvcUser.Salt;
            blUser.EmailAddress = mvcUser.EmailAddress;
            blUser.Firstname = mvcUser.Firstname;
            blUser.Lastname = mvcUser.Lastname;
            blUser.Birthday = mvcUser.Birthday;
            blUser.CountryId = mvcUser.CountryId;
            blUser.MobileNumber = mvcUser.MobileNumber;
            blUser.Gender = mvcUser.Gender;
            blUser.ProfilePicture = mvcUser.ProfilePicture;
            blUser.DateCreated = mvcUser.DateCreated;
            blUser.AboutMe = mvcUser.AboutMe;

            return blUser;
        }

        public static UserModel MapBLUserToMVCUSer(UserEntity blUser)
        {
            UserModel mvcUser = new UserModel();

            mvcUser.Id = blUser.Id;
            mvcUser.Username = blUser.Username;
            mvcUser.Password = blUser.Password;
            mvcUser.Salt = blUser.Salt;
            mvcUser.EmailAddress = blUser.EmailAddress;
            mvcUser.Firstname = blUser.Firstname;
            mvcUser.Lastname = blUser.Lastname;
            mvcUser.Birthday = blUser.Birthday;
            mvcUser.CountryId = blUser.CountryId;
            mvcUser.MobileNumber = blUser.MobileNumber;
            mvcUser.Gender = blUser.Gender;
            mvcUser.ProfilePicture = blUser.ProfilePicture;
            mvcUser.DateCreated = blUser.DateCreated;
            mvcUser.AboutMe = blUser.AboutMe;

            return mvcUser;
        }

        public static CommentEntity MapMVCCommentToBLComment(CommentModel mvcComment)
        {
            CommentEntity blComment = new CommentEntity();

            blComment.Id = mvcComment.Id;
            blComment.PostId = mvcComment.PostId;
            blComment.PosterId = mvcComment.PosterId;
            blComment.Content = mvcComment.Content;
            blComment.DateCreated = mvcComment.DateCreated;

            return blComment;
        }

        public static CommentModel MapBLCommentToMVCComment(CommentEntity blComment)
        {
            CommentModel mvcComment = new CommentModel();

            mvcComment.Id = blComment.Id;
            mvcComment.PostId = blComment.PostId;
            mvcComment.PosterId = blComment.PosterId;
            mvcComment.Content = blComment.Content;
            mvcComment.DateCreated = blComment.DateCreated;

            return mvcComment;
        }

        public static FriendEntity MapMVCFriendToBLFriend(FriendModel mvcFriend)
        {
            FriendEntity blFriend = new FriendEntity();

            blFriend.Id = mvcFriend.Id;
            blFriend.UserId = mvcFriend.UserId;
            blFriend.FriendId = mvcFriend.FriendId;
            blFriend.Request = mvcFriend.Request;
            blFriend.IsBlocked = mvcFriend.IsBlocked;
            blFriend.CreatedDate = mvcFriend.CreatedDate;

            return blFriend;
        }

        public static FriendModel MapBLFriendToMVCFriend(FriendEntity blFriend)
        {
            FriendModel mvcFriend = new FriendModel();

            mvcFriend.Id = blFriend.Id;
            mvcFriend.UserId = blFriend.UserId;
            mvcFriend.FriendId = blFriend.FriendId;
            mvcFriend.Request = Convert.ToChar(blFriend.Request);
            mvcFriend.IsBlocked = Convert.ToChar(blFriend.IsBlocked);
            mvcFriend.CreatedDate = blFriend.CreatedDate;

            return mvcFriend;
        }

        public static LikeEntity MapMVCLikeToBLLike(LikeModel mvcLike)
        {
            LikeEntity tbLike = new LikeEntity();

            tbLike.Id = mvcLike.Id;
            tbLike.PostId = mvcLike.PostId;
            tbLike.LikedBy = mvcLike.LikedBy;

            return tbLike;
        }

        public static LikeModel MapBLLikeToMVCLike(LikeEntity blLike)
        {
            LikeModel mvcLike = new LikeModel();

            mvcLike.Id = blLike.Id;
            mvcLike.PostId = blLike.PostId;
            mvcLike.LikedBy = blLike.LikedBy;

            return mvcLike;
        }

        public static NotificationEntity MapMVCNotificationToBLNotification(NotificationModel mvcNotification)
        {
            NotificationEntity blNotification = new NotificationEntity();

            blNotification.Id = mvcNotification.Id;
            blNotification.NotificationType = mvcNotification.NotificationType;
            blNotification.ReceiverId = mvcNotification.ReceiverId;
            blNotification.SenderId = mvcNotification.SenderId;
            blNotification.CreatedDate = mvcNotification.CreatedDate;
            blNotification.CommentId = mvcNotification.CommentId;
            blNotification.PostId = mvcNotification.PostId;
            blNotification.Seen = mvcNotification.Seen;

            return blNotification;
        }

        public static NotificationModel MapBLNotificationToMVCNotification(NotificationEntity blNotification)
        {
            NotificationModel mvcNotification = new NotificationModel();

            mvcNotification.Id = blNotification.Id;
            mvcNotification.NotificationType = blNotification.NotificationType;
            mvcNotification.ReceiverId = blNotification.ReceiverId;
            mvcNotification.SenderId = blNotification.SenderId;
            mvcNotification.CreatedDate = blNotification.CreatedDate;
            mvcNotification.CommentId = blNotification.CommentId;
            mvcNotification.PostId = blNotification.PostId;
            mvcNotification.Seen = blNotification.Seen;

            return mvcNotification;
        }

        public static PostEntity MapMVCPostToBLPost(PostModel mvcPost)
        {
            PostEntity blPost = new PostEntity();

            blPost.Id = mvcPost.Id;
            blPost.CreatedDate = mvcPost.CreatedDate;
            blPost.Content = mvcPost.Content;
            blPost.ProfileOwnerId = mvcPost.ProfileOwnerId;
            blPost.PosterId = mvcPost.PosterId;

            return blPost;
        }

        public static PostModel MapBLPostToMVCPost(PostEntity blPost)
        {
            PostModel mvcPost = new PostModel();

            mvcPost.Id = blPost.Id;
            mvcPost.CreatedDate = blPost.CreatedDate;
            mvcPost.Content = blPost.Content;
            mvcPost.ProfileOwnerId = blPost.ProfileOwnerId;
            mvcPost.PosterId = blPost.PosterId;

            return mvcPost;
        }

        public static CountryEntity MapMVCCountryTOBLCountry(CountryModel mvcCountry)
        {
            CountryEntity blCountry = new CountryEntity();

            blCountry.Id = mvcCountry.Id;
            blCountry.Country = mvcCountry.Country;

            return blCountry;
        }

        public static CountryModel MapBLCountryTOMVCCountry(CountryEntity blCountry)
        {
            CountryModel mvcCountry = new CountryModel();

            mvcCountry.Id = blCountry.Id;
            mvcCountry.Country = blCountry.Country;

            return mvcCountry;
        }

    }
}