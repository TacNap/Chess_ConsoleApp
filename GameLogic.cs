using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Chess_ConsoleApp
{
    public class GameLogic
    {
       

        // PROPERTIES
        public Dictionary<string, int> Dict { get; set; }
        private bool IsWhiteTurn { get; set; }
        private int TurnCounter { get; set; }
        public ChessBoard Board { get; set; }
        private Regex CatchAll { get; }

        // CONSTRUCTORS
        public GameLogic()
        {
            IsWhiteTurn = true;
            TurnCounter = 1;
            Board = new ChessBoard();
            CatchAll = new Regex(@"(?:[-]\\w*)|(?:[a-h][1-8] [a-h][1-8])");
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
            // Header
            string turn = IsWhiteTurn ? "White to Move" : "Black to Move";
            var rule = new Rule($"[red]({TurnCounter}) {turn}[/]"); // Refer to team colour & turn counter here
            rule.Style = Style.Parse("red");
            AnsiConsole.Write(rule);

            // Receive & validate CLI input, then execute accordingly.
            ReceiveCLInput();

           Board.PrintBoard();
        }

        /// <summary>
        /// Takes and reads CLI Input to determine which function should be called.
        /// </summary>
        public void ReceiveCLInput()
        {
            string pattern = @"(?:[-]\w*)|(?:[a-hA-H][1-8] [a-hA-H][1-8])";
            string input = Console.ReadLine();
            MatchCollection matches = Regex.Matches(input, pattern);
            if(matches.Count > 0 )
            {
                switch (matches[0].Value.ToLower())
                {
                    case "-save":
                        Console.WriteLine("SAVE!");
                        CommandSave();
                        break;
                    case "-reset":
                        Console.WriteLine("RESET!");
                        CommandReset();
                        break;
                    case "-load":
                        Console.WriteLine("LOAD!");
                        CommandLoad();
                        break;
                    case "-rotate":
                        Console.WriteLine("ROTATE!");
                        CommandRotate();
                        break;
                    default:
                        Console.WriteLine("MOVE!");
                        // match needs to be fed into commandMove
                        CommandMove(matches[0].Value.ToLower());
                        break;
                }
            } else
            {
                Console.WriteLine("INVALID INPUT");
            }

        }

        public void CommandMove(string input)
        {
            FormatMoveInput(input, out int sourceRow, out int sourceCol, out int targetRow, out int targetCol);
            if (Board.Board[sourceRow, sourceCol] != null) // if there is a piece to move
            {

                // Check if valid move for that piece
                Board.Board[sourceRow, sourceCol].CalculateMoveset(Board.Board);
                if (
                    ValidateMoveInput(Board.Board[sourceRow, sourceCol], targetRow, targetCol))
                {
                    Board.MovePiece(Board.Board[sourceRow, sourceCol], targetRow, targetCol);
                    IsWhiteTurn = !IsWhiteTurn;
                    TurnCounter++;
                }
                else
                {
                    Console.WriteLine("Not a valid move!");
                }
            }
            else
            {
                AnsiConsole.MarkupLine("[red]There is no piece there![/]");
            }
        }

        /// <summary>
        /// Takes user input in the form of 'Piece Target'
        /// Splits input accordingly,
        /// Returns 4 ints, referring to Row and Column of Piece and Target. 
        /// </summary>
        /// <param name="sourceRow">Row of piece to move</param>
        /// <param name="sourceCol">Column of piece to move</param>
        /// <param name="targetRow">Row of proposed move</param>
        /// <param name="targetCol">Column of proposed move</param>
        public void FormatMoveInput(string input, out int sourceRow, out int sourceCol, out int targetRow, out int targetCol)
        {
            string S_sourceCol;
            string S_targetCol;

            string[] parts = input.Split(' ');
            ParseInput(parts[0], out sourceRow, out S_sourceCol);
            ParseInput(parts[1], out targetRow, out S_targetCol);

            sourceCol = Dict[S_sourceCol];
            targetCol = Dict[S_targetCol];
        }

        public bool ValidateMoveInput(Piece piece, int targetRow, int targetCol)
        {
            if (piece.IsWhite != IsWhiteTurn)
            {
                return false;
            }
            (int, int) proposedMove = (targetRow, targetCol);
            return (piece.Moveset.Contains(proposedMove));
        }

        public void ParseInput(string input, out int row, out string col)
        {
            // Extract the numeric and alpha parts
            string num = "";
            string alpha = "";

            foreach (char c in input)
            {
                if (char.IsDigit(c))
                    num += c;
                else
                    alpha += c;
            }

            row = int.Parse(num) - 1; // -1 to align with indices
            col = alpha;
        }

        public void CommandLoad()
        {
            Console.WriteLine("To be implemented");
        }

        public void CommandSave()
        {
            Console.WriteLine("To be implemented");
        }
        public void CommandReset()
        {
            Console.WriteLine("To be implemented");
        }
        public void CommandRotate()
        {
            Console.WriteLine("To be implemented");
        }
    }
}
