namespace JeuV2.Models
{
    public class Board
    {
        public const int MapSize = 8;
        public const int CellSize = 50;
        public int[,] GameMap { get; private set; }
        public Button[,] GameButtons { get; private set; }

        public Board()
        {
            InitializeBoard();
        }

        public void InitializeBoard()
        {
            GameMap = new int[MapSize, MapSize]
            {
                { 0,1,0,1,0,1,0,1 },
                { 1,0,1,0,1,0,1,0 },
                { 0,1,0,1,0,1,0,1 },
                { 0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0 },
                { 2,0,2,0,2,0,2,0 },
                { 0,2,0,2,0,2,0,2 },
                { 2,0,2,0,2,0,2,0 }
            };

            GameButtons = new Button[MapSize, MapSize];
        }

        public bool IsInsideBorders(int x, int y)
        {
            return x >= 0 && x < MapSize && y >= 0 && y < MapSize;
        }
    }
}