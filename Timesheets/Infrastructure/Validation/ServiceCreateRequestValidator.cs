using FluentValidation;
using Timesheets.Models.Dto;

namespace Timesheets.Infrastructure.Validation
{
    public class ServiceCreateRequestValidator : AbstractValidator<ServiceCreateRequest>
    {
        public ServiceCreateRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                .WithMessage(ValidationMessages.ServiceNameError)
                .WithErrorCode(ValidationErrorCodes.ServiceNameErrorCode);
        }            
    }
}
