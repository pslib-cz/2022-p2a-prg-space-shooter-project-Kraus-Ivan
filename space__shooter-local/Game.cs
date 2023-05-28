using space__shooter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace space__shooter
{
    internal class Game
    {
        public Player Player { get; private set; }
        public List<Enemy> Enemies { get; private set; }
        public List<Projectile> Projectiles { get; private set; }
        public List<Meteor> Meteors { get; private set; }
        public int Score { get; private set; }
        public int HighScore { get; private set; } // High score
        private Random _rand;
        public int _gameSpeed = 60;
        private DateTime _lastTime;
        private int _scoreCounter = 0;

        public Game()
        {
            Player = new Player(Console.WindowWidth / 2, Console.WindowHeight / 2);
            Enemies = new List<Enemy>();
            Projectiles = new List<Projectile>();
            Meteors = new List<Meteor>();
            Score = 0;
            HighScore = LoadHighScore(); // Load high score from file
            _rand = new Random();
            _lastTime = DateTime.Now;
        }

        public void SpawnEnemy()
        {
            Enemies.Add(new Enemy(_rand.Next(Console.WindowWidth), 0));
        }

        public void SpawnMeteor()
        {
            List<Position> positions = new List<Position>();
            int numParts = _rand.Next(1, 6);
            int initialX = _rand.Next(Console.WindowWidth);
            int initialY = _rand.Next(Console.WindowHeight - 5) + 1;

            for (int i = 0; i < numParts; i++)
            {
                int offsetX = _rand.Next(-1, 2);
                int offsetY = _rand.Next(-1, 2);
                positions.Add(new Position(initialX + offsetX, initialY + offsetY));
            }

            Meteors.Add(new Meteor(positions));
        }

        public void MovePlayer(int dx, int dy)
        {
            Player.Move(dx, dy);
        }

        public void PlayerShoot()
        {
            Projectiles.Add(Player.Shoot());
        }

        public void CheckCollisions()
        {
            foreach (var projectile in Projectiles)
            {
                if (!projectile.IsPlayerProjectile && projectile.Position.Equals(Player.Position))
                {
                    Player.TakeDamage();
                    Projectiles.Remove(projectile);
                    return;
                }
            }

            foreach (var projectile in Projectiles)
            {
                if (projectile.IsPlayerProjectile)
                {
                    foreach (var enemy in Enemies)
                    {
                        if (projectile.Position.Equals(enemy.Position))
                        {
                            Enemies.Remove(enemy);
                            Projectiles.Remove(projectile);
                            Score += 75;
                            return;
                        }
                    }

                    foreach (var meteor in Meteors)
                    {
                        foreach (var part in meteor.Parts)
                        {
                            if (projectile.Position.Equals(part.Position))
                            {
                                meteor.RemovePart(part);
                                Projectiles.Remove(projectile);
                                Score += 50;
                                return;
                            }
                        }
                    }
                }
            }
        }

        public void SpawnEntities()
        {
            if (_rand.NextDouble() < 0.01)
            {
                SpawnEnemy();
            }

            if (_rand.NextDouble() < 0.02)
            {
                SpawnMeteor();
            }
        }

        public void RemoveOutOfBoundsEntities()
        {
            Enemies.RemoveAll(e => e.Position.Y >= Console.WindowHeight);
            Projectiles.RemoveAll(p => p.Position.Y < 0 || p.Position.Y >= Console.WindowHeight);
            foreach (var meteor in Meteors)
            {
                meteor.Parts.RemoveAll(part => part.Position.Y >= Console.WindowHeight);
            }
            Meteors.RemoveAll(m => m.Parts.Count == 0);
        }

        public void Update()
        {
            foreach (var enemy in Enemies) enemy.Move();
            foreach (var projectile in Projectiles) projectile.Move();
            foreach (var meteor in Meteors) meteor.Move();
            _scoreCounter++;
            if (_scoreCounter % 10 == 0)
            {
                Score++;
            }

            foreach (var enemy in Enemies)
            {
                var projectile = enemy.Shoot();
                if (projectile != null)
                {
                    Projectiles.Add(projectile);
                }
            }

            CheckCollisions();

            RemoveOutOfBoundsEntities();

            var now = DateTime.Now;
            if ((now - _lastTime).TotalSeconds >= 5)
            {
                _gameSpeed -= 1;
                if (_gameSpeed < 0) _gameSpeed = 0;
                _lastTime = now;
            }
        }

        public void SaveHighScore() // Save high score to file
        {
            using (var writer = new StreamWriter("highscore.txt"))
            {
                writer.Write(HighScore);
            }
        }

        public int LoadHighScore() // Load high score from file
        {
            if (File.Exists("highscore.txt"))
            {
                using (var reader = new StreamReader("highscore.txt"))
                {
                    if (int.TryParse(reader.ReadToEnd(), out int highScore))
                    {
                        return highScore;
                    }
                }
            }
            return 0;
        }
    }
}
