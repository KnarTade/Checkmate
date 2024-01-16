namespace CheckmateLibrary;
public enum Figure
{
    King,
    Queen,
    Bishop,
    Rook,
    Knight
}


public enum FigureColor
{
    White,
    Black
}
public static class GetSymbol
{
    public static char GetSymbolChar(Figure symbol, FigureColor color)
    {
        switch (symbol)
        {
            case Figure.King:
                return (color == FigureColor.White) ? 'K' : 'k';
            case Figure.Queen:
                return (color == FigureColor.White) ? 'Q' : 'q';
            case Figure.Rook:
                return (color == FigureColor.White) ? 'R' : 'r';
            case Figure.Bishop:
                return (color == FigureColor.White) ? 'B' : 'b';
            case Figure.Knight:
                return (color == FigureColor.White) ? 'N' : 'n';
            default:
                throw new ArgumentOutOfRangeException(nameof(symbol), symbol, null);
        }
    }
}



