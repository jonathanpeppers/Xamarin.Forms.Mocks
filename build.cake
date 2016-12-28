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
string version = "0.1.0.0";

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
        var src = dirs[1] + File("Xamarin.Forms.Core.UnitTests.dll");
        var dest = dirs[0] + File("Xamarin.Forms.Mocks.dll");
        CreateDirectory(Directory("build"));
        CopyFile(src, dest);

        var settings   = new NuGetPackSettings
        {
            Verbosity = NuGetVerbosity.Detailed,
            Version = version,
            Files = new [] 
            {
                new NuSpecContent { Source = dest, Target = "lib/portable-net45+win8+wpa81+wp8" },
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

        //NuGetPush("./nuget/Live.Forms.iOS." + version + ".nupkg", new NuGetPushSettings 
        //{
        //    Verbosity = NuGetVerbosity.Detailed,
        //    Source = "nuget.org",
        //    ApiKey = apiKey
        //});

        NuGetPush("./nuget/Live.Forms.Fody." + version + ".nupkg", new NuGetPushSettings 
        {
            Verbosity = NuGetVerbosity.Detailed,
            Source = "nuget.org",
            ApiKey = apiKey
        });
    });

Task("Default")
    .IsDependentOn("NuGet-Package");

RunTarget(target);