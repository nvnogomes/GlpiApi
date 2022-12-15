using FluentValidation;

namespace GLPIService.Application.Tickets.Commands.CloseTicket {

    public class CloseTicketCommandValidator : GlpiRequestValidator<CloseTicketCommand> {


        public CloseTicketCommandValidator() {

            RuleFor(r => r.TicketId)
                .ValidIdentifier();

            RuleFor(r => r.Content)
                .NotEmpty();

        }
    }
}
