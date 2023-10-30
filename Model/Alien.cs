using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Alien
    {
        // Déclaration des attributs de la classe Alien
        public int posX;          // Position en X de l'alien
        public int posY;          // Position en Y de l'alien
        public int width;         // Largeur de l'alien
        public int height;        // Hauteur de l'alien
        public int speed;         // Vitesse de déplacement de l'alien
        public string ImageLink;  // Lien vers une image associée à l'alien

        // Constructeur de la classe Alien
        public Alien(int posX, int posY, string ImageLink, int width, int height, int speed)
        {
            // Initialisation des attributs avec les valeurs passées en paramètres
            this.posX = posX;
            this.posY = posY;
            this.ImageLink = ImageLink;
            this.width = width;
            this.height = height;
            this.speed = speed;
        }

        // Méthode pour déplacer l'alien
        public void move(int maxWidth)
        {
            // Vérifie si la position X actuelle de l'alien est plus grande que la somme de sa largeur et de la largeur maximale
            if (this.posX > this.width + maxWidth)
            {
                // Si c'est le cas, réinitialise la position X à une valeur négative
                // et déplace l'alien vers le bas
                this.posX = (-this.width - 10) + (this.posX - (this.width + maxWidth));
                this.posY += this.height + 10;
            }
            else
            {
                // Sinon, incrémente la position X de l'alien par sa vitesse
                this.posX += this.speed;
            }
        }
    }
}
