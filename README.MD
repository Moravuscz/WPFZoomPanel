# ZoomPanel: A Custom WPF Zoom and Pan Control
![IDE = Visual Studio 2019](https://img.shields.io/badge/IDE-Visual%20Studio%202019-blue)
![Language = C#](https://img.shields.io/badge/Language-C%23-blue)
![dotNet Framework 4.8](https://img.shields.io/badge/.NET-Framework%204.8-blue)
![dotNet 6.0 Windows Desktop](https://img.shields.io/badge/.NET-6.0--windows-blue)  
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

1. Install the package from NuGet    
[![Nuget](https://img.shields.io/nuget/v/Moravuscz.WPFZoomPanel?logo=nuget)](https://www.nuget.org/packages/Moravuscz.WPFZoomPanel)  
or download the packages or binaries from [Releases page](https://github.com/Moravuscz/WPFZoomPanel/releases)
2. Add `xmlns:WPFZoomPanel="https://github.com/Moravuscz/WPFZoomPanel"` to your XAML Window definition
3. Use `<WPFZoomPanel:ZoomPanel>` or `</WPFZoomPanel:ZoomPanelScrollViewer>` for whatever *content* you want to be zoom/pan-able.
4. Add `<WPFZoomPanel:ZoomPanelViewBox>` to  add an interactive minimap for the ZoomPanel.

For more details see [Wiki](https://github.com/Moravuscz/WPFZoomPanel/wiki)
