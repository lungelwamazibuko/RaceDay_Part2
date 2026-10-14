using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RaceDay_Part2.Data;
using RaceDay_Part2.Models.ViewModels;

namespace RaceDay_Part2.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserRolesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<UserRolesController> _logger;

        public UserRolesController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<UserRolesController> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }
        // GET: UserRoles
        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.ToList();
            var userRolesViewModel = new List<UserRolesViewModel>();
            foreach (var user in users)
            {
                var thisViewModel = new UserRolesViewModel
                {
                    UserId = user.Id,
                    Email = user.Email ?? "",
                    UserName = user.UserName ?? "",
                    Roles = await _userManager.GetRolesAsync(user)
                };

                userRolesViewModel.Add(thisViewModel);
            }
            return View(userRolesViewModel);

        }
        public async Task<IActionResult> Manage(string userId)
        {
            ViewBag.userId = userId;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }
            ViewBag.UserName = user.UserName;
            var model = new List<ManageUserRolesViewModel>();
            foreach (var role in _roleManager.Roles.ToList())
            {
                var userRolesViewModel = new ManageUserRolesViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name ?? ""
                };
                if (await _userManager.IsInRoleAsync(user, role.Name ?? ""))
                {
                    userRolesViewModel.isSelected = true;
                }
                else
                {
                    userRolesViewModel.isSelected = false;
                }
                    model.Add(userRolesViewModel);
            }
            return View(model);
        }
        // POST: UserRoles/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Manage(string userId, List<ManageUserRolesViewModel> model)
        {
            _logger.LogInformation("Manage POST for user {UserId}, model count={Count}", userId, model?.Count ?? 0);
            foreach (var m in model ?? Enumerable.Empty<ManageUserRolesViewModel>())
                _logger.LogInformation("Role {Role} selected={Selected}", m.RoleName, m.isSelected);

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("NotFound");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }

            // ensure 'model' is not null before calling LINQ methods
            var selectedRoleNames = (model ?? Enumerable.Empty<ManageUserRolesViewModel>())
                .Where(x => x.isSelected)
                .Select(y => y.RoleName);

            result = await _userManager.AddToRolesAsync(user, selectedRoleNames);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> GetRolesPartial(string userId)
        {
            ViewBag.userId = userId;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            ViewBag.UserName = user.UserName;
            var model = new List<ManageUserRolesViewModel>();
            foreach (var role in _roleManager.Roles.ToList())
            {
                var userRolesViewModel = new ManageUserRolesViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name ?? ""
                };
                userRolesViewModel.isSelected = await _userManager.IsInRoleAsync(user, role.Name ?? "");
                model.Add(userRolesViewModel);
            }
            return PartialView("_ManageRolesPartial", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> UpdateRolesAjax(string userId, List<ManageUserRolesViewModel> model)
        {
            if (string.IsNullOrEmpty(userId))
                return Json(new { success = false, message = "Missing userId" });

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return Json(new { success = false, message = "User not found" });
            }

            // Remove all existing roles
            var currentRoles = await _userManager.GetRolesAsync(user);
            var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!removeResult.Succeeded)
            {
                return Json(new { success = false, message = "Failed to remove existing roles" });
            }

            // Add selected roles (safe null handling)
            var selectedRoles = (model ?? Enumerable.Empty<ManageUserRolesViewModel>())
                .Where(x => x.isSelected)
                .Select(x => x.RoleName)
                .ToArray();

            var addResult = await _userManager.AddToRolesAsync(user, selectedRoles);
            if (!addResult.Succeeded)
            {
                return Json(new { success = false, message = "Failed to add selected roles" });
            }

            var rolesText = string.Join(", ", selectedRoles);
            _logger.LogInformation("Updated roles for user {UserId}: {Roles}", userId, rolesText);

            return Json(new { success = true, userId = userId, roles = rolesText });
        }
    }
}
