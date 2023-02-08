using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCPOS.Controllers
{
    public class AccountController : Controller
    {
        //Returns Login View
        public ActionResult Login()
        {
            return View();
        }


        public ActionResult Logout()
        {
            return View();
        }

        public ActionResult ChangePassword()
        {
            return View();
        }


    }
}