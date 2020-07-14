//-----------------------------------------------------------------------
// <copyright file="PlayerCar.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace TrafficRush.Model.game_objects
{
    using TrafficRush.Model.Config;

    /// <summary>
    /// Definition of a PlayerCar object.
    /// </summary>
    public class PlayerCar : Car
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerCar"/> class.
        /// </summary>
        public PlayerCar()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerCar"/> class.
        /// </summary>
        /// <param name="pos">Post as ActiveLane enum.</param>
        /// <param name="width">Width as int.</param>
        /// <param name="height">Height as int.</param>
        public PlayerCar(ActiveLane pos, int width, int height)
            : base(pos, width, height)
        {
            this.Health = GameObjectConfig.BaseCarHealth;
            this.Speed = GameObjectConfig.BaseCarSpeed;
            this.SetY(GameObjectConfig.PlayerCarStartY);
        }

        /// <summary>
        /// Gets or Sets the Health.
        /// </summary>
        public int Health { get; set; }

        /// <summary>
        /// Gets or Sets the speed property.
        /// </summary>
        public double Speed { get; set; }

        /// <summary>
        /// Gets or sets player bullet count.
        /// </summary>
        public int BulletCount { get; set; }
    }
}
