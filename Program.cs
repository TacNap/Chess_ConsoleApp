using System;

namespace Chess_ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Init Pawn Functions
            ChessBoard board = new ChessBoard();

            // Testing Piece[8,8] for ChessBoard
            board.InitBoard();
            board.PrintBoard();
            // User Input
            Console.WriteLine("** Start User Input **");
            board.Iterate();






        }
    }
}