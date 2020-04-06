using System;
using Generic = System.Collections.Generic;
using LibraryInterfaces = CardGameLibrary.Interfaces;

namespace CardGames
{
    class Program
    {
        static void Main(string[] args)
        { 
            //Ask for user input for game name
            Console.WriteLine("Enter game name:");

            //Accept user input for game name
            string gameName = Console.ReadLine();

            //Add StructureMap for DI
            var container = new StructureMap.Container(x =>
            {
                x.For<LibraryInterfaces.IDeckOfCards>().
                Use<CardGameLibrary.DeckOfCards>()
                .Singleton()
                .Ctor<string>("cardType");
            });

            var deckOfCards = container.With("cardType")
                .EqualTo(gameName)
                .GetInstance<LibraryInterfaces.IDeckOfCards>();

            switch (gameName.ToUpper())
            {
                case CardGameLibrary.Constants.BLACKJACK:
                    PlayBlackJack(deckOfCards);
                    break;
                case CardGameLibrary.Constants.HIGHCARD:
                    PlayHighCard(deckOfCards);
                    break;
                default:
                    Console.WriteLine("Please enter a supported game name(BLACKJACK or HIGHCARD).");
                    break;
            }
        }

        private static void PlayHighCard(LibraryInterfaces.IDeckOfCards deckOfCards)
        {
            var highCardGame = new HighCardGame(deckOfCards);
            //Ask for user input for number of players
            Console.WriteLine("Enter number of players not greater than 52:");
            int numPlayers = 0;
            if (!int.TryParse(Console.ReadLine(), out numPlayers) || numPlayers > CardGameLibrary.Constants.NCARDS)
            {
                Console.WriteLine("Number of players needs to be an integer lesser than 52.");
                return;
            }

            highCardGame.Play(numPlayers);

            foreach (var player in highCardGame.PlayerOfCardsInGame)
            {
                ConsoleWriteLinePlayerScore(player.PlayerNumber, false, player.PlayerScore, player.PlayerCards);
            }

            var winners = highCardGame.EvaluateWinners();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("Winners are ");
            foreach (var winner in winners)
            {
                sb.Append(winner + " ");
            }
            Console.WriteLine(sb);
        }

        //Assumption: BlackJack is usually played in casinos between the dealer and
        //5-7 users, hence, allowing max 7 players. Also, assuming  one hand per player
        private static void PlayBlackJack(CardGameLibrary.Interfaces.IDeckOfCards deckOfCards)
        {
            var blackJackGame = new BlackJackGame(deckOfCards);

            //Ask for user input for number of players
            Console.WriteLine("Enter number of players not greater than 7:");
            int numPlayers = 0;
            if (!int.TryParse(Console.ReadLine(), out numPlayers) || numPlayers > 7)
            {
                Console.WriteLine("Number of players needs to be an integer lesser than 7.");
                return;
            }

            blackJackGame.Play(numPlayers);
            foreach(var player in blackJackGame.PlayerOfCardsInGame)
            {
                if (!player.IsDealer)
                {
                    while (player.PlayerScore < 21 || (player.HasSoftScore && (player.PlayerScore - 10) < 21))
                    {
                        ConsoleWriteLinePlayerScore(player.PlayerNumber, player.HasSoftScore, player.PlayerScore, player.PlayerCards, true);

                        var toHitOrNotToHit = Console.ReadLine();
                        if (toHitOrNotToHit.Trim().ToUpper() == "Y")
                        {
                            blackJackGame.DealForPlayer(player.PlayerNumber);
                        }
                        else
                        {
                            break;
                        }
                    }

                    ConsoleWriteLinePlayerScore(player.PlayerNumber, player.HasSoftScore, player.PlayerScore, player.PlayerCards);

                }
                else
                {
                    while(player.PlayerScore < 17)
                    {
                        ConsoleWriteLineDealerScore(player.HasSoftScore, player.PlayerScore, player.PlayerCards);

                        blackJackGame.DealForPlayer(player.PlayerNumber);
                    }

                    ConsoleWriteLineDealerScore(player.HasSoftScore, player.PlayerScore, player.PlayerCards);
                }
            }

            var winners = blackJackGame.EvaluateWinners();
            var sb = new System.Text.StringBuilder();
            sb.Append("Result of the game is as follows: \n");
            foreach (var winner in winners)
            {
                if (winner.whoWon == CardGameLibrary.DealerOrPlayerOrNone.Dealer)
                {
                    sb.Append("Dealer won against " + winner.playerNumber + ".\n");
                }
                else if (winner.whoWon == CardGameLibrary.DealerOrPlayerOrNone.Player)
                { 
                    sb.Append("Player#" + winner.playerNumber + " won against dealer.\n");
                }
                else
                {
                    sb.Append("Player#" + winner.playerNumber + " and dealer reached a push; no one won.\n");
                }
            }
            Console.WriteLine(sb);
        }

        private static void ConsoleWriteLineDealerScore(bool hasSoftScore, int score,
            Generic.List<LibraryInterfaces.ICard> cards)
        {
            var builder = new System.Text.StringBuilder();
            builder.Append("Dealer ");
            builder.Append("has a ");
            if (hasSoftScore)
            {
                builder.Append("soft ");
            }
            builder.Append("score of ");
            builder.Append(score);
            builder.Append(" with cards: ");
            foreach (var card in cards)
            {
                builder.Append(card.ToString());
                builder.Append(" ");
            }
            Console.WriteLine(builder);
        }

        private static void ConsoleWriteLinePlayerScore(int playerNumber, bool hasSoftScore, int score,
            Generic.List<LibraryInterfaces.ICard> cards, bool hit = false)
        {
            var builder = new System.Text.StringBuilder();
            builder.Append("Player#");
            builder.Append(playerNumber);
            builder.Append(" has a ");
            if (hasSoftScore)
            {
                builder.Append("soft ");
            }
            builder.Append("score of ");
            builder.Append(score);
            builder.Append(" with card(s): ");
            foreach (var card in cards)
            {
                builder.Append(card.ToString());
                builder.Append(" ");
            }
            if (hit)
            {
                builder.Append("Please enter Y/y to hit.");
            }
            Console.WriteLine(builder);
        }
    }
}
