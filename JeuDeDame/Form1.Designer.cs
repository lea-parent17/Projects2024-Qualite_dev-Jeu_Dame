using System.Windows.Forms;

namespace JeuDeDames
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private Button btnDamesFrancaises;
        private Button btnDamesEuropeennes;

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
            this.btnDamesEuropeennes = new System.Windows.Forms.Button();

            this.SuspendLayout();
            // 
            // btnDamesFrancaises
            // 
            this.btnDamesFrancaises.Location = new System.Drawing.Point(100, 100);
            this.btnDamesFrancaises.Name = "btnDamesFrancaises";
            this.btnDamesFrancaises.Size = new System.Drawing.Size(280, 60);
            this.btnDamesFrancaises.TabIndex = 0;
            this.btnDamesFrancaises.Text = "Dames Françaises (8x8)";
            this.btnDamesFrancaises.UseVisualStyleBackColor = true;
            this.btnDamesFrancaises.Click += new System.EventHandler(this.btnDamesFrancaises_Click);
            // 
            // btnDamesEuropeennes
            // 
            this.btnDamesEuropeennes.Location = new System.Drawing.Point(100, 200);
            this.btnDamesEuropeennes.Name = "btnDamesEuropeennes";
            this.btnDamesEuropeennes.Size = new System.Drawing.Size(280, 60);
            this.btnDamesEuropeennes.TabIndex = 1;
            this.btnDamesEuropeennes.Text = "Dames Européennes (10x10)";
            this.btnDamesEuropeennes.UseVisualStyleBackColor = true;
            this.btnDamesEuropeennes.Click += new System.EventHandler(this.btnDamesEuropeennes_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(480, 480);
            this.Controls.Add(this.btnDamesFrancaises);
            this.Controls.Add(this.btnDamesEuropeennes);
            this.Name = "Form1";
            this.Text = "Jeu de Dames";
            this.ResumeLayout(false);
        }
    }
}
