using System.Diagnostics;
using System.Threading.Tasks;
using System.Security.Claims;
using AutoMapper;
using IdentitySample.Entites;
using IdentitySample.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;

namespace IdentitySample.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public AccountController(IMapper mapper, UserManager<User> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegistrationModel userModel)
        {
            if(!ModelState.IsValid)
            {
                return View(userModel);
            }

            var user = _mapper.Map<User>(userModel);

            var result = await _userManager.CreateAsync(user, userModel.Password);
            if(!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return View(userModel);
            }

            await _userManager.AddToRoleAsync(user, "Guest");

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginModel userModel, string returnUrl = null)
        {
            if(!ModelState.IsValid)
            {
                return View(userModel);
            }

            var user = await _userManager.FindByEmailAsync(userModel.Email);
            if(user != null && 
               await _userManager.CheckPasswordAsync(user, userModel.Password))
            {
                var identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
                identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                // 可以添加更多自定义用户信息
                identity.AddClaim(new Claim("firstname", user.FirstName));
                identity.AddClaim(new Claim("lastname", user.LastName));
                var roles = await _userManager.GetRolesAsync(user);
                foreach (var role in roles)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, role));
                }

                await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme,
                                              new ClaimsPrincipal(identity));

                return RedirectToLocal(returnUrl);
            }
            else
            {
                ModelState.AddModelError("", "无效的用户名或密码");
                return View();
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction(nameof(HomeController.Index), "Home");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login2([FromServices]SignInManager<User> signInManager, UserLoginModel userModel, string returnUrl = null)
        {
            if(!ModelState.IsValid)
            {
                return View(userModel);
            }

            var result = await signInManager.PasswordSignInAsync(
                userModel.Email, userModel.Password, 
                userModel.RememberMe, false);
    
            if (result.Succeeded)
            {
                return RedirectToLocal(returnUrl);
            }

            ModelState.AddModelError("", "无效的用户名或密码");
            return View();
        }

        public async Task<IActionResult> Logout([FromServices]SignInManager<User> signInManager)
        {
            await signInManager.SignOutAsync();

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel forgotPasswordModel)
        {
            return View(forgotPasswordModel);
        }

        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }
    }
}