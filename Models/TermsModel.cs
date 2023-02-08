using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCPOS.Models
{
    public class TermsModel
    {
        public int lngTermsID { get; set; }
        public string strTerms { get; set; }
        public int lngSort { get; set; }
        public bool ysnInactive { get; set; }
        public int lngNumofDays { get; set; }
        }
}