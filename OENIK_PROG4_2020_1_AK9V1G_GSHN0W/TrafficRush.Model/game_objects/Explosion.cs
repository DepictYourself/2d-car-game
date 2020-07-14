//-----------------------------------------------------------------------
// <copyright file="Explosion.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace TrafficRush.Model.game_objects
{
    /// <summary>
    /// Class describes an Explosion object.
    /// </summary>
    public class Explosion : GameObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Explosion"/> class.
        /// </summary>
        public Explosion()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Explosion"/> class.
        /// </summary>
        /// <param name="pos">Position as ActiveLane enum.</param>
        /// <param name="width">Width as int.</param>
        /// <param name="height">Height as int.</param>
        public Explosion(ActiveLane pos, int width, int height)
            : base(pos, width, height)
        {
            this.State = 1;
        }

        /// <summary>
        /// Gets or Sets the State of the explosion object.
        /// The object will be rendered differently dependint on the state
        /// state should be between 0 and 6.
        /// </summary>
        public int State { get; set; }
    }
}
