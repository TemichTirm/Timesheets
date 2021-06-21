using FluentValidation;
using Timesheets.Models.Dto;

namespace Timesheets.Infrastructure.Validation
{
    public class EmployeeCreateRequestValidator : AbstractValidator<EmployeeCreateRequest>
    {
        public EmployeeCreateRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                .WithMessage(ValidationMessages.EmployeeNameError)
                .WithErrorCode(ValidationErrorCodes.EmployeeNameErrorCode);

            RuleFor(x => x.UserId).NotEmpty()
                .WithMessage(ValidationMessages.EmployeeUserIdError)
                .WithErrorCode(ValidationErrorCodes.EmployeeUserIdErrorCode);
        }
    }
}
