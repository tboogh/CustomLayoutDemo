using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FormsDemo.Views;

namespace FormsDemo.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;

        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            DisplayPageCommand = DelegateCommand<string>.FromAsyncHandler(DisplayPage);
        }

        private async Task DisplayPage(string pageName)
        {
            await _navigationService.NavigateAsync(pageName);
        }

        public DelegateCommand<string> DisplayPageCommand { get; }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            
        }
    }
}
