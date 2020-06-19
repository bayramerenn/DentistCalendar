using System.ComponentModel.DataAnnotations;

namespace DentistCalendar.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Lütfen kullanıcı adınızı belirtiniz")]
        [Display(Name = "Kullancı Adınız: ")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Lütfen parolanızı belirtiniz")]
        [Display(Name = "Parolanız: ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Display(Name = "Beni Hatırla:")]
        public bool RememberMe { get; set; }
    }
}