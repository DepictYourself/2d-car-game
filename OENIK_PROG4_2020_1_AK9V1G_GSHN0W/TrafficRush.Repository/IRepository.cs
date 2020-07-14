//-----------------------------------------------------------------------
// <copyright file="IRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace TrafficRush.Repository
{
    /// <summary>
    /// Repository defining interface.
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Method for getting high score.
        /// </summary>
        /// <returns>High score.</returns>
        double GetHighScore();

        /// <summary>
        /// Method for saving high score.
        /// </summary>
        /// <param name="score">New high score.</param>
        void SetHighScore(double score);

        /// <summary>
        /// Method for saving current state.
        /// </summary>
        void SaveModelToXml();

        /// <summary>
        /// Method for saving current state.
        /// </summary>
        void LoadModelFromXml();
    }
}
