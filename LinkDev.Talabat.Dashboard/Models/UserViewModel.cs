using Microsoft.AspNetCore.Identity;

namespace LinkDev.Talabat.Dashboard.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }= null!;
        public string UserName { get; set; } = null!;
        public string DisplayName { get; set; }=null!;

        public string Email { get; set; }= null!;

        public string PhoneNumber { get; set; }= null!;

        public IEnumerable<string> Roles { get; set; } = new List<string>();
    }
}
