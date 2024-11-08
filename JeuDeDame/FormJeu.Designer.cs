namespace JeuDeDames
{
    partial class FormJeu
    {
        private System.ComponentModel.IContainer components = null;

        // Ce code est généré automatiquement pour gérer l'interface visuelle
        private void InitializeComponent()
        {
            this.btnDamesFrancaises = new System.Windows.Forms.Button();
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
            // FormJeu
            // 
            this.ClientSize = new System.Drawing.Size(480, 480);
            this.Controls.Add(this.btnDamesFrancaises);
            this.Name = "FormJeu";
            this.Text = "Jeu de Dames";
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button btnDamesFrancaises;
    }
}
