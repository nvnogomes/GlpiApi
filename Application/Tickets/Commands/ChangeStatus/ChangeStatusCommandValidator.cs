using FluentValidation;

namespace GLPIService.Application.Tickets.Commands.ChangeStatus {
    public class ChangeStatusCommandValidator : GlpiRequestValidator<ChangeStatusCommand> {

        public ChangeStatusCommandValidator() {

            RuleFor(r => r.TicketId)
                .ValidIdentifier();

            RuleFor(r => r.Status)
                .IsInEnum();
        }

    }
}
