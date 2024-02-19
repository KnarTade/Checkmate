using CheckmateLibrary;

namespace Checkmate;

internal class Program
{
    static void Main()
    {
        char[,] chessboard = BoardPrint.InitializeChessboard();
        BoardPrint.PrintChessBoard(chessboard);
        BoardPrint.SetPieces(chessboard);
        char[,] chessboardWithFigures = BoardPrint.SetPieces(chessboard);
        // Ask the user which team's covered positions they need
        Console.WriteLine("Which team's covered positions do you need? (black/white): ");
        string teamInput = Console.ReadLine().Trim().ToLower();

    }
}

    
