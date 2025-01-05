using CheckerGame.Models;

namespace CheckerGame.Controllers;

public class GameController
{
    public Board Board { get; set; }

    public Button PreviousButton { get; set; }

    public Button pressedButton { get; set; }

    public int countEatSteps { get; set; }

    public int currentPlayer { get; set; }

    public bool isContinue { get; set; }

    public bool isMoving { get; set; }

    public List<Button> simpleSteps = new List<Button>();

    public GameController(Board board)
    {
        currentPlayer = 1;
        isMoving = false;
        Board = board;
        PreviousButton = null;
    }


    /// <summary>
    /// Permet de sélectionner la couleur de la case
    /// </summary>
    /// <param name="prevButton">bouton sur lequel on va appliquer la couleur</param>
    /// <returns>la future couleur du bouton</returns>
    public Color GetPrevButtonColor(Button prevButton)
    {
        if (((prevButton.Location.Y / Board.CellSize % 2) != 0 && (prevButton.Location.X / Board.CellSize % 2) == 0) || ((prevButton.Location.Y / Board.CellSize) % 2 == 0 && (prevButton.Location.X / Board.CellSize) % 2 != 0))
        {
            return Color.Gray;
        }
        return Color.White;
    }

    /// <summary>
    /// Applique la couleur appropriée à chaque case
    /// </summary>
    public void CloseSteps()
    {
        for (int i = 0; i < Board.MapSize; i++)
        {
            for (int j = 0; j < Board.MapSize; j++)
            {
                if (Board.GameButtons[i, j] != null) // Vérification si le bouton est initialisé
                {
                    Board.GameButtons[i, j].BackColor = GetPrevButtonColor(Board.GameButtons[i, j]);
                }

            }
        }
    }

    /// <summary>
    /// Affiche les mouvements autorisées ou obligatoire
    /// </summary>
    /// <param name="iCurrFigure">Position I du pion sur le plateau</param>
    /// <param name="jCurrFigure">Position J du pion sur le plateau</param>
    /// <param name="isOnestep">si le pion ne peut se deplacer que d'un mouvement (s'il est une dame ou pas)</param>
    public void ShowSteps(int iCurrFigure, int jCurrFigure, bool isOnestep = true)
    {
        simpleSteps.Clear();
        ShowDiagonal(iCurrFigure, jCurrFigure, isOnestep);
        if (countEatSteps > 0)
        {
            CloseSimpleSteps(simpleSteps);
        }
    }

    /// <summary>
    /// Affiche les mouvements possibles
    /// </summary>
    /// <param name="iCurrFigure">Position I du pion sur le plateau</param>
    /// <param name="jCurrFigure">Position J du pion sur le plateau</param>
    /// <param name="isOnestep">Si le pion ne peut se déplacer que d'un mouvement (s'il est une dame ou pas)</param>
    public void ShowDiagonal(int icurrFigure, int jcurrFigure, bool isOneStep = false)
    {
        var j = jcurrFigure + 1;
        for (var i = icurrFigure - 1; i >= 0; i--)
        {
            if (currentPlayer == 1 && isOneStep && !isContinue)
            {
                break;
            }

            if (Board.IsInsideBorders(i, j))
            {
                if (!DeterminePath(i, j))
                    break;
            }

            if (j < 7)
            {
                j++;
            }
            else
            {
                break;
            }

            if (isOneStep)
            {
                break;
            }
        }

        j = jcurrFigure - 1;
        for (var i = icurrFigure - 1; i >= 0; i--)
        {
            if (currentPlayer == 1 && isOneStep && !isContinue)
            {
                break;
            }

            if (Board.IsInsideBorders(i, j))
            {
                if (!DeterminePath(i, j))
                {
                    break;
                }
            }
            if (j > 0)
            {
                j--;
            }
            else
            {
                break;
            }

            if (isOneStep)
            {
                break;
            }
        }

        j = jcurrFigure - 1;
        for (var i = icurrFigure + 1; i < 8; i++)
        {
            if (currentPlayer == 2 && isOneStep && !isContinue)
            {
                break;
            }

            if (Board.IsInsideBorders(i, j))
            {
                if (!DeterminePath(i, j))
                {
                    break;
                }
            }
            if (j > 0)
            {
                j--;
            }
            else
            {
                break;
            }

            if (isOneStep)
            {
                break;
            }
        }

        j = jcurrFigure + 1;
        for (var i = icurrFigure + 1; i < 8; i++)
        {
            if (currentPlayer == 2 && isOneStep && !isContinue)
            {
                break;
            }

            if (Board.IsInsideBorders(i, j))
            {
                if (!DeterminePath(i, j))
                {
                    break;
                }
            }
            if (j < 7)
            {
                j++;
            }
            else
            {
                break;
            }

            if (isOneStep)
            {
                break;
            }
        }
    }

