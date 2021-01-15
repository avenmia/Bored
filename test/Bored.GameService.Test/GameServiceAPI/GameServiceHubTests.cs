namespace Bored.GameService.Test.GameServiceAPI
{
    using Bored.Common;
    using Bored.GameService.Clients;
    using Bored.GameService.Factories;
    using Bored.GameService.GameServiceAPI;
    using Bored.GameService.GameSession;
    using Bored.GameService.Models;
    using Microsoft.AspNetCore.SignalR;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Tests for the Game Service Hub.
    /// </summary>
    [TestFixture]
    public class GameServiceHubTests
    {
        private Mock<IGameSessionContext> gameSessionContextMock;

        private Mock<IFactory> factoryMock;

        /// <summary>
        /// Setting up the game service hub tests.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            gameSessionContextMock = new Mock<IGameSessionContext>();
            factoryMock = new Mock<IFactory>();
        }

        /// <summary>
        /// Test for the SendMessage function.
        /// </summary>
        [Test]
        public void SendMessageTest()
        {
            // Arrange
            const string game = "TestGame";
            const string gameMove = "TestMove";
            const string gameId = "1";
            const string state = "state";
            const string expectedResult = "finalState";

            var gameStateMock = new Mock<IGameState>();
            var gameLogicMock = new Mock<IGameLogic>();
            var gameMoveMock = new Mock<IGameMove>();
            var clientsMock = new Mock<IHubCallerClients<IGameClient>>();
            var gameClient = new Mock<IGameClient>();

            gameLogicMock.Setup(x => x.MakeMove(It.IsAny<IGameMove>())).Returns(gameStateMock.Object);
            gameSessionContextMock.Setup(x => x.GetGameState(gameId)).Returns(state);
            gameSessionContextMock.Setup(x => x.AddGameState(gameId, gameStateMock.Object)).Returns(expectedResult);
            factoryMock.Setup(x => x.GameStateFactory(game, state)).Returns(gameStateMock.Object);
            factoryMock.Setup(x => x.GameFactory(game, gameStateMock.Object)).Returns(gameLogicMock.Object);
            factoryMock.Setup(x => x.GameMoveFactory(game, gameMove)).Returns(gameMoveMock.Object);
            clientsMock.Setup(x => x.All).Returns(gameClient.Object);

            GameMessage message = new GameMessage { Game = "TestGame", GameID = "1", Move = "TestMove" };
            GameServiceHub hub = new GameServiceHub(gameSessionContextMock.Object, factoryMock.Object);
            hub.Clients = clientsMock.Object;

            // Act
            hub.SendMessage(message);

            // Assert
            gameSessionContextMock.Verify(x => x.GetGameState("1"), Times.Once());
            gameSessionContextMock.Verify(x => x.AddGameState(gameId, gameStateMock.Object), Times.Once());

            factoryMock.Verify(x => x.GameStateFactory(game, state), Times.Once());
            factoryMock.Verify(x => x.GameFactory(game, It.IsAny<IGameState>()), Times.Once());
            factoryMock.Verify(x => x.GameMoveFactory(game, gameMove), Times.Once());
        }
    }
}
