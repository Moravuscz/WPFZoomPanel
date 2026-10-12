using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Moravuscz.WPFZoomPanel.Enums;
using Moravuscz.WPFZoomPanel.Helpers;

namespace Moravuscz.WPFZoomPanel
{
    /// <summary>
    /// A class that wraps up zooming and panning of it's content.
    /// </summary>
    public class ZoomPanelMinimap : ContentControl
    {
        #region Public Fields

        public static readonly DependencyProperty VisualProperty = DependencyProperty.Register("Visual",
                    typeof(FrameworkElement), typeof(ZoomPanelMinimap), new FrameworkPropertyMetadata(null, OnVisualChanged));

        #endregion Public Fields

        #region Private Fields

        /// <summary>
        /// The control for creating a drag border
        /// </summary>
        private Border _dragBorder;

        /// <summary>
        /// Specifies the current state of the mouse handling logic.
        /// </summary>
        private MouseHandlingMode _mouseHandlingMode = MouseHandlingMode.None;

        /// <summary>
        /// The point that was clicked relative to the content that is contained
        /// within the ZoomAndPanControl.
        /// </summary>
        private Point _origContentMouseDownPoint;

        /// <summary>
        /// The control for creating a drag border
        /// </summary>
        private Border _sizingBorder;

        /// <summary>
        /// The control for containing a zoom border
        /// </summary>
        private Canvas _viewportCanvas;

        /// <summary>
        /// Visual currently mirrored into the minimap background.
        /// </summary>
        private FrameworkElement _backgroundVisual;

        /// <summary>
        /// Brush that paints the mirrored visual onto PART_Content.
        /// </summary>
        private VisualBrush _backgroundBrush;

        /// <summary>
        /// Kept so SetBackground can unsubscribe before re-subscribing.
        /// </summary>
        private SizeChangedEventHandler _backgroundSizeChangedHandler;

        #endregion Private Fields

        #region Public Constructors + Destructors

        /// <summary>
        /// Static constructor to define metadata for the control (and link it
        /// to the style in Generic.xaml).
        /// </summary>
        static ZoomPanelMinimap() => DefaultStyleKeyProperty.OverrideMetadata(typeof(ZoomPanelMinimap), new FrameworkPropertyMetadata(typeof(ZoomPanelMinimap)));

        #endregion Public Constructors + Destructors

        #region Public Properties

        /// <summary>
        /// The X coordinate of the content focus, this is the point that we are
        /// focusing on when zooming.
        /// </summary>
        public FrameworkElement Visual
        {
            get => (FrameworkElement)GetValue(VisualProperty);
            set => SetValue(VisualProperty, value);
        }

        #endregion Public Properties

        #region Public Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _dragBorder = Template.FindName("PART_DraggingBorder", this) as Border;
            _sizingBorder = Template.FindName("PART_SizingBorder", this) as Border;
            _viewportCanvas = Template.FindName("PART_Content", this) as Canvas;
            SetBackground(Visual);
        }

        #endregion Public Methods

        #region Protected Methods

        /// <summary>
        /// Event raised with the double click command
        /// </summary>
        protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
        {
            base.OnMouseDoubleClick(e);

            if ((Keyboard.Modifiers & ModifierKeys.Shift) == 0)
            {
                ZoomPanel zoomAndPanControl = GetZoomAndPanControl();
                zoomAndPanControl.SaveZoom();
                zoomAndPanControl.AnimatedSnapTo(e.GetPosition(_viewportCanvas));
            }
        }

        /// <summary>
        /// Event raised on mouse down in the ZoomAndPanControl.
        /// </summary>
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            GetZoomAndPanControl().SaveZoom();
            _mouseHandlingMode = MouseHandlingMode.DragPanning;
            _origContentMouseDownPoint = e.GetPosition(_viewportCanvas);

            if ((Keyboard.Modifiers & ModifierKeys.Shift) != 0)
            {
                // Shift + left- or right-down initiates zooming mode.
                _mouseHandlingMode = MouseHandlingMode.DragZooming;
                _dragBorder.Visibility = Visibility.Hidden;
                _sizingBorder.Visibility = Visibility.Visible;
                Canvas.SetLeft(_sizingBorder, _origContentMouseDownPoint.X);
                Canvas.SetTop(_sizingBorder, _origContentMouseDownPoint.Y);
                _sizingBorder.Width = 0;
                _sizingBorder.Height = 0;
            }
            else
            {
                // Just a plain old left-down initiates panning mode.
                _mouseHandlingMode = MouseHandlingMode.DragPanning;
            }

