using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using WebApplication1.Models.CustomIdentity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace WebApplication1.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<WebApplication1User> _userManager;

        public ConfirmEmailModel(UserManager<WebApplication1User> userManager)
        {
            _userManager = userManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Impossible de charger l'utilisateur avec l'ID '{userId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            StatusMessage = result.Succeeded ? "L'Courriel a été confirmé avec succes." : "Erreur de confirmation Courriel.";
            //if (user.EmailConfirmed) 
            //{ 
            //    await _emailSender.SendEmailAsync(user.Email, "Création de votre compte",
            //                $" Votre compte a été activé et vous pouvez commencer à l'utliliser. ");
            //}
            return Page();
        }
    }
}
