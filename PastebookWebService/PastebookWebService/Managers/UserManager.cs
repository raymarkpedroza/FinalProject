﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PastebookEF;
using PastebookWebService.Entities;
using PastebookWebService.Mappers;

namespace PastebookWebService.Managers
{
    public class UserManager
    {
        public int RegisterUser(UserEntity wcfUserEntity)
        {
            int result = 0;
            PASTEBOOK_USER dbUserTable = new PASTEBOOK_USER();

            dbUserTable = Mapper.MapWCFUserEntityToDBUserTable(wcfUserEntity);

            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    context.PASTEBOOK_USER.Add(dbUserTable);
                    result = context.SaveChanges();
                }
            }

            catch (Exception ex)
            {
                
            }

            return result;
        }

        public UserEntity RetrieveUser(string emailaddress)
        {
            UserEntity user = new UserEntity();

            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    var result = context.PASTEBOOK_USER.Where(x => x.EMAIL_ADDRESS == emailaddress).SingleOrDefault();
                    user = Mapper.MapDBUserTableToWCFUserEntity(result);
                }
            }
            catch (Exception)
            {
            }

            return user;
        }
    }
}