// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScoreboardControlPanelView.xaml.cs" company="Starboard">
//   Copyright © 2011 All Rights Reserved
// </copyright>
// <summary>
//   View for the scoreboard control panel, which has all the main controls for the scoreboard.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Starboard.View
{
    using Starboard.ViewModel;
    using System;
    using System.Collections.Specialized;
    using System.IO;
    using System.Windows;
    using System.Windows.Input;

    /// <summary> Interaction logic for ScoreboardControlPanelView.xaml </summary>
    public partial class ScoreboardControlPanelView
    {
        #region Constructors and Destructors

        /// <summary>
        /// Reference to the timer for updating the XSplit output.
        /// </summary>
        //private readonly TimedBroadcasterPlugin plugin;

        /// <summary> Initializes a new instance of the <see cref="ScoreboardControlPanelView"/> class. </summary>
        public ScoreboardControlPanelView()
        {
            this.InitializeComponent();

            DataContextChanged += ScoreboardControlPanelView_DataContextChanged;

           
        }

        void ScoreboardControlPanelView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext != null)
            {
                ((ScoreboardControlPanelViewModel)DataContext).ImageSource.Visual = contentView;
            }
        }

        #endregion
    }
}