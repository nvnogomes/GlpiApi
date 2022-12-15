using MediatR;

namespace GLPIService.Application.Generics.Commands.GenericRequest {
    public class GenericRequestCommand : IGlpiRequest, IRequest<string> {

        public string Method { get; set; } = "";
        public object? Parameters { get; set; }

    }
}
