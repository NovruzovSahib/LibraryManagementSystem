using LibraryManagementSystem.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ReleaseTest.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        //Register Page

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            IdentityUser user = new IdentityUser
            {
                Email = model.Email,
                UserName = model.Username,
            };
            IdentityResult res = await _userManager.CreateAsync(user, model.Password);
            if (res.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }
            foreach (var err in res.Errors)
            {
                ModelState.AddModelError("", err.Description);
            }
            return View();
        }

        // Login Page

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home"); // Redirect to home page if the user is in the "User" role
            }
            ModelState.AddModelError("error", "Login Or Password Is not correct");
            return View(model);
        }

        //Create Role

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateRole(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = model.RoleName
                };

                var result = await _roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    ViewBag.Success = "Role is Created";
                    return View();
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        // Delete Role

        [HttpGet]
        public IActionResult DeleteRole()
        {
            List<RoleViewModel> roles = new List<RoleViewModel>();
            foreach (var role in _roleManager.Roles)
            {
                RoleViewModel model = new RoleViewModel()
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };
                roles.Add(model);
            }

            return View(roles);
        }
        public async Task<IActionResult> DeletedRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role != null)
            {
                await _roleManager.DeleteAsync(role);
            }
            return RedirectToAction("DeleteRole", "Account");
        }

        //AddRoleToUser

        [HttpGet]
        public IActionResult AddRoleToUser()
        {
            AddRoleToUser model = new AddRoleToUser
            {
                RolerList = _roleManager.Roles.ToList(),
                UserList = _userManager.Users.ToList()
            };

            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddRoleToUser(AddRoleToUser model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            await _userManager.AddToRoleAsync(user, model.RoleId);

            return RedirectToAction("AddRoleToUser", "Account");
        }

        //Logout

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        //Denied Page

        [HttpGet]
        public IActionResult Denied()
        {
            return View();
        }

        //Profile Page

        [HttpGet]
        public IActionResult Profile()
        {
            var currentUser = _userManager.GetUserAsync(User).Result;

            if (currentUser != null)
            {
                ProfileViewModel profileViewModel = new ProfileViewModel
                {
                    Username = currentUser.UserName,
                    Email = currentUser.Email,
                };

                return View(profileViewModel);
            }
            else
            {
                return NotFound("User not found");
            }
        }
    }
}
