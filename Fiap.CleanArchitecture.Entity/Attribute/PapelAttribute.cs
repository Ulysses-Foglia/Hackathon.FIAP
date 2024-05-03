using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Fiap.CleanArchitecture.Entity.Attribute
{
    public class PapelAttribute : TypeFilterAttribute
    {
        public PapelAttribute(string claimValue) : base(typeof(ValidarPapelFilter))
        {
            Arguments = new object[] { claimValue };
        }
    }

    public class ValidarPapelFilter : IAuthorizationFilter
    {
        private readonly string _claimValue;

        public ValidarPapelFilter(string claimValue)
        {
            _claimValue = claimValue;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var hasClaim = context.HttpContext.User.Claims
                .Any(c => c.Type == ClaimTypes.Role && c.Value == _claimValue);
            
            if (!hasClaim)
                context.Result = new UnauthorizedObjectResult("Operação não autorizada!");
        }
    }
}
