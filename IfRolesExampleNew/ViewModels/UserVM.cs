using System.ComponentModel.DataAnnotations;

namespace IfRolesExampleNew.ViewModels
{
    public class UserVM
    {

        [Key]
        
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
