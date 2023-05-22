using System.Runtime.InteropServices;
using Chess.DTOs;
using Chess.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chess.Controllers
{
    [Route("api/pieces")]
    [ApiController]
    public class PiecesController : ControllerBase
    {
        ChessBoard board;

        public PiecesController() {
            board = new ChessBoard();
        }

        [HttpPost]
        [Route("move")]
        public async Task<IActionResult> MovePiece([FromBody] MovePieceDTO movePieceDTO)
        {
            board.UploadBoard();
            board.UpdateBoard();
            board.Game(movePieceDTO.PieceName, movePieceDTO.Position);
            board.SaveBoard();
            return Ok("Piece moved!");
        }

        [HttpGet]
        [Route("get-chess")]
        public async Task<IActionResult> GetChess()
        {
            board.UploadBoard();
            board.UpdateBoard();
            return Ok(board.PrintBoard());
        }

        [HttpPost]
        [Route("setup")]
        public async Task<IActionResult> SetupBoard()
        {
            board.Setup();
            board.UpdateBoard();
            board.SaveBoard();
            return Ok("Board set up!\n");
        }

        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> SaveBoard()
        {
            board.SaveBoard();
            return Ok("Board saved!");
        }

    }
}
