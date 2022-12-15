using FluentValidation;

namespace GLPIService.Application.Common.Validators {
    public class GlpiRequestValidator<T> : AbstractValidator<T> where T : IGlpiRequest {

        public GlpiRequestValidator() {
            ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;
            ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;
        }


    }
}
