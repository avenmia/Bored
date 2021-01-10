using Bored.Common;
using Bored.Game.TicTacToe;
using Newtonsoft.Json;
using System;

namespace Bored.GameService.Factories
{
    public class Factory : IFactory
    {
        public IGameLogic GameFactory(string gameName, IGameState state)
        {
            return gameName switch
            {
                "TicTacToe" => new TicTacToe(state as TicTacToeState),
                _ => throw new Exception("Invalid game."),
            };
        }

        public IGameState GameStateFactory(string gameName, string state)
        {
            return gameName switch
            {
                "TicTacToe" => state == null ? new TicTacToeState() : JsonConvert.DeserializeObject<TicTacToeState>(state),
                _ => throw new Exception("Invalid game state."),
            };
        }

        public IGameMove GameMoveFactory(string gameName, string move)
        {
            return gameName switch
            {
                "TicTacToe" => JsonConvert.DeserializeObject<TicTacToeMove>(move),
                _ => throw new Exception("Invalid game move."),
            };
        }
    }
}
