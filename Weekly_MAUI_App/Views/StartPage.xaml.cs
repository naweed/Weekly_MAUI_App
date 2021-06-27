namespace Weekly_MAUI_App.Views
{
    public partial class StartPage : ContentPage
    {
        StartPageViewModel ViewModel;

        public StartPage()
        {
            InitializeComponent();

            this.BindingContext = ViewModel = new();

            ViewModel.DataDownloadCompleted += StartPage_DownloadCompleted;

#if WINDOWS
            MauiWinUIApplication.Current.MainWindow.Title = "Weekly MAUI App";
#endif
        }

        private async void StartPage_DownloadCompleted(object sender, EventArgs e)
        {
            //Show Bottom Navigation Animation
            if (!ViewModel.IsErrorState)
            {
                await Task.Delay(1000);
                await BottomMenu.FadeTo(1, 2000, Easing.CubicInOut);
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ViewModel.OnNavigatedTo();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            ViewModel.DataDownloadCompleted -= StartPage_DownloadCompleted;
        }

    }
}