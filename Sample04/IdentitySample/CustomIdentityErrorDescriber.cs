using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace IdentitySample
{
    public class CustomIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError PasswordTooShort(int length)
        {
            return new()
            {
                Code = nameof(PasswordTooShort),
                Description = $"密码至少需要 {length} 位"
            };
        }

        public override IdentityError DuplicateEmail(string email)
        {
            return new()
            {
                Code = nameof(DuplicateUserName), 
                Description = $"{email} 已存在。"
            };
        }

        public override IdentityError DuplicateUserName(string username)
        {
            return new()
            {
                Code = nameof(DuplicateUserName), 
                Description = $"{username} 已存在。"
            };
        }
    }
}
