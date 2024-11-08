using System.Windows.Forms;
using System;

namespace JeuDeDames
{
    public partial class FormJeu : Form
    {
        private int boardSize;  // La taille du plateau (8 ou 10)

        public FormJeu(int size)
        {
            InitializeComponent();
            boardSize = size;
            InitializeBoard(boardSize);
        }

        private void InitializeBoard(int size)
        {
            this.Controls.Clear();  // Efface les contrôles existants

            // Créer les boutons pour chaque case du plateau
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Button btn = new Button();
                    btn.Size = new System.Drawing.Size(60, 60);
                    btn.Location = new System.Drawing.Point(j * 60, i * 60);
                    btn.TabIndex = i * size + j;
                    btn.Click += new EventHandler(this.Case_Click);
                    this.Controls.Add(btn);
                }
            }

            this.ClientSize = new System.Drawing.Size(size * 60, size * 60);  // Ajuster la taille du formulaire
        }

        private void Case_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            MessageBox.Show($"Vous avez cliqué sur la case {btn.TabIndex + 1}");
        }

        // Ajoutez la méthode pour gérer l'événement de clic sur le bouton "Dames Françaises"
        private void btnDamesFrancaises_Click(object sender, EventArgs e)
        {
            // Appel de la fonction pour initialiser le plateau en 8x8
            FormJeu formJeu = new FormJeu(8);  // Plateau 8x8
            formJeu.Show();  // Affiche le formulaire de jeu
        }
    }
}
