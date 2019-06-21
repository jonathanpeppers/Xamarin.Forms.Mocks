#tool nuget:?package=NUnit.ConsoleRunner&version=3.8.0

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
    Directory("./Xamarin.Forms.Mocks/obj") + Directory(configuration),
    Directory("./Xamarin.Forms.Mocks.Tests/obj") + Directory(configuration),
    Directory("./Xamarin.Forms.Mocks.Xaml/obj") + Directory(configuration),
};
string sln = "./Xamarin.Forms.Mocks.sln";
string version = "3.5.0.2";
string suffix = "";

MSBuildSettings MSBuildSettings()
{
    var settings = new MSBuildSettings { Configuration = configuration };

    if (IsRunningOnWindows())
    {
        // Find MSBuild for Visual Studio 2019 and newer
        DirectoryPath vsLatest = VSWhereLatest();
        FilePath msBuildPath = vsLatest?.CombineWithFilePath("./MSBuild/Current/Bin/MSBuild.exe");

        // Find MSBuild for Visual Studio 2017
        if (msBuildPath != null && !FileExists(msBuildPath))
            msBuildPath = vsLatest.CombineWithFilePath("./MSBuild/15.0/Bin/MSBuild.exe");

        // Have we found MSBuild yet?
        if (!FileExists(msBuildPath))
        {
            throw new Exception($"Failed to find MSBuild: {msBuildPath}");
        }

        Information("Building using MSBuild at " + msBuildPath);
        settings.ToolPath = msBuildPath;
    }
    else
    {
        settings.ToolPath = Context.Tools.Resolve("msbuild");
    }

    return settings.WithRestore();
}

Task("Clean")
    .Does(() =>
    {
        foreach (var dir in dirs)
            CleanDirectory(dir);
    });

Task("Build")
    .IsDependentOn("Clean")
    .Does(() =>
    {
        MSBuild(sln, MSBuildSettings());
    });

Task("NUnit")
    .IsDependentOn("Build")
    .Does(() =>
    {
        NUnit3(dirs[2] + File("./net461/Xamarin.Forms.Mocks.Tests.dll"));
    });

Task("NuGet-Package")
    .IsDependentOn("NUnit")
    .Does(() =>
    {
        var settings   = new NuGetPackSettings
        {
            Verbosity = NuGetVerbosity.Detailed,
            Version = version + suffix,
            Files = new [] 
            {
                new NuSpecContent { Source = dirs[1] + File("netstandard2.0/Xamarin.Forms.Core.UnitTests.dll"), Target = "lib/netstandard2.0" },
                new NuSpecContent { Source = dirs[3] + File("netstandard2.0/Xamarin.Forms.Xaml.UnitTests.dll"), Target = "lib/netstandard2.0" },
            },
            OutputDirectory = dirs[0]
        };
            
        NuGetPack("./Xamarin.Forms.Mocks.nuspec", settings);
    });

Task("NuGet-Push")
    .Does(() =>
    {
        var apiKey = TransformTextFile ("./.nugetapikey").ToString();

        NuGetPush("./build/Xamarin.Forms.Mocks." + version + suffix + ".nupkg", new NuGetPushSettings 
        {
            Verbosity = NuGetVerbosity.Detailed,
            Source = "nuget.org",
            ApiKey = apiKey
        });
    });

Task("Default")
    .IsDependentOn("NuGet-Package");

RunTarget(target);