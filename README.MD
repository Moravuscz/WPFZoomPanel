# ZoomPanel: A Custom WPF Zoom and Pan Control

Based on Clifford Nelson's [Enhanced WPF Custom Control for Zooming and Panning](https://www.codeproject.com/Articles/1119476/An-Enhanced-WPF-Custom-Control-for-Zooming-and-Pan)

## Main Changes

- Added option for panning the view with mouse wheel up/down
  - Support for horizontal wheel scrolling / mouse wheel tilt
- Fully customizable mouse and keyboard controls
- Refactored and cleaned up code
  - Renamed the namespace & control - for easier differentiation from the original control
  - Fixed some bugs
- XAML namespace definitions and prefix for ease of integration

## Installation & usage

1. Install the package from NuGet, or download the latest [.nupkg](https://github.com/Moravuscz/WPFZoomPanel/releases/latest/download/WpfZoomPanel.nupkg) or [compiled binaries](https://github.com/Moravuscz/WPFZoomPanel/releases/latest/download/WpfZoomPanel.zip) from [Releases page](https://github.com/Moravuscz/WPFZoomPanel/releases)
2. Add `xmlns:WPFZoomPanel="https://github.com/Moravuscz/WPFZoomPanel"` to your XAML Window definition
3. Use `<WPFZoomPanel:ZoomPanel>` or `</WPFZoomPanel:ZoomPanelScrollViewer>` for whatever *content* you want to be zoom/pan-able.
4. Add `<WPFZoomPanel:ZoomPanelViewBox>` to  add an interactive minimap for the ZoomPanel.

For more details see [Wiki](https://github.com/Moravuscz/WPFZoomPanel/wiki) or play around with the included [AdvancedZoomAndPanSample](https://github.com/Moravuscz/WPFZoomPanel/releases/latest/download/AdvancedZoomAndPanSample.zip) project.
