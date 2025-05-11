using BookingBackOffice.Models;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingBackOffice.Controllers;

public class AccountController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager) : Controller
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;

    [Authorize(Roles = "Admin")]
    public IActionResult SignUp()
    {
        var signUpModel = new SignUpViewModel();

        if (TempData["ErrorMessage"] is string errorMessage && !string.IsNullOrWhiteSpace(errorMessage))
            signUpModel.ErrorMessage = errorMessage;

        return View(signUpModel);
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> SubmitSignUp(SignUpViewModel model)
    {
        if (ModelState.IsValid)
        {
            var exists = await _userManager.Users.AnyAsync(x => x.Email == model.SignUpModel.Email);
            if (exists)
            {
                this.SetError("Email already exists");
                return View();
            }

            var user = new UserEntity
            {
                UserName = model.SignUpModel.Email,
                Email = model.SignUpModel.Email,
                RestaurantId = model.SignUpModel.RestaurantId
            };

            var result = await _userManager.CreateAsync(user, model.SignUpModel.Password);
            if(result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                return RedirectToAction("SignIn");
            }
            else
            {
                this.SetError(result.Errors.First().Description);
                return RedirectToAction("SignUp");
            }
        }

        this.SetError("You must fill out all the necessary fields!");
        return RedirectToAction("SignUp");
    }

    public IActionResult SignIn()
    {
        if(User.Identity!.IsAuthenticated)
            return RedirectToAction("Index", "Home");

        var signInModel = new SignInViewModel();

        if(TempData["ErrorMessage"] is string errorMessage && !string.IsNullOrWhiteSpace(errorMessage))
            signInModel.ErrorMessage = errorMessage;

        return View(signInModel);
    }

    public async Task<IActionResult> SubmitSignIn(SignInViewModel model)
    {
        if (ModelState.IsValid)
        {
            var signInResult = await _signInManager.PasswordSignInAsync(model.SignInModel.Email, model.SignInModel.Password, false, false);
            if(signInResult.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                this.SetError("Invalid login attempt.");
                return RedirectToAction("SignIn");
            }
        }
        this.SetError("You must fill out all the necessary fields!");
        return RedirectToAction("SignIn");
    }

    public async Task<IActionResult> LogOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("SignIn");
    }
}
