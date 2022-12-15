using MediatR;

namespace GLPIService.Application.Documents.Queries.GetDocument {


    public class GetDocumentQueryHandler : IRequestHandler<GetDocumentQuery, ExportFileVm> {

        private readonly IGlpiService _glpiService;

        public GetDocumentQueryHandler(IGlpiService glpiService) {
            _glpiService = glpiService;
        }


        public async Task<ExportFileVm> Handle(GetDocumentQuery request, CancellationToken cancellationToken) {

            var response = await _glpiService.GetDocument(request.DocumentId);

            if (response is not null) {
                var contents = await _glpiService.DownloadDocument(request.DocumentId);

                if (contents is not null) {
                    return new ExportFileVm {
                        Content = contents,
                        ContentType = response.Mime,
                        FileName = response.Filename
                    };
                }
            }

            throw new Exception("Error downloading file contents");
        }
    }
}
