namespace GLPIService.Application.Documents.Queries.GetDocument {

    public class GetDocumentQueryValidator : GlpiRequestValidator<GetDocumentQuery> {

        public GetDocumentQueryValidator() {

            RuleFor(r => r.DocumentId)
                .ValidIdentifier();

        }


    }
}
