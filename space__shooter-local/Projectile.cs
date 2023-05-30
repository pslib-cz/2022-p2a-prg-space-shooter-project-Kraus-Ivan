using space__shooter_local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace space__shooter
{
    internal class Projectile : Entity
    {
        /// <summary>
        /// Třída představující projektil
        /// </summary>
        public bool IsPlayerProjectile { get; private set; }
        private int _moveCounter = 0;

        public Projectile(int x, int y, bool isPlayerProjectile) : base(x, y)
        { 
            IsPlayerProjectile = isPlayerProjectile;
        }

        public override void Move()
        {
            if (IsPlayerProjectile)
                Position.Y -= 1;
            else if (_moveCounter++ % 2 == 0)
                Position.Y += 1;
        }
    }
}
