using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PastebookEF;
using PastebookWebService.Entities;

namespace PastebookWebService.Mappers
{
    public static class Mapper
    {
        public static PASTEBOOK_USER MapWCFUserEntityToDBUserTable(UserEntity wcfUserEntity)
        {
            PASTEBOOK_USER dbUserTable = new PASTEBOOK_USER();

            //dbUserTable.ID = wcfUserEntity.Id;
            dbUserTable.USER_NAME = wcfUserEntity.Username;
            dbUserTable.PASSWORD = wcfUserEntity.Password;
            dbUserTable.SALT = wcfUserEntity.Salt;
            dbUserTable.EMAIL_ADDRESS = wcfUserEntity.EmailAddress;
            dbUserTable.FIRST_NAME = wcfUserEntity.Firstname;
            dbUserTable.LAST_NAME = wcfUserEntity.Lastname;
            dbUserTable.BIRTHDAY = wcfUserEntity.Birthday;
            dbUserTable.COUNTRY_ID = wcfUserEntity.CountryId;
            dbUserTable.MOBILE_NO = wcfUserEntity.MobileNumber;
            dbUserTable.GENDER = wcfUserEntity.Gender;
            dbUserTable.PROFILE_PIC = wcfUserEntity.ProfilePicture;
            dbUserTable.DATE_CREATED = wcfUserEntity.DateCreated;
            dbUserTable.ABOUT_ME = wcfUserEntity.AboutMe;

            return dbUserTable;
        }

        public static UserEntity MapDBUserTableToWCFUserEntity(PASTEBOOK_USER dbUserTable)
        {
            UserEntity wcfUserEntity = new UserEntity();

            //wcfUserEntity.Id = dbUserTable.ID;
            wcfUserEntity.Username = dbUserTable.USER_NAME;
            wcfUserEntity.Password = dbUserTable.PASSWORD;
            wcfUserEntity.Salt = dbUserTable.SALT;
            wcfUserEntity.EmailAddress = dbUserTable.EMAIL_ADDRESS;
            wcfUserEntity.Firstname = dbUserTable.FIRST_NAME;
            wcfUserEntity.Lastname = dbUserTable.LAST_NAME;
            wcfUserEntity.Birthday = dbUserTable.BIRTHDAY;
            wcfUserEntity.CountryId = (int)dbUserTable.COUNTRY_ID;
            wcfUserEntity.MobileNumber = dbUserTable.MOBILE_NO;
            wcfUserEntity.Gender = dbUserTable.GENDER;
            wcfUserEntity.ProfilePicture = dbUserTable.PROFILE_PIC;
            wcfUserEntity.DateCreated = dbUserTable.DATE_CREATED;
            wcfUserEntity.AboutMe = dbUserTable.ABOUT_ME;

            return wcfUserEntity;
        }

        public static PASTEBOOK_COMMENT MapWCFCommentEntityToDBCommentTable(CommentEntity wcfCommentEntity)
        {
            PASTEBOOK_COMMENT dbCommentTable = new PASTEBOOK_COMMENT();

            dbCommentTable.ID = wcfCommentEntity.Id;
            dbCommentTable.POST_ID = wcfCommentEntity.PostId;
            dbCommentTable.POSTER_ID = wcfCommentEntity.PosterId;
            dbCommentTable.CONTENT = wcfCommentEntity.Content;
            dbCommentTable.DATE_CREATED = wcfCommentEntity.DateCreated;

            return dbCommentTable;
        }

        public static CommentEntity MapDBCommentTableToWCFCommentEntity(PASTEBOOK_COMMENT dbCommentTable)
        {
            CommentEntity wcfCommentEntity = new CommentEntity();

            wcfCommentEntity.Id = dbCommentTable.ID;
            wcfCommentEntity.PostId = dbCommentTable.POST_ID;
            wcfCommentEntity.PosterId = dbCommentTable.POSTER_ID;
            wcfCommentEntity.Content = dbCommentTable.CONTENT;
            wcfCommentEntity.DateCreated = dbCommentTable.DATE_CREATED;

            return wcfCommentEntity;
        }

        public static PASTEBOOK_FRIEND MapWCFFriendEntityToDBFriendTable(FriendEntity wcfFriendEntity)
        {
            PASTEBOOK_FRIEND dbFriendTable = new PASTEBOOK_FRIEND();

            dbFriendTable.ID = wcfFriendEntity.Id;
            dbFriendTable.USER_ID = wcfFriendEntity.UserId;
            dbFriendTable.FRIEND_ID = wcfFriendEntity.FriendId;
            dbFriendTable.REQUEST = wcfFriendEntity.Request.ToString();
            dbFriendTable.IsBLOCKED = wcfFriendEntity.IsBlocked.ToString();
            dbFriendTable.CREATED_DATE = wcfFriendEntity.CreatedDate;

            return dbFriendTable;
        }

        public static FriendEntity MapDBFriendTableToWCFFriendEntity(PASTEBOOK_FRIEND dbFriendTable)
        {
            FriendEntity wcfFriendEntity = new FriendEntity();

            wcfFriendEntity.Id = dbFriendTable.ID;
            wcfFriendEntity.UserId = dbFriendTable.USER_ID;
            wcfFriendEntity.FriendId = dbFriendTable.FRIEND_ID;
            wcfFriendEntity.Request = Convert.ToChar(dbFriendTable.REQUEST);
            wcfFriendEntity.IsBlocked = Convert.ToChar(dbFriendTable.IsBLOCKED);
            wcfFriendEntity.CreatedDate = dbFriendTable.CREATED_DATE;

            return wcfFriendEntity;
        }

