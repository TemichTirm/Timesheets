using FluentValidation;
using Timesheets.Models.Dto;

namespace Timesheets.Infrastructure.Validation
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Login).NotEmpty()
                .WithMessage(ValidationMessages.LoginEmptyError)
                .WithErrorCode(ValidationErrorCodes.LoginEmptyErrorCode);

            RuleFor(x => x.Password).NotEmpty()
                .WithMessage(ValidationMessages.PasswordEmptyError)
                .WithErrorCode(ValidationErrorCodes.PasswordEmptyErrorCode);
        }            
    }
}
