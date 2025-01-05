using CheckerGame.Controllers;
using CheckerGame.Models;
using System.Drawing;
using System.Windows.Forms;


namespace CheckerGameTestUnitaire
{
    [TestClass]
    public class GameContollerTest
    {
        private Board _board;
        private GameController _controller;

        [TestInitialize]
        public void Setup()
        {
            // Initialisation du plateau et du contrôleur
            _board = new Board();
            _board.InitializeBoard();
            _controller = new GameController(_board);
        }

        [TestMethod]
        [Description("Test si l'on récupère correctement la couleur de fond initiale d'un bouton")]
        public void GetPrevButtonColor_Should_ReturnCorrectColor()
        {
            _board.GameButtons[0, 0] = new Button { Location = new Point(0, 0) };
            _board.GameButtons[0, 1] = new Button { Location = new Point(Board.CellSize, 0) };

            var color1 = _controller.GetPrevButtonColor(_board.GameButtons[0, 0]);
            var color2 = _controller.GetPrevButtonColor(_board.GameButtons[0, 1]);


            Assert.AreEqual(Color.White, color1);
            Assert.AreEqual(Color.Gray, color2);
        }


        [TestMethod]
        [Description("Test si la méthode CloseSteps réinitialise correctement les couleurs des cases jaunes")]
        public void CloseSteps_Should_ResetButtonColors()
        {
            _board.GameButtons[0, 0] = new Button { Location = new Point(0, 0), BackColor = Color.Blue };
            _board.GameButtons[0, 1] = new Button { Location = new Point(Board.CellSize, 0), BackColor = Color.Red };

            _controller.CloseSteps();

            Assert.AreEqual(Color.White, _board.GameButtons[0, 0].BackColor);
            Assert.AreEqual(Color.Gray, _board.GameButtons[0, 1].BackColor);
        }


        [TestMethod]
        public void ShowSteps_ShouldDisplayValidMoves()
        {
            // Arrange
            _board.GameMap[2, 2] = 1;
            _board.GameButtons[2, 2] = new Button { Text = "" };

            // Act
            _controller.ShowSteps(2, 2);

            // Assert
            foreach (var btn in _controller.simpleSteps)
            {
                Assert.AreEqual(Color.Yellow, btn.BackColor);
            }
        }

        [TestMethod]
        public void ShowDiagonal_ShouldHighlightDiagonalMoves()
        {
            // Arrange
            _board.GameMap[3, 3] = 1;
            _board.GameButtons[4, 4] = new Button();
            _board.GameButtons[5, 5] = new Button();

            // Act
            _controller.ShowDiagonal(3, 3, true);

            // Assert
            Assert.IsTrue(_board.GameButtons[4, 4].Enabled);
            Assert.IsTrue(_board.GameButtons[5, 5].Enabled);
        }

        [TestMethod]
        public void DeterminePath_ShouldHighlightValidMoves()
        {
            // Arrange
            _board.GameMap[3, 3] = 1;
            _board.GameButtons[4, 4] = new Button();

            // Act
            var result = _controller.DeterminePath(4, 4);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(Color.Yellow, _board.GameButtons[4, 4].BackColor);
        }

