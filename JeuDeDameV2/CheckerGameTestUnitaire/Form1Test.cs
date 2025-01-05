using CheckerGame.Models;
using CheckerGame;
using System.Drawing;

namespace CheckerGameTestUnitaire
{
    [TestClass]
    public class Form1Test
    {
        [TestMethod]
        [Description("Test que l'initialisation de la classe Form1 configure correctement le jeu")]
        public void Form1_Initialization_Should_SetupGameProperly()
        {
            var form = new Form1();

            var gameController = form.GetGameController();
            var board = gameController.Board;

            Assert.IsNotNull(gameController);
            Assert.IsNotNull(board);
            Assert.AreEqual(1, gameController.currentPlayer); //le joueur courant est 1
        }

        [TestMethod]
        [Description("Test que la méthode CreateGameUI crée correctement tous les boutons du plateau")]
        public void CreateGameUI_Should_CreateAllButtons()
        {
            var form = new Form1();
            var board = new Board();

            form.CreateGameUI(board);

            for (int y = 0; y < Board.MapSize; y++)
            {
                for (int x = 0; x < Board.MapSize; x++)
                {
                    var button = board.GameButtons[y, x];
                    Assert.IsNotNull(button);
                    Assert.AreEqual(Board.CellSize, button.Width);
                    Assert.AreEqual(Board.CellSize, button.Height);
                }
            }
        }

        [TestMethod]
        [Description("Test que la méthode UpdateGameUI met correctement à jour les images des boutons en fonction des pièces sur le plateau")]
        public void UpdateGameUI_Should_SetButtonImagesCorrectly()
        {
            var form = new Form1();
            var gameController = form.GetGameController();
            var board = gameController.Board;

            board.GameMap[0, 0] = 1; // Place un pion blanc
            board.GameMap[7, 7] = 2; // Place un pion noir

            form.UpdateGameUI(board);

            Assert.AreEqual(form.whiteFigure, board.GameButtons[0, 0].Image, "L'image du pion blanc doit être correctement définie.");
            Assert.AreEqual(form.blackFigure, board.GameButtons[7, 7].Image, "L'image du pion noir doit être correctement définie.");
        }

        [TestMethod]
        [Description("Test que la méthode OnCellClick active les mouvements valides pour une pièce sélectionnée ")]
        public void OnCellClick_Should_ActivateValidMoves_ForSelectedPiece()
        {
            var form = new Form1();
            var board = form.GetGameController().Board;

            board.GameMap[5, 2] = 1; // piece joeur 1
            var button = board.GameButtons[5, 2];

            form.OnCellClick(button, null);

            Assert.AreEqual(Color.Red, button.BackColor); // le bouton doit etre mis en avant
        }

        [TestMethod]
        [Description("Test que la méthode OnCellClick gère correctement un clic invalide (par exemple, sur une case sans pièce)")]
        public void OnCellClick_Should_HandleInvalidClick_Gracefully()
        {
            var form = new Form1();
            var gameController = form.GetGameController();
            var board = gameController.Board;

            // Simuler un clic invalide (par exemple, sur une case blanche sans pièce)
            var invalidButton = board.GameButtons[0, 0]; // Case blanche sans pièce
            Assert.AreEqual(Color.White, invalidButton.BackColor, "La case doit initialement être blanche.");

            form.OnCellClick(invalidButton, null);

            Assert.AreEqual(Color.White, invalidButton.BackColor, "La couleur ne doit pas changer après un clic invalide.");
        }


        [TestMethod]
        [Description("Test que la méthode IsMoving gère correctement un mouvement valide")]
        public void IsMoving_Should_SwitchPlayers_AfterValidMove()
        {

            var form = new Form1();
            var gameController = form.GetGameController();
            var board = gameController.Board;

            // initialise un scenario valide
            board.GameMap[5, 2] = 1; // piece du joueur 1
            var startButton = board.GameButtons[5, 2];
            var endButton = board.GameButtons[4, 3];

            gameController.pressedButton = endButton;
            gameController.PreviousButton = startButton;
            gameController.isMoving = true;

            // Act
            form.IsMoving();

            // Assert
            Assert.AreEqual(0, board.GameMap[5, 2]); // la cellule de depart doit etre vide
            Assert.AreEqual(1, board.GameMap[4, 3]); // la cellule d'arriver doit avoir la piece
            Assert.AreEqual(2, gameController.currentPlayer); // le tour passe au joueur 2
        }

        [TestMethod]
        [Description("Test que la méthode PlayerPossibleMove met correctement en évidence les mouvements possibles pour une pièce sélectionnée")]
        public void PlayerPossibleMove_Should_HighlightPossibleMoves()
        {
            var form = new Form1();
            var gameController = form.GetGameController();
            var board = gameController.Board;

            // Initialise une piece pour le joeur 1
            board.GameMap[5, 2] = 1;
            var button = board.GameButtons[5, 2];

            gameController.pressedButton = button;

            form.PlayerPossibleMove();


            Assert.AreEqual(Color.Red, button.BackColor); // Le bouton doit etre mis en avant
        }

        [TestMethod]
        [Description("Test que la méthode OnResetGameClick réinitialise correctement le plateau et le jeu ")]
        public void OnResetGameClick_Should_ResetBoard()
        {
            var form = new Form1();
            var gameController = form.GetGameController();
            var board = gameController.Board;

            // Pon fait quelque mouvement
            board.GameMap[5, 2] = 1;
            gameController.currentPlayer = 2;

            form.OnResetGameClick(null, null);

            Assert.AreEqual(1, gameController.currentPlayer); // le joueur qui doit jouer revient a 1
            Assert.AreEqual(1, board.GameMap[0, 1]); // initialisation de base 
            Assert.AreEqual(2, board.GameMap[5, 0]);
        }

    }
}
