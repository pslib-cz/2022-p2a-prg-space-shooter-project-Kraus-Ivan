using space__shooter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace space__shooter_local
{
    internal abstract class Entity
    {
        public Position Position { get; set; }

        protected Entity(int x, int y)
        {
            Position = new Position(x, y);
        }
        /// <summary>
        /// Abstraktní metoda pro pohyb entity
        /// </summary>
        public abstract void Move();

    }
}
