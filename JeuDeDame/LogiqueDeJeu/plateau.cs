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

        public bool DeplacerPiece(int xOrigine, int yOrigine, int xDest, int yDest)
        {
            int deltaX = xDest - xOrigine;
            int deltaY = yDest - yOrigine;

            // Vérifier que le déplacement est valide
            if (Math.Abs(deltaX) == 1 && Math.Abs(deltaY) == 1)
            {
                // Déplacement simple
                Cases[xDest, yDest] = Cases[xOrigine, yOrigine];
                Cases[xOrigine, yOrigine] = CouleurPion.Vide;
                return true;
            }
            else if (Math.Abs(deltaX) == 2 && Math.Abs(deltaY) == 2)
            {
                // Déplacement avec saut (manger un pion)
                int xIntermediaire = xOrigine + deltaX / 2;
                int yIntermediaire = yOrigine + deltaY / 2;

                if (Cases[xIntermediaire, yIntermediaire] != CouleurPion.Vide &&
                    Cases[xIntermediaire, yIntermediaire] != Cases[xOrigine, yOrigine])
                {
                    // Supprimer le pion mangé
                    Cases[xIntermediaire, yIntermediaire] = CouleurPion.Vide;

                    // Déplacer le pion
                    Cases[xDest, yDest] = Cases[xOrigine, yOrigine];
                    Cases[xOrigine, yOrigine] = CouleurPion.Vide;

                    return true;
                }
            }

            return false; // Déplacement non valide
        }

        public List<(int, int)> ObtenirDeplacementsPourManger(int x, int y)
        {
            var deplacements = new List<(int, int)>();
            int[] dx = { -1, -1, 1, 1 }; // Directions diagonales possibles
            int[] dy = { -1, 1, -1, 1 };

            for (int k = 0; k < 4; k++)
            {
                int nx = x + dx[k];
                int ny = y + dy[k];
                int nx2 = x + 2 * dx[k];
                int ny2 = y + 2 * dy[k];

                if (nx >= 0 && ny >= 0 && nx < Cases.GetLength(0) && ny < Cases.GetLength(1) &&
                    Cases[nx, ny] != CouleurPion.Vide && Cases[nx, ny] != Cases[x, y] &&
                    Cases[nx2, ny2] == CouleurPion.Vide)
                {
                    deplacements.Add((nx2, ny2));
                }
            }
            return deplacements;
        }


    }
}
