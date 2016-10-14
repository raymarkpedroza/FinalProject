using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PastebookEF;
using PastebookBusinessLogic.Entities;
using PastebookBusinessLogic.Mappers;

namespace PastebookBusinessLogic.Managers
{
    public class UserManager
    {
        public UserEntity CreateUserObject(int id, string username, string password, string salt, string email, string firstname, string lastname, DateTime birthday, int countryId, string mobilenumber, char gender, byte[] profilePicture, DateTime dateCreated, string aboutMe)
        {
            UserEntity blUser = new UserEntity();

            blUser.Id = id;
            blUser.Username = username;
            blUser.Password = password;
            blUser.Salt = salt;
            blUser.EmailAddress = email;
            blUser.Firstname = firstname;
            blUser.Lastname = lastname;
            blUser.Birthday = birthday;
            blUser.CountryId = countryId;
            blUser.MobileNumber = mobilenumber;
            blUser.Gender = gender.ToString();
            blUser.ProfilePicture = profilePicture;
            blUser.DateCreated = dateCreated;
            blUser.AboutMe = aboutMe;

            return blUser;
        }

        public int RegisterUser(UserEntity blUser)
        {
            int result = 0;
            PASTEBOOK_USER tbUser = new PASTEBOOK_USER();

            tbUser = Mapper.MapBLUserToDBUSer(blUser);

            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    context.PASTEBOOK_USER.Add(tbUser);
                    result = context.SaveChanges();
                }
            }

            catch (Exception ex)
            {
                
            }

            return result;
        }
    }
}
