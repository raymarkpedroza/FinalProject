using PastebookEF;
using PastebookWebService.Entities;
using PastebookWebService.Mappers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PastebookWebService.Managers
{
    public class FriendManager
    {
        public int AddFriend(FriendEntity friend)
        {
            int result = 0;

            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    context.PASTEBOOK_FRIEND.Add(Mapper.MapWCFFriendEntityToDBFriendTable(friend));
                    result = context.SaveChanges();
                }
            }

            catch 
            {
            }
            return result;
        }

        public List<FriendEntity> RetrieveFriends(int id)
        {
            List<FriendEntity> listOfFriends = new List<FriendEntity>();

            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    var userFriends = context.PASTEBOOK_FRIEND.Where(x => x.FRIEND_ID == id);
                    var friendUsers = context.PASTEBOOK_FRIEND.Where(x=>x.USER_ID == id);

                    foreach (var friend in userFriends)
                    {
                        listOfFriends.Add(Mapper.MapDBFriendTableToWCFFriendEntity(friend));
                    }

                    foreach (var friend in friendUsers)
                    {
                        listOfFriends.Add(Mapper.MapDBFriendTableToWCFFriendEntity(friend));
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