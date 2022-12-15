using FluentValidation;

namespace GLPIService.Application.Commands.CreateTicket {

    public class CreateTicketCommandValidator : GlpiRequestValidator<CreateTicketCommand> {

        public CreateTicketCommandValidator() {

            RuleFor(r => r.Title)
                .NotEmpty();

            RuleFor(r => r.Content)
                .NotEmpty();

            RuleFor(r => r.Type)
                .ValidIdentifier();

            RuleFor(r => r.Category)
                .ValidIdentifier();

            RuleFor(r => r.AssignedToGroup)
                .NotEmpty();

            RuleForEach(r => r.AssignedToGroup)
                .ValidIdentifier();

            RuleFor(r => r.EntityId)
                .ValidIdentifier();

        }
    }
}
