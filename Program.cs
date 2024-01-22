using Spectre.Console;
using System;

namespace Chess_ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Init Pawn Functions
            //char[] unicodes = new char[]
            //{
            //    '\u2654',
            //    '\u2655',
            //    '\u2656',
            //    '\u2657',
            //    '\u2658',
            //    '\u2659',
            //    '\u265A',
            //    '\u265B',
            //    '\u265C',
            //    '\u265D',
            //    '\u265E',
            //    '\u265F'
            //};

            //for(int i = 0; i < unicodes.Length; i++)
            //{
            //    Console.WriteLine(unicodes[i]);
            //    AnsiConsole.Markup($"[red]{unicodes[i]}[/]");

            ChessBoard board = new ChessBoard();

            // Testing Piece[8,8] for ChessBoard
            board.InitBoard();
            board.PrintBoard();
            

            while (true)
            {
                board.Iterate();
            }

        }




    }
}
