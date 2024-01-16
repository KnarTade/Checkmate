using System;
using CheckmateLibrary;
using CheckmateLibrary.Structs;


namespace Checkmate;


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
    //Console.WriteLine("Enter the position for white king");

    public static void SetPieces(char[,] chessboard)
    {
        PlacePiece(Figure.King, FigureColor.Black,chessboard);
        PlacePiece(Figure.King, FigureColor.White, chessboard);
        PlacePiece(Figure.Queen, FigureColor.White, chessboard);
        PlacePiece(Figure.Rook, FigureColor.White, chessboard);
        PlacePiece(Figure.Rook, FigureColor.White , chessboard);
    }
   
    private static void PlacePiece(Figure piece, FigureColor color,char[,] chessboard)
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
            Console.WriteLine($"Invalid input. Please enter a valid position for {color}{figure}.");
            return false;
        }
        else
        {
            chessboard[coordinates.Row,coordinates.Column] = GetSymbol.GetSymbolChar(figure, color);

            return true;
        }

    }
}
