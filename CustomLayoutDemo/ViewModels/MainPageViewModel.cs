using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomLayoutDemo.Views;

namespace CustomLayoutDemo.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;

        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NextCommand = DelegateCommand.FromAsyncHandler(Next);
        }

        public DelegateCommand NextCommand { get; }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            
        }

        private async Task Next()
        {
            await _navigationService.NavigateAsync($"{nameof(SemiStackLayoutPage)}");
        }
    }
}
