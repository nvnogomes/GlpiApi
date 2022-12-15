namespace GLPIService.Application.Groups.Queries.GetGroupTickets {

    public class GetGroupTicketsQueryValidator : GlpiRequestValidator<GetGroupTicketsQuery> {


        public GetGroupTicketsQueryValidator() {

            RuleFor(r => r.GroupId)
                .ValidIdentifier();
        }
    }
}
