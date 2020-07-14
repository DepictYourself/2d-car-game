//-----------------------------------------------------------------------
// <copyright file="GameObjectConfig.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace TrafficRush.Model.Config
{
    /// <summary>
    /// Game object config class.
    /// </summary>
    public static class GameObjectConfig
    {
        /// <summary>
        /// Car width.
        /// </summary>
        public static int CarWidth = 100;

        /// <summary>
        /// Car height.
        /// </summary>
        public static int CarHeight = 160;

        /// <summary>
        /// Start coordinate for player.
        /// </summary>
        public static double PlayerCarStartY = 520;

        /// <summary>
        /// The amount of pixels the player needs to be set back after a collision.
        /// </summary>
        public static int playerHealthLoss = 160;

        /// <summary>
        /// Base player health.
        /// </summary>
        public static int BaseCarHealth = 3;

        /// <summary>
        /// Base car speed.
        /// </summary>
        public static double BaseCarSpeed = 10;

        /// <summary>
        /// Base player bullet speed.
        /// </summary>
        public static double PlayerBulletSpeed = -15;

        /// <summary>
        /// Base enemy bullet speed.
        /// </summary>
        public static double EnemyBulletSpeed = 8;

        /// <summary>
        /// Bullet width.
        /// </summary>
        public static int BulletWidth = 15;

        /// <summary>
        /// Bullet height.
        /// </summary>
        public static int BulletHeight = 50;

        /// <summary>
        /// Explosion size. Height and Width.
        /// </summary>
        public static int ExplosionSize = 180;
    }
}
