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
        public Projectile(double x, double y, ConsoleColor color, char symbol) : base(x, y, color, symbol)
        {
        }


        public override void Update(Game game)
        {
            // pohyb dolů
            Y -= 2;

            if (CollidesWith(game.Player))
            {
                game.GameOver();
            }

            foreach (var meteor in game.Meteors)
            {
                var hitPart = meteor.Parts.FirstOrDefault(part => CollidesWith(part));
                if (hitPart != null)
                {
                    meteor.parts.Remove(hitPart);
                    break;
                }
            }
        }

    }
}
