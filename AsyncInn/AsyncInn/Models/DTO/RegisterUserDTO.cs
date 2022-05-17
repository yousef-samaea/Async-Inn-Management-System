using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.DTO
{
    public class RegisterUserDTO
    {
        [Required(ErrorMessage = "Enter The UserName")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "invalid UserName")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Enter The Passwoed")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Enter The EmailAddres")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}

