using System.Security.Claims;

namespace API.Helpers
{
    public static class ClaimsPrincipalExtensions
    {

        public static string GetEmail(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return string.IsNullOrEmpty(principal.FindFirstValue(ClaimTypes.Email))
                ? principal.FindFirstValue(ClaimTypes.Upn)
                : principal.FindFirstValue(ClaimTypes.Email);
        }
    }
}
