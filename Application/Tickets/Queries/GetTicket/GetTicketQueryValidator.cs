namespace GLPIService.Application.Tickets.Queries.GetTicket {

    public class GetTicketQueryValidator : GlpiRequestValidator<GetTicketQuery> {

        public GetTicketQueryValidator() {

            RuleFor(r => r.TicketId)
                .ValidIdentifier();

        }

    }
}
