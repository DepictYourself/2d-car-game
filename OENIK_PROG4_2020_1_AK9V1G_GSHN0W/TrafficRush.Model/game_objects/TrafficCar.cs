//-----------------------------------------------------------------------
// <copyright file="TrafficCar.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace TrafficRush.Model.game_objects
{
    using System.Xml.Serialization;

    /// <summary>
    /// Definition of TrafficCar object.
    /// </summary>
    [XmlInclude(typeof(PoliceCar))]
    public class TrafficCar : Car
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrafficCar"/> class.
        /// </summary>
        /// <param name="pos">Position.</param>
        /// <param name="width">Width.</param>
        /// <param name="height">Height.</param>
        public TrafficCar(ActiveLane pos, int width, int height)
            : base(pos, width, height)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrafficCar"/> class.
        /// </summary>
        /// <param name="pos">Position.</param>
        /// <param name="width">Width.</param>
        /// <param name="height">Height.</param>
        /// <param name="y">Y coordinate as double.</param>
        public TrafficCar(ActiveLane pos, int width, int height,  double y)
            : this(pos, width, height)
        {
            SetY(y);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrafficCar"/> class.
        /// </summary>
        public TrafficCar()
        {
        }
    }
}
