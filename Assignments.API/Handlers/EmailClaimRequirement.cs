using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Assignments.API.Handlers
{
    public class EmailClaimRequirement : IAuthorizationRequirement
    {
    }
    public class EmailClaimHandler : AuthorizationHandler<EmailClaimRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EmailClaimRequirement requirement)
        {

            var hasEmailClaim = context.User.HasClaim(c => c.Type == ClaimTypes.Email && c.Value.Contains("moag"));
            //EmpRole is a custom claim type
            var hasRoleClaim = context.User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == "Admin");

            if (hasEmailClaim && hasRoleClaim)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }   
}
