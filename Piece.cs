using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_ConsoleApp
{
    public abstract class Piece
    {
        // FIELDS
        private int id;
        private string type;
        private char symbol;
        private int x;
        private int y;

        private static int count;
        
        // PROPERTIES
        public string Type
        {
            get;
            set;
        }

        public char Symbol
        {
            get;
            set;
        }

        public int ID
        {
            get;
            set;
        }

        public static int Count
        {
            get;
            set;

        }

        public int X // Will need validators
        {
            get;
            set;
        }

        public int Y // Will need validators
        {
            get;
            set;
        }

        // CONSTRUCTORS
        public Piece(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        // METHODS
        public void PrintInfo()
        {
            Console.WriteLine("Info about this Piece:");
            Console.WriteLine("Type: {0}", Type);
            Console.WriteLine("Symbol: {0}", Symbol);
            Console.WriteLine("ID: {0}", ID);
            Console.WriteLine("Current Location: {0}, {1}", X, Y);
            Console.WriteLine("Total Pieces in play: {0}",Piece.Count);

        }

        public void MovePiece(ChessBoard board, int targetX, int targetY)
        {
            // board.board[targetX, targetY] 
        }

        
    }

    public class Pawn : Piece
    {
        public Pawn(int x, int y) : base(x, y)
        {
            this.Type = "Pawn";
            this.Symbol = 'P';
            this.ID = Piece.Count;
            Piece.Count++;
        }


    }
}
