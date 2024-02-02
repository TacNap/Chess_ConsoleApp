using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace Chess_ConsoleApp
{
    internal class Graveyard
    {
        // FIELDS
        private string whiteColour = "#a61782";
        private string blackColour = "black";

        // PROPERTIES
        public List<char> GraveyardWhite { get; set; }
        public List<char> GraveyardBlack { get; set; }

        // CONSTRUCTORS
        public Graveyard()
        {
            GraveyardWhite = new List<char>();
            GraveyardBlack = new List<char>();
        }

        // METHODS

        
        public void AddTo(Piece piece)
        {
            if(piece.IsWhite)
            {
                GraveyardWhite.Add(piece.Symbol);
            } else
            {
                GraveyardBlack.Add(piece.Symbol);
            }
        }
        // public void Print

        public void Print()
        {
            string infoColour = "#ff8000";
            AnsiConsole.MarkupLine($"[{infoColour} on default]Graveyard[/]");
            AnsiConsole.MarkupLine($"[{infoColour} on default]White:[/]");
            for(int i = 0; i < GraveyardWhite.Count; i++)
            {
                AnsiConsole.Markup($"[{whiteColour} on default]{GraveyardWhite[i]} [/]");
                
            }
            AnsiConsole.MarkupLine($"[{infoColour} on default]\nBlack:[/]");
            for (int i = 0; i < GraveyardBlack.Count; i++)
            {
                AnsiConsole.Markup($"[{blackColour} on white]{GraveyardBlack[i]} [/]");
                
            }
            Console.WriteLine();
        }

    }
}
