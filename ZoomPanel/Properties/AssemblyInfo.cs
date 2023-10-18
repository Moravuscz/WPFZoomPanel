using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Markup;

// XML Namespace Definitions
[assembly: XmlnsPrefix("https://github.com/Moravuscz/WPFZoomPanel", "WPFZoomPanel")]
[assembly: XmlnsDefinition("https://github.com/Moravuscz/WPFZoomPanel", "Moravuscz.WPFZoomPanel")]
[assembly: XmlnsDefinition("https://github.com/Moravuscz/WPFZoomPanel", "Moravuscz.WPFZoomPanel.Annotations")]
[assembly: XmlnsDefinition("https://github.com/Moravuscz/WPFZoomPanel", "Moravuscz.WPFZoomPanel.Commands")]
[assembly: XmlnsDefinition("https://github.com/Moravuscz/WPFZoomPanel", "Moravuscz.WPFZoomPanel.Converters")]
[assembly: XmlnsDefinition("https://github.com/Moravuscz/WPFZoomPanel", "Moravuscz.WPFZoomPanel.Enums")]
[assembly: XmlnsDefinition("https://github.com/Moravuscz/WPFZoomPanel", "Moravuscz.WPFZoomPanel.Events")]
[assembly: XmlnsDefinition("https://github.com/Moravuscz/WPFZoomPanel", "Moravuscz.WPFZoomPanel.Helpers")]

// General Information about an assembly is controlled through the following set
// of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("ZoomPanel")]
[assembly: AssemblyDescription("An Enhanced WPF Custom Control for Zooming and Panning by Clifford Nelson, modified by Moravuscz")]
[assembly: AssemblyProduct("ZoomPanel")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyCopyright("Clifford Nelson & Moravuscz")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: AssemblyConfiguration("")]

// Setting ComVisible to false makes the types in this assembly not visible to
// COM components. If you need to access a type in this assembly from COM, set
// the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("b5cc1c1d-98b7-46a8-9419-567b5f752b66")]
[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
                                     //(used if a resource is not found in the page,
                                     // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
                                              //(used if a resource is not found in the page,
                                              // app, or any theme specific resource dictionaries)
)]

// Version information for an assembly consists of the following four values:
//
// Major Version Minor Version Build Number Revision
//
// You can specify all the values or you can default the Build and Revision
// Numbers by using the '*' as shown below: [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.8551.14301")]
[assembly: AssemblyInformationalVersion("1.0.0.0")]
