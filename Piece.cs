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

        public List<(int, int)> Moveset
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

    public class Bishop : Piece
    {
        public Bishop(int row, int col, bool isWhite) : base(row, col, isWhite)
        {
            this.Type = "Bishop";
            this.Row = row;
            this.Col = col;
            this.FirstMove = true;
            if (isWhite)
            {
                this.Symbol = 'B'; // ♙
                this.Colour = "#a61782";
            }
            else
            {
                this.Symbol = 'B'; // ♟
                this.Colour = "black";
            }
        }

        public override void CalculateMoveset(Piece[,] board)
        {
            throw new NotImplementedException();
        }
    }

    public class Knight : Piece
    {
        public Knight(int row, int col, bool isWhite) : base(row, col, isWhite)
        {
        }

        public override void CalculateMoveset(Piece[,] board)
        {
            throw new NotImplementedException();
        }
    }

    public class Rook : Piece
    {
        public Rook(int row, int col, bool isWhite) : base(row, col, isWhite)
        {
        }

        public override void CalculateMoveset(Piece[,] board)
        {
            // This can definitely be improved
            Moveset = new List<(int, int)>();

            // Top-Left Diagonal
            for (int i = 1; Row + i < board.GetLength(0) && Col - i >= 0; i++)
            {
                Moveset.Add((Row + i, Col - i));
                if (board[Row + i, Col - i] != null)
                {
                    break;
                }
            }

            // Top-Right Diagonal
            for (int i = 1; Row + i < board.GetLength(0) && Col + i < board.GetLength(0); i++)
            {
                Moveset.Add((Row + i, Col + i));
                if (board[Row + i, Col + i] != null)
                {
                    break;
                }
            }

            // Bottom-Right Diagonal
            for (int i = 1; Row - i >= 0 && Col + i < board.GetLength(0); i++)
            {
                Moveset.Add((Row - i, Col + i));
                if (board[Row - i, Col + i] != null)
                {
                    break;
                }
            }

            // Bottom-Left Diagonal
            for (int i = 1; Row - i >= 0 && Col - i >= 0; i++)
            {
                Moveset.Add((Row - i, Col - i));
                if (board[Row - i, Col - i] != null)
                {
                    break;
                }
            }
        }
    }

    public class Queen : Piece
    {
        // FIELDS


        // PROPERTIES


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
            Moveset = new List<(int, int)>();

            AddMovesInDirection(board, 1, 0);   // Vertical Up
            AddMovesInDirection(board, -1, 0);  // Vertical Down
            AddMovesInDirection(board, 0, 1);   // Right Horizontal
            AddMovesInDirection(board, 0, -1);  // Left Horizontal
            AddMovesInDirection(board, 1, 1);   // Top-Right Diagonal
            AddMovesInDirection(board, 1, -1);  // Top-Left Diagonal
            AddMovesInDirection(board, -1, 1);  // Bottom-Right Diagonal
            AddMovesInDirection(board, -1, -1); // Bottom-Left Diagonal
        }

        // All subclasses should probably implement this
        private void AddMovesInDirection(Piece[,] board, int rowDirection, int colDirection)
        {
            for (int i = 1; IsValidCoordinate(Row + i * rowDirection, Col + i * colDirection, board); i++)
            {
                Moveset.Add((Row + i * rowDirection, Col + i * colDirection));
                if (board[Row + i * rowDirection, Col + i * colDirection] != null)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Ensures the tile being determined is within the bounds of the board.
        /// </summary>
        /// <param name="row">The row of the tile that is being checked by CalculateMoveset</param>
        /// <param name="col">The column of the tile that is being checked by CalculateMoveset</param>
        /// <param name="board"></param>
        /// <returns>true if within bounds of the board, false otherwise</returns>
        private bool IsValidCoordinate(int row, int col, Piece[,] board)
        {
            return row >= 0 && row < board.GetLength(0) && col >= 0 && col < board.GetLength(1);
        }

        


        //public override void CalculateMoveset(Piece[,] board)
        //{
        //    // This can definitely be improved
        //    Moveset = new List<(int, int)>();

        //    // Top-Left Diagonal
        //    // Conditional reads: while next tile is within bounds AND piece (if not null) is on opposition...
        //    for (int i = 1; Row + i < board.GetLength(0) && Col - i >= 0 && board[Row + i, Col - i]?.IsWhite != this.IsWhite; i++)
        //    {
        //        Moveset.Add((Row + i, Col - i));

        //        if (board[Row + i, Col - i] != null)
        //        {
        //            break;
        //        }
        //    }

        //    // Vertical Up
        //    for (int i = 1; Row + i < board.GetLength(0) && board[Row + i, Col]?.IsWhite != this.IsWhite; i++)
        //    {
        //        Moveset.Add((Row + i, Col));

        //        if (board[Row + i, Col] != null)
        //        {
        //            break;
        //        }
        //    }

        //    // Top-Right Diagonal
        //    for (int i = 1; Row + i < board.GetLength(0) && Col + i < board.GetLength(0) && board[Row + i, Col + i]?.IsWhite != this.IsWhite; i++)
        //    {
        //        if (board[Row + i, Col + i] != null)
        //        {
        //            if (board[Row + i, Col + i].IsWhite == this.IsWhite)
        //            {
        //                break;
        //            }
        //            Moveset.Add((Row + i, Col + i));
        //            break; 
        //        }
        //        Moveset.Add((Row + i, Col + i));
        //    }

        //    // Right Horizontal
        //    for (int i = 1; Col + i < board.GetLength(0); i++)
        //    {
        //        Moveset.Add((Row, Col + i));
        //        if (board[Row, Col + i] != null)
        //        {
        //            break; 
        //        }
        //    }

        //    // Bottom-Right Diagonal
        //    for (int i = 1; Row - i >= 0 && Col + i < board.GetLength(0); i++)
        //    {
        //        Moveset.Add((Row - i, Col + i));
        //        if (board[Row - i, Col + i] != null)
        //        {
        //            break;
        //        }
        //    }

        //    // Vertical Down
        //    for (int i = 1; Row - i >= 0; i++)
        //    {
        //        Moveset.Add((Row - i, Col));
        //        if (board[Row - i, Col] != null)
        //        {
        //            break;
        //        }
        //    }

        //    // Bottom-Left Diagonal
        //    for (int i = 1; Row - i >= 0 && Col - i >= 0; i++)
        //    {
        //        Moveset.Add((Row - i, Col - i));
        //        if (board[Row - i, Col - i] != null)
        //        {
        //            break;
        //        }
        //    }

        //    // Left Horizontal 
        //    for (int i = 1; Col - i >= 0; i++)
        //    {
        //        Moveset.Add((Row, Col - i));
        //        if (board[Row, Col - i] != null)
        //        {
        //            break;
        //        }
        //    }

        //}
    }


}
