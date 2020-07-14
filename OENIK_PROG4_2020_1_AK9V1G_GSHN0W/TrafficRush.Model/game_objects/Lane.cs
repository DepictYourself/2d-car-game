//-----------------------------------------------------------------------
// <copyright file="Lane.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace TrafficRush.Model.game_objects
{
    using System.Collections.Generic;
    using System.Windows;

    /// <summary>
    /// Describes a Lane object.
    /// </summary>
    public class Lane
    {
        private Rect area;

        /// <summary>
        /// Initializes a new instance of the <see cref="Lane"/> class.
        /// </summary>
        public Lane()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Lane"/> class.
        /// </summary>
        /// <param name="x">X coordinate as int.</param>
        /// <param name="y">Y coordinate as int.</param>
        /// <param name="width">Width as int.</param>
        /// <param name="height">Height as int.</param>
        public Lane(int x, int y, int width, int height)
        {
            this.area = new Rect(x, y, width, height);
            this.Cars = new List<TrafficCar>();
        }

        /// <summary>
        /// Gets the Area property.
        /// </summary>
        public Rect Area { get => this.area; }

        /// <summary>
        /// Gets or Sets the List of cars on this lane.
        /// </summary>
        public List<TrafficCar> Cars { get; set; }
    }
}
