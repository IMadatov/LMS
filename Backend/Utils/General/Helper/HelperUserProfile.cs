using General.Enums;
using General.Models;
using System.Security.Claims;

namespace General.Helper;

public static class HelperUserProfile
{
    public static UserProfile GetUserProfile(ClaimsPrincipal? model)
    {
        if(model==null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        var claims = model.Claims;
       
        var enumerable = claims as Claim[] ?? claims.ToArray();
       
        var profile = new UserProfile
        {
            Id = Guid.Parse(enumerable.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value),
            UserName = enumerable.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value,
            Language = (Languages)Enum.Parse(typeof(Languages), enumerable.FirstOrDefault(x => x.Type == ClaimTypes.Country).Value, true) | Languages.ENGLISH,
            Role = enumerable.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value,
            Fullname=enumerable.FirstOrDefault(x=>x.Type=="FULLNAME").Value,

        };

        return profile;
    }
}
