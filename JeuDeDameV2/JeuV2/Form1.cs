using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JeuV2
{
    public partial class Form1 : Form
    {
        const int MapSize = 8;
        const int CellSize = 50;

        int CurrentPlayer;

        List<Button> AvailableMoves = new List<Button>();

        int EatMoveCount = 0;
        Button PreviousButton;
        Button ActiveButton;
        bool IsContinue = false;

        bool IsMoving;

        int[,] GameMap = new int[MapSize, MapSize];

        Button[,] GameButtons = new Button[MapSize, MapSize];

        Image WhiteFigureImage;
        Image BlackFigureImage;

        public Form1()
        {
            InitializeComponent();

            WhiteFigureImage = new Bitmap(new Bitmap(@"C:\Users\natha\OneDrive\Bureau\ALL\Cours_BUT3\Qualite_dev\ProjetJeuDeDame\JeuDeDameV2\JeuV2\Sprites\w.png"), new Size(CellSize - 10, CellSize - 10));
            BlackFigureImage = new Bitmap(new Bitmap(@"C:\Users\natha\OneDrive\Bureau\ALL\Cours_BUT3\Qualite_dev\ProjetJeuDeDame\JeuDeDameV2\JeuV2\Sprites\b.png"), new Size(CellSize - 10, CellSize - 10));
            this.Text = "Checkers";

            Init();
        }

        public void Init()
        {
            CurrentPlayer = 1;
            IsMoving = false;
            PreviousButton = null;

            GameMap = new int[MapSize,MapSize] {
                { 0,1,0,1,0,1,0,1 },
                { 1,0,1,0,1,0,1,0 },
                { 0,1,0,1,0,1,0,1 },
                { 0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0 },
                { 2,0,2,0,2,0,2,0 },
                { 0,2,0,2,0,2,0,2 },
                { 2,0,2,0,2,0,2,0 }
            };

            CreateMap();
        }

        public void ResetGame()
        {
            bool player1 = false;
            bool player2 = false;

            for(int i = 0; i < MapSize; i++)
            {
                for (int j = 0; j < MapSize; j++)
                {
                    if (GameMap[i, j] == 1)
                        player1 = true;
                    if (GameMap[i, j] == 2)
                        player2 = true;
                }
            }
            if (!player1 || !player2)
            {
                this.Controls.Clear();
                Init();
            }
        }

        public void CreateMap()
        {
            this.Width = (MapSize + 1) * CellSize;
            this.Height = (MapSize + 1) * CellSize;

            for(int i = 0; i < MapSize; i++)
            {
                for (int j = 0; j < MapSize; j++)
                {
                    Button button = new Button();
                    button.Location = new Point(j * CellSize, i * CellSize);
                    button.Size = new Size(CellSize, CellSize);
                    button.Click += new EventHandler(OnFigurePress);
                    if (GameMap[i, j] == 1)
                        button.Image = WhiteFigureImage;
                    else if (GameMap[i, j] == 2) button.Image = BlackFigureImage;

                    button.BackColor = GetPrevButtonColor(button);
                    button.ForeColor = Color.Red;

                    GameButtons[i, j] = button;

                    this.Controls.Add(button);
                }
            }
        }

        public void SwitchPlayer()
        {
            CurrentPlayer = CurrentPlayer == 1 ? 2 : 1;
            ResetGame();
        }

        public Color GetPrevButtonColor(Button PreviousButton)
        {
            if ((PreviousButton.Location.Y/CellSize % 2) != 0)
            {
                if ((PreviousButton.Location.X / CellSize % 2) == 0)
                {
                    return Color.Gray;
                }
            }
            if ((PreviousButton.Location.Y / CellSize) % 2 == 0)
            {
                if ((PreviousButton.Location.X / CellSize) % 2 != 0)
                {
                    return Color.Gray;
                }
            }
            return Color.White;
        }

        public void OnFigurePress(object sender, EventArgs e)
        {
            if (PreviousButton != null)
                PreviousButton.BackColor = GetPrevButtonColor(PreviousButton);

            ActiveButton = sender as Button;

            if(GameMap[ActiveButton.Location.Y/CellSize, ActiveButton.Location.X/CellSize] != 0 && GameMap[ActiveButton.Location.Y / CellSize, ActiveButton.Location.X / CellSize] == CurrentPlayer)
            {
                CloseSteps();
                ActiveButton.BackColor = Color.Red;
                DeactivateAllButtons();
                ActiveButton.Enabled = true;
                EatMoveCount = 0;
                if(ActiveButton.Text == "D")
                ShowSteps(ActiveButton.Location.Y / CellSize, ActiveButton.Location.X / CellSize,false);
                else ShowSteps(ActiveButton.Location.Y / CellSize, ActiveButton.Location.X / CellSize);

                if (IsMoving)
                {
                    CloseSteps();
                    ActiveButton.BackColor = GetPrevButtonColor(ActiveButton);
                    ShowPossibleSteps();
                    IsMoving = false;
                }
                else
                    IsMoving = true;
            }
            else
            {
                if (IsMoving)
                {
                    IsContinue = false;
                      if (Math.Abs(ActiveButton.Location.X / CellSize - PreviousButton.Location.X/CellSize) > 1)
                    {
                        IsContinue = true;
                        DeleteEaten(ActiveButton, PreviousButton);                        
                    }
                    int temp = GameMap[ActiveButton.Location.Y / CellSize, ActiveButton.Location.X / CellSize];
                    GameMap[ActiveButton.Location.Y /CellSize, ActiveButton.Location.X / CellSize] = GameMap[PreviousButton.Location.Y / CellSize, PreviousButton.Location.X / CellSize];
                    GameMap[PreviousButton.Location.Y / CellSize, PreviousButton.Location.X / CellSize] = temp;
                    ActiveButton.Image = PreviousButton.Image;
                    PreviousButton.Image = null;
                    ActiveButton.Text = PreviousButton.Text;
                    PreviousButton.Text = "";
                    SwitchButtonToCheat(ActiveButton);
                    EatMoveCount = 0;
                    IsMoving = false;                    
                    CloseSteps();
                    DeactivateAllButtons();
                    if (ActiveButton.Text == "D")
                        ShowSteps(ActiveButton.Location.Y / CellSize, ActiveButton.Location.X / CellSize, false);
                    else ShowSteps(ActiveButton.Location.Y / CellSize, ActiveButton.Location.X / CellSize);
                    if (EatMoveCount == 0 || !IsContinue)
                    {
                        CloseSteps();
                        SwitchPlayer();
                        ShowPossibleSteps();
                        IsContinue = false;
                    }else if(IsContinue)
                    {
                        ActiveButton.BackColor = Color.Red;
                        ActiveButton.Enabled = true;
                        IsMoving = true;
                    }
                }
            }

            PreviousButton = ActiveButton;
        }

        public void ShowPossibleSteps()
        {
            bool isOneStep = true;
            bool isEatStep = false;
            DeactivateAllButtons();
            for(int i = 0; i < MapSize; i++)
            {
                for (int j= 0; j < MapSize; j++)
                {
                    if (GameMap[i, j] == CurrentPlayer)
                    {
                        if (GameButtons[i, j].Text == "D")
                            isOneStep = false;
                        else isOneStep = true;
                        if (IsButtonHasEatStep(i, j, isOneStep, new int[2] { 0, 0 }))
                        {
                            isEatStep = true;
                            GameButtons[i, j].Enabled = true;
                        }
                    }
                }
            }
            if (!isEatStep)
                ActivateAllButtons();
        }

        public void SwitchButtonToCheat(Button button)
        {
            if (GameMap[button.Location.Y / CellSize, button.Location.X / CellSize] == 1 && button.Location.Y / CellSize == MapSize - 1) 
            {
                button.Text = "D";
                
            }
            if (GameMap[button.Location.Y / CellSize, button.Location.X / CellSize] == 2 && button.Location.Y / CellSize == 0)
            {
                button.Text = "D";
            }
        }

        public void DeleteEaten(Button endButton, Button startButton)
        {
            int count = Math.Abs(endButton.Location.Y / CellSize - startButton.Location.Y / CellSize);
            int startIndexX = endButton.Location.Y / CellSize - startButton.Location.Y / CellSize;
            int startIndexY = endButton.Location.X / CellSize - startButton.Location.X / CellSize;
            startIndexX = startIndexX < 0 ? -1 : 1;
            startIndexY = startIndexY < 0 ? -1 : 1;
            int currCount = 0;
            int i = startButton.Location.Y / CellSize + startIndexX;
            int j = startButton.Location.X / CellSize + startIndexY;
            while (currCount < count-1)
            {
                GameMap[i, j] = 0;
                GameButtons[i, j].Image = null;
                GameButtons[i, j].Text = "";
                i += startIndexX;
                j += startIndexY;
                currCount++;
            }

        }

        public void ShowSteps(int iCurrFigure, int jCurrFigure,bool isOnestep = true)
        {
            AvailableMoves.Clear();
            ShowDiagonal(iCurrFigure, jCurrFigure,isOnestep);
            if (EatMoveCount > 0)
                CloseAvailableMoves(AvailableMoves);
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
        
        public bool DeterminePath(int ti,int tj)
        {
            
            if (GameMap[ti, tj] == 0 && !IsContinue)
            {
                GameButtons[ti, tj].BackColor = Color.Yellow;
                GameButtons[ti, tj].Enabled = true;
                AvailableMoves.Add(GameButtons[ti, tj]);
            }else
            {
                
                if (GameMap[ti, tj] != CurrentPlayer)
                {
                    if (ActiveButton.Text == "D")
                        ShowProceduralEat(ti, tj, false);
                    else ShowProceduralEat(ti, tj);
                }

                return false;
            }
            return true;
        }

        public void CloseAvailableMoves(List<Button> AvailableMoves)
        {
            if (AvailableMoves.Count > 0)
            {
                for (int i = 0; i < AvailableMoves.Count; i++)
                {
                    AvailableMoves[i].BackColor = GetPrevButtonColor(AvailableMoves[i]);
                    AvailableMoves[i].Enabled = false;
                }
            }
        }
        public void ShowProceduralEat(int i,int j,bool isOneStep = true)
        {
            int dirX = i - ActiveButton.Location.Y / CellSize;
            int dirY = j - ActiveButton.Location.X / CellSize;
            dirX = dirX < 0 ? -1 : 1;
            dirY = dirY < 0 ? -1 : 1;
            int il = i;
            int jl = j;
            bool isEmpty = true;
            while (IsInsideBorders(il, jl))
            {
                if (GameMap[il, jl] != 0 && GameMap[il, jl] != CurrentPlayer)
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
                return;
            List<Button> toClose = new List<Button>();
            bool closeSimple = false;
            int ik = il + dirX;
            int jk = jl + dirY;
            while (IsInsideBorders(ik,jk))
            {
                if (GameMap[ik, jk] == 0 )
                {
                    if (IsButtonHasEatStep(ik, jk, isOneStep, new int[2] { dirX, dirY }))
                    {
                        closeSimple = true;
                    }
                    else
                    {
                        toClose.Add(GameButtons[ik, jk]);
                    }
                    GameButtons[ik, jk].BackColor = Color.Yellow;
                    GameButtons[ik, jk].Enabled = true;
                    EatMoveCount++;
                }
                else break;
                if (isOneStep)
                    break;
                jk += dirY;
                ik += dirX;
            }
            if (closeSimple && toClose.Count > 0)
            {
                CloseAvailableMoves(toClose);
            }
            
        }

        public bool IsButtonHasEatStep(int IcurrFigure, int JcurrFigure, bool isOneStep, int[] dir)
        {
            bool eatStep = false;
            int j = JcurrFigure + 1;
            for (int i = IcurrFigure - 1; i >= 0; i--)
            {
                if (CurrentPlayer == 1 && isOneStep && !IsContinue) break;
                if (dir[0] == 1 && dir[1] == -1 && !isOneStep)break;
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

        public void CloseSteps()
        {
            for (int i = 0; i < MapSize; i++)
            {
                for (int j = 0; j < MapSize; j++)
                {
                    GameButtons[i, j].BackColor = GetPrevButtonColor(GameButtons[i, j]);
                }
            }
        }

        public bool IsInsideBorders(int ti,int tj)
        {
            if(ti>=MapSize || tj >= MapSize || ti<0 || tj < 0)
            {
                return false;
            }
            return true;
        }

        public void ActivateAllButtons()
        {
            for (int i = 0; i < MapSize; i++)
            {
                for (int j = 0; j < MapSize; j++)
                {
                    GameButtons[i, j].Enabled = true;
                }
            }
        }

        public void DeactivateAllButtons()
        {
            for (int i = 0; i < MapSize; i++)
            {
                for (int j = 0; j < MapSize; j++)
                {
                    GameButtons[i, j].Enabled = false;
                }
            }
        }
    }
}
