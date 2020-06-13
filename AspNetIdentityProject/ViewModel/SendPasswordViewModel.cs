using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetIdentityProject.ViewModel
{
    public class SendPasswordViewModel
    {
        [Required(ErrorMessage = "Email alanı gereklidir.")]
        [Display(Name = "Email Adresiniz")]
        [EmailAddress]
        [DataType(DataType.Password)]
        public string Email { get; set; }
       
    }
}
