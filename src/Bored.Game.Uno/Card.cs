using Bored.Common;
using System;

namespace Bored.Game.Uno
{
    public record Card
    {
        public CardValue Value { get; }
        public CardColor? Color { get; }
        public static CardValue[] GetAllValues() => Enum.GetValues<CardValue>();
        public static bool IsWild(CardValue value) => value == CardValue.Wild || value == CardValue.WildDrawFour;
        public bool IsWildCard() => IsWild(Value);
        public bool IsSpecialCard() => IsWildCard() || Value == CardValue.DrawTwo || Value == CardValue.Reverse || Value == CardValue.Skip;
        public override string ToString() => Value.ToString() + Color != null ? " " + Color.ToString() : "";
        public Card(CardValue value)
        {
            if (!IsWild(value))
            {
                throw new GameLogicException("Card needs a color");
            }
            Value = value;
        }
        public Card(CardValue value, CardColor color)
        {
            if (IsWild(value))
            {
                throw new GameLogicException("Wild cards cannot have color");
            }
            Value = value;
            Color = color;
        }
    }
}
