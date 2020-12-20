using Bored.GameService.GameSession;
using Moq;
using Newtonsoft.Json;
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

        [TestCase("game", "Here is the state")]
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
            string gameID = "game";
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
            // Arrange
            string gameID = "game";
            object state = new { Winner = "Player 1", Board = new int[3, 3] { { 1, -1, 0 }, { 1, -1, 0 }, { 1, 0, -1 } } };
            string serializedState = JsonConvert.SerializeObject(state);
            dbMock.Setup(d => d.StringSet(gameID, serializedState, null, When.Always, CommandFlags.None)).Returns(true);
            GameSessionContext context = new GameSessionContext(multiplexerMock.Object);

            // Act
            var result = context.AddGameState(gameID, state);

            // Assert
            Assert.AreEqual(state, result);
            dbMock.Verify(mock => mock.StringSet(gameID, serializedState, null, When.Always, CommandFlags.None), Times.Once());
        }

        [Test]
        public void FailedAddGameStateTest()
        {
            // Arrange
            string gameID = "game";
            object state = new { Winner = "Player 1", Board = new int[3, 3] { { 1, -1, 0 }, { 1, -1, 0 }, { 1, 0, -1 } } };
            string serializedState = JsonConvert.SerializeObject(state);
            dbMock.Setup(d => d.StringSet(gameID, serializedState, null, When.Always, CommandFlags.None)).Returns(false);
            GameSessionContext context = new GameSessionContext(multiplexerMock.Object);

            // Act
            var result = context.AddGameState(gameID, state);

            // Assert
            Assert.AreEqual(null, result);
            dbMock.Verify(mock => mock.StringSet(gameID, serializedState, null, When.Always, CommandFlags.None), Times.Once());
        }
    }
}
