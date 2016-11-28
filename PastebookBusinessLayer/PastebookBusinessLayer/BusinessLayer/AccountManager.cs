using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PastebookEF;
using System.Security.Cryptography;
using PastebookDataAccess;

namespace PastebookBusinessLayer.BusinessLayer
{
    public class AccountManager
    {
        IUserRepository _userRepository;

        public AccountManager()
        {
            _userRepository = new UserRepository();
        }

        #region[Password]
        public string GenerateHashedPassword(string password, out string salt)
        {
            salt = GetSaltString();

            string saltedpassword = password + salt;

            return GetHashedPasswordAndSalt(saltedpassword);
        }

        public string GetHashedPasswordAndSalt(string saltedPassword)
        {
            SHA256 sha = new SHA256CryptoServiceProvider();

            byte[] dataBytes = Encoding.ASCII.GetBytes(saltedPassword);
            byte[] resultBytes = sha.ComputeHash(dataBytes);

            return Encoding.ASCII.GetString(resultBytes);
        }

        public string GetSaltString()
        {
            RNGCryptoServiceProvider cryptoServiceProvider = new RNGCryptoServiceProvider();
            byte[] saltBytes = new byte[24];
            cryptoServiceProvider.GetNonZeroBytes(saltBytes);

            return Encoding.ASCII.GetString(saltBytes);
        }

        public bool IsPasswordMatch(string password, string salt, string hashedpassword)
        {
            string saltedPassword = password + salt;
            return hashedpassword == GetHashedPasswordAndSalt(saltedPassword);
        }
        #endregion

        public bool LoginUser(string email, string password, out USER user)
        {
            bool result = false;

            user = _userRepository.Find(x => x.EMAIL_ADDRESS.Equals(email)).FirstOrDefault();

            if (user != null)
            {
                result = IsPasswordMatch(password, user.SALT, user.PASSWORD);
            }

            return result;
        }

        public bool Register(USER user)
        {
            string salt = string.Empty;

            user.PASSWORD = GenerateHashedPassword(user.PASSWORD, out salt);
            user.SALT = salt;

            return _userRepository.Create(user);
        }

        public USER GetUser(Func<USER, bool> condition)
        {
            return _userRepository.Find(condition).FirstOrDefault();
        }

        public USER GetUserWithCountry(Func<USER, bool> condition)
        {
            return _userRepository.GetUserWithCountry(condition).FirstOrDefault();
        }

        public List<USER> SearchUsers(string keyword)
        {
            return _userRepository.Find(x => x.FIRST_NAME.ToLower() == keyword.ToLower() || x.LAST_NAME.ToLower() == keyword.ToLower());
        }

        public bool UpdateUser(USER user)
        {
            return _userRepository.Update(user);
        }
    }
}
