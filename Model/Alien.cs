using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Alien
    {
        
        public int posX;
        public int posY;
        public int width;
        public int height;
        public int speed;
        public string ImageLink;

        public Alien(int posX, int posY, string ImageLink, int width, int height, int speed)
        {
            this.posX = posX;
            this.posY = posY;
            this.ImageLink = ImageLink;
            this.width = width;
            this.height = height;
            this.speed = speed;
        }
        public void move(int maxWidth)
        {
            if (this.posX > this.width+ maxWidth)
            {
                this.posX = (- this.width - 10)+(this.posX-(this.width + maxWidth));
                this.posY += this.height + 10;
            }
            else
            this.posX += this.speed;
        }
    }
}