        [TestMethod]
        public void DeterminePath_ShouldReturnFalse_IfPathBlocked()
        {
            // Arrange
            _board.GameMap[4, 4] = 2;

            // Act
            var result = _controller.DeterminePath(4, 4);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ShowProceduralEat_ShouldHighlightCapturePath()
        {
            // Arrange
            _board.GameMap[3, 3] = 1;
            _board.GameMap[4, 4] = 2;
            _board.GameButtons[5, 5] = new Button();

            // Act
            _controller.ShowProceduralEat(4, 4);

            // Assert
            Assert.AreEqual(Color.Yellow, _board.GameButtons[5, 5].BackColor);
        }



        //A ajouter ----> IsButtonHasEatStep




        [TestMethod]
        public void SwitchPlayer_ShouldUpdateCurrentPlayer()
        {
            var lbl = new Label();
            var lblVictory = new Label();

            _controller.SwitchPlayer(lbl, lblVictory);

            Assert.AreEqual(2, _controller.currentPlayer);
            Assert.AreEqual("Au noir de jouer", lbl.Text);
        }

        [TestMethod]
        public void ResetGame_ShouldDeclareWinner_IfNoPiecesLeft()
        {
            // Arrange
            _board.GameMap[0, 0] = 1;
            _board.GameMap[7, 7] = 0;
            var lblVictory = new Label();

            // Act
            _controller.ResetGame(lblVictory);

            // Assert
            Assert.AreEqual("Victoire du joueur blanc !!!", lblVictory.Text);
        }


        // A ajouter ---->  CloseSimpleSteps


        // A ajouter ----> ShowPossibleSteps


        // A ajouter ----> DeleteEaten



        // A ajouter ----> SwitchButtonToCheat





        /////////////////////////////////A revoir///////////////////////
        [TestMethod]
        public void ButtonShouldBeActive_ShouldReturnTrueForValidPositions()
        {
            var validPositions = new[]
            {
            new Point(0, 50),  // Blanc
            new Point(50, 0),  // Gris
            new Point(100, 150), // Blanc
            new Point(150, 100)  // Gris
            };

            foreach (var position in validPositions)
            {
                var button = new Button { Location = position };

                var result = _controller.ButtonShouldBeActive(button);

                Assert.IsTrue(result, $"Le bouton a la position {position} doir être active mais ne l'est pas");
            }
        }

        [TestMethod]
        public void ButtonShouldBeActive_ShouldReturnFalseForInvalidPositions()
        {
            var invalidPositions = new[]
            {
            new Point(0, 0),    // Noir
            new Point(50, 50),  // Noir
            new Point(100, 100), // Noir
            new Point(150, 150) // Noir
            };

            foreach (var position in invalidPositions)
            {
                var button = new Button { Location = position };

                var result = _controller.ButtonShouldBeActive(button);

                Assert.IsFalse(result, $"Le bouton a la position {position} ne devrait pas être activé mais l'est");
            }
        }

        /////////////////////////////////A revoir///////////////////////

        [TestMethod]
        public void ActivateAllNecessaryButtons_ShouldEnableValidButtons()
        {

            for (int i = 0; i < _board.GameMap.GetLength(0); i++)
            {
                for (int j = 0; j < _board.GameMap.GetLength(1); j++)
                {
                    _board.GameButtons[i, j] = new Button
                    {
                        Location = new Point(j * Board.CellSize, i * Board.CellSize), // Position calculée
                        Enabled = false // Initialement désactivé
                    };
                }
            }

            _controller.ActivateAllNecessaryButtons();

            for (int i = 0; i < _board.GameMap.GetLength(0); i++)
            {
                for (int j = 0; j < _board.GameMap.GetLength(1); j++)
                {
                    bool shouldBeEnabled = _controller.ButtonShouldBeActive(_board.GameButtons[i, j]);
                    Assert.AreEqual(shouldBeEnabled, _board.GameButtons[i, j].Enabled, $"Le bouton ({i},{j}) n'as pas le bon état");
                }
            }

            // AUTRE TEST MOINS COMPLET 
            //_board.GameButtons[0, 0] = new Button { Location = new Point(0, 0) };
            //_board.GameButtons[1, 1] = new Button { Location = new Point(50, 50) };

            //_controller.ActivateAllNecessaryButtons();

            //Assert.IsTrue(_board.GameButtons[0, 0].Enabled);
            //Assert.IsTrue(_board.GameButtons[1, 1].Enabled);
        }

        [TestMethod]
        public void DeactivateAllButtons_ShouldDisableAllButtons()
        {            
            for (int i = 0; i < _board.GameMap.GetLength(0); i++)
            {
                for (int j = 0; j < _board.GameMap.GetLength(1); j++)
                {
                    _board.GameButtons[i, j] = new Button { Enabled = true }; // Initialisation des boutons
                }
            }

            _controller.DeactivateAllButtons();

            for (int i = 0; i < _board.GameMap.GetLength(0); i++)
            {
                for (int j = 0; j < _board.GameMap.GetLength(1); j++)
                {
                    Assert.IsFalse(_board.GameButtons[i, j].Enabled, $"Le boutton ({i},{j}) n'est pas desactive");
                }
            }


            // AUTRE TEST MOINS COMPLET 
            //_board.GameButtons[0, 0] = new Button { Enabled = true };

            //_controller.DeactivateAllButtons();

            //Assert.IsFalse(_board.GameButtons[0, 0].Enabled);

        }
    }
}

