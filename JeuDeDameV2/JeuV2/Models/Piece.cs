namespace JeuV2.Models
{
    /// <summary>
    /// Represente une piece du jeu.
    /// </summary>
    public class Piece
    {
        /// <summary>
        /// Identifie le joueur auquel appartient cette piece.
        /// 1 pour le joueur 1, 2 pour le joueur 2.
        /// </summary>
        public int Player { get; set; }

        /// <summary>
        /// Indique si cette piece est une dame (roi).
        /// </summary>
        public bool IsKing { get; set; }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="Piece"/>.
        /// </summary>
        /// <param name="player">Le numero du joueur auquel appartient cette piece.</param>
        /// <param name="isKing">Indique si la piece est une dame (par defaut, false).</param>
        public Piece(int player, bool isKing = false)
        {
            Player = player;
            IsKing = isKing;
        }
    }
}
