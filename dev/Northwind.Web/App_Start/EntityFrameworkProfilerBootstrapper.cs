using HibernatingRhinos.Profiler.Appender.EntityFramework;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Northwind.Web.App_Start.EntityFrameworkProfilerBootstrapper), "PreStart")]
namespace Northwind.Web.App_Start
{
	public static class EntityFrameworkProfilerBootstrapper
	{
		public static void PreStart()
		{
			// Initialize the profiler
			EntityFrameworkProfiler.Initialize();
			
			// You can also use the profiler in an offline manner.
			// This will generate a file with a snapshot of all the EntityFramework activity in the application,
			// which you can use for later analysis by loading the file into the profiler.
			// var filename = @"c:\profiler-log";
			// EntityFrameworkProfiler.InitializeOfflineProfiling(filename);
		}
	}
}

C:\Tfs\lelong37.codeplex.com\GenericUnitofWorkAndRepositories\dev\Northwind.Web\Areas\Spa\Extensions\