using System;

namespace JeuDeDames.LogiqueDeJeu
{
    public class Jeu
    {
        private Plateau plateau;

        public Jeu(int taillePlateau)
        {
            plateau = new Plateau(taillePlateau);
        }

        public bool DeplacerPiece(int departX, int departY, int arriveeX, int arriveeY)
        {
            return plateau.DeplacerPiece(departX, departY, arriveeX, arriveeY);
        }

        public Plateau GetPlateau()
        {
            return plateau;
        }
    }
}
