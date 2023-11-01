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
        public int posX;         // Position en X de la balle (coin supérieur gauche)
        public int posY;         // Position en Y de la balle (coin supérieur gauche)
        public int height;       // Hauteur de la balle (rectangle, vers la droite de posY)
        public int width;        // Largeur de la balle (rectangle, vers le bas de posX)
        public int speed;        // Vitesse de déplacement de la balle (distance parcouru à chaque déplacement)
        public string fillColor; // Couleur du centre de la balle (dois convenir à SolidColorBrush)
        public string strokeColor; // Couleur des bordures de la balle (dois convenir à SolidColorBrush)
        public bool needToDelete = false; // Un indicateur pour indiquer si la balle doit être supprimée

        /// <summary>
        /// Constructeur de la classe "Bullet"
        /// </summary>
        /// <param name="posX">Position en X de la balle (coin supérieur gauche)</param>
        /// <param name="posY">Position en Y de la balle (coin supérieur gauche)</param>
        /// <param name="height">Hauteur de la balle (rectangle, vers la droite de posY)</param>
        /// <param name="width">Largeur de la balle (rectangle, vers le bas de posX)</param>
        /// <param name="speed">Vitesse de déplacement de la balle (distance parcouru à chaque déplacement)</param>
        /// <param name="fillColor">Couleur du centre de la balle (dois convenir à SolidColorBrush)</param>
        /// <param name="strokeColor">Couleur des bordures de la balle (dois convenir à SolidColorBrush)</param>
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

        /// <summary>
        /// Méthode pour faire apparaître la balle en fonction de la position du joueur
        /// </summary>
        /// <param name="player">player est égal au joueur qui à tirer</param>
        public void Spawn(Player player)
        {
            //centre le bullet au milieut du player
            this.posX = player.posX + player.width / 2 - this.width / 2;
            this.posY = player.posY + this.height;
        }

        /// <summary>
        ///  Méthode pour déplacer la balle vers le haut
        /// </summary>
        /// <param name="TOPLIMIT"></param>
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
