using System;
namespace CardGameLibrary.Interfaces
{
    public interface IDeckOfCards
    {
        void Shuffle(int number);
        void Shuffle(int number, int i, int j);
        Card Deal();
    }
}
