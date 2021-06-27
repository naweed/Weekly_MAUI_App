using Application = Microsoft.Maui.Controls.Application;

namespace Weekly_MAUI_App
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
		}

		protected override IWindow CreateWindow(IActivationState activationState)
		{
			this.On<Microsoft.Maui.Controls.PlatformConfiguration.Windows>()
				.SetImageDirectory("Assets");

			return new Microsoft.Maui.Controls.Window(new NavigationPage(new StartPage()));
		}
	}
}
