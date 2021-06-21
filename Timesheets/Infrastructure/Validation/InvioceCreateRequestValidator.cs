using FluentValidation;
using Timesheets.Models.Dto;

namespace Timesheets.Infrastructure.Validation
{
    public class ContractCreateRequestValidator : AbstractValidator<ContractCreateRequest>
    {
        public ContractCreateRequestValidator()
        {
            RuleFor(x => x.Title).NotEmpty()
                .WithMessage(ValidationMessages.InvoiceContractIdError)
                .WithErrorCode(ValidationErrorCodes.InvoiceContractIdErrorCode);

            RuleFor(x => x.DateStart).LessThanOrEqualTo(x => x.DateEnd)
                .WithMessage(ValidationMessages.InvoiceDateStartError)
                .WithErrorCode(ValidationErrorCodes.InvoiceDateStartErrorCode);

            RuleFor(x => x.DateEnd).GreaterThanOrEqualTo(x => x.DateStart)
                .WithMessage(ValidationMessages.InvoiceDateEndError)
                .WithErrorCode(ValidationErrorCodes.InvoiceDateEndErrorCode);
        }
    }
}
