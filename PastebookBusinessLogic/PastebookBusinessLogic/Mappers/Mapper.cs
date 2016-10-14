using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PastebookEF;
using PastebookBusinessLogic.Entities;

namespace PastebookBusinessLogic.Mappers
{
    public static class Mapper
    {
        public static PASTEBOOK_USER MapBLUserToDBUSer(UserEntity blUser)
        {
            PASTEBOOK_USER tbUser = new PASTEBOOK_USER();

            tbUser.ID = blUser.Id;
            tbUser.USER_NAME = blUser.Username;
            tbUser.PASSWORD = blUser.Password;
            tbUser.SALT = blUser.Salt;
            tbUser.EMAIL_ADDRESS = blUser.EmailAddress;
            tbUser.FIRST_NAME = blUser.Firstname;
            tbUser.LAST_NAME = blUser.Lastname;
            tbUser.BIRTHDAY = blUser.Birthday;
            tbUser.COUNTRY_ID = blUser.CountryId;
            tbUser.MOBILE_NO = blUser.MobileNumber;
            tbUser.GENDER = blUser.Gender;
            tbUser.PROFILE_PIC = blUser.ProfilePicture;
            tbUser.DATE_CREATED = blUser.DateCreated;
            tbUser.ABOUT_ME = blUser.AboutMe;

            return tbUser;
        }

        public static UserEntity MapDBUserToBLUSer(PASTEBOOK_USER tbUser)
        {
            UserEntity blUser = new UserEntity();

            blUser.Id = tbUser.ID;
            blUser.Username = tbUser.USER_NAME;
            blUser.Password = tbUser.PASSWORD;
            blUser.Salt = tbUser.SALT;
            blUser.EmailAddress = tbUser.EMAIL_ADDRESS;
            blUser.Firstname = tbUser.FIRST_NAME;
            blUser.Lastname = tbUser.LAST_NAME;
            blUser.Birthday = tbUser.BIRTHDAY;
            blUser.CountryId = (int)tbUser.COUNTRY_ID;
            blUser.MobileNumber = tbUser.MOBILE_NO;
            blUser.Gender = tbUser.GENDER;
            blUser.ProfilePicture = tbUser.PROFILE_PIC;
            blUser.DateCreated = tbUser.DATE_CREATED;
            blUser.AboutMe = tbUser.ABOUT_ME;

            return blUser;
        }

        public static PASTEBOOK_COMMENT MapBLCommentToDBComment(CommentEntity blComment)
        {
            PASTEBOOK_COMMENT tbComment = new PASTEBOOK_COMMENT();

            tbComment.ID = blComment.Id;
            tbComment.POST_ID = blComment.PostId;
            tbComment.POSTER_ID = blComment.PosterId;
            tbComment.CONTENT = blComment.Content;
            tbComment.DATE_CREATED = blComment.DateCreated;

            return tbComment;
        }

        public static CommentEntity MapDBCommentToBLComment(PASTEBOOK_COMMENT tbComment)
        {
            CommentEntity blComment = new CommentEntity();

            blComment.Id = tbComment.ID;
            blComment.PostId = tbComment.POST_ID;
            blComment.PosterId = tbComment.POSTER_ID;
            blComment.Content = tbComment.CONTENT;
            blComment.DateCreated = tbComment.DATE_CREATED;

            return blComment;
        }

        public static PASTEBOOK_FRIEND MapBLFriendToDBFriend(FriendEntity blFriend)
        {
            PASTEBOOK_FRIEND tbFriend = new PASTEBOOK_FRIEND();

            tbFriend.ID = blFriend.Id;
            tbFriend.USER_ID = blFriend.UserId;
            tbFriend.FRIEND_ID = blFriend.FriendId;
            tbFriend.REQUEST = blFriend.Request.ToString();
            tbFriend.IsBLOCKED = blFriend.IsBlocked.ToString();
            tbFriend.CREATED_DATE = blFriend.CreatedDate;

            return tbFriend;
        }

