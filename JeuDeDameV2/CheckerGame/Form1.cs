using CheckerGame.Models;
using CheckerGame.Controllers;

namespace CheckerGame;

public partial class Form1 : Form
{
    private readonly GameController _gameController;

    public Image whiteFigure;
    public Image blackFigure;
    
    /// <summary>
    /// Initialise une nouvelle instance de la classe <see cref="GameForm"/>
    /// </summary>
    public Form1()
    {
        InitializeComponent();
        
        whiteFigure = new Bitmap(new Bitmap(@"Sprites\w.png"), new Size(Board.CellSize - 10, Board.CellSize - 10));
        blackFigure = new Bitmap(new Bitmap(@"Sprites\b.png"), new Size(Board.CellSize - 10, Board.CellSize - 10));
        
        lblVictory.Hide();
        
        var _board = new Board();
        _gameController = new GameController(_board);
        CreateGameUI(_board);
    }

    /// <summary>
    /// Crée l'interface utilisateur pour représenter le plateau de jeu
    /// </summary>
    /// <param name="board">L'objet <see cref="Board"/> Représentant le plateau de jeu</param>
    public void CreateGameUI(Board board)
    {
        for (int y = 0; y < Board.MapSize; y++)
        {
            for (int x = 0; x < Board.MapSize; x++)
            {
                var button = new Button();
                button.Location = new Point(x * Board.CellSize, y * Board.CellSize);
                button.Size = new Size(Board.CellSize, Board.CellSize);
                button.BackColor = (x + y) % 2 == 0 ? Color.White : Color.Gray;

                button.Click += OnCellClick; 
                board.GameButtons[y, x] = button;
                Controls.Add(button);
            }
        }

        UpdateGameUI(board);
    }

    /// <summary>
    /// Mets à jour l'interface utilisateur pour refléter l'état actuel du plateau de jeu
    /// </summary>
    /// <param name="board">L'objet <see cref="Board"/> Représentant le plateau de jeu</param>
    public void UpdateGameUI(Board board)
    {
        for (int y = 0; y < Board.MapSize; y++)
        {
            for (int x = 0; x < Board.MapSize; x++)
            {
                var button = board.GameButtons[y, x];
                int piece = board.GameMap[y, x];
                button.Image = piece == 1 ? whiteFigure :
                                piece == 2 ? blackFigure : null;
            }
        }
    }

    /// <summary>
    /// Gère le clic sur une cellule du plateau de jeu
    /// </summary>
    /// <param name="pressedButton">Le bouton cliqué par l'utilisateur</param>
    /// <param name="_gameController">L'objet <see cref="GameController"/> représentant le contrôleur des actions de la partie</param>
    public void OnCellClick(object sender, EventArgs e)
    {
        if (_gameController.PreviousButton != null)
        {
            _gameController.PreviousButton.BackColor = _gameController.GetPrevButtonColor(_gameController.PreviousButton);
        }

        _gameController.pressedButton = sender as Button;

        if(_gameController.pressedButton != null && _gameController.Board.GameMap[_gameController.pressedButton.Location.Y/Board.CellSize,_gameController.pressedButton.Location.X/Board.CellSize] != 0 && _gameController.Board.GameMap[_gameController.pressedButton.Location.Y / Board.CellSize, _gameController.pressedButton.Location.X / Board.CellSize] == _gameController.currentPlayer)
        {
            PlayerPossibleMove();
        }
        else
        {
            if (_gameController.isMoving)
            {
                IsMoving();
            }
        }

        _gameController.PreviousButton = _gameController.pressedButton;
    }


