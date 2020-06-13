using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetIdentityProject.ViewModel
{
    public class UserLoginViewModel
    {
        [Required(ErrorMessage ="Email alanı gereklidir.")]
        [Display(Name ="Email Adresiniz")]
        [EmailAddress]
        [DataType(DataType.Password)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Şifre alanı gereklidir.")]
        [Display(Name = "Şifreniz")]
        [DataType(DataType.Password)]
        [MinLength(5,ErrorMessage ="Şifreniz en az 5 karakter olmalıdır.")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
