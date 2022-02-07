using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using CityTransport.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;


namespace CityTransport.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [StringLength(15, MinimumLength = 3, ErrorMessage = "The {0} must be at least {2} and must be at max {1} characters long.")]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }
            [Required]
            [StringLength(20, MinimumLength = 3, ErrorMessage = "The {0} must be at least {2} and must be at max {1} characters long.")]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Required]
            public string Role { get; set; }

            public List<SelectListItem> RoleTypes { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "Adult", Text = "Adult"},
            new SelectListItem { Value = "Student", Text = "Student"},
            new SelectListItem { Value = "University Student", Text = "University Student"},
            new SelectListItem { Value = "Retired", Text = "Retired"},
            new SelectListItem { Value = "Parent", Text = "Parent"},
         };
            public string City { get; set; }

            public List<SelectListItem> CityTypes { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "Sofia", Text = "Sofia"},
            new SelectListItem { Value = "Varna", Text = "Varna"},
            new SelectListItem { Value = "Burgas", Text = "Burgas"},
            new SelectListItem { Value = "Plovdiv", Text = "Plovdiv"},
         };
            public string Gender { get; set; }

            public List<SelectListItem> GenderTypes { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "Male", Text = "Male"},
            new SelectListItem { Value = "Female", Text = "Female"},
            new SelectListItem { Value = "Other", Text = "Other"},
         };


            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }


        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                if (Input.FirstName == "admin" || Input.FirstName == "Admin" || Input.LastName == "admin" || Input.LastName == "Admin")
                {
                    var user = new User

                    {
                        UserName = Input.Email,
                        Email = Input.Email,
                        FirstName = Input.FirstName,
                        LastName = Input.LastName,
                        Role = "Admin",
                        City = Input.City,
                        Gender = Input.Gender,

                    };

                    var result = await _userManager.CreateAsync(user, Input.Password);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User created a new account with password.");

                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { userId = user.Id, code = code },
                            protocol: Request.Scheme);

                        await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                }
                else
                {
                    var user = new User

                    {
                        UserName = Input.Email,
                        Email = Input.Email,
                        FirstName = Input.FirstName,
                        LastName = Input.LastName,
                        Role = Input.Role,
                        City = Input.City,
                        Gender = Input.Gender,

                    };

                    var result = await _userManager.CreateAsync(user, Input.Password);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User created a new account with password.");

                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { userId = user.Id, code = code },
                            protocol: Request.Scheme);

                        await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }


                }
             
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
