namespace Weekly_MAUI_App
{
	public class Startup : IStartup
	{
		public void Configure(IAppHostBuilder appBuilder)
		{
			//System.AppContext.SetSwitch("System.Net.Http.UseNativeHttpHandler", true);

			appBuilder
				.UseMauiApp<App>()
				.ConfigureServices(services =>
				{
					services.AddSingleton<IAppService>(new GithubDataService(AppConstants.GitHubURL));
					services.AddSingleton<IArticleHelperService>(new ArticleHelperService());
				})
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont("IBMPlexSans-Medium.ttf", "MediumFont");
					fonts.AddFont("IBMPlexSans-Regular.ttf", "RegularFont");
				});
		}
	}
}