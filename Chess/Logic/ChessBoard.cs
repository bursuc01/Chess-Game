using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Chess.Entities;

namespace Chess.Logic
{
    public class ChessBoard
    {
        String fullpath = @"E:\Projects\ChessProject\Chess\Data\board.txt";
        String fullpath2 = @"E:\Projects\ChessProject\Chess\Data\turn.txt";
        //board
        private Piece[,] board;
        //white pieces
        private List<Piece> pawnsW;
        private Piece rook1W;
        private Piece rook2W;
        private Piece knight1W;
        private Piece knight2W;
        private Piece bishop1W;
        private Piece bishop2W;
        private Piece queenW;
        private Piece kingW;

        //black pieces
        private List<Piece> pawnsB;
        private Piece rook1B;
        private Piece rook2B;
        private Piece knight1B;
        private Piece knight2B;
        private Piece bishop1B;
        private Piece bishop2B;
        private Piece queenB;
        private Piece kingB;


        private bool turn;

        Dictionary<Piece, string> pieceToFenSymbol;

        public ChessBoard()
        {
            board = new Piece[8, 8];
            
            Setup();
            pieceToFenSymbol = new Dictionary<Piece, string>()
            {
                { rook1B, " - " },
                { knight1B, "-" },
                { bishop1B, "-" },
                { rook2B, "+" },
                { knight2B, "+" },
                { bishop2B, "+" },
                { rook1W, "-" },
                { knight1W, "-" },
                { bishop1W, "-" },
                { rook2W, "+" },
                { knight2W, "+" },
                { bishop2W, "+" },
                { pawnsB[0], "!" },
                { pawnsB[1], "@" },
                { pawnsB[2], "#" },
                { pawnsB[3], "$" },
                { pawnsB[4], "%" },
                { pawnsB[5], "^" },
                { pawnsB[6], "&" },
                { pawnsB[7], "*" },
                { pawnsW[0], "!" },
                { pawnsW[1], "@" },
                { pawnsW[2], "#" },
                { pawnsW[3], "$" },
                { pawnsW[4], "%" },
                { pawnsW[5], "^" },
                { pawnsW[6], "&" },
                { pawnsW[7], "*" },
            };

        }

        // Moves a specific piece and manages turn and checks
        public void Game(string name, Position pos)
        {
            if (File.ReadAllText(fullpath2) == "True")
                turn = true;
            else
                turn = false;

            // If there is no check, the movement is as usual
            if (!CheckW() && !CheckB())
            {
                if (turn)
                {
                    MoveBlack(name, pos);
                    turn = false;
                }
                else
                {
                    MoveWhite(name, pos);
                    turn = true;
                }
            }
            else
            // If it detects check, take special actions 
            if (CheckB())
            {
               
                Position auxPos = kingB.GetPosition();
                Piece piece = MoveBlack(name, pos);
                if (CheckB())
                {
                    piece.SetPosition(auxPos);
                    UpdateBoard();
                    throw new Exception("Check for the black king");
                }
     
            }
            else
            {
                
                Position auxPos = kingW.GetPosition();
                Piece piece = MoveWhite(name, pos);
                if (CheckW())
                {
                    piece.SetPosition(auxPos);
                    UpdateBoard();
                    throw new Exception("Check for the white king");
                }

            }

            File.WriteAllText(fullpath2, turn.ToString());
        }

