using space__shooter_local;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace space_shooter
{
    internal class Player : GameObject
    {
        public override void Update(Game game)
        {
        }
        
        public Player(int x, int y) : base(x, y, ConsoleColor.Green, 'A')
        {
        }

        public void Shoot(Game game)
        {
            game.Projectiles.Add(new Projectile(X, Y - 1));
        }

        public void MoveLeft()
        {
            if(X > 0)
                X--;
        }

        public void MoveRight()
        {
            if (X < Console.WindowWidth - 1) ;
                X++;
        }

        public void MoveUp()
        {
            if (Y > 0) 
                Y--;
        }

        public void MoveDown()
        {
            if(Y < Console.WindowHeight - 1)
                Y++;
        }
    }
}
