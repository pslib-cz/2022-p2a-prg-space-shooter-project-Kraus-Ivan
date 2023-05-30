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
        /// <summary>
        /// Třída starající se o vykreslování obrazu do konzole
        /// </summary>
        /// 
        
        private readonly Dictionary<Type, (ConsoleColor color, char c)> _entityDrawData = new Dictionary<Type, (ConsoleColor color, char c)>
        {
            { typeof(Player), (ConsoleColor.Green, 'A') },
            { typeof(Enemy), (ConsoleColor.Red, 'V') },
            { typeof(MeteorPart), (ConsoleColor.Yellow, 'O') },
        };

        /// <summary>
        /// Vykresluje jednotlivé objekty
        /// </summary>
        /// <param name="game">Hra</param>
        public void Render(Game game)
        {
            Console.Clear();

            DrawEntity(game.Player);
            foreach (var enemy in game.Enemies)
                DrawEntity(enemy);
            foreach (var projectile in game.Projectiles)
                DrawProjectile(projectile);
            foreach (var meteor in game.Meteors)
                foreach (var part in meteor.Parts)
                    DrawEntity(part);

            Console.SetCursorPosition(0, 0);
            Console.Write("Score: {0} High Score: {1} Lives: {2}", game.Score, game.HighScore, game.Player.Lives);
        }

        /// <summary>
        /// Vykresluje konec hry
        /// </summary>
        /// <param name="game">Hra</param>
        public void RenderGameOver(Game game)
        {
            Console.SetCursorPosition(Console.WindowWidth/2, Console.WindowHeight / 2);
            Console.Clear();
            Console.WriteLine("Game Over! Score: {0}, High Score: {1}", game.Score, game.HighScore);
            Console.WriteLine("Press 'R' to restart, 'Q' to quit.");
        }

        /// <summary>
        /// Vykresluje danou entitu
        /// </summary>
        /// <param name="entity">Entita</param>
        private void DrawEntity(Entity entity)
        {
            if (_entityDrawData.TryGetValue(entity.GetType(), out var drawData))
                Draw(entity.Position, drawData.color, drawData.c);
        }

        /// <summary>
        /// Vykresluje daný projektil
        /// </summary>
        /// <param name="projectile">Projektil</param>
        private void DrawProjectile(Projectile projectile)
        {
            var drawChar = projectile.IsPlayerProjectile ? '|' : '.';
            Draw(projectile.Position, ConsoleColor.White, drawChar);
        }

        /// <summary>
        /// Vykreslí daný obsah
        /// </summary>
        /// <param name="position">Pozice</param>
        /// <param name="color">Barva</param>
        /// <param name="c">Znak</param>
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
