using System;
namespace CardGameLibrary
{
    //Inspired by https://www2.cs.duke.edu/csed/ap/cards/cardstuff.pdf
    public class DeckOfCards : Interfaces.IDeckOfCards
    {
        private Interfaces.ICard[] _deckOfCards;
        private int _currentCard;
        private Random _randNum;

        public DeckOfCards(string cardType)
        {
            _deckOfCards = new Interfaces.ICard[Constants.NCARDS];
            int i = 0;
            for (Suit suit = Suit.Clubs; suit <= Suit.Spades; suit++)
            {
                for (int rank = 1; rank <= 13; rank++)
                {
                    if (cardType.Equals(Constants.BLACKJACK, StringComparison.CurrentCultureIgnoreCase))
                    {
                        _deckOfCards[i++] = new BlackJackCard(suit, rank);
                    }
                    else
                    {
                        _deckOfCards[i++] = new HighCard(suit, rank);
                    }
                }
            }
            _currentCard = 0;
        }

        public void Shuffle(int number)
        {
            int i, j;
            _randNum = new Random();
            for (int k = 0; k < number; k++)
            {
                i = (int)(_randNum.Next(Constants.NCARDS));
                j = (int)(_randNum.Next(Constants.NCARDS));
                Card tmp = (Card)_deckOfCards[i];
                _deckOfCards[i] = _deckOfCards[j];
                _deckOfCards[j] = tmp;
            }

            _currentCard = 0;
        }

        public void Shuffle(int number, int i, int j)
        {
            _randNum = new Random();
            for (int k = 0; k < number; k++)
            {
                Card tmp = (Card)_deckOfCards[i];
                _deckOfCards[i] = _deckOfCards[j];
                _deckOfCards[j] = tmp;
            }

            _currentCard = 0;
        }

        public Card Deal()
        {
            if (_currentCard < Constants.NCARDS)
                return (Card)(_deckOfCards[_currentCard++]);
            else
                return (null);
        }
    }
}
