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
        private Dictionary<string, int> dict;
        

        // to be implemented
        // private int turnCounter;

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
            {"A", 0},
            {"B", 1},
            {"C", 2},
            {"D", 3},
            {"E", 4},
            {"F", 5},
            {"G", 6},
                };
        }


        // METHODS
        public void Iterate()
        {
            int sourceRow;
            string sourceCol;
            int targetRow;
            string targetCol;

            Console.WriteLine("Input: ");
            string input = Console.ReadLine();

            string[] parts = input.Split(' ');
            ParseInput(parts[0], out sourceRow, out sourceCol);
            ParseInput(parts[1], out targetRow, out targetCol);

            // Requires calculation of moveset
            // And validation

            MovePiece(
                Board[sourceRow, Dict[sourceCol]],
                targetRow,
                Dict[targetCol]
                );

            PrintBoard();


            // Split based on space

            // receive user input
            // check if the piece can move there:
            //      Calculate piece moveset
            //      Check if target X,Y is in moveset
            // if valid
            //      Move piece
            //      Render board
            //      Iterate move counter
            //      Switch team
            // if !valid
            //      ask user to try again, return to start of loop
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
            Graveyard = new List<char>();
            int pwRow = 1; // Row for White Pawns
            int pbRow = 6; // Row for Black Pawns
            for(int i = 0; i < 8; i++)
            {
                Board[pwRow, i] = new Pawn(pwRow, i, true);
                Board[pbRow, i] = new Pawn(pbRow, i, false);

            }
        }

        public void PrintBoard()
        {
            Console.WriteLine("\n");
            for(int row = 7; row >= 0; row--) // Reversed to match input for rows
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
            Console.WriteLine("\nGraveyard:\n");
            
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
