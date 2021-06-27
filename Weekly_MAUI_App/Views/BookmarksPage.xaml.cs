
namespace Weekly_MAUI_App.Views
{
    public partial class BookmarksPage : ContentPage
    {
        BookmarksPageViewModel ViewModel;

        public BookmarksPage()
        {
            InitializeComponent();

            this.BindingContext = ViewModel = new();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ViewModel.OnNavigatedTo();
        }

    }
}