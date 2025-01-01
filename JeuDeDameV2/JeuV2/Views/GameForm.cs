using JeuV2.Controllers;
using JeuV2.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace JeuV2.Views
{
    /// <summary>
    /// Représente le formulaire principal pour l'affichage et l'interaction du jeu.
    /// </summary>
    public partial class GameForm : Form
    {
        /// <summary>
        /// Contrôleur principal du jeu, responsable de la logique du jeu.
        /// </summary>
        private readonly GameController _gameController;

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="GameForm"/>.
        /// </summary>
        public GameForm()
        {
            InitializeComponent();

            var board = new Board();
            _gameController = new GameController(board);

            CreateGameUI(board);
        }

        /// <summary>
        /// Crée l'interface utilisateur pour représenter le plateau de jeu.
        /// </summary>
        /// <param name="board">L'objet <see cref="Board"/> représentant le plateau de jeu.</param>
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

        /// <summary>
        /// Met à jour l'interface utilisateur pour refléter l'état actuel du plateau de jeu.
        /// </summary>
        /// <param name="board">L'objet <see cref="Board"/> représentant le plateau de jeu.</param>
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

        /// <summary>
        /// Gère le clic sur une cellule du plateau de jeu.
        /// </summary>
        /// <param name="clickedButton">Le bouton cliqué par l'utilisateur.</param>
        /// <param name="board">L'objet <see cref="Board"/> représentant le plateau de jeu.</param>
        private void OnCellClick(Button clickedButton, Board board)
        {
            if (_gameController.PreviousButton == null)
            {
                _gameController.PreviousButton = clickedButton;
            }
            else
            {
                _gameController.MovePiece(_gameController.PreviousButton, clickedButton);
                int? winner = _gameController.CheckVictory();
                if (winner.HasValue)
                {
                    DisplayVictoryMessage(winner.Value);
                    return; // Arrêtez les interactions
                }

                _gameController.SwitchPlayer();
                _gameController.PreviousButton = null;
                UpdateGameUI(board);
            }
        }

        /// <summary>
        /// Affiche un message de victoire pour le joueur gagnant.
        /// </summary>
        /// <param name="winner">Le numéro du joueur gagnant (1 ou 2).</param>
        private void DisplayVictoryMessage(int winner)
        {
            lblVictoryMessage.Text = $"Victoire du joueur {(winner == 1 ? "blanc" : "noir")}";
        }

        /// <summary>
        /// Réinitialise le jeu lorsque le bouton de réinitialisation est cliqué.
        /// </summary>
        /// <param name="sender">L'objet à l'origine de l'événement.</param>
        /// <param name="e">Les données de l'événement.</param>
        private void OnResetGameClick(object sender, EventArgs e)
        {
            _gameController.ResetGame();
            lblVictoryMessage.Text = ""; // Réinitialise le message
            UpdateGameUI(_gameController.Board); // Rafraîchit l'interface
        }
    }
}
