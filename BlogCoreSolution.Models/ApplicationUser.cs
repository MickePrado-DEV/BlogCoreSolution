using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlogCoreSolution.Models
{
    public class ApplicationUser :IdentityUser
    {

        [Required(ErrorMessage = "El Nombre es obligatiorio")]
        public string Name { get; set; }
        [Required(ErrorMessage = "La direccion obligatioria")]
        public string Address { get; set; }
        [Required(ErrorMessage = "La ciudad es obligatioria")]
        public string City { get; set; }
        [Required(ErrorMessage = "El pais es obligatiorio")]
        public string Country { get; set; }
    }
}
