# Pre-render Wasm Server-side
One way to improve the performance of Blazor WebAssembly apps is to pre-render the app on the server and then serve the pre-rendered HTML to the browser. This approach avoids the need to load the Blazor framework and the app's assemblies on the client. The pre-rendered HTML is displayed until the Blazor framework has loaded and the app starts up.
By adding a few lines of code to the app, you can pre-render the app on the server and then serve the pre-rendered HTML to the browser. 
The following sections describe how to add this code to an existing Blazor WebAssembly app.

When we create a new Blazor WebAssembly project with an ASP.NET Core backend we will get three different projects.
Client, Server, and Shared.
They are named TheNameOfOutProject.Server for example.
In the instructions below we will only call them by their suffix.

1. Add a new razor page to the server project and name it `_Host.cshtml`. 
```html
@page
@*<Add Taghelper and namespace to client project>*@
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
@using WasmServerPrerendered.Client
@*</Add Taghelper and namespace to client project>*@
    
<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <title>WasmServerPrerendered</title>
    <base href="/" />
    <link href="css/bootstrap/bootstrap.min.css" rel="stylesheet" />
    <link href="css/app.css" rel="stylesheet" />
    <link href="WasmServerPrerendered.Client.styles.css" rel="stylesheet" />
</head>

<body>
    @*<This part differs from the index.html file>*@
    <component type="typeof(App)" render-mode="WebAssemblyPrerendered" />
    @*</This part differs from the index.html file>*@
    <div id="blazor-error-ui">
        An unhandled error has occurred.
        <a href="" class="reload">Reload</a>
        <a class="dismiss">🗙</a>
    </div>
    <script src="_framework/blazor.webassembly.js"></script>
</body>

</html>

```
This file is the entry point for the application. 
It is the first page that is loaded when the application starts.
This file should look almost the same as the `wwwroot/index.html` file in the Client project.
2. In the server project we need to change the program.cs
```csharp
//<Change this>
//app.MapFallbackToFile("index.html");
//To this:
app.MapFallbackToPage("/_Host");
//</Change this>
```
3. In the Client project we need to change the Program.cs file 
```csharp
//<Remove these lines>
//builder.RootComponents.Add<App>("#app");
//builder.RootComponents.Add<HeadOutlet>("head::after");
//</Remove these lines>
```
The default behaviour is that Blazor hooks up the div tag with id "app" to render the WebAssembly.
But since we replaced the html file with a Razor page that is no longer needed.

We are now all set.
The page will be prerendered on the server and then replaced with the WebAssembly version once it's done loading.

This is not a problem free solution, it will load the infomation on the server, render the contet and sent it to the browser.
Once the WebAssembly is finished loading it will also get the content and render the page.
If you are getting data from a database for example this will happen twice.
There are ways to avoid that as well by using ```<persist-component-state />``` this is a separate demo.