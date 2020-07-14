//-----------------------------------------------------------------------
// <copyright file="TRModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace TrafficRush.Model
{
    using System;
    using System.Collections.Generic;
    using TrafficRush.Model.Config;
    using TrafficRush.Model.game_objects;

    /// <summary>
    /// The Model class describes a momentary state of the game.
    /// </summary>
    public class TRModel : IModel
    {
        private static readonly Random Rnd = new Random();

        /// <summary>
        /// Initializes a new instance of the <see cref="TRModel"/> class.
        /// </summary>
        public TRModel()
        {
            this.Player = new PlayerCar(ActiveLane.MIDDLE, GameObjectConfig.CarWidth, GameObjectConfig.CarHeight) { BulletCount = 10 };
            this.Traffic = new List<TrafficCar>();
            this.Bullets = new List<Bullet>();
            this.Enemy = new EnemyCar(ActiveLane.MIDDLE, GameObjectConfig.CarWidth, GameObjectConfig.CarHeight, -200) { Health = 5 };
            this.Lanes = new List<Lane>()
            {
                new Lane(
                    GameWindowConfig.LaneOneX,
                    0,
                    GameWindowConfig.LaneWidth,
                    GameWindowConfig.LaneHeight),

                new Lane(
                    GameWindowConfig.LaneTwoX,
                    0,
                    GameWindowConfig.LaneWidth,
                    GameWindowConfig.LaneHeight),

                new Lane(
                    GameWindowConfig.LaneThreeX,
                    0,
                    GameWindowConfig.LaneWidth,
                    GameWindowConfig.LaneHeight),
            };
            AddTrafficToLanes();
            this.Explosions = new List<Explosion>();
        }

        /// <summary>
        /// Gets or Sets the Player object.
        /// </summary>
        public PlayerCar Player { get; set; }

        /// <summary>
        /// Gets or Sets the Collection of traffic objects.
        /// </summary>
        public List<TrafficCar> Traffic { get; set; }

        /// <summary>
        /// Gets or Sets the Collection of the Bullets.
        /// </summary>
        public List<Bullet> Bullets { get; set; }

        /// <summary>
        /// Gets or Sets the Enemy object.
        /// </summary>
        public EnemyCar Enemy { get; set; }

        /// <summary>
        /// Gets or Sets the lane objects of the game.
        /// </summary>
        public List<Lane> Lanes { get; set; }

        /// <summary>
        /// Gets or Sets the Score.
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// Gets or Sets the collection of the Explosion object in the game.
        /// </summary>
        public List<Explosion> Explosions { get; set; }

        public object bulletLockObject { get; set; }

        /// <summary>
        /// Initializes the state of the TrafficCar objects.
        /// </summary>
        public void InitalizeTraffic()
        {
            Traffic.Add(new TrafficCar(ActiveLane.LEFT, GameObjectConfig.CarWidth, GameObjectConfig.CarHeight, -200));
            Traffic.Add(new TrafficCar(ActiveLane.MIDDLE, GameObjectConfig.CarWidth, GameObjectConfig.CarHeight, -200));
            Traffic.Add(new TrafficCar(ActiveLane.RIGHT, GameObjectConfig.CarWidth, GameObjectConfig.CarHeight, -200));
            Traffic.Add(new PoliceCar(ActiveLane.MIDDLE, GameObjectConfig.CarWidth, GameObjectConfig.CarHeight, -200));
        }

        private void AddTrafficToLanes()
        {
            foreach (var car in Traffic)
            {
                Lanes[(int)car.Position].Cars.Add(car);
            }
        }
    }
}
