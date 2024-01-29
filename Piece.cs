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
        public string Type { get; set; } // Name of Piece
        public char Symbol { get; set; } // Char that is representative of Piece
        public int ID { get; set; }
        public static int Count { get; private set; } // Total number of Pieces in play
        public int Row { get; set; } // Current Row pos of Piece
        public int Col { get; set; } // Current Col pos of Piece
        public bool IsWhite { get; private set; } // Determines team
        public bool Living { get; set; } // True if in play. Currently not used.
        public string Colour { get; set; } // Hex value colour of font that will be printed to console
        public List<(int, int)> Moveset { get; set; } // Holds the currently available moves for current Piece
        public bool FirstMove { get; set; } // False after this Piece's first move is made

        // CONSTRUCTORS
        public Piece(int row, int col, bool isWhite) {
            FirstMove = true;
            Row = row;
            Col = col;
            ID = ++Count;
            IsWhite = isWhite;
        }

        // METHODS

        /// <summary>
        /// Iterates in a single direction to provide 3 outcomes:
        /// if the referenced cell HAS A PIECE and IT IS AN ENEMY, add the cell to the moveset, exit
        /// if the referenced cell HAS A PIECE and IT IS A TEAMMATE, simply exit
        /// otherwise, add the cell to the moveset, and continue iterating
        /// </summary>
        internal void AddMovesInDirection(Piece[,] board, int rowDirection, int colDirection)
        {
            for (int i = 1; IsValidCoordinate(Row + i * rowDirection, Col + i * colDirection, board); i++)
            {
                int tRow = Row + i * rowDirection;
                int tCol = Col + i * colDirection;

                if (board[tRow, tCol] != null && board[tRow, tCol].IsWhite != this.IsWhite) // Target is enemy
                {
                    Moveset.Add((tRow, tCol));
                    break;
                } else if (board[tRow, tCol] != null && board[tRow, tCol].IsWhite == this.IsWhite) // Target is tm
                {
                    break;
                }

                // Target is empty
                Moveset.Add((tRow, tCol));
            }
        }

        /// <summary>
        /// Ensures that the supplied Row and Col values are within the bounds of the board
        /// </summary>
        internal bool IsValidCoordinate(int row, int col, Piece[,] board)
        {
            return row >= 0 && row < board.GetLength(0) && col >= 0 && col < board.GetLength(1);
        }

        /// <summary>
        /// Prints some debug values about the given Piece to the console
        /// </summary>
        public void PrintInfo()
        {
            Console.WriteLine("/ / / / / / / /");
            Console.WriteLine("Info about Piece ID: {0}", ID);
            Console.WriteLine("Type: {0}", Type);
            Console.WriteLine("Symbol: {0}", Symbol);
            Console.WriteLine("Current Location: Row:{0}, Col:{1}", Row, Col);
            Console.WriteLine($"First Move: {FirstMove}");
            Console.WriteLine("Total Pieces in play: {0}",Piece.Count);
            Console.WriteLine("/ / / / / / / /");

        }

        /// <summary>
        /// Calculated each turn. Populates Moveset with all valid moves from the current position
        /// </summary>
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
            Moveset = new List<(int, int)>();

            AddMovesInDirection(board, 1, 1);   // Top-Right Diagonal
            AddMovesInDirection(board, 1, -1);  // Top-Left Diagonal
            AddMovesInDirection(board, -1, 1);  // Bottom-Right Diagonal
            AddMovesInDirection(board, -1, -1); // Bottom-Left Diagonal
        }
    }

    public class Knight : Piece
    {
        public Knight(int row, int col, bool isWhite) : base(row, col, isWhite)
        {
            this.Type = "Knight";
            if (isWhite)
            {
                this.Symbol = 'N'; // ♙
                this.Colour = "#a61782";
            }
            else
            {
                this.Symbol = 'N'; // ♟
                this.Colour = "black";
            }
        }

        public override void CalculateMoveset(Piece[,] board)
        {
            for(int i = 1; i <= 2; i++ )
            {
                // FORGOT TO ADD OUT OF BOUNDS VALIDATION
                if (board[Row+i, Col+(3-i)] == null || board[Row+i, Col+(3-i)].IsWhite != this.IsWhite) 
                {
                    Moveset.Add((Row+i, Col+(3-i)));
                }
                if (board[Row - i, Col + (3 - i)] == null || board[Row - i, Col + (3 - i)].IsWhite != this.IsWhite)
                {
                    Moveset.Add((Row - i, Col + (3 - i)));
                }

                if (board[Row + i, Col - (3 - i)] == null || board[Row + i, Col - (3 - i)].IsWhite != this.IsWhite)
                {
                    Moveset.Add((Row + i, Col - (3 - i)));
                }
                if (board[Row - i, Col - (3 - i)] == null || board[Row - i, Col - (3 - i)].IsWhite != this.IsWhite)
                {
                    Moveset.Add((Row - i, Col - (3 - i)));
                }
            }
        }
    }

    public class Rook : Piece
    {
        public Rook(int row, int col, bool isWhite) : base(row, col, isWhite)
        {
            this.Type = "Rook";
            if (isWhite)
            {
                this.Symbol = 'R'; // ♙
                this.Colour = "#a61782";
            }
            else
            {
                this.Symbol = 'R'; // ♟
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

    }


}
