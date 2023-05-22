# Chess: The backend version
This is the backend version of the famous chess game. Being only backend, there is no great visual representation of the pieces or the board itself. The only representation available is an ascii model of the board with FEN notations for the pieces, where the black pieces are represented by large letters and white pieces are represented by small letters.
- Pawns - p, P.
- Rooks - r, R.
- Knights - n, N (the K letter is used for the king).
- Bishops - b, B.
- Queens - q, Q.
- Kings - k, K.
### Project description:
This project is realised using the C# language combined with the asp.net core framework. Also, the code editor used is Microsoft Visual Studio 2022. Currently there are 4 folders containing the project logic, controllers, entities and data transfer objects. There is a folder contaning some data files in which I store the position of the pieces for the program to remember them, and the turn order for the players.
###### The logic folder:
Contains the ChessBoard class, the Piece class and the Position class.
- The Piece class is an abstract class with the position, color and symbol attribute. It also contains the Move method which is abstract as well, the isValidMove method which is the same as Move but doesn't perform any action, and the ToString method.
- The Position class is a simple class withc X and Y for every piece to remember.
- The ChesBoard class is the master class with the most logic of them all: it has methods for moving the pieces, for checking the check state, for writing and reading from the Data folder and for initialization of the game. The most important method is the Move method, which determines what piece will move at a distinct location.

###### The entities folder:
Contains classes for every piece there is such as: Pawn, Rook, Knight, Bishop, King, Queen. Every entity mentioned extends the piece class and implements the Move method in it's overridden form, the method ToString in it's overridden version and the method IsValidMove.

###### The DTO folder: 
Contains a single data transfer object class which is used in the controller at the move method, because I needed to pass parameters.
###### The data folder:
Contains 2 text files, both used for saving data for the chess game. The turn file saves only the current turn of the chess game. The turn is initialized at the beggining of the game with false, which means that white begins. The second folder present is the pieces position folder, which contains the position of every piece that is and is not on the board. You will notice that a captured piece has the coordinates (-1, -1). That is because I needed them removed from the chessboard.

###### The Controller folder:
Contains the PieceController files which manages the endpoints of this project. The endpoints are presented below.

### Project endpoints
The project at hand uses 4 endpoints : 3 used for using the app and one using for saving the chessboard in a text file, with the position of every chess piece. The save endpoint is not so important, as 2 of the other endpoints save the board in the save file as well. The endpoints responsible for using and testing the app are:
- Setup endpoint: sets the chess board with the pieces placed at the location characteristic of a clasic chess game. The pieces are then saved by position in a data file as piecename:xCoordinate,yCoordinate. Every endpoint reads and updates this file.
- Move endpoint: moves a piece that you choose to the location desired. The move endpoint is fully working, traking if the pieces are moving correctly, checking turns and making sure that the king is not in chess, and if it is, there is no available move but the ones that make the king safe. As I mentioned, you need to provide the piece and location. The pieces have names that need to be written for the endpoint to work. Here is an example:

```
{
  "position": {
    "x": 0,
    "y": 3
  },
  "pieceName": "pawn0B"
}
```

The full list of white pieces are : panw0W, pawn1W, pawn2W, pawn3W, pawn4W, pawn5W, pawn6W, pawn7W, rook1W, knight1W, bishop1W, queenW, kingW, bishop2W, knight2W, rook2W.
The full list of black pieces are : panw0B, pawn1B, pawn2B, pawn3B, pawn4B, pawn5B, pawn6B, pawn7B, rook1B, knight1B, bishop1B, queenB, kingB, bishop2B, knight2B, rook2.
The piecename in the move endpoint needs to be written as above, specifying color and which of the piece you want to move.
The chessboard has coordinate notation, from 0 to 7, as a matrix: (0,0) at the top left corner and (7,7) at the bottopm right corner. If the piecename and position are specified correctly, the piece selected should move to the location chosen but only if it is a valid move. The endpoint throws various exceptions if the piece is not moved correctly.
- Get endpoint: it returns a string which represents the full chessboards with the pieces being letters from the fen notations of the pieces: the capital letters being the black peices and the small letters are the white pieces. The get endpoint is updated so if you use a move endpoint it takes notice and is able to update if used again
### How to see if it really works
You need to run the app, and then in this order call the following endpoints: setup (sets up the board), get (to see if it really set the board up), and then move with the correct parameters. From here you can play by clicking move and get over and over, because it updates the board and you can see the game in real time (almost real time).
