using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Moravuscz.WPFZoomPanel.Converters
{
    public class ZoomPanelMinimapClampConverter : MarkupExtension, IMultiValueConverter
    {
        #region Public Methods

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            //NOTE: Cannot pass ExtentWidth or ExtentHeight as one of the values because it does not seem to update
            ZoomPanel zoomAndPanControl = (ZoomPanel)values[3];
            if (values[0] == null || zoomAndPanControl == null)
            {
                return DependencyProperty.UnsetValue;
            }

            double size = (double)values[0];
            double offset = (double)values[1];
            double zoom = (double)values[2];
            return Math.Max((parameter?.ToString().ToLower() == "width")
                 ? Math.Min(zoomAndPanControl.ExtentWidth / zoom - offset, size)
                 : Math.Min(zoomAndPanControl.ExtentHeight / zoom - offset, size), 0);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotImplementedException();

        public override object ProvideValue(IServiceProvider serviceProvider) => this;

        #endregion Public Methods
    }
}
