using System.ComponentModel.DataAnnotations;

namespace DentistCalendar.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Lütfen kullanıcı adınızı belirtiniz")]
        [Display(Name = "Kullancı Adınız: ")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Lütfen adınızı belirtiniz")]
        [Display(Name = "Adınız: ")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Lütfen soyadınız belirtiniz")]
        [Display(Name = "Soyadınız: ")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Lütfen parolanızı belirtiniz")]
        [Display(Name = "Parolanız: ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Lütfen emailiiniz belirtiniz")]
        [Display(Name = "Emailiniz: ")]
        [EmailAddress(ErrorMessage = "Lütfen email adresinizi kontrol ediniz")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lütfen randevu rengini belirtiniz")]
        [Display(Name = "Randevu Rengi :")]
        public string Color { get; set; }


        [Display(Name = "Doktorum")]
        public bool IsDentist { get; set; }
    }
}