        // A switch case for the black pieces
        private Piece MoveBlack(String name, Position pos)
        {
            switch (name)
            {
                // Black pieces
                case "pawn0B":
                    pawnsB.ElementAt(0).Move(pos, board);
                    return pawnsB.ElementAt(0);
                    
                case "pawn1B":
                    pawnsB.ElementAt(1).Move(pos, board);
                    return pawnsB.ElementAt(1);
                    
                case "pawn2B":
                    pawnsB.ElementAt(2).Move(pos, board);
                    return pawnsB.ElementAt(2);

                case "pawn3B":
                    pawnsB.ElementAt(3).Move(pos, board);
                    return pawnsB.ElementAt(3);

                case "pawn4B":
                    pawnsB.ElementAt(4).Move(pos, board);
                    return pawnsB.ElementAt(4);

                case "pawn5B":
                    pawnsB.ElementAt(5).Move(pos, board);
                    return pawnsB.ElementAt(5);

                case "pawn6B":
                    pawnsB.ElementAt(6).Move(pos, board);
                    return pawnsB.ElementAt(6);

                case "pawn7B":
                    pawnsB.ElementAt(7).Move(pos, board);
                    return pawnsB.ElementAt(7);

                case "rook1B":
                    rook1B.Move(pos, board);
                    return rook1B;

                case "knight1B":
                    knight1B.Move(pos, board);
                    return knight1B;

                case "bishop1B":
                    bishop1B.Move(pos, board);
                    return bishop1B;

                case "rook2B":
                    rook2B.Move(pos, board);
                    return rook2B;

                case "knight2B":
                    knight2B.Move(pos, board);
                    return knight2B;

                case "bishop2B":
                    bishop2B.Move(pos, board);
                    return bishop2B;

                case "kingB":
                    kingB.Move(pos, board);
                    return kingB;

                case "queenB":
                    queenB.Move(pos, board);
                    return queenB;

                default:
                    throw new Exception("Not white's turn");

            }
        }

        // A switch case for the white pieces
        private Piece MoveWhite(String name, Position pos)
        {
            switch (name)
            {
                // White pieces
                case "pawn0W":
                    pawnsW.ElementAt(0).Move(pos, board);
                    return pawnsW.ElementAt(0);

                case "pawn1W":
                    pawnsW.ElementAt(1).Move(pos, board);
                    return pawnsW.ElementAt(1);

                case "pawn2W":
                    pawnsW.ElementAt(2).Move(pos, board);
                    return pawnsW.ElementAt(2);

                case "pawn3W":
                    pawnsW.ElementAt(3).Move(pos, board);
                    return pawnsW.ElementAt(3);

                case "pawn4W":
                    pawnsW.ElementAt(4).Move(pos, board);
                    return pawnsW.ElementAt(4);
                    
                case "pawn5W":
                    pawnsW.ElementAt(5).Move(pos, board);
                    return pawnsW.ElementAt(5);

                case "pawn6W":
                    pawnsW.ElementAt(6).Move(pos, board);
                    return pawnsW.ElementAt(6);

                case "pawn7W":
                    pawnsW.ElementAt(7).Move(pos, board);
                    return pawnsW.ElementAt(7);

                case "rook1W":
                    rook1W.Move(pos, board);
                    return rook1W;

                case "knight1W":
                    knight1W.Move(pos, board);
                    return knight1W;

                case "bishop1W":
                    bishop1W.Move(pos, board);
                    return bishop1W;

                case "rook2W":
                    rook2W.Move(pos, board);
                    return rook2W;

                case "knight2W":
                    knight2W.Move(pos, board);
                    return knight2W;

                case "bishop2W":
                    bishop2W.Move(pos, board);
                    return bishop2W;

                case "kingW":
                    kingW.Move(pos, board);
                    return kingW;

                case "queenW":
                    queenW.Move(pos, board);
                    return queenW;

                default:
                    throw new Exception("Not black's turn");

            }
        }
        
