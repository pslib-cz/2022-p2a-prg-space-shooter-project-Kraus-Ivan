using space_shooter;
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
            if (CollidesWith(game.Player))
            {
                game.Player.Hit(game);

                var parentMeteor = game.Meteors.FirstOrDefault(m => m.Parts.Contains(this));
                if (parentMeteor != null)
                {
                    parentMeteor.RemovePart(this);
                }
            }
        }
    }
}
