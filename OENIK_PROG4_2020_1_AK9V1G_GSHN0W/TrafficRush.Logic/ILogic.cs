//-----------------------------------------------------------------------
// <copyright file="ILogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using TrafficRush.Model.game_objects;

namespace TrafficRush.Logic
{
    /// <summary>
    /// Logic defining interface.
    /// </summary>
    public interface ILogic
    {
        /// <summary>
        /// Event for refreshing screen.
        /// </summary>
        event EventHandler RefreshScreen;

        /// <summary>
        /// Event for game end.
        /// </summary>
        event EventHandler GameEnded;

        /// <summary>
        /// Method for changing lanes.
        /// </summary>
        /// <param name="car">Car to change lanes with.</param>
        /// <param name="direction">Left or right.</param>
        void ChangeLane(Car car, Direction direction);

        /// <summary>
        /// Method for moving traffic.
        /// </summary>
        void MoveTraffic();

        /// <summary>
        /// Method for spawning traffic.
        /// </summary>
        void SpawnTraffic();

        /// <summary>
        /// Method for shooting.
        /// </summary>
        void Shoot();

        /// <summary>
        /// Method for saving current state.
        /// </summary>
        void Save();

        /// <summary>
        /// Method for loading saved state.
        /// </summary>
        void LoadSave();

        /// <summary>
        /// Method gets current saved highscore from file via repository.
        /// </summary>
        /// <returns>Highscore value as double.</returns>
        double GetHighScore();

        /// <summary>
        /// Method for writing new highscore to file via repository.
        /// </summary>
        void SetHighScore();

        /// <summary>
        /// Method for explosion logic.
        /// </summary>
        void ChangeExplosionStates();
    }
}