        public static FriendEntity MapDBFriendToBLFriend(PASTEBOOK_FRIEND tbFriend)
        {
            FriendEntity blFriend = new FriendEntity();

            blFriend.Id = tbFriend.ID;
            blFriend.UserId = tbFriend.USER_ID;
            blFriend.FriendId = tbFriend.FRIEND_ID;
            blFriend.Request = Convert.ToChar(tbFriend.REQUEST);
            blFriend.IsBlocked = Convert.ToChar(tbFriend.IsBLOCKED);
            blFriend.CreatedDate = tbFriend.CREATED_DATE;

            return blFriend;
        }

        public static PASTEBOOK_LIKE MapBLLikeToDBLike(LikeEntity blLike)
        {
            PASTEBOOK_LIKE tbLike = new PASTEBOOK_LIKE();

            tbLike.ID = blLike.Id;
            tbLike.POST_ID = blLike.PostId;
            tbLike.LIKED_BY = blLike.LikedBy;

            return tbLike;
        }

        public static LikeEntity MapDBLikeToBLLike(PASTEBOOK_LIKE tbLike)
        {
            LikeEntity blEntity = new LikeEntity();

            blEntity.Id = tbLike.ID;
            blEntity.PostId = tbLike.POST_ID;
            blEntity.LikedBy = tbLike.LIKED_BY;

            return blEntity;
        }

        public static PASTEBOOK_NOTIFICATION MapBLNotificationToDBNotification(NotificationEntity blNotification)
        {
            PASTEBOOK_NOTIFICATION tbNotification = new PASTEBOOK_NOTIFICATION();

            tbNotification.ID = blNotification.Id;
            tbNotification.NOTIF_TYPE = blNotification.NotificationType;
            tbNotification.RECEIVER_ID = blNotification.ReceiverId;
            tbNotification.SENDER_ID = blNotification.SenderId;
            tbNotification.CREATED_DATE = blNotification.CreatedDate;
            tbNotification.COMMENT_ID = blNotification.CommentId;
            tbNotification.POST_ID = blNotification.PostId;
            tbNotification.SEEN = blNotification.Seen.ToString();

            return tbNotification;
        }

        public static NotificationEntity MapDBNotificationToBLNotification(PASTEBOOK_NOTIFICATION tbNotification)
        {
            NotificationEntity blNotification = new NotificationEntity();

            blNotification.Id = tbNotification.ID;
            blNotification.NotificationType = tbNotification.NOTIF_TYPE;
            blNotification.ReceiverId = tbNotification.RECEIVER_ID;
            blNotification.SenderId = tbNotification.SENDER_ID;
            blNotification.CreatedDate = tbNotification.CREATED_DATE;
            blNotification.CommentId = (int)tbNotification.COMMENT_ID;
            blNotification.PostId = (int)tbNotification.POST_ID;
            blNotification.Seen = Convert.ToChar(tbNotification.SEEN);

            return blNotification;
        }

        public static PASTEBOOK_POST MapBLPostToDBPost(PostEntity blPost)
        {
            PASTEBOOK_POST tbPost = new PASTEBOOK_POST();

            tbPost.ID = blPost.Id;
            tbPost.CREATED_DATE = blPost.CreatedDate;
            tbPost.CONTENT = blPost.Content;
            tbPost.PROFILE_OWNER_ID = blPost.ProfileOwnerId;
            tbPost.POSTER_ID = blPost.PosterId;

            return tbPost;
        }

        public static PostEntity MapDBPostToBLPost(PASTEBOOK_POST tbPost)
        {
            PostEntity blPost = new PostEntity();

            blPost.Id = tbPost.ID;
            blPost.CreatedDate = tbPost.CREATED_DATE;
            blPost.Content = tbPost.CONTENT;
            blPost.ProfileOwnerId = tbPost.PROFILE_OWNER_ID;
            blPost.PosterId = tbPost.POSTER_ID;

            return blPost;
        }

        public static REF_COUNTRY MapBLCountryToDBCountry(CountryEntity blCountry)
        {
            REF_COUNTRY tbCountry = new REF_COUNTRY();

            tbCountry.ID = blCountry.Id;
            tbCountry.COUNTRY = blCountry.Country;

            return tbCountry;
        }

        public static CountryEntity MapDBCountryToBLCountry(REF_COUNTRY tbCountry)
        {
            CountryEntity blCountry = new CountryEntity();

            blCountry.Id = tbCountry.ID;
            blCountry.Country = tbCountry.COUNTRY;

            return blCountry;
        }

    }
}
