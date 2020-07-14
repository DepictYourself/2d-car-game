//-----------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace TrafficRush
{
    using System;
    using System.Windows;

    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        private TRControl trControl;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            trControl = BuildControl();
            this.AddChild(trControl);
        }

        private TRControl BuildControl()
        {
            TRControl control = new TRControl();
            control.Width = Model.Config.GameWindowConfig.WindowWidth;
            control.Height = Model.Config.GameWindowConfig.WindowHeight;
            control.GameEnded += (inobj, inargs) => ReasignContent(inobj, inargs);
            return control;
        }

        private void ReasignContent(object obj, EventArgs args)
        {
            trControl = BuildControl();
            this.Content = trControl;
        }
    }
}
