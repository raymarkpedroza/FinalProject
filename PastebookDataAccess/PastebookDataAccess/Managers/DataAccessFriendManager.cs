using PastebookEF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PastebookDataAccess.Managers
{
    public class DataAccessFriendManager
    {
        public int AddFriend(PASTEBOOK_FRIEND friend)
        {
            int result = 0;

            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    context.PASTEBOOK_FRIEND.Add(friend);
                    result = context.SaveChanges();
                }
            }

            catch 
            {
            }
            return result;
        }

        public List<PASTEBOOK_FRIEND> RetrieveFriends(int id)
        {
            List<PASTEBOOK_FRIEND> listOfFriends = new List<PASTEBOOK_FRIEND>();

            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    var userFriends = context.PASTEBOOK_FRIEND.Where(x => x.FRIEND_ID == id);
                    var friendUsers = context.PASTEBOOK_FRIEND.Where(x=>x.USER_ID == id);

                    foreach (var friend in userFriends)
                    {
                        listOfFriends.Add(friend);
                    }

                    foreach (var friend in friendUsers)
                    {
                        listOfFriends.Add(friend);
                    }
                }
            }

            catch
            {
            }

            return listOfFriends;
        }

        public int AcceptFriendRequest(int friendId, string request)
        {
            int result = 0;

            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    var friend = context.PASTEBOOK_FRIEND.Where(x => x.ID == friendId).SingleOrDefault();
                    friend.REQUEST = request;
                    context.Entry(friend).State = EntityState.Modified;
                    result = context.SaveChanges();
                }
            }
            catch
            {
            }

            return result;
        }

        public PASTEBOOK_FRIEND RetrieveFriend(int friendRequestId)
        {
            PASTEBOOK_FRIEND friendRequest = new PASTEBOOK_FRIEND();
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    friendRequest = context.PASTEBOOK_FRIEND.Where(x => x.ID == friendRequestId).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {

            }

            return friendRequest;
        }

        public int RejectFriendRequest(int friendId)
        {
            int result = 0;

            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    var friend = context.PASTEBOOK_FRIEND.Where(x => x.ID == friendId).SingleOrDefault();
                    context.PASTEBOOK_FRIEND.Remove(friend);
                    result = context.SaveChanges();
                }
            }
            catch
            {
            }

            return result;
        }
    }
}