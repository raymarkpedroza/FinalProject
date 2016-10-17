﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace PastebookWebService.Managers
{
    public class PasswordManager
    {

        //password
        public string GeneratePasswordHash(string password, out string salt)
        {
            salt = GetSaltString();

            string finalString = password + salt;

            return GetPasswordHashAndSalt(finalString);
        }

        public bool IsPasswordMatch(string password, string salt, string hash)
        {
            string finalString = password + salt;
            return hash == GetPasswordHashAndSalt(finalString);
        }

        //hash
        public string GetPasswordHashAndSalt(string message)
        {
            // Let us use SHA256 algorithm to 
            // generate the hash from this salted password
            SHA256 sha = new SHA256CryptoServiceProvider();
            byte[] dataBytes = GetBytes(message);
            byte[] resultBytes = sha.ComputeHash(dataBytes);

            // return the hash string to the caller
            return GetString(resultBytes);
        }

        //salt
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

        //utility
        public static byte[] GetBytes(string message)
        {
            return Encoding.ASCII.GetBytes(message);
        }

        public static string GetString(byte[] resultBytes)
        {
            return Encoding.ASCII.GetString(resultBytes);
        }
    }
}