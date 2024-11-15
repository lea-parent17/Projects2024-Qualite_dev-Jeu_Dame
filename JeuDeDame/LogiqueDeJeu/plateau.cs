using System;
using System.Collections.Generic;

namespace JeuDeDames.LogiqueDeJeu
{
    public class Plateau
    {
        public CouleurPion[,] Cases { get; private set; }

        public Plateau(int taille)
        {
            Cases = new CouleurPion[taille, taille];

            for (int i = 0; i < taille; i++)
            {
                for (int j = 0; j < taille; j++)
                {
                    if ((i + j) % 2 != 0) // Cases noires
                    {
                        if (i < 3)
                            Cases[i, j] = CouleurPion.Gris; // Pions gris
                        else if (i >= taille - 3)
                            Cases[i, j] = CouleurPion.Blanc; // Pions blancs
                        else
                            Cases[i, j] = CouleurPion.Vide; // Cases vides
                    }
                    else
                    {
                        Cases[i, j] = CouleurPion.Vide; // Cases blanches
                    }
                }
            }
        }

        public List<(int, int)> ObtenirDeplacementsPossibles(int x, int y)
        {
            var mouvements = new List<(int, int)>();

            if (Cases[x, y] == CouleurPion.Blanc || Cases[x, y] == CouleurPion.Gris)
            {
                int direction = (Cases[x, y] == CouleurPion.Blanc) ? -1 : 1;

                // Vérifier les diagonales
                for (int dx = -1; dx <= 1; dx += 2)
                {
                    int nx = x + direction;
                    int ny = y + dx;

                    if (nx >= 0 && nx < Cases.GetLength(0) && ny >= 0 && ny < Cases.GetLength(1) && Cases[nx, ny] == CouleurPion.Vide)
                    {
                        mouvements.Add((nx, ny));
                    }
                }
            }

            return mouvements;
        }

        public bool DeplacerPiece(int xOrigine, int yOrigine, int xDestination, int yDestination)
        {
            if (Cases[xDestination, yDestination] == CouleurPion.Vide)
            {
                Cases[xDestination, yDestination] = Cases[xOrigine, yOrigine];
                Cases[xOrigine, yOrigine] = CouleurPion.Vide;
                return true;
            }
            return false;
        }
    }
}
