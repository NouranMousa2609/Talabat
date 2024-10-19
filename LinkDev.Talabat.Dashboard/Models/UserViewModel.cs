using Microsoft.AspNetCore.Identity;

namespace LinkDev.Talabat.Dashboard.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; } = null!;
        public string DisplayName { get; set; }=null!;

        public string Email { get; set; }= null!;

        public string PhoneNumber { get; set; }= null!;

        public IEnumerable<String> Roles { get; set; }= null!;
    }
}
