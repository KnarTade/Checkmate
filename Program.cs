namespace Checkmate;

internal class Program
{
    static void Main()
    {
        char[,] chessboard = BoardPrint.InitializeChessboard();
        BoardPrint.PrintChessBoard(chessboard);
        BoardPrint.SetPieces(chessboard);
        BoardPrint.PrintChessBoard(chessboard);
    }
}