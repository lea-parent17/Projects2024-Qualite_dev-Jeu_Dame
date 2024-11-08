using System;

namespace JeuDeDames.LogiqueDeJeu
{
    public class Plateau
    {
        public Piece[,] Cases { get; private set; }

        public Plateau()
        {
            Cases = new Piece[8, 8];
            InitialiserPlateau();
        }

        private void InitialiserPlateau()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((i + j) % 2 != 0)
                    {
                        if (i < 3)
                        {
                            Cases[i, j] = new Piece(true); // Pièces noires
                        }
                        else if (i > 4)
                        {
                            Cases[i, j] = new Piece(false); // Pièces blanches
                        }
                    }
                }
            }
        }

        public bool DeplacerPiece(int xDepart, int yDepart, int xArrivee, int yArrivee, bool estNoir)
        {
            if (Cases[xDepart, yDepart] != null && Cases[xDepart, yDepart].EstNoir == estNoir)
            {
                // Exemple de validation simple pour un déplacement
                if (Math.Abs(xArrivee - xDepart) == 1 && Math.Abs(yArrivee - yDepart) == 1)
                {
                    // Déplacement simple
                    Cases[xArrivee, yArrivee] = Cases[xDepart, yDepart];
                    Cases[xDepart, yDepart] = null;
                    return true;
                }
            }
            return false;
        }
    }
}
