using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using WebApplication1.Models.CustomIdentity;

namespace WebApplication1.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ResetPasswordModel : PageModel
    {
        private readonly UserManager<WebApplication1User> _userManager;

        public ResetPasswordModel(UserManager<WebApplication1User> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Champ E-mail requis.")]
            [EmailAddress(ErrorMessage = "Saisissez une adresse e-mail valide.")]
            [Display(Name = "E-mail ou ou Nom d'utilisateur")]
            //[RegularExpression(@"^[A-Za-z]+[0-9]*(.[A-Za-z0-9-]+)*@msss.gouv.qc.ca$",
            //ErrorMessage = "Saisissez une adresse e-mail valide avec \"@msss.gouv.qc.ca\".")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Champ Mot de passe requis.")]
            [StringLength(100, ErrorMessage = "Le mot de passe doit être au moins {2} caractère de logueur.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Mot de passe")]
            public string Password { get; set; }
            [Required(ErrorMessage = "Le champ Confirmation de Mot de passe est requis")]
            [DataType(DataType.Password)]
            [Display(Name = "Confirmation de mot de passe")]
            [Compare("Password", ErrorMessage = "Le mot de passe et le mot de passe de confirmation ne correspondent pas.")]
            public string ConfirmPassword { get; set; }

            public string Code { get; set; }
        }

        public IActionResult OnGet(string code = null)
        {
            if (code == null)
            {
                return BadRequest("Un code doit être fourni pour la réinitialisation du mot de passe.");
            }
            else
            {
                Input = new InputModel
                {
                    Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code))
                };
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToPage("./ResetPasswordConfirmation");
            }

            var result = await _userManager.ResetPasswordAsync(user, Input.Code, Input.Password);
            if (result.Succeeded)
            {
                return RedirectToPage("./ResetPasswordConfirmation");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }
    }
}
