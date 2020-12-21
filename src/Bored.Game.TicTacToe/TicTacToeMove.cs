﻿using Bored.Common;

namespace Bored.Game.TicTacToe
{
    public class TicTacToeMove : IGameMove
    {
        public TicTacToePlayer Player;
        public (byte row, byte col) Cell;

        public TicTacToeMove(TicTacToePlayer _player, (byte row, byte col) _cell)
        {
            Player = _player;
            Cell = _cell;
        }
    }
}