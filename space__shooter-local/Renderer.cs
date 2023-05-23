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
    internal class Renderer
    {
        /// <summary>
        /// Vykreslí objekty na obrazovku
        /// </summary>
        /// <param name="game">Daná hra</param>
        public void Render(Game game)
        {
            Console.Clear();

            DrawGameObject(game.Player);

            DrawScore(game.Score, game.HighScore);

            foreach (var enemy in game.Enemies.ToList())
            {
                DrawGameObject(enemy);
            }

            foreach (var projectile in game.Projectiles.ToList())
            {
                DrawGameObject(projectile);
            }

            foreach (var meteor in game.Meteors.ToList())
            {
                DrawMeteorParts(meteor.Parts);
            }
        }


        /// <summary>
        /// Vykreslí skóre na obrazovku
        /// </summary>
        /// <param name="score">Aktuální skóre</param>
        /// <param name="highScore">Nejvyšší dosažené skóre</param>
        private void DrawScore(double score, double highScore)
        {
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"Score: {score} HighScore: {highScore}");
            Console.ResetColor();
        }


        /// <summary>
        /// Vykreslí objekt na jeho souřadnice
        /// </summary>
        /// <param name="gameObject">Objekt k vykreslení</param>
        private void DrawGameObject(GameObject gameObject)
        {
            if (gameObject.X >= 0 && gameObject.Y >= 0 && gameObject.X < Console.WindowWidth && gameObject.Y < Console.WindowHeight)
            {
                Console.SetCursorPosition((int)gameObject.X, (int)gameObject.Y);
                Console.ForegroundColor = gameObject.color;
                Console.Write(gameObject.Symbol);
                Console.ResetColor();
            }
        }

        public void DrawMeteorParts(IEnumerable<MeteorPart> parts)
        {
            foreach (var part in parts)
            {
                if (part.X >= 0 && part.Y >= 0 && part.X < Console.WindowWidth && part.Y < Console.WindowHeight)
                {
                    DrawGameObject(part);
                }
            }
        }


    }
}
