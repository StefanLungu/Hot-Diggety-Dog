using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Security.Resources;
using System;
using System.Collections.Generic;

namespace Security.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RoleAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly List<string> _roles;

        public RoleAuthorizeAttribute(string roles)
        {
            _roles = new List<string>(roles.Split(','));
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            User user = (User)context.HttpContext.Items[Constants.UserItem];

            if (user == null || !_roles.Contains(user.Role.ToString()))
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
