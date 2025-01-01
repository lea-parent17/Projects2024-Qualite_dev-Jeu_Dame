namespace JeuV2
{
    /// <summary>
    /// Partial class for the GameForm UI, containing the generated UI components and their initialization.
    /// </summary>
    partial class GameForm
    {
        /// <summary>
        /// Container for managed resources.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Label to display the victory message.
        /// </summary>
        private Label lblVictoryMessage;

        /// <summary>
        /// Button to reset the game.
        /// </summary>
        private Button btnResetGame;

        /// <summary>
        /// Releases the resources used by the GameForm.
        /// </summary>
        /// <param name="disposing">Indicates whether to dispose managed resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Initializes the components of the form, setting up the UI layout and event handlers.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblVictoryMessage = new System.Windows.Forms.Label();
            this.btnResetGame = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // 
            // btnResetGame
            // 
            this.btnResetGame.Location = new System.Drawing.Point(20, 50); // Sets the button's position.
            this.btnResetGame.Name = "btnResetGame";
            this.btnResetGame.Size = new System.Drawing.Size(100, 30); // Sets the button's dimensions.
            this.btnResetGame.TabIndex = 1;
            this.btnResetGame.Text = "Réinitialiser"; // Sets the button's label.
            this.btnResetGame.UseVisualStyleBackColor = true;
            this.btnResetGame.Click += new System.EventHandler(this.OnResetGameClick); // Adds the click event handler.

            // 
            // lblVictoryMessage
            // 
            // Placeholder for additional setup if needed.

            // Add controls to the form.
            this.Controls.Add(this.btnResetGame);

            this.ResumeLayout(false);
        }
    }
}
