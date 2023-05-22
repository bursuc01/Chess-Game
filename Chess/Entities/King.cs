using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Logic;

namespace Chess.Entities
{
    internal class King : Piece
    {
        public King(Position position, string color) : base(position, color)
        {
            if (GetColor() == "black")
                SetSymbol('K');
            else
                SetSymbol('k');
        }

        public override bool isValidMove(Position position, Piece[,] board)
        {
            Piece piece = board[position.getX(), position.getY()];

            Position actualPosition = this.GetPosition();

            int dx = Math.Abs(actualPosition.getX() - position.getX());
            int dy = Math.Abs(actualPosition.getY() - position.getY());

            // Check if the move is a valid king move (one step in any direction)
            if ((dx == 1 && dy == 0) || (dx == 0 && dy == 1) || (dx == 1 && dy == 1))
            {
                if (!piece.GetColor().Equals(this.GetColor()))
                {
                    return true;
                }
            }
            return false;
        }

        public override void Move(Position position, Piece[,] board)
        {
            Piece piece = board[position.getX(), position.getY()];

            Position actualPosition = this.GetPosition();

            int dx = Math.Abs(actualPosition.getX() - position.getX());
            int dy = Math.Abs(actualPosition.getY() - position.getY());

            // Check if the move is a valid king move (one step in any direction)
            if ((dx == 1 && dy == 0) || (dx == 0 && dy == 1) || (dx == 1 && dy == 1))
            {
                if (piece == null || !piece.GetColor().Equals(this.GetColor()))
                {
                    this.SetPosition(position);
                    if (piece != null)
                    {
                        piece.SetPosition(new Position(-1, -1));
                    }
                }
                else
                {
                    throw new Exception("Cannot capture a piece of the same color");
                }
            }
            else
            {
                throw new Exception("Not a correct Position");
            }
        }

        public override string ToString()
        {
            return this.GetPosition().getX() + "," + this.GetPosition().getY();
        }
    }
}
