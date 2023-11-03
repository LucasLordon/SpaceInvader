using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;

namespace Model.Tests
{
    [TestClass()]
    public class AlienTests
    {
        [TestMethod()]
        public void moveTest()
        {
            // Test le déplacement normal de l'alien
            Alien alien1 = new Alien(0, 0, "alien.png", 30, 40, 5);
            alien1.move(100); // Supposez que la largeur maximale soit de 100
            Assert.AreEqual(5, alien1.posX);

            // Test le déplacement lorsque l'alien atteint la limite
            Alien alien2 = new Alien(130, 0, "alien.png", 30, 40, 5);
            alien2.move(100); // Supposez que la largeur maximale soit de 100
            // L'alien doit être réinitialisé à une nouvelle ligne et se déplacer vers la gauche
            Assert.AreEqual(-40, alien2.posX); // Nouvelle position X après le rebond
            Assert.AreEqual(50, alien2.posY); // Nouvelle position Y après le rebond
        }
    }
}
