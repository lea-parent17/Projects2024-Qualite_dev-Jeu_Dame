namespace JeuV2.Models
{
    public class Piece
    {
        public int Player { get; set; }
        public bool IsKing { get; set; }

        public Piece(int player, bool isKing = false)
        {
            Player = player;
            IsKing = isKing;
        }
    }
}