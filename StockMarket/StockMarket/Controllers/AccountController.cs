namespace StockMarket.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceContracks.DTO;
using StockMarket.Entities.IdentityEntities;

[Route("[controller]/[action]")]
[AllowAnonymous]
public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        this._userManager = userManager;
        this._signInManager = signInManager;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterDTO registerDTO)
    {
        // check for validation errors
        if (ModelState.IsValid == false)
        {
            ViewBag.Errors = ModelState.Values.SelectMany(x => x.Errors).Select(temp => temp.ErrorMessage);
            return View(registerDTO);
        }

        ApplicationUser user = new ApplicationUser
        {
            PersonName = registerDTO.PersonName,
            Email = registerDTO.Email,
            PhoneNumber = registerDTO.Phone,
            UserName = registerDTO.Email
        };

        IdentityResult identityResult = await _userManager.CreateAsync(user, registerDTO.Password);

        if (identityResult.Succeeded)
        {
            // Sign in
            await _signInManager.SignInAsync(user, false);
            return RedirectToAction(nameof(TradeController.Orders), "Trade");
        }
        else
        {
            foreach (IdentityError identityError in identityResult.Errors)
            {
                ModelState.AddModelError("Register", identityError.Description);
            }
        }
        return View(registerDTO);
    }
}
