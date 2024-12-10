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
        private CouleurPion joueurActuel = CouleurPion.Blanc;
        private Label lblTourJoueur;
        private Panel panelPlateau;
        private int tailleCase = 60; // Taille initiale des cases

        public FormJeu(int taille)
        {
            InitializeComponent();
            plateau = new Plateau(taille);

            InitializeGameInfo();
            InitializeBoard(taille);
            this.Resize += new EventHandler(this.FormJeu_Resize); // Attacher l'événement Resize
        }

        private void InitializeGameInfo()
        {
            lblTourJoueur = new Label();
            lblTourJoueur.Text = "Tour : Joueur 1";
            lblTourJoueur.Font = new Font("Arial", 14, FontStyle.Bold);
            lblTourJoueur.Location = new Point(10, 10);
            lblTourJoueur.AutoSize = true;
            this.Controls.Add(lblTourJoueur);
        }

        private void InitializeBoard(int taille)
        {
            panelPlateau = new Panel
            {
                Location = new Point(0, lblTourJoueur.Bottom + 10), // Juste sous le label
                AutoScroll = true // Permet de défiler si le plateau dépasse
            };

            this.Controls.Add(panelPlateau);

            RedessinerPlateau(taille);
        }

        private void RedessinerPlateau(int taille)
        {
            panelPlateau.Controls.Clear(); // Efface les anciens contrôles

            int plateauSize = taille * tailleCase;
            panelPlateau.Size = new Size(plateauSize, plateauSize);

            for (int i = 0; i < taille; i++)
            {
                for (int j = 0; j < taille; j++)
                {
                    Button btn = new Button
                    {
                        Size = new Size(tailleCase, tailleCase),
                        Location = new Point(j * tailleCase, i * tailleCase),
                        TabIndex = i * taille + j,
                        BackColor = (i + j) % 2 == 0 ? ColorTranslator.FromHtml("#DEB887") : ColorTranslator.FromHtml("#493316")
                    };

                    // Ajout des pions
                    if (plateau.Cases[i, j] == CouleurPion.Blanc)
                    {
                        btn.Text = "⚪";
                        btn.ForeColor = Color.White;
                    }
                    else if (plateau.Cases[i, j] == CouleurPion.Gris)
                    {
                        btn.Text = "⚫";
                        btn.ForeColor = Color.Gray;
                    }

                    btn.Click += new EventHandler(this.Case_Click);
                    panelPlateau.Controls.Add(btn);
                }
            }

            CenterPlateau(); // Centre le plateau à chaque redessin
        }

        private void FormJeu_Resize(object sender, EventArgs e)
        {
            CenterPlateau();
            AjusterTailleCases();
        }

        private void CenterPlateau()
        {
            int offsetX = (this.ClientSize.Width - panelPlateau.Width) / 2;
            int offsetY = (this.ClientSize.Height - panelPlateau.Height) / 2;

            panelPlateau.Location = new Point(Math.Max(offsetX, 10), Math.Max(lblTourJoueur.Bottom + 10, offsetY));
        }

        private void AjusterTailleCases()
        {
            int taille = (int)Math.Sqrt(plateau.Cases.Length);
            int espaceDisponible = Math.Min(this.ClientSize.Width, this.ClientSize.Height - lblTourJoueur.Bottom - 10);
            tailleCase = Math.Max(40, espaceDisponible / taille); // Taille minimum de 40px

            RedessinerPlateau(taille); // Recalculer le plateau avec la nouvelle taille
        }

        private void Case_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            int taille = (int)Math.Sqrt(plateau.Cases.Length);
            int i = btn.TabIndex / taille;
            int j = btn.TabIndex % taille;

            if (caseSelectionnee == null)
            {
                if (plateau.Cases[i, j] == joueurActuel)
                {
                    caseSelectionnee = btn.TabIndex;
                    btn.FlatAppearance.BorderColor = Color.Yellow;

                    var mouvements = plateau.ObtenirDeplacementsPossibles(i, j);
                    foreach (var (x, y) in mouvements)
                    {
                        Button targetBtn = (Button)panelPlateau.Controls[x * taille + y];
                        targetBtn.BackColor = Color.Blue;
                    }

                    var mouvementsManger = plateau.ObtenirDeplacementsPourManger(i, j);
                    foreach (var (x, y) in mouvementsManger)
                    {
                        Button targetBtn = (Button)panelPlateau.Controls[x * taille + y];
                        targetBtn.BackColor = Color.Green;
                    }
                }
            }
            else
            {
                int nouvelleCase = btn.TabIndex;
                int xOrigine = caseSelectionnee.Value / taille;
                int yOrigine = caseSelectionnee.Value % taille;

                var mouvementsPossibles = plateau.ObtenirDeplacementsPossibles(xOrigine, yOrigine);
                var mouvementsPourManger = plateau.ObtenirDeplacementsPourManger(xOrigine, yOrigine);

                if (mouvementsPossibles.Contains((i, j)) || mouvementsPourManger.Contains((i, j)))
                {
                    if (plateau.DeplacerPiece(xOrigine, yOrigine, i, j))
                    {
                        if (Math.Abs(xOrigine - i) == 2 && Math.Abs(yOrigine - j) == 2)
                        {
                            int xMange = (xOrigine + i) / 2;
                            int yMange = (yOrigine + j) / 2;
                            plateau.Cases[xMange, yMange] = CouleurPion.Vide;
                            MettreAJourCase(xMange, yMange);
                        }

                        ReinitialiserCouleurs(taille);
                        MettreAJourCase(xOrigine, yOrigine);
                        MettreAJourCase(i, j);

                        PasserAuTourSuivant();
                    }
                }

                caseSelectionnee = null;
            }
        }

        private void PasserAuTourSuivant()
        {
            joueurActuel = (joueurActuel == CouleurPion.Blanc) ? CouleurPion.Gris : CouleurPion.Blanc;
            lblTourJoueur.Text = $"Tour : {(joueurActuel == CouleurPion.Blanc ? "Joueur 1" : "Joueur 2")}";
        }

        private void MettreAJourCase(int i, int j)
        {
            int taille = (int)Math.Sqrt(plateau.Cases.Length);
            Button btn = (Button)panelPlateau.Controls[i * taille + j];

            if (plateau.Cases[i, j] == CouleurPion.Blanc)
            {
                btn.Text = "⚪";
                btn.ForeColor = Color.White;
            }
            else if (plateau.Cases[i, j] == CouleurPion.Gris)
            {
                btn.Text = "⚫";
                btn.ForeColor = Color.Gray;
            }
            else
            {
                btn.Text = "";
            }

            btn.BackColor = (i + j) % 2 == 0 ? ColorTranslator.FromHtml("#DEB887") : ColorTranslator.FromHtml("#493316");
        }


        private void ReinitialiserCouleurs(int taille)
        {
            foreach (Control ctrl in panelPlateau.Controls)
            {
                if (ctrl is Button btn)
                {
                    int index = btn.TabIndex;
                    int i = index / taille;
                    int j = index % taille;

                    btn.BackColor = (i + j) % 2 == 0 ? ColorTranslator.FromHtml("#DEB887") : ColorTranslator.FromHtml("#493316");
                }
            }
        }
    }
}
