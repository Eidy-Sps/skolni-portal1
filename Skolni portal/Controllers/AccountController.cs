using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Skolni_portal.Data;
using Skolni_portal.ViewModels;

namespace Skolni_portal.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context,
            ILogger<AccountController> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Register(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                // Dvojitá kontrola, že e-mail končí správnou doménou
                if (!model.Email.EndsWith("@spstrutnovska.cz", StringComparison.OrdinalIgnoreCase))
                {
                    ModelState.AddModelError(string.Empty, "Registrace je povolena pouze pro doménu @spstrutnovska.cz");
                    return View(model);
                }

                // Ověření kódu učitele
                if (model.IsTeacher)
                {
                    if (string.IsNullOrWhiteSpace(model.TeacherCode))
                    {
                        ModelState.AddModelError(string.Empty, "Pro registraci jako učitel je vyžadován správní kód.");
                        return View(model);
                    }

                    var validCode = await _context.TeacherCodes
                        .FirstOrDefaultAsync(tc => tc.Code == model.TeacherCode && tc.IsActive);

                    if (validCode == null)
                    {
                        ModelState.AddModelError(string.Empty, "Neplatný správní kód pro učitele.");
                        return View(model);
                    }
                }

                var user = new ApplicationUser 
                { 
                    UserName = model.Email, 
                    Email = model.Email,
                    IsTeacher = model.IsTeacher
                };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Přiřazení role podle typu uživatele
                    if (model.IsTeacher)
                    {
                        await _userManager.AddToRoleAsync(user, "Teacher");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "Student");
                    }

                    _logger.LogInformation("Uživatel si úspěšně vytvořil účet. Učitel: {IsTeacher}", model.IsTeacher);

                    // Přidání IsTeacher claim
                    if (model.IsTeacher)
                    {
                        await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("IsTeacher", "True"));
                    }

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToLocal(returnUrl);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Email);

                var result = await _signInManager.PasswordSignInAsync(
                    model.Email,
                    model.Password,
                    model.RememberMe,
                    lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    _logger.LogInformation("Uživatel se úspěšně přihlásil.");
                    return RedirectToLocal(returnUrl);
                }

                if (result.IsLockedOut)
                {
                    _logger.LogWarning("Uživatelský účet je zamčený.");
                    ModelState.AddModelError(string.Empty, "Účet je dočasně zamčený. Zkuste to později.");
                    return View(model);
                }

                ModelState.AddModelError(string.Empty, "Neplatný email nebo heslo.");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("Uživatel se odhlásil.");
            return RedirectToAction("Index", "Home");
        }

        private IActionResult RedirectToLocal(string? returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
