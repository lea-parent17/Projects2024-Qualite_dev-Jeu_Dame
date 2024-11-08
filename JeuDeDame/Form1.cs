using System;
using System.Windows.Forms;

namespace JeuDeDames
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Lors du clic sur le bouton Dames Françaises (8x8)
        private void btnDamesFrancaises_Click(object sender, EventArgs e)
        {
            // Créer un formulaire pour les dames françaises avec un plateau 8x8
            FormJeu formJeu = new FormJeu(8);  // Passer la taille 8 pour 8x8
            formJeu.Show();  // Ouvrir le formulaire de jeu
            this.Hide();  // Masquer le formulaire d'accueil
        }

        // Lors du clic sur le bouton Dames Européennes (10x10)
        private void btnDamesEuropeennes_Click(object sender, EventArgs e)
        {
            // Créer un formulaire pour les dames européennes avec un plateau 10x10
            FormJeu formJeu = new FormJeu(10);  // Passer la taille 10 pour 10x10
            formJeu.Show();  // Ouvrir le formulaire de jeu
            this.Hide();  // Masquer le formulaire d'accueil
        }
    }
}
