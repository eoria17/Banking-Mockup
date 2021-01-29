using McbaAdmin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace McbaAdmin.Filters
{
    public class AuthorizeCustomerAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var LoginID = context.HttpContext.Session.GetString(nameof(Login.LoginID));
            if (LoginID==null)
                context.Result = new RedirectToActionResult("Login", "Login", null);
        }
    }
}
