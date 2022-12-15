namespace GLPIService.Application.Documents.Commands.UploadDocument;

public class UploadDocumentCommandValidator : GlpiRequestValidator<UploadDocumentCommand> {

    public UploadDocumentCommandValidator() {

        RuleFor(r => r.Document)
            .ValidDocument();

        RuleFor(r => r.TicketId)
            .ValidIdentifier();

    }

}

