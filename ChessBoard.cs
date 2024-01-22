using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace Chess_ConsoleApp
{
    public class ChessBoard
    {
        // FIELDS
        private Piece[,] board;
        private char whiteCell = '\u2591'; // ░ 
        private char blackCell = '\u2588'; // █
        private List<string> graveyard;
        private Dictionary<string, int> dict;


        // to be implemented
        // private int turnCounter;

        // PROPERTIES
        public Piece[,] Board // Will need validators
        {
            get;
            set;
        }

        public List<string> Graveyard
        {
            get;
            set;
        }

        public Dictionary<string, int> Dict
        {
            get;
            set;
        }

        // CONSTRUCTORS
        public ChessBoard()
        {
            Board = new Piece[8, 8];
            Dict = new Dictionary<string, int>
                {
            {"a", 0},
            {"b", 1},
            {"c", 2},
            {"d", 3},
            {"e", 4},
            {"f", 5},
            {"g", 6},
            {"h", 7},
                };
        }


        // METHODS
        public void Iterate()
        {
            var rule = new Rule("[red]Input[/]"); // Refer to team colour & turn counter here
            rule.Style = Style.Parse("red");
            AnsiConsole.Write(rule);

            ReceiveMoveInput(out int sourceRow, out int sourceCol, out int targetRow, out int targetCol);
            if (Board[sourceRow, sourceCol] != null)
            {
                Board[sourceRow, sourceCol].CalculateMoveset(Board);
                if(ValidateMoveInput(Board[sourceRow, sourceCol].Moveset, targetRow, targetCol))
                {
                    MovePiece(Board[sourceRow, sourceCol],targetRow,targetCol);
                } else
                {
                    Console.WriteLine("Not a valid move!");
                }
            } else
            {
                AnsiConsole.MarkupLine("[red]There is no piece there![/]");
            }
            
            PrintBoard();


            // Split based on space

            // receive user input
            // check if the piece can move there:
            //      Calculate piece moveset
            //      Check if target Row,Col is in moveset
            // if valid
            //      Move piece
            //      Render board
            //      Iterate move counter
            //      Switch team
            // if !valid
            //      ask user to try again, return to start of loop
        }

        /// <summary>
        /// Takes user input in the form of standard Chess Algebraic notation, 'Piece Target'
        /// Splits input accordingly,
        /// Returns 4 ints, referring to Row and Column of Piece and Target. 
        /// </summary>
        /// <param name="sourceRow">Row of piece to move</param>
        /// <param name="sourceCol">Column of piece to move</param>
        /// <param name="targetRow">Row of proposed move</param>
        /// <param name="targetCol">Column of proposed move</param>
        public void ReceiveMoveInput(out int sourceRow, out int sourceCol, out int targetRow, out int targetCol)
        {
            string S_sourceCol;
            string S_targetCol;
            string input = Console.ReadLine();

            string[] parts = input.Split(' ');
            ParseInput(parts[0], out sourceRow, out S_sourceCol);
            ParseInput(parts[1], out targetRow, out S_targetCol); 

            sourceCol = Dict[S_sourceCol];
            targetCol = Dict[S_targetCol];
        } 

        public bool ValidateMoveInput(List<(int, int)> moveset, int targetRow, int targetCol)
        {

            (int, int) proposedMove = (targetRow, targetCol);
            return(moveset.Contains(proposedMove));
        }

        public void ParseInput(string input, out int row, out string col)
        {
            // Extract the numeric and alpha parts
            string num = "";
            string alpha = "";

            foreach(char c in input)
            {
                if (char.IsDigit(c))
                    num += c;
                else
                    alpha += c;
            }

            row = int.Parse(num)-1; // -1 to align with indices
            col = alpha;
        }

        public void InitBoard()
        {
            Graveyard = new List<string>();
            int pwRow = 1; // Row for White Pawns
            int pbRow = 6; // Row for Black Pawns
            for(int i = 0; i < 8; i++)
            {
                Board[pwRow, i] = new Pawn(pwRow, i, true);
                Board[pbRow, i] = new Pawn(pbRow, i, false);

            }

            Board[0, 3] = new Queen(0, 3, true);
            Board[7, 3] = new Queen(7, 3, false);

        }

        public void PrintBoard()
        {
            bool isWhiteTile = false;
            char tile = '\u2588';
            string blackTile = "#7d9c68"; // Green
            string whiteTile = "#edecd1"; // Cream
            string cellWidth = "     ";
            
            Piece piece;
            string blackStart = $"   [default on {blackTile}]{cellWidth}[/][default on {whiteTile}]{cellWidth}[/][default on {blackTile}]{cellWidth}[/][default on {whiteTile}]{cellWidth}[/][default on {blackTile}]{cellWidth}[/][default on {whiteTile}]{cellWidth}[/][default on {blackTile}]{cellWidth}[/][default on {whiteTile}]{cellWidth}[/]";
            string whiteStart = $"   [default on {whiteTile}]{cellWidth}[/][default on {blackTile}]{cellWidth}[/][default on {whiteTile}]{cellWidth}[/][default on {blackTile}]{cellWidth}[/][default on {whiteTile}]{cellWidth}[/][default on {blackTile}]{cellWidth}[/][default on {whiteTile}]{cellWidth}[/][default on {blackTile}]{cellWidth}[/]";

            Console.WriteLine("\n");
            for(int row = 7; row >= 0; row--) // Reversed such that row 0 is at the bottom
            {
                StringBuilder rowString = new StringBuilder(); 

                for(int col = 0; col < 8; col++)
                {
                    piece = Board[row, col];
                    isWhiteTile = !isWhiteTile; 

                    string tileColour = isWhiteTile ? "#edecd1" : "#7d9c68";
                    string pieceColor = piece != null ? piece.Colour : "default";
                    string pieceSymbol = piece != null ? piece.Symbol.ToString() : " ";

                    rowString.Append($"[{pieceColor} on {tileColour}]  {pieceSymbol}  [/]");

                }
                string bufferRow = isWhiteTile ? blackStart : whiteStart;
                AnsiConsole.MarkupLine(bufferRow);
                Console.Write(row + 1 + "| ");
                AnsiConsole.MarkupLine(rowString.ToString());
                AnsiConsole.MarkupLine(bufferRow);
                //Console.WriteLine();0
                isWhiteTile = !isWhiteTile;
            }
            Console.WriteLine("   ----------------------------------------------");
            Console.WriteLine($"     a    b    c    d    e    f    g    h");

            // TO BE IMPLEMENTED
            Console.WriteLine("\nGraveyard:\n");            
            for(int i = 0; i < Graveyard.Count; i++)
            {
                Console.Write(Graveyard[i] + ", ");
            }
            Console.WriteLine("\n");

        }

        public void MovePiece(Piece piece, int targetRow, int targetCol)
        {
            // Clear the current position            
            
            Board[piece.Row, piece.Col] = null;

            if (Board[targetRow, targetCol] != null)
            {
                Graveyard.Add(Board[targetRow, targetCol].Symbol.ToString());

                Board[targetRow, targetCol] = null;
            }

            // Update the piece's position
            piece.Row = targetRow;
            piece.Col = targetCol;

            // Place the piece at the new position
            Board[targetRow, targetCol] = piece;

            piece.FirstMove = false;

        }

        


    }
}
