using NUnit.Framework;
[assembly: LevelOfParallelism(1)]
namespace Tests
{
    public partial class Tests
    {

        [Test]
        public void HighCardGameOnePlayerWinner()
        {
            var highCardGame = new CardGames.HighCardGame
                (new CardGameLibrary.DeckOfCards(CardGameLibrary.Constants.HIGHCARD));
            highCardGame.Play(1);
            var winner = highCardGame.EvaluateWinners();
            Assert.AreEqual(winner.Count, 1);
            Assert.AreEqual(winner[0], 1);
        }

        [Test]
        public void HighCardGameTwoPlayersWinner()
        {
            var deckOfCards = new CardGameLibrary.DeckOfCards(CardGameLibrary.Constants.HIGHCARD);
            deckOfCards.Shuffle(CardGameLibrary.Constants.NCARDS);
            var highCardGame = new CardGames.HighCardGame(deckOfCards);

            var playerOfCard1 = new CardGameLibrary.PlayerOfCard();
            playerOfCard1.PlayerNumber = 1;
            playerOfCard1.PlayerCards.Add(highCardGame.GetCard());
            highCardGame.PlayerOfCardsInGame.Add(playerOfCard1);

            var playerOfCard2 = new CardGameLibrary.PlayerOfCard();
            playerOfCard2.PlayerNumber = 2;
            playerOfCard2.PlayerCards.Add(highCardGame.GetCard());
            highCardGame.PlayerOfCardsInGame.Add(playerOfCard2);

            var evaluatedWinners = highCardGame.EvaluateWinners();
            Assert.IsTrue(evaluatedWinners.Count <= 2);

            for (int i = 0; i < highCardGame.PlayerOfCardsInGame.Count; i++)
            {
                var player = highCardGame.PlayerOfCardsInGame[i];
                Assert.AreEqual(player.PlayerCards.Count, 1);
                if (i > 0)
                {
                    var previousPlayer = highCardGame.PlayerOfCardsInGame[i - 1];
                    if (previousPlayer.PlayerCards[0].GetRank() > player.PlayerCards[0].GetRank())
                    {
                        Assert.AreEqual(evaluatedWinners.Count, 1);
                        Assert.AreEqual(evaluatedWinners[0], 1);
                    }
                    else if (previousPlayer.PlayerCards[0].GetRank() < player.PlayerCards[0].GetRank())
                    {
                        Assert.AreEqual(evaluatedWinners.Count, 1);
                        Assert.AreEqual(evaluatedWinners[0], 2);
                    }
                    else
                    {
                        Assert.AreEqual(evaluatedWinners.Count, 2);
                        Assert.IsTrue(evaluatedWinners.Exists(e => e == 1));
                        Assert.IsTrue(evaluatedWinners.Exists(e => e == 2));
                    }
                }
            }
        }

        [Test]
        public void HighCardGameMultiplePlayersWinner()
        {
            var deckOfCards = new CardGameLibrary.DeckOfCards(CardGameLibrary.Constants.HIGHCARD);
            deckOfCards.Shuffle(CardGameLibrary.Constants.NCARDS);
            var highCardGame = new CardGames.HighCardGame(deckOfCards);

            for (int i = 1; i <= CardGameLibrary.Constants.NCARDS/2; i++)
            {
                var playerOfCard = new CardGameLibrary.PlayerOfCard();
                playerOfCard.PlayerNumber = i;
                playerOfCard.PlayerCards.Add(highCardGame.GetCard());
                highCardGame.PlayerOfCardsInGame.Add(playerOfCard);
            }

            var evaluatedWinners = highCardGame.EvaluateWinners();
            Assert.IsTrue(evaluatedWinners.Count <= 4);

            var winners = new System.Collections.Generic.List<int>();
            int winningRank = 0;
            for (int i = 0; i < highCardGame.PlayerOfCardsInGame.Count; i++)
            {
                var player = highCardGame.PlayerOfCardsInGame[i];
                Assert.AreEqual(player.PlayerCards.Count, 1);
                if (i > 0)
                {
                    var previousPlayer = highCardGame.PlayerOfCardsInGame[i - 1];
                    if (previousPlayer.PlayerCards[0].GetRank() > player.PlayerCards[0].GetRank()
                        && winningRank < previousPlayer.PlayerCards[0].GetRank())
                    {
                        winningRank = previousPlayer.PlayerCards[0].GetRank();
                    }
                    else if (previousPlayer.PlayerCards[0].GetRank() < player.PlayerCards[0].GetRank()
                        && winningRank < player.PlayerCards[0].GetRank())
                    {
                        winningRank = player.PlayerCards[0].GetRank();
                    }
                    else if(winningRank < player.PlayerCards[0].GetRank())
                    {
                        winningRank = player.PlayerCards[0].GetRank();
                    }
                }
            }

            foreach(var player in highCardGame.PlayerOfCardsInGame)
            {
                if (player.PlayerCards[0].GetRank() == winningRank)
                {
                    winners.Add(player.PlayerNumber);
                }
            }

            //CollectionAssert.AreEqual
            Assert.AreEqual(evaluatedWinners.Count, winners.Count);

            foreach (var winner in winners)
            {
                Assert.IsTrue(evaluatedWinners.Exists(e => e == winner));
            }
        }

