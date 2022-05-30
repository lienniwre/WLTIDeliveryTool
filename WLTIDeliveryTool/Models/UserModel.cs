using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;
using WLTIDeliveryTool.Repository;
using WLTIDeliveryTool.Helper;
using System.Web.Security;

namespace WLTIDeliveryTool.Models
{
    public class UserModel
    {
        public readonly UnitOfWork _unitofwork;
        public UserModel()
        {
            _unitofwork = new UnitOfWork(new WLTIDeliveryToolsEntities1());
        }

        public void CreateUser(string username, string password, int role)
        {
            var auth = new AuthHelper();
            var salt = auth.GenerateSalt(16);
            var credential = this.GeneratePassword(password,salt);
            var user = new User { DateCreated = DateTime.Now, UserName = username, Password = credential.Password,Salt = credential.Salt, UserRole = role,Status = 1,LastLogin = DateTime.Now };
            _unitofwork.Users.Add(user);
            _unitofwork.Complete();
        }

        private Credential GeneratePassword(string password,string salt)
        {
            var auth = new AuthHelper();
           
            var encrypted = AuthHelper.EncryptString(AuthHelper.Encryption.MD5, password);
            encrypted = encrypted + salt;
            encrypted = AuthHelper.EncryptString(AuthHelper.Encryption.SHA, encrypted);
            return new Credential { Password = encrypted, Salt = salt};
        }

        public AccountSession GetSession()
        {

            var authCookie = System.Web.HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            var authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            string[] userData = null ?? authTicket.UserData.Split(',');
            var userId = Convert.ToInt32(userData[0]);

            var accountSession = new AccountSession
            {
                UserId = userId,
                Role = null ?? userData[1],
                Name = authTicket.Name
            };

            return accountSession;
        }
        
        public bool Login(User user,string password)
        {
           
            if (user !=null)
            {
                var credential = GeneratePassword(password, user.Salt);
                if(user.Password == credential.Password)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

       
    }
}