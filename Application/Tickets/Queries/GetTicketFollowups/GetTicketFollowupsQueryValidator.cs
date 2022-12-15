namespace GLPIService.Application.Tickets.Queries.GetTicketFollowups {
    public class GetTicketFollowupsQueryValidator : GlpiRequestValidator<GetTicketFollowupsQuery> {

        public GetTicketFollowupsQueryValidator() {

            RuleFor(r => r.TicketId)
                .ValidIdentifier();

        }
    }
}
