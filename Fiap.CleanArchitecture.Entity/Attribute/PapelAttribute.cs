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

        public PapelAttribute(object[] claimValue) : base(typeof(ValidarPapelFilter))
        {
            Arguments = new object[] { claimValue };
        }
    }

    public class ValidarPapelFilter : IAuthorizationFilter
    {
        private readonly string _claimValue;
        private readonly object[] _claimValues;

        public ValidarPapelFilter(string claimValue)
        {
            _claimValue = claimValue;
        }

        public ValidarPapelFilter(object[] claimValues)
        {
            _claimValues = claimValues;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var hasClaim = context.HttpContext.User.Claims
                .Any(c => c.Type == ClaimTypes.Role && c.Value == _claimValue);
            bool existeRegra = false;

            if (_claimValues != null && _claimValues.Any()) 
            {
                foreach (string Valor in _claimValues)
                {
                    existeRegra = context.HttpContext.User.Claims
                    .Any(c => c.Type == ClaimTypes.Role && c.Value == Valor);
                    if (existeRegra) break;
                }

                if (!existeRegra)
                    context.Result = new UnauthorizedObjectResult("Operação não autorizada!");
               
                return;
            }

            if (!hasClaim)
                context.Result = new UnauthorizedObjectResult("Operação não autorizada!");
        }
    }
}
