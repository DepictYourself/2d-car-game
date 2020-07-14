using System.Windows;

namespace TrafficRush.Model.game_objects
{
    /// <summary>
    /// Bullet.
    /// </summary>
    public class Bullet : GameObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Bullet"/> class.
        /// </summary>
        /// <param name="pos">Position.</param>
        /// <param name="width">Width.</param>
        /// <param name="height">Height.</param>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        public Bullet(ActiveLane pos, int width, int height, int x, int y)
            : base(pos, width, height)
        {
            this.Area = new Rect(x, y, width, height);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Bullet"/> class.
        /// </summary>
        public Bullet()
        {
        }

        /// <summary>
        /// Gets or sets bullet speed.
        /// </summary>
        public double Speed { get; set; }

        /// <summary>
        /// Gets or setssource of bullet (Car).
        /// </summary>
        public Car Source { get; set; }
    }
}
