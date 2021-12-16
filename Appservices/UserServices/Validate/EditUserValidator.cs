using AppServices.UserServices.DTO;
using EFCore.DbConnection;
using EFCore.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppServices.UserServices.Validate
{
    public class EditUserValidator : AbstractValidator<User>
    {
        private readonly UserContext _userContext;

        public EditUserValidator(UserContext userContext)
        {
            _userContext = userContext;

            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("A valid email is required");

            RuleFor(user => user.FullName)
                .NotEmpty().WithMessage("FullName is repuired")
                .MinimumLength(2).WithMessage("FullName is at leat 2 characters")
                .MaximumLength(255).WithMessage("FullName can not over 255 characters")
                .Must(fullname => string.IsNullOrEmpty(GetValidName(fullname)))
                .WithMessage("FullName is exist");


            RuleFor(user => user.Age)
                .NotEmpty().WithMessage("Age is repuired")
                .Must(age => age > 0).WithMessage("Age must be greater than 0")
                .Must(age => age < 255).WithMessage("Age must be greater than 255");
        }
        public string GetValidName(string fullname)
        {
            var user = _userContext.Users
                        .Where(u => (u.FullName.Trim(' ').ToLower() == fullname.Trim(' ').ToLower()))
                        .FirstOrDefault();
            if (user != null)
            {
                return user.FullName;
            }
            else
            {
                return null;
            }
        }
        public bool IsValidNameEdit(Guid id, string fullname)
        {

            var user = _userContext.Users
                        .Where(u => (u.FullName.Trim(' ').ToLower() == fullname.Trim(' ').ToLower()))
                        .FirstOrDefault();
            if (user == null || user.Id.Equals(id))
            {
                return true;
            }
            else
            {
                return false;
            };
        }

    }
}
