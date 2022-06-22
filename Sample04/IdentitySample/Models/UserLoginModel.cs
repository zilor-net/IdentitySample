using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentitySample.Models
{
    public class UserLoginModel
    {
        [Display(Name = "电子邮箱")]
        [Required(ErrorMessage = "电子邮箱不能为空")]
        [EmailAddress(ErrorMessage = "电子邮箱格式不正确")]
        public string Email { get; set; }

        [Display(Name = "密码")]
        [Required(ErrorMessage = "密码不能为空")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "记住账号")]
        public bool RememberMe { get; set; }
    }
}
