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


        public List<Meteor> Meteors { get; private set; }

        public int Score { get; private set; }

        public int HighScore { get; private set; }
 
        private int _tickCounter;

        public Game()
        {
            Reset();
            LoadHighScore();
            Meteors = new List<Meteor>();
        }


        public void Update()
        {
            IncreaseScore(1);
            _tickCounter++;
            Player.Update(this);


            if (_tickCounter % 50 == 0)
            {
                SpawnEnemies();
                SpawnMeteors();
            }

            foreach (var enemy in Enemies.ToList())
             {
                enemy.Update(this);
            }

            foreach (var projectile in Projectiles.ToList())
            {
                projectile.Update(this);
            }

            foreach (var meteor in Meteors.ToList())
            {
                meteor.Update(this);
            }

            // Odstraní nepřátele a meteority, kteří byli zasaženi střelou nebo se nachází mimo obrazovku
            Enemies.RemoveAll(e => Projectiles.Any(p => p.CollidesWith(e)) || e.Y >= Console.WindowHeight);

            Meteors.RemoveAll(m => Projectiles.Any(p => p.CollidesWith(m)) || m.Y >= Console.WindowHeight);

            // Odstraní projektily mimo obrazovku
            Projectiles.RemoveAll(p => p.Y < 0);

            // Přidá 100 skóre za zničení nepřítele
            IncreaseScore(Enemies.Count(e => Projectiles.Any(p => p.CollidesWith(e))) * 100);

            // Přidá 25 skóre za zničení meteoritu
            IncreaseScore(Meteors.Count(m => Projectiles.Any(p => p.CollidesWith(m))) * 25);
        }

        public void Reset()
        {
            Player = new Player(Console.WindowWidth / 2, Console.WindowHeight - 1);
            Enemies = new List<Enemy>();
            Projectiles = new List<Projectile>();
            Meteors = new List<Meteor>();
            Score = 0;
            _tickCounter = 0;
            InitializeGameObjects();
        }

        private void InitializeGameObjects()
        {
            SpawnEnemies();
            SpawnMeteors();
        }

        public void SpawnEnemies()
        {
            int enemyCount = new Random().Next(1, 5);
            for (int i = 0; i < enemyCount; i++)
            {
                int x = new Random().Next(0, Console.WindowWidth);
                Enemies.Add(new Enemy(x, 0));
            }
        }
        public void SpawnMeteors()
        {
            int meteorCount = new Random().Next(1, 5);
            for (int i = 0; i < meteorCount; i++)
            {
                int x = new Random().Next(0, Console.WindowWidth);
                Meteors.Add(new Meteor(x, 0));
            }
        }

        public void RemoveEnemy(Enemy enemy)
        {
            Enemies.Remove(enemy);
        }
        public void RemoveMeteor(Meteor meteor)
        {
            Meteors.Remove(meteor);
        }

        public void IncreaseScore(int value)
        {
            Score += value;
            if(Score > HighScore)
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
                if(int.TryParse(highScoreText, out int loadedHighScore))
                {
                    HighScore = loadedHighScore;
                }
            }
        }


    }
}
