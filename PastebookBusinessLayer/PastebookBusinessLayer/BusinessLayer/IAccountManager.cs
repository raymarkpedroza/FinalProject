using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookBusinessLayer.BusinessLayer
{
    public interface IAccountManager
    {
        string GeneratePasswordHash(string password, out string salt);
        bool IsPasswordMatch(string password, string salt, string hash);
        string GetSaltString();
        string GetPasswordHashAndSalt(string saltedPassword);

        byte[] GetBytes(string message);
        string GetString(byte[] resultBytes);

        bool LoginUser(string email, string password, out PASTEBOOK_USER user);
        bool RegisterUser(PASTEBOOK_USER user);
        bool UpdateUser(PASTEBOOK_USER user);

        PASTEBOOK_USER RetrieveUserByEmail(string email);
        PASTEBOOK_USER RetrieveUserByUsername(string username);
        PASTEBOOK_USER RetrieveUserById(int id);
        List<PASTEBOOK_USER> SearchUserByName(string name);
    }
}
