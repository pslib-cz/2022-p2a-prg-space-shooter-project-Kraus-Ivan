using space__shooter_local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace space__shooter
{
    internal class MeteorPart : Entity
    {
        /// <summary>
        /// Třída představující jednotlivou část meteoru
        /// </summary>
        private int _moveCounter = 0;
        public MeteorPart(int x, int y) : base(x, y)
        {
        }

        public override void Move()
        {
            _moveCounter++;

            if (_moveCounter % 5 == 0)
            {
                Position.Y += 1;
            }
        }
    }

}
