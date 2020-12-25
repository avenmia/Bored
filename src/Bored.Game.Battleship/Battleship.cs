using System.Linq;

namespace Bored.Game.Battleship
{
    public record Point
    {
        public byte X { get; }
        public byte Y { get; }
        public Point(byte x, byte y) => (X, Y) = (x, y);
        public bool IsOutOfBounds() => X > 10 || Y > 10;
    }
    public record ShipPosition
    {
        public Point Start { get; }
        public Point End { get; }
        public ShipPosition(Point start, Point end) => (Start, End) = (start, end);
    }
    public enum CellState
    {
        Hit,
        Miss
    }
    public class GameBoard
    {
        public CellState[,] Cells { get; set; }
        public CellState this[int i, int j]
        {
            get { return Cells[i, j]; }
            set { Cells[i, j] = value; }
        }
        public CellState this[Point p]
        {
            get { return Cells[p.X, p.Y]; }
            set { Cells[p.X, p.Y] = value; }
        }
    }
    public enum Orientation
    {
        Horizontal,
        Vertical
    }
    public enum ShipType
    {
        Carrier = 5,
        Battleship = 4,
        Cruiser = 3,
        Submarine = 3,
        Destroyer = 2
    }
    public class Ship
    {
        public ShipType Type { get; set; }
        public ShipPosition Position { get; set; }
        public byte Hits { get; set; } = 0;
        public bool IsSunk() => Hits == (byte)Type;
        public Ship(ShipType type) => Type = type;
        public void PlaceShip(Point startPoint, Orientation orientation)
        {
            byte endX, endY;
            if (orientation == Orientation.Vertical)
            {
                endX = startPoint.X;
                endY = (byte)(startPoint.Y + (byte)Type);
            }
            else
            {
                endX = (byte)(startPoint.X + (byte)Type);
                endY = startPoint.Y;
            }
            var endPoint = new Point(endX, endY);
            if (!startPoint.IsOutOfBounds() && !endPoint.IsOutOfBounds())
            {
                Position = new ShipPosition(startPoint, endPoint);
            }
        }
        public static Ship[] GetNewShips() =>
            new Ship[5] {
                new Ship(ShipType.Destroyer),
                new Ship(ShipType.Submarine),
                new Ship(ShipType.Battleship),
                new Ship(ShipType.Cruiser),
                new Ship(ShipType.Carrier)
            };
    }
    public class Player
    {
        public GameBoard Board = new();
        public Ship[] Ships = Ship.GetNewShips();
        public bool IsDefeated() => Ships.All(s => s.IsSunk());
    }
    public class GameState
    {
        public Player[] Players = new Player[2] { new Player(), new Player() };
    }
    public record GameMove
    {
        public Player Player { get; }
        public Point Point { get; }
        public GameMove(Player player, Point point) => (Player, Point) = (player, point);
    }
    public class Battleship
    {
        public GameState State = new();
        public GameState? MakeMove(GameMove move)
        {
            return State;
        }
    }
}