        // Places every piece at the start positions
        public void Setup()
        {
            // White begins
            //File.WriteAllText(fullpath2, "False");
            
            // Initialize black pawns
            pawnsB = new List<Piece>();
            for (int i = 0; i < 8; i++)
            {
                pawnsB.Add(new Pawn(new Position(i, 1), "black"));
            }

            // Initialize black pieces
            rook1B = new Rook(new Position(0, 0), "black");
            rook2B = new Rook(new Position(7, 0), "black");
            knight1B = new Knight(new Position(1, 0), "black");
            knight2B = new Knight(new Position(6, 0), "black");
            bishop1B = new Bishop(new Position(2, 0), "black");
            bishop2B = new Bishop(new Position(5, 0), "black");
            queenB = new Queen(new Position(3, 0), "black");
            kingB = new King(new Position(4, 0), "black");

            // Initialize white pawns
            pawnsW = new List<Piece>();
            for (int i = 0; i < 8; i++)
            {
                pawnsW.Add(new Pawn(new Position(i, 6), "white"));
            }

            // Initialize white pieces
            rook1W = new Rook(new Position(0, 7), "white");
            rook2W = new Rook(new Position(7, 7), "white");
            knight1W = new Knight(new Position(1, 7), "white");
            knight2W = new Knight(new Position(6, 7), "white");
            bishop1W = new Bishop(new Position(2, 7), "white");
            bishop2W = new Bishop(new Position(5, 7), "white");
            queenW = new Queen(new Position(3, 7), "white");
            kingW = new King(new Position(4, 7), "white");
        }

        // Updates board to the current state assigning values to the board matrix
        public void UpdateBoard()
        {
            ResetBoard();
            // Place pieces on the board
            if (rook1B.GetPosition().getX() != -1)
                board[rook1B.GetPosition().getX(), rook1B.GetPosition().getY()] = rook1B;
            if (knight1B.GetPosition().getX() != -1)
                board[knight1B.GetPosition().getX(), knight1B.GetPosition().getY()] = knight1B;
            if (bishop1B.GetPosition().getX() != -1)
                board[bishop1B.GetPosition().getX(), bishop1B.GetPosition().getY()] = bishop1B;
            if (queenB.GetPosition().getX() != -1)
                board[queenB.GetPosition().getX(), queenB.GetPosition().getY()] = queenB;
            if (kingB.GetPosition().getX() != -1)
                board[kingB.GetPosition().getX(), kingB.GetPosition().getY()] = kingB;
            if (bishop2B.GetPosition().getX() != -1)
                board[bishop2B.GetPosition().getX(), bishop2B.GetPosition().getY()] = bishop2B;
            if (knight2B.GetPosition().getX() != -1)
                board[knight2B.GetPosition().getX(), knight2B.GetPosition().getY()] = knight2B;
            if (rook2B.GetPosition().getX() != -1)
                board[rook2B.GetPosition().getX(), rook2B.GetPosition().getY()] = rook2B;

            for (int i = 0; i < 8; i++)
            {
                if (pawnsB[i].GetPosition().getX() != -1)
                    board[pawnsB[i].GetPosition().getX(), pawnsB[i].GetPosition().getY()] = pawnsB[i];
            }

            for (int i = 0; i < 8; i++)
            {
                if (pawnsW[i].GetPosition().getX() != -1)
                    board[pawnsW[i].GetPosition().getX(), pawnsW[i].GetPosition().getY()] = pawnsW[i];
            }

            if (rook1W.GetPosition().getX() != -1)
                board[rook1W.GetPosition().getX(), rook1W.GetPosition().getY()] = rook1W;
            if (knight1W.GetPosition().getX() != -1)
                board[knight1W.GetPosition().getX(), knight1W.GetPosition().getY()] = knight1W;
            if (bishop1W.GetPosition().getX() != -1)
                board[bishop1W.GetPosition().getX(), bishop1W.GetPosition().getY()] = bishop1W;
            if (queenW.GetPosition().getX() != -1)
                board[queenW.GetPosition().getX(), queenW.GetPosition().getY()] = queenW;
            if (kingW.GetPosition().getX() != -1)
                board[kingW.GetPosition().getX(), kingW.GetPosition().getY()] = kingW;
            if (bishop2W.GetPosition().getX() != -1)
                board[bishop2W.GetPosition().getX(), bishop2W.GetPosition().getY()] = bishop2W;
            if (knight2W.GetPosition().getX() != -1)
                board[knight2W.GetPosition().getX(), knight2W.GetPosition().getY()] = knight2W;
            if (rook2W.GetPosition().getX() != -1)
                board[rook2W.GetPosition().getX(), rook2W.GetPosition().getY()] = rook2W;
        }

