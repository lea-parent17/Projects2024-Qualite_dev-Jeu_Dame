using System.ComponentModel;

namespace CheckerGame;

partial class Form1
{
    /// <summary>
    /// Container for managed resources.
    /// </summary>
    private IContainer components = null;

    /// <summary>
    /// Label to display the victory message.
    /// </summary>
    private Label lblVictoryMessage;

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
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        lblVictoryMessage = new Label();
        clearbtn = new Button();
        lblPlayerWhoPLay = new Label();
        lblVictory = new Label();
        SuspendLayout();
        // 
        // lblVictoryMessage
        // 
        lblVictoryMessage.Location = new Point(0, 0);
        lblVictoryMessage.Name = "lblVictoryMessage";
        lblVictoryMessage.Size = new Size(100, 23);
        lblVictoryMessage.TabIndex = 0;
        // 
        // clearbtn
        // 
        clearbtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        clearbtn.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
        clearbtn.Location = new Point(464, 12);
        clearbtn.Name = "clearbtn";
        clearbtn.Size = new Size(137, 32);
        clearbtn.TabIndex = 0;
        clearbtn.Text = "Réinitialiser";
        clearbtn.UseVisualStyleBackColor = true;
        clearbtn.Click += OnResetGameClick;
        // 
        // lblPlayerWhoPLay
        // 
        lblPlayerWhoPLay.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblPlayerWhoPLay.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
        lblPlayerWhoPLay.Location = new Point(445, 47);
        lblPlayerWhoPLay.Name = "lblPlayerWhoPLay";
        lblPlayerWhoPLay.Size = new Size(171, 28);
        lblPlayerWhoPLay.TabIndex = 2;
        lblPlayerWhoPLay.Text = "Au blanc de jouer";
        lblPlayerWhoPLay.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblVictory
        // 
        lblVictory.BackColor = Color.Transparent;
        lblVictory.Font = new Font("Arial", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
        lblVictory.Location = new Point(104, 160);
        lblVictory.Name = "lblVictory";
        lblVictory.Size = new Size(404, 109);
        lblVictory.TabIndex = 3;
        lblVictory.Text = "Victoire du joueur blanc !!!";
        lblVictory.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // Form1
        // 
        ClientSize = new Size(628, 449);
        Controls.Add(lblPlayerWhoPLay);
        Controls.Add(clearbtn);
        Controls.Add(lblVictory);
        Name = "Form1";
        ResumeLayout(false);
    }

    private System.Windows.Forms.Label lblVictory;

    private System.Windows.Forms.Label lblPlayerWhoPLay;

    private System.Windows.Forms.Button clearbtn;
}