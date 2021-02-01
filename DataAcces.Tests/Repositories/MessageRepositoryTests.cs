using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nouwan.Smeuj.DataAccess.Repositories;
using Nouwan.Smeuj.Domain;

namespace Nouwan.Smeuj.DataAccess.Tests.Repositories
{
    [TestClass]
    public class MessageRepositoryTests
    {
        private const int MessageId = 3;
        private readonly DateTime dateTime = DateTime.Now;
        private readonly CancellationToken cancellationToken = new CancellationToken(); 
        private readonly IMessageRepository repository
            = new MessageRepository(DbTestHelper.GetConnectionFactory());

        [TestInitialize]
        public void TestInit()
        {
            DbTestHelper.ClearDatabase();
        }

        [TestMethod, TestCategory(TestCategories.DataTestCategory)]
        public async Task Add_WithMessage_ExpectMessageAdded()
        {
            //arrange
            var authorId = DbTestHelper.AddAuthor(TestDataFactory.CreateAuthor());
            var message = new Message(authorId, dateTime, MessageId);

            //act
            var result = await repository.Add(message, cancellationToken);

            //assert
            var results = DbTestHelper.GetAllMessages();
            var expectedResult = results.Single();
            result.Payload.Should().NotBeNull();
            result.Payload?.Id.Should().Be(expectedResult.Id);
            result.Payload?.AuthorId.Should().Be(expectedResult.AuthorId);
            result.Payload?.MessageId.Should().Be(expectedResult.MessageId);
            result.Payload?.SendOn.Should().BeCloseTo(expectedResult.SendOn);
        }

        [TestMethod, TestCategory(TestCategories.DataTestCategory)]
        public async Task Add_WithMessageAndExistingRecords_ExpectMessageAdded()
        {
            //arrange
            var authorId = DbTestHelper.AddAuthor(TestDataFactory.CreateAuthor());
            var messageToInsert = TestDataFactory.CreateMessage(authorId);
            var existingMessage = messageToInsert with { Id = DbTestHelper.AddMessage(messageToInsert) };
            var message = new Message(authorId, dateTime, MessageId);

            //act
            var result = await repository.Add(message, cancellationToken);

            //assert
            var results = DbTestHelper.GetAllMessages();
            results.Count.Should().Be(2);
            var expectedResult = results.Single(row => row.Id == result.Payload?.Id);
            var expectedNotResult = results.Single(row => row.Id != result.Payload?.Id);

            if (result.Payload == null) throw new AssertionFailedException("Payload should not be null");
            ShouldBeTheSameMessage(result.Payload,expectedResult);
            ShouldBeTheSameMessage(expectedNotResult,existingMessage);
        }

        private static void ShouldBeTheSameMessage(Message result, Message expected)
        {
            result.Should().NotBeNull();
            result.Id.Should().Be(expected.Id);
            result.AuthorId.Should().Be(expected.AuthorId);
            result.MessageId.Should().Be(expected.MessageId);
            result.SendOn.Should().BeCloseTo(expected.SendOn);
        }
    }
}