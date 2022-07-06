using GymManager.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymManager.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _userManagerRoles;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(SignInManager<IdentityUser> signInManager, 
            UserManager<IdentityUser> userManager, RoleManager<IdentityRole> userManagerRoles)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userManagerRoles = userManagerRoles;

            if (!_userManager.Users.Any())
            {

                var result = _userManager.CreateAsync(new IdentityUser
                {
                    Email = "abner@gmail.com",
                    EmailConfirmed = true,
                    UserName = "abner@gmail.com",
                    
                }, "Pa$$w0rd.1").Result;

                
            }

            //IdentityUser identityUser = new IdentityUser
            //{
            //    Email = "jesus3@gmail.com",
            //    EmailConfirmed = true,
            //    UserName = "jesus3@gmail.com"
            //};

            //IdentityRole role = new IdentityRole("Admin");

            //var z = _userManagerRoles.CreateAsync(role).Result;

            //var x = _userManager.CreateAsync(identityUser, "Pa$$w0rd.1").Result;

            //var y = _userManager.AddToRoleAsync(identityUser, "Admin").Result;

            IdentityUser identityUser = _userManager.FindByNameAsync("jesus3@gmail.com").Result;

            var x = _userManager.GetRolesAsync(identityUser).Result;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            string returnUrl = string.IsNullOrEmpty(Request.Query["returnUrl"]) ? Url.Content("~/") : Request.Query["returnUrl"];

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    return LocalRedirect(returnUrl);
                }

                if (result.IsLockedOut)
                {
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View();
                }

            }


            return View();
        }
    }
}
