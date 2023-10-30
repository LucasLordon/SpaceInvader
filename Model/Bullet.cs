using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Bullet
    {
        public int posX;
        public int posY;
        public int height;
        public int width;
        public int speed;
        public string fillColor;
        public string strokeColor;
        public bool needToDelete=false;

        /// <summary>
        /// Constructeur de la class "Bullet"
        /// </summary>
        /// <param name="posX">position x</param>
        /// <param name="posY">position y</param>
        /// <param name="height">hauteur du bullet (car le bullet et un rectangle)</param>
        /// <param name="width">largeur du bullet (car le bullet et un rectangle)</param>
        /// <param name="fillColor">couleur du centre du bullet</param>
        /// <param name="strokeColor">couleur des bordures du bullet</param>
        public Bullet(int posX, int posY,int height, int width,int speed, string fillColor, string strokeColor )
        {
            this.posX = posX;
            this.posY = posY;
            this.height = height;
            this.width = width;
            this.speed = speed;
            this.fillColor = fillColor;
            this.strokeColor = strokeColor;
            
        }
        public void Spawn(Player player)
        {
            this.posX = player.posX+player.width/2-this.width/2;
            this.posY = player.posY+this.height;
        }
        public void MoveUp(int TOPLIMIT)
        {
            this.posY -= this.speed;
            if(this.posY < TOPLIMIT)
            {
                this.needToDelete =true;
            }
        }
        public void MoveDown(int WINDOWSHEIGT)
        {
            this.posY += this.speed;
            if (this.posY > WINDOWSHEIGT)
            {
                this.needToDelete = true;
            }
        }
    }
}
