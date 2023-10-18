using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace ZoomAndPanSample
{
    /// <summary>
    /// Interaction logic for ZoomAndPanControlView.xaml
    /// </summary>
    public partial class ZoomAndPanControlView : UserControl
    {
        #region Private Fields

        /// <summary>
        /// Specifies the current state of the mouse handling logic.
        /// </summary>
        private bool _mouseDragging;

        /// <summary>
        /// The point that was clicked relative to the content that is contained
        /// within the ZoomAndPanControl.
        /// </summary>
        private Point _origContentMouseDownPoint;

        #endregion Private Fields

        #region Public Constructors + Destructors

        public ZoomAndPanControlView()
        {
            InitializeComponent();
            Loaded += (sender, eventArgs) => FocusManager.SetFocusedElement(this, ZoomAndPanControlExample);
            GotFocus += (sender, eventArgs) => Keyboard.Focus(ZoomAndPanControlExample);
        }

        #endregion Public Constructors + Destructors
        #region Private Methods

        /// <summary>
        /// Event raised when a mouse button is clicked down over a Rectangle.
        /// </summary>
        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            actualContent.Focus();
            Keyboard.Focus(actualContent);

            if (Keyboard.Modifiers == ZoomAndPanControlExample.MouseDragZoomModifier)
            {
                // When the shift key is held down special zooming logic is
                // executed in content_MouseDown, so don't handle mouse input here.
                return;
            }

            if (_mouseDragging)
            {
                return;
            }

            _mouseDragging = true;
            _origContentMouseDownPoint = e.GetPosition(actualContent);
            ((Rectangle)sender).CaptureMouse();
            e.Handled = true;
        }

        /// <summary>
        /// Event raised when the mouse cursor is moved when over a Rectangle.
        /// </summary>
        private void Rectangle_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_mouseDragging)
            {
                return;
            }

            Point curContentPoint = e.GetPosition(actualContent);
            Vector rectangleDragVector = curContentPoint - _origContentMouseDownPoint;

            // When in 'dragging rectangles' mode update the position of the
            // rectangle as the user drags it.

            _origContentMouseDownPoint = curContentPoint;

            Rectangle rectangle = (Rectangle)sender;
            Canvas.SetLeft(rectangle, Canvas.GetLeft(rectangle) + rectangleDragVector.X);
            Canvas.SetTop(rectangle, Canvas.GetTop(rectangle) + rectangleDragVector.Y);

            e.Handled = true;
        }

        /// <summary>
        /// Event raised when a mouse button is released over a Rectangle.
        /// </summary>
        private void Rectangle_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!_mouseDragging)
            {
                return;
            }

            _mouseDragging = false;
            ((Rectangle)sender).ReleaseMouseCapture();
            e.Handled = true;
        }

        #endregion Private Methods
    }
}
