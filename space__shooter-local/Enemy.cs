using space__shooter;
using System;
using System.Collections.Generic;
using space__shooter_local;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace space__shooter
{
    internal class Enemy : Entity
    {
        private bool _isStopped;
        private Random _rand;
        private int _moveCounter = 0;

        public Enemy(int x, int y) : base(x, y)
        {
            _rand = new Random();
            _isStopped = false;
        }

        public override void Move()
        {
            if (_isStopped)
                return;

            _moveCounter++;

            if (_moveCounter % 2 == 0)
            {
                Position.Y += 1;
            }

            if (_rand.NextDouble() < 0.1) // 10% procentní šance na zastavení nepřítele
                _isStopped = true;

            if (Position.Y > Console.WindowHeight - 1)
                Position.Y = Console.WindowHeight - 1;
        }

        public Projectile Shoot()
        {
            if (_isStopped && _rand.NextDouble() < 0.01) // 1% procentní šance na střelbu, když je nepřítel zastavený
                return new Projectile(Position.X, Position.Y + 1, false);

            return null;
        }
    }
}

