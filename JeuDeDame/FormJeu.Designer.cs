namespace JeuDeDames
{
    partial class FormJeu
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FormJeu
            // 
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "FormJeu";
            this.Text = "Jeu de Dames";
            this.ResumeLayout(false);
        }
    }
}
