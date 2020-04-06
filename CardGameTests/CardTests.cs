using NUnit.Framework;

namespace Tests
{
    public partial class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void HighCardTestAceRank()
        {
            var highCard = new CardGameLibrary.HighCard(CardGameLibrary.Suit.Clubs, 1);
            Assert.AreEqual(highCard.GetRank(), 14);
        }

        [Test]
        public void HighCardTestNonAceRank2()
        {
            var highCard = new CardGameLibrary.HighCard(CardGameLibrary.Suit.Diamonds, 2);
            Assert.AreEqual(highCard.GetRank(), 2);
        }

        [Test]
        public void HighCardTestNonAceRank13()
        {
            var highCard = new CardGameLibrary.HighCard(CardGameLibrary.Suit.Spades, 13);
            Assert.AreEqual(highCard.GetRank(), 13);
        }

        [Test]
        public void HighCardTestSuit()
        {
            var highCard = new CardGameLibrary.HighCard(CardGameLibrary.Suit.Hearts, 1);
            Assert.AreEqual(CardGameLibrary.Suit.Hearts, highCard.GetSuit());
        }

        [Test]
        public void BlackJackCardTestSuit()
        {
            var blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Hearts, 1);
            Assert.AreEqual(CardGameLibrary.Suit.Hearts, blackJackCard.GetSuit());
        }

        [Test]
        public void BlackJackCardTestAceRank()
        {
            var blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Hearts, 1);
            Assert.AreEqual(1, blackJackCard.GetRank());
        }

        [Test]
        public void BlackJackCardTestFaceRank11()
        {
            var blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Hearts, 11);
            Assert.AreEqual(10, blackJackCard.GetRank());
        }

        [Test]
        public void BlackJackCardTestFaceRank12()
        {
            var blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Hearts, 12);
            Assert.AreEqual(10, blackJackCard.GetRank());
        }

        [Test]
        public void BlackJackCardTestFaceRank13()
        {
            var blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Hearts, 13);
            Assert.AreEqual(10, blackJackCard.GetRank());
        }

        [Test]
        public void DeckOfCardsTypeBlackJack()
        {
            var deckOfCards = new CardGameLibrary.DeckOfCards(CardGameLibrary.Constants.BLACKJACK);
            Assert.AreEqual(deckOfCards.Deal().GetType(), typeof(CardGameLibrary.BlackJackCard));
        }

        [Test]
        public void DeckOfCardsTypeHighCard()
        {
            var deckOfCards = new CardGameLibrary.DeckOfCards(CardGameLibrary.Constants.HIGHCARD);
            Assert.AreEqual(deckOfCards.Deal().GetType(), typeof(CardGameLibrary.HighCard));
        }

        [Test]
        public void DeckOfCardsShuffleBlackJack()
        {
            var deckOfCards = new CardGameLibrary.DeckOfCards(CardGameLibrary.Constants.BLACKJACK);

            deckOfCards.Shuffle(1, 0, 1);

            var blackJackCard1 = deckOfCards.Deal();
            Assert.AreEqual(blackJackCard1.GetSuit(), CardGameLibrary.Suit.Clubs);
            Assert.AreEqual(blackJackCard1.GetRank(), 2);

            var blackJackCard2 = deckOfCards.Deal();
            Assert.AreEqual(blackJackCard2.GetSuit(), CardGameLibrary.Suit.Clubs);
            Assert.AreEqual(blackJackCard2.GetRank(), 1);
        }

        [Test]
        public void DeckOfCardsShuffleHighCard()
        {
            var deckOfCards = new CardGameLibrary.DeckOfCards(CardGameLibrary.Constants.HIGHCARD);

            deckOfCards.Shuffle(1, 0, 1);

            var highCard1 = deckOfCards.Deal();
            Assert.AreEqual(highCard1.GetSuit(), CardGameLibrary.Suit.Clubs);
            Assert.AreEqual(highCard1.GetRank(), 2);

            var highCard2 = deckOfCards.Deal();
            Assert.AreEqual(highCard2.GetSuit(), CardGameLibrary.Suit.Clubs);
            Assert.AreEqual(highCard2.GetRank(), 14);
        }
    }
}