        [Test]
        public void HighCardGameMaxMultiplePlayersWinner()
        {
            var deckOfCards = new CardGameLibrary.DeckOfCards(CardGameLibrary.Constants.HIGHCARD);
            deckOfCards.Shuffle(CardGameLibrary.Constants.NCARDS);
            var highCardGame = new CardGames.HighCardGame(deckOfCards);

            for (int i = 1; i <= CardGameLibrary.Constants.NCARDS; i++)
            {
                var playerOfCard = new CardGameLibrary.PlayerOfCard();
                playerOfCard.PlayerNumber = i;
                playerOfCard.PlayerCards.Add(highCardGame.GetCard());
                highCardGame.PlayerOfCardsInGame.Add(playerOfCard);
            }

            var evaluatedWinners = highCardGame.EvaluateWinners();
            Assert.AreEqual(evaluatedWinners.Count, 4);

            var winners = new System.Collections.Generic.List<int>();
            int winningRank = 0;
            for (int i = 0; i < highCardGame.PlayerOfCardsInGame.Count; i++)
            {
                var player = highCardGame.PlayerOfCardsInGame[i];
                Assert.AreEqual(player.PlayerCards.Count, 1);
                if (i > 0)
                {
                    var previousPlayer = highCardGame.PlayerOfCardsInGame[i - 1];
                    if (previousPlayer.PlayerCards[0].GetRank() > player.PlayerCards[0].GetRank()
                        && winningRank < previousPlayer.PlayerCards[0].GetRank())
                    {
                        winningRank = previousPlayer.PlayerCards[0].GetRank();
                    }
                    else if (previousPlayer.PlayerCards[0].GetRank() < player.PlayerCards[0].GetRank()
                        && winningRank < player.PlayerCards[0].GetRank())
                    {
                        winningRank = player.PlayerCards[0].GetRank();
                    }
                    else if (winningRank < player.PlayerCards[0].GetRank())
                    {
                        winningRank = player.PlayerCards[0].GetRank();
                    }
                }
            }

            Assert.AreEqual(winningRank, 14);

            foreach (var player in highCardGame.PlayerOfCardsInGame)
            {
                var winningPlayerRank = player.PlayerCards[0].GetRank();

                if (winningPlayerRank == winningRank)
                {
                    winners.Add(player.PlayerNumber);
                }
            }

            //CollectionAssert.AreEqual
            Assert.AreEqual(evaluatedWinners.Count, winners.Count);

            foreach (var winner in winners)
            {
                Assert.IsTrue(evaluatedWinners.Exists(e => e == winner));
            }
        }

        [Test]
        public void BlackJackGameBlackJackA10PlayerWins()
        {
            var deckOfCards = new CardGameLibrary.DeckOfCards(CardGameLibrary.Constants.BLACKJACK);
            deckOfCards.Shuffle(CardGameLibrary.Constants.NCARDS);
            var blackJackGame = new CardGames.BlackJackGame(deckOfCards);

            var playerOfCard = new CardGameLibrary.PlayerOfCard();
            playerOfCard.PlayerNumber = 1;
            var blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Spades, 1);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Hearts, 10);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackGame.PlayerOfCardsInGame.Add(playerOfCard);

            playerOfCard = new CardGameLibrary.PlayerOfCard();
            playerOfCard.PlayerNumber = 2;
            playerOfCard.IsDealer = true;
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Spades, 10);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Diamonds, 10);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackGame.PlayerOfCardsInGame.Add(playerOfCard);


            var evaluatedWinners = blackJackGame.EvaluateWinners();

            Assert.AreEqual(evaluatedWinners.Count, 1);
            Assert.AreEqual(evaluatedWinners[0].playerNumber, 1);
            Assert.AreEqual(evaluatedWinners[0].whoWon, CardGameLibrary.DealerOrPlayerOrNone.Player);
        }

        [Test]
        public void BlackJackGameBlackJackAJPlayerWins()
        {
            var deckOfCards = new CardGameLibrary.DeckOfCards(CardGameLibrary.Constants.BLACKJACK);
            deckOfCards.Shuffle(CardGameLibrary.Constants.NCARDS);
            var blackJackGame = new CardGames.BlackJackGame(deckOfCards);

            var playerOfCard = new CardGameLibrary.PlayerOfCard();
            playerOfCard.PlayerNumber = 1;
            var blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Spades, 1);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Hearts, 11);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackGame.PlayerOfCardsInGame.Add(playerOfCard);

            playerOfCard = new CardGameLibrary.PlayerOfCard();
            playerOfCard.PlayerNumber = 2;
            playerOfCard.IsDealer = true;
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Spades, 10);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Diamonds, 10);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackGame.PlayerOfCardsInGame.Add(playerOfCard);


            var evaluatedWinners = blackJackGame.EvaluateWinners();

            Assert.AreEqual(evaluatedWinners.Count, 1);
            Assert.AreEqual(evaluatedWinners[0].playerNumber, 1);
            Assert.AreEqual(evaluatedWinners[0].whoWon, CardGameLibrary.DealerOrPlayerOrNone.Player);
        }

        [Test]
        public void BlackJackGameBlackJackAQPlayerWins()
        {
            var deckOfCards = new CardGameLibrary.DeckOfCards(CardGameLibrary.Constants.BLACKJACK);
            deckOfCards.Shuffle(CardGameLibrary.Constants.NCARDS);
            var blackJackGame = new CardGames.BlackJackGame(deckOfCards);

            var playerOfCard = new CardGameLibrary.PlayerOfCard();
            playerOfCard.PlayerNumber = 1;
            var blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Spades, 1);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Hearts, 12);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackGame.PlayerOfCardsInGame.Add(playerOfCard);

            playerOfCard = new CardGameLibrary.PlayerOfCard();
            playerOfCard.PlayerNumber = 2;
            playerOfCard.IsDealer = true;
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Spades, 10);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Diamonds, 12);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackGame.PlayerOfCardsInGame.Add(playerOfCard);


            var evaluatedWinners = blackJackGame.EvaluateWinners();

            Assert.AreEqual(evaluatedWinners.Count, 1);
            Assert.AreEqual(evaluatedWinners[0].playerNumber, 1);
            Assert.AreEqual(evaluatedWinners[0].whoWon, CardGameLibrary.DealerOrPlayerOrNone.Player);
        }

        [Test]
        public void BlackJackGameBlackJackAKPlayerWins()
        {
            var deckOfCards = new CardGameLibrary.DeckOfCards(CardGameLibrary.Constants.BLACKJACK);
            deckOfCards.Shuffle(CardGameLibrary.Constants.NCARDS);
            var blackJackGame = new CardGames.BlackJackGame(deckOfCards);

            var playerOfCard = new CardGameLibrary.PlayerOfCard();
            playerOfCard.PlayerNumber = 1;
            var blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Spades, 1);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Hearts, 13);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackGame.PlayerOfCardsInGame.Add(playerOfCard);

            playerOfCard = new CardGameLibrary.PlayerOfCard();
            playerOfCard.PlayerNumber = 2;
            playerOfCard.IsDealer = true;
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Spades, 10);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Diamonds, 10);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackGame.PlayerOfCardsInGame.Add(playerOfCard);


            var evaluatedWinners = blackJackGame.EvaluateWinners();

            Assert.AreEqual(evaluatedWinners.Count, 1);
            Assert.AreEqual(evaluatedWinners[0].playerNumber, 1);
            Assert.AreEqual(evaluatedWinners[0].whoWon, CardGameLibrary.DealerOrPlayerOrNone.Player);
        }

        [Test]
        public void BlackJackGamePlayerJustSoftBustsDealerHardBustsPlayerWins()
        {
            var deckOfCards = new CardGameLibrary.DeckOfCards(CardGameLibrary.Constants.BLACKJACK);
            deckOfCards.Shuffle(CardGameLibrary.Constants.NCARDS);
            var blackJackGame = new CardGames.BlackJackGame(deckOfCards);

            var playerOfCard = new CardGameLibrary.PlayerOfCard();
            playerOfCard.PlayerNumber = 1;
            var blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Spades, 1);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Hearts, 10);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Clubs, 2);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackGame.PlayerOfCardsInGame.Add(playerOfCard);

            playerOfCard = new CardGameLibrary.PlayerOfCard();
            playerOfCard.PlayerNumber = 2;
            playerOfCard.IsDealer = true;
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Spades, 10);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Diamonds, 10);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Clubs, 10);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackGame.PlayerOfCardsInGame.Add(playerOfCard);

            var evaluatedWinners = blackJackGame.EvaluateWinners();

            Assert.AreEqual(evaluatedWinners.Count, 1);
            Assert.AreEqual(evaluatedWinners[0].playerNumber, 1);
            Assert.AreEqual(evaluatedWinners[0].whoWon, CardGameLibrary.DealerOrPlayerOrNone.Player);
        }

        [Test]
        public void BlackJackGameBothBustPlayerLoses()
        {
            var deckOfCards = new CardGameLibrary.DeckOfCards(CardGameLibrary.Constants.BLACKJACK);
            deckOfCards.Shuffle(CardGameLibrary.Constants.NCARDS);
            var blackJackGame = new CardGames.BlackJackGame(deckOfCards);

            var playerOfCard = new CardGameLibrary.PlayerOfCard();
            playerOfCard.PlayerNumber = 1;
            var blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Spades, 9);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Hearts, 9);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Clubs, 9);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackGame.PlayerOfCardsInGame.Add(playerOfCard);

            playerOfCard = new CardGameLibrary.PlayerOfCard();
            playerOfCard.PlayerNumber = 2;
            playerOfCard.IsDealer = true;
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Spades, 10);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Diamonds, 10);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Clubs, 10);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackGame.PlayerOfCardsInGame.Add(playerOfCard);

            var evaluatedWinners = blackJackGame.EvaluateWinners();

            Assert.AreEqual(evaluatedWinners.Count, 1);
            Assert.AreEqual(evaluatedWinners[0].playerNumber, 1);
            Assert.AreEqual(evaluatedWinners[0].whoWon, CardGameLibrary.DealerOrPlayerOrNone.Dealer);
        }

        [Test]
        public void BlackJackGamePlayerBustsPlayerLoses()
        {
            var deckOfCards = new CardGameLibrary.DeckOfCards(CardGameLibrary.Constants.BLACKJACK);
            deckOfCards.Shuffle(CardGameLibrary.Constants.NCARDS);
            var blackJackGame = new CardGames.BlackJackGame(deckOfCards);

            var playerOfCard = new CardGameLibrary.PlayerOfCard();
            playerOfCard.PlayerNumber = 1;
            var blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Spades, 1);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Hearts, 10);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Clubs, 2);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackGame.PlayerOfCardsInGame.Add(playerOfCard);

            playerOfCard = new CardGameLibrary.PlayerOfCard();
            playerOfCard.PlayerNumber = 2;
            playerOfCard.IsDealer = true;
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Spades, 10);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Diamonds, 2);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Hearts, 2);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackGame.PlayerOfCardsInGame.Add(playerOfCard);

            var evaluatedWinners = blackJackGame.EvaluateWinners();

            Assert.AreEqual(evaluatedWinners.Count, 1);
            Assert.AreEqual(evaluatedWinners[0].playerNumber, 1);
            Assert.AreEqual(evaluatedWinners[0].whoWon, CardGameLibrary.DealerOrPlayerOrNone.Dealer);
        }

        [Test]
        public void BlackJackGamePlayerLosesWithLessScore()
        {
            var deckOfCards = new CardGameLibrary.DeckOfCards(CardGameLibrary.Constants.BLACKJACK);
            deckOfCards.Shuffle(CardGameLibrary.Constants.NCARDS);
            var blackJackGame = new CardGames.BlackJackGame(deckOfCards);

            var playerOfCard = new CardGameLibrary.PlayerOfCard();
            playerOfCard.PlayerNumber = 1;
            var blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Spades, 1);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Hearts, 10);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Clubs, 2);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackGame.PlayerOfCardsInGame.Add(playerOfCard);

            playerOfCard = new CardGameLibrary.PlayerOfCard();
            playerOfCard.PlayerNumber = 2;
            playerOfCard.IsDealer = true;
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Spades, 10);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Diamonds, 10);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Hearts, 1);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackGame.PlayerOfCardsInGame.Add(playerOfCard);

            var evaluatedWinners = blackJackGame.EvaluateWinners();

            Assert.AreEqual(evaluatedWinners.Count, 1);
            Assert.AreEqual(evaluatedWinners[0].playerNumber, 1);
            Assert.AreEqual(evaluatedWinners[0].whoWon, CardGameLibrary.DealerOrPlayerOrNone.Dealer);
        }

        [Test]
        public void BlackJackGameDealerBustsPlayerDoesNotWithSoftScoreAndWins()
        {
            var deckOfCards = new CardGameLibrary.DeckOfCards(CardGameLibrary.Constants.BLACKJACK);
            deckOfCards.Shuffle(CardGameLibrary.Constants.NCARDS);
            var blackJackGame = new CardGames.BlackJackGame(deckOfCards);

            var playerOfCard = new CardGameLibrary.PlayerOfCard();
            playerOfCard.PlayerNumber = 1;
            var blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Spades, 1);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Hearts, 10);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Clubs, 2);
            playerOfCard.PlayerCards.Add(blackJackCard);
            playerOfCard.HasSoftScore = true;
            blackJackGame.PlayerOfCardsInGame.Add(playerOfCard);

            playerOfCard = new CardGameLibrary.PlayerOfCard();
            playerOfCard.PlayerNumber = 2;
            playerOfCard.IsDealer = true;
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Spades, 10);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Diamonds, 10);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Clubs, 10);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackGame.PlayerOfCardsInGame.Add(playerOfCard);

            var evaluatedWinners = blackJackGame.EvaluateWinners();

            Assert.AreEqual(evaluatedWinners.Count, 1);
            Assert.AreEqual(evaluatedWinners[0].playerNumber, 1);
            Assert.AreEqual(evaluatedWinners[0].whoWon, CardGameLibrary.DealerOrPlayerOrNone.Player);
        }

        [Test]
        public void BlackJackGameDealerBustsPlayerDoesNotAndWins()
        {
            var deckOfCards = new CardGameLibrary.DeckOfCards(CardGameLibrary.Constants.BLACKJACK);
            deckOfCards.Shuffle(CardGameLibrary.Constants.NCARDS);
            var blackJackGame = new CardGames.BlackJackGame(deckOfCards);

            var playerOfCard = new CardGameLibrary.PlayerOfCard();
            playerOfCard.PlayerNumber = 1;
            var blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Spades, 2);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Hearts, 10);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Clubs, 2);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackGame.PlayerOfCardsInGame.Add(playerOfCard);

            playerOfCard = new CardGameLibrary.PlayerOfCard();
            playerOfCard.PlayerNumber = 2;
            playerOfCard.IsDealer = true;
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Spades, 10);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Diamonds, 10);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Clubs, 10);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackGame.PlayerOfCardsInGame.Add(playerOfCard);

            var evaluatedWinners = blackJackGame.EvaluateWinners();

            Assert.AreEqual(evaluatedWinners.Count, 1);
            Assert.AreEqual(evaluatedWinners[0].playerNumber, 1);
            Assert.AreEqual(evaluatedWinners[0].whoWon, CardGameLibrary.DealerOrPlayerOrNone.Player);
        }

        [Test]
        public void BlackJackGamePlayerNotBustWithScoreGreaterThanDealerPlayerWins()
        {
            var deckOfCards = new CardGameLibrary.DeckOfCards(CardGameLibrary.Constants.BLACKJACK);
            deckOfCards.Shuffle(CardGameLibrary.Constants.NCARDS);
            var blackJackGame = new CardGames.BlackJackGame(deckOfCards);

            var playerOfCard = new CardGameLibrary.PlayerOfCard();
            playerOfCard.PlayerNumber = 1;
            var blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Spades, 11);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Hearts, 10);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackGame.PlayerOfCardsInGame.Add(playerOfCard);

            playerOfCard = new CardGameLibrary.PlayerOfCard();
            playerOfCard.PlayerNumber = 2;
            playerOfCard.IsDealer = true;
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Spades, 10);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Diamonds, 9);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackGame.PlayerOfCardsInGame.Add(playerOfCard);

            var evaluatedWinners = blackJackGame.EvaluateWinners();

            Assert.AreEqual(evaluatedWinners.Count, 1);
            Assert.AreEqual(evaluatedWinners[0].playerNumber, 1);
            Assert.AreEqual(evaluatedWinners[0].whoWon, CardGameLibrary.DealerOrPlayerOrNone.Player);
        }

        [Test]
        public void BlackJackGameBothBlackJackPushNoOneWins()
        {
            var deckOfCards = new CardGameLibrary.DeckOfCards(CardGameLibrary.Constants.BLACKJACK);
            deckOfCards.Shuffle(CardGameLibrary.Constants.NCARDS);
            var blackJackGame = new CardGames.BlackJackGame(deckOfCards);

            var playerOfCard = new CardGameLibrary.PlayerOfCard();
            playerOfCard.PlayerNumber = 1;
            var blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Spades, 1);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Hearts, 10);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackGame.PlayerOfCardsInGame.Add(playerOfCard);

            playerOfCard = new CardGameLibrary.PlayerOfCard();
            playerOfCard.PlayerNumber = 2;
            playerOfCard.IsDealer = true;
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Clubs, 1);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Diamonds, 10);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackGame.PlayerOfCardsInGame.Add(playerOfCard);

            var evaluatedWinners = blackJackGame.EvaluateWinners();

            Assert.AreEqual(evaluatedWinners.Count, 1);
            Assert.AreEqual(evaluatedWinners[0].playerNumber, 1);
            Assert.AreEqual(evaluatedWinners[0].whoWon, CardGameLibrary.DealerOrPlayerOrNone.None);
        }

        [Test]
        public void BlackJackGameSameSoftScorePushNoOneWins()
        {
            var deckOfCards = new CardGameLibrary.DeckOfCards(CardGameLibrary.Constants.BLACKJACK);
            deckOfCards.Shuffle(CardGameLibrary.Constants.NCARDS);
            var blackJackGame = new CardGames.BlackJackGame(deckOfCards);

            var playerOfCard = new CardGameLibrary.PlayerOfCard();
            playerOfCard.PlayerNumber = 1;
            var blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Spades, 1);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Hearts, 10);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Clubs, 2);
            playerOfCard.PlayerCards.Add(blackJackCard);
            playerOfCard.HasSoftScore = true;
            blackJackGame.PlayerOfCardsInGame.Add(playerOfCard);

            playerOfCard = new CardGameLibrary.PlayerOfCard();
            playerOfCard.PlayerNumber = 2;
            playerOfCard.IsDealer = true;
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Diamonds, 1);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Clubs, 10);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Hearts, 2);
            playerOfCard.PlayerCards.Add(blackJackCard);
            playerOfCard.HasSoftScore = true;
            blackJackGame.PlayerOfCardsInGame.Add(playerOfCard);

            var evaluatedWinners = blackJackGame.EvaluateWinners();

            Assert.AreEqual(evaluatedWinners.Count, 1);
            Assert.AreEqual(evaluatedWinners[0].playerNumber, 1);
            Assert.AreEqual(evaluatedWinners[0].whoWon, CardGameLibrary.DealerOrPlayerOrNone.None);
        }

        [Test]
        public void BlackJackGameSameScorePushNoOneWins()
        {
            var deckOfCards = new CardGameLibrary.DeckOfCards(CardGameLibrary.Constants.BLACKJACK);
            deckOfCards.Shuffle(CardGameLibrary.Constants.NCARDS);
            var blackJackGame = new CardGames.BlackJackGame(deckOfCards);

            var playerOfCard = new CardGameLibrary.PlayerOfCard();
            playerOfCard.PlayerNumber = 1;
            var blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Spades, 3);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Hearts, 10);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Clubs, 2);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackGame.PlayerOfCardsInGame.Add(playerOfCard);

            playerOfCard = new CardGameLibrary.PlayerOfCard();
            playerOfCard.PlayerNumber = 2;
            playerOfCard.IsDealer = true;
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Diamonds, 3);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Clubs, 10);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Hearts, 2);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackGame.PlayerOfCardsInGame.Add(playerOfCard);

            var evaluatedWinners = blackJackGame.EvaluateWinners();

            Assert.AreEqual(evaluatedWinners.Count, 1);
            Assert.AreEqual(evaluatedWinners[0].playerNumber, 1);
            Assert.AreEqual(evaluatedWinners[0].whoWon, CardGameLibrary.DealerOrPlayerOrNone.None);
        }

        [Test]
        public void BlackJackGamePlayerWithTwoAcesInHandNotBustWinner()
        {
            var deckOfCards = new CardGameLibrary.DeckOfCards(CardGameLibrary.Constants.BLACKJACK);
            deckOfCards.Shuffle(CardGameLibrary.Constants.NCARDS);
            var blackJackGame = new CardGames.BlackJackGame(deckOfCards);

            var playerOfCard = new CardGameLibrary.PlayerOfCard();
            playerOfCard.PlayerNumber = 1;
            var blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Spades, 1);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Hearts, 1);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Clubs, 2);
            playerOfCard.PlayerCards.Add(blackJackCard);
            playerOfCard.HasSoftScore = true;
            blackJackGame.PlayerOfCardsInGame.Add(playerOfCard);

            playerOfCard = new CardGameLibrary.PlayerOfCard();
            playerOfCard.PlayerNumber = 2;
            playerOfCard.IsDealer = true;
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Diamonds, 2);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Clubs, 9);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Hearts, 2);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackGame.PlayerOfCardsInGame.Add(playerOfCard);

            var evaluatedWinners = blackJackGame.EvaluateWinners();

            Assert.AreEqual(evaluatedWinners.Count, 1);
            Assert.AreEqual(evaluatedWinners[0].playerNumber, 1);
            Assert.AreEqual(evaluatedWinners[0].whoWon, CardGameLibrary.DealerOrPlayerOrNone.Player);
        }

        [Test]
        public void BlackJackGamePlayerWithThreeAcesInHandNotBustWinner()
        {
            var deckOfCards = new CardGameLibrary.DeckOfCards(CardGameLibrary.Constants.BLACKJACK);
            deckOfCards.Shuffle(CardGameLibrary.Constants.NCARDS);
            var blackJackGame = new CardGames.BlackJackGame(deckOfCards);

            var playerOfCard = new CardGameLibrary.PlayerOfCard();
            playerOfCard.PlayerNumber = 1;
            var blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Spades, 1);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Hearts, 1);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Clubs, 1);
            playerOfCard.PlayerCards.Add(blackJackCard);
            playerOfCard.HasSoftScore = true;
            blackJackGame.PlayerOfCardsInGame.Add(playerOfCard);

            playerOfCard = new CardGameLibrary.PlayerOfCard();
            playerOfCard.PlayerNumber = 2;
            playerOfCard.IsDealer = true;
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Diamonds, 2);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Clubs, 8);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackCard = new CardGameLibrary.BlackJackCard(CardGameLibrary.Suit.Hearts, 2);
            playerOfCard.PlayerCards.Add(blackJackCard);
            blackJackGame.PlayerOfCardsInGame.Add(playerOfCard);

            var evaluatedWinners = blackJackGame.EvaluateWinners();

            Assert.AreEqual(evaluatedWinners.Count, 1);
            Assert.AreEqual(evaluatedWinners[0].playerNumber, 1);
            Assert.AreEqual(evaluatedWinners[0].whoWon, CardGameLibrary.DealerOrPlayerOrNone.Player);
        }
    }
}