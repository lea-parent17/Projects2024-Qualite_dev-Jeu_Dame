using System.ComponentModel;

namespace CheckerGame;

partial class Form1
{
    /// <summary>
    /// Container for managed resources.
    /// </summary>
    private IContainer _components = null;

    /// <summary>
    /// Label to display the victory message.
    /// </summary>
    private Label _lblVictoryMessage;

    /// <summary>
    /// Releases the resources used by the GameForm.
    /// </summary>
    /// <param name="disposing">Indicates whether to dispose managed resources.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (_components != null))
        {
            _components.Dispose();
        }
        base.Dispose(disposing);
    }

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        _lblVictoryMessage = new Label();
        _clearbtn = new Button();
        _lblPlayerWhoPLay = new Label();
        _lblVictory = new Label();
        SuspendLayout();
        // 
        // lblVictoryMessage
        // 
        _lblVictoryMessage.Location = new Point(0, 0);
        _lblVictoryMessage.Name = "lblVictoryMessage";
        _lblVictoryMessage.Size = new Size(100, 23);
        _lblVictoryMessage.TabIndex = 0;
        // 
        // clearbtn
        // 
        _clearbtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _clearbtn.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
        _clearbtn.Location = new Point(464, 12);
        _clearbtn.Name = "clearbtn";
        _clearbtn.Size = new Size(137, 32);
        _clearbtn.TabIndex = 0;
        _clearbtn.Text = "Réinitialiser";
        _clearbtn.UseVisualStyleBackColor = true;
        _clearbtn.Click += OnResetGameClick;
        // 
        // lblPlayerWhoPLay
        // 
        _lblPlayerWhoPLay.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _lblPlayerWhoPLay.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
        _lblPlayerWhoPLay.Location = new Point(445, 47);
        _lblPlayerWhoPLay.Name = "lblPlayerWhoPLay";
        _lblPlayerWhoPLay.Size = new Size(171, 28);
        _lblPlayerWhoPLay.TabIndex = 2;
        _lblPlayerWhoPLay.Text = "Au blanc de jouer";
        _lblPlayerWhoPLay.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblVictory
        // 
        _lblVictory.BackColor = Color.Transparent;
        _lblVictory.Font = new Font("Arial", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
        _lblVictory.Location = new Point(104, 160);
        _lblVictory.Name = "lblVictory";
        _lblVictory.Size = new Size(404, 109);
        _lblVictory.TabIndex = 3;
        _lblVictory.Text = "Victoire du joueur blanc !!!";
        _lblVictory.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // Form1
        // 
        ClientSize = new Size(628, 449);
        Controls.Add(_lblPlayerWhoPLay);
        Controls.Add(_clearbtn);
        Controls.Add(_lblVictory);
        Name = "Form1";
        ResumeLayout(false);
    }

    private System.Windows.Forms.Label _lblVictory;

    private System.Windows.Forms.Label _lblPlayerWhoPLay;

    private System.Windows.Forms.Button _clearbtn;
}