using System;
using System.Collections.Generic;
using space__shooter_local;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace space__shooter
{
    internal class Player : Entity
    {
        /// <summary>
        /// Třída představující hráče
        /// </summary>
        public int Lives { get; private set; }

        public Player(int x, int y) : base(x, y)
        {
            Lives = 3;
        }

        public override void Move()
        {
        }

        public void Move(int dx, int dy)
        {
            Position.X += dx;
            Position.Y += dy;

            if (Position.X < 0) Position.X = 0;
            if (Position.Y < 0) Position.Y = 0;
            if (Position.X > Console.WindowWidth - 1) Position.X = Console.WindowWidth - 1;
            if (Position.Y > Console.WindowHeight - 1) Position.Y = Console.WindowHeight - 1;
        }

        public Projectile Shoot()
        {
            return new Projectile(Position.X, Position.Y - 1, true);
        }

        public void TakeDamage()
        {
            Lives--;
        }
    }
}
