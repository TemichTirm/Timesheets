using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timesheets.Infrastructure.Validation
{
    /// <summary> Message strings for validation cases </summary>
    public static class ValidationMessages
    {
        public const string InvalidValue = "Invalid value";

        // Messages for SheetCreateRequest validation
        public const string SheetAmountError = "Amount should be between 1 and 8 hours";
        public const string SheetEmployeeIdError = "EmployeeID field can't be empty";
        public const string SheetContractIdError = "ContractId field can't be empty";
        public const string SheetServiceIdError = "ServiceId field can't be empty";

        // Messages for UserCreateRequest validation
        public const string UserUsernameEmptyError = "Username can't be empty";
        public const string UserPasswordEmptyError = "Password can't be empty";
        public const string UserPasswordLengthError = "Password length can't be less than 4 symbols";

        // Messages for LoginRequest validation
        public const string LoginEmptyError = "Login can't be empty";
        public const string PasswordEmptyError = "Password can't be empty";

        // Messages for InvoiceCreateRequest validation
        public const string InvoiceContractIdError = "ContractId field can't be empty";
        public const string InvoiceDateStartError = "Start date should be less than or equal to the end date";
        public const string InvoiceDateEndError = "End date should be greater than or equal to the start date";

        // Messages for EmployeeCreateRequest validation
        public const string EmployeeNameError = "Name can't be empty";
        public const string EmployeeUserIdError = "UserId field can't be empty";

        // Messages for ContractCreateRequest validation
        public const string ContractTitleError = "ContractId field can't be empty";
        public const string ContractDateStartError = "Start date should be less than or equal to the end date";
        public const string ContractDateEndError = "End date should be greater than or equal to the start date";

        // Messages for ServiceCreateRequest validation
        public const string ServiceNameError = "Service name can't be empty";
    }
}
