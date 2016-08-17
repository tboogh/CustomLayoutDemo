using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FormsDemo.Services;
using Prism.Navigation;

namespace FormsDemo.ViewModels
{
    public class SampleGridViewPageViewModel : BindableBase, INavigationAware
    {
        private readonly IHttpService _httpService;
        Task<IList<Person>> _downloadPeopleTask;
        private ObservableCollection<Person> _people;

        public SampleGridViewPageViewModel(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public ObservableCollection<Person> People
        {
            get { return _people; }
            set { SetProperty(ref _people, value); }
        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            var people = await DownloadPeople();
            People = new ObservableCollection<Person>(people);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        private Task<IList<Person>> DownloadPeople()
        {
            if (_downloadPeopleTask?.IsCompleted ?? true)
            {
                _downloadPeopleTask = _httpService.DownloadPeople(CancellationToken.None);

            }
            return _downloadPeopleTask;
        }
    }
}
