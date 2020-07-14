//-----------------------------------------------------------------------
// <copyright file="TRRenderer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace TrafficRush
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using TrafficRush.Model;
    using TrafficRush.Model.Config;
    using TrafficRush.Model.game_objects;
    using Brush = System.Windows.Media.Brush;
    using Brushes = System.Windows.Media.Brushes;
    using Point = System.Windows.Point;

    /// <summary>
    /// This class provides public functionality to draw the state of the game.
    /// </summary>
    public class TRRenderer
    {
        private const string IMG_ROOT_PATH = "TrafficRush.Images";
        private IModel model;
        private Rect background = new Rect(0, 0, GameWindowConfig.WindowWidth, GameWindowConfig.WindowHeight);
        private FormattedText formattedText;
        private int oldScoreValue = -1;

        private ImageBrush backgroundBrush;

        private ImageBrush playerCarBrush;

        private ImageBrush policeCarBrush;

        private Rect road;
        private ImageBrush roadBrush;

        private ImageBrush trafficCarBrush;

        private ImageBrush playerBulletBrush;

        private ImageBrush enemyBulletBrush;

        private ImageBrush enemyCarBrush;

        private Dictionary<int, ImageBrush> explosionBrushDictionary;

        private Typeface scoreFontType = new Typeface("Arial");
        private Point scoreTextLocation = new Point(20, 20);
        private Brush scoreTextColor = Brushes.Black;

        /// <summary>
        /// Initializes a new instance of the <see cref="TRRenderer"/> class.
        /// Contructur.
        /// </summary>
        /// <param name="model">Needs the gameModel as an argument.</param>
        public TRRenderer(IModel model)
        {
            this.model = model;

            backgroundBrush = GetImageBrush(IMG_ROOT_PATH + ".grass.jpg");
            roadBrush = GetImageBrush(IMG_ROOT_PATH + ".road.jpg");
            playerCarBrush = GetImageBrush(IMG_ROOT_PATH + ".player.png");
            policeCarBrush = GetImageBrush(IMG_ROOT_PATH + ".police.png");
            trafficCarBrush = GetImageBrush(IMG_ROOT_PATH + ".car1.png");
            enemyCarBrush = GetImageBrush(IMG_ROOT_PATH + ".enemy.png");
            playerBulletBrush = GetImageBrush(IMG_ROOT_PATH + ".rocket.png");
            enemyBulletBrush = GetImageBrush(IMG_ROOT_PATH + ".enemyRocket.png");
            backgroundBrush.TileMode = TileMode.Tile;
            backgroundBrush.Stretch = Stretch.None;
            backgroundBrush.Viewport = new Rect(0, 0, 152, 152);
            backgroundBrush.ViewportUnits = BrushMappingMode.Absolute;
            road = new Rect(
                GameWindowConfig.LaneOneX - GameWindowConfig.LaneGap,
                0,
                (GameWindowConfig.LaneWidth * 3) + (GameWindowConfig.LaneGap * 2),
                GameWindowConfig.LaneHeight);
            explosionBrushDictionary = new Dictionary<int, ImageBrush>();
            InitExplosionBrushes();
        }

        /// <summary>
        /// Draws the model objects on the DrawingContext in order.
        /// </summary>
        /// <param name="context">Canvas to draw the game's shapes on as DrawingContext object.</param>
        public void DrawModelUsingContext(DrawingContext context)
        {
            this.DrawBackground(context);
            this.DrawRoad(context);
            this.DrawTraffic(context);
            this.DrawPlayerBullets(context);
            this.DrawPlayer(context);
            this.DrawExplosions(context);
            this.DrawEnemy(context);
            this.DrawText(context);
        }

        private void DrawExplosions(DrawingContext context)
        {
            foreach (var explosion in this.model.Explosions)
            {
                context.DrawRectangle(this.explosionBrushDictionary[explosion.State], null, explosion.Area);
            }
        }

        private void DrawBackground(DrawingContext context)
        {
            context.DrawRectangle(this.backgroundBrush, null, this.background);
        }

        private ImageBrush GetImageBrush(string imgfileName)
        {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = Assembly.GetExecutingAssembly().GetManifestResourceStream(imgfileName);
            bitmapImage.EndInit();

            return new ImageBrush(bitmapImage);
        }

        private void DrawRoad(DrawingContext context)
        {
            context.DrawRectangle(this.roadBrush, null, this.road);
        }

        private void DrawEnemy(DrawingContext context)
        {
            context.DrawRectangle(this.enemyCarBrush, null, this.model.Enemy.Area);
        }

        private void DrawTraffic(DrawingContext context)
        {
            foreach (Car car in this.model.Traffic)
            {
                if (car.GetType() == typeof(PoliceCar))
                {
                    context.DrawRectangle(this.policeCarBrush, null, car.Area);
                }
                else if (car.GetType() == typeof(TrafficCar))
                {
                    context.DrawRectangle(this.trafficCarBrush, null, car.Area);
                }
            }
        }

        private void DrawPlayer(DrawingContext context)
        {
            context.DrawRectangle(this.playerCarBrush, null, this.model.Player.Area);
        }

        private void DrawPlayerBullets(DrawingContext context)
        {
            lock (model.bulletLockObject)
            {
                foreach (Bullet bullet in this.model.Bullets)
                {
                    if (bullet.Source.GetType() == typeof(PlayerCar))
                    {
                        context.DrawRectangle(this.playerBulletBrush, null, bullet.Area);
                    }
                    else
                    {
                        context.DrawRectangle(this.enemyBulletBrush, null, bullet.Area);
                    }
                }
            }
        }

        private void DrawText(DrawingContext context)
        {
            if (oldScoreValue != model.Score)
            {
                formattedText = new FormattedText(Math.Round(model.Score).ToString(),
                                                  System.Globalization.CultureInfo.CurrentCulture,
                                                  FlowDirection.LeftToRight,
                                                  scoreFontType,
                                                  18,
                                                  scoreTextColor);
            }

            context.DrawText(formattedText, scoreTextLocation);
        }

        private void InitExplosionBrushes()
        {
            for (int i = 0; i < 7; i++)
            {
                this.explosionBrushDictionary.Add(i, GetImageBrush(IMG_ROOT_PATH + ".explosion" + i + ".png"));
            }
        }
    }
}
