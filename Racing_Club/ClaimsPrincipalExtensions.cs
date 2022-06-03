using System.Security.Claims;

namespace Racing_Club;

public static class ClaimsPrincipalExtensions
{
    /// <summary>
    ///     Help us work with the giant object
    ///     from IHttpContextAccessor
    ///     and break into small pieces
    /// </summary>
    /// <param name="user"></param>
    /// <returns>AppUserId</returns>
    public static string GetUserId(this ClaimsPrincipal user)
    {
        return user.FindFirst(ClaimTypes.NameIdentifier).Value;
    }
}