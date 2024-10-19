using LinkDev.Talabat.Dashboard.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.Talabat.Dashboard.Controllers
{
    public class UserController(UserManager<IdentityUser> _userManager,RoleManager<IdentityRole> _roleManager) : Controller
    {
        public async Task <IActionResult> Index()
        {
            var users =await _userManager.Users.Select(U=>new UserViewModel()
            {
                Id=U.Id,
                //DisplayName=U.DisplayName,
                UserName =U.UserName!,
                PhoneNumber=U.PhoneNumber!,
                Email=U.Email!,
                Roles=_userManager.GetRolesAsync(U).Result,

            }).ToListAsync();
            return View(users);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roles = await _roleManager.Roles.ToListAsync();
            var ViewModel = new UserRoleViewModel()
            {
                userId = user.Id,
                UserName = user.UserName,
                Roles = roles.Select(r => new RoleViewModel()
                {
                    Id = r.Id,
                    Name = r.Name,
                    IsSelected = _userManager.IsInRoleAsync(user, r.Name).Result
                }).ToList()
            };
            return View (ViewModel);
        }
        [HttpPost]
        
        public async Task<IActionResult> Edit(string id, UserRoleViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.userId);

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var role in model.Roles)
            {
                if (userRoles.Any(r => r == role.Name) && !role.IsSelected)
                {
                    await _userManager.RemoveFromRoleAsync(user, role.Name);
                }

                if (!userRoles.Any(r => r == role.Name) && role.IsSelected)
                {
                    await _userManager.AddToRoleAsync(user, role.Name);
                }
            }

            return RedirectToAction(nameof(Index));
        }


    }
}
