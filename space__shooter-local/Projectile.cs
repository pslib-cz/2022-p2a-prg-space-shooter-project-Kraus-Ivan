﻿using space_shooter;
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
            Y -= 1;

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
