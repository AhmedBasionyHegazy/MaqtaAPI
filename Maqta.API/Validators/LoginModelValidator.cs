using FluentValidation;
using MasMasr.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maqta.API.Validators
{
    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        public LoginModelValidator()
        {
            RuleFor(p => p.Email).EmailAddress().WithMessage("Enter Email In Correct Format");
            RuleFor(p => p.Password).MaximumLength(20).MinimumLength(7).WithMessage("Password Minimum Length 7 and Maximum Length 20");
        }
    }
}
