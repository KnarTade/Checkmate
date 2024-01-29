using CheckmateLibrary;
using CheckmateLibrary.Figures;
using CheckmateLibrary.Structs;

namespace Checkmate;

    public class BoardPrint
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

        public static List<string> GetCoveredPositions(char[,] chessboard)
        {
            List<string> coveredPositions = new List<string>();

            // Get covered positions by white queen
            foreach (var position in GetAllWhiteQueenMoves(chessboard))
            {
                if (!coveredPositions.Contains(position))
                {
                    coveredPositions.Add(position);
                }
            }

            // Get covered positions by white king
            foreach (var position in GetAllWhiteKingMoves(chessboard))
            {
                if (!coveredPositions.Contains(position))
                {
                    coveredPositions.Add(position);
                }
            }

            // Get covered positions by white rooks
            foreach (var position in GetAllWhiteRookMoves(chessboard))
            {
                if (!coveredPositions.Contains(position))
                {
                    coveredPositions.Add(position);
                }
            }

            return coveredPositions;
        }

        private static List<string> GetAllWhiteQueenMoves(char[,] chessboard)
        {
            List<string> moves = new List<string>();

            // Find the position of the white queen
            Coordinates queenPosition = FindPiecePosition(chessboard, Figure.Queen, FigureColor.White);

            // Get valid queen moves on an empty board
            moves.AddRange(Queen.GetValidQueenMoves(chessboard, queenPosition));

            return moves;
        }

        private static List<string> GetAllWhiteKingMoves(char[,] chessboard)
        {
            List<string> moves = new List<string>();

            // Find the position of the white king
            Coordinates kingPosition = FindPiecePosition(chessboard, Figure.King, FigureColor.White);

            // Get valid king moves on an empty board
            moves.AddRange(King.GetValidKingMoves(chessboard, kingPosition, FigureColor.White));

            return moves;
        }

        private static List<string> GetAllWhiteRookMoves(char[,] chessboard)
        {
            List<string> moves = new List<string>();

            // Find the positions of the white rooks
            List<Coordinates> rookPositions = FindAllPiecePositions(chessboard, Figure.Rook, FigureColor.White);

            // Get valid rook moves on an empty board for each rook
            foreach (var position in rookPositions)
            {
                moves.AddRange(Rook.GetValidRookMoves(chessboard, position));
            }

            return moves;
        }

        private static Coordinates FindPiecePosition(char[,] chessboard, Figure piece, FigureColor color)
        {
            for (int i = 0; i < chessboard.GetLength(0); i++)
            {
                for (int j = 0; j < chessboard.GetLength(1); j++)
                {
                    if (chessboard[i, j] == GetSymbol.GetSymbolChar(piece, color))
                    {
                        return new Coordinates(i, j);
                    }
                }
            }

            // If the piece is not found, return an invalid position
            return new Coordinates(-1, -1);
        }

        private static List<Coordinates> FindAllPiecePositions(char[,] chessboard, Figure piece, FigureColor color)
        {
            List<Coordinates> positions = new List<Coordinates>();

            for (int i = 0; i < chessboard.GetLength(0); i++)
            {
                for (int j = 0; j < chessboard.GetLength(1); j++)
                {
                    if (chessboard[i, j] == GetSymbol.GetSymbolChar(piece, color))
                    {
                        positions.Add(new Coordinates(i, j));
                    }
                }
            }

            return positions;
        }




    }
