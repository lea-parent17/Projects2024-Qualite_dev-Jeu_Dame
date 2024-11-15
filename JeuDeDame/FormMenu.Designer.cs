namespace JeuDeDames
{
    partial class FormMenu
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
            this.btnDamesFrancaises = new System.Windows.Forms.Button();
            this.btnDamesEuropeennes = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // btnDamesFrancaises
            this.btnDamesFrancaises.Location = new System.Drawing.Point(50, 30);
            this.btnDamesFrancaises.Name = "btnDamesFrancaises";
            this.btnDamesFrancaises.Size = new System.Drawing.Size(200, 50);
            this.btnDamesFrancaises.Text = "Dames Françaises";
            this.btnDamesFrancaises.UseVisualStyleBackColor = true;
            this.btnDamesFrancaises.Click += new System.EventHandler(this.btnDamesFrancaises_Click);

            // btnDamesEuropeennes
            this.btnDamesEuropeennes.Location = new System.Drawing.Point(50, 100);
            this.btnDamesEuropeennes.Name = "btnDamesEuropeennes";
            this.btnDamesEuropeennes.Size = new System.Drawing.Size(200, 50);
            this.btnDamesEuropeennes.Text = "Dames Européennes";
            this.btnDamesEuropeennes.UseVisualStyleBackColor = true;
            this.btnDamesEuropeennes.Click += new System.EventHandler(this.btnDamesEuropeennes_Click);

            // FormMenu
            this.ClientSize = new System.Drawing.Size(300, 200);
            this.Controls.Add(this.btnDamesFrancaises);
            this.Controls.Add(this.btnDamesEuropeennes);
            this.Text = "Menu - Jeu de Dames";
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button btnDamesFrancaises;
        private System.Windows.Forms.Button btnDamesEuropeennes;
    }
}
