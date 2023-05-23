using space_shooter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace space__shooter_local
{
    internal class Enemy : GameObject                                                                                               
    {
        int delay = 0;
        public Enemy(double x, double y) : base(x, y, ConsoleColor.Red, 'V')
        {
        }

        public void Shoot(Game game)
        {
            game.Projectiles.Add(new EnemyProjectile(X, Y + 2));
        }

        public override void Update(Game game)
        {
            if(delay == 5)
            {
                this.Shoot(game);
            }

            // pohyb dolů
            Y += Speed;

            // pokud se enemy dotkne hráče, tak se hra ukončí
            if (CollidesWith(game.Player))
            {
                game.GameOver();
            }
            delay++;
        }

    }
}
