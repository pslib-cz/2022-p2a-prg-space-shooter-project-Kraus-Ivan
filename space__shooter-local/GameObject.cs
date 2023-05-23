using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace space_shooter
{
    internal abstract class GameObject
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Speed { get; set; } = 0.2;

        public ConsoleColor color { get; set; }
        public char Symbol { get; set; } // Symbol představující enemy

        public GameObject(double x, double y, ConsoleColor color, char symbol)
        {
            X = x;
            Y = y;
            this.color = color;
            Symbol = symbol;
        }

        public abstract void Update(Game game);
        
        public bool CollidesWith(GameObject other)
        {
            return X == other.X && Y == other.Y;
        }
    }
}
