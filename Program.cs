namespace Checkmate;

internal class Program
{
    static void Main()
    {
        //// Create a configuration builder
        //var builder = new ConfigurationBuilder()
        //    .SetBasePath(Directory.GetCurrentDirectory())
        //    .AddJsonFile("jsconfig2.json", optional: true, reloadOnChange: true);


        ////// Build the configuration
        //var configuration = builder.Build();

        //// Now you can access configuration values
        //var theme = configuration["theme"];




        char[,] chessboard = BoardPrint.InitializeChessboard();
        BoardPrint.PrintChessBoard(chessboard);
        BoardPrint.SetPieces(chessboard);
        char[,] chessboardWithFigures = BoardPrint.SetPieces(chessboard);
        //BoardPrint.PrintChessBoard(chessboard);
    }
}