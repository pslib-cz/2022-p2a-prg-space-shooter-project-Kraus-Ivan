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

        public Enemy(int x, int y) : base(x, y, ConsoleColor.Red, 'V')
        {
        }

        public override void Update(Game game)
        {
            // pohyb dolů
            Y++;

            // pokud se enemy dotkne hráče, tak se hra ukončí
            if (CollidesWith(game.Player))
            {
                game.GameOver();
            }
        }

    }
}
