using MediatR;

namespace GLPIService.Application.Groups.Queries.GetGroup {

    public class GetGroupQuery : IGlpiRequest, IRequest<GroupDto> {

        public int GroupId { get; set; }

    }
}
