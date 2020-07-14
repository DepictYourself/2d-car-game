//-----------------------------------------------------------------------
// <copyright file="GameWindowConfig.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace TrafficRush.Model.Config
{
    /// <summary>
    /// Static class contains Configuration data for GameWindow.
    /// </summary>
    public static class GameWindowConfig
    {
        /// <summary>
        /// Width of the GameWindow.
        /// </summary>
        public static double WindowWidth = 608;

        /// <summary>
        /// Height of the GameWindow.
        /// </summary>
        public static double WindowHeight = 1000;

        /// <summary>
        /// Margin of the GameWindow on left and right.
        /// </summary>
        public static int WindowSideBorder = 80;

        /// <summary>
        /// Distance between the side of the window and the start of the road.
        /// </summary>
        public static int LaneMargin = 80;

        /// <summary>
        /// Width of a lane.
        /// </summary>
        public static int LaneWidth = 146;

        /// <summary>
        /// Distance between lanes.
        /// </summary>
        public static int LaneGap = 5;

        /// <summary>
        /// Height of a lane. Same as the Window Height.
        /// </summary>
        public static int LaneHeight = (int)GameWindowConfig.WindowHeight;

        /// <summary>
        /// X Coordinate of lane1 from the left.
        /// </summary>
        public static int LaneOneX = LaneMargin;

        /// <summary>
        /// X Coordinate of lane 2 from the left.
        /// </summary>
        public static int LaneTwoX = LaneMargin + LaneWidth + LaneGap;

        /// <summary>
        /// X Coordinate of lane 3 from the left;
        /// </summary>
        public static int LaneThreeX = LaneMargin + (2 * LaneWidth) + (2 * LaneGap);
    }
}
