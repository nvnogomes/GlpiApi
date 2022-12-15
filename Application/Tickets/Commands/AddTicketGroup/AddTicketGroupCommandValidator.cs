namespace GLPIService.Application.Tickets.Commands.AddTicketGroup {

    public class AddTicketGroupCommandValidator : GlpiRequestValidator<AddTicketGroupCommand> {

        public AddTicketGroupCommandValidator() {

            RuleFor(r => r.TicketId)
                .ValidIdentifier();

            RuleFor(r => r.GroupId)
                .ValidIdentifier();

            RuleFor(r => r.TypeId)
                .ValidIdentifier();

        }
    }
}
