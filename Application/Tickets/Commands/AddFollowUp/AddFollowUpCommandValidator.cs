using FluentValidation;

namespace GLPIService.Application.Tickets.Commands.AddFollowUp {

    public class AddFollowUpCommandValidator : GlpiRequestValidator<AddFollowUpCommand> {

        public AddFollowUpCommandValidator() {

            RuleFor(r => r.TicketId)
                .ValidIdentifier();

            RuleFor(r => r.Content)
                .NotEmpty();
        }

    }
}
