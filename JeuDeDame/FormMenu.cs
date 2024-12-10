using System;
using System.Windows.Forms;

namespace JeuDeDames
{
    /// <summary>
    /// Classe représentant le menu principal du jeu de dames
    /// Permet de choisir entre les modes de jeu "Dames Françaises" et "Dames Européennes"
    /// </summary>
    public partial class FormMenu : Form
    {
        /// <summary>
        /// Initialise une nouvelle instance du formulaire de menu principal
        /// </summary>
        public FormMenu()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gestionnaire d'événement pour le bouton "Dames Françaises (8x8)"
        /// Ouvre un nouveau formulaire de jeu avec un plateau 8x8 et masque le menu principal
        /// </summary>
        /// <param name="sender">Source de l'événement</param>
        /// <param name="e">Arguments de l'événement</param>
        private void btnDamesFrancaises_Click(object sender, EventArgs e)
        {
            FormJeu formJeu = new FormJeu(8); // Plateau 8x8
            formJeu.Show();
            this.Hide();
        }

        /// <summary>
        /// Gestionnaire d'événement pour le bouton "Dames Européennes (10x10)"
        /// Ouvre un nouveau formulaire de jeu avec un plateau 10x10 et masque le menu principal
        /// </summary>
        /// <param name="sender">Source de l'événement</param>
        /// <param name="e">Arguments de l'événement</param>
        private void btnDamesInternationales_Click(object sender, EventArgs e)
        {
            FormJeu formJeu = new FormJeu(10); // Plateau 10x10
            formJeu.Show();
            this.Hide();
        }
    }
}
