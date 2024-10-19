namespace LinkDev.Talabat.Dashboard.Models
{
    public class UserRoleViewModel
    {
        public string userId { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public List<RoleViewModel> Roles { get; set; } = null!;


    }
}
