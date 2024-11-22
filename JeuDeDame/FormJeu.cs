using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using JeuDeDames.LogiqueDeJeu;

namespace JeuDeDames
{
    public partial class FormJeu : Form
    {
        private Plateau plateau;
        private int? caseSelectionnee = null;
        private CouleurPion joueurActuel = CouleurPion.Blanc; // Par défaut, les Blancs commencent


        public FormJeu(int taille)
        {
            InitializeComponent();
            plateau = new Plateau(taille);
            InitializeBoard(taille);
        }

        private void InitializeBoard(int taille)
        {
            this.Controls.Clear(); // Efface les contrôles existants

            for (int i = 0; i < taille; i++)
            {
                for (int j = 0; j < taille; j++)
                {
                    Button btn = new Button();
                    btn.Size = new System.Drawing.Size(60, 60);
                    btn.Location = new System.Drawing.Point(j * 60, i * 60);
                    btn.TabIndex = i * taille + j;

                    // Couleur de la case
                    btn.BackColor = (i + j) % 2 == 0 ? ColorTranslator.FromHtml("#DEB887") : ColorTranslator.FromHtml("#493316");

                    // Ajout des pions
                    if (plateau.Cases[i, j] == CouleurPion.Blanc)
                    {
                        btn.Text = "⚪";
                        btn.ForeColor = System.Drawing.Color.White;
                    }
                    else if (plateau.Cases[i, j] == CouleurPion.Gris)
                    {
                        btn.Text = "⚫";
                        btn.ForeColor = System.Drawing.Color.Gray;
                    }

                    btn.Click += new EventHandler(this.Case_Click);
                    this.Controls.Add(btn);
                }
            }

            this.ClientSize = new System.Drawing.Size(taille * 60, taille * 60); // Ajuster la taille de la fenêtre
        }

        private void Case_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            int taille = (int)Math.Sqrt(plateau.Cases.Length);
            int i = btn.TabIndex / taille;
            int j = btn.TabIndex % taille;

            if (caseSelectionnee == null)
            {
                // Vérifier que le joueur actif sélectionne ses propres pions
                if (plateau.Cases[i, j] == joueurActuel)
                {
                    caseSelectionnee = btn.TabIndex;
                    btn.FlatAppearance.BorderColor = System.Drawing.Color.Yellow;

                    // Afficher les déplacements possibles en bleu
                    var mouvements = plateau.ObtenirDeplacementsPossibles(i, j);
                    foreach (var (x, y) in mouvements)
                    {
                        Button targetBtn = (Button)this.Controls[x * taille + y];
                        targetBtn.BackColor = System.Drawing.Color.Blue;
                    }

                    // Afficher les déplacements pour manger un pion
                    var mouvementsManger = plateau.ObtenirDeplacementsPourManger(i, j);
                    foreach (var (x, y) in mouvementsManger)
                    {
                        Button targetBtn = (Button)this.Controls[x * taille + y];
                        targetBtn.BackColor = System.Drawing.Color.Green; // Déplacement de manger en vert
                    }
                }
            }
            else
            {
                // Déplacement
                int nouvelleCase = btn.TabIndex;
                int xOrigine = caseSelectionnee.Value / taille;
                int yOrigine = caseSelectionnee.Value % taille;

                // Valider si le mouvement est autorisé (déplacement ou manger)
                var mouvementsPossibles = plateau.ObtenirDeplacementsPossibles(xOrigine, yOrigine);
                var mouvementsPourManger = plateau.ObtenirDeplacementsPourManger(xOrigine, yOrigine);

                if (mouvementsPossibles.Contains((i, j)) || mouvementsPourManger.Contains((i, j)))
                {
                    if (plateau.DeplacerPiece(xOrigine, yOrigine, i, j))
                    {
                        // Si un pion est mangé, le retirer du plateau
                        if (Math.Abs(xOrigine - i) == 2 && Math.Abs(yOrigine - j) == 2)
                        {
                            int xMange = (xOrigine + i) / 2;
                            int yMange = (yOrigine + j) / 2;
                            plateau.Cases[xMange, yMange] = CouleurPion.Vide;
                            MettreAJourCase(xMange, yMange);
                        }

                        ReinitialiserCouleurs(taille);
                        MettreAJourCase(xOrigine, yOrigine); // Case d'origine
                        MettreAJourCase(i, j);              // Case de destination

                        PasserAuTourSuivant(); // Changer de joueur après un déplacement valide
                    }
                }

                caseSelectionnee = null;
            }
        }



        private void PasserAuTourSuivant()
        {
            joueurActuel = (joueurActuel == CouleurPion.Blanc) ? CouleurPion.Gris : CouleurPion.Blanc;
        }






        private void MettreAJourCase(int i, int j)
        {
            int taille = (int)Math.Sqrt(plateau.Cases.Length);
            Button btn = (Button)this.Controls[i * taille + j];

            // Mise à jour du texte et des couleurs selon l'état de la case
            if (plateau.Cases[i, j] == CouleurPion.Blanc)
            {
                btn.Text = "⚪";
                btn.ForeColor = System.Drawing.Color.White;
            }
            else if (plateau.Cases[i, j] == CouleurPion.Gris)
            {
                btn.Text = "⚫";
                btn.ForeColor = System.Drawing.Color.Gray;
            }
            else
            {
                btn.Text = "";
            }

            // Restaurer la couleur par défaut si nécessaire
            btn.BackColor = (i + j) % 2 == 0 ? ColorTranslator.FromHtml("#DEB887") : ColorTranslator.FromHtml("#493316");
        }


        private void ReinitialiserCouleurs(int taille)
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Button btn)
                {
                    int index = btn.TabIndex;
                    int i = index / taille;
                    int j = index % taille;

                    // Restaurer la couleur par défaut
                    btn.BackColor = (i + j) % 2 == 0 ? ColorTranslator.FromHtml("#DEB887") : ColorTranslator.FromHtml("#493316");
                }
            }
        }

        



    }
}
