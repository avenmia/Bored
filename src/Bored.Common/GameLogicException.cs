using System;

namespace Bored.Common
{
    public class GameLogicException : Exception
    {
        public GameLogicException()
        {
        }

        public GameLogicException(string message)
            : base(message)
        {
        }

        public GameLogicException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
