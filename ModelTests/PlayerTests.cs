using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;

namespace Model.Tests
{
    [TestClass()]
    public class PlayerTests
    {
        [TestMethod()]
        public void GoRightTest()
        {
            // Arrange
            Player player = new Player(10, 0, "player.png", 10, 30, 40);
            int maxWidth = 100; // Supposons que la largeur maximale soit de 100

            // Act
            player.GoRight(maxWidth);

            // Assert
            // Vérifiez que la position X a été mise à jour correctement
            Assert.AreEqual(20, player.posX);
        }

        [TestMethod()]
        public void GoRightWithMaxWidthTest()
        {
            // Arrange
            Player player = new Player(70, 0, "player.png", 10, 30, 40);
            int maxWidth = 100; // Supposons que la largeur maximale soit de 100

            // Act
            player.GoRight(maxWidth);

            // Assert
            // Vérifiez que la position X n'a pas été mise à jour car elle atteint déjà la limite
            Assert.AreEqual(70, player.posX);
        }
    }
}
