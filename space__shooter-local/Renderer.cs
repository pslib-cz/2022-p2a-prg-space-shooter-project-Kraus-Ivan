using space__shooter;
using space__shooter_local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace space__shooter
{
    internal class Renderer
    {
        private readonly Dictionary<Type, (ConsoleColor color, char c)> _entityDrawData = new Dictionary<Type, (ConsoleColor color, char c)>
        {
            { typeof(Player), (ConsoleColor.Green, 'A') },
            { typeof(Enemy), (ConsoleColor.Red, 'V') },
            { typeof(Projectile), (ConsoleColor.White, '.') },
            { typeof(MeteorPart), (ConsoleColor.Yellow, 'O') },
        };

        public void Render(Game game)
        {
            Console.Clear();

            DrawEntity(game.Player);
            foreach (var enemy in game.Enemies)
                DrawEntity(enemy);
            foreach (var projectile in game.Projectiles)
                DrawEntity(projectile);
            foreach (var meteor in game.Meteors)
                foreach (var part in meteor.Parts)
                    DrawEntity(part);

            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.Write("Score: {0} High Score: {1} Lives: {2}", game.Score, game.HighScore, game.Player.Lives);
        }

        private void DrawEntity(Entity entity)
        {
            if (_entityDrawData.TryGetValue(entity.GetType(), out var drawData))
                Draw(entity.Position, drawData.color, drawData.c);
        }

        private void Draw(Position position, ConsoleColor color, char c)
        {
            if (position.X >= 0 && position.X < Console.WindowWidth &&
                position.Y >= 0 && position.Y < Console.WindowHeight)
            {
                Console.SetCursorPosition(position.X, position.Y);
                Console.ForegroundColor = color;
                Console.Write(c);
                Console.ResetColor();
            }
        }
    }
}
