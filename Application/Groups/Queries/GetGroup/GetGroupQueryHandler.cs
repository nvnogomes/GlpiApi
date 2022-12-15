using MediatR;

namespace GLPIService.Application.Groups.Queries.GetGroup {

    public class GetGroupQueryHandler : IRequestHandler<GetGroupQuery, GroupDto> {

        private readonly IGlpiService _glpiService;

        public GetGroupQueryHandler(IGlpiService glpiService) {
            _glpiService = glpiService;
        }


        public async Task<GroupDto> Handle(GetGroupQuery request, CancellationToken cancellationToken) {

            return await _glpiService.GetGroup(request.GroupId);
        }
    }
}
