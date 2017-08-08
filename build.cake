#tool nuget:?package=NUnit.Runners&version=2.6.4

// Input args
string target = Argument("target", "Default");
string configuration = Argument("configuration", "Release");

// Define vars
var dirs = new[] 
{
    Directory("./build"),
    Directory("./Xamarin.Forms.Mocks/bin") + Directory(configuration),
    Directory("./Xamarin.Forms.Mocks.Tests/bin") + Directory(configuration),
    Directory("./Xamarin.Forms.Mocks.Xaml/bin") + Directory(configuration),
};
string sln = "./Xamarin.Forms.Mocks.sln";
string version = "2.3.5.1-pre6";

Task("Clean")
    .Does(() =>
    {
        foreach (var dir in dirs)
            CleanDirectory(dir);
    });

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
    {
        NuGetRestore(sln);
    });

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
    {
        if(IsRunningOnWindows())
        {
            MSBuild(sln, settings =>
                settings
                    //.WithProperty("Platform", new[] { "iPhoneSimulator" })
                    .SetConfiguration(configuration));
        }
        else
        {
            XBuild(sln, settings =>
                settings
                    //.WithProperty("Platform", new[] { "iPhoneSimulator" })
                    .SetConfiguration(configuration));
        }
    });

Task("NUnit")
    .IsDependentOn("Build")
    .Does(() =>
    {
        NUnit(dirs[2] + File("Xamarin.Forms.Mocks.Tests.dll"));
    });

Task("NuGet-Package")
    .IsDependentOn("NUnit")
    .Does(() =>
    {
        var settings   = new NuGetPackSettings
        {
            Verbosity = NuGetVerbosity.Detailed,
            Version = version,
            Files = new [] 
            {
                new NuSpecContent { Source = dirs[1] + File("Xamarin.Forms.Core.UnitTests.dll"), Target = "lib/portable-net45+win8+wpa81+wp8" },
                new NuSpecContent { Source = dirs[3] + File("Xamarin.Forms.Xaml.UnitTests.dll"), Target = "lib/portable-net45+win8+wpa81+wp8" },
            },
            OutputDirectory = dirs[0]
        };
            
        NuGetPack("./Xamarin.Forms.Mocks.nuspec", settings);
    });

Task("NuGet-Push")
    .IsDependentOn("NuGet-Package")
    .Does(() =>
    {
        var apiKey = TransformTextFile ("./.nugetapikey").ToString();

        NuGetPush("./build/Xamarin.Forms.Mocks." + version + ".nupkg", new NuGetPushSettings 
        {
            Verbosity = NuGetVerbosity.Detailed,
            Source = "nuget.org",
            ApiKey = apiKey
        });
    });

Task("Default")
    .IsDependentOn("NuGet-Push");

RunTarget(target);