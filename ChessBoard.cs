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
        private char[,] board;
        private char whiteCell = '\u2591'; // ░ 
        private char blackCell = '\u2588'; // █

        // PROPERTIES


        // CONSTRUCTORS
        public ChessBoard()
        {
            board = new char[8, 8];
        }

        // METHODS
        public void InitBoard()
        {
            for(int i = 0; i < 8; i++)
            {
                for(int ii = 0; ii < 8; ii++)
                {
                    board[i, ii] = '.';
                } 
            }
        }

        public void PrintBoard()
        {
            bool lever = false;
            for (int i = 0; i < 8; i++)
            {
                for (int ii = 0; ii < 8; ii++)
                {
                    if (lever)
                    {
                        Console.Write(whiteCell);
                    } else
                    {
                        Console.Write(blackCell);
                    }
                    lever = !lever;
                }
                Console.WriteLine();
                lever = !lever;
            }
        }

        public void ChangePiece(
            int currentX,
            int currentY,
            int targetX,
            int targetY,
            char Symbol            
            )
        {
            board[currentX, currentY] = '.';
            board[targetX, targetY] = Symbol;
            
        }

        


    }
}
