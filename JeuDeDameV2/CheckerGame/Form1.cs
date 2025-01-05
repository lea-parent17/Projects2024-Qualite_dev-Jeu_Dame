using CheckerGame.Models;
using CheckerGame.Controllers;

namespace CheckerGame;

public partial class Form1 : Form
{
    private readonly GameController _gameController;

    public Image WhiteFigure;
    public Image BlackFigure;
    
    /// <summary>
    /// Initialise une nouvelle instance de la classe <see cref="GameForm"/>
    /// </summary>
    public Form1()
    {
        InitializeComponent();
        
        WhiteFigure = new Bitmap(new Bitmap(@"Sprites\w.png"), new Size(Board.CellSize - 10, Board.CellSize - 10));
        BlackFigure = new Bitmap(new Bitmap(@"Sprites\b.png"), new Size(Board.CellSize - 10, Board.CellSize - 10));
        
        _lblVictory.Hide();
        
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
                button.Image = piece == 1 ? WhiteFigure :
                                piece == 2 ? BlackFigure : null;
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

        _gameController.PressedButton = sender as Button;

        if(_gameController.PressedButton != null && _gameController.Board.GameMap[_gameController.PressedButton.Location.Y/Board.CellSize,_gameController.PressedButton.Location.X/Board.CellSize] != 0 && _gameController.Board.GameMap[_gameController.PressedButton.Location.Y / Board.CellSize, _gameController.PressedButton.Location.X / Board.CellSize] == _gameController.CurrentPlayer)
        {
            PlayerPossibleMove();
        }
        else
        {
            if (_gameController.IsMoving)
            {
                IsMoving();
            }
        }

        _gameController.PreviousButton = _gameController.PressedButton;
    }


    /// <summary>
    /// Méthode qui gère le déplacement d'un pion
    /// </summary>
    /// <param name="_gameController">L'objet <see cref="GameController"/> représentant le contrôleur des actions de la partie</param>
    public void IsMoving()
    {
        _gameController.IsContinue = false;
        
        if (Math.Abs(_gameController.PressedButton.Location.X / Board.CellSize - _gameController.PreviousButton.Location.X/Board.CellSize) > 1)
        {
            _gameController.IsContinue = true;
            _gameController.DeleteEaten(_gameController.PressedButton, _gameController.PreviousButton);                        
        }
        
        int temp = _gameController.Board.GameMap[_gameController.PressedButton.Location.Y / Board.CellSize, _gameController.PressedButton.Location.X / Board.CellSize];
        _gameController.Board.GameMap[_gameController.PressedButton.Location.Y / Board.CellSize, _gameController.PressedButton.Location.X / Board.CellSize] = _gameController.Board.GameMap[_gameController.PreviousButton.Location.Y / Board.CellSize, _gameController.PreviousButton.Location.X / Board.CellSize];
        _gameController.Board.GameMap[_gameController.PreviousButton.Location.Y / Board.CellSize, _gameController.PreviousButton.Location.X / Board.CellSize] = temp;
        
        _gameController.PressedButton.Image = _gameController.PreviousButton.Image;
        _gameController.PreviousButton.Image = null;
        
        _gameController.PressedButton.Text = _gameController.PreviousButton.Text;
        _gameController.PreviousButton.Text = "";
        
        _gameController.SwitchButtonToCheat(_gameController.PressedButton);
        
        _gameController.CountEatSteps = 0;
        _gameController.IsMoving = false;
        
        _gameController.CloseSteps();
        _gameController.DeactivateAllButtons();
        
        if (_gameController.PressedButton.Text == "D")
        {
            _gameController.ShowSteps(_gameController.PressedButton.Location.Y / Board.CellSize,
                _gameController.PressedButton.Location.X / Board.CellSize, false);
        }
        else
        {
            _gameController.ShowSteps(_gameController.PressedButton.Location.Y / Board.CellSize,
                _gameController.PressedButton.Location.X / Board.CellSize);
        }
        
        if (_gameController.CountEatSteps == 0 || !_gameController.IsContinue)
        {
            _gameController.CloseSteps();
            _gameController.SwitchPlayer(_lblPlayerWhoPLay, _lblVictory);
            _gameController.ShowPossibleSteps();
            _gameController.IsContinue = false;
        }
        else if(_gameController.IsContinue)
        {
            _gameController.PressedButton.BackColor = Color.Red;
            _gameController.PressedButton.Enabled = true;
            _gameController.IsMoving = true;
        }
    }


    /// <summary>
    /// Méthode qui permet d'afficher à l'utilisateur les différents mouvements possibles ou obligatoires
    /// </summary>
    /// <param name="_gameController">L'objet <see cref="GameController"/> représentant le contrôleur des actions de la partie</param>
    public void PlayerPossibleMove()
    {
        _gameController.CloseSteps();
        _gameController.PressedButton.BackColor = Color.Red;
        _gameController.DeactivateAllButtons();
        _gameController.PressedButton.Enabled = true;
        _gameController.CountEatSteps = 0;

        if (_gameController.PressedButton.Text == "D")
        {
            _gameController.ShowSteps(_gameController.PressedButton.Location.Y / Board.CellSize, _gameController.PressedButton.Location.X / Board.CellSize,false);
        }
        else
        {
            _gameController.ShowSteps(_gameController.PressedButton.Location.Y / Board.CellSize, _gameController.PressedButton.Location.X / Board.CellSize);
        }

        if (_gameController.IsMoving)
        {
            _gameController.CloseSteps();
            _gameController.PressedButton.BackColor = _gameController.GetPrevButtonColor(_gameController.PressedButton);
            _gameController.ShowPossibleSteps();
            _gameController.IsMoving = false;
        }
        else
        {
            _gameController.IsMoving = true;
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
        _gameController.CurrentPlayer = 1;
        
        CreateGameUI(board);
        
        Controls.Add(_clearbtn);
        Controls.Add(_lblVictory);
        Controls.Add(_lblPlayerWhoPLay);
        
        _lblVictory.Hide();
        _lblPlayerWhoPLay.Text = "Au blanc de jouer";
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