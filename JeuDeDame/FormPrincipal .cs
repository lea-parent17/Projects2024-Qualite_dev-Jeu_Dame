using System;
using System.Windows.Forms;

namespace JeuDeDames
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void btnDamesFrancaises_Click(object sender, EventArgs e)
        {
            FormJeu formJeu = new FormJeu(8); // Plateau 8x8
            formJeu.Show();
        }

        private void btnDamesEuropeennes_Click(object sender, EventArgs e)
        {
            FormJeu formJeu = new FormJeu(10); // Plateau 10x10
            formJeu.Show();
        }
    }
}
