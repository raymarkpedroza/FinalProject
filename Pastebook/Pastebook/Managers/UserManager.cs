using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PastebookDataAccess.Managers;
using PastebookEF;

namespace Pastebook.Managers
{
    public class UserManager
    {
        DataAccessUserManager daUserManager = new DataAccessUserManager();
        DataAccessPasswordManager daPasswordmanager = new DataAccessPasswordManager();

        public bool LoginUser(string email, string password, out PASTEBOOK_USER user)
        {
            bool result = false;
            string hashPassword = string.Empty;
            string salt = string.Empty;

            user = daUserManager.RetrieveUserByEmail(email);

            if (user != null)
            {
                result = daPasswordmanager.IsPasswordMatch(password, user.SALT, user.PASSWORD);
            }

            return result;
        }
    }
}