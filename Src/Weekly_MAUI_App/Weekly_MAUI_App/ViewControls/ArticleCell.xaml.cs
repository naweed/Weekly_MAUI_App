namespace Weekly_MAUI_App.ViewControls
{
    public partial class ArticleCell : ContentView
    {
        public static readonly BindableProperty ParentContextProperty = BindableProperty.Create("ParentContext", typeof(object), typeof(ArticleCell), null, propertyChanged: OnParentContextPropertyChanged);

        public object ParentContext
        {
            get => GetValue(ParentContextProperty); 
            set => SetValue(ParentContextProperty, value); 
        }

        private static void OnParentContextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != oldValue && newValue != null)
            {
                (bindable as ArticleCell).ParentContext = newValue;
            }
        }

        public ArticleCell()
        {
            InitializeComponent();
        }
    }
}