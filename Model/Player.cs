using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Model
{
    public class Player
    {
        // Déclaration des attributs de la classe Player
        public int posX;           // Position en X du joueur
        public int posY;           // Position en Y du joueur
        public int width;          // Largeur du joueur
        public int height;         // Hauteur du joueur
        private int ammoStock;     // Stock de munitions du joueur
        public string ImageLink;   // Lien vers l'image associée au joueur

        // Constructeur de la classe "Player"
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

        // Méthode pour déplacer le joueur vers la gauche
        public void GoLeft(int leftLimit)
        {
            if (this.posX - 10 >= leftLimit)
            {
                this.posX -= 10;
            }
        }

        // Méthode pour déplacer le joueur vers la droite
        public void GoRight(int maxWidth)
        {
            if (this.posX + 10 <= maxWidth - 20 - this.width)
            {
                this.posX += 10;
            }
        }

        // Méthode pour permettre au joueur de tirer une balle
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
