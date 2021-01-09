using Bored.Common;
using Bored.Game.TicTacToe;
using Bored.GameService.Factories;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bored.GameService.Test.Factories
{
    public class FactoryTests
    {
        [Test]
        public void GameFactoryTest()
        {
            // Arrange
            IGameState state = new TicTacToeState();
            Factory factory = new Factory();
            TicTacToe expectedResult = new TicTacToe(state as TicTacToeState);

            // Act
            TicTacToe result = factory.GameFactory("TicTacToe", state) as TicTacToe;

            // Assert
            Assert.AreEqual(typeof(TicTacToe), result.GetType());
            Assert.AreEqual(JsonConvert.SerializeObject(expectedResult), JsonConvert.SerializeObject(result));
        }

        [Test]
        public void GameFactoryInvalidGameTest()
        {
            Factory factory = new Factory();
            Assert.Throws<Exception>(() => factory.GameFactory("RandomName", null));
        }

        [Test]
        public void GameStateFactoryTest()
        {
            // Arrange
            IGameState expectedState = new TicTacToeState();
            string serializedState = JsonConvert.SerializeObject(expectedState);
            Factory factory = new Factory();

            // Act
            IGameState result = factory.GameStateFactory("TicTacToe", serializedState);

            // Assert
            Assert.AreEqual(typeof(TicTacToeState), result.GetType());
            Assert.AreEqual(JsonConvert.SerializeObject(expectedState), JsonConvert.SerializeObject(result));
        }

        [Test]
        public void GameStateFactoryInvalidStateTest()
        {
            Factory factory = new Factory();
            Assert.Throws<Exception>(() => factory.GameStateFactory("RandomName", null));
        }

        [Test]
        public void GameMoveFactoryTest()
        {
            // Arrange
            IGameMove expectedMove = new TicTacToeMove(TicTacToePlayer.O, new TicTacToeCell(0,0));
            string serializedMove = JsonConvert.SerializeObject(expectedMove);
            Factory factory = new Factory();

            // Act
            IGameMove result = factory.GameMoveFactory("TicTacToe", serializedMove);

            // Assert
            Assert.AreEqual(typeof(TicTacToeMove), result.GetType());
            Assert.AreEqual(JsonConvert.SerializeObject(expectedMove), JsonConvert.SerializeObject(result));
        }

        [Test]
        public void GameMoveFactoryInvalidMoveTest()
        {
            Factory factory = new Factory();
            Assert.Throws<Exception>(() => factory.GameMoveFactory("RandomName", null));
        }
    }
}
