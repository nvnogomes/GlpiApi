namespace GLPIService.Application.Common.Vms {
    public class TicketVm {

        public TicketDto Ticket { get; set; } = new();

        public List<DocumentDto> Documents { get; set; } = new();

        public List<FollowUpDto> Followups { get; set; } = new();

    }
}
