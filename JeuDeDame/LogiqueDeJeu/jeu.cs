using JeuDeDames.LogiqueDeJeu;
using System;
using System.Collections.Generic;

namespace JeuDeDames.LogiqueDeJeu
{
    public class Jeu
    {
        public Plateau Plateau { get; private set; }
        public bool PartieTerminee { get; private set; }
        public bool Joueur1Tour { get; private set; }

        public Jeu()
        {
            Plateau = new Plateau();
            Joueur1Tour = true; // Le joueur 1 commence
            PartieTerminee = false;
        }

        public bool DeplacerPiece(int xDepart, int yDepart, int xArrivee, int yArrivee)
        {
            if (Plateau.DeplacerPiece(xDepart, yDepart, xArrivee, yArrivee, Joueur1Tour))
            {
                Joueur1Tour = !Joueur1Tour; // Changer de joueur
                return true;
            }
            return false;
        }

        public void TerminerPartie()
        {
            PartieTerminee = true;
        }
    }
}
