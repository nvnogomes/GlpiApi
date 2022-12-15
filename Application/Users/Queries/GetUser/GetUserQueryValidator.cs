namespace GLPIService.Application.Users.Queries.GetUser {

    public class GetUserQueryValidator : GlpiRequestValidator<GetUserQuery> {

        public GetUserQueryValidator() {

            RuleFor(r => r.UserId)
                .ValidIdentifier();
        }
    }
}
