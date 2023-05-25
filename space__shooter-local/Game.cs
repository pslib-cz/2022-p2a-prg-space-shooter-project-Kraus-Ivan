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
    internal class Game
    {
        public Player Player { get; private set; }
        public List<Enemy> Enemies { get; private set; }

        public List<Projectile> Projectiles { get; private set; }

        public List<EnemyProjectile> EnemyProjectiles { get; private set; }

        public List<Meteor> Meteors { get; private set; }

        public double Score { get; private set; }

        public double HighScore { get; private set; }

        private int _tickCounter;

        public Game()
        {
            Reset();
            LoadHighScore();
            Meteors = new List<Meteor>();
        }

        public void Update()
        {
            IncreaseScore(0.5);

            foreach (var projectile in Projectiles.ToList())
            {
                projectile.Update(this);
            }

            foreach (var enemyProjectile in EnemyProjectiles.ToList())
            {
                enemyProjectile.Update(this);
            }

            foreach (var enemy in Enemies.ToList())
            {
                enemy.Update(this);
            }

            foreach (var meteor in Meteors.ToList())
            {
                meteor.Update(this);
            }

            if (_tickCounter % 20 == 0)
            {
                SpawnEnemy();
                SpawnMeteor();
            }

            foreach (var meteor in Meteors.ToList())
            {
                meteor.Update(this);
                meteor.RemoveOffScreenParts();
            }

            // Zkontrolovat kolizi projektilů s nepřáteli a meteory a aktualizovat skóre a odstranit zasažené objekty
            for (int i = Projectiles.Count - 1; i >= 0; i--)
            {
                var projectile = Projectiles[i];
                var hitEnemy = Enemies.FirstOrDefault(e => e.CollidesWith(projectile));
                var hitMeteor = Meteors.FirstOrDefault(m => m.CollidesWith(projectile));
                var hitMeteorPart = Meteors.FirstOrDefault(m => m.CollidesWith(projectile));

                if (hitEnemy != null)
                {
                    IncreaseScore(100);
                    RemoveEnemy(hitEnemy);
                    RemoveProjectile(projectile);
                }
                else if (hitMeteor != null)
                {
                    IncreaseScore(25);
                    RemoveMeteor(hitMeteor);
                    RemoveProjectile(projectile);
                }

            }

            // Odstranit objekty mimo obrazovku
            EnemyProjectiles.RemoveAll(p => p.Y >= Console.WindowHeight);
            Projectiles.RemoveAll(p => p.Y < 0 || p.Y >= Console.WindowHeight);
            Enemies.RemoveAll(e => e.Y >= Console.WindowHeight);
            Meteors.RemoveAll(m => m.Y >= Console.WindowHeight);

            _tickCounter++;
            Player.Update(this);
        }


        public void Reset()
        {
            Player = new Player(Console.WindowWidth / 2, Console.WindowHeight - 1);
            Enemies = new List<Enemy>();
            Projectiles = new List<Projectile>();
            EnemyProjectiles = new List<EnemyProjectile>();
            Meteors = new List<Meteor>();
            Score = 0;
            _tickCounter = 0;
            InitializeGameObjects();
        }

        private void InitializeGameObjects()
        {
            SpawnEnemy();
            SpawnMeteor();
        }

        public void SpawnEnemy()
        {
            int x = new Random().Next(0, Console.WindowWidth);
            Enemies.Add(new Enemy(x, 0));
        }
        public void SpawnMeteor()
        {
            Random rand = new Random();
            int size = rand.Next(1, 4);
            int y = 0 + size;
            int x = rand.Next(1, Console.WindowWidth - 1);
            Meteor meteor = new Meteor(x, y, size);
            Meteors.Add(meteor);
        }

        public void RemoveEnemy(Enemy enemy)
        {
            Enemies.Remove(enemy);
        }
        public void RemoveMeteor(Meteor meteor)
        {
            Meteors.Remove(meteor);
        }

        public void IncreaseScore(double value)
        {
            Score += value;
            if (Score > HighScore)
            {
                HighScore = Score;
                SaveHighScore();
            }
        }

        public void SaveHighScore()
        {
            File.WriteAllText("highscore.txt", HighScore.ToString());
        }

        public void RemoveProjectile(Projectile projectile)
        {
            Projectiles.Remove(projectile);
        }

        public void RemoveEnemyProjectile(EnemyProjectile projectile)
        {
            EnemyProjectiles.Remove(projectile);
        }

        public void GameOver() 
        {
            Console.Clear();
            Console.WriteLine("Game Over");
            Console.WriteLine("Score: " + Score);
            Console.WriteLine("High Score: " + HighScore);
            Environment.Exit(0);
        }

        
        public void LoadHighScore()
        {
            if (File.Exists("highscore.txt"))
            {
                string highScoreText = File.ReadAllText("highscore.txt");
                if(double.TryParse(highScoreText, out double loadedHighScore))
                {
                    HighScore = loadedHighScore;
                }
            }
        }
    }
}
