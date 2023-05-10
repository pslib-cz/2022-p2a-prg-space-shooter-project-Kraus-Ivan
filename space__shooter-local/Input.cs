using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace space_shooter
{
    internal class Input
    {
        // kontroluje, zda je na konzoli dostupný vstup a přečte jej
        public ConsoleKeyInfo ReadInput()
        {
            return Console.KeyAvailable ? Console.ReadKey(intercept: true) : default(ConsoleKeyInfo);
        }
        
        public bool IsFirePressed()
        {
            return ReadInput().Key == ConsoleKey.Spacebar;
        }

        public bool IsMoveLeftPressed()
        {
            return ReadInput().Key == ConsoleKey.LeftArrow;
        }

        public bool IsMoveRightPressed()
        {
            return ReadInput().Key == ConsoleKey.RightArrow;
        }

        public bool IsMoveUpPressed()
        {
            return ReadInput().Key == ConsoleKey.UpArrow;
        }

        public bool IsMoveDownPressed()
        {
            return ReadInput().Key == ConsoleKey.DownArrow;
        }
    }
}
