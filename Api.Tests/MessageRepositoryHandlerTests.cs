using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nouwan.Smeuj.Api.Handlers;
using Nouwan.Smeuj.DataAccess.Repositories;
using Nouwan.Smeuj.Domain;
using Nouwan.Smeuj.Framework;
using Nouwan.Smeuj.UnitTests.Common;
using NSubstitute;

namespace Nouwan.Smeuj.Api.Tests
{
    [TestClass]
    public class MessageRepositoryHandlerTests
    {
        private readonly IMessageRepository messageRepositoryMock = Substitute.For<IMessageRepository>();
        private AddMessageHandler messageHandler = null!;

        [TestInitialize]
        public void TestInit()
        {
            messageHandler = new AddMessageHandler(messageRepositoryMock);
        }

        [TestMethod]
        public async Task Add_WithMessage_ExpectMessageAdded()
        {
            //arrange
            var requestMessage = TestDataFactory.CreateMessage(id:0);
            var insertedMessage = TestDataFactory.CreateMessage(id:42);
            var cancellationToken = new CancellationToken();
            messageRepositoryMock.Add(requestMessage, cancellationToken)
                .Returns(Result<Message>.Ok(insertedMessage));
            var request = new AddMessageRequest(requestMessage);

            //act
            var result = await messageHandler.Handle(request, cancellationToken);

            //assert
            result.Payload.Should().Be(insertedMessage);
        }

    }
}
