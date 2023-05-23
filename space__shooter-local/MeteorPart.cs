﻿using space_shooter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace space__shooter_local
{
    internal class MeteorPart : GameObject
    {
        private int updateCounter = 0;

        public MeteorPart(double x, double y) : base(x, y, ConsoleColor.DarkYellow, 'O')
        {
        }

        public override void Update(Game game)
        {
        }
    }
}