﻿using System;
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
        private Dictionary<string, int> dict;
        private Graveyard grave = new Graveyard();


        // to be implemented
        // private int turnCounter;

        // PROPERTIES
        public Piece[,] Board { get; set; }
    
        public Dictionary<string, int> Dict { get; set; }

        private bool IsWhiteTurn { get; set; }

        private int TurnCounter { get; set; } 

        // CONSTRUCTORS
        public ChessBoard()
        {
            Board = new Piece[8, 8];
            IsWhiteTurn = true;
            TurnCounter = 1;
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

            // Take CLI Input. Validates only the input format
            ReceiveMoveInput(out int sourceRow, out int sourceCol, out int targetRow, out int targetCol);

            if (Board[sourceRow, sourceCol] != null) // if there is a piece to move
            {
                
                // Check if valid move for that piece
                Board[sourceRow, sourceCol].CalculateMoveset(Board);
                if(
                    ValidateMoveInput(Board[sourceRow, sourceCol], targetRow, targetCol)                    ) 
                {
                    MovePiece(Board[sourceRow, sourceCol],targetRow,targetCol);
                    IsWhiteTurn = !IsWhiteTurn;
                    TurnCounter++;
                } else
                {
                    Console.WriteLine("Not a valid move!");
                }
            } else
            {
                AnsiConsole.MarkupLine("[red]There is no piece there![/]");
            }
            
            PrintBoard();
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

        public bool ValidateMoveInput(Piece piece, int targetRow, int targetCol)
        {
            if(piece.IsWhite != IsWhiteTurn)
            {
                return false;
            }
            (int, int) proposedMove = (targetRow, targetCol);
            return(piece.Moveset.Contains(proposedMove));
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
            int pwRow = 1; // Row for White Pawns
            int pbRow = 6; // Row for Black Pawns
            for (int i = 0; i < 8; i++)
            {
                Board[pwRow, i] = new Pawn(pwRow, i, true);
                Board[pbRow, i] = new Pawn(pbRow, i, false);

            }

            
            Board[0, 3] = new Queen(0, 3, true);
            Board[7, 3] = new Queen(7, 3, false);
            Board[0, 0] = new Rook(0, 0, true);
            Board[0, 7] = new Rook(0, 7, true);
            Board[7, 0] = new Rook(7, 0, false);
            Board[7, 7] = new Rook(7, 7, false);
            Board[0, 5] = new Bishop(0, 5, true);
            Board[0, 2] = new Bishop(0, 2, true);
            Board[7, 5] = new Bishop(7, 5, false);
            Board[7, 2] = new Bishop(7, 2, false);
            Board[0, 1] = new Knight(0, 1, true);
            Board[0, 6] = new Knight(0, 6, true);
            Board[7, 1] = new Knight(7, 1, false);
            Board[7, 6] = new Knight(7, 6, false);
            Board[0, 4] = new King(0, 4, true);
            Board[7, 4] = new King(7, 4, false);


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

                    string tileColour = isWhiteTile ? whiteTile : blackTile;
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

            grave.Print();

        }

        public void MovePiece(Piece piece, int targetRow, int targetCol)
        {
            // Clear the current position            
            
            Board[piece.Row, piece.Col] = null;

            if (Board[targetRow, targetCol] != null)
            {
                // Change to "AddTo" and only reference the piece
                grave.AddTo(Board[targetRow, targetCol]);
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
