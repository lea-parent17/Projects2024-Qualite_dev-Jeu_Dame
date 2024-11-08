namespace JeuDeDames.LogiqueDeJeu
{
    public enum TypePiece
    {
        Aucune,
        Pion,
        Dame
    }

    public class Piece
    {
        public TypePiece Type { get; set; }
        public bool EstNoir { get; set; }

        public Piece(bool estNoir)
        {
            EstNoir = estNoir;
            Type = TypePiece.Pion;
        }

        public void Promouvoir()
        {
            Type = TypePiece.Dame;
        }
    }
}
