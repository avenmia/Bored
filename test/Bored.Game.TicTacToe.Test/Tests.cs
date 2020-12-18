using NUnit.Framework;

namespace Bored.Game.TicTacToe.Test
{
    public class Tests
    {
        private TicTacToeState State;

        [SetUp]
        public void Setup()
        {
            this.State = new TicTacToeState();
        }

        [Test]
        public void TestInitialState()
        {
            Assert.IsNull(this.State.Winner);
            Assert.AreEqual(this.State.Turn, TicTacToePlayer.X);
        }

        [Test]
        public void MoveX()
        {
            var game = new TicTacToe(this.State);
            var move = new TicTacToeMove(TicTacToePlayer.X, (0, 1));
            game.MakeMove(move);
            Assert.AreEqual(game.State.Cells[0, 1].Value, TicTacToePlayer.X);
        }

        [Test]
        public void MoveOFail()
        {
            var game = new TicTacToe(this.State);
            var move = new TicTacToeMove(TicTacToePlayer.O, (0, 1));
            var newState = game.MakeMove(move);
            Assert.IsNull(newState);
        }

        [Test]
        public void ThreeInTheRow()
        {
            this.State.Cells[0, 0] = this.State.Cells[0, 1] = new TicTacToeCell(TicTacToePlayer.X);
            this.State.Cells[1, 0] = this.State.Cells[2, 1] = new TicTacToeCell(TicTacToePlayer.O);
            var game = new TicTacToe(this.State);
            Assert.IsNull(game.State.Winner);
            var move = new TicTacToeMove(TicTacToePlayer.X, (0, 2));
            var newState = game.MakeMove(move);
            Assert.AreEqual(newState.Winner, TicTacToePlayer.X);
        }

        [Test]
        public void ThreeInTheColumn()
        {
            this.State.Cells[0, 0] = this.State.Cells[0, 1] = this.State.Cells[2, 2] = new TicTacToeCell(TicTacToePlayer.X);
            this.State.Cells[0, 1] = this.State.Cells[1, 1] = new TicTacToeCell(TicTacToePlayer.O);
            this.State.Turn = TicTacToePlayer.O;
            var game = new TicTacToe(this.State);
            Assert.IsNull(game.State.Winner);
            var move = new TicTacToeMove(TicTacToePlayer.O, (2, 1));
            var newState = game.MakeMove(move);
            Assert.AreEqual(newState.Winner, TicTacToePlayer.O);
        }

        [Test]
        public void ThreeInTheDiagonal()
        {
            this.State.Cells[0, 0] = this.State.Cells[1, 1] = new TicTacToeCell(TicTacToePlayer.X);
            this.State.Cells[1, 0] = this.State.Cells[2, 1] = new TicTacToeCell(TicTacToePlayer.O);
            var game = new TicTacToe(this.State);
            Assert.IsNull(game.State.Winner);
            var move = new TicTacToeMove(TicTacToePlayer.X, (2, 2));
            var newState = game.MakeMove(move);
            Assert.AreEqual(newState.Winner, TicTacToePlayer.X);
        }

        [Test]
        public void ThreeInTheOtherDiagonal()
        {
            this.State.Cells[2, 0] = this.State.Cells[1, 1] = new TicTacToeCell(TicTacToePlayer.X);
            this.State.Cells[1, 0] = this.State.Cells[2, 1] = new TicTacToeCell(TicTacToePlayer.O);
            var game = new TicTacToe(this.State);
            Assert.IsNull(game.State.Winner);
            var move = new TicTacToeMove(TicTacToePlayer.X, (0, 2));
            var newState = game.MakeMove(move);
            Assert.AreEqual(newState.Winner, TicTacToePlayer.X);
        }

        [Test]
        public void TokenAlreadyExists()
        {
            this.State.Cells[1, 1] = new TicTacToeCell(TicTacToePlayer.X);
            var game = new TicTacToe(this.State);
            Assert.IsNull(game.State.Winner);
            var move = new TicTacToeMove(TicTacToePlayer.O, (1, 1));
            var newState = game.MakeMove(move);
            Assert.IsNull(newState);
        }

        [Test]
        public void OutOfBounds()
        {
            this.State.Cells[1, 1] = new TicTacToeCell(TicTacToePlayer.X);
            var game = new TicTacToe(this.State);
            Assert.IsNull(game.State.Winner);
            var move = new TicTacToeMove(TicTacToePlayer.O, (1, 3));
            var newState = game.MakeMove(move);
            Assert.IsNull(newState);
        }

        [Test]
        public void MoveAfterGameEnds()
        {
            this.State.Cells[0, 0] = this.State.Cells[0, 1] = this.State.Cells[0, 2] = new TicTacToeCell(TicTacToePlayer.X);
            this.State.Cells[1, 0] = this.State.Cells[1, 1] = new TicTacToeCell(TicTacToePlayer.O);
            this.State.Winner = TicTacToePlayer.X;
            var game = new TicTacToe(this.State);
            var move = new TicTacToeMove(TicTacToePlayer.O, (1, 3));
            var newState = game.MakeMove(move);
            Assert.IsNull(newState);
        }
    }
}