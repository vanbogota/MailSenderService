using Identity.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.DAL.Context;

public class IdentityDB : IdentityDbContext<User, Role, string>
{
    public IdentityDB(DbContextOptions<IdentityDB> option) : base(option)
    {

    }
}
