using iTextSharp.text;
using iTextSharp.text.pdf;
using MVCPOS.DataLayer;
using MVCPOS.Entities;
using MVCPOS.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCPOS.Controllers
{
    public class PaymentController : Controller
    {
        CustomerRepository repo = new CustomerRepository();
        // GET: Payment
        public ActionResult Payment()
        {
            
            return View();
        }


       


    }
}