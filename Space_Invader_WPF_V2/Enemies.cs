using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invader_WPF_V2
{
    internal class Enemies
    {
        private void makeEnemies(int limit)
        {
            totalEnemies = limit;
            int left = 0;

            for (int i = 0; i < limit; i++)
            {
                ImageBrush EnemySkin = new ImageBrush();

                Rectangle newEnemy = new Rectangle()
                {
                    Tag = "enemy",
                    Height = 45,
                    Width = 45,
                    Fill = EnemySkin,
                };
                Canvas.SetTop(newEnemy, 10);
                Canvas.SetLeft(newEnemy, left);
                myCanvas.Children.Add(newEnemy);
                left -= 60;

                ennemyImage++;

                if (ennemyImage > 8)
                {
                    ennemyImage = 1;
                }

                switch (ennemyImage)
                {
                    case 1:
                        EnemySkin.ImageSource = new BitmapImage(new Uri("C:/Users/pu61qgw/Documents/GitHub/SpaceInvader/Space_Invader_WPF/Image/invader1.gif"));
                        break;
                    case 2:
                        EnemySkin.ImageSource = new BitmapImage(new Uri("C:/Users/pu61qgw/Documents/GitHub/SpaceInvader/Space_Invader_WPF/Image/invader2.gif"));
                        break;
                    case 3:
                        EnemySkin.ImageSource = new BitmapImage(new Uri("C:/Users/pu61qgw/Documents/GitHub/SpaceInvader/Space_Invader_WPF/Image/invader3.gif"));
                        break;
                    case 4:
                        EnemySkin.ImageSource = new BitmapImage(new Uri("C:/Users/pu61qgw/Documents/GitHub/SpaceInvader/Space_Invader_WPF/Image/invader4.gif"));
                        break;
                    case 5:
                        EnemySkin.ImageSource = new BitmapImage(new Uri("C:/Users/pu61qgw/Documents/GitHub/SpaceInvader/Space_Invader_WPF/Image/invader5.gif"));
                        break;
                    case 6:
                        EnemySkin.ImageSource = new BitmapImage(new Uri("C:/Users/pu61qgw/Documents/GitHub/SpaceInvader/Space_Invader_WPF/Image/invader6.gif"));
                        break;
                    case 7:
                        EnemySkin.ImageSource = new BitmapImage(new Uri("C:/Users/pu61qgw/Documents/GitHub/SpaceInvader/Space_Invader_WPF/Image/invader7.gif"));
                        break;
                    case 8:
                        EnemySkin.ImageSource = new BitmapImage(new Uri("C:/Users/pu61qgw/Documents/GitHub/SpaceInvader/Space_Invader_WPF/Image/invader8.gif"));
                        break;

                }

            }
        }
    }
}
