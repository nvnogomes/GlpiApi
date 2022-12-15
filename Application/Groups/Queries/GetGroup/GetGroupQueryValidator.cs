namespace GLPIService.Application.Groups.Queries.GetGroup {

    public class GetGroupQueryValidator : GlpiRequestValidator<GetGroupQuery> {

        public GetGroupQueryValidator() {

            RuleFor(r => r.GroupId)
                .ValidIdentifier();
        }
    }
}
