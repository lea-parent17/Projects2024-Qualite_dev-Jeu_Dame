using CheckerGame.Controllers;
using CheckerGame.Models;
using System.Drawing;
using System.Windows.Forms;
using CheckerGame;



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
        [Description("Test ")]
        public void ShowSteps_ShouldDisplayValidMoves()
        {
            var form = new Form1();

            _board.GameMap[2, 2] = 1;
            _board.GameButtons[2, 2] = new Button { Text = "" };
            form.CreateGameUI(_board);

            _controller.ShowSteps(2, 2);

            foreach (var btn in _controller.SimpleSteps)
            {
                Assert.AreEqual(Color.Yellow, btn.BackColor);
            }
        }


        [TestMethod]
        [Description("Test ")]
        public void ShowDiagonal_ShouldHighlightDiagonalMoves()
        {
            var form = new Form1();

            _board.GameMap[3, 3] = 1;
            _board.GameButtons[4, 4] = new Button();
            _board.GameButtons[5, 5] = new Button();
            form.CreateGameUI(_board);

            _controller.ShowDiagonal(3, 3, true);

            Assert.IsTrue(_board.GameButtons[4, 4].Enabled);
            Assert.IsTrue(_board.GameButtons[5, 5].Enabled);
        }


        [TestMethod]
        [Description("Test ")]
        public void DeterminePath_ShouldHighlightValidMoves()
        {
            var form = new Form1(); //
            _board.GameMap[3, 3] = 1;
            form.CreateGameUI(_board); //
            _board.GameButtons[4, 4] = new Button();

            var result = _controller.DeterminePath(4, 4);

            Assert.IsTrue(result);
            Assert.AreEqual(Color.Yellow, _board.GameButtons[4, 4].BackColor);
        }


        [TestMethod]
        [Description("Test ")]
        public void DeterminePath_ShouldReturnFalse_IfPathBlocked()
        {
            var form = new Form1();

            _board.GameMap[4, 4] = 2;
            form.CreateGameUI(_board);
            _controller.PressedButton = new Button();

            var result = _controller.DeterminePath(4, 4);

            Assert.IsFalse(result);
        }



        [TestMethod]
        [Description("Test ")]
        public void ShowProceduralEat_ShouldHighlightCapturePath()
        {
            var form = new Form1();
            _board.GameMap = new int[8, 8];

            _board.GameMap[4, 3] = 2;
            _board.GameButtons[4, 3] = new Button();
            _board.GameButtons[5, 4] = new Button();
            _controller.PressedButton = _board.GameButtons[4, 3];
            form.CreateGameUI(_board);

            _controller.ShowProceduralEat(4, 3);

            Assert.AreEqual(Color.Yellow, _board.GameButtons[5, 4].BackColor);
        }

        ///////////////////////////////////////////////////

        //A ajouter ----> IsButtonHasEatStep


        ///////////////////////////////////////////////////


        [TestMethod]
        [Description("Test que le changement de joueur s'effectue bien entre chaque tour et que l'affichage se met également à jour")]
        public void SwitchPlayer_ShouldUpdateCurrentPlayer()
        {
            var lbl = new Label();
            var lblVictory = new Label();

            _controller.SwitchPlayer(lbl, lblVictory);

            Assert.AreEqual(2, _controller.CurrentPlayer);
            Assert.AreEqual("Au noir de jouer", lbl.Text);
        }

        [TestMethod]
        [Description("Test que la victoire est bien prise en compte (le joueur 1 gagne lorsque le joueur 2 n'a plus de pièces)")]
        public void ResetGame_ShouldDeclareWinner_IfNoPiecesLeft2()
        {
            _board.GameMap = new int[8, 8]; // Initialise un plateau vide.
            _board.GameMap[0, 0] = 1; // Place une pièce pour le joueur 1 (blanc).
            var lblVictory = new Label();

            _controller.ResetGame(lblVictory);

            Assert.AreEqual("Victoire du joueur blanc !!!", lblVictory.Text);
            Assert.AreEqual(Color.Transparent, lblVictory.BackColor);
        }

        /////////////////////////////////////////////////////////

        // A ajouter ---->  CloseSimpleSteps




        // A ajouter ----> ShowPossibleSteps


        /////////////////////////////////////////////////////////

        [TestMethod]
        [Description("Test ")]
        public void DeleteEaten_ShouldRemoveEatenPiece2()
        {
            var form = new Form1();
            _board.GameMap = new int[8, 8];
            form.CreateGameUI(_board);

            Button startButton = _controller.Board.GameButtons[2, 3]; // Bouton de départ
            Button endButton = _controller.Board.GameButtons[4, 5];   // Bouton d'arrivée

            _controller.Board.GameMap[3, 4] = 1; // Pion adverse (eaten piece)

            _controller.DeleteEaten(endButton, startButton);

            Assert.AreEqual(0, _controller.Board.GameMap[3, 4]);  // La position doit maintenant être vide
            Assert.IsNull(_controller.Board.GameButtons[3, 4].Image);  // L'image doit être null
            Assert.AreEqual("", _controller.Board.GameButtons[3, 4].Text);  // Le texte doit être vide
        }



        [TestMethod]
        [Description("Test ")]
        public void SwitchButtonToCheat_ShouldChangeStateToCheat()
        {
            _controller.Board.GameMap = new int[8, 8];
            _controller.Board.GameButtons = new Button[8, 8];

            var button = new Button
            {
                Location = new Point(3 * Board.CellSize, 7 * Board.CellSize), // Colonne 3, ligne 7
                Text = string.Empty
            };
            _controller.Board.GameButtons[7, 3] = button;

            // Indique que le bouton correspond à un pion du joueur 1 (1 dans GameMap)
            _controller.Board.GameMap[7, 3] = 1;

            _controller.SwitchButtonToCheat(button);

            Assert.AreEqual("D", button.Text);
            Assert.IsTrue(button.Enabled);
        }


        [TestMethod]
        [Description("Test ")]
        public void ButtonShouldBeActive_ShouldReturnTrueForValidPositions()
        {
            var validPositions = new[]
            {
            new Point(0, 50),  // Gris
            new Point(50, 0),  // Gris
            new Point(100, 150), // Gris
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
        [Description("Test ")]
        public void ButtonShouldBeActive_ShouldReturnFalseForInvalidPositions()
        {
            var invalidPositions = new[]
            {
            new Point(0, 0),    // Blanc
            new Point(50, 50),  // Blanc
            new Point(100, 100), // Blanc
            new Point(150, 150) // Blanc
            };

            foreach (var position in invalidPositions)
            {
                var button = new Button { Location = position };

                var result = _controller.ButtonShouldBeActive(button);

                Assert.IsFalse(result, $"Le bouton a la position {position} ne devrait pas être activé mais l'est");
            }
        }


        [TestMethod]
        [Description("Test ")]
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
        [Description("Test ")]
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

