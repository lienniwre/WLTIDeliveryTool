using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WLTIDeliveryTool.Models;
using WLTIDeliveryTool.Repository;
using WLTIDeliveryTool.ViewModel;
using DAL;
using System.Web.Security;
using System.Linq.Dynamic.Core;
using HashidsNet;
using WLTIDeliveryTool.Helper;

namespace WLTIDeliveryTool.Controllers
{
    
    public class AccountController : Controller
    {
        // GET: Account
        public readonly UnitOfWork _unitofwork;
        private readonly Hashids hashids;
        public AccountController()
        {
            _unitofwork = new UnitOfWork(new WLTIDeliveryToolsEntities1());
            hashids = new Hashids("washink19", 12);
        }
        
        [MyAuthorize(Roles="1")]
        public ActionResult Index()
        {
            return View();
        }


      
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model,string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = _unitofwork.Users.GetAll().Where(m=>m.UserName == model.Username).FirstOrDefault();
                var verifyLogin = new UserModel().Login(user, model.Password);

                if (verifyLogin == true)
                {
                    string[] myCookies = Request.Cookies.AllKeys;
                    foreach (string cookie in myCookies)
                    {
                        Response.Cookies[cookie].Expires = DateTime.Now.AddDays(1);
                    }

                    var textInfo = new System.Globalization.CultureInfo("en-Us", false).TextInfo;
                    var name = textInfo.ToTitleCase(user.UserName);

                    string userData = user.UserId + "," + user.UserRole;

                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,name, DateTime.Now, DateTime.Now.AddDays(1), true, userData, FormsAuthentication.FormsCookiePath);

                    string hash = FormsAuthentication.Encrypt(ticket);

                    HttpCookie cookies = new HttpCookie(FormsAuthentication.FormsCookieName, hash);

                    if (ticket.IsPersistent)
                    {
                        cookies.Expires = ticket.Expiration;
                    }
                    Response.Cookies.Add(cookies);

                    if (Url.IsLocalUrl(ReturnUrl) && ReturnUrl.Length > 1 && ReturnUrl.StartsWith("/")
                        && !ReturnUrl.StartsWith("//") && !ReturnUrl.StartsWith("/\\"))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                      
                            return RedirectToAction("Index", "Home", new { area = "Admin" });
                       

                    }
                   
                }
                ModelState.AddModelError("Result", "The username or password is incorrect.");
               
            }

            return View(model);

        }
        [MyAuthorize(Roles="1")]
        [HttpGet]
        public ActionResult CreateUser()
        {
            return View();
        }
        [MyAuthorize(Roles = "1,2")]
        [HttpPost]
        public JsonResult SaveUser(CreateUserViewModel model)
        {
            var user = new UserModel();
            user.CreateUser(model.Username, model.Password,model.Role);

            return Json(new { alertCode = 200,message="success" });
        }

        public JsonResult IsAlreadyUsed(string username)
        {
            try
            {
                bool alreadyUsed = _unitofwork.Users.GetAll().Any(m => m.UserName == username);
                return Json(!alreadyUsed);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public JsonResult DisplayUsers()
        {
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];


            var list =from user in  _unitofwork.Users.GetAll().Where(m=>m.Status == 1)select new {
                UserName = user.UserName,
                UserId = user.UserId,
                F = hashids.Encode(user.UserId),
                LastLogin = user.LastLogin.Value,
                Status = user.Status == 1?"Active":"Deactivated"
            };
            int totalRows = list.Count();

            if (!string.IsNullOrEmpty(searchValue))
            {
                list = list.Where(x => x.UserName.ToLower().Contains(searchValue.ToLower())).ToList();
            }
            int totalRowsAfterFiltering = list.Count();

            list = list.AsQueryable().OrderBy(sortColumnName + " " + sortDirection).ToList();

            list = list.Skip(start).Take(length).ToList();

            return Json(new
            {
                data = list,
                draw = Request["draw"],
                recordsTotal = totalRows,
                recordsFiltered = totalRowsAfterFiltering
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult UserDetail(string f)
        {
            try
            {
                var id = hashids.Decode(f)[0];
                var user = _unitofwork.Users.Get(id);
                return View(user);
            }
            catch (Exception)
            {

                return View();
            }
        }
        [HttpGet]
        public ActionResult UpdateUser(string f)
        {
            try
            {
                var id = hashids.Decode(f)[0];
                var user = _unitofwork.Users.Get(id);
                return View(user);
            }
            catch (Exception)
            {

                return View();
            }
        }
        [HttpPost]
        public JsonResult SaveChanges(User model,string F)
        {
            try
            {

                var userId = hashids.Decode(F)[0];

                var user = _unitofwork.Users.Get(userId);


                user.UserName = model.UserName;
                user.Status = model.Status;
                user.UserRole = model.UserRole;


                _unitofwork.Users.Update(user);
                _unitofwork.Complete();
               
                return Json(new { returnCode = 200, message = "success" });
            }
            catch (Exception e)
            {

                return Json(new { returnCode = 201, message = e.Message });
            }
        }
        [Authorize]
        public ActionResult ChangePassword(string f)
        {
            return View();
        }
        [HttpPost]
        public JsonResult ChangePassword(ChangePasswordViewModel model,string f)
        {
            try
            {

                    var userId = hashids.Decode(f)[0];
                    var user = _unitofwork.Users.Get(userId);

                    var Auth = new AuthHelper();

                    var encrypted = AuthHelper.EncryptString(AuthHelper.Encryption.MD5, model.OldPassword);
                    encrypted = encrypted + user.Salt;
                    encrypted = AuthHelper.EncryptString(AuthHelper.Encryption.SHA, encrypted);

                    if (user.Password == encrypted)
                    {
                        var salt = Auth.GenerateSalt(16);
                        encrypted = AuthHelper.EncryptString(AuthHelper.Encryption.MD5, model.NewPassword);
                        encrypted = encrypted + salt;
                        encrypted = AuthHelper.EncryptString(AuthHelper.Encryption.SHA, encrypted);

                        user.Password = encrypted;
                        user.Salt = salt;
                        _unitofwork.Users.Update(user);
                        _unitofwork.Complete();


                        return Json(new { message = "Successfully saved!" });
                    }
                    else
                    {
                        return Json(new { message = "Old Password Incorrect" });
                    }
                
               

            }
            catch (Exception e)
            {
                return Json(new { message = e.Message });
            }

        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        public ActionResult Forbidden()
        {
            return View();
        }
    }
}