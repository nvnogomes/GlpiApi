using FluentValidation;

namespace GLPIService.Application.Common.Validators {
    public static class IdentifierValidator {


        public static IRuleBuilderOptions<T, int> ValidIdentifier<T>(this IRuleBuilder<T, int> ruleBuilder) {
            return ruleBuilder
                    .NotEmpty()
                    .GreaterThan(0)
                        .WithMessage("Invalid identifier.");
        }

        public static IRuleBuilderOptions<T, int?> ValidIdentifier<T>(this IRuleBuilder<T, int?> ruleBuilder) {

            return (IRuleBuilderOptions<T, int?>)ruleBuilder.Custom((obj, context) => {

                if (obj.HasValue && obj.Value <= 0) {
                    context.AddFailure("Invalid identifier.");
                }

            });
        }


        // LONG
        public static IRuleBuilderOptions<T, long> ValidIdentifier<T>(this IRuleBuilder<T, long> ruleBuilder) {
            return ruleBuilder
                    .NotEmpty()
                    .GreaterThan(0)
                        .WithMessage("Invalid identifier.");
        }

        public static IRuleBuilderOptions<T, long?> ValidIdentifier<T>(this IRuleBuilder<T, long?> ruleBuilder) {

            return (IRuleBuilderOptions<T, long?>)ruleBuilder.Custom((obj, context) => {

                if (obj.HasValue && obj.Value <= 0) {
                    context.AddFailure("Invalid identifier.");
                }

            });
        }

    }
}
