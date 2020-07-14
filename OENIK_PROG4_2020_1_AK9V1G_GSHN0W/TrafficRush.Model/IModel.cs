//-----------------------------------------------------------------------
// <copyright file="IModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace TrafficRush.Model
{
    using System.Collections.Generic;
    using TrafficRush.Model.game_objects;

    /// <summary>
    /// Interface describes the public methods and properties of the Model class.
    /// </summary>
    public interface IModel
    {
        /// <summary>
        /// Gets or Sets the Player object.
        /// </summary>
        PlayerCar Player { get; set; }

        /// <summary>
        /// Gets or Sets the Collection of traffic objects.
        /// </summary>
        List<TrafficCar> Traffic { get; set; }

        /// <summary>
        /// Gets or Sets the Collection of the Bullets.
        /// </summary>
        List<Bullet> Bullets { get; set; }

        /// <summary>
        /// Gets or Sets the Enemy object.
        /// </summary>
        EnemyCar Enemy { get; set; }

        /// <summary>
        /// Gets or Sets the lane objects of the game.
        /// </summary>
        List<Lane> Lanes { get; set; }

        /// <summary>
        /// Gets or Sets the Score.
        /// </summary>
        double Score { get; set; }

        /// <summary>
        /// Gets or Sets the collection of the Explosion object in the game.
        /// </summary>
        List<Explosion> Explosions { get; set; }

        /// <summary>
        /// Object for thread safe bullet handling.
        /// </summary>
        object bulletLockObject { get; set; }

        /// <summary>
        /// Initializes the state of the TrafficCar objects.
        /// </summary>
        void InitalizeTraffic();
    }
}
