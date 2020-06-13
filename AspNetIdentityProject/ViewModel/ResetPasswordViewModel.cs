using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetIdentityProject.ViewModel
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Şifre alanı gereklidir.")]
        [Display(Name = "Yeni Şifreniz")]
        [DataType(DataType.Password)]
        [MinLength(5, ErrorMessage = "Şifreniz en az 5 karakter olmalıdır.")]
        public string NewPassword { get; set; }
    }
}
