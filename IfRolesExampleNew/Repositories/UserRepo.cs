using IfRolesExampleNew.Data;
using IfRolesExampleNew.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace IfRolesExampleNew.Repositories
{
    public class UserRepo
    {
        private readonly ApplicationDbContext _context;

        public UserRepo(ApplicationDbContext context)
        {
            this._context = context;
          
        }

        public List<UserVM> GetAllUsers()
        {
            var users = _context.Users.Select(u => new UserVM { Email = u.Email }).ToList();

            return users;
        }

      
    }

}
