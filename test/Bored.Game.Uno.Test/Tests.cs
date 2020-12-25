using NUnit.Framework;

namespace Bored.Game.Uno.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var card = new Card(CardValue.One, CardColor.Yellow);
            var card2 = new Card(CardValue.Two, CardColor.Yellow);
            Assert.AreNotEqual(card, card2);
            Assert.AreEqual(card.ToString(), "");
        }
    }
}