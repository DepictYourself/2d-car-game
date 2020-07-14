//-----------------------------------------------------------------------
// <copyright file="PauseWindowViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace TrafficRush
{
    using System;
    using TrafficRush.Logic;

    /// <summary>
    /// Model class for PauseWindow.
    /// </summary>
    public class PauseWindowViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PauseWindowViewModel"/> class.
        /// </summary>
        /// <param name="logic">A reference which implements the ILogic interface.</param>
        public PauseWindowViewModel(ILogic logic)
        {
            SaveAction += () => logic.Save();

            LoadAction += () => logic.LoadSave();
        }

        /// <summary>
        /// Gets an Action which hold the functionality to save the game.
        /// </summary>
        public Action SaveAction { get; private set; }

        /// <summary>
        /// Gets the Action which contains the method call to load a new game state.
        /// </summary>
        public Action LoadAction { get; private set; }
    }
}
