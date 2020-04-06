using System;
namespace CardGameLibrary.Interfaces

{
    public interface ICardGame
	{
		Card GetCard();
		void Play(int numPlayers);
		System.Collections.Generic.List<int> EvaluateWinners();
	}
}
