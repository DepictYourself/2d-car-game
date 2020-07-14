//-----------------------------------------------------------------------
// <copyright file="GameObject.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace TrafficRush.Model.game_objects
{
    using System.Windows;

    /// <summary>
    /// Base GameObject definition.
    /// Every other gameObject derives from this.
    /// </summary>
    public class GameObject
    {
        private Rect area;
        private ActiveLane position;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameObject"/> class.
        /// </summary>
        public GameObject()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameObject"/> class.
        /// </summary>
        /// <param name="pos">Position as ActiveLane enum.</param>
        /// <param name="width">Width as int.</param>
        /// <param name="height">Height as int.</param>
        public GameObject(ActiveLane pos, int width, int height)
        {
            this.position = pos;
            this.area.Width = width;
            this.area.Height = height;
        }

        /// <summary>
        /// Gets or Sets the Area Rect property.
        /// </summary>
        public Rect Area
        {
            get { return area; }
            set { area = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the gameobject is active or not.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the Position property of ActiveLane type.
        /// </summary>
        public ActiveLane Position
        {
            get { return position; }
            set { position = value; }
        }

        /// <summary>
        /// Add to the X coodinate.
        /// </summary>
        /// <param name="x">What amount to add.</param>
        public void AddX(double x)
        {
            this.area.X += x;
        }

        /// <summary>
        /// Add to the Y coordinate.
        /// </summary>
        /// <param name="y">What amount to add.</param>
        public void AddY(double y)
        {
            this.area.Y += y;
        }

        /// <summary>
        /// Set the X coordinate.
        /// </summary>
        /// <param name="x">What to set the X coordinate.</param>
        public void SetX(double x)
        {
            this.area.X = x;
        }

        /// <summary>
        /// Set the Y coordinate.
        /// </summary>
        /// <param name="y">What to set to the Y coordinate.</param>
        public void SetY(double y)
        {
            this.area.Y = y;
        }

        /// <summary>
        /// Set X and Y coordinate.
        /// </summary>
        /// <param name="x">What to set the X coordinate.</param>
        /// <param name="y">What to set to the Y coordinate.</param>
        public void SetXY(double x, double y)
        {
            this.area.X = x;
            this.area.Y = y;
        }
    }
}