    /// <summary>
    /// Active et mets en jaune les cases où le pion peut se déplacer
    /// </summary>
    /// <param name="ti">Position i de la case</param>
    /// <param name="tj">Position j de la case</param>
    public bool DeterminePath(int ti, int tj)
    {
        if (Board.GameMap[ti, tj] == 0 && !isContinue)
        {
            Board.GameButtons[ti, tj].BackColor = Color.Yellow;
            Board.GameButtons[ti, tj].Enabled = true;
            simpleSteps.Add(Board.GameButtons[ti, tj]);
        }
        else
        {
            if (Board.GameMap[ti, tj] != currentPlayer)
            {
                if (pressedButton.Text == "D")
                {
                    ShowProceduralEat(ti, tj, false);
                }
                else
                {
                    ShowProceduralEat(ti, tj);
                }
            }
            return false;
        }
        return true;
    }

    /// <summary>
    /// Affiche le chemin pour manger un pion adverse
    /// </summary>
    /// <param name="i">Position i de la case</param>
    /// <param name="j">Position j de la case</param>
    /// <param name="isOneStep">Si le pion ne peut se déplacer que d'un mouvement (s'il est une dame ou pas)</param>
    public void ShowProceduralEat(int i, int j, bool isOneStep = true)
    {
        int dirX = i - pressedButton.Location.Y / Board.CellSize;
        int dirY = j - pressedButton.Location.X / Board.CellSize;
        dirX = dirX < 0 ? -1 : 1;
        dirY = dirY < 0 ? -1 : 1;
        int il = i;
        int jl = j;
        bool isEmpty = true;

        while (Board.IsInsideBorders(il, jl))
        {
            if (Board.GameMap[il, jl] != 0 && Board.GameMap[il, jl] != currentPlayer)
            {
                isEmpty = false;
                break;
            }
            il += dirX;
            jl += dirY;

            if (isOneStep)
                break;
        }

        if (isEmpty)
        {
            return;
        }

        List<Button> toClose = new List<Button>();
        bool closeSimple = false;
        int ik = il + dirX;
        int jk = jl + dirY;

        while (Board.IsInsideBorders(ik, jk))
        {
            if (Board.GameMap[ik, jk] == 0)
            {
                if (IsButtonHasEatStep(ik, jk, isOneStep, new int[2] { dirX, dirY }))
                {
                    closeSimple = true;
                }
                else
                {
                    toClose.Add(Board.GameButtons[ik, jk]);
                }
                Board.GameButtons[ik, jk].BackColor = Color.Yellow;
                Board.GameButtons[ik, jk].Enabled = true;
                countEatSteps++;
            }
            else
            {
                break;
            }

            if (isOneStep)
            {
                break;
            }

            jk += dirY;
            ik += dirX;
        }
        if (closeSimple && toClose.Count > 0)
        {
            CloseSimpleSteps(toClose);
        }
    }

