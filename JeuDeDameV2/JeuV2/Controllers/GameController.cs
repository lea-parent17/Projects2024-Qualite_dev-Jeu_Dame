using JeuV2.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace JeuV2.Controllers
{
    public class GameController
    {
        private readonly Board _board;

        /// <summary>
        /// Identifie le joueur courant (1 ou 2).
        /// </summary>
        public int CurrentPlayer { get; private set; }

        /// <summary>
        /// Bouton précédemment sélectionné.
        /// </summary>
        public Button PreviousButton { get; private set; }

        /// <summary>
        /// Bouton actuellement actif.
        /// </summary>
        public Button ActiveButton { get; private set; }

        /// <summary>
        /// Indique si une piece est en cours de déplacement.
        /// </summary>
        public bool IsMoving { get; private set; }

        /// <summary>
        /// Liste des boutons disponibles pour les déplacements.
        /// </summary>
        public List<Button> AvailableMoves { get; private set; }

        /// <summary>
        /// Compte le nombre de déplacements oé une pièce est mangée.
        /// </summary>
        public int EatMoveCount { get; private set; }

        /// <summary>
        /// Initialise le controleur de jeu avec un plateau donne.
        /// </summary>
        /// <param name="board">Instance du plateau de jeu.</param>
        public GameController(Board board)
        {
            _board = board;
            CurrentPlayer = 1;
            AvailableMoves = new List<Button>();
        }

        /// <summary>
        /// Permet de changer le joueur actif.
        /// </summary>
        public void SwitchPlayer()
        {
            CurrentPlayer = CurrentPlayer == 1 ? 2 : 1;
        }

        /// <summary>
        /// Reinitialise le jeu si l'un des joueurs n'a plus de pieces.
        /// </summary>
        public void ResetGame()
        {
            bool player1Exists = false;
            bool player2Exists = false;

            for (int i = 0; i < Board.MapSize; i++)
            {
                for (int j = 0; j < Board.MapSize; j++)
                {
                    if (_board.GameMap[i, j] == 1) player1Exists = true;
                    if (_board.GameMap[i, j] == 2) player2Exists = true;
                }
            }

            if (!player1Exists || !player2Exists)
            {
                _board.InitializeBoard();
                IsMoving = false;
                PreviousButton = null;
            }
        }

        /// <summary>
        /// Deplace une piece du bouton de depart au bouton de destination.
        /// </summary>
        /// <param name="startButton">Bouton de depart.</param>
        /// <param name="endButton">Bouton de destination.</param>
        public void MovePiece(Button startButton, Button endButton)
        {
            int startX = startButton.Location.X / Board.CellSize;
            int startY = startButton.Location.Y / Board.CellSize;
            int endX = endButton.Location.X / Board.CellSize;
            int endY = endButton.Location.Y / Board.CellSize;

            // Mise a jour de la carte du jeu
            _board.GameMap[endY, endX] = _board.GameMap[startY, startX];
            _board.GameMap[startY, startX] = 0;

            // Mise a jour des images des boutons
            endButton.Image = startButton.Image;
            startButton.Image = null;
            PreviousButton = endButton;

            // Verifie si une victoire a eu lieu
            int? winner = CheckVictory();
            if (winner.HasValue)
            {
                ShowVictoryMessage(winner.Value);
                return;
            }
        }

        /// <summary>
        /// Affiche un message de victoire.
        /// </summary>
        /// <param name="winner">Numero du joueur gagnant.</param>
        private void ShowVictoryMessage(int winner)
        {
            MessageBox.Show($"Le joueur {winner} a gagne la partie !", "Fin de la partie", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Affiche les deplacements diagonaux possibles pour une piece donnee.
        /// </summary>
        /// <param name="IcurrFigure">Position verticale actuelle de la piece.</param>
        /// <param name="JcurrFigure">Position horizontale actuelle de la piece.</param>
        /// <param name="isOneStep">Indique si la piece se deplace d'un seul pas.</param>
        public void ShowDiagonal(int IcurrFigure, int JcurrFigure, bool isOneStep = false)
        {
            // Logique pour calculer les mouvements diagonaux...
        }

        /// <summary>
        /// Supprime les pieces mangees pendant un deplacement.
        /// </summary>
        /// <param name="endButton">Bouton de destination.</param>
        /// <param name="startButton">Bouton de depart.</param>
        public void DeleteEaten(Button endButton, Button startButton)
        {
            int count = Math.Abs(endButton.Location.Y / Board.CellSize - startButton.Location.Y / Board.CellSize);
            int dirX = (endButton.Location.Y / Board.CellSize - startButton.Location.Y / Board.CellSize) < 0 ? -1 : 1;
            int dirY = (endButton.Location.X / Board.CellSize - startButton.Location.X / Board.CellSize) < 0 ? -1 : 1;

            for (int i = 1; i < count; i++)
            {
                int x = startButton.Location.X / Board.CellSize + dirX * i;
                int y = startButton.Location.Y / Board.CellSize + dirY * i;
                _board.GameMap[y, x] = 0;
                _board.GameButtons[y, x].Image = null;
            }
        }

        /// <summary>
        /// Verifie si une case a une etape pour manger une autre piece.
        /// </summary>
        /// <param name="IcurrFigure">Position verticale de la piece actuelle.</param>
        /// <param name="JcurrFigure">Position horizontale de la piece actuelle.</param>
        /// <param name="isOneStep">Indique si la verification concerne un seul pas.</param>
        /// <param name="dir">Direction du deplacement.</param>
        /// <returns>True si une piece peut etre mangee, sinon false.</returns>
        public bool IsButtonHasEatStep(int IcurrFigure, int JcurrFigure, bool isOneStep, int[] dir)
        {
            // Logique pour verifier les etapes ou une piece peut etre mangee...
            return false;
        }

        /// <summary>
        /// Verifie si l'un des joueurs a gagne la partie.
        /// </summary>
        /// <returns>Numero du joueur gagnant ou null si aucun joueur n'a gagne.</returns>
        public int? CheckVictory()
        {
            bool player1Exists = false;
            bool player2Exists = false;

            for (int i = 0; i < Board.MapSize; i++)
            {
                for (int j = 0; j < Board.MapSize; j++)
                {
                    if (_board.GameMap[i, j] == 1) player1Exists = true;
                    if (_board.GameMap[i, j] == 2) player2Exists = true;
                }
            }

            if (!player1Exists) return 2; 
            if (!player2Exists) return 1; 

            return null; 
        }
    }
}
