using FluentAssertions;
using GLPIService.Application.Tickets.Commands.CloseTicket;

namespace WebApi.UnitTests.Tickets.Commands {

    using static Testing;

    public class CloseTicketCommandTests : TestBase {


        [Test]
        public async Task Success_TicketClosed() {
            // arrange

            var command = new CloseTicketCommand {
                TicketId = 15961,
                Content = "Elvis left the building"
            };

            // act
            var response = await SendAsync(command);

            // assert
            response.Should().BePositive();
        }



    }
}
