using API.Entities;
using API.Extensions;
using Microsoft.AspNetCore.Identity;

namespace API.Entities;

public class AppUser : IdentityUser<int>
{
    public string KnownAs { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime LastActive { get; set; } = DateTime.UtcNow;
    public ICollection<PlayerCharacter> MyCharacters {get;set;}
    public List<AdventureSave> AdventureSaves { get; set; }
    public ICollection<AppUserRole> UserRoles { get; set; }
}

