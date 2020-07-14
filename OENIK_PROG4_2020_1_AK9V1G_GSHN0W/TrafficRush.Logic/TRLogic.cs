//-----------------------------------------------------------------------
// <copyright file="TRLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using TrafficRush.Model;
using TrafficRush.Model.Config;
using TrafficRush.Model.game_objects;
using TrafficRush.Repository;

namespace TrafficRush.Logic
{
    /// <summary>
    /// Logic class.
    /// </summary>
    public class TRLogic : ILogic
    {

        /// <summary>
        /// Event for UI refreshing.
        /// </summary>
        public event EventHandler RefreshScreen;

        /// <summary>
        /// Event for game end.
        /// </summary>
        public event EventHandler GameEnded;

        private event EventHandler BossSequence;

        private const double SPEED_MULTIPLIER = 1.0001;
        private IModel model;
        private IRepository repository;
        private Dispatcher dispatcher;

        private PlayerCar player;
        private List<TrafficCar> traffic;
        private EnemyCar enemy;
        private List<Lane> lanes;
        private bool bossSequence = false;
        private bool gameOver = false;
        private double speed = GameObjectConfig.BaseCarSpeed;


        private object bulletLockObject = new object();
        private object playerBulletCountLockObject = new object();


        /// <summary>
        /// Initializes a new instance of the <see cref="TRLogic"/> class.
        /// </summary>
        /// <param name="model">Model.</param>
        /// <param name="repository">Repository.</param>
        /// <param name="dispatcher">Dispatcher.</param>
        public TRLogic(IModel model, IRepository repository, Dispatcher dispatcher)
        {
            this.model = model;
            this.player = model.Player;
            this.traffic = model.Traffic;
            this.enemy = model.Enemy;
            this.lanes = model.Lanes;
            this.repository = repository;
            this.dispatcher = dispatcher;
            BossSequence += StartBossSequence;
            model.bulletLockObject = bulletLockObject;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TRLogic"/> class.
        /// </summary>
        /// <param name="model">Model.</param>
        /// <param name="repository">Repository.</param>
        public TRLogic(IModel model, IRepository repository)
        {
            this.model = model;
            this.player = model.Player;
            this.traffic = model.Traffic;
            this.enemy = model.Enemy;
            this.lanes = model.Lanes;
            this.repository = repository;
            BossSequence += StartBossSequence;
        }

        /// <summary>
        /// Method for saving current state.
        /// </summary>
        public void Save()
        {
            repository.SaveModelToXml();
        }

        /// <summary>
        /// Method for loading last state.
        /// </summary>
        public void LoadSave()
        {
            this.repository.LoadModelFromXml();
            this.player = model.Player;
            this.traffic = model.Traffic;
            this.enemy = model.Enemy;
            this.lanes = model.Lanes;
            RefreshScreen?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Method for getting highscore.
        /// </summary>
        /// <returns>Highscore.</returns>
        public double GetHighScore()
        {
            return repository.GetHighScore();
        }

        /// <summary>
        /// Method for setting highscore.
        /// </summary>
        public void SetHighScore()
        {
            if (model.Score > GetHighScore())
            {
                repository.SetHighScore(model.Score);
            }
        }

        /// <summary>
        /// Method for spawning traffic.
        /// </summary>
        public void SpawnTraffic()
        {
            foreach (var car in traffic)
            {
                SpawnCar(car);
            }
        }

        /// <summary>
        /// Method for shooting.
        /// </summary>
        public void Shoot()
        {
            if (!gameOver && player.BulletCount > 0)
            {
                player.BulletCount -= 1;
                SpawnBullet(player);
            }
        }

        /// <summary>
        /// This method increments the state of the explosion objects.
        /// Also removes explosion objects after after 6th state.
        /// </summary>
        public void ChangeExplosionStates()
        {
            for (int i = 0; i < this.model.Explosions.Count; i++)
            {
                Explosion explosion = this.model.Explosions[i];
                explosion.State++;
                if (explosion.State >= 6)
                {
                    this.model.Explosions.Remove(explosion);
                }
            }
        }

        /// <summary>
        /// Method for changing lanes.
        /// </summary>
        /// <param name="car">Car to change lane with.</param>
        /// <param name="direction">Left or right.</param>
        public void ChangeLane(Car car, Direction direction)
        {
            switch (direction)
            {
                case Direction.LEFT:
                    if (car.Position != ActiveLane.LEFT)
                    {
                        DecrementEnum(car);
                        SlideToSide(car, direction);
                    }
                    break;
                case Direction.RIGHT:
                    if (car.Position != ActiveLane.RIGHT)
                    {
                        IncrementEnum(car);
                        SlideToSide(car, direction);
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Method for moving traffic.
        /// </summary>
        public void MoveTraffic()
        {
            foreach (var car in traffic)
            {
                if (!gameOver)
                {
                    speed *= SPEED_MULTIPLIER;
                    model.Score += speed / 100;
                    if (!bossSequence && Math.Round(model.Score) != 0 && Math.Round(model.Score) % 500 == 0)
                    {
                        BossSequence?.Invoke(this, EventArgs.Empty);
                    }
                    if (!gameOver && car.Active)
                    {
                        MoveCar(car);
                    }
                    RefreshScreen?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private void SlideToSide(Car car, Direction direction)
        {
            new Task(() =>
            {
                for (int i = 0; i < 5; i++)
                {
                    Thread.Sleep(20);
                    if (direction.Equals(Direction.LEFT))
                    {
                        car.AddX(-30);
                    }
                    else
                    {
                        car.AddX(30);
                    }
                }
            }, TaskCreationOptions.LongRunning).Start();
        }

        private void MoveCar(Car car)
        {
            car.AddY(speed);
            if (car.Area.Y >= GameWindowConfig.WindowHeight)
            {
                ResetCar(car);
            }

            // CRASH
            if (car.Area.IntersectsWith(player.Area))
            {
                SpawnExplosion(car);
                ResetCar(car);
                LowerPlayerHealth(car);
                if (player.Area.Y >= GameWindowConfig.WindowHeight)
                {
                    GameOver();
                }
            }
        }

        private void GameOver()
        {
            bossSequence = false;
            gameOver = true;
            this.dispatcher.Invoke(GameEnded, new object[] { this, EventArgs.Empty });
            SetHighScore();
        }

        private void StartBossSequence(object sender, EventArgs e)
        {
            new Task(() =>
            {
                player.BulletCount = 50;
                bossSequence = true;
                MoveEnemy();
                var stopwatch = Stopwatch.StartNew();
                while (enemy.Health > 0 && stopwatch.ElapsedMilliseconds < 10000)
                {
                    Thread.Sleep(LogicUtils.rnd.Next(8, 11) * 100);
                    new Task(() =>
                    {
                        AttackPlayer();
                    }, TaskCreationOptions.LongRunning).Start();
                    ChangeLane(enemy, (Direction)LogicUtils.rnd.Next(0, 2));
                }
                bossSequence = false;
                enemy.Health = 5;
                while (enemy.Area.Y > -200)
                {
                    enemy.AddY(-0.0001);
                }
                ActivateTraffic();
            }, TaskCreationOptions.LongRunning).Start();
        }

        private void MoveEnemy()
        {
            while (enemy.Area.Y < 150)
            {
                enemy.AddY(0.0001);
            }
        }

        private void AttackPlayer()
        {
            if (bossSequence)
            {
                SpawnBullet(enemy);
            }
        }

        private void SpawnCar(Car car)
        {
            bool condition = true;
            if (car.GetType() == typeof(PoliceCar))
            {
                int p = LogicUtils.rnd.Next(0, 101);
                condition = p <= 50;
            }
            if (condition && !bossSequence)
            {
                new Task(() =>
                {
                    Thread.Sleep(LogicUtils.rnd.Next(0, 4) * 1000);
                    car.Active = true;
                }).Start();
            }
        }

        private void ResetCar(Car car)
        {
            car.Active = false;
            car.SetY(-200);
            SpawnCar(car);
        }

        private void SpawnBullet(Car car)
        {
            new Task(() =>
            {
                Bullet bullet;
                if (car.GetType() == typeof(PlayerCar))
                {
                    bullet = new Bullet(
                        car.Position,
                        GameObjectConfig.BulletWidth,
                        GameObjectConfig.BulletHeight,
                        (int)((car.Area.X + (car.Area.Width / 2)) - GameObjectConfig.BulletWidth), // Bullet X center calculation
                        (int)car.Area.Y - 40);
                }
                else
                {
                    bullet = new Bullet(
                        car.Position,
                        GameObjectConfig.BulletWidth,
                        GameObjectConfig.BulletHeight,
                        (int)((car.Area.X + (car.Area.Width / 2)) - GameObjectConfig.BulletWidth), // Bullet X center calculation
                        (int)car.Area.Y + 200);
                }
                bullet.Active = true;
                bullet.Source = car;
                lock (bulletLockObject)
                {
                    model.Bullets.Add(bullet);
                }
                while (!gameOver && bullet.Active)
                {
                    if (car.GetType() == typeof(PlayerCar))
                    {
                        bullet.Speed = GameObjectConfig.PlayerBulletSpeed;
                    }
                    else
                    {
                        bullet.Speed = GameObjectConfig.EnemyBulletSpeed;
                    }
                    MoveBullet(bullet);
                    Thread.Sleep(10);
                }
                lock (bulletLockObject)
                {
                    model.Bullets.Remove(bullet);
                }
            }, TaskCreationOptions.LongRunning).Start();
        }

        private void MoveBullet(Bullet bullet)
        {
            bullet.AddY(bullet.Speed);
            traffic.ForEach(c =>
            {
                if (c.Area.IntersectsWith(bullet.Area))
                {
                    SpawnExplosion(c);
                    ResetCar(c);
                    bullet.Active = false;
                }
            });
            if (bullet.Area.IntersectsWith(player.Area))
            {
                SpawnExplosion(player);
                bullet.Active = false;
                LowerPlayerHealth();
                if (player.Area.Y >= GameWindowConfig.WindowHeight)
                {
                    GameOver();
                }
            }
            if (bullet.Area.IntersectsWith(enemy.Area))
            {
                SpawnExplosion(enemy);
                bullet.Active = false;
                enemy.Health -= 1;
                if (enemy.Health <= 0)
                {
                    bossSequence = false;
                }
            }
        }

        private void LowerPlayerHealth(Car otherCar = null)
        {
            if (otherCar != null && otherCar.GetType() == typeof(PoliceCar))
            {
                player.AddY(GameObjectConfig.playerHealthLoss * 2);
            }
            else
            {
                player.AddY(GameObjectConfig.playerHealthLoss * 1);
            }
        }

        private void SpawnExplosion(Car car)
        {
            Explosion explosion = new Explosion(car.Position, GameObjectConfig.ExplosionSize, GameObjectConfig.ExplosionSize);
            explosion.SetXY(car.Area.X, car.Area.Y);
            this.model.Explosions.Add(explosion);
        }

        private void ActivateTraffic()
        {
            player.BulletCount = 10;
            foreach (var car in traffic)
            {
                SpawnCar(car);
            }
        }

        private void IncrementEnum(Car car)
        {
            car.Position = (ActiveLane)((int)car.Position + 1);
        }

        private void DecrementEnum(Car car)
        {
            car.Position = (ActiveLane)((int)car.Position - 1);
        }
    }
}
