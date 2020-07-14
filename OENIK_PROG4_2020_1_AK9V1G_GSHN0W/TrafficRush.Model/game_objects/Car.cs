//-----------------------------------------------------------------------
// <copyright file="Car.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace TrafficRush.Model.game_objects
{
    using System.Windows;

    /// <summary>
    /// Enum used for lane changing.
    /// </summary>
    public enum Direction
    {
        /// <summary>
        /// Left.
        /// </summary>
        LEFT = 0,

        /// <summary>
        /// Right.
        /// </summary>
        RIGHT = 1,
    }

    /// <summary>
    /// Enum for active lane.
    /// </summary>
    public enum ActiveLane
    {
        /// <summary>
        /// Left.
        /// </summary>
        LEFT = 0,

        /// <summary>
        /// Middle.
        /// </summary>
        MIDDLE = 1,

        /// <summary>
        /// Right.
        /// </summary>
        RIGHT = 2,
    }

    /// <summary>
    /// Base class for car objects.
    /// </summary>
    public class Car : GameObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Car"/> class.
        /// </summary>
        /// <param name="pos">Position.</param>
        /// <param name="width">Width.</param>
        /// <param name="height">Height.</param>
        public Car(ActiveLane pos, int width, int height)
            : base(pos, width, height)
        {
            int x = 0;
            int y = -160;
            if (pos == ActiveLane.LEFT)
            {
                x = 103;
            }
            if (pos == ActiveLane.MIDDLE)
            {
                x = 254;
            }
            if (pos == ActiveLane.RIGHT)
            {
                x = 405;
            }
            this.Area = new Rect(x, y, width, height);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Car"/> class.
        /// </summary>
        public Car()
        {
        }
    }
}
