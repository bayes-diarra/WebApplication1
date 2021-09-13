using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models.CustomIdentity;

namespace WebApplication1.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<WebApplication1User> _userManager;
        private readonly SignInManager<WebApplication1User> _signInManager;

        public IndexModel(
            UserManager<WebApplication1User> userManager,
            SignInManager<WebApplication1User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Prénom")]
            public string FirstName { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Nom")]
            public string LastName { get; set; }

            [Required]
            [Display(Name = "Date d'anniversaire")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public DateTime DateBirth { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Unité administrative")]
            public string UniteAdmin { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Poste")]
            public string Poste { get; set; }

            [Required]
            [Display(Name = "Rôle")]
            public Role Role { get; set; }

            [Phone]
            [Display(Name = "Numéro de téléphone")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(WebApplication1User user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateBirth = user.DateBirth,
                UniteAdmin = user.UniteAdmin,
                Poste = user.Poste,
                Role = user.Role,
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Impossible de trouver l\'utilisateur '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Impossible de trouver l\'utilisateur '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Une Erreur est surbenue lors de la tentative de définition du numéro de téléphone.";
                    return RedirectToPage();
                }
            }


            if (Input.Poste != user.Poste)
            {
                user.Poste = Input.Poste;
            }


            await _userManager.UpdateAsync(user);


            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Votre profil a étét mise à jour!";
            return RedirectToPage();
        }
    }
}
