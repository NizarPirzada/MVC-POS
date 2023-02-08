using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MVCPOS.Models;
using MVCPOS.DataLayer;
namespace MVCPOS.ApiControllers
{
    public class TermsController : ApiController
    {
        TermsRepository repo = new TermsRepository();
        //Get Terms
        public IEnumerable<TermsModel> GetTerms()
        {
            List<TermsModel> terms = new List<TermsModel>();
            terms = repo.GetTermsList();
            return terms;
        }
    }
}
