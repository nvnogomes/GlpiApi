namespace Application.UnitTests.Groups.Queries {

    public class GetGroupQueryTests : TestBase {

        //[Test]
        //public async Task Error_ErrorFetchingDocumentInfo() {
        //    // arrange
        //    var responseDto = new GlpiResponseDto {
        //        Id = 0,
        //        Message = "Something bad happened",
        //        Successful = false
        //    };
        //    var moqGlpi = new Mock<IGlpiService> {
        //        CallBase = true
        //    };
        //    moqGlpi.Setup(g => g.GetDocument(It.IsAny<int>()))
        //        .ReturnsAsync(responseDto);

        //    var handler = new GetDocumentQueryHandler(moqGlpi.Object);
        //    var command = new GetDocumentQuery {
        //        DocumentId = 13445
        //    };

        //    // act
        //    async Task f() => await handler!.Handle(command!, CancellationToken.None);

        //    // assert
        //    await FluentActions.Invoking(f).Should().ThrowAsync<Exception>();
        //}

        //[Test]
        //public async Task Error_ErrorFetchingDocumentContent() {
        //    // arrange
        //    var responseDto = new GlpiResponseDto {
        //        Id = 100,
        //        Message = JObject.FromObject(new DocumentDto()).ToString(),
        //        Successful = true
        //    };
        //    var moqGlpi = new Mock<IGlpiService> {
        //        CallBase = true
        //    };
        //    moqGlpi.Setup(g => g.GetDocument(It.IsAny<int>()))
        //        .ReturnsAsync(responseDto);

        //    byte[]? empty = null;
        //    moqGlpi.Setup(g => g.DownloadDocument(It.IsAny<int>()))
        //        .ReturnsAsync(empty!);

        //    var handler = new GetDocumentQueryHandler(moqGlpi.Object);
        //    var command = new GetDocumentQuery {
        //        DocumentId = 13445
        //    };

        //    // act
        //    async Task f() => await handler!.Handle(command!, CancellationToken.None);

        //    // assert
        //    await FluentActions.Invoking(f).Should().ThrowAsync<Exception>();
        //}


        //[Test]
        //public async Task Success_DocumentRetrieved() {
        //    // arrange
        //    var responseDto = new GlpiResponseDto {
        //        Id = 100,
        //        Message = JObject.FromObject(new DocumentDto()).ToString(),
        //        Successful = true
        //    };
        //    var moqGlpi = new Mock<IGlpiService> {
        //        CallBase = false
        //    };
        //    moqGlpi.Setup(g => g.GetDocument(It.IsAny<int>()))
        //        .ReturnsAsync(responseDto);


        //    moqGlpi.Setup(g => g.DownloadDocument(It.IsAny<int>()))
        //        .ReturnsAsync(Encoding.ASCII.GetBytes("Dummy content"));

        //    var handler = new GetDocumentQueryHandler(moqGlpi.Object);
        //    var command = new GetDocumentQuery {
        //        DocumentId = 13445
        //    };

        //    // act
        //    var response = await handler.Handle(command, CancellationToken.None);

        //    // assert
        //    response.Should().NotBeNull();
        //}


    }
}
