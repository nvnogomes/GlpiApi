using FluentValidation;
using System.Text.RegularExpressions;

namespace GLPIService.Application.Common.Validators {
    public static class FileValidator {

        public static IRuleBuilderOptions<T, IFormFile> ValidDocument<T>(this IRuleBuilder<T, IFormFile> ruleBuilder) {         

            return ruleBuilder
                .NotEmpty()
                .Must((docObj) => {
                    return docObj.Length > 0;
                })
                    .WithMessage("Document is empty (No length)")
                .Must((docObj) => {
                    return docObj.FileName.Length < 512;
                })
                    .WithMessage("The file name is too long.")
                ;
        }

        public static IRuleBuilderOptions<T, IFormFile> PdfFormat<T>(this IRuleBuilder<T, IFormFile> ruleBuilder) {
            return ruleBuilder
                .Must((docObj) => {

                    return docObj.FileName.ToLower().EndsWith("pdf");

                })
                .WithMessage("Document must be in pdf format.");
        }

        public static IRuleBuilderOptions<T, IFormFile> ExcelFormat<T>(this IRuleBuilder<T, IFormFile> ruleBuilder) {
            return ruleBuilder
                .Must((docObj) => {
                    return docObj.FileName.ToLower().EndsWith("xlsx")
                        || docObj.FileName.ToLower().EndsWith("xls");
                })
                .WithMessage("Document must be in xls or xlsx format.");
        }

        public static IRuleBuilderOptions<T, IFormFile> ExpenseFilename<T>(this IRuleBuilder<T, IFormFile> ruleBuilder) {
            return ruleBuilder
                .Must((docObj) => {
                    var pattern = @"(C-|R-){1}\d+.*\.pdf$";
                    var regex = new Regex(pattern);

                    return regex.IsMatch(docObj.FileName);
                })
                .WithMessage("Invalid document name.");
        }
    }
}
