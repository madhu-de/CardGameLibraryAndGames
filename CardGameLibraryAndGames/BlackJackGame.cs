using Generic = System.Collections.Generic;
using LibraryInterfaces = CardGameLibrary.Interfaces;
namespace CardGames
{
	public class BlackJackGame
	{
		LibraryInterfaces.IDeckOfCards _deckOfCards;

        public Generic.List<CardGameLibrary.PlayerOfCard> PlayerOfCardsInGame { get; private set; }

        //Assumption: One set of 52 cards is being used in the deck of cards and not multiple
        public BlackJackGame(LibraryInterfaces.IDeckOfCards deckOfCards)
        {
            PlayerOfCardsInGame = new Generic.List<CardGameLibrary.PlayerOfCard>();
            _deckOfCards = deckOfCards;
        }

        public LibraryInterfaces.ICard GetCard()
        {
            return _deckOfCards.Deal();
        }

        //i.Multiple players and a dealer are dealt 2 cards from a shuffled set of cards
        public void Play(int numPlayers)
		{
            _deckOfCards.Shuffle(CardGameLibrary.Constants.NCARDS);
            for (int i = 0; i < numPlayers + 1; i++)
            {
                CardGameLibrary.PlayerOfCard playerOfCards = new CardGameLibrary.PlayerOfCard();
                playerOfCards.PlayerNumber = i + 1;
                if (i == numPlayers) playerOfCards.IsDealer = true;
                playerOfCards.PlayerCards.Add(GetCard());
                playerOfCards.PlayerCards.Add(GetCard());
                PlayerOfCardsInGame.Add(playerOfCards);
            }
        }

        //ii.Each player may choose to hit or stay until their total equals or exceeds 21
        public void DealForPlayer(int playerNum)
        {
            var player = PlayerOfCardsInGame.Find(p => p.PlayerNumber == playerNum);
            if(player != null)
            {
                player.PlayerCards.Add(GetCard());
            }
        }

        //iii.If the player's hand is higher than the dealer's and does not exceed 21, the player wins.
        //iv.If the player's hand is lower than the dealer's or exceeds 21, the player loses.
        //Ace value can be 11 (soft) or 1 (hard)
        //Source: https://en.wikipedia.org/wiki/Blackjack
        //If the player is dealt an Ace and a ten-value card(called a "blackjack" or "natural"), and the dealer does not, the player wins and usually receives a bonus.
        //If the player exceeds a sum of 21 ("busts"); the player loses, even if the dealer also exceeds 21.
        //If the dealer exceeds 21 ("busts") and the player does not; the player wins.
        //If the player attains a final sum higher than the dealer and does not bust; the player wins.
        //If both dealer and player receive a blackjack or any other hands with the same sum, called a "push", no one wins.
        public Generic.List<(CardGameLibrary.DealerOrPlayerOrNone whoWon, int playerNumber)> EvaluateWinners()
		{
            var winnerList = new Generic.List<(CardGameLibrary.DealerOrPlayerOrNone whoWon, int playerNumber)>();

            var dealer = PlayerOfCardsInGame.Find(p => p.IsDealer == true);
            var dealerScoreToBeat = dealer.PlayerScore;
            if(dealer.HasSoftScore && dealerScoreToBeat > 21)
            {
                dealerScoreToBeat = EvaluateHardScore(dealerScoreToBeat, dealer.NumSoftCards);//using hard value so as not to get bust
            }

			foreach(var player in PlayerOfCardsInGame)
            {
                if(!player.IsDealer)
                {
                    var playerScore = player.PlayerScore;
                    if (player.HasSoftScore && playerScore > 21)
                    {
                        playerScore = EvaluateHardScore(playerScore, player.NumSoftCards);//using hard value so as not to get bust
                    }


                    //If the player is dealt an Ace and a ten-value card(called a "blackjack" or "natural"),
                    //and the dealer does not, the player wins.
                    if (playerScore == 21 && dealerScoreToBeat != 21)
                    {
                        var result = (whoWon: CardGameLibrary.DealerOrPlayerOrNone.Player, playerNumber: player.PlayerNumber);
                        winnerList.Add(result);
                    }

                    ///If the player exceeds a sum of 21 ("busts"); the player loses, even if the dealer also exceeds 21.
                    //iv.If the player's hand is lower than the dealer's or exceeds 21, the player loses.
                    else if (playerScore > 21 || (playerScore < dealerScoreToBeat && dealerScoreToBeat <= 21))
                    {
                        var result = (whoWon: CardGameLibrary.DealerOrPlayerOrNone.Dealer, playerNumber: player.PlayerNumber);
                        winnerList.Add(result);
                    }

                    //If the dealer exceeds 21("busts") and the player does not; the player wins.
                    else if (dealerScoreToBeat > 21 && playerScore <= 21)
                    {
                        var result = (whoWon: CardGameLibrary.DealerOrPlayerOrNone.Player, playerNumber: player.PlayerNumber);
                        winnerList.Add(result);
                    }

                    //If the player attains a final sum higher than the dealer and does not bust; the player wins.
                    //iii.If the player's hand is higher than the dealer's and does not exceed 21, the player wins.
                    else if (playerScore > dealerScoreToBeat && playerScore <= 21)
                    {
                        var result = (whoWon: CardGameLibrary.DealerOrPlayerOrNone.Player, playerNumber: player.PlayerNumber);
                        winnerList.Add(result);
                    }

                    //If both dealer and player receive a blackjack or any other hands with the same sum, called a "push", no one wins.
                    else if (playerScore == dealerScoreToBeat)
                    {
                        var result = (whoWon: CardGameLibrary.DealerOrPlayerOrNone.None, playerNumber: player.PlayerNumber);
                        winnerList.Add(result);
                    }
                }
            }

            return winnerList;
		}

        private int EvaluateHardScore(int softScore, int numAces)
        {
            int hardScore = softScore;

            for(int n = 1; n <= numAces; n++)
            {
                if (hardScore > 21)
                    hardScore -= 10;
            }

            return hardScore;
        }
	}
}
