using Microsoft.AspNetCore.Identity;

namespace Core.Domain.Models.Core;

public class UserModel: IdentityUser
{
    public string? Name { get; set; }
}