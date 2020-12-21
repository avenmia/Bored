using NUnit.Framework;

namespace Bored.Game.TicTacToe.Test
{
    public class Tests
    {
        private TicTacToeState State;

        [SetUp]
        public void Setup()
        {
            State = new TicTacToeState();
        }

        [Test]
        public void TestInitialState()
        {
            Assert.IsNull(State.Winner);
            Assert.AreEqual(State.Turn, TicTacToePlayer.X);
        }

        [Test]
        public void MoveX()
        {
            var game = new TicTacToe(State);
            game.MakeMove(new TicTacToeMove(TicTacToePlayer.X, (0, 1)));
            Assert.AreEqual(game.State.Cells[0, 1], TicTacToePlayer.X);
        }

        [Test]
        public void MoveOFail()
        {
            var game = new TicTacToe(State);
            var move = new TicTacToeMove(TicTacToePlayer.O, (0, 1));
            var newState = game.MakeMove(move);
            Assert.IsNull(newState);
        }

        [Test]
        public void ThreeInTheRow()
        {
            State.Cells[0, 0] = State.Cells[0, 1] = TicTacToePlayer.X;
            State.Cells[1, 0] = State.Cells[2, 1] = TicTacToePlayer.O;
            var game = new TicTacToe(State);
            Assert.IsNull(game.State.Winner);
            var newState = game.MakeMove(new TicTacToeMove(TicTacToePlayer.X, (0, 2)));
            Assert.AreEqual(((TicTacToeState)newState).Winner, TicTacToePlayer.X);
        }

        [Test]
        public void ThreeInTheColumn()
        {
            State.Cells[0, 0] = State.Cells[0, 1] = State.Cells[2, 2] = TicTacToePlayer.X;
            State.Cells[0, 1] = State.Cells[1, 1] = TicTacToePlayer.O;
            State.Turn = TicTacToePlayer.O;
            var game = new TicTacToe(State);
            Assert.IsNull(game.State.Winner);
            var newState = game.MakeMove(new TicTacToeMove(TicTacToePlayer.O, (2, 1)));
            Assert.AreEqual(((TicTacToeState)newState).Winner, TicTacToePlayer.O);
        }

        [Test]
        public void ThreeInTheDiagonal()
        {
            State.Cells[0, 0] = State.Cells[1, 1] = TicTacToePlayer.X;
            State.Cells[1, 0] = State.Cells[2, 1] = TicTacToePlayer.O;
            var game = new TicTacToe(State);
            Assert.IsNull(game.State.Winner);
            var newState = game.MakeMove(new TicTacToeMove(TicTacToePlayer.X, (2, 2)));
            Assert.AreEqual(((TicTacToeState)newState).Winner, TicTacToePlayer.X);
        }

        [Test]
        public void ThreeInTheOtherDiagonal()
        {
            State.Cells[2, 0] = State.Cells[1, 1] = TicTacToePlayer.X;
            State.Cells[1, 0] = State.Cells[2, 1] = TicTacToePlayer.O;
            var game = new TicTacToe(State);
            Assert.IsNull(game.State.Winner);
            var newState = game.MakeMove(new TicTacToeMove(TicTacToePlayer.X, (0, 2)));
            Assert.AreEqual(((TicTacToeState)newState).Winner, TicTacToePlayer.X);
        }

        [Test]
        public void TokenAlreadyExists()
        {
            State.Cells[1, 1] = TicTacToePlayer.X;
            var game = new TicTacToe(State);
            Assert.IsNull(game.State.Winner);
            Assert.IsNull(game.MakeMove(new TicTacToeMove(TicTacToePlayer.O, (1, 1))));
        }

        [Test]
        public void OutOfBounds()
        {
            State.Cells[1, 1] = TicTacToePlayer.X;
            State.Turn = TicTacToePlayer.O;
            var game = new TicTacToe(State);
            Assert.IsNull(game.State.Winner);
            Assert.IsNull(game.MakeMove(new TicTacToeMove(TicTacToePlayer.O, (1, 3))));
            Assert.IsNull(game.MakeMove(new TicTacToeMove(TicTacToePlayer.O, (3, 2))));
        }

        [Test]
        public void MoveAfterGameEnds()
        {
            State.Cells[0, 0] = State.Cells[0, 1] = State.Cells[0, 2] = TicTacToePlayer.X;
            State.Cells[1, 0] = State.Cells[1, 1] = TicTacToePlayer.O;
            State.Winner = TicTacToePlayer.X;
            var game = new TicTacToe(State);
            Assert.IsNull(game.MakeMove(new TicTacToeMove(TicTacToePlayer.O, (1, 3))));
        }

        [Test]
        public void TestFlipMove()
        {
            var game = new TicTacToe(State);
            Assert.AreEqual(game.State.Turn, TicTacToePlayer.X);
            game.MakeMove(new TicTacToeMove(TicTacToePlayer.X, (0, 1)));
            Assert.AreEqual(game.State.Turn, TicTacToePlayer.O);
            game.MakeMove(new TicTacToeMove(TicTacToePlayer.O, (1, 1)));
            Assert.AreEqual(game.State.Turn, TicTacToePlayer.X);
        }
    }
}