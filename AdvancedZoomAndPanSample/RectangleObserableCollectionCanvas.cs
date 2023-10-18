using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ZoomAndPanSample
{
    public class RectangleObserableCollectionCanvas : Canvas
    {
        #region Public Fields

        public static readonly DependencyProperty RectanglesProperty =
                DependencyProperty.Register("Rectangles", typeof(ObservableCollection<Tuple<Rect, Color>>),
                    typeof(RectangleObserableCollectionCanvas), new PropertyMetadata(null, ObservableCollectionChangedCallback));

        public static readonly DependencyProperty ScaleProperty =
                DependencyProperty.Register("Scale", typeof(double), typeof(RectangleObserableCollectionCanvas), new PropertyMetadata(.95, PropertyChangedCallback));

        public static readonly DependencyProperty ShowProperty =
                DependencyProperty.Register("Show", typeof(bool), typeof(RectangleObserableCollectionCanvas), new PropertyMetadata(true, PropertyChangedCallback));

        public static readonly DependencyProperty StrokeDashStyleProperty =
                DependencyProperty.Register("StrokeDashStyle", typeof(DoubleCollection), typeof(RectangleObserableCollectionCanvas), new PropertyMetadata(new DoubleCollection { }, PropertyChangedCallback));

        public static readonly DependencyProperty StrokeThicknessProperty =
                DependencyProperty.Register("StrokeThickness", typeof(double), typeof(RectangleObserableCollectionCanvas), new PropertyMetadata(1.0, PropertyChangedCallback));

        #endregion Public Fields

        #region Public Constructors + Destructors

        static RectangleObserableCollectionCanvas()
        {
            IsHitTestVisibleProperty.OverrideMetadata(typeof(RectangleObserableCollectionCanvas), new FrameworkPropertyMetadata(false));
            BackgroundProperty.OverrideMetadata(typeof(RectangleObserableCollectionCanvas), new FrameworkPropertyMetadata(new SolidColorBrush(Colors.Transparent)));
        }

        #endregion Public Constructors + Destructors
        #region Public Properties

        public ObservableCollection<Tuple<Rect, Color>> Rectangles { get => (ObservableCollection<Tuple<Rect, Color>>)GetValue(RectanglesProperty); set => SetValue(RectanglesProperty, value); }
        public double Scale { get => (double)GetValue(ScaleProperty); set => SetValue(ScaleProperty, value); }
        public bool Show { get => (bool)GetValue(ShowProperty); set => SetValue(ShowProperty, value); }
        public DoubleCollection StrokeDashStyle { get => (DoubleCollection)GetValue(StrokeDashStyleProperty); set => SetValue(StrokeDashStyleProperty, value); }
        public double StrokeThickness { get => (double)GetValue(StrokeThicknessProperty); set => SetValue(StrokeThicknessProperty, value); }

        #endregion Public Properties
        #region Protected Methods

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            Redraw();
        }

        #endregion Protected Methods

        #region Private Methods

        private static void ObservableCollectionChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            RectangleObserableCollectionCanvas rectangleObserableCollectionCanvas = (RectangleObserableCollectionCanvas)dependencyObject;
            rectangleObserableCollectionCanvas.Rectangles.CollectionChanged += (s, e) =>
                rectangleObserableCollectionCanvas?.Redraw();
            rectangleObserableCollectionCanvas?.Redraw();
        }

        private static void PropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs) => (dependencyObject as RectangleObserableCollectionCanvas)?.Redraw();

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

                if (!(Rectangles is null))
                {
                    foreach (Tuple<Rect, Color> rectangleProperties in Rectangles)
                    {
                        Rectangle rectangle = new Rectangle
                        {
                            Stroke = new SolidColorBrush(rectangleProperties.Item2),
                            HorizontalAlignment = HorizontalAlignment.Left,
                            VerticalAlignment = VerticalAlignment.Top,
                            StrokeDashArray = StrokeDashStyle,
                            Width = rectangleProperties.Item1.Width,
                            Height = rectangleProperties.Item1.Height,
                            StrokeThickness = StrokeThickness / Scale,
                        };
                        SetLeft(rectangle, rectangleProperties.Item1.Left);
                        SetTop(rectangle, rectangleProperties.Item1.Top);
                        _ = Children.Add(rectangle);
                    }
                }
            }
            catch { }
        }

        #endregion Private Methods
    }
}
