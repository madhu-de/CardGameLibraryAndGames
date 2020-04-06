using System;
namespace CardGameLibrary
{
	public class BlackJackCard : Card
	{
        public BlackJackCard(Suit suit, int rank) : base(suit, rank) { }

        public override int GetRank()
        {
            if (cardRank > 10 && cardRank < 14) //Face cards J, Q, K have a value of 10 each
                cardRank = 10;

            return (cardRank);
        }
    }
}
