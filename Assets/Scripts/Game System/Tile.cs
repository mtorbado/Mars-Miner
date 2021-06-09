public class Tile {
    public int X { get; set; }
    public int Y { get; set; }
    public Tile(int x, int y) => (X, Y) = (x, y);
}