    /// <summary>
    /// Méthode qui gère le déplacement d'un pion
    /// </summary>
    /// <param name="_gameController">L'objet <see cref="GameController"/> représentant le contrôleur des actions de la partie</param>
    public void IsMoving()
    {
        _gameController.isContinue = false;
        
        if (Math.Abs(_gameController.pressedButton.Location.X / Board.CellSize - _gameController.PreviousButton.Location.X/Board.CellSize) > 1)
        {
            _gameController.isContinue = true;
            _gameController.DeleteEaten(_gameController.pressedButton, _gameController.PreviousButton);                        
        }
        
        int temp = _gameController.Board.GameMap[_gameController.pressedButton.Location.Y / Board.CellSize, _gameController.pressedButton.Location.X / Board.CellSize];
        _gameController.Board.GameMap[_gameController.pressedButton.Location.Y / Board.CellSize, _gameController.pressedButton.Location.X / Board.CellSize] = _gameController.Board.GameMap[_gameController.PreviousButton.Location.Y / Board.CellSize, _gameController.PreviousButton.Location.X / Board.CellSize];
        _gameController.Board.GameMap[_gameController.PreviousButton.Location.Y / Board.CellSize, _gameController.PreviousButton.Location.X / Board.CellSize] = temp;
        
        _gameController.pressedButton.Image = _gameController.PreviousButton.Image;
        _gameController.PreviousButton.Image = null;
        
        _gameController.pressedButton.Text = _gameController.PreviousButton.Text;
        _gameController.PreviousButton.Text = "";
        
        _gameController.SwitchButtonToCheat(_gameController.pressedButton);
        
        _gameController.countEatSteps = 0;
        _gameController.isMoving = false;
        
        _gameController.CloseSteps();
        _gameController.DeactivateAllButtons();
        
        if (_gameController.pressedButton.Text == "D")
        {
            _gameController.ShowSteps(_gameController.pressedButton.Location.Y / Board.CellSize,
                _gameController.pressedButton.Location.X / Board.CellSize, false);
        }
        else
        {
            _gameController.ShowSteps(_gameController.pressedButton.Location.Y / Board.CellSize,
                _gameController.pressedButton.Location.X / Board.CellSize);
        }
        
        if (_gameController.countEatSteps == 0 || !_gameController.isContinue)
        {
            _gameController.CloseSteps();
            _gameController.SwitchPlayer(lblPlayerWhoPLay, lblVictory);
            _gameController.ShowPossibleSteps();
            _gameController.isContinue = false;
        }
        else if(_gameController.isContinue)
        {
            _gameController.pressedButton.BackColor = Color.Red;
            _gameController.pressedButton.Enabled = true;
            _gameController.isMoving = true;
        }
    }


    /// <summary>
    /// Méthode qui permet d'afficher à l'utilisateur les différents mouvements possibles ou obligatoires
    /// </summary>
    /// <param name="_gameController">L'objet <see cref="GameController"/> représentant le contrôleur des actions de la partie</param>
    public void PlayerPossibleMove()
    {
        _gameController.CloseSteps();
        _gameController.pressedButton.BackColor = Color.Red;
        _gameController.DeactivateAllButtons();
        _gameController.pressedButton.Enabled = true;
        _gameController.countEatSteps = 0;

        if (_gameController.pressedButton.Text == "D")
        {
            _gameController.ShowSteps(_gameController.pressedButton.Location.Y / Board.CellSize, _gameController.pressedButton.Location.X / Board.CellSize,false);
        }
        else
        {
            _gameController.ShowSteps(_gameController.pressedButton.Location.Y / Board.CellSize, _gameController.pressedButton.Location.X / Board.CellSize);
        }

        if (_gameController.isMoving)
        {
            _gameController.CloseSteps();
            _gameController.pressedButton.BackColor = _gameController.GetPrevButtonColor(_gameController.pressedButton);
            _gameController.ShowPossibleSteps();
            _gameController.isMoving = false;
        }
        else
        {
            _gameController.isMoving = true;
        }
    }

    /// <summary>
    /// Remets le plateau à son état initial
    /// </summary>
    public void OnResetGameClick(object sender, EventArgs e)
    {
        var board = new Board();
        
        Controls.Clear();
        
        _gameController.Board = board;
        _gameController.currentPlayer = 1;
        
        CreateGameUI(board);
        
        Controls.Add(clearbtn);
        Controls.Add(lblVictory);
        Controls.Add(lblPlayerWhoPLay);
        
        lblVictory.Hide();
        lblPlayerWhoPLay.Text = "Au blanc de jouer";
    }


    /// <summary>
    /// Expose l'objet _gameController
    /// Pour permetre aux tests d'y accéder sans exposer directement les champs privés
    /// </summary>
    public GameController GetGameController()
    {
        return _gameController;
    }

}