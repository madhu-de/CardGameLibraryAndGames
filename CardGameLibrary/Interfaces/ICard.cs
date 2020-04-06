using System;
namespace CardGameLibrary.Interfaces
{
    public interface ICard
    {
        Suit GetSuit();
        int GetRank();
        string ToString();
    }
}
