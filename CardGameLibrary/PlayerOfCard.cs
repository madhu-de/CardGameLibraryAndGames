using System;
using Generic = System.Collections.Generic;
namespace CardGameLibrary
{
	public class PlayerOfCard
	{
		public int PlayerNumber { get; set; }
        public bool IsDealer { get; set; }
        public bool HasSoftScore { get; set; }
        public int NumSoftCards { get; set; }
        public Generic.List<Interfaces.ICard> PlayerCards { get; private set; }
        public int PlayerScore
        {
            get
            {
                int score = 0;
                foreach(var playerCard in PlayerCards)
                {
                    var rank = playerCard.GetRank();

                    if (rank == 1)
                    {
                        HasSoftScore = true;
                        NumSoftCards++;
                        score += rank + 10; //Ace treated as 11 (soft) instead of 1 (hard)
                    }
                    else
                    {
                        score += rank;
                    }
                }
                return score;
            }
        }

		public PlayerOfCard()
		{
			PlayerCards = new Generic.List<Interfaces.ICard>();
		}
	}
}
