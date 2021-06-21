using FluentValidation;
using Timesheets.Models.Dto;

namespace Timesheets.Infrastructure.Validation
{
    public class InvioceCreateRequestValidator : AbstractValidator<InvoiceCreateRequest>
    {
        public InvioceCreateRequestValidator()
        {
            RuleFor(x => x.ContractId).NotEmpty()
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
