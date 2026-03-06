using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E4_Project.Data.Models
{
    internal class UserLogin
    { 
        public int Id { get; set; }

        [Required(ErrorMessage = "Gebruikersnaam is verplicht.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-mailadres is verplicht.")]
        [EmailAddress(ErrorMessage = "Ongeldig e-mailadres.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Wachtwoord is verplicht.")]
        public string Password { get; set; } = string.Empty;
    }
}

