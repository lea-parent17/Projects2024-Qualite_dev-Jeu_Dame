using System;

namespace JeuDeDames.LogiqueDeJeu
{
    public class Jeu
    {
        private Plateau plateau;

        public Jeu(int taillePlateau)
        {
            // Initialisation du plateau avec la taille spécifiée (8x8 ou 10x10)
            plateau = new Plateau(taillePlateau);
        }

        public bool DeplacerPiece(int departX, int departY, int arriveeX, int arriveeY)
        {
            // Vérifie si le déplacement est possible et effectue le déplacement
            return plateau.DeplacerPiece(departX, departY, arriveeX, arriveeY);
        }

        public void AfficherPlateau()
        {
            for (int i = 0; i < plateau.Taille; i++)
            {
                for (int j = 0; j < plateau.Taille; j++)
                {
                    var pion = plateau.Cases[i, j];
                    if (pion == CouleurPion.Vide)
                        Console.Write("[ ]");
                    else if (pion == CouleurPion.Blanc)
                        Console.Write("[B]");
                    else if (pion == CouleurPion.Gris)
                        Console.Write("[G]");
                }
                Console.WriteLine();
            }
        }
    }
}
