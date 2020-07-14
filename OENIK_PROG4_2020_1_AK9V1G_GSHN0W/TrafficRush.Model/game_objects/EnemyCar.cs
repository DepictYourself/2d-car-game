//-----------------------------------------------------------------------
// <copyright file="EnemyCar.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TrafficRush.Model.game_objects
{
    /// <summary>
    /// Describes an EnemyCar object.
    /// </summary>
    public class EnemyCar : Car
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnemyCar"/> class.
        /// </summary>
        /// <param name="pos">Lane position as ActiveLane enum.</param>
        /// <param name="width">Width.</param>
        /// <param name="height">Height.</param>
        /// <param name="y">Y coordinate as int.</param>
        public EnemyCar(ActiveLane pos, int width, int height, int y)
            : base(pos, width, height)
        {
            this.SetY(y);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnemyCar"/> class.
        /// </summary>
        public EnemyCar()
        {
        }

        /// <summary>
        /// Gets or Sets the shots required to destroy an EnemyCar obj in the game.
        /// </summary>
        public int Health { get; set; }
    }
}
