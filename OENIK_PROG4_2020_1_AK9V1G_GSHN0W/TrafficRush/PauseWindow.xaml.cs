//-----------------------------------------------------------------------
// <copyright file="PauseWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace TrafficRush
{
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    /// Interaction logic for PauseWindow.xaml.
    /// </summary>
    public partial class PauseWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PauseWindow"/> class.
        /// </summary>
        public PauseWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PauseWindow"/> class.
        /// </summary>
        /// <param name="vm">ViewModel reference.</param>
        public PauseWindow(PauseWindowViewModel vm)
            : this()
        {
            this.DataContext = vm;
            KeyDown += PauseWindow_KeyDown;
        }

        private void PauseWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                DialogResult = true;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as PauseWindowViewModel).SaveAction();
            DialogResult = true;
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as PauseWindowViewModel).LoadAction();
            DialogResult = true;
        }
    }
}
