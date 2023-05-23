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
        private List<MeteorPart> parts = new List<MeteorPart>();
        private int updateCounter = 0;

        public Meteor(double x, double y, int size) : base(x, y, ConsoleColor.DarkYellow, 'O')
        {
            for (int i = 0; i < size; i++)
            {

                if (x + i > Console.WindowWidth || x + i < 0 || y + i > Console.WindowHeight || y + i < 0)
                    return;
                else
                {
                    parts.Add(new MeteorPart(x + i, y));
                    parts.Add(new MeteorPart(x, y + i));
                }
            }
        }

        public override void Update(Game game)
        {
            updateCounter++;
            if (updateCounter % slowDown == 0) 
            {
                Y += Speed;
            }
            // pohyb dolů
            foreach (var part in parts)
            {
                if (updateCounter % slowDown == 0)
                {
                    part.Y += Speed;
                }
                if (part.CollidesWith(game.Player))
                {
                    game.GameOver();
                }
            }
        }

        public void RemoveOffScreenParts()
        {
            parts.RemoveAll(p => p.X < 0 || p.X >= Console.WindowWidth || p.Y < 0 || p.Y >= Console.WindowHeight);
        }


        public IEnumerable<MeteorPart> Parts { get { return parts; } }
    }
}
