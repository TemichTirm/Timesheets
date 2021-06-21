using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timesheets.Infrastructure.Validation
{
    public static class ValidationErrorCodes
    {
        // SheetCreateRequest error codes ER-100
        public const string SheetAmountErrorCode = "ER-100.1";
        public const string SheetEmployeeIdErrorCode = "ER-100.2";
        public const string SheetContractIdErrorCode = "ER-100.3";
        public const string SheetServiceIdErrorCode = "ER-100.4";

        // UserCreateRequest error codes ER-200
        public const string UserUsernameEmptyErrorCode = "ER-200.1";
        public const string UserPasswordEmptyErrorCode = "ER-200.2";
        public const string UserPasswordLengthErrorCode = "ER-200.3";

        // LoginRequest error codes ER-300
        public const string LoginEmptyErrorCode = "ER-300.1";
        public const string PasswordEmptyErrorCode = "ER-300.2";

        // InvoiceCreateRequest error codes ER-400
        public const string InvoiceContractIdErrorCode = "ER-400.1";
        public const string InvoiceDateStartErrorCode = "ER-400.2";
        public const string InvoiceDateEndErrorCode = "ER-400.3";

        // EmployeeCreateRequest error codes ER-500
        public const string EmployeeNameErrorCode = "ER-500.1";
        public const string EmployeeUserIdErrorCode = "ER-500.2";

        // ContractCreateRequest error codes ER-600
        public const string ContractContractIdErrorCode = "ER-600.1";
        public const string ContractDateStartErrorCode = "ER-600.2";
        public const string ContractDateEndErrorCode = "ER-600.3";

        // ServiceCreateRequest error codes ER-700
        public const string ServiceNameErrorCode = "ER-700.1";
    }
}
