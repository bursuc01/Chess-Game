using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Logic;

namespace Chess.Entities
{
    internal class Bishop : Piece
    {
        public Bishop(Position position, string color) : base(position, color)
        {
            if (GetColor() == "black")
                SetSymbol('B');
            else
                SetSymbol('b');
        }

        public override bool isValidMove(Position position, Piece[,] board)
        {
             Piece piece = board[position.getX(), position.getY()];

            Position actualPosition = this.GetPosition();

            int dx = Math.Abs(actualPosition.getX() - position.getX());
            int dy = Math.Abs(actualPosition.getY() - position.getY());

            // Check if the move is a valid diagonal move
            if (dx == dy)
            {
                if (IsPathClear(actualPosition, position, board))
                {
                    if (!piece.GetColor().Equals(this.GetColor()))
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

            // Check if the move is a valid diagonal move
            if (dx == dy)
            {
                if (IsPathClear(actualPosition, position, board))
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
                    throw new Exception("There is an obstacle in the path");
                }
            }
            else
            {
                throw new Exception("Not a correct Position");
            }
        }

        public override string ToString()
        {
            return this.GetPosition().getX()+","+this.GetPosition().getY();
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


    }
}
