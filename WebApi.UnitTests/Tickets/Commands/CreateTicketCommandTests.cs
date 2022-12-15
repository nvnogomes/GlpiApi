using FluentAssertions;
using GLPIService.Application.Commands.CreateTicket;
using GLPIService.Application.Common.Enums;

namespace WebApi.UnitTests.Tickets.Commands {

    using static Testing;

    public class CreateTicketCommandTests : TestBase {


        [Test]
        public async Task Success_TicketCreated() {
            // arrange

            var command = new CreateTicketCommand {
                Title = "GLPI isnt accepting documents",
                Content = "Keeps replying with 'Error on request'",
                Category = 484,
                Type = (int)TicketType.Incident
            };

            // act
            var response = await SendAsync(command);

            // assert
            response.Should().BePositive();
        }



    }
}
