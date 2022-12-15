using FluentAssertions;
using GLPIService.Application.Common.Dtos;
using GLPIService.Application.Common.Exceptions;
using GLPIService.Application.Common.Interfaces;
using GLPIService.Application.Documents.Queries.GetDocument;
using Moq;
using System.Text;

namespace Application.UnitTests.Documents.Queries {
    public class GetDocumentQueryTests : TestBase {


        [Test]
        public async Task Error_Exception() {
            // arrange
            var moqGlpi = new Mock<IGlpiService> {
                CallBase = true
            };
            moqGlpi.Setup(g => g.GetDocument(It.IsAny<int>()))
                .ThrowsAsync(new GlpiException("Boo boo"));

            byte[]? empty = null;
            moqGlpi.Setup(g => g.DownloadDocument(It.IsAny<int>()))
                .ReturnsAsync(empty!);

            var handler = new GetDocumentQueryHandler(moqGlpi.Object);
            var command = new GetDocumentQuery {
                DocumentId = 13445
            };

            // act
            async Task f() => await handler!.Handle(command!, CancellationToken.None);

            // assert
            await FluentActions.Invoking(f).Should().ThrowAsync<Exception>();
        }


        [Test]
        public async Task Success_DocumentRetrieved() {
            // arrange
            var responseDto = new DocumentDto {
                Id = 115,
                Date_Creation = new DateTime(2022, 9, 26),
                Filename = "Shihiro.img",
            };
            var contents = Encoding.ASCII.GetBytes("Dummy content");
            var moqGlpi = new Mock<IGlpiService> {
                CallBase = false
            };
            moqGlpi.Setup(g => g.GetDocument(It.IsAny<int>()))
                .ReturnsAsync(responseDto);

            moqGlpi.Setup(g => g.DownloadDocument(It.IsAny<int>()))
                .ReturnsAsync(contents);

            var handler = new GetDocumentQueryHandler(moqGlpi.Object);
            var command = new GetDocumentQuery {
                DocumentId = 13445
            };

            // act
            var response = await handler.Handle(command, CancellationToken.None);

            // assert
            response.Should().NotBeNull();
            response.Content.Should().BeEquivalentTo(contents);
            response.FileName.Should().Be(responseDto.Filename);
        }



    }
}
