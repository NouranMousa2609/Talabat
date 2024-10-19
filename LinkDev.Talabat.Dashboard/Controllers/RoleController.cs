using LinkDev.Talabat.Dashboard.Models;
using LinkDev.Talabat.Dashboard.Views.Role;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace LinkDev.Talabat.Dashboard.Controllers
{
    public class RoleController(RoleManager<IdentityRole> _roleManager) : Controller
    {
        public async Task <IActionResult> Index()
        {
            var roles=await _roleManager.Roles.ToListAsync();

            return View(roles);
        }

        [HttpPost]
        public async Task <IActionResult> Create (RoleFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                var roleExists=await _roleManager.RoleExistsAsync(model.Name);
                if (!roleExists)
                {
                    await _roleManager.CreateAsync(new IdentityRole(model.Name.Trim()));
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Name", "Role is already Exist");
                    return View("Index",await _roleManager.Roles.ToListAsync());
                }
            }
            return RedirectToAction(nameof(Index));


        }
        public async Task<IActionResult> Delete (string id )
        {
            var role =await _roleManager.FindByIdAsync(id);
             await _roleManager.DeleteAsync(role);
            return RedirectToAction(nameof(Index));


        }
        public async Task <IActionResult> Edit (string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            var MappedRole = new RoleViewModel
            {
                Name = role.Name
            };
            return View(MappedRole);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string id,RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var roleExists = await _roleManager.RoleExistsAsync(model.Name);
                if (!roleExists)
                {
                    var role=await _roleManager.FindByIdAsync(model.Id);
                    role.Name=model.Name;
                    await _roleManager.UpdateAsync(role);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Name", "Role is already Exist");
                    return View("Index", await _roleManager.Roles.ToListAsync());
                }
            }
            return RedirectToAction(nameof(Index));



        }

    }
}
