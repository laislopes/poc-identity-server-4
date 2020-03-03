using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProtectedApi.Config
{
    public class MyRequirement : IAuthorizationRequirement
    {
        //private readonly ISeuRepositorio _repositorio;
        //public MyRequirement(ISeuRepositorio repositorio)
        //{
        //    _repositorio = repositorio;
        //}
        public bool HasPermission(System.Security.Claims.ClaimsPrincipal user) => true;
    }

    public class AuthorizationHandler : AuthorizationHandler<MyRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MyRequirement requirement)
        {
            var user = context.User;
            var name = user.Identities;
            if (requirement.HasPermission(user))
                context.Succeed(requirement);
            else
                context.Fail();

            return Task.CompletedTask;
        }
    }
}
