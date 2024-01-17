using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_ConsoleApp
{
    public class ChessBoard
    {
        // FIELDS
        private Piece[,] board;
        private char whiteCell = '\u2591'; // ░ 
        private char blackCell = '\u2588'; // █
        private List<char> graveyard;

        // PROPERTIES
        public Piece[,] Board // Will need validators
        {
            get;
            set;
        }

        public List<char> Graveyard
        {
            get;
            set;
        }

        // CONSTRUCTORS
        public ChessBoard()
        {
           Board = new Piece[8, 8];
        }

        // METHODS
        public void InitBoard()
        {
            Graveyard = new List<char>();
            int pwRow = 1; // Row for White Pawns
            int pbRow = 6; // Row for Black Pawns
            for(int i = 0; i < 8; i++)
            {
                Board[pwRow, i] = new Pawn(pwRow, i, true);
                Board[pbRow, i] = new Pawn(pbRow, i, false);

            }
            Console.WriteLine(Piece.Count);
        }

        public void PrintBoard()
        {
            Console.WriteLine("\n");
            for(int row = 0; row < 8; row++)
            {
                for(int col = 0; col < 8; col++)
                {
                    if (Board[row,col]!= null && Board[row,col].Living)
                    {
                        Console.Write(Board[row, col].Symbol + " ");
                        continue;
                    }
                    else if (Board[row,col]!= null && !Board[row,col].Living)
                    {
                        // Currently, items are added to graveyard on each render
                        // Ideally, Piece objects could add and remove themselves from the graveyard
                        Graveyard.Add(Board[row, col].Symbol);
                    }
                        Console.Write(". ");
                    
                }
                Console.WriteLine();
            }
            // Render graveyard list
            // Will not need to be cleared once Pieces can add / remove themselves from graveyard
            Console.WriteLine("Graveyard:\n");
            
            for(int i = 0; i < Graveyard.Count; i++)
            {
                Console.Write(Graveyard[i] + ", ");
            }
            
            Graveyard.Clear();
            Console.WriteLine("\n");

        }

        public void MovePiece(Piece piece, int targetX, int targetY)
        {
            // Clear the current position
            Board[piece.X, piece.Y] = null;

            // Update the piece's position
            piece.X = targetX;
            piece.Y = targetY;

            // Place the piece at the new position
            Board[targetX, targetY] = piece;

        }

        


    }
}
