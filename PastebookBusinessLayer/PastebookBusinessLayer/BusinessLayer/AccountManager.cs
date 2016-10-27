using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PastebookEF;
using System.Security.Cryptography;
using PastebookDataAccess.Repositories;

namespace PastebookBusinessLayer.BusinessLayer
{
    public class AccountManager : IAccountManager
    {
        private IUserRepository _userRepo;

        public AccountManager()
        {
            _userRepo = new UserRepository();
        }

        public string GeneratePasswordHash(string password, out string salt)
        {
            salt = GetSaltString();

            string saltedPassword = password + salt;

            return GetPasswordHashAndSalt(saltedPassword);
        }

        public byte[] GetBytes(string text)
        {
            return Encoding.ASCII.GetBytes(text);
        }

        public string GetPasswordHashAndSalt(string saltedPassword)
        {
            // Let us use SHA256 algorithm to 
            // generate the hash from this salted password
            SHA256 sha = new SHA256CryptoServiceProvider();
            byte[] dataBytes = GetBytes(saltedPassword);
            byte[] resultBytes = sha.ComputeHash(dataBytes);

            // return the hash string to the caller
            return GetString(resultBytes);
        }

        public string GetSaltString()
        {
            RNGCryptoServiceProvider cryptoServiceProvider = new RNGCryptoServiceProvider();

            // Lets create a byte array to store the salt bytes
            byte[] saltBytes = new byte[24];

            // lets generate the salt in the byte array
            cryptoServiceProvider.GetNonZeroBytes(saltBytes);

            // Let us get some string representation for this salt
            string saltString = GetString(saltBytes);

            // Now we have our salt string ready lets return it to the caller
            return saltString;
        }

        public string GetString(byte[] resultBytes)
        {
            return Encoding.ASCII.GetString(resultBytes);
        }

        public PASTEBOOK_USER RetrieveUserByEmail(string email)
        {
            PASTEBOOK_USER user = new PASTEBOOK_USER();
            user = _userRepo.RetrieveSpecificRecord(x=>x.EMAIL_ADDRESS.Equals(email));

            return user;
        }

        public PASTEBOOK_USER RetrieveUserById(int id)
        {
            PASTEBOOK_USER user = new PASTEBOOK_USER();
            user = _userRepo.RetrieveSpecificRecord(x => x.ID.Equals(id), c => c.REF_COUNTRY);

            return user;
        }

        public PASTEBOOK_USER RetrieveUserByUsername(string username)
        {
            PASTEBOOK_USER user = new PASTEBOOK_USER();
            user = _userRepo.RetrieveSpecificRecord(x => x.USER_NAME.Equals(username), c => c.REF_COUNTRY);

            return user;
        }

        public bool IsPasswordMatch(string password, string salt, string hash)
        {
            string saltedPassword = password + salt;
            return hash == GetPasswordHashAndSalt(saltedPassword);
        }

        public bool LoginUser(string email, string password, out PASTEBOOK_USER user)
        {
            bool result = false;
            string hashPassword = string.Empty;
            string salt = string.Empty;

            user = _userRepo.RetrieveSpecificRecord(x=>x.EMAIL_ADDRESS.Equals(email));

            if (user != null)
            {
                result = IsPasswordMatch(password, user.SALT, user.PASSWORD);
            }

            return result;
        }

        public bool RegisterUser(PASTEBOOK_USER user)
        {
            bool result = false;
            string salt = string.Empty;
            user.PASSWORD = GeneratePasswordHash(user.PASSWORD, out salt);
            user.SALT = salt;
            result = _userRepo.CreateRecord(user);

            return result;
        }

        public List<PASTEBOOK_USER> SearchUserByName(string name)
        {
            List<PASTEBOOK_USER> searchResults = new List<PASTEBOOK_USER>();
            searchResults = _userRepo.RetrieveList(x=>x.FIRST_NAME.ToLower().Equals(name.ToLower()));
            searchResults.AddRange(_userRepo.RetrieveList(x=>x.LAST_NAME.ToLower().Equals(name.ToLower())));

            return searchResults;
        }

        public bool UpdateUser(PASTEBOOK_USER user)
        {
            bool result = false;
            result = _userRepo.UpdateRecord(user);

            return result;
        }

        public bool CheckUserIfExist_Email(string email)
        {
            return _userRepo.RetrieveList(x=>x.EMAIL_ADDRESS.ToLower() == email.ToLower()).Count() > 0;
        }

        public bool CheckUserIfExist_Username(string username)
        {
            return _userRepo.RetrieveList(x => x.USER_NAME.ToLower() == username.ToLower()).Count() > 0;
        }
    }
}
