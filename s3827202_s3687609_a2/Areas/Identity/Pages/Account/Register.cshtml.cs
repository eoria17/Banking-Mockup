using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using s3827202_s3687609_a2.Areas.Identity.Data;
using s3827202_s3687609_a2.Areas.Banking.Models;
using s3827202_s3687609_a2.Data;

namespace s3827202_s3687609_a2.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<BankDbUser> _signInManager;
        private readonly UserManager<BankDbUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly BankDbContext _context;

        public RegisterModel(
            UserManager<BankDbUser> userManager,
            SignInManager<BankDbUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            BankDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Full Name")]
            public string Name { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "Account Type")]
            public AccountType AccountType { get; set; }

            [Required]
            [Display(Name = "Starting Balance")]
            public decimal Amount { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new BankDbUser { UserName = Input.Email, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    await _userManager.AddToRoleAsync(user, "Customer");

                    //Create a new Customer
                    var bankUser = _context.Users.Where(x => x.Email == Input.Email).FirstOrDefault();

                    //for testing purposes
                    int _min = 1000;
                    int _max = 9999;
                    Random _rdm = new Random();

                    bankUser.Customer = new Customer()
                    {
                        CustomerID = _rdm.Next(_min, _max),
                        CustomerName = Input.Name,
                        Phone = "+6112345678",
                        Status = CustomerStatus.Unlocked,
                        Accounts = new List<s3827202_s3687609_a2.Areas.Banking.Models.Account>()
                        {
                             new s3827202_s3687609_a2.Areas.Banking.Models.Account
                             {
                                  AccountNumber = _rdm.Next(_min, _max),
                                  AccountType = Input.AccountType,
                                  Balance = Input.Amount,
                                  ModifyDate = DateTime.Now,
                                  FreeTransaction = 4,
                                  Transactions = new List<Transaction>()
                                  {
                                      new Transaction()
                                      {
                                          TransactionType = TransactionType.Deposit,
                                          Amount = Input.Amount,
                                          Comment = "Initial Deposit",
                                          TransactionStatus = TransactionStatus.Idle
                                      }
                                  }
                             }
                        }
                    };

                    await _context.SaveChangesAsync();
                    //bankUser.CustomerID = bankUser.Customer.CustomerID;

                    

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        //await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
