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
        public int posX;
        public int posY;
        public int width;
        public int height;
        private int ammoStock;
        public string ImageLink;

        public Player(int posX, int posY, string ImageLink, int ammoStock, int width, int height)
        {
            this.posX = posX;
            this.posY = posY;
            this.ImageLink = ImageLink;
            this.ammoStock = ammoStock;
            this.width = width;
            this.height = height;
        }

        public void GoLeft(int leftLimit)
        {
            if (this.posX-10 >= leftLimit)
            {
                this.posX -= 10;
            }
        }

        public void GoRight(int maxWidth)
        {
            if (this.posX + 10 <= maxWidth-20-this.width)
            {
                this.posX += 10;
            }
        }

        public Bullet Shoot()
        {
            if (this.ammoStock > 0)
            {
                Bullet bullet = new Bullet(this.posX, this.posY, 20, 5,20, "White", "Red");
                bullet.Spawn(this);
                this.ammoStock--;
                return bullet;
            }
            return null;
        }


    }
}