        // Prints the elements of the board 
        public String PrintBoard()
        {
            String strBoard = new String("");
            strBoard = String.Concat(strBoard, "  +-----------------+\n");
            for (int row = 7; row >= 0; row--)
            {
                strBoard = String.Concat(strBoard, row + " | ");
                for (int col = 0; col < 8; col++)
                {
                    Piece piece = board[row, col];
                    if (piece == null)
                    {
                        strBoard = String.Concat(strBoard, ". ");
                    }
                    else
                    {
                        strBoard = String.Concat(strBoard, piece.GetSymbol() + " ");
                    }
                }
                strBoard = String.Concat(strBoard, "|\n");
            }
            strBoard = String.Concat(strBoard, "  +-----------------+\n");
            strBoard = String.Concat(strBoard, "    0 1 2 3 4 5 6 7");
            return strBoard;
        }

        // Resets the board to all null
        public void ResetBoard()
        {
            // Loop through the rows and columns of the board array and set each element to null
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    board[row, col] = null;
                }
            }

        }

        // Saves the current board state to a file
        public void SaveBoard()
        {
            File.WriteAllText(fullpath, GenerateSaveFile());
        }

        // Generates the format for the pieces position
        private String GenerateSaveFile()
        {
            StringBuilder builder = new StringBuilder();
            //black pieces
            builder.Append("rook1B:" + rook1B.ToString() + "\n");
            builder.Append("knight1B:" + knight1B.ToString() + "\n");
            builder.Append("bishop1B:" + bishop1B.ToString() + "\n");
            builder.Append("kingB:" + kingB.ToString() + "\n");
            builder.Append("queenB:" + queenB.ToString() + "\n");
            builder.Append("rook2B:" + rook2B.ToString() + "\n");
            builder.Append("bishop2B:" + bishop2B.ToString() + "\n");
            builder.Append("knight2B:" + knight2B.ToString() + "\n");
            for (int i = 0; i < 8; i++)
            {
                builder.Append("pawn" + i + "B:" + pawnsB[i].ToString() + "\n");
            }
            //white pieces
            builder.Append("rook1W:" + rook1W.ToString() + "\n");
            builder.Append("knight1W:" + knight1W.ToString() + "\n");
            builder.Append("bishop1W:" + bishop1W.ToString() + "\n");
            builder.Append("kingW:" + kingW.ToString() + "\n");
            builder.Append("queenW:" + queenW.ToString() + "\n");
            builder.Append("rook2W:" + rook2W.ToString() + "\n");
            builder.Append("bishop2W:" + bishop2W.ToString() + "\n");
            builder.Append("knight2W:" + knight2W.ToString() + "\n");
            for (int i = 0; i < 8; i++)
            {
                builder.Append("pawn" + i + "W:" + pawnsW[i].ToString() + "\n");
            }

            return builder.ToString();
        }

        // Uploads the positions of the pieces from the file
        public void UploadBoard()
        {
            string[] lines = File.ReadAllLines(fullpath);

            foreach (string line in lines)
            {
                string[] parts = line.Split(':');
                string pieceName = parts[0];
                string[] coordinates = parts[1].Split(',');

                int x = int.Parse(coordinates[0]);
                int y = int.Parse(coordinates[1]);

                switch (pieceName)
                {
                    // Black pieces
                    case "pawn0B":
                        pawnsB[0].SetPosition(new Position(x, y));
                        break;
                    case "pawn1B":
                        pawnsB[1].SetPosition(new Position(x, y));
                        break;
                    case "pawn2B":
                        pawnsB[2].SetPosition(new Position(x, y));
                        break;
                    case "pawn3B":
                        pawnsB[3].SetPosition(new Position(x, y));
                        break;
                    case "pawn4B":
                        pawnsB[4].SetPosition(new Position(x, y));
                        break;
                    case "pawn5B":
                        pawnsB[5].SetPosition(new Position(x, y));
                        break;
                    case "pawn6B":
                        pawnsB[6].SetPosition(new Position(x, y));
                        break;
                    case "pawn7B":
                        pawnsB[7].SetPosition(new Position(x, y));
                        break;
                    case "rook1B":
                        rook1B.SetPosition(new Position(x, y));
                        break;
                    case "knight1B":
                        knight1B.SetPosition(new Position(x, y));
                        break;
                    case "bishop1B":
                        bishop1B.SetPosition(new Position(x, y));
                        break;
                    case "rook2B":
                        rook2B.SetPosition(new Position(x, y));
                        break;
                    case "knight2B":
                        knight2B.SetPosition(new Position(x, y));
                        break;
                    case "bishop2B":
                        bishop2B.SetPosition(new Position(x, y));
                        break;
                    case "kingB":
                        kingB.SetPosition(new Position(x, y));
                        break;
                    case "queenB":
                        queenB.SetPosition(new Position(x, y));
                        break;

                    // White pieces
                    case "pawn0W":
                        pawnsW[0].SetPosition(new Position(x, y));
                        break;
                    case "pawn1W":
                        pawnsW[1].SetPosition(new Position(x, y));
                        break;
                    case "pawn2W":
                        pawnsW[2].SetPosition(new Position(x, y));
                        break;
                    case "pawn3W":
                        pawnsW[3].SetPosition(new Position(x, y));
                        break;
                    case "pawn4W":
                        pawnsW[4].SetPosition(new Position(x, y));
                        break;
                    case "pawn5W":
                        pawnsW[5].SetPosition(new Position(x, y));
                        break;
                    case "pawn6W":
                        pawnsW[6].SetPosition(new Position(x, y));
                        break;
                    case "pawn7W":
                        pawnsW[7].SetPosition(new Position(x, y));
                        break;
                    case "rook1W":
                        rook1W.SetPosition(new Position(x, y));
                        break;
                    case "knight1W":
                        knight1W.SetPosition(new Position(x, y));
                        break;
                    case "bishop1W":
                        bishop1W.SetPosition(new Position(x, y));
                        break;
                    case "rook2W":
                        rook2W.SetPosition(new Position(x, y));
                        break;
                    case "knight2W":
                        knight2W.SetPosition(new Position(x, y));
                        break;
                    case "bishop2W":
                        bishop2W.SetPosition(new Position(x, y));
                        break;
                    case "kingW":
                        kingW.SetPosition(new Position(x, y));
                        break;
                    case "queenW":
                        queenW.SetPosition(new Position(x, y));
                        break;

                    default:
                        
                        break;
                }
            }
        }

        // Check method for white king
        public bool CheckW()
        {
            // Get the position of the white king
            Position whiteKingPosition = kingW.GetPosition();

            // Iterate over all black pieces and check if any can attack the white king
            foreach (Piece piece in board)
            {
                if((piece!=null) && 
                    (piece.GetColor() == "black") && 
                    (piece.isValidMove(whiteKingPosition, board)))
                    return true;
            }

            return false;
        }

        // Check method for black king
        public bool CheckB()
        {
            // Get the position of the black king
            Position blackKingPosition = kingB.GetPosition();

            // Iterate over all white pieces and check if any can attack the black king
            foreach (Piece piece in board)
            {
                if ((piece != null) && 
                    (piece.GetColor() == "white") && 
                    (piece.isValidMove(blackKingPosition, board)))
                    return true;
            }

            return false;
            
        }
    }
}