    /// <summary>
    /// Vérifie si le déplacement a entrainer la capture d'un pion
    /// </summary>
    /// <param name="icurrFigure">Position I du pion sur le plateau</param>
    /// <param name="jcurrFigure">Position J du pion sur le plateau</param>
    /// <param name="isOneStep">Si le pion ne peut se déplacer que d'un mouvement (s'il est une dame ou pas)</param>
    /// <param name="dir"></param>
    /// <returns>Si oui ou non le déplacement à manger un pion</returns>
    public bool IsButtonHasEatStep(int icurrFigure, int jcurrFigure, bool isOneStep, int[] dir)
    {
        bool eatStep = false;
        int j = jcurrFigure + 1;
        for (int i = icurrFigure - 1; i >= 0; i--)
        {
            if (currentPlayer == 1 && isOneStep && !isContinue)
            {
                break;
            }

            if (dir[0] == 1 && dir[1] == -1 && !isOneStep)
            {
                break;
            }

            if (Board.IsInsideBorders(i, j))
            {
                if (Board.GameMap[i, j] != 0 && Board.GameMap[i, j] != currentPlayer)
                {
                    eatStep = true;
                    if (!Board.IsInsideBorders(i - 1, j + 1))
                        eatStep = false;
                    else if (Board.GameMap[i - 1, j + 1] != 0)
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

        j = jcurrFigure - 1;
        for (int i = icurrFigure - 1; i >= 0; i--)
        {
            if (currentPlayer == 1 && isOneStep && !isContinue) break;
            if (dir[0] == 1 && dir[1] == 1 && !isOneStep) break;
            if (Board.IsInsideBorders(i, j))
            {
                if (Board.GameMap[i, j] != 0 && Board.GameMap[i, j] != currentPlayer)
                {
                    eatStep = true;
                    if (!Board.IsInsideBorders(i - 1, j - 1))
                        eatStep = false;
                    else if (Board.GameMap[i - 1, j - 1] != 0)
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

        j = jcurrFigure - 1;
        for (int i = icurrFigure + 1; i < 8; i++)
        {
            if (currentPlayer == 2 && isOneStep && !isContinue) break;
            if (dir[0] == -1 && dir[1] == 1 && !isOneStep) break;
            if (Board.IsInsideBorders(i, j))
            {
                if (Board.GameMap[i, j] != 0 && Board.GameMap[i, j] != currentPlayer)
                {
                    eatStep = true;
                    if (!Board.IsInsideBorders(i + 1, j - 1))
                        eatStep = false;
                    else if (Board.GameMap[i + 1, j - 1] != 0)
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

        j = jcurrFigure + 1;
        for (int i = icurrFigure + 1; i < 8; i++)
        {
            if (currentPlayer == 2 && isOneStep && !isContinue) break;
            if (dir[0] == -1 && dir[1] == -1 && !isOneStep) break;
            if (Board.IsInsideBorders(i, j))
            {
                if (Board.GameMap[i, j] != 0 && Board.GameMap[i, j] != currentPlayer)
                {
                    eatStep = true;
                    if (!Board.IsInsideBorders(i + 1, j + 1))
                        eatStep = false;
                    else if (Board.GameMap[i + 1, j + 1] != 0)
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

    /// <summary>
    /// Change le joueur qui doit jouer
    /// </summary>
    public void SwitchPlayer(Label lbl, Label lblVictory)
    {
        currentPlayer = currentPlayer == 1 ? 2 : 1;
        lbl.Text = currentPlayer == 1 ? "Au blanc de jouer" : "Au noir de jouer";
        ResetGame(lblVictory);
    }

    /// <summary>
    /// Vérifie si la partie est finie, et désigne un joueur
    /// </summary>
    public void ResetGame(Label lblVictory)
    {
        bool player1 = false;
        bool player2 = false;

        for (int i = 0; i < Board.MapSize; i++)
        {
            for (int j = 0; j < Board.MapSize; j++)
            {
                if (Board.GameMap[i, j] == 1)
                    player1 = true;
                if (Board.GameMap[i, j] == 2)
                    player2 = true;
            }
        }
        if (!player1 || !player2)
        {
            lblVictory.Text = player1 ? "Victoire du joueur blanc !!!" : "Victoire du joueur noir !!!";
            lblVictory.BackColor = Color.Transparent;
            lblVictory.Show();
        }
    }

    /// <summary>
    /// Remets les couleurs des cases de base sur les cases qui affichaient un chemin
    /// </summary>
    /// <param name="simpleSteps">Liste des boutons où il faut changer la couleur</param>
    public void CloseSimpleSteps(List<Button> simpleSteps)
    {
        if (simpleSteps.Count > 0)
        {
            for (int i = 0; i < simpleSteps.Count; i++)
            {
                simpleSteps[i].BackColor = GetPrevButtonColor(simpleSteps[i]);
                simpleSteps[i].Enabled = false;
            }
        }
    }

    /// <summary>
    /// Affiche les chemins possibles
    /// </summary>
    public void ShowPossibleSteps()
    {
        bool isOneStep = true;
        bool isEatStep = false;
        DeactivateAllButtons();
        for (int i = 0; i < Board.MapSize; i++)
        {
            for (int j = 0; j < Board.MapSize; j++)
            {
                if (Board.GameMap[i, j] == currentPlayer)
                {
                    if (Board.GameButtons[i, j].Text == "D")
                        isOneStep = false;
                    else isOneStep = true;
                    if (IsButtonHasEatStep(i, j, isOneStep, new int[2] { 0, 0 }))
                    {
                        isEatStep = true;
                        Board.GameButtons[i, j].Enabled = true;
                    }
                }
            }
        }
        if (!isEatStep)
            ActivateAllNecessaryButtons();
    }

    /// <summary>
    /// Enlève les images des boutons qui ont été mangés lors d'un déplacement
    /// </summary>
    /// <param name="endButton">Bouton de fin</param>
    /// <param name="startButton">Bouton de début</param>
    public void DeleteEaten(Button endButton, Button startButton)
    {
        int count = Math.Abs(endButton.Location.Y / Board.CellSize - startButton.Location.Y / Board.CellSize);
        int startIndexX = endButton.Location.Y / Board.CellSize - startButton.Location.Y / Board.CellSize;
        int startIndexY = endButton.Location.X / Board.CellSize - startButton.Location.X / Board.CellSize;
        startIndexX = startIndexX < 0 ? -1 : 1;
        startIndexY = startIndexY < 0 ? -1 : 1;
        int currCount = 0;
        int i = startButton.Location.Y / Board.CellSize + startIndexX;
        int j = startButton.Location.X / Board.CellSize + startIndexY;
        while (currCount < count - 1)
        {
            Board.GameMap[i, j] = 0;
            Board.GameButtons[i, j].Image = null;
            Board.GameButtons[i, j].Text = "";
            i += startIndexX;
            j += startIndexY;
            currCount++;
        }

    }

    /// <summary>
    /// Passe le pion en dame s'il atteint le bord adverse
    /// </summary>
    /// <param name="button">Boutons que l'on vérifie</param>
    public void SwitchButtonToCheat(Button button)
    {
        if ((Board.GameMap[button.Location.Y / Board.CellSize, button.Location.X / Board.CellSize] == 1 && button.Location.Y / Board.CellSize == Board.MapSize - 1)
            || (Board.GameMap[button.Location.Y / Board.CellSize, button.Location.X / Board.CellSize] == 2 && button.Location.Y / Board.CellSize == 0))
        {
            button.Text = "D";
        }
    }

    /// <summary>
    /// Vérifie si le bouton doit être activé ou pas (s'il est blanc ou gris)
    /// </summary>
    /// <param name="prevButton">Bouton à vérifier</param>
    /// <returns>Si oui ou non, il doit être activé</returns>
    public bool ButtonShouldBeActive(Button prevButton)
    {
        if (((prevButton.Location.Y / Board.CellSize % 2) != 0 && (prevButton.Location.X / Board.CellSize % 2) == 0) || ((prevButton.Location.Y / Board.CellSize) % 2 == 0 && (prevButton.Location.X / Board.CellSize) % 2 != 0))
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Active tous les boutons nécessaires du plateau
    /// </summary>
    public void ActivateAllNecessaryButtons()
    {
        for (int i = 0; i < Board.MapSize; i++)
        {
            for (int j = 0; j < Board.MapSize; j++)
            {

                //if (ButtonShouldBeActive(Board.GameButtons[i, j]))
                //{
                //    Board.GameButtons[i, j].Enabled = true;
                //}

                if (Board.GameButtons[i, j] != null && ButtonShouldBeActive(Board.GameButtons[i, j]))
                {
                    Board.GameButtons[i, j].Enabled = true;
                }
            }
        }
    }

    /// <summary>
    /// Désactive tous les boutons du plateau
    /// </summary>
    public void DeactivateAllButtons()
    {
        for (int i = 0; i < Board.MapSize; i++)
        {
            for (int j = 0; j < Board.MapSize; j++)
            {
                if (Board.GameButtons[i, j] != null) // Vérifiez que le bouton est initialisé
                {
                    Board.GameButtons[i, j].Enabled = false;
                }

            }
        }
    }
}