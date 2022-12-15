namespace GLPIService.Application.Tickets.Commands.AddTicketUser {

    public class AddTicketUserCommandValidator : GlpiRequestValidator<AddTicketUserCommand> {

        public AddTicketUserCommandValidator() {

            RuleFor(r => r.TicketId)
                .ValidIdentifier();

            RuleFor(r => r.UserId)
                .ValidIdentifier();

            RuleFor(r => r.TypeId)
                .ValidIdentifier();


        }


    }
}
