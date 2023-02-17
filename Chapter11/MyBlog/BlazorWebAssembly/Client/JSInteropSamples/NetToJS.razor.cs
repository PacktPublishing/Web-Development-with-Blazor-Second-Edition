using System.Runtime.InteropServices.JavaScript;
namespace BlazorWebAssembly.Client.JSInteropSamples;
public partial class NetToJS
{
    [JSImport("showAlert", "nettojs")]
    internal static partial string ShowAlert(string message);
}
