using JeuV2.Controllers;
using JeuV2.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace JeuV2.Views
{
    public partial class GameForm : Form
    {
        private readonly GameController _gameController;

        public GameForm()
        {
            InitializeComponent();

            var board = new Board();
            _gameController = new GameController(board);

            CreateGameUI(board);
        }

        private void CreateGameUI(Board board)
        {
            for (int y = 0; y < Board.MapSize; y++)
            {
                for (int x = 0; x < Board.MapSize; x++)
                {
                    var button = new Button
                    {
                        Location = new Point(x * Board.CellSize, y * Board.CellSize),
                        Size = new Size(Board.CellSize, Board.CellSize),
                        BackColor = (x + y) % 2 == 0 ? Color.White : Color.Gray
                    };

                    button.Click += (sender, args) => OnCellClick(button, board);
                    board.GameButtons[y, x] = button;
                    Controls.Add(button);
                }
            }

            UpdateGameUI(board);
        }

        private void UpdateGameUI(Board board)
        {
            for (int y = 0; y < Board.MapSize; y++)
            {
                for (int x = 0; x < Board.MapSize; x++)
                {
                    var button = board.GameButtons[y, x];
                    int piece = board.GameMap[y, x];
                    button.Image = piece == 1 ? Image.FromFile("Sprites/w.png") :
                                    piece == 2 ? Image.FromFile("Sprites/b.png") : null;
                }
            }
        }

        private void OnCellClick(Button clickedButton, Board board)
        {
            if (_gameController.PreviousButton == null)
            {
                _gameController.PreviousButton = clickedButton;
            }
            else
            {
                _gameController.MovePiece(_gameController.PreviousButton, clickedButton);
                _gameController.SwitchPlayer();
                _gameController.PreviousButton = null;
                UpdateGameUI(board);
            }
        }
    }
}
