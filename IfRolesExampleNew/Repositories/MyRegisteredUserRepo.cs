using IfRolesExampleNew.Data;
using IfRolesExampleNew.Models;
using IfRolesExampleNew.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Threading.Tasks;

namespace IfRolesExampleNew.Repositories
{
    public class MyRegisteredUserRepo
    {

        private readonly ApplicationDbContext _context;

        public MyRegisteredUserRepo(ApplicationDbContext context)
        {
            this._context = context;
        }
    
   

        public async Task<string>  GetUserNameByEmailAsync(string email)
        {
    
            // Assuming MyRegisteredUser has an Email property
            var registeredUser = await _context.MyRegisteredUsers
                                               .FirstOrDefaultAsync(mru => mru.Email == email);

            if (registeredUser != null)
            {
              var userFullName = $"{registeredUser.FirstName} {registeredUser.LastName}";

                return userFullName;           
             }

            return email;
        }

    }
}
