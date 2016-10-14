using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Pastebook.Models;
using PastebookEF;

namespace Pastebook.Mappers
{
    public static class Mapper
    {
        public static PASTEBOOK_USER MapMVCUserToDBUSer(UserModel mvcUser)
        {
            PASTEBOOK_USER tbUser = new PASTEBOOK_USER();

            tbUser.ID = mvcUser.Id;
            tbUser.USER_NAME = mvcUser.Username;
            tbUser.PASSWORD = mvcUser.Password;
            tbUser.SALT = mvcUser.Salt;
            tbUser.EMAIL_ADDRESS = mvcUser.EmailAddress;
            tbUser.FIRST_NAME = mvcUser.Firstname;
            tbUser.LAST_NAME = mvcUser.Lastname;
            tbUser.BIRTHDAY = mvcUser.Birthday;
            tbUser.COUNTRY_ID = mvcUser.CountryId;
            tbUser.MOBILE_NO = mvcUser.MobileNumber;
            tbUser.GENDER = mvcUser.Gender;
            tbUser.PROFILE_PIC = mvcUser.ProfilePicture;
            tbUser.DATE_CREATED = mvcUser.DateCreated;
            tbUser.ABOUT_ME = mvcUser.AboutMe;

            return tbUser;
        }

        public static UserModel MapDBUserToMVCUSer(PASTEBOOK_USER tbUser)
        {
            UserModel mvcUser = new UserModel();

            mvcUser.Id = tbUser.ID;
            mvcUser.Username = tbUser.USER_NAME;
            mvcUser.Password = tbUser.PASSWORD;
            mvcUser.Salt = tbUser.SALT;
            mvcUser.EmailAddress = tbUser.EMAIL_ADDRESS;
            mvcUser.Firstname = tbUser.FIRST_NAME;
            mvcUser.Lastname = tbUser.LAST_NAME;
            mvcUser.Birthday = tbUser.BIRTHDAY;
            mvcUser.CountryId = (int)tbUser.COUNTRY_ID;
            mvcUser.MobileNumber = tbUser.MOBILE_NO;
            mvcUser.Gender = tbUser.GENDER;
            mvcUser.ProfilePicture = tbUser.PROFILE_PIC;
            mvcUser.DateCreated = tbUser.DATE_CREATED;
            mvcUser.AboutMe = tbUser.ABOUT_ME;

            return mvcUser;
        }

        public static PASTEBOOK_COMMENT MapMVCCommentToDBComment(CommentModel mvcComment)
        {
            PASTEBOOK_COMMENT tbComment = new PASTEBOOK_COMMENT();

            tbComment.ID = mvcComment.Id;
            tbComment.POST_ID = mvcComment.PostId;
            tbComment.POSTER_ID = mvcComment.PosterId;
            tbComment.CONTENT = mvcComment.Content;
            tbComment.DATE_CREATED = mvcComment.DateCreated;

            return tbComment;
        }

        public static CommentModel MapDBCommentToMVCComment(PASTEBOOK_COMMENT tbComment)
        {
            CommentModel mvcComment = new CommentModel();

            mvcComment.Id = tbComment.ID;
            mvcComment.PostId = tbComment.POST_ID;
            mvcComment.PosterId = tbComment.POSTER_ID;
            mvcComment.Content = tbComment.CONTENT;
            mvcComment.DateCreated = tbComment.DATE_CREATED;

            return mvcComment;
        }

        public static PASTEBOOK_FRIEND MapMVCFriendToDBFriend(FriendModel mvcFriend)
        {
            PASTEBOOK_FRIEND tbFriend = new PASTEBOOK_FRIEND();

            tbFriend.ID = mvcFriend.Id;
            tbFriend.USER_ID = mvcFriend.UserId;
            tbFriend.FRIEND_ID = mvcFriend.FriendId;
            tbFriend.REQUEST = mvcFriend.Request.ToString();
            tbFriend.IsBLOCKED = mvcFriend.IsBlocked.ToString();
            tbFriend.CREATED_DATE = mvcFriend.CreatedDate;

            return tbFriend;
        }

        public static FriendModel MapMVCFriendToDBFriend(PASTEBOOK_FRIEND tbFriend)
        {
            FriendModel mvcFriend = new FriendModel();

            mvcFriend.Id = tbFriend.ID;
            mvcFriend.UserId = tbFriend.USER_ID;
            mvcFriend.FriendId = tbFriend.FRIEND_ID;
            mvcFriend.Request = Convert.ToChar(tbFriend.REQUEST);
            mvcFriend.IsBlocked = Convert.ToChar(tbFriend.IsBLOCKED);
            mvcFriend.CreatedDate = tbFriend.CREATED_DATE;

            return mvcFriend;
        }

