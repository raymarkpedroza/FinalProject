using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PastebookEF;
using PastebookDataAccess.Managers;
using System.Data.Entity;

namespace PastebookDataAccess.Managers
{
    public class DataAccessUserManager
    {
        DataAccessPasswordManager passwordManager = new DataAccessPasswordManager();

        public int RegisterUser(PASTEBOOK_USER user)
        {
            int result = 0;

            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    context.PASTEBOOK_USER.Add(user);
                    result = context.SaveChanges();
                }
            }

            catch (Exception ex)
            {
            }

            return result;
        }

        public PASTEBOOK_USER RetrieveUserByEmail(string emailaddress)
        {
            PASTEBOOK_USER user = new PASTEBOOK_USER();

            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    var result = context.PASTEBOOK_USER.Where(x => x.EMAIL_ADDRESS == emailaddress).SingleOrDefault();
                    user = result;
                }
            }
            catch (Exception)
            {
            }

            return user;
        }

        public PASTEBOOK_USER RetrieveUserById(int id)
        {
            PASTEBOOK_USER user = new PASTEBOOK_USER();

            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    var result = context.PASTEBOOK_USER.Include(c=>c.REF_COUNTRY).Where(x => x.ID == id).SingleOrDefault();
                    user = result;
                }
            }
            catch (Exception)
            {
            }

            return user;
        }

        public PASTEBOOK_USER RetrieveUserByUsername(string username)
        {
            PASTEBOOK_USER user = new PASTEBOOK_USER();

            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    var result = context.PASTEBOOK_USER.Include(c => c.REF_COUNTRY).Where(x => x.USER_NAME == username).SingleOrDefault();
                    user = result;
                }
            }
            catch (Exception)
            {
            }

            return user;
        }

        public List<PASTEBOOK_USER> RetrieveAllUsers()
        {
            List<PASTEBOOK_USER> listOfUsers = new List<PASTEBOOK_USER>();
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    foreach (var user in context.PASTEBOOK_USER)
                    {
                        listOfUsers.Add(user);
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return listOfUsers;
        }

        public int UpdateProfile(PASTEBOOK_USER user)
        {
            int result = 0;
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    context.Entry(user).State = EntityState.Modified;
                    context.Entry(user.REF_COUNTRY).State = EntityState.Modified;
                    result = context.SaveChanges();
                }
            }
            catch 
            {
            }

            return result;
        }

        public List<PASTEBOOK_USER> SearchUser(string name)
        {
            List<PASTEBOOK_USER> listOfUsers = new List<PASTEBOOK_USER>();

            try
            {
                using (var context =new PASTEBOOKEntities())
                {
                    var searchFirstname = context.PASTEBOOK_USER.Where(x => x.FIRST_NAME.ToLower() == name.ToLower());
                    var searchLastname = context.PASTEBOOK_USER.Where(x => x.LAST_NAME.ToLower() == name.ToLower());

                    listOfUsers.AddRange(searchFirstname);
                    listOfUsers.AddRange(searchLastname);
                }
            }
            catch 
            {
            
                
            }

            return listOfUsers;
        }
    }
}
