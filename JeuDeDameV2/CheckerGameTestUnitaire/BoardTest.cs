using CheckerGame.Models;

namespace CheckerGameTestUnitaire
{
    [TestClass]
    public class BoardTest
    {
        [TestMethod]
        [Description("Test si l'initialisation du plateau de jeu est correcte")]
        public void InitializeBoard_Should_SetStartingPositionsCorrectly()
        {
            var board = new Board();

            board.InitializeBoard();

            // On teste différentes cases du plateau pour voir si on y retrouve bien les pièces nécessaires ou aucune pièce
            Assert.AreEqual(1, board.GameMap[0, 1]); // Test si on retrouve bien une pièce du joueur 1 à cette position
            Assert.AreEqual(2, board.GameMap[5, 0]); // Test si on retrouve bien une pièce du joueur 2 à cette position
            Assert.AreEqual(0, board.GameMap[3, 3]); // Test s'il n'y a bien aucune pièce à cette position
        }

        [TestMethod]
        [Description("Test si le programme arrive à détecter si on lui donne différentes positions dans le plateau de jeu")]
        public void IsInsideBorders_Should_ReturnTrue_ForValidCoordinates()
        {
            var board = new Board();

            bool result = board.IsInsideBorders(4, 4);

            Assert.IsTrue(result);
        }

        [DataTestMethod]
        [Description("Test si le programme arrive à détecter si on lui donne différentes positions en dehors du plateau de jeu")]

        [DataRow(-1, 0)]
        [DataRow(8, 8)]
        [DataRow(0, -1)]
        public void IsInsideBorders_Should_ReturnFalse_ForInvalidCoordinates(int x, int y)
        {
            var board = new Board();

            
            bool result = board.IsInsideBorders(x, y);

            Assert.IsFalse(result);
        }
    }
}