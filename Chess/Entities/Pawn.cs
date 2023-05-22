using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Chess.Logic;

namespace Chess.Entities
{
    internal class Pawn : Piece
    {
        public Pawn(Position position, string color) : base(position, color)
        {
            if (GetColor() == "black")
                SetSymbol('P');
            else
                SetSymbol('p');

        }

        public override bool isValidMove(Position position, Piece[,] board)
        {
            Position actualPosition = GetPosition();
            int dx = Math.Abs(actualPosition.X - position.X);
            int dy = Math.Abs(actualPosition.Y - position.Y);

            if (GetColor().Equals("black"))
            {
                if (actualPosition.Y < 8)
                {
                    // Pawn moves in front one Tile
                    if (dx == 0 && dy == 1 && actualPosition.Y == position.Y - 1 && board[position.X, position.Y] == null)
                        return true;

                    // Pawn moves 2 tiles if the match just started
                    if (dx == 0 && dy == 2 && actualPosition.Y == 1 &&
                        (actualPosition.Y == position.Y - 1 || actualPosition.Y == position.Y - 2) &&
                        board[position.X, position.Y] == null && board[position.X, position.Y + 1] == null)
                        return true;

                    // Pawn captures diagonally
                    if (dx == 1 && dy == 1 && actualPosition.Y == position.Y - 1 && board[position.X, position.Y] != null &&
                        board[position.X, position.Y].GetColor() != GetColor())
                        return true;
                }
            }
            else if (GetColor().Equals("white"))
            {
                if (actualPosition.Y >= 0)
                {
                    // Pawn moves in front one Tile
                    if (dx == 0 && dy == 1 && actualPosition.Y == position.Y + 1 && board[position.X, position.Y] == null)
                        return true;

                    // Pawn moves 2 tiles if the match just started
                    else
                        if (dx == 0 && dy == 2 && actualPosition.Y == 6 &&
                            (actualPosition.Y == position.Y + 1 || actualPosition.Y == position.Y + 2) &&
                            board[position.X, position.Y] == null && board[position.X, position.Y - 1] == null)
                        return true;
                    else
                    // Pawn captures diagonally
                        if (dx == 1 && dy == 1 && actualPosition.Y == position.Y + 1 && board[position.X, position.Y] != null &&
                            board[position.X, position.Y].GetColor() != GetColor())
                        return true;
                    
                }
            }
            return false;
        }

        //black always begin in the upper part of the board and white on the bottom
        public override void Move(Position position, Piece[,] board)
        {
            Position actualPosition = GetPosition();
            int dx = Math.Abs(actualPosition.X - position.X);
            int dy = Math.Abs(actualPosition.Y - position.Y);

            if (GetColor().Equals("black"))
            {
                if (actualPosition.Y < 8)
                {
                    // Pawn moves in front one Tile
                    if (dx == 0 && dy == 1 && actualPosition.Y == position.Y - 1 && board[position.X, position.Y] == null)
                        SetPosition(position);

                    // Pawn moves 2 tiles if the match just started
                    if (dx == 0 && dy == 2 && actualPosition.Y == 1 &&
                        (actualPosition.Y == position.Y - 1 || actualPosition.Y == position.Y - 2) &&
                        board[position.X, position.Y] == null && board[position.X, position.Y + 1] == null)
                        SetPosition(position);

                    // Pawn captures diagonally
                    if (dx == 1 && dy == 1 && actualPosition.Y == position.Y - 1 && board[position.X, position.Y] != null &&
                        board[position.X, position.Y].GetColor() != GetColor())
                        SetPosition(position);
                }
            }
            else if (GetColor().Equals("white"))
            {
                if (actualPosition.Y >= 0)
                {
                    // Pawn moves in front one Tile
                    if (dx == 0 && dy == 1 && actualPosition.Y == position.Y + 1 && board[position.X, position.Y] == null)
                        SetPosition(position);

                    // Pawn moves 2 tiles if the match just started
                    else
                        if (dx == 0 && dy == 2 && actualPosition.Y == 6 &&
                            (actualPosition.Y == position.Y + 1 || actualPosition.Y == position.Y + 2) &&
                            board[position.X, position.Y] == null && board[position.X, position.Y - 1] == null)
                        SetPosition(position);
                    else
                    // Pawn captures diagonally
                        if (dx == 1 && dy == 1 && actualPosition.Y == position.Y + 1 && board[position.X, position.Y] != null &&
                            board[position.X, position.Y].GetColor() != GetColor())
                        SetPosition(position);
                    else
                        throw new Exception("Not a correct position");
                }
            }
            else
            {
                throw new Exception("Not a correct position");
            }
        }


        public override string ToString()
        {
            return this.GetPosition().getX() + "," + this.GetPosition().getY();
        }
    }
}
