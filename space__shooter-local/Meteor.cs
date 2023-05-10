using space_shooter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace space__shooter_local
{
    internal class Meteor : GameObject
    {
        public Meteor(int x, int y) : base(x, y, ConsoleColor.DarkYellow, 'O')
        {
        }

        public override void Update(Game game)
        {
            // pohyb dolů
            Y++;

            // pokud se meteor dotkne hráče, tak se hra ukončí
            if (CollidesWith(game.Player))
            {
                game.GameOver();
            }
        }
    }
}
