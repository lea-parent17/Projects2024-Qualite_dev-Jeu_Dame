using CheckerGame.Controllers;
using CheckerGame.Models;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace CheckerGameTestUnitaire
{
    [TestClass]
    public class GameContollerTest
    {
        private Board _board;
        private GameController _gameController;

        [TestInitialize]
        public void Setup()
        {
            // Initialisation du plateau et du contrôleur
            _board = new Board();
            _board.InitializeBoard();
            _gameController = new GameController(_board);
        }

        [TestMethod]
        [Description("Test si l'on récupère correctement la couleur de fond initiale d'un bouton")]
        public void GetPrevButtonColor_Should_ReturnCorrectColor()
        {          
            _board.GameButtons[0, 0] = new Button { Location = new Point(0, 0) };
            _board.GameButtons[0, 1] = new Button { Location = new Point(Board.CellSize, 0) };

            var color1 = _gameController.GetPrevButtonColor(_board.GameButtons[0, 0]);
            var color2 = _gameController.GetPrevButtonColor(_board.GameButtons[0, 1]);

            
            Assert.AreEqual(Color.White, color1);
            Assert.AreEqual(Color.Gray, color2);
        }


        [TestMethod]
        [Description("Test si la méthode CloseSteps réinitialise correctement les couleurs des cases jaunes")]
        public void CloseSteps_Should_ResetButtonColors()
        {
            _board.GameButtons[0, 0] = new Button { Location = new Point(0, 0), BackColor = Color.Blue };
            _board.GameButtons[0, 1] = new Button { Location = new Point(Board.CellSize, 0), BackColor = Color.Red };

            _gameController.CloseSteps();

            // Assert
            Assert.AreEqual(Color.White, _board.GameButtons[0, 0].BackColor);
            Assert.AreEqual(Color.Gray, _board.GameButtons[0, 1].BackColor);
        }


        /// //////////////////////////////////////////////

        [TestMethod]
        [Description("Test si ShowSteps affiche correctement les mouvements simples sans obligation de manger")]
        public void ShowSteps_Should_DisplaySimpleSteps()
        {
            // Arrange
            int iCurrFigure = 2, jCurrFigure = 2;
            _gameController.countEatSteps = 0; // Aucun mouvement obligatoire
            _gameController.simpleSteps = new List<Button>();

            // Act
            _gameController.ShowSteps(iCurrFigure, jCurrFigure, isOnestep: true);

            // Assert
            Assert.IsTrue(_gameController.simpleSteps.Count > 0, "Les étapes simples devraient être ajoutées.");
        }

        [TestMethod]
        [Description("Test si ShowSteps affiche correctement les mouvements simples sans obligation de manger")]
        public void ShowSteps_Should_DisplaySimpleSteps2()
        {
            // Arrange
            int iCurrFigure = 2, jCurrFigure = 1;

            // Initialiser le plateau et le contrôleur
            _board = new Board();
            _board.GameMap = new int[8, 8]; // Plateau standard

            // Initialisation du contrôleur avec le plateau
            _gameController = new GameController(_board);

            // Positionner une pièce pour tester les déplacements
            _board.GameMap[iCurrFigure, jCurrFigure] = 1; // Placer une pièce (vérifiez si 1 est la bonne valeur)

            // Aucun mouvement obligatoire
            _gameController.countEatSteps = 0;

            // Act
            _gameController.ShowSteps(iCurrFigure, jCurrFigure, isOnestep: true);

            // Assert
            Assert.IsTrue(_gameController.simpleSteps.Count > 0, "Les étapes simples devraient être ajoutées.");
        }

        [TestMethod]
        [Description("Test si ShowSteps affiche uniquement les mouvements obligatoires lorsque countEatSteps > 0")]
        public void ShowSteps_Should_DisplayOnlyMandatorySteps()
        {
            // Arrange
            int iCurrFigure = 2, jCurrFigure = 2;
            _gameController.countEatSteps = 2; // Des mouvements obligatoires existent
            _gameController.simpleSteps = new List<Button>();

            // Act
            _gameController.ShowSteps(iCurrFigure, jCurrFigure, isOnestep: true);

            // Assert
            Assert.AreEqual(0, _gameController.simpleSteps.Count, "Les étapes simples devraient être supprimées.");
        }

        [TestMethod]
        [Description("Test si ShowSteps gère correctement les dames avec plusieurs étapes possibles")]
        public void ShowSteps_Should_HandleQueenMoves()
        {
            // Arrange
            int iCurrFigure = 3, jCurrFigure = 3;
            _gameController.countEatSteps = 0; // Aucun mouvement obligatoire
            _gameController.simpleSteps = new List<Button>();

            // Act
            _gameController.ShowSteps(iCurrFigure, jCurrFigure, isOnestep: false);

            // Assert
            Assert.IsTrue(_gameController.simpleSteps.Count > 0, "Les étapes pour une dame devraient être ajoutées.");
        }

        [TestMethod]
        [Description("Test si ShowSteps ne plante pas lorsqu'il n'y a aucun mouvement possible")]
        public void ShowSteps_Should_HandleNoMoves()
        {
            // Arrange
            int iCurrFigure = 0, jCurrFigure = 0;
            _gameController.countEatSteps = 0;
            _gameController.simpleSteps = new List<Button>();

            // Act
            _gameController.ShowSteps(iCurrFigure, jCurrFigure, isOnestep: true);

            // Assert
            Assert.AreEqual(0, _gameController.simpleSteps.Count, "Il ne devrait pas y avoir d'étapes possibles.");
        }


    















    //////////////////////////////////////////////////////////////////////////////////

    [TestMethod]
        [Description("Test si DeterminePath active correctement les cases vides pour un déplacement")]
        public void DeterminePath_Should_ActivateEmptyCells()
        {
            // Arrange
            int x = 3, y = 3;
            _board.GameMap[x, y] = 0; // La case est vide

            // Act
            var result = _gameController.DeterminePath(x, y);

            // Assert
            Assert.IsTrue(result); // La case est valide pour un déplacement
            Assert.AreEqual(Color.Yellow, _board.GameButtons[x, y].BackColor);
            Assert.IsTrue(_board.GameButtons[x, y].Enabled);
        }

        [TestMethod]
        [Description("Test si DeterminePath n'active pas les cases occupées")]
        public void DeterminePath_Should_NotActivateOccupiedCells()
        {
            // Arrange
            int x = 4, y = 4;
            _board.GameMap[x, y] = 1; // Case occupée

            // Act
            var result = _gameController.DeterminePath(x, y);

            // Assert
            Assert.IsFalse(result);
            Assert.AreNotEqual(Color.Yellow, _board.GameButtons[x, y].BackColor);
            Assert.IsFalse(_board.GameButtons[x, y].Enabled);
        }

        [TestMethod]
        [Description("Test si SwitchPlayer change correctement de joueur")]
        public void SwitchPlayer_Should_ChangeCurrentPlayerAndUpdateLabel()
        {
            // Arrange
            var currentPlayerLabel = new Label();
            var victoryLabel = new Label();

            _gameController.currentPlayer = 1;

            // Act
            _gameController.SwitchPlayer(currentPlayerLabel, victoryLabel);

            // Assert
            Assert.AreEqual(2, _gameController.currentPlayer);
            Assert.AreEqual("Au noir de jouer", currentPlayerLabel.Text);
        }

        [TestMethod]
        [Description("Test si ResetGame réinitialise le plateau et affiche le message de victoire")]
        public void ResetGame_Should_DeclareWinnerAndResetGame()
        {
            // Arrange
            var victoryLabel = new Label();
            _board.GameMap[0, 0] = 1; // Exemple : victoire du joueur blanc

            // Act
            _gameController.ResetGame(victoryLabel);

            // Assert
            Assert.AreEqual("Victoire du joueur blanc !!!", victoryLabel.Text);
        }

        [TestMethod]
        [Description("Test si ShowSteps met en évidence les déplacements valides pour une pièce")]
        public void ShowSteps_Should_HighlightValidMoves()
        {
            // Arrange
            _board.GameMap[2, 2] = 1; // Un pion blanc
            _gameController.currentPlayer = 1;

            // Act
            _gameController.ShowSteps(2, 2, true);

            // Assert
            Assert.AreEqual(Color.Yellow, _board.GameButtons[1, 3].BackColor); // Case valide
            Assert.IsTrue(_board.GameButtons[1, 3].Enabled);
        }

        [TestMethod]
        [Description("Test si ShowSteps n'affiche pas de mouvements pour une pièce adverse")]
        public void ShowSteps_Should_NotHighlightMovesForOpponentPiece()
        {
            // Arrange
            _board.GameMap[2, 2] = 2; // Un pion noir
            _gameController.currentPlayer = 1;

            // Act
            _gameController.ShowSteps(2, 2, true);

            // Assert
            Assert.AreNotEqual(Color.Yellow, _board.GameButtons[1, 3].BackColor); // Aucune case ne doit être mise en évidence
        }
    }
}


