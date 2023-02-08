using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MVCPOS.DataLayer;
using MVCPOS.Models;
namespace MVCPOS.ApiControllers
{

    
    public class UserController : ApiController
    {
        UserRepository userRepo = new UserRepository();
        
        //Login user
        [HttpPost]
        public UserModel Login([FromBody]UserModel model)
        {
            var user = userRepo.Login(model);
            return user;
        }

     }
}
