using Bored.Game.TicTacToe;
using Bored.GameService.GameServiceAPI;
using Bored.GameService.GameSession;
using Bored.GameService.Models;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bored.GameService.Test.GameServiceAPI
{
    [TestFixture]
    public class GameServiceHubTests
    {
        private Mock<IGameSessionContext> gameSessionContextMock;

        [SetUp]
        public void Setup()
        {
            gameSessionContextMock = new Mock<IGameSessionContext>();
        }

        [Test]
        public void SetGameTypeTest()
        {
            // Arrange 
            GameServiceHub hub = new GameServiceHub(gameSessionContextMock.Object);
            GameMessage message = new GameMessage { Game = "TicTacToe", GameID = "1", Move = JsonConvert.SerializeObject(new TicTacToeMove(TicTacToePlayer.X, new TicTacToeCell(0, 0)))};
            
            // Act
            hub.SetGameType(message);
            
            // Assert
            Assert.AreEqual(hub.Game, typeof(TicTacToe));
            Assert.AreEqual(hub.GameState, typeof(TicTacToeState));
            Assert.AreEqual(hub.GameMove, typeof(TicTacToeMove));
        }

    }
}