            if (_mouseHandlingMode != MouseHandlingMode.None)
            {
                // Capture the mouse so that we eventually receive the mouse up event.
                _viewportCanvas.CaptureMouse();
                e.Handled = true;
            }
        }

        /// <summary>
        /// Event raised on mouse move in the ZoomAndPanControl.
        /// </summary>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (_mouseHandlingMode == MouseHandlingMode.DragPanning)
            {
                Point curContentPoint = e.GetPosition(_viewportCanvas);
                Vector rectangleDragVector = curContentPoint - _origContentMouseDownPoint;
                // When in 'dragging rectangles' mode update the position of the
                // rectangle as the user drags it.
                _origContentMouseDownPoint = e.GetPosition(_viewportCanvas).Clamp();
                Canvas.SetLeft(_dragBorder, Canvas.GetLeft(_dragBorder) + rectangleDragVector.X);
                Canvas.SetTop(_dragBorder, Canvas.GetTop(_dragBorder) + rectangleDragVector.Y);
            }
            else if (_mouseHandlingMode == MouseHandlingMode.DragZooming)
            {
                Point curContentPoint = e.GetPosition(_viewportCanvas);
                Rect rect = ViewportHelpers.Clip(curContentPoint, _origContentMouseDownPoint, new Point(0, 0), new Point(_viewportCanvas.Width, _viewportCanvas.Height));
                ViewportHelpers.PositionBorderOnCanvas(_sizingBorder, rect);
            }

            e.Handled = true;
        }

        /// <summary>
        /// Event raised on mouse up in the ZoomAndPanControl.
        /// </summary>
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            if (_mouseHandlingMode == MouseHandlingMode.DragZooming)
            {
                ZoomPanel zoomAndPanControl = GetZoomAndPanControl();
                Point curContentPoint = e.GetPosition(_viewportCanvas);
                Rect rect = ViewportHelpers.Clip(curContentPoint, _origContentMouseDownPoint, new Point(0, 0),
                    new Point(_viewportCanvas.Width, _viewportCanvas.Height));
                zoomAndPanControl.AnimatedZoomTo(rect);
                _dragBorder.Visibility = Visibility.Visible;
                _sizingBorder.Visibility = Visibility.Hidden;
            }
            _mouseHandlingMode = MouseHandlingMode.None;
            _viewportCanvas.ReleaseMouseCapture();
            e.Handled = true;
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            if (ActualWidth > 0 && _viewportCanvas != null)
            {
                _sizingBorder.BorderThickness = _dragBorder.BorderThickness = new Thickness(
                   _viewportCanvas.ActualWidth / ActualWidth * BorderThickness.Left,
                   _viewportCanvas.ActualWidth / ActualWidth * BorderThickness.Top,
                   _viewportCanvas.ActualWidth / ActualWidth * BorderThickness.Right,
                   _viewportCanvas.ActualWidth / ActualWidth * BorderThickness.Bottom);
            }
        }

        #endregion Protected Methods

        #region Private Methods

        private static void OnVisualChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ZoomPanelMinimap c = (ZoomPanelMinimap)d;
            c.SetBackground(e.NewValue as FrameworkElement);
        }

        private ZoomPanel GetZoomAndPanControl()
        {
            ZoomPanel zoomAndPanControl = (DataContext as ZoomPanel) ??
                                    (DataContext as ZoomPanelScrollViewer)?.ZoomAndPanContent;
            if (zoomAndPanControl == null)
            {
                throw new NullReferenceException("DataContext is not of type ZoomAndPanControl");
            }

            return zoomAndPanControl;
        }

        private void SetBackground(FrameworkElement frameworkElement)
        {
            if (_backgroundVisual != null && _backgroundSizeChangedHandler != null)
            {
                _backgroundVisual.SizeChanged -= _backgroundSizeChangedHandler;
            }

            frameworkElement = frameworkElement ?? (DataContext as ContentControl)?.Content as FrameworkElement;
            _backgroundVisual = frameworkElement;
            if (frameworkElement == null)
            {
                _backgroundBrush = null;
                return;
            }

            _backgroundBrush = new VisualBrush
            {
                Visual = frameworkElement,
                ViewboxUnits = BrushMappingMode.RelativeToBoundingBox,
                ViewportUnits = BrushMappingMode.RelativeToBoundingBox,
                TileMode = TileMode.None,
                Stretch = Stretch.Fill
            };

            // Visual may be bound before OnApplyTemplate; SizeChanged can fire while PART_Content is still null.
            _backgroundSizeChangedHandler = OnBackgroundVisualSizeChanged;
            frameworkElement.SizeChanged += _backgroundSizeChangedHandler;
            ApplyBackgroundFromVisual();
        }

        private void OnBackgroundVisualSizeChanged(object sender, SizeChangedEventArgs e)
        {
            ApplyBackgroundFromVisual();
        }

        private void ApplyBackgroundFromVisual()
        {
            if (_viewportCanvas == null || _backgroundVisual == null || _backgroundBrush == null)
            {
                return;
            }

            _viewportCanvas.Height = _backgroundVisual.ActualHeight;
            _viewportCanvas.Width = _backgroundVisual.ActualWidth;
            _viewportCanvas.Background = _backgroundBrush;
        }

        #endregion Private Methods
    }
}
