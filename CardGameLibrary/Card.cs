using System;
namespace CardGameLibrary
{
    public abstract class Card: Interfaces.ICard
    {
        protected Suit cardSuit;
        protected int cardRank;

        public Card(Suit suit, int rank)
        {
            cardRank = rank;
            cardSuit = suit;
        }

        public virtual Suit GetSuit()
        {
            return cardSuit;
        }

        public virtual int GetRank()
        {
            return cardRank;
        }

        public virtual new string ToString()
        {
            return cardRank + " of " + cardSuit;
        }

        public static bool IsPair(Interfaces.ICard card1, Interfaces.ICard card2)
        {
            if(card1 != null && card2 != null && card1.GetRank() == card2.GetRank())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsOfSameSuit(Interfaces.ICard card1, Interfaces.ICard card2)
        {
            if (card1 != null && card2 != null && card1.GetSuit() == card2.GetSuit())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
