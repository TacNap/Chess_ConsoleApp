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
        private int row;
        private int col;
        private bool isWhite; // Change to ENUM
        private bool living;
        private bool firstMove;

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

        // Row Position on game board
        public int Row // Will need validators
        {
            get;
            set;
        }

        // Column Position on game board 
        public int Col // Will need validators
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

        public string Colour
        {
            get;
            set;
        }

        public abstract List<(int, int)> Moveset
        {
            get;
            set;
        }

        public bool FirstMove
        {
            get;
            set;
        }

        // CONSTRUCTORS
        public Piece(int row, int col, bool isWhite)
        {
            // Is it good practice to put these here?
            this.Row = row;
            this.Col = col;
            this.IsWhite = isWhite;
            this.ID = ++Count;
            this.Living = true;
            this.firstMove = true;
        }

        // METHODS
        public void PrintInfo()
        {
            Console.WriteLine("/ / / / / / / /");
            Console.WriteLine("Info about this Piece:");
            Console.WriteLine("Type: {0}", Type);
            Console.WriteLine("Symbol: {0}", Symbol);
            Console.WriteLine("ID: {0}", ID);
            Console.WriteLine("Current Location: Row:{0}, Col:{1}", Row, Col);
            //Console.WriteLine("Total Pieces in play: {0}",Piece.Count);
            Console.WriteLine("/ / / / / / / /");

        }

        public abstract void CalculateMoveset(Piece[,] board);

        
    }

    public class Pawn : Piece
    {
        // FIELDS
        

        // PROPERTIES
        
        // Holds the currently available spaces to move to
        public override List<(int, int)> Moveset {
            get;
            set;
        }

        // CONSTRUCTOR
        public Pawn(int row, int col, bool isWhite) : base(row, col, isWhite)
        {
            this.Type = "Pawn";
            this.Row = row;
            this.Col = col;
            this.FirstMove = true;
            if (isWhite)
            {
                this.Symbol = 'P'; // ♙
                this.Colour = "#a61782";
            } else
            {
                this.Symbol = 'P'; // ♟
                this.Colour = "black";
            }
            
        }

        public override void CalculateMoveset(Piece[,] board)
        {
            Moveset = new List<(int, int)>();

            if(this.IsWhite)
            {
                // White Pawn Movement
                // Move 2 spaces
                if(
                    FirstMove && 
                    board[Row + 1, Col] == null &&
                    board[Row + 2, Col ] == null
                    )
                {
                    Moveset.Add((Row + 2, Col));
                }

                // Move 1 space
                if (
                    Row + 1 <= 7 &&
                    board[Row + 1, Col] == null
                    )
                {
                    Moveset.Add((Row + 1, Col));
                }

                // Attacking moves
                // Forward-Left
                if (
                    Col - 1 >= 0 && // Within bounds
                    Row + 1 <= 7 &&
                    board[Row + 1, Col - 1] != null && // If there's a piece there
                    board[Row + 1, Col - 1].IsWhite != this.IsWhite // If it's the opposition
                    )
                {
                    Moveset.Add((Row + 1, Col - 1));
                }

                // Forward-Right
                if (
                    Col + 1 <= 7 &&
                    Row + 1 <= 7 &&
                    board[Row + 1, Col + 1] != null && 
                    board[Row + 1, Col + 1].IsWhite != this.IsWhite
                    )
                {
                    Moveset.Add((Row + 1, Col + 1));
                }
            } else
            {
                // Black Pawn Movement
                // Move 2 spaces
                if (
                    FirstMove &&
                    board[Row - 1, Col] == null &&
                    board[Row - 2, Col] == null
                    )
                {
                    Moveset.Add((Row - 2, Col));
                }

                // Move 1 space
                if (
                    Row - 1 >= 0 &&
                    board[Row - 1, Col] == null
                    )
                {
                    Moveset.Add((Row - 1, Col));
                }

                // Attacking moves
                // Forward-Left
                if (
                    Col - 1 >= 0 && // Within bounds
                    Row - 1 >= 0 &&
                    board[Row - 1, Col - 1] != null && // If there's a piece there
                    board[Row - 1, Col - 1].IsWhite != this.IsWhite // If it's the opposition
                    )
                {
                    Moveset.Add((Row - 1, Col - 1));
                }

                // Forward-Right
                if (
                    Col + 1 <= 7 &&
                    Row - 1 >= 0 &&
                    board[Row - 1, Col + 1] != null &&
                    board[Row - 1, Col + 1].IsWhite != this.IsWhite
                    )
                {
                    Moveset.Add((Row - 1, Col + 1));
                }
            }

        }
    }

    public class Queen : Piece
    {
        // FIELDS


        // PROPERTIES

        // Holds the currently available spaces to move to
        public override List<(int, int)> Moveset
        {
            get;
            set;
        }

        // CONSTRUCTOR
        public Queen(int row, int col, bool isWhite) : base(row, col, isWhite)
        {
            this.Type = "Queen";
            this.Row = row;
            this.Col = col;
            this.FirstMove = true;
            if (isWhite)
            {
                this.Symbol = 'Q'; // ♙
                this.Colour = "#a61782";
            }
            else
            {
                this.Symbol = 'Q'; // ♟
                this.Colour = "black";
            }

        }
        public override void CalculateMoveset(Piece[,] board)
        {
            throw new NotImplementedException();
        }
    }


}
