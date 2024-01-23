using CheckmateLibrary;
using CheckmateLibrary.Structs;
using CheckmateLibrary.Figures;
namespace Checkmate
{
    internal class BoardPrint
    {
        public static char[,] InitializeChessboard()
        {
            char[,] chessboard = new char[8, 8];
            for (int i = 0; i < chessboard.GetLength(0); i++)
            {
                for (int j = 0; j < chessboard.GetLength(1); j++)
                {
                    chessboard[i, j] = ' ';
                }
            }
            return chessboard;
        }
        public static void PrintChessBoard(char[,] chessboard)
        {
            Console.Write("  A B C D E F G H");
            Console.WriteLine();
            ConsoleColor originalConsoleColor = Console.BackgroundColor;
            for (int i = 0; i < chessboard.GetLength(0); i++)
            {
                Console.Write(i + 1);
                for (int j = 0; j < chessboard.GetLength(1); j++)
                {
                    Console.BackgroundColor = (i + j) % 2 == 0 ? ConsoleColor.DarkGray : ConsoleColor.White;
                    Console.Write(chessboard[i, j] + " ");
                }
                Console.BackgroundColor = originalConsoleColor;
                Console.WriteLine();
            }
        }
        private static bool IsValidInput(string input)
        {
            if (input.Length == 2)
            {
                char firstChar = input[0];
                char secondChar = input[1];
                bool isFirstCharValid = char.IsLetter(firstChar) && (firstChar >= 'A' && firstChar <= 'H');
                bool isSecondCharValid = char.IsDigit(secondChar) && (secondChar >= '1' && secondChar <= '8');
                return isFirstCharValid && isSecondCharValid;
            }
            return false;
        }
        public static void SetPieces(char[,] chessboard)
        {
            PlacePiece(Figure.King, FigureColor.Black, chessboard);
            PlacePiece(Figure.King, FigureColor.White, chessboard);
            PlacePiece(Figure.Queen, FigureColor.White, chessboard);
            PlacePiece(Figure.Rook, FigureColor.White, chessboard);
            PlacePiece(Figure.Rook, FigureColor.White, chessboard);
            /* 
            if (!AreFiguresOverlapping(chessboard))
            {
               Console.WriteLine("Some error") ;
            } */
        }
        private static bool AreFiguresOverlapping(char[,] chessboard)
        {
            HashSet<char> occupiedPositions = new HashSet<char>();
            for (int i = 0; i < chessboard.GetLength(0); i++)
            {
                for (int j = 0; j < chessboard.GetLength(1); j++)
                {
                    char currentPiece = chessboard[i, j];
                    // Skip empty positions
                    if (currentPiece == ' ')
                        continue;
                    // Check if the position is already occupied by another piece
                    if (occupiedPositions.Contains(currentPiece))
                    {
                        Coordinates position = new Coordinates(i, j);
                        Coordinates.PrintError($"Error: Position {position} is occupied by more than one piece.");
                        return false;
                    }
                    occupiedPositions.Add(currentPiece);
                }
            }
            return true;
        }
        private static void PlacePiece(Figure piece, FigureColor color, char[,] chessboard)
        {
            while (true)
            {
                Console.Write($"\nEnter the position for the {color} {piece} (e.g., A8): ");
                string position = Console.ReadLine();
                if (PlaceSymbolOnBoard(position, piece, color, chessboard))
                {
                    break;
                }
            }
        }
        private static bool PlaceSymbolOnBoard(string position, Figure figure, FigureColor color, char[,] chessboard)
        {
            Coordinates coordinates = new Coordinates(position);
            if (!Coordinates.IsValidCoordinates(coordinates))
            {
                Coordinates.PrintError($"Invalid input. Please enter a valid position for {color}{figure}.");
                return false;
            }
            else
            {
                chessboard[coordinates.Row, coordinates.Column] = GetSymbol.GetSymbolChar(figure, color);
                return true;
            }
        }
        public static bool IsNearbyPositionsEmpty(char[,] chessboard, Coordinates kingPosition)
        {
            int[] rowOffsets = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] colOffsets = { -1, 0, 1, -1, 1, -1, 0, 1 };
            for (int i = 0; i < rowOffsets.Length; i++)
            {
                int newRow = kingPosition.Row + rowOffsets[i];
                int newCol = kingPosition.Column + colOffsets[i];
                Coordinates newPosition = new Coordinates(newRow, newCol);
                if (Coordinates.IsValidCoordinates(newPosition))
                {
                    char pieceAtPosition = chessboard[newPosition.Row, newPosition.Column];
                    if (pieceAtPosition != ' ')
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    

    public static List<string> GetValidQueenMoves(char[,] chessboard, Coordinates queenPosition)
    {
        List<string> validMoves = new List<string>();

        // Extract row and column from the current position
        char column = (char)queenPosition.Column;
        char row = (char)queenPosition.Row;

        // Check all possible moves in horizontal, vertical, and diagonal directions
        for (char newColumn = 'A'; newColumn <= 'H'; newColumn++)
        {
            for (int newRow = 1; newRow <= 8; newRow++)
            {
                if (Queen.IsValidMove( queenPosition , new Coordinates(newColumn,newRow)))
                {
                    validMoves.Add($"{newColumn}{newRow}");
                }
            }
        }

        return validMoves;
    }



}