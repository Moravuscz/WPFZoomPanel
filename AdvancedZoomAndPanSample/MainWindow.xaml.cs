using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace ZoomAndPanSample
{
    /// <summary>
    /// This is a Window that uses ZoomAndPanControl to zoom and pan around some
    /// content. This demonstrates how to use application specific mouse
    /// handling logic with ZoomAndPanControl.
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Public Constructors + Destructors

        public MainWindow() => InitializeComponent();

        #endregion Public Constructors + Destructors

        #region Private Methods

        /// <summary>
        /// Event raised when the Window has loaded.
        /// </summary>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            HelpTextWindow helpTextWindow = new HelpTextWindow
            {
                Left = Left + Width + 5,
                Top = Top,
                Owner = this
            };
            helpTextWindow.Show();
        }

        #endregion Private Methods
    }
}
