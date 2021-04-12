using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nouwan.Smeuj.Api.Handlers;
using Nouwan.Smeuj.UnitTests.Common;

namespace Nouwan.Smeuj.Api.Tests
{
    [TestClass]
    public class AddMessageRequestTests
    {
        [TestMethod]
        public void Add_WithMessage_ExpectMessageAdded()
        {
            //arrange
            var message = TestDataFactory.CreateMessage();

            //act
            var request = new AddMessageRequest(message);

            //assert
            request.Message.Should().Be(message);
        }
    }
}
