using IALDashboard.DAL;
using IALDashboard.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IALDashboard.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Login()
        {
            if (Session["isLogedin"] != null)
            {
                return RedirectToAction("Dashboard", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Verify(User user)
        {
            DataTable dt = new User_DAL().VerifyUser(user);

            //DataTable dt = new db().getUserInfo(user);

            if (dt.Rows.Count > 0)
            {

                DataRow dr = dt.Rows[0];
                Session["user_id"] = Convert.ToString(dr["USER_ID"]);
                Session["user_name"] = Convert.ToString(dr["USER_NAME"]);
                Session["user_area"] = Convert.ToString(dr["USER_AREA"]);
                Session["isLogedin"] = true;

                DataTable menuAccess = new User_DAL().UserMenuAccess(Convert.ToString(dr["USER_ID"]));
                Session["menuAccess"] = menuAccess;

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.error = "User ID or Password is not correct";
                return View("Login");
            }

        }

        public ActionResult Logout()
        {

            Session["isLogedin"] = null;
            return RedirectToAction("Login", "User");
        }


        [HttpGet]
        public string user_info(string user_id)
        {
            DataTable dt = new User_DAL().UserInfo(user_id);
            //DataTable dt = new db().getUserInfo(user);
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(dt);
            return json;

        }


        [Filters.AuthorizedUser]
        public ActionResult Create()
        {
            DataTable menu = new User_DAL().getMenu("");
            ViewBag.usermenu = menu;
            return View();
        }


        [Filters.AuthorizedUser]
        public ActionResult Save(FormCollection f)
        {

            string user_id = f["USER_ID"];
            string USER_NAME = f["USER_NAME"];
            string USER_EMAIL = f["USER_EMAIL"];
            string USER_PASS = f["USER_PASS"];
            string USER_AREA = f["USER_AREA"];

            int result = new User_DAL().user_save(user_id, USER_NAME, USER_PASS, USER_EMAIL, USER_AREA);
            if (result != 0)
            {
                if (f["MENU_ID"] != "")
                {
                    string[] MENUS = f["MENU_ID"].Split(',');
                    foreach (string item in MENUS)
                    {
                        int MENU_ID = int.Parse(item);
                        new User_DAL().menu_access_save(user_id, MENU_ID);
                    }
                }
                ViewBag.save_message = "User id " + user_id + " created successfully";
            }
            else
            {
                ViewBag.save_message = "User id " + user_id + " has not created. Please try again.";
            }
            DataTable menu = new User_DAL().getMenu("");
            ViewBag.usermenu = menu;

            return View("Create");
        }


        [Filters.AuthorizedUser]
        public ActionResult UserList()
        {
            DataTable dt = new User_DAL().UserInfo("");
            ViewBag.userlist = dt;
            return View();
        }



        [Filters.AuthorizedUser]
        public ActionResult EditUser(string user_id)
        {

            DataTable dt = new User_DAL().UserInfo(user_id);
            ViewBag.userinfo = dt;

            DataTable menu = new User_DAL().getMenu(user_id);
            ViewBag.usermenu = menu;

            return View();
        }


        [Filters.AuthorizedUser]
        public ActionResult UserUpdate(FormCollection f)
        {

            string user_id = f["USER_ID"];
            string USER_NAME = f["USER_NAME"];
            string USER_EMAIL = f["USER_EMAIL"];
            string USER_AREA = f["USER_AREA"];

            int result = new User_DAL().user_update(user_id, USER_NAME, USER_EMAIL, USER_AREA);
            if (result != 0)
            {
                new User_DAL().menu_access_delete(user_id);

                if (f["MENU_ID"] != "")
                {
                    string[] MENUS = f["MENU_ID"].Split(',');
                    foreach (string item in MENUS)
                    {
                        int MENU_ID = int.Parse(item);
                        new User_DAL().menu_access_save(user_id, MENU_ID);
                    }
                }
                ViewBag.save_message = "User id " + user_id + " Update successfully";
            }
            else
            {
                ViewBag.save_message = "User id " + user_id + " has not updated. Please try again.";
            }
            DataTable menu = new User_DAL().getMenu("");
            ViewBag.usermenu = menu;

            return View("Create");
        }
    }
}