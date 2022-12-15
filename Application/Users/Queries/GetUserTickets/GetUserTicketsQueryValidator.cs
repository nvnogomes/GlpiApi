namespace GLPIService.Application.Users.Queries.GetUserTickets {

    public class GetUserTicketsQueryValidator : GlpiRequestValidator<GetUserTicketsQuery> {


        public GetUserTicketsQueryValidator() {

            RuleFor(r => r.UserId)
                .ValidIdentifier();

        }

    }
}
