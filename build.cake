///////////////////////////////////////////////////////////////////////////////
// TOOLS
///////////////////////////////////////////////////////////////////////////////



///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var solutionDir = Argument("solutionDir", ".\\");
string projectDir = solutionDir + "EventPlayer\\";

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(ctx =>
{
	// Executed BEFORE the first task.
	Information("Building...");
});

Teardown(ctx =>
{
	// Executed AFTER the last task.
	Information("Finished building.");
});

///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////

string installerAppName = string.Empty;
 
Task("__Clean")
	.Does(() => {
		var binGlob = "./**/bin";
		var objGlob = "./**/obj";

		Information("Cleaning: build directories /n- {0}\n- {1}\n]", binGlob, objGlob);
		
		CleanDirectories(binGlob);
		CleanDirectories(objGlob);
	});

string outputBinDirectory = projectDir + "bin\\" + configuration + "\\";

Task("__Build")
	.Does(() => {

		var evtPlayerSolution = "EventPlayer.sln";
	    NuGetRestore(evtPlayerSolution);

		DotNetBuild(evtPlayerSolution, settings =>
			settings.SetConfiguration(configuration)
				.SetVerbosity(Verbosity.Minimal)
				.WithTarget("Build")
			);
	});

Task("__Test").Does(() => {

    var testPath = @"./EventPlayer.Test/EventPlayer.Test.csproj"; //**/bin/" + configuration + "/**/EventPlayer.Test.dll";
    Information("Testing... [{0}]", testPath);
    
	DotNetCoreTest(
	    testPath
	);
});

Task("__PostBuild")
	.IsDependentOn("__Test");

Task("Default")
	.IsDependentOn("__Clean")
	.IsDependentOn("__Build")
    .IsDependentOn("__PostBuild");

RunTarget(target);