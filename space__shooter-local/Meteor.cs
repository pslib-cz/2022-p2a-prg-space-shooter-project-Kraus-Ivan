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
        /// <summary>
        /// Třída představující meteor
        /// </summary>
        public List<MeteorPart> Parts { get; private set; } // Meteor obsahuje několik částí

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

        /// <summary>
        /// Odstraní část meteoru
        /// </summary>
        /// <param name="part">Část meteoru</param>
        public void RemovePart(MeteorPart part)
        {
            Parts.Remove(part);
        }

    }
}
