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
        public int CurrentPlayer { get; private set; }
        public Button PreviousButton { get; private set; }
        public Button ActiveButton { get; private set; }
        public bool IsMoving { get; private set; }
        public List<Button> AvailableMoves { get; private set; }
        public int EatMoveCount { get; private set; }

        public GameController(Board board)
        {
            _board = board;
            CurrentPlayer = 1;
            AvailableMoves = new List<Button>();
        }

        public void SwitchPlayer()
        {
            CurrentPlayer = CurrentPlayer == 1 ? 2 : 1;
        }

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

        public void MovePiece(Button startButton, Button endButton)
        {
            int startX = startButton.Location.X / Board.CellSize;
            int startY = startButton.Location.Y / Board.CellSize;
            int endX = endButton.Location.X / Board.CellSize;
            int endY = endButton.Location.Y / Board.CellSize;

            _board.GameMap[endY, endX] = _board.GameMap[startY, startX];
            _board.GameMap[startY, startX] = 0;

            endButton.Image = startButton.Image;
            startButton.Image = null;
            PreviousButton = endButton;

            // Vérifiez la victoire
            int? winner = CheckVictory();
            if (winner.HasValue)
            {
                ShowVictoryMessage(winner.Value);
                return; // Ne pas réinitialiser immédiatement
            }
        }

        private void ShowVictoryMessage(int winner)
        {
            // Affiche un message de victoire sans réinitialiser directement
            MessageBox.Show($"Le joueur {winner} a gagné la partie !");
            ResetGame(); // Appelé uniquement après que l'utilisateur a fermé le message
        }





        public void ShowDiagonal(int IcurrFigure, int JcurrFigure, bool isOneStep = false)
        {
            int j = JcurrFigure + 1;
            for (int i = IcurrFigure - 1; i >= 0; i--)
            {
                if (CurrentPlayer == 1 && isOneStep && !IsContinue) break;
                if (IsInsideBorders(i, j))
                {
                    if (!DeterminePath(i, j))
                        break;
                }
                if (j < 7)
                    j++;
                else break;

                if (isOneStep)
                    break;
            }

            j = JcurrFigure - 1;
            for (int i = IcurrFigure - 1; i >= 0; i--)
            {
                if (CurrentPlayer == 1 && isOneStep && !IsContinue) break;
                if (IsInsideBorders(i, j))
                {
                    if (!DeterminePath(i, j))
                        break;
                }
                if (j > 0)
                    j--;
                else break;

                if (isOneStep)
                    break;
            }

            j = JcurrFigure - 1;
            for (int i = IcurrFigure + 1; i < 8; i++)
            {
                if (CurrentPlayer == 2 && isOneStep && !IsContinue) break;
                if (IsInsideBorders(i, j))
                {
                    if (!DeterminePath(i, j))
                        break;
                }
                if (j > 0)
                    j--;
                else break;

                if (isOneStep)
                    break;
            }

            j = JcurrFigure + 1;
            for (int i = IcurrFigure + 1; i < 8; i++)
            {
                if (CurrentPlayer == 2 && isOneStep && !IsContinue) break;
                if (IsInsideBorders(i, j))
                {
                    if (!DeterminePath(i, j))
                        break;
                }
                if (j < 7)
                    j++;
                else break;

                if (isOneStep)
                    break;
            }
        }

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

        public bool IsButtonHasEatStep(int IcurrFigure, int JcurrFigure, bool isOneStep, int[] dir)
        {
            bool eatStep = false;
            int j = JcurrFigure + 1;
            for (int i = IcurrFigure - 1; i >= 0; i--)
            {
                if (CurrentPlayer == 1 && isOneStep && !IsContinue) break;
                if (dir[0] == 1 && dir[1] == -1 && !isOneStep) break;
                if (IsInsideBorders(i, j))
                {
                    if (GameMap[i, j] != 0 && GameMap[i, j] != CurrentPlayer)
                    {
                        eatStep = true;
                        if (!IsInsideBorders(i - 1, j + 1))
                            eatStep = false;
                        else if (GameMap[i - 1, j + 1] != 0)
                            eatStep = false;
                        else return eatStep;
                    }
                }
                if (j < 7)
                    j++;
                else break;

                if (isOneStep)
                    break;
            }

            j = JcurrFigure - 1;
            for (int i = IcurrFigure - 1; i >= 0; i--)
            {
                if (CurrentPlayer == 1 && isOneStep && !IsContinue) break;
                if (dir[0] == 1 && dir[1] == 1 && !isOneStep) break;
                if (IsInsideBorders(i, j))
                {
                    if (GameMap[i, j] != 0 && GameMap[i, j] != CurrentPlayer)
                    {
                        eatStep = true;
                        if (!IsInsideBorders(i - 1, j - 1))
                            eatStep = false;
                        else if (GameMap[i - 1, j - 1] != 0)
                            eatStep = false;
                        else return eatStep;
                    }
                }
                if (j > 0)
                    j--;
                else break;

                if (isOneStep)
                    break;
            }

            j = JcurrFigure - 1;
            for (int i = IcurrFigure + 1; i < 8; i++)
            {
                if (CurrentPlayer == 2 && isOneStep && !IsContinue) break;
                if (dir[0] == -1 && dir[1] == 1 && !isOneStep) break;
                if (IsInsideBorders(i, j))
                {
                    if (GameMap[i, j] != 0 && GameMap[i, j] != CurrentPlayer)
                    {
                        eatStep = true;
                        if (!IsInsideBorders(i + 1, j - 1))
                            eatStep = false;
                        else if (GameMap[i + 1, j - 1] != 0)
                            eatStep = false;
                        else return eatStep;
                    }
                }
                if (j > 0)
                    j--;
                else break;

                if (isOneStep)
                    break;
            }

            j = JcurrFigure + 1;
            for (int i = IcurrFigure + 1; i < 8; i++)
            {
                if (CurrentPlayer == 2 && isOneStep && !IsContinue) break;
                if (dir[0] == -1 && dir[1] == -1 && !isOneStep) break;
                if (IsInsideBorders(i, j))
                {
                    if (GameMap[i, j] != 0 && GameMap[i, j] != CurrentPlayer)
                    {
                        eatStep = true;
                        if (!IsInsideBorders(i + 1, j + 1))
                            eatStep = false;
                        else if (GameMap[i + 1, j + 1] != 0)
                            eatStep = false;
                        else return eatStep;
                    }
                }
                if (j < 7)
                    j++;
                else break;

                if (isOneStep)
                    break;
            }
            return eatStep;
        }

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

            if (!player1Exists) return 2; // Joueur 2 gagne
            if (!player2Exists) return 1; // Joueur 1 gagne

            return null; // Pas de victoire
        }


    }
}