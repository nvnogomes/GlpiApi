using FluentAssertions;
using GLPIService.Application.Tickets.Commands.AddDocument;

namespace WebApi.UnitTests.Tickets.Commands {

    using static Testing;

    public class AddDocumentCommandTests : TestBase {


        [Test]
        public async Task Success_DocumentAdded() {
            // arrange

            var command = new AddDocumentCommand {
                TicketId = 15916,
                Document = FileMOQ_Pdf
            };

            // act
            var response = await SendAsync(command);

            // assert
            response.Should().BePositive();
        }



    }
}
