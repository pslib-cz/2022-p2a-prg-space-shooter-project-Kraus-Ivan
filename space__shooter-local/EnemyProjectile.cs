using space_shooter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace space__shooter_local
{
    internal class EnemyProjectile : Projectile
    {
        private int updateCounter = 0;

        public EnemyProjectile(double x, double y) : base(x, y, ConsoleColor.Red, '.')
        {
        }

        public override void Update(Game game)
        {
            updateCounter++;
            slowDown = slowDown / 2;
            if (updateCounter % slowDown == 0) // pohybuje se pouze při každé páté aktualizaci
            {
                Y += 1;
            }
            // pohyb dolů

            if (CollidesWith(game.Player))
            {
                game.RemoveEnemyProjectile(this);
                game.Player.Hit(game);
            }

            // pokud je projektil mimo obrazovku, odstraní ho
            else if (Y >= Console.WindowHeight)
            {
                game.RemoveEnemyProjectile(this);
            }
        }

    }
}
