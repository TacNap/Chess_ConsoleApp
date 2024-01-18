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
        private bool isWhite; // Change to ENUM
        private bool living;

        private static int count;
        
        // PROPERTIES
        // Type: King, Queen etc..
        public string Type
        {
            get;
            set;
        }

        // Symbol to be rendered to console
        public char Symbol
        {
            get;
            set;
        }

        // Unique ID for each instanced object
        public int ID
        {
            get;
            set;
        }

        // Total number of pieces in play
        public static int Count
        {
            get;
            set;

        }

        // X Position on game board
        public int X // Will need validators
        {
            get;
            set;
        }

        // Y Position on game board 
        public int Y // Will need validators
        {
            get;
            set;
        }

        public bool IsWhite
        {
            get;
            private set;
        }

        public bool Living
        {
            get;
            set;

        }

        // CONSTRUCTORS
        public Piece(int x, int y, bool isWhite)
        {
            // Is it good practice to put these here?
            this.X = x;
            this.Y = y;
            this.IsWhite = isWhite;
            this.ID = ++Count;
            this.Living = true;
        }

        // METHODS
        public void PrintInfo()
        {
            Console.WriteLine("/ / / / / / / /");
            Console.WriteLine("Info about this Piece:");
            Console.WriteLine("Type: {0}", Type);
            Console.WriteLine("Symbol: {0}", Symbol);
            Console.WriteLine("ID: {0}", ID);
            Console.WriteLine("Current Location: X:{0}, Y:{1}", X, Y);
            //Console.WriteLine("Total Pieces in play: {0}",Piece.Count);
            Console.WriteLine("/ / / / / / / /");

        }

        public abstract void CalculateMoveset(ChessBoard board);

        
    }

    public class Pawn : Piece
    {
        // FIELDS
        // CONSTRUCTOR
        public Pawn(int x, int y, bool isWhite) : base(x, y, isWhite)
        {
            this.Type = "Pawn";
            this.X = x;
            this.Y = y;
            if (isWhite)
            {
                this.Symbol = 'p'; // ♙
            } else
            {
                this.Symbol = 'P'; // ♟
            }
            
        }

        public override void CalculateMoveset(ChessBoard board)
        {
            // Requires Implementation
        }
    }

    public class Rook : Piece
    {
        // FIELDS
        // CONSTRUCTOR
        public Rook(int x, int y, bool isWhite) : base(x, y, isWhite)
        {
            this.Type = "Rook";
            this.X = x;
            this.Y = y;
            if (isWhite)
            {
                this.Symbol = 'r';
            } else
            {
                this.Symbol = 'R';
            }
        }

        public override void CalculateMoveset(ChessBoard board)
        {
            // moveset array = calculate each direction
            // until a Null is hit

        }
    }
}
