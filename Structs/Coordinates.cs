

namespace CheckmateLibrary.Structs
{
    public struct Coordinates
    {
        public int Column;
        public int Row;



        public Coordinates(string input)
        {
            Column = input[0] - 'A';
            Row = int.Parse(input[1].ToString()) - 1;

        }

        public static bool IsValidCoordinates(Coordinates coordinates)
        {
            return coordinates.Row >= 0 && coordinates.Row < 8 &&
                   coordinates.Column >= 0 && coordinates.Column < 8;
        }

    }
}
