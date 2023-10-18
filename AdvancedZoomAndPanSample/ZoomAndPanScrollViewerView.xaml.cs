using System.Collections.ObjectModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ZoomAndPanSample
{
    /// <summary>
    /// Interaction logic for ZoomAndPanScrollViewerView.xaml
    /// </summary>
    public partial class ZoomAndPanScrollViewerView : UserControl
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

        public ZoomAndPanScrollViewerView()
        {
            InitializeComponent();
            Loaded += (sender, eventArgs) => FocusManager.SetFocusedElement(this, ZoomAndPanScrollViewerExample.ZoomAndPanContent);
            GotFocus += (sender, eventArgs) => Keyboard.Focus(ZoomAndPanScrollViewerExample.ZoomAndPanContent);
            
            Rectangles = new ObservableCollection<Tuple<Rect, Color>>
            {
                new Tuple<Rect, Color>(new Rect(50,25,50,25), Colors.Blue ),
                new Tuple<Rect, Color>(new Rect(150,100,100,50), Colors.Aqua ),
            };
        }

        #endregion Public Constructors + Destructors

        #region Public Properties

        public ObservableCollection<Tuple<Rect, Color>> Rectangles
        {
            get;
            private set;
        }

        #endregion Public Properties

        #region Private Methods

        /// <summary>
        /// Event raised when a mouse button is clicked down over a Rectangle.
        /// </summary>
        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            actualContent.Focus();
            Keyboard.Focus(actualContent);

            if ((Keyboard.Modifiers & ModifierKeys.Shift) != 0)
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
