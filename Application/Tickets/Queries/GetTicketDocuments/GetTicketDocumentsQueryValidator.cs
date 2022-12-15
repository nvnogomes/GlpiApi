namespace GLPIService.Application.Tickets.Queries.GetTicketDocuments {
    public class GetTicketDocumentsQueryValidator : GlpiRequestValidator<GetTicketDocumentsQuery> {

        public GetTicketDocumentsQueryValidator() {

            RuleFor(r => r.TicketId)
                .ValidIdentifier();

        }
    }
}
