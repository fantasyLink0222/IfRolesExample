using IfRolesExampleNew.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IfRolesExampleNew.ViewModels;

namespace IfRolesExampleNew.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<MyRegisteredUser> MyRegisteredUsers { get; set; }
        public DbSet<IfRolesExampleNew.ViewModels.RoleVM> RoleVM { get; set; } = default!;
        public DbSet<IfRolesExampleNew.ViewModels.UserRoleVM> UserRoleVM { get; set; } = default!;
    }
}
