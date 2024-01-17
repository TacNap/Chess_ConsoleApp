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

            // Testing Moving Pieces
            //board.MovePiece(board.Board[1, 0], 1, 2);
            board.PrintBoard();
            board.MovePiece(board.Board[1, 0], 2, 0);
            board.MovePiece(board.Board[1, 2], 2, 2);
            board.MovePiece(board.Board[1, 7], 3, 7);


            board.PrintBoard();






        }
    }
}