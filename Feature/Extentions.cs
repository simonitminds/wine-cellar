using System.Security.Claims;

public static class ExtentionMethods
{
    public static int GetUserId(this HttpContext context)
    {
        var userIdString = context
            .User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier)
            .Value;
        if (int.TryParse(userIdString, out int userId) == false)
        {
            throw new UnauthorizedAccessException("User not found");
        }
        return userId;
    }
}
