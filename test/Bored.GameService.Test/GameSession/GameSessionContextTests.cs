﻿namespace Bored.GameService.Test.GameSession
{
    using Bored.Common;
    using Bored.Game.TicTacToe;
    using Bored.GameService.GameSession;
    using Moq;
    using Newtonsoft.Json;
    using NUnit.Framework;
    using StackExchange.Redis;

    /// <summary>
    /// Tests for the GameSession Context.
    /// </summary>
    [TestFixture]
    public class GameSessionContextTests
    {
        private Mock<IConnectionMultiplexer> multiplexerMock;
        private Mock<IDatabase> dbMock;

        /// <summary>
        /// Sets up the GameSession Context tests.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            multiplexerMock = new Mock<IConnectionMultiplexer>();
            dbMock = new Mock<IDatabase>();
            multiplexerMock.Setup(x => x.GetDatabase(It.IsAny<int>(), It.IsAny<object>())).Returns(dbMock.Object);
        }

        /// <summary>
        /// Test for the GetGameState function.
        /// </summary>
        /// <param name="gameID">The game ID.</param>
        /// <param name="state">The game state.</param>
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

        /// <summary>
        /// Test to get a non existant game.
        /// </summary>
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

        /// <summary>
        /// Test to add game state.
        /// </summary>
        [Test]
        public void AddGameStateTest()
        {
            // Arrange
            string gameID = "game";
            IGameState state = new TicTacToeState();
            string serializedState = JsonConvert.SerializeObject(state);
            dbMock.Setup(d => d.StringSet(gameID, serializedState, null, When.Always, CommandFlags.None)).Returns(true);
            GameSessionContext context = new GameSessionContext(multiplexerMock.Object);

            // Act
            var result = context.AddGameState(gameID, state);

            // Assert
            Assert.AreEqual(serializedState, result);
            dbMock.Verify(mock => mock.StringSet(gameID, serializedState, null, When.Always, CommandFlags.None), Times.Once());
        }

        /// <summary>
        /// Test for a failed add game state.
        /// </summary>
        [Test]
        public void FailedAddGameStateTest()
        {
            // Arrange
            string gameID = "game";
            IGameState state = new TicTacToeState();
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
