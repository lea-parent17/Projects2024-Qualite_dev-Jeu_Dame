using System;
using System.Collections.Generic;

namespace JeuDeDames.LogiqueDeJeu
{
    public enum CouleurPion
    {
        Vide ,
        Blanc,
        Gris
    }

    public class Plateau
    {
        public CouleurPion[,] Cases { get; private set; } // Grille des cases avec pions
        public int Taille { get; private set; }

        public Plateau(int taille)
        {
            if (taille != 8 && taille != 10)
            {
                throw new ArgumentException("La taille du plateau doit être 8x8 ou 10x10.");
            }

            Taille = taille;
            Cases = new CouleurPion[taille, taille];
            InitialiserPions();
        }

        private void InitialiserPions()
        {
            // Nombre de rangées de pions en fonction de la taille
            int nombreRangees = Taille == 8 ? 3 : 4;

            // Initialisation des pions blancs
            for (int i = 0; i < nombreRangees; i++)
            {
                for (int j = 0; j < Taille; j++)
                {
                    if ((i + j) % 2 != 0) // Cases noires uniquement
                    {
                        Cases[i, j] = CouleurPion.Blanc;
                    }
                }
            }

            // Initialisation des pions gris
            for (int i = Taille - nombreRangees; i < Taille; i++)
            {
                for (int j = 0; j < Taille; j++)
                {
                    if ((i + j) % 2 != 0) // Cases noires uniquement
                    {
                        Cases[i, j] = CouleurPion.Gris;
                    }
                }
            }
        }

        public bool DeplacerPiece(int departX, int departY, int arriveeX, int arriveeY)
        {
            if (departX < 0 || departX >= Taille || departY < 0 || departY >= Taille ||
                arriveeX < 0 || arriveeX >= Taille || arriveeY < 0 || arriveeY >= Taille)
            {
                return false;
            }

            if (Cases[departX, departY] == CouleurPion.Vide)
            {
                return false;
            }

            Cases[arriveeX, arriveeY] = Cases[departX, departY];
            Cases[departX, departY] = CouleurPion.Vide;

            return true;
        }
        public List<(int, int)> ObtenirDeplacementsPossibles(int x, int y)
        {
            List<(int, int)> deplacements = new List<(int, int)>();

            if (Cases[x, y] != CouleurPion.Vide)
            {
                // Ajouter la logique des déplacements (par exemple, diagonales)
                if (x > 0 && y > 0 && Cases[x - 1, y - 1] == CouleurPion.Vide)
                    deplacements.Add((x - 1, y - 1));
                if (x > 0 && y < Taille - 1 && Cases[x - 1, y + 1] == CouleurPion.Vide)
                    deplacements.Add((x - 1, y + 1));
                if (x < Taille - 1 && y > 0 && Cases[x + 1, y - 1] == CouleurPion.Vide)
                    deplacements.Add((x + 1, y - 1));
                if (x < Taille - 1 && y < Taille - 1 && Cases[x + 1, y + 1] == CouleurPion.Vide)
                    deplacements.Add((x + 1, y + 1));
            }

            return deplacements;
        }

    }
}
