//-----------------------------------------------------------------------
// <copyright file="PoliceCar.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace TrafficRush.Model.game_objects
{
    /// <summary>
    /// Police car class.
    /// </summary>
    public class PoliceCar : TrafficCar
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PoliceCar"/> class.
        /// </summary>
        public PoliceCar()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PoliceCar"/> class.
        /// </summary>
        /// <param name="pos">Position.</param>
        /// <param name="width">Width.</param>
        /// <param name="height">Height.</param>
        public PoliceCar(ActiveLane pos, int width, int height)
            : base(pos, width, height)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PoliceCar"/> class.
        /// </summary>
        /// <param name="pos">Position.</param>
        /// <param name="width">Width.</param>
        /// <param name="height">Height.</param>
        /// <param name="y">Y coordinate.</param>
        public PoliceCar(ActiveLane pos, int width, int height, double y)
            : base(pos, width, height, y)
        {
        }
    }
}
