﻿using System.Security.Claims;

namespace Voluntr.Crosscutting.Domain.Helpers.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static string GetUserIdFromToken(this ClaimsPrincipal claimsPrincipal)
        {
            claimsPrincipal.CheckClaimsPrincipal();

            var claims = claimsPrincipal.FindAll(t => t.Type == ClaimTypes.NameIdentifier);

            if (!claims.Any()) return string.Empty;

            return claims.FirstOrDefault()?.Value;
        }

        private static void CheckClaimsPrincipal(this ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal == null)
                throw new Exception("There is no User. Probably [Authorize] is missing.");
        }
    }
}
