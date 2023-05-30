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
        /// <summary>
        /// Hlavní třídá zajišťující průběh hry
        /// </summary>
        public Player Player { get; private set; }
        public List<Enemy> Enemies { get; private set; }
        public List<Projectile> Projectiles { get; private set; }
        public List<Meteor> Meteors { get; private set; }
        public int Score { get; private set; } = 0;
        public int HighScore { get; private set; } = 0;
        private Random _rand;
        public int GameSpeed { get; private set; } = 60;
        private DateTime _lastTime;
        private int _scoreCounter = 0;

        public Game()
        {
            Player = new Player(Console.WindowWidth / 2, Console.WindowHeight -5);
            Enemies = new List<Enemy>();
            Projectiles = new List<Projectile>();
            Meteors = new List<Meteor>();
            Score = 0;
            LoadHighScore();
            _rand = new Random();
            _lastTime = DateTime.Now;
        }
        
        /// <summary>
        /// Spawnuje nepřátele
        /// </summary>
        public void SpawnEnemy()
        {
            Enemies.Add(new Enemy(_rand.Next(Console.WindowWidth), 0));
        }

        /// <summary>
        /// Spawnuje meteory
        /// </summary>
        public void SpawnMeteor()
        {
            List<Position> positions = new List<Position>();
            int numParts = _rand.Next(1, 6);
            int initialX = _rand.Next(Console.WindowWidth);
            int initialY = _rand.Next(0) + 1;

            for (int i = 0; i < numParts; i++)
            {
                int offsetX = _rand.Next(-1, 2);
                int offsetY = _rand.Next(-1, 2);
                positions.Add(new Position(initialX + offsetX, initialY + offsetY));
            }

            Meteors.Add(new Meteor(positions));
        }

        /// <summary>
        /// Zajišťuje ovládání pozice hráče
        /// </summary>
        /// <param name="dx">X osa</param>
        /// <param name="dy">Y osa</param>
        public void MovePlayer(int dx, int dy)
        {
            Player.Move(dx, dy);
        }

        /// <summary>
        /// Zajišťuje střelbu hráče
        /// </summary>
        public void PlayerShoot()
        {
            Projectiles.Add(Player.Shoot());
        }

        /// <summary>
        /// Kontroluje kolizi objektů ve hře
        /// </summary>
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

            foreach (var enemy in Enemies)
            {
                if (Player.Position.Equals(enemy.Position))
                {
                    Player.TakeDamage();
                    Enemies.Remove(enemy);
                    return;
                }
            }

            foreach (var meteor in Meteors)
            {
                foreach (var part in meteor.Parts)
                {
                    if (Player.Position.Equals(part.Position))
                    {
                        Player.TakeDamage();
                        meteor.RemovePart(part);
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Spawnuje meteory a nepřátele s určitou pravděpodobností každý průběh hry
        /// </summary>
        public void SpawnEntities()
        {

            if (_rand.NextDouble() < 0.01) // Pravděpodobnost 1%
            {
                SpawnEnemy();
            }

            if (_rand.NextDouble() < 0.02) // Pravděpodobnost 2%
            {
                SpawnMeteor();
            }
        }

        /// <summary>
        /// Odstraní objekty, které se nenachází v obrazovce
        /// </summary>
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

        /// <summary>
        /// Kontroluje, zda je hráč mrtev
        /// </summary>
        /// <returns>Boolean</returns>
        public bool IsPlayerDead()
        {
            return Player.Lives <= 0;
        }

        /// <summary>
        /// Updatuje hru
        /// </summary>
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
                GameSpeed -= 1;
                if (GameSpeed < 0) GameSpeed = 0;
                _lastTime = now;
            }
        }

        private const string HighScoreFilePath = "highscore.txt";

        // Zapisuje skóre, pokud je větší než highscore
        public void SaveHighScore()
        {
            if(Score > HighScore)
            {
                File.WriteAllText(HighScoreFilePath, Score.ToString());
            }
        }

        // Načítá highscore
        public void LoadHighScore()
        {
            if (File.Exists(HighScoreFilePath))
            {
                string highScoreText = File.ReadAllText(HighScoreFilePath);
                if (int.TryParse(highScoreText, out int loadedHighScore))
                {
                    HighScore = loadedHighScore;
                }
            }
        }
    }
}
