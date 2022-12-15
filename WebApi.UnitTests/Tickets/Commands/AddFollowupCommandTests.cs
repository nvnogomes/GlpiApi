using FluentAssertions;
using GLPIService.Application.Tickets.Commands.AddFollowUp;

namespace WebApi.UnitTests.Tickets.Commands {

    using static Testing;

    public class AddFollowupCommandTests : TestBase {

        [Test]
        public async Task Success_FollowUpAdded() {
            // arrange

            var command = new AddFollowUpCommand {
                TicketId = 15961,
                Content = "Unit test content"
            };

            // act
            var response = await SendAsync(command);

            // assert
            response.Should().BePositive();
            response.Should().BeGreaterThanOrEqualTo(0);
        }

    }
}
