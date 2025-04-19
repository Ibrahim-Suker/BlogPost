using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="FullName is Required")]
        public string FullName { get; set; }
        [Required(ErrorMessage ="Email is Required")]
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "Password Not Match")]
        public string ConfiermPassword { get; set; }
    }
}
