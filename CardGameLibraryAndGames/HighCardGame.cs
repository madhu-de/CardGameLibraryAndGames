using Generic = System.Collections.Generic;
using LibraryInterfaces = CardGameLibrary.Interfaces;
namespace CardGames
{
    public class HighCardGame
    {
        LibraryInterfaces.IDeckOfCards _deckOfCards;

        public Generic.List<CardGameLibrary.PlayerOfCard> PlayerOfCardsInGame { get; private set; }

        public HighCardGame(LibraryInterfaces.IDeckOfCards deckOfCards)
        {
            PlayerOfCardsInGame = new Generic.List<CardGameLibrary.PlayerOfCard>();
            _deckOfCards = deckOfCards;
        }

		public LibraryInterfaces.ICard GetCard()
		{
            return _deckOfCards.Deal();
		}

		//i.Multiple players are dealt a single card from a shuffled set of card
		public void Play(int numPlayers)
		{
            _deckOfCards.Shuffle(CardGameLibrary.Constants.NCARDS);
            for (int i = 0; i < numPlayers; i++)
			{
                CardGameLibrary.PlayerOfCard playerOfCards = new CardGameLibrary.PlayerOfCard();
				playerOfCards.PlayerNumber = i + 1;
				playerOfCards.PlayerCards.Add(GetCard());
				PlayerOfCardsInGame.Add(playerOfCards);
			}
		}

        //ii.Highest Card wins
        public Generic.List<int> EvaluateWinners()
		{
			Generic.List<int> winningPlayerNumbers = new Generic.List<int>();
			Generic.List<CardGameLibrary.PlayerOfCard> highestPlayersOfCards = new Generic.List<CardGameLibrary.PlayerOfCard>();
            CardGameLibrary.PlayerOfCard highestPlayerOfCards = null;

            //Initially, assign first player as highest card winner
			if (PlayerOfCardsInGame.Count > 0)
			{
				highestPlayerOfCards = PlayerOfCardsInGame[0];
                highestPlayersOfCards.Add(highestPlayerOfCards);
			}

			for (int i = 1; i < PlayerOfCardsInGame.Count; i++)
			{
                //If previous player(s) temporarily assigned
                //as highest card winner is outranked, remove
                //and add new player as currently
                //assigned highest card winner
                if (PlayerOfCardsInGame[i].PlayerScore >
                       highestPlayerOfCards.PlayerScore)
                {
                    highestPlayersOfCards.RemoveAll
                        (match => match.PlayerScore
                        == highestPlayerOfCards.PlayerScore);
                    highestPlayerOfCards = PlayerOfCardsInGame[i];
                    highestPlayersOfCards.Add(highestPlayerOfCards);
                }
                //Multiple players having cards of same rank can be co-winners
                else if (PlayerOfCardsInGame[i].PlayerScore ==
                   highestPlayerOfCards.PlayerScore)
                {
                    highestPlayerOfCards = PlayerOfCardsInGame[i];
                    highestPlayersOfCards.Add(highestPlayerOfCards);
                }
				
			}

            //This should be a list of at most Count == 4 for co-winner use-cases
            foreach (CardGameLibrary.PlayerOfCard playerOfCards in highestPlayersOfCards)
            {
                winningPlayerNumbers.Add(playerOfCards.PlayerNumber);
            }

			return winningPlayerNumbers;
		}
	}
}
