using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentitySample.Models
{
    public class ResetPasswordModel
    {
        [Display(Name = "密码")]
        [Required(ErrorMessage = "密码不能为空")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "确认密码")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "密码与确认密码不匹配。")]
        public string ConfirmPassword { get; set; }

        public string Email { get; set; }
        public string Token { get; set; }
    }
}