        public static PASTEBOOK_LIKE MapWCFLikeEntityToDBLikeTable(LikeEntity wcfLikeEntity)
        {
            PASTEBOOK_LIKE dbLikeTable = new PASTEBOOK_LIKE();

            dbLikeTable.ID = wcfLikeEntity.Id;
            dbLikeTable.POST_ID = wcfLikeEntity.PostId;
            dbLikeTable.LIKED_BY = wcfLikeEntity.LikedBy;

            return dbLikeTable;
        }

        public static LikeEntity MapDBLikeTableToWCFLikeEntity(PASTEBOOK_LIKE dbLikeTable)
        {
            LikeEntity wcfLikeEntity = new LikeEntity();

            wcfLikeEntity.Id = dbLikeTable.ID;
            wcfLikeEntity.PostId = dbLikeTable.POST_ID;
            wcfLikeEntity.LikedBy = dbLikeTable.LIKED_BY;

            return wcfLikeEntity;
        }

        public static PASTEBOOK_NOTIFICATION MapWCFNotificationEntityToDBNotificationTable(NotificationEntity wcfNotificationEntity)
        {
            PASTEBOOK_NOTIFICATION dbNotificationTable = new PASTEBOOK_NOTIFICATION();

            dbNotificationTable.ID = wcfNotificationEntity.Id;
            dbNotificationTable.NOTIF_TYPE = wcfNotificationEntity.NotificationType;
            dbNotificationTable.RECEIVER_ID = wcfNotificationEntity.ReceiverId;
            dbNotificationTable.SENDER_ID = wcfNotificationEntity.SenderId;
            dbNotificationTable.CREATED_DATE = wcfNotificationEntity.CreatedDate;
            dbNotificationTable.COMMENT_ID = wcfNotificationEntity.CommentId;
            dbNotificationTable.POST_ID = wcfNotificationEntity.PostId;
            dbNotificationTable.SEEN = wcfNotificationEntity.Seen.ToString();

            return dbNotificationTable;
        }

        public static NotificationEntity MapDBNotificationTableToWCFNotificationEntity(PASTEBOOK_NOTIFICATION dbNotificationTable)
        {
            NotificationEntity wcfNotificationEntity = new NotificationEntity();

            wcfNotificationEntity.Id = dbNotificationTable.ID;
            wcfNotificationEntity.NotificationType = dbNotificationTable.NOTIF_TYPE;
            wcfNotificationEntity.ReceiverId = dbNotificationTable.RECEIVER_ID;
            wcfNotificationEntity.SenderId = dbNotificationTable.SENDER_ID;
            wcfNotificationEntity.CreatedDate = dbNotificationTable.CREATED_DATE;
            wcfNotificationEntity.CommentId = (int)dbNotificationTable.COMMENT_ID;
            wcfNotificationEntity.PostId = (int)dbNotificationTable.POST_ID;
            wcfNotificationEntity.Seen = Convert.ToChar(dbNotificationTable.SEEN);

            return wcfNotificationEntity;
        }

        public static PASTEBOOK_POST MapWCFPostEntityToDBPostTable(PostEntity wcfPostEntity)
        {
            PASTEBOOK_POST dbPostTable = new PASTEBOOK_POST();

            dbPostTable.ID = wcfPostEntity.Id;
            dbPostTable.CREATED_DATE = wcfPostEntity.CreatedDate;
            dbPostTable.CONTENT = wcfPostEntity.Content;
            dbPostTable.PROFILE_OWNER_ID = wcfPostEntity.ProfileOwnerId;
            dbPostTable.POSTER_ID = wcfPostEntity.PosterId;

            return dbPostTable;
        }

        public static PostEntity MapDBPostTableToWCFPostEntity(PASTEBOOK_POST dbPostTable)
        {
            PostEntity wcfPostEntity = new PostEntity();

            wcfPostEntity.Id = dbPostTable.ID;
            wcfPostEntity.CreatedDate = dbPostTable.CREATED_DATE;
            wcfPostEntity.Content = dbPostTable.CONTENT;
            wcfPostEntity.ProfileOwnerId = dbPostTable.PROFILE_OWNER_ID;
            wcfPostEntity.PosterId = dbPostTable.POSTER_ID;

            return wcfPostEntity;
        }

        public static REF_COUNTRY MapWCFCountryEntityToDBCountryTable(CountryEntity wcfCountryEntity)
        {
            REF_COUNTRY dbCountryTable = new REF_COUNTRY();

            dbCountryTable.ID = wcfCountryEntity.Id;
            dbCountryTable.COUNTRY = wcfCountryEntity.Country;

            return dbCountryTable;
        }

        public static CountryEntity MapDBCountryTableToWCFCountryEntity(REF_COUNTRY dbCountryTable)
        {
            CountryEntity wcfCountryEntity = new CountryEntity();

            wcfCountryEntity.Id = dbCountryTable.ID;
            wcfCountryEntity.Country = dbCountryTable.COUNTRY;

            return wcfCountryEntity;
        }

    }
}
