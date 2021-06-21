using FluentValidation;
using FluentValidation.Results;
using Timesheets.Domain.Interfaces;
using Timesheets.Models.Dto;

namespace Timesheets.Infrastructure.Validation
{
    public class UserCreateRequestValidator : AbstractValidator<UserCreateRequest>
    {
        public UserCreateRequestValidator()
        {
            RuleFor(x => x.Username).NotEmpty()
                .WithMessage(ValidationMessages.UserUsernameEmptyError)
                .WithErrorCode(ValidationErrorCodes.UserUsernameEmptyErrorCode);

            RuleFor(x => x.Password).NotEmpty()
                .WithMessage(ValidationMessages.UserPasswordEmptyError)
                .WithErrorCode(ValidationErrorCodes.UserPasswordEmptyErrorCode);
            
            RuleFor(x => x.Password)
                .MinimumLength(ValidationConstants.MinUserPasswordLength)
                .When(x => x.Password.Length != 0)
                .WithMessage(ValidationMessages.UserPasswordLengthError)
                .WithErrorCode(ValidationErrorCodes.UserPasswordLengthErrorCode);                
        }            
    }
}
