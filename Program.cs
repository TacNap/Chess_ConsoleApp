using Spectre.Console;
using System;
using System.Text;

namespace Chess_ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ChessBoard board = new ChessBoard();
            Console.OutputEncoding = Encoding.Unicode;

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
