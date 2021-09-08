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
using WebApplication1.Models.CustomIdentity;
namespace WebApplication1.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<WebApplication1User> _signInManager;
        private readonly UserManager<WebApplication1User> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<WebApplication1User> userManager,
            SignInManager<WebApplication1User> signInManager,
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

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required (ErrorMessage = "Le champ Prenom est requis")]
            [DataType(DataType.Text)]
            [Display(Name = "Prenom")]
            public string FirstName { get; set; }

            [Required(ErrorMessage = "Le champ Nom est requis")]
            [DataType(DataType.Text)]
            [Display(Name = "Nom*")]
            public string LastName { get; set; }

            [Required (ErrorMessage = "Le champ Date de naissance est requis")]
            [Display(Name = "Date de naissance")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public DateTime DateBirth { get; set; }

            [Required(ErrorMessage = "Le champ E-mail est requis")]
            [EmailAddress(ErrorMessage = "Saisissez une adresse e-mail valide.")]
            [Display(Name = "E-mail ou Nom d'utilisateur")]
            //[RegularExpression(@"^[A-Za-z]+[0-9]*(.[A-Za-z0-9-]+)*@msss.gouv.qc.ca$",
            //ErrorMessage = "Saisissez une adresse e-mail valide avec \"@msss.gouv.qc.ca\".")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Le champ Mot de passe est requis")]
            [StringLength(100, ErrorMessage = "Le mot de passe doit être au moins {2} caractère de logueur.", MinimumLength = 8)]
            [DataType(DataType.Password)]
            [Display(Name = "Mot de passe")]
            public string Password { get; set; }

            [Required(ErrorMessage = "Le champ Confirmation de Mot de passe est requis")]
            [DataType(DataType.Password)]
            [Display(Name = "Confirmation de mot de passe")]
            [Compare("Password", ErrorMessage = "Le mot de passe et le mot de passe de confirmation ne correspondent pas.")]
            public string ConfirmPassword { get; set; }

            
            [DataType(DataType.Text)]
            [Display(Name = "Unité administrative")]
            public string UniteAdmin { get; set; }

            
            [DataType(DataType.Text)]
            [Display(Name = "Poste")]
            public string Poste { get; set; }

            [Required(ErrorMessage = "Vous devez choisir un rôle")]
            [Display(Name = "Rôle")]
            public Role Role { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            int count = 1;
            StringBuilder username = new StringBuilder(Input.LastName.Substring(0, Input.LastName.Length >= 3 ? 3 : Input.LastName.Length).ToUpper()
                                        + Input.FirstName.Substring(0, Input.FirstName.Length >= 2 ? 2 : Input.FirstName.Length).ToUpper(), 100);
                                        //+ Input.DateBirth.Day + "" + Input.DateBirth.Month+""+ Input.DateBirth.Year.ToString().Substring(2, 3), 100);
            bool exist = true;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            do
            {
                if (count <= 9)
                {
                    username.Append("0" + count);
                }
                if (count > 9)
                {
                    username.Append(count);
                }
                var user = await _userManager.FindByNameAsync(username.ToString());
                if (user == null)
                {
                    exist = false;
                }
                if (user != null)
                {
                    count++;
                }

            } while (exist);
            if (ModelState.IsValid)
            {
                var user = new WebApplication1User
                {
                    UserName = Input.Email,//username.ToString(),
                    Email = Input.Email,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    DateBirth = Input.DateBirth,
                    Poste = Input.Poste,
                    Role = Input.Role,
                    UniteAdmin = Input.UniteAdmin
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("L'utilisateur a créé un nouveau compte avec un mot de passe.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync("bayes.diarra@msss.gouv.qc.ca", "Confirmation d'e-mail",
                        $" S'il vous plaît, confirmez l'e-mail en <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>cliquant sur ce lien</a>.");

                    await _emailSender.SendEmailAsync(Input.Email, "Création de votre compte",
                            $" Votre compte a été Créé sont activation pourrait prendre quelques instants que vous puissez commencer à l'utliliser. \n" +
                            $"Vos Identiants sont : \n" +
                            $"Nom d'utilisateur : {Input.Email} \n" +
                            $"Mot de passe : {Input.Password}");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
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
