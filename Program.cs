using System;

namespace Chess_ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Pawn p1 = new Pawn(1, 1);
            Pawn p2 = new Pawn(1, 2);
            Pawn p3 = new Pawn(1, 3);
            Pawn p4 = new Pawn(1, 4);
            p1.PrintInfo();
            p2.PrintInfo();
            ChessBoard board = new ChessBoard();
            board.InitBoard();
            board.PrintBoard();

           



        }
    }
}