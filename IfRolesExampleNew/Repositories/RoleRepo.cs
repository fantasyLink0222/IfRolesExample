using IfRolesExampleNew.Data;
using IfRolesExampleNew.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IfRolesExampleNew.Repositories
{
    public class RoleRepo
    {
        private readonly ApplicationDbContext _context;
    


        public RoleRepo(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            this._context = context;
            CreateInitialRole();
       
        }

        public RoleRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<RoleVM> GetAllRoles()
        {
            var roles = _context.Roles.Select(r => new RoleVM
            {
                Id = r.Id,
                RoleName = r.Name
            }).ToList();

            return roles;
        }

        public RoleVM GetRole(string roleName)
        {
            var role =
                _context.Roles.Where(r => r.Name == roleName)
                              .FirstOrDefault();

            if (role != null)
            {
                return new RoleVM()
                {
                    RoleName = role.Name
                                    ,
                    Id = role.Id
                };
            }
            return null;
        }

        public bool CreateRole(string roleName)
        {
            bool isSuccess = true;


            try
            {
                _context.Roles.Add(new IdentityRole
                {
                    Name = roleName,
                    Id = roleName,
                    NormalizedName = roleName.ToUpper()
                });
                _context.SaveChanges();
            }
            catch (Exception)
            {
                isSuccess = false;
            }

            return isSuccess;
        }

        public void CreateInitialRole()
        {
            const string ADMIN = "Admin";

            var role = GetRole(ADMIN);

            if (role == null)
            {
                CreateRole(ADMIN);
            }
        }

        public async Task<(bool isSuccess, string message)> DeleteRoleAsync(string id)
        {
            // Check if the role is assigned to any user
            var isRoleAssigned = await _context.UserRoles.AnyAsync(ur => ur.RoleId == id);
            if (isRoleAssigned)
            {
                return (false, $"Role: {id} is assigned to a user and cannot be deleted.");
            }

            try
            {
                // If not, proceed to delete the role
                var roleToDelete = new IdentityRole { Id = id };
                _context.Roles.Attach(roleToDelete);
                _context.Roles.Remove(roleToDelete);
                await _context.SaveChangesAsync();

                return (true, $"Role: {id} deleted successfully.");
            }
            catch (Exception e)
            {
                return (false, $"Error deleting role with ID {id}: {e.Message}");
            }
        }

    }
}
