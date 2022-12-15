using FluentAssertions;
using GLPIService.Application.Common.Exceptions;
using GLPIService.Application.Common.Interfaces;
using GLPIService.Application.Tickets.Commands.AddDocument;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Application.UnitTests.Documents.Commands {
    public class AddDocumentCommandTests : TestBase {

        [Test]
        public async Task Error_ExceptionThrown() {
            // arrange
            var moqGlpi = new Mock<IGlpiService> {
                CallBase = true
            };
            moqGlpi.Setup(g => g.AddTicketDocument(It.IsAny<int>(), It.IsAny<IFormFile>()))
                .ThrowsAsync(new GlpiException("Generic exception"));

            var handler = new AddDocumentCommandHandler(moqGlpi.Object);
            var command = new AddDocumentCommand {
                TicketId = 13450,
                Document = FileMOQ_Pdf
            };

            // act
            async Task f() => await handler!.Handle(command!, CancellationToken.None);

            // assert
            await FluentActions.Invoking(f).Should().ThrowAsync<GlpiException>();
        }


        [Test]
        public async Task Success_DocumentAdded() {
            // arrange
            var moqGlpi = new Mock<IGlpiService> {
                CallBase = true
            };
            moqGlpi.Setup(g => g.AddTicketDocument(It.IsAny<int>(), It.IsAny<IFormFile>()))
                .ReturnsAsync(155);

            var handler = new AddDocumentCommandHandler(moqGlpi.Object);
            var command = new AddDocumentCommand {
                TicketId = 13446,
                Document = FileMOQ_Pdf
            };

            // act
            var response = await handler.Handle(command, CancellationToken.None);

            // assert
            response.Should().BePositive();
        }

    }
}
