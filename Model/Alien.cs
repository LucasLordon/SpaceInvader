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
        public int posX;          // Position en X de l'alien (coin supérieur gauche)
        public int posY;          // Position en Y de l'alien (coin supérieur gauche)
        public int width;         // Largeur de l'alien (rectangle, vers la droite de posY)
        public int height;        // Hauteur de l'alien (rectangle, vers le bas de posX)
        public int speed;         // Vitesse de déplacement de l'alien (distance parcouru à chaque déplacement)
        public string ImageLink;  // Lien vers l' image associée à l'alien (le lien pars du bin)

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
            // Vérifie si la position X actuelle de l'alien est plus grande que la somme de sa largeur et de la largeur maximale de l'écran (cela permet de verifier si l'alien déplace l'écran sur la droite)
            if (this.posX > this.width + maxWidth)
            {
                //La nouvelle position est égal à : (- la largeur de l'alien - l'espace entre les alien) + (le décalage que l'alien à sur la droite de l'écran)    Cela permet déviter d'empiler les alien lors de l'accélération de la vitesse des alien.
                this.posX = (-this.width - 10) + (this.posX - (this.width + maxWidth));

                //permet de décendre l'alien d'une ligne (le +10 est l'espace vide avec les aliens du dessu
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