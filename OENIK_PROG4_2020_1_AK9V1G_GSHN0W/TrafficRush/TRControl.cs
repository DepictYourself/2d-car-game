//-----------------------------------------------------------------------
// <copyright file="TRControl.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace TrafficRush
{
    using System;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Threading;
    using TrafficRush.Logic;
    using TrafficRush.Model;
    using TrafficRush.Model.game_objects;
    using TrafficRush.Repository;

    /// <summary>
    /// This class controls the game,
    /// holds references to logic, model, renderer.
    /// Also uses a DispatchTimer to sequentialy call certain methods which make the game work.
    /// </summary>
    public class TRControl : FrameworkElement
    {
        private ILogic logic;
        private IModel model;
        private TRRenderer renderer;
        private IRepository repository;
        private DispatcherTimer tickTimer;

        /// <summary>
        /// Initializes a new instance of the <see cref="TRControl"/> class.
        /// </summary>
        public TRControl()
        {
            Loaded += TRControl_Loaded;
        }

        /// <summary>
        /// Event for end of the game.
        /// </summary>
        public event EventHandler GameEnded;

        /// <summary>
        /// Event for game pause. Which happens if you hit enter.
        /// </summary>
        public event EventHandler Paused;

        /// <summary>
        /// Contains rendering functionality.
        /// </summary>
        /// <param name="drawingContext">Board to draw the game.</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            if (renderer != null)
            {
                renderer.DrawModelUsingContext(drawingContext);
            }
        }

        private void TRControl_Loaded(object sender, RoutedEventArgs e)
        {
            model = new TRModel();
            model.InitalizeTraffic();
            repository = new TRRepository(model);
            logic = new TRLogic(model, repository, Dispatcher);
            renderer = new TRRenderer(model);

            Window win = Window.GetWindow(this);
            if (win != null)
            {
                tickTimer = new DispatcherTimer();
                tickTimer.Interval = TimeSpan.FromMilliseconds(30);
                tickTimer.Tick += TickTimer_Tick;
                tickTimer.Start();
                logic.SpawnTraffic();

                win.KeyDown += Win_KeyDown;
            }

            logic.RefreshScreen += (obj, args) => InvalidateVisual();
            logic.GameEnded += (obj, args) => GameEnd(obj, args);
            InvalidateVisual();
        }

        private void GameEnd(object obj, EventArgs args)
        {
            double newScore = this.model.Score;
            double highscore = logic.GetHighScore();
            tickTimer.Stop();
            GameOverViewModel vm = new GameOverViewModel()
            {
                NewScore = (int)(newScore > highscore ? newScore : highscore),
                Message = newScore > highscore ? "New Highscore!" : "Nice, but the highscore is",
            };
            GameOverWindow gameOver = new GameOverWindow(vm);
            if (gameOver.ShowDialog() == true)
            {
                this.GameEnded?.Invoke(this, EventArgs.Empty);
            }
        }

        private void Win_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left || e.Key == Key.A)
            {
                logic.ChangeLane(model.Player, Direction.LEFT);
            }
            else if (e.Key == Key.Right || e.Key == Key.D)
            {
                logic.ChangeLane(model.Player, Direction.RIGHT);
            }
            else if (e.Key == Key.Escape)
            {
                tickTimer.IsEnabled = false;
                PauseWindowViewModel pauseVM = new PauseWindowViewModel(logic);
                PauseWindow pauseWindow = new PauseWindow(pauseVM);
                if (pauseWindow.ShowDialog() == true)
                {
                }
                tickTimer.Start();
            }
            else if (e.Key == Key.Space)
            {
                logic.Shoot();
            }
        }

        private void TickTimer_Tick(object sender, EventArgs e)
        {
            logic.MoveTraffic();
            logic.ChangeExplosionStates();
        }
    }
}
