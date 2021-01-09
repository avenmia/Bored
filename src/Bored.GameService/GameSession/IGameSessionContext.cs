using Bored.Common;

namespace Bored.GameService.GameSession
{
    public interface IGameSessionContext
    {
        string GetGameState(string gameID);

        string AddGameState(string gameID, IGameState state);
    }
}
