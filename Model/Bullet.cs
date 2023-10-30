using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Bullet
    {
        // Déclaration des attributs de la classe Bullet
        public int posX;         // Position en X de la balle
        public int posY;         // Position en Y de la balle
        public int height;       // Hauteur de la balle (rectangle)
        public int width;        // Largeur de la balle (rectangle)
        public int speed;        // Vitesse de la balle
        public string fillColor; // Couleur du centre de la balle
        public string strokeColor; // Couleur des bordures de la balle
        public bool needToDelete = false; // Un indicateur pour indiquer si la balle doit être supprimée

        /// <summary>
        /// Constructeur de la classe "Bullet"
        /// </summary>
        /// <param name="posX">position x</param>
        /// <param name="posY">position y</param>
        /// <param name="height">hauteur de la balle (rectangle)</param>
        /// <param name="width">largeur de la balle (rectangle)</param>
        /// <param name="speed">vitesse de la balle</param>
        /// <param name="fillColor">couleur du centre de la balle</param>
        /// <param name="strokeColor">couleur des bordures de la balle</param>
        public Bullet(int posX, int posY, int height, int width, int speed, string fillColor, string strokeColor)
        {
            // Initialisation des attributs avec les valeurs passées en paramètres
            this.posX = posX;
            this.posY = posY;
            this.height = height;
            this.width = width;
            this.speed = speed;
            this.fillColor = fillColor;
            this.strokeColor = strokeColor;
        }

        // Méthode pour faire apparaître la balle en fonction de la position du joueur
        public void Spawn(Player player)
        {
            this.posX = player.posX + player.width / 2 - this.width / 2;
            this.posY = player.posY + this.height;
        }

        // Méthode pour déplacer la balle vers le haut
        public void MoveUp(int TOPLIMIT)
        {
            this.posY -= this.speed;
            if (this.posY < TOPLIMIT)
            {
                this.needToDelete = true;
            }
        }

        // Méthode pour déplacer la balle vers le bas
        public void MoveDown(int WINDOWSHEIGHT)
        {
            this.posY += this.speed;
            if (this.posY > WINDOWSHEIGHT)
            {
                this.needToDelete = true;
            }
        }
    }
}
