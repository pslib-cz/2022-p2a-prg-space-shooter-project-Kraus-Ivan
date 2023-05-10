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

        public Input Input { get; private set; }

        public List<Meteor> Meteors { get; private set; }

        public int Score { get; private set; }

        public int HighScore { get; private set; }

        private int _tickCounter;

        public Game()
        {
            LoadHighScore();
            Reset();
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

        public void RemoveEvenmy(Enemy enemy)
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

        public void Update()
        {
            
        }

    }
}
