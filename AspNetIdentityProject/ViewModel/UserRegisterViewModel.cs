using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetIdentityProject.ViewModel
{
    public class UserRegisterViewModel
    {
        [Required(ErrorMessage ="Kullanıcı ismi boş bırakılamaz.")]
        [Display(Name ="Kullanıcı Adı")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Mail adresi  boş bırakılamaz.")]
        [Display(Name = "Mail Adresi")]
        [EmailAddress(ErrorMessage ="Email adresi doğru formatta değil.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Telefon Numarası boş bırakılamaz.")]
        [Display(Name = "Telefon Numarası")]
        [Phone(ErrorMessage ="Telefon numarası doğru formatta değil.")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Şifre boş bırakılamaz.")]
        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
