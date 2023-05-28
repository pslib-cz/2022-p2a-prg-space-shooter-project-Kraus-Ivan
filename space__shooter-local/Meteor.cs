using space__shooter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace space__shooter
{
    internal class Meteor
    {
        public List<MeteorPart> Parts { get; private set; }

        public Meteor(List<Position> initialPositions)
        {
            Parts = new List<MeteorPart>();

            foreach (var position in initialPositions)
            {
                Parts.Add(new MeteorPart(position.X, position.Y));
            }
        }

        public void Move()
        {
            foreach (var part in Parts)
            {
                part.Move();
            }
        }

        public void RemovePart(MeteorPart part)
        {
            Parts.Remove(part);
        }

    }
}
