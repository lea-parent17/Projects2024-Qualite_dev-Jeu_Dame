using System;
using System.Drawing;
using System.Windows.Forms;
using JeuDeDames.LogiqueDeJeu;

namespace JeuDeDames
{
    public partial class FormJeu : Form
    {
        private Plateau plateau;
        private int? caseSelectionnee = null;

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
                    btn.BackColor = (i + j) % 2 == 0 ? System.Drawing.Color.White : ColorTranslator.FromHtml("#493316");

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
                // Sélection d'un pion
                if (plateau.Cases[i, j] == CouleurPion.Blanc || plateau.Cases[i, j] == CouleurPion.Gris)
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
                }
            }
            else
            {
                // Déplacement
                int nouvelleCase = btn.TabIndex;
                int xOrigine = caseSelectionnee.Value / taille;
                int yOrigine = caseSelectionnee.Value % taille;

                if (plateau.DeplacerPiece(xOrigine, yOrigine, i, j))
                {
                    InitializeBoard(taille); // Réinitialiser le plateau après un mouvement valide
                }

                caseSelectionnee = null; // Réinitialise la sélection
            }
        }
    }
}
