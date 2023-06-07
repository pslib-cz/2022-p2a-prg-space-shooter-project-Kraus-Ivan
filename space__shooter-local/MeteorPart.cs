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
        Random rnd = new Random();
        private int _moveStep = 0;

        public MeteorPart(int x, int y) : base(x, y)
        {
            _moveStep = rnd.Next(2, 10);
        }

        public override void Move()
        {
            _moveCounter++;

            if (_moveCounter % _moveStep == 0)
            {
                Position.Y += 1;
            }
        }
    }

}
