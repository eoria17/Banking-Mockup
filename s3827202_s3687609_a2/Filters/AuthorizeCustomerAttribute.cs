﻿using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using s3827202_s3687609_a2.Areas.Banking.Models;

namespace s3827202_s3687609_a2.Filters
{
    public class AuthorizeCustomerAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var customerID = context.HttpContext.Session.GetInt32(nameof(Customer.CustomerID));
            if (!customerID.HasValue)
                context.Result = new RedirectToActionResult("Index", "Home", null);
        }
    }
}
