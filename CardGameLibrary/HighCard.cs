using System;
namespace CardGameLibrary
{
    public class HighCard : Card
    {
        public HighCard(Suit suit, int rank) : base(suit, rank) { }

        public override int GetRank()
        {
            if (cardRank == 1)
                cardRank = 14; //Ace is considered the highest card

            return (cardRank);
        }
    }
}
