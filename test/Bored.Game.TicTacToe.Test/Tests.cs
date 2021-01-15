namespace Bored.Game.TicTacToe.Test
{
    using NUnit.Framework;

    /// <summary>
    /// TicTacToe tests.
    /// </summary>
    public class Tests
    {
        /// <summary>
        /// The TicTacToe state.
        /// </summary>
        private TicTacToeState state;

        /// <summary>
        /// Sets up the TicTacToe unit tests.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            state = new TicTacToeState();
        }

        /// <summary>
        /// Tests the InitialState method.
        /// </summary>
        [Test]
        public void TestInitialState()
        {
            Assert.IsNull(state.Winner);
            Assert.AreEqual(state.Turn, TicTacToePlayer.X);
        }

        /// <summary>
        /// Tests the moving player X.
        /// </summary>
        [Test]
        public void MoveX()
        {
            var game = new TicTacToe(state);
            game.MakeMove(new TicTacToeMove(TicTacToePlayer.X, new TicTacToeCell(0, 1)));
            Assert.AreEqual(game.State.Cells[0, 1], TicTacToePlayer.X);
        }

        /// <summary>
        /// Tests an invalid player O move.
        /// </summary>
        [Test]
        public void MoveOFail()
        {
            var game = new TicTacToe(state);
            var move = new TicTacToeMove(TicTacToePlayer.O, new TicTacToeCell(0, 1));
            var newState = game.MakeMove(move);
            Assert.IsNull(newState);
        }

        /// <summary>
        /// Tests 3 in a row winning condition.
        /// </summary>
        [Test]
        public void ThreeInTheRow()
        {
            state.Cells[0, 0] = state.Cells[0, 1] = TicTacToePlayer.X;
            state.Cells[1, 0] = state.Cells[2, 1] = TicTacToePlayer.O;
            var game = new TicTacToe(state);
            Assert.IsNull(game.State.Winner);
            var newState = game.MakeMove(new TicTacToeMove(TicTacToePlayer.X, new TicTacToeCell(0, 2)));
            Assert.AreEqual(((TicTacToeState)newState).Winner, TicTacToePlayer.X);
        }

        /// <summary>
        /// Tests 3 in a column winning condition.
        /// </summary>
        [Test]
        public void ThreeInTheColumn()
        {
            state.Cells[0, 0] = state.Cells[0, 1] = state.Cells[2, 2] = TicTacToePlayer.X;
            state.Cells[0, 1] = state.Cells[1, 1] = TicTacToePlayer.O;
            state.Turn = TicTacToePlayer.O;
            var game = new TicTacToe(state);
            Assert.IsNull(game.State.Winner);
            var newState = game.MakeMove(new TicTacToeMove(TicTacToePlayer.O, new TicTacToeCell(2, 1)));
            Assert.AreEqual(((TicTacToeState)newState).Winner, TicTacToePlayer.O);
        }

        /// <summary>
        /// Tests 3 in a diagonal winning condition.
        /// </summary>
        [Test]
        public void ThreeInTheDiagonal()
        {
            state.Cells[0, 0] = state.Cells[1, 1] = TicTacToePlayer.X;
            state.Cells[1, 0] = state.Cells[2, 1] = TicTacToePlayer.O;
            var game = new TicTacToe(state);
            Assert.IsNull(game.State.Winner);
            var newState = game.MakeMove(new TicTacToeMove(TicTacToePlayer.X, new TicTacToeCell(2, 2)));
            Assert.AreEqual(((TicTacToeState)newState).Winner, TicTacToePlayer.X);
        }

        /// <summary>
        /// Tests 3 in the other diagonal winning condition.
        /// </summary>
        [Test]
        public void ThreeInTheOtherDiagonal()
        {
            state.Cells[2, 0] = state.Cells[1, 1] = TicTacToePlayer.X;
            state.Cells[1, 0] = state.Cells[2, 1] = TicTacToePlayer.O;
            var game = new TicTacToe(state);
            Assert.IsNull(game.State.Winner);
            var newState = game.MakeMove(new TicTacToeMove(TicTacToePlayer.X, new TicTacToeCell(0, 2)));
            Assert.AreEqual(((TicTacToeState)newState).Winner, TicTacToePlayer.X);
        }

        /// <summary>
        /// Test for if a token already exists.
        /// </summary>
        [Test]
        public void TokenAlreadyExists()
        {
            state.Cells[1, 1] = TicTacToePlayer.X;
            var game = new TicTacToe(state);
            Assert.IsNull(game.State.Winner);
            Assert.IsNull(game.MakeMove(new TicTacToeMove(TicTacToePlayer.O, new TicTacToeCell(1, 1))));
        }

        /// <summary>
        /// Testing if move is out of bounds.
        /// </summary>
        [Test]
        public void OutOfBounds()
        {
            state.Cells[1, 1] = TicTacToePlayer.X;
            state.Turn = TicTacToePlayer.O;
            var game = new TicTacToe(state);
            Assert.IsNull(game.State.Winner);
            Assert.IsNull(game.MakeMove(new TicTacToeMove(TicTacToePlayer.O, new TicTacToeCell(1, 3))));
            Assert.IsNull(game.MakeMove(new TicTacToeMove(TicTacToePlayer.O, new TicTacToeCell(3, 2))));
        }

        /// <summary>
        /// Test for move after a game ends.
        /// </summary>
        [Test]
        public void MoveAfterGameEnds()
        {
            state.Cells[0, 0] = state.Cells[0, 1] = state.Cells[0, 2] = TicTacToePlayer.X;
            state.Cells[1, 0] = state.Cells[1, 1] = TicTacToePlayer.O;
            state.Winner = TicTacToePlayer.X;
            var game = new TicTacToe(state);
            Assert.IsNull(game.MakeMove(new TicTacToeMove(TicTacToePlayer.O, new TicTacToeCell(1, 3))));
        }

        /// <summary>
        /// Test for flipping the move.
        /// </summary>
        [Test]
        public void TestFlipMove()
        {
            var game = new TicTacToe(state);
            Assert.AreEqual(game.State.Turn, TicTacToePlayer.X);
            game.MakeMove(new TicTacToeMove(TicTacToePlayer.X, new TicTacToeCell(0, 1)));
            Assert.AreEqual(game.State.Turn, TicTacToePlayer.O);
            game.MakeMove(new TicTacToeMove(TicTacToePlayer.O, new TicTacToeCell(1, 1)));
            Assert.AreEqual(game.State.Turn, TicTacToePlayer.X);
        }
    }
}