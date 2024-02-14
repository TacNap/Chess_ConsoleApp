using Spectre.Console;
using System;
using System.Text;

namespace Chess_ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialising
            Console.OutputEncoding = Encoding.Unicode;
            GameLogic Master = new GameLogic();
            Master.Board.InitBoard();
            Master.Board.PrintBoard();

            while (true)
            {
                Master.Iterate();
            }





        }
    }
}
