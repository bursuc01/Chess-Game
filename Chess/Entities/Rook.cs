using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Logic;

namespace Chess.Entities
{
    public class Rook : Piece
    {
        public Rook(Position position, string color) : base(position, color)
        {
            if (GetColor() == "black")
                SetSymbol('R');
            else
                SetSymbol('r');
        }

        public override bool isValidMove(Position position, Piece[,] board)
        {
            Piece piece = board[position.getX(), position.getY()];

            Position actualPosition = this.GetPosition();

            int dx = Math.Abs(actualPosition.getX() - position.getX());
            int dy = Math.Abs(actualPosition.getY() - position.getY());

            // Check if the move is a valid line or row move like a rook
            if (piece == null)
            {
                if ((dx == 0) || (dy == 0))
                {
                    // Check if there are any pieces in between
                    if (IsPathClear(actualPosition, position, board))
                    {
                        return true;
                    }
                }
            }
            else if (!piece.GetColor().Equals(this.GetColor()))
            {
                if ((dx == 0) || (dy == 0))
                {
                    // Check if there are any pieces in between
                    if (IsPathClear(actualPosition, position, board))
                    {
                        return true;
                    }
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

            // Check if the move is a valid line or row move like a rook
            if (piece == null)
            {
                if ((dx == 0) || (dy == 0))
                {
                    // Check if there are any pieces in between
                    if (IsPathClear(actualPosition, position, board))
                    {
                        this.SetPosition(position);
                    }
                    else
                    {
                        throw new Exception("Obstacle detected in the path");
                    }
                }
                else
                {
                    throw new Exception("Not a correct Position");
                }
            }
            else if (!piece.GetColor().Equals(this.GetColor()))
            {
                if ((dx == 0) || (dy == 0))
                {
                    // Check if there are any pieces in between
                    if (IsPathClear(actualPosition, position, board))
                    {
                        this.SetPosition(position);
                        piece.SetPosition(new Position(-1, -1));
                    }
                    else
                    {
                        throw new Exception("Obstacle detected in the path");
                    }
                }
                else
                {
                    throw new Exception("Not a correct Position");
                }
            }
            else
            {
                // Piece of the same color encountered
                throw new Exception("Cannot capture a piece of the same color");
            }
        }

        private bool IsPathClear(Position start, Position end, Piece[,] board)
        {
            int dx = Math.Sign(end.getX() - start.getX());
            int dy = Math.Sign(end.getY() - start.getY());

            int currentX = start.getX() + dx;
            int currentY = start.getY() + dy;

            while (currentX != end.getX() || currentY != end.getY())
            {
                if (board[currentX, currentY] != null)
                {
                    // Obstacle detected in the path
                    return false;
                }

                currentX += dx;
                currentY += dy;
            }

            return true;
        }


        public override string ToString()
        {
            return this.GetPosition().getX() + "," + this.GetPosition().getY();
        }

        
    }
}
