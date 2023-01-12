
using Microsoft.AspNetCore.Identity;

namespace CmsShoppingCart.DAL.Models
{
    public class AppUser : IdentityUser
    {
        public string Occupation { get; set; }
    }
}
