namespace GLPIService.Application.Tickets.Commands.AddDocument {

    public class AddDocumentCommandValidator : GlpiRequestValidator<AddDocumentCommand> {

        public AddDocumentCommandValidator() {

            RuleFor(r => r.TicketId)
                .ValidIdentifier();

            RuleFor(r => r.Document)
                .ValidDocument();

        }

    }
}
