using CRUD_EF.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_EF.FluentValidator
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {           
            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("A valid email is required");

            RuleFor(user => user.FullName)
                .NotEmpty().WithMessage("FullName is repuired")
                .MinimumLength(2).WithMessage("FullName is at leat 2 characters")
                .MaximumLength(255).WithMessage("FullName can not over 255 characters");
            //  .Matches("^[a-zA-Z0-9 ]*$").WithMessage("No special characrers ");

            RuleFor(user => user.Age)
                .NotEmpty().WithMessage("Age is repuired")
                .Must(age => age > 0).WithMessage("Age must be greater than 0")
                .Must(age => age < 255).WithMessage("Age must be greater than 255");
            
        }
    }

}
