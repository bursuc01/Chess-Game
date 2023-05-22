using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Logic
{
    public abstract class Piece
    {   
        private Position position;
        private String color;
        private char symbol;

        //black always begin in the upper part of the board and white on the bottom
        public Piece(Position position, string color) { 
            this.position = position;
            this.color = color;
        }

        ////////////////////Getter and Setter zone////////////////////
        public Position GetPosition()
        {
            return this.position;
        }

        public void SetPosition(Position position)
        {
            this.position = position;
        }

        public String GetColor()
        {
            return this.color;
        }
       
        public void SetColor(String color)
        {
            this.color = color;
        }

        public char GetSymbol() { return this.symbol; }

        public void SetSymbol(char symbol) { this.symbol = symbol; }
        //////////////////////////////////////////////////////////////
        

        // Moves a piece by updating it's position
        public abstract void Move(Position position, Piece[,] board);
        
        // Returns the string representation of a Piece
        public override abstract String ToString();

        public abstract bool isValidMove(Position position, Piece[,] board);
        
    }

}
