using Bored.GameService.GameSession;
using Moq;
using NUnit.Framework;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bored.GameService.Test.GameSession
{
    [TestFixture]
    public class GameSessionContextTests
    {
        private Mock<IConnectionMultiplexer> multiplexerMock;
        private Mock<IDatabase> dbMock;

        [SetUp]
        public void Setup()
        {
            multiplexerMock = new Mock<IConnectionMultiplexer>();
            dbMock = new Mock<IDatabase>();
            multiplexerMock.Setup(x => x.GetDatabase(It.IsAny<int>(), It.IsAny<object>())).Returns(dbMock.Object);
        }

        [TestCase("game1", "Here is the state")]
        [TestCase(null, null)]
        public void GetGameStateTest(string gameID, string state)
        {
            // Arrange
            dbMock.Setup(d => d.StringGet(gameID, It.IsAny<CommandFlags>())).Returns(state);
            GameSessionContext context = new GameSessionContext(multiplexerMock.Object);

            // Act
            var result = context.GetGameState(gameID);

            // Assert
            Assert.AreEqual(state, result);
            dbMock.Verify(mock => mock.StringGet(gameID, It.IsAny<CommandFlags>()), Times.Once());
        }

        [Test]
        public void GetNonExistantGameStateTest()
        {
            // Arrange
            string gameID = "game2";
            GameSessionContext context = new GameSessionContext(multiplexerMock.Object);

            // Act
            var result = context.GetGameState(gameID);

            // Assert
            Assert.AreEqual(null, result);
            dbMock.Verify(mock => mock.StringGet(gameID, It.IsAny<CommandFlags>()), Times.Once());
        }

        [Test]
        public void AddGameStateTest()
        {

        }
    }
}
