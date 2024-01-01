using System.Security.Claims;

namespace API.Helpers
{
    public static class ClaimsPrincipalExtensions
    {
        const string ClaimName = "name";
        const string PreferredUserName = "preferred_username";

        public static string GetDisplayName(this ClaimsPrincipal principal)
        {
            var firstName = principal.FindFirstValue(ClaimTypes.GivenName);
            var lastName = principal.FindFirstValue(ClaimTypes.Surname);
            var email = principal.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName))
            {
                if (!string.IsNullOrEmpty(email))
                    return email;

                return "";
            }

            if (string.IsNullOrEmpty(firstName))
                return lastName;

            if (string.IsNullOrEmpty(lastName))
                return firstName;

            return $"{firstName} {lastName}";
        }

        public static string GetEmail(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return string.IsNullOrEmpty(principal.FindFirstValue(ClaimTypes.Email)) ? principal.FindFirstValue(ClaimTypes.Upn) : principal.FindFirstValue(ClaimTypes.Email);
        }

        public static int GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            var userIdClaim = principal.FindFirstValue(ClaimTypes.NameIdentifier);

            if (int.TryParse(userIdClaim, out var userId))
                return userId;

            return 0;
        }

        public static string GetName(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirstValue(ClaimName);
        }

        public static string GetPreferredUserName(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return string.IsNullOrEmpty(principal.FindFirstValue(PreferredUserName)) ? principal.FindFirstValue(ClaimTypes.Upn) : principal.FindFirstValue(PreferredUserName);
        }
    }
}
