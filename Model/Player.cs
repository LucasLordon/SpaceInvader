using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Model
{
    /// <summary>
    /// Classe Player
    /// </summary>
    public class Player
    {
        // Déclaration des attributs de la classe Player
        public int posX;           // Position en X du joueur (coin supérieur gauche)
        public int posY;           // Position en Y du joueur (coin supérieur gauche)
        public int width;          // Largeur du joueur (rectangle, vers la droite de posY)
        public int height;         // Hauteur du joueur (rectangle, vers le bas de posX)
        private int ammoStock;     // Stock de munitions du joueur
        public string ImageLink;   // Lien vers l'image associée au joueur (le lien pars du bin)

        /// <summary>
        /// Constructeur de la classe "Player"
        /// </summary>
        /// <param name="posX">Position en X du joueur (coin supérieur gauche)</param>
        /// <param name="posY">Position en Y du joueur (coin supérieur gauche)</param>
        /// <param name="ImageLink">Lien vers l'image associée au joueur (le lien pars du bin)</param>
        /// <param name="ammoStock">Stock de munitions du joueur</param>
        /// <param name="width">Largeur du joueur (rectangle, vers la droite de posY)</param>
        /// <param name="height">Hauteur du joueur (rectangle, vers le bas de posX)</param>
        public Player(int posX, int posY, string ImageLink, int ammoStock, int width, int height)
        {
            // Initialisation des attributs avec les valeurs passées en paramètres
            this.posX = posX;
            this.posY = posY;
            this.ImageLink = ImageLink;
            this.ammoStock = ammoStock;
            this.width = width;
            this.height = height;
        }

        /// <summary>
        /// Méthode pour déplacer le joueur vers la gauche
        /// </summary>
        /// <param name="leftLimit">limit gauche de l'espace de jeu du player</param>
        public void GoLeft(int leftLimit)
        {
            if (this.posX - 10 >= leftLimit)
            {
                this.posX -= 10;
            }
        }

        /// <summary>
        /// Méthode pour déplacer le joueur vers la droite
        /// </summary>
        /// <param name="maxWidth">Taille de l'écrant</param>
        public void GoRight(int maxWidth)
        {
            if (this.posX + 10 <= maxWidth - 20 - this.width)
            {
                this.posX += 10;
            }
        }

        /// <summary>
        /// Méthode pour permettre au joueur de tirer une balle
        /// </summary>
        /// <returns>Un nouvelle objet de type bullet</returns>
        public Bullet Shoot()
        {
            if (this.ammoStock > 0)
            {
                // Crée une nouvelle balle, lui donne des attributs, la fait apparaître
                // au niveau du joueur, réduit le stock de munitions et retourne la balle
                Bullet bullet = new Bullet(this.posX, this.posY, 20, 5, 20, "White", "Red");
                bullet.Spawn(this);
                this.ammoStock--;
                return bullet;
            }
            return null; // Retourne null si le joueur n'a plus de munitions
        }
    }
}
