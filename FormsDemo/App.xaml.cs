using FormsDemo.Layouts;
using FormsDemo.Services;
using FormsDemo.ViewModels;
using Prism.Unity;
using FormsDemo.Views;
using Microsoft.Practices.Unity;
using Xamarin.Forms;

namespace FormsDemo
{
    public partial class App : PrismApplication
    {
        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(MainPage)}");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<SemiStackLayoutPage>();
            Container.RegisterTypeForNavigation<CornerLayoutPage>();
            Container.RegisterTypeForNavigation<ObservableDemoPage>();
            Container.RegisterTypeForNavigation<ObservableAsyncDemoPage>();
            Container.RegisterTypeForNavigation<ObservableViewModelDemoPage, ObservableViewModelDemoViewModel>();
            Container.RegisterTypeForNavigation<SampleGridViewPage>();

            Container.RegisterType<IHttpService, HttpService>();
        }
    }
}
