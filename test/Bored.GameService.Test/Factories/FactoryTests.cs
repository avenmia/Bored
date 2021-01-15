namespace Bored.GameService.Test.Factories
{
    using System;
    using Bored.Common;
    using Bored.Game.TicTacToe;
    using Bored.GameService.Factories;
    using Newtonsoft.Json;
    using NUnit.Framework;

    /// <summary>
    /// Tests for the factory class.
    /// </summary>
    public class FactoryTests
    {
        /// <summary>
        /// Test for the GameFactory method.
        /// </summary>
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

        /// <summary>
        /// Test for GameFactory method using an invalid game.
        /// </summary>
        [Test]
        public void GameFactoryInvalidGameTest()
        {
            Factory factory = new Factory();
            Assert.Throws<Exception>(() => factory.GameFactory("RandomName", null));
        }

        /// <summary>
        /// Test for the GameStateFactory method.
        /// </summary>
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

        /// <summary>
        /// Test for GameStateFactory method with an invalid state.
        /// </summary>
        [Test]
        public void GameStateFactoryInvalidStateTest()
        {
            Factory factory = new Factory();
            Assert.Throws<Exception>(() => factory.GameStateFactory("RandomName", null));
        }

        /// <summary>
        /// Test for GameMoveFactory method.
        /// </summary>
        [Test]
        public void GameMoveFactoryTest()
        {
            // Arrange
            IGameMove expectedMove = new TicTacToeMove(TicTacToePlayer.O, new TicTacToeCell(0, 0));
            string serializedMove = JsonConvert.SerializeObject(expectedMove);
            Factory factory = new Factory();

            // Act
            IGameMove result = factory.GameMoveFactory("TicTacToe", serializedMove);

            // Assert
            Assert.AreEqual(typeof(TicTacToeMove), result.GetType());
            Assert.AreEqual(JsonConvert.SerializeObject(expectedMove), JsonConvert.SerializeObject(result));
        }

        /// <summary>
        /// Test for GameMoveFactory method with invalid move.
        /// </summary>
        [Test]
        public void GameMoveFactoryInvalidMoveTest()
        {
            Factory factory = new Factory();
            Assert.Throws<Exception>(() => factory.GameMoveFactory("RandomName", null));
        }
    }
}
