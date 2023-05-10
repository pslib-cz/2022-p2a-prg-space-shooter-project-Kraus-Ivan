using space_shooter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace space__shooter_local
{
    internal class Projectile : GameObject
    {
        public Projectile(int x, int y, ConsoleColor color, char symbol) : base(x, y, ConsoleColor.Yellow, '|')
        {
        }

        public override void Update(Game game)
        {
            // pohyb dolů
            Y--;

            // pokud se projektil dotkne hráče, tak se hra ukončí
            if (CollidesWith(game.Player))
            {
                game.GameOver();
            }

            // pokud projektil zasáhne enemy, tak se enemy zničí
            foreach (var enemy in game.Enemies)
            {
                if (CollidesWith(enemy))
                {
                    game.RemoveEnemy(enemy);
                    game.RemoveProjectile(this);
                    break;
                }
            }
        }
    }
}
