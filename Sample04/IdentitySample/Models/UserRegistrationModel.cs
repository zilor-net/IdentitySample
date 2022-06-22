using System.ComponentModel.DataAnnotations;

namespace IdentitySample.Models
{
    public class UserRegistrationModel
    {
        [Display(Name = "姓氏")]
        public string FirstName { get; set; }

        [Display(Name = "名字")]
        public string LastName { get; set; }

        [Display(Name = "电子邮箱")]
        [Required(ErrorMessage = "电子邮箱不能为空")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "密码")]
        [Required(ErrorMessage = "密码不能为空")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "确认密码")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "密码与确认密码不匹配。")]
        public string ConfirmPassword { get; set; }
    }
}
