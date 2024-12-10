using System.Windows.Forms;

namespace JeuDeDames
{
    partial class FormMenu
    {
        private System.ComponentModel.IContainer components = null;
        private Button btnDamesFrancaises;
        private Button btnDamesInternationales;

        // Méthode pour libérer les ressources
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        // Méthode d'initialisation des composants
        private void InitializeComponent()
        {
            this.btnDamesFrancaises = new System.Windows.Forms.Button();
            this.btnDamesInternationales = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnDamesFrancaises
            // 
            this.btnDamesFrancaises.Location = new System.Drawing.Point(33, 39);
            this.btnDamesFrancaises.Name = "btnDamesFrancaises";
            this.btnDamesFrancaises.Size = new System.Drawing.Size(280, 60);
            this.btnDamesFrancaises.TabIndex = 0;
            this.btnDamesFrancaises.Text = "Dames Françaises (8x8)";
            this.btnDamesFrancaises.UseVisualStyleBackColor = true;
            this.btnDamesFrancaises.Click += new System.EventHandler(this.btnDamesFrancaises_Click);
            // 
            // btnDamesEuropeennes
            // 
            this.btnDamesInternationales.Location = new System.Drawing.Point(33, 136);
            this.btnDamesInternationales.Name = "btnDamesInternationales";
            this.btnDamesInternationales.Size = new System.Drawing.Size(280, 60);
            this.btnDamesInternationales.TabIndex = 1;
            this.btnDamesInternationales.Text = "Dames Internationales (10x10)";
            this.btnDamesInternationales.UseVisualStyleBackColor = true;
            this.btnDamesInternationales.Click += new System.EventHandler(this.btnDamesInternationales_Click);
            // 
            // FormMenu
            // 
            this.ClientSize = new System.Drawing.Size(344, 232);
            this.Controls.Add(this.btnDamesFrancaises);
            this.Controls.Add(this.btnDamesInternationales);
            this.Name = "FormMenu";
            this.Text = "Jeu de Dames";
            this.ResumeLayout(false);

        }
    }
}