        public static PASTEBOOK_LIKE MapMVCLikeToDBLike(LikeModel mvcLike)
        {
            PASTEBOOK_LIKE tbLike = new PASTEBOOK_LIKE();

            tbLike.ID = mvcLike.Id;
            tbLike.POST_ID = mvcLike.PostId;
            tbLike.LIKED_BY = mvcLike.LikedBy;

            return tbLike;
        }

        public static LikeModel MapDBLikeToMVCLike(PASTEBOOK_LIKE tbLike)
        {
            LikeModel mvcLike = new LikeModel();

            mvcLike.Id = tbLike.ID;
            mvcLike.PostId = tbLike.POST_ID;
            mvcLike.LikedBy = tbLike.LIKED_BY;

            return mvcLike;
        }

        public static PASTEBOOK_NOTIFICATION MapMVCNotificationToDBNotification(NotificationModel mvcNotification)
        {
            PASTEBOOK_NOTIFICATION tbNotification = new PASTEBOOK_NOTIFICATION();

            tbNotification.ID = mvcNotification.Id;
            tbNotification.NOTIF_TYPE = mvcNotification.NotificationType;
            tbNotification.RECEIVER_ID = mvcNotification.ReceiverId;
            tbNotification.SENDER_ID = mvcNotification.SenderId;
            tbNotification.CREATED_DATE = mvcNotification.CreatedDate;
            tbNotification.COMMENT_ID = mvcNotification.CommentId;
            tbNotification.POST_ID = mvcNotification.PostId;
            tbNotification.SEEN = mvcNotification.Seen.ToString();

            return tbNotification;
        }

        public static NotificationModel MapDBNotificationToMVCNotification(PASTEBOOK_NOTIFICATION tbNotification)
        {
            NotificationModel mvcNotification = new NotificationModel();

            mvcNotification.Id = tbNotification.ID;
            mvcNotification.NotificationType = tbNotification.NOTIF_TYPE;
            mvcNotification.ReceiverId = tbNotification.RECEIVER_ID;
            mvcNotification.SenderId = tbNotification.SENDER_ID;
            mvcNotification.CreatedDate = tbNotification.CREATED_DATE;
            mvcNotification.CommentId = (int)tbNotification.COMMENT_ID;
            mvcNotification.PostId = (int)tbNotification.POST_ID;
            mvcNotification.Seen = Convert.ToChar(tbNotification.SEEN);

            return mvcNotification;
        }

        public static PASTEBOOK_POST MapMVCPostToDBPost(PostModel mvcPost)
        {
            PASTEBOOK_POST tbPost = new PASTEBOOK_POST();

            tbPost.ID = mvcPost.Id;
            tbPost.CREATED_DATE = mvcPost.CreatedDate;
            tbPost.CONTENT = mvcPost.Content;
            tbPost.PROFILE_OWNER_ID = mvcPost.ProfileOwnerId;
            tbPost.POSTER_ID = mvcPost.PosterId;

            return tbPost;
        }

        public static PostModel MapDBPostToMVCPost(PASTEBOOK_POST tbPost)
        {
            PostModel mvcPost = new PostModel();

            mvcPost.Id = tbPost.ID;
            mvcPost.CreatedDate = tbPost.CREATED_DATE;
            mvcPost.Content = tbPost.CONTENT;
            mvcPost.ProfileOwnerId = tbPost.PROFILE_OWNER_ID;
            mvcPost.PosterId = tbPost.POSTER_ID;

            return mvcPost;
        }

        public static REF_COUNTRY MapMVCCountryTODBCountry(CountryModel mvcCountry)
        {
            REF_COUNTRY tbCountry = new REF_COUNTRY();

            tbCountry.ID = mvcCountry.Id;
            tbCountry.COUNTRY = mvcCountry.Country;

            return tbCountry;
        }

        public static CountryModel MapMVCCountryTODBCountry(REF_COUNTRY tbCountry)
        {
            CountryModel mvcCountry = new CountryModel();

            mvcCountry.Id = tbCountry.ID;
            mvcCountry.Country = tbCountry.COUNTRY;

            return mvcCountry;
        }

    }
}