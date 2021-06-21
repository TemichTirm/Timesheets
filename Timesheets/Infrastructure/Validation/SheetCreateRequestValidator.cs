using FluentValidation;
using Timesheets.Models.Dto;

namespace Timesheets.Infrastructure.Validation
{
    public class SheetCreateRequestValidator : AbstractValidator<SheetCreateRequest>
    {
        public SheetCreateRequestValidator()
        {
            RuleFor(x => x.Amount).InclusiveBetween(ValidationConstants.MinSheetAmount, ValidationConstants.MaxSheetAmount)
                .WithMessage(ValidationMessages.SheetAmountError)
                .WithErrorCode(ValidationErrorCodes.SheetAmountErrorCode);

            RuleFor(x => x.EmployeeId).NotEmpty()
                .WithMessage(ValidationMessages.SheetEmployeeIdError)
                .WithErrorCode(ValidationErrorCodes.SheetEmployeeIdErrorCode);

            RuleFor(x => x.ContractId).NotEmpty()
                .WithMessage(ValidationMessages.SheetContractIdError)
                .WithErrorCode(ValidationErrorCodes.SheetContractIdErrorCode);

            RuleFor(x => x.ServiceId).NotEmpty()
                .WithMessage(ValidationMessages.SheetServiceIdError)
                .WithErrorCode(ValidationErrorCodes.SheetServiceIdErrorCode);
        }
            
    }
}
