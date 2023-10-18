using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ZoomAndPanSample
{
    public class CenteredCrossHairCanvas : Canvas
    {
        #region Public Fields

        public static readonly DependencyProperty HorizontalLinesProperty =
                DependencyProperty.Register("HorizontalLines", typeof(int), typeof(CenteredCrossHairCanvas), new PropertyMetadata(1, PropertyChangedCallback));

        public static readonly DependencyProperty ScaleProperty =
                DependencyProperty.Register("Scale", typeof(double), typeof(CenteredCrossHairCanvas), new PropertyMetadata(.95, PropertyChangedCallback));

        public static readonly DependencyProperty ShowProperty =
                DependencyProperty.Register("Show", typeof(bool), typeof(CenteredCrossHairCanvas), new PropertyMetadata(true, PropertyChangedCallback));

        public static readonly DependencyProperty StrokeBrushProperty =
            DependencyProperty.Register("StrokeBrush", typeof(Brush), typeof(CenteredCrossHairCanvas), new PropertyMetadata(new SolidColorBrush(Colors.Black), PropertyChangedCallback));

        public static readonly DependencyProperty StrokeDashStyleProperty =
                DependencyProperty.Register("StrokeDashStyle", typeof(DoubleCollection), typeof(CenteredCrossHairCanvas), new PropertyMetadata(new DoubleCollection { }, PropertyChangedCallback));

        public static readonly DependencyProperty StrokeThicknessProperty =
                DependencyProperty.Register("StrokeThickness", typeof(double), typeof(CenteredCrossHairCanvas), new PropertyMetadata(1.0, PropertyChangedCallback));

        public static readonly DependencyProperty VerticalLinesProperty =
                DependencyProperty.Register("VerticalLines", typeof(int), typeof(CenteredCrossHairCanvas), new PropertyMetadata(3, PropertyChangedCallback));

        #endregion Public Fields

        #region Public Constructors + Destructors

        static CenteredCrossHairCanvas()
        {
            IsHitTestVisibleProperty.OverrideMetadata(typeof(CenteredCrossHairCanvas), new FrameworkPropertyMetadata(false));
            BackgroundProperty.OverrideMetadata(typeof(CenteredCrossHairCanvas), new FrameworkPropertyMetadata(new SolidColorBrush(Colors.Transparent)));
        }

        #endregion Public Constructors + Destructors
        #region Public Properties

        public int HorizontalLines { get => (int)GetValue(HorizontalLinesProperty); set => SetValue(HorizontalLinesProperty, value); }
        public double Scale { get => (double)GetValue(ScaleProperty); set => SetValue(ScaleProperty, value); }
        public bool Show { get => (bool)GetValue(ShowProperty); set => SetValue(ShowProperty, value); }
        public Brush StrokeBrush { get => (Brush)GetValue(StrokeBrushProperty); set => SetValue(StrokeBrushProperty, value); }
        public DoubleCollection StrokeDashStyle { get => (DoubleCollection)GetValue(StrokeDashStyleProperty); set => SetValue(StrokeDashStyleProperty, value); }
        public double StrokeThickness { get => (double)GetValue(StrokeThicknessProperty); set => SetValue(StrokeThicknessProperty, value); }
        public int VerticalLines { get => (int)GetValue(VerticalLinesProperty); set => SetValue(VerticalLinesProperty, value); }

        #endregion Public Properties
        #region Protected Methods

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            Redraw();
        }

        #endregion Protected Methods

        #region Private Methods

        private static void PropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs) => (dependencyObject as CenteredCrossHairCanvas)?.Redraw();

        private void Redraw()
        {
            Children.Clear();
            if (!Show)
            {
                return;
            }

            try
            {
                if (Math.Abs(ActualHeight) < 1 || Math.Abs(ActualWidth) < 1)
                {
                    return;
                }

                for (int i = 1; i <= HorizontalLines; i++)
                {
                    Line horizontalLine = new Line
                    {
                        Stroke = StrokeBrush,
                        StrokeDashArray = StrokeDashStyle,
                        X1 = 0,
                        X2 = ActualWidth,
                        Y1 = (ActualHeight * i) / (HorizontalLines + 1),
                        Y2 = (ActualHeight * i) / (HorizontalLines + 1),
                        StrokeThickness = StrokeThickness / Scale,
                    };
                    Children.Add(horizontalLine);
                }
                for (int i = 1; i <= VerticalLines; i++)
                {
                    Line verticalLine = new Line
                    {
                        Stroke = StrokeBrush,
                        StrokeDashArray = StrokeDashStyle,
                        Y1 = 0,
                        Y2 = ActualHeight,
                        X1 = (ActualWidth * i) / (VerticalLines + 1),
                        X2 = (ActualWidth * i) / (VerticalLines + 1),
                        StrokeThickness = StrokeThickness / Scale,
                    };
                    Children.Add(verticalLine);
                }
            }
            catch { }
        }

        #endregion Private Methods
    }
}
