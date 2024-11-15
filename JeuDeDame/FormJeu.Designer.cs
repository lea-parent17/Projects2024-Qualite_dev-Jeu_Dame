namespace JeuDeDames
{
    partial class FormJeu
    {
        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources gérées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
          
            this.ClientSize = new System.Drawing.Size(480, 480);
            this.Name = "FormJeu";
            this.Text = "Jeu de Dames";
            this.ResumeLayout(false);
        }

        #endregion

        private System.ComponentModel.IContainer components = null;
    }
}
