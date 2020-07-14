//-----------------------------------------------------------------------
// <copyright file="GameOverWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace TrafficRush
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for GameOverWindow.xaml.
    /// </summary>
    public partial class GameOverWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameOverWindow"/> class.
        /// </summary>
        public GameOverWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameOverWindow"/> class.
        /// </summary>
        /// <param name="vm">Requires a ViewModel which will be set as DataContext for this window.
        /// The ViewModel's properties will be read one time.</param>
        public GameOverWindow(GameOverViewModel vm)
            : this()
        {
            this.DataContext = vm;
        }

        private void NewGameClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
