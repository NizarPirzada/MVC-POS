using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCPOS.Models
{
    public class UserModel
    {
        public int lngUserID { get; set; }
        public string strUserID { get; set; }
        public string strUserPwd { get; set; }
        public string strUserEmail { get; set; }
        public bool ysnAdmin { get; set; }

      

    }
}