using Food_Menu.Models;
using Food_Menu.Models.Request;
using Food_Menu.Models.Response;
using Food_Menu.Services;
using Food_Menu.Utils;
using Food_Menu.ViewModel.Subscribe.Model;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Food_Menu.ViewModel.Subscribe
{
    public class CountersViewModel : BaseViewModel
    {
        private ObservableCollection<CounterItem> _counters;
        private CounterItem _selectedCounter;
        public RelayCommand DoneCommand { get; set; }
        public CountersViewModel()
        {
            Counters = new ObservableCollection<CounterItem>();
            DoneCommand = new RelayCommand(() =>
            {
                var navigationService = ServiceLocator.Current.GetInstance<NavigationService>();
                navigationService.Navigate(FileUtils.DestinationType(typeof(Screens.ViewMenu)), null);
            });
        }

        public CounterItem SelectedCounter
        {
            get { return _selectedCounter; }
            set
            {
                if (value != _selectedCounter)
                {
                    _selectedCounter = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ObservableCollection<CounterItem> Counters
        {
            get { return _counters; }
            set
            {
                if (value != _counters)
                {
                    _counters = value;
                    RaisePropertyChanged();
                }
            }
        }

        public async Task FetchCounters(string organizationId)
        {
            Counters.Clear();
            ResponseData responseData = await ConnectionManager.SendRequestPacket<GetCounterRequest>("getCounters.php", new GetCounterRequest(organizationId));
            if (responseData.ResponseType.Equals(Constants.ErrorString))
            {
                var error = responseData.Payload.ToObject<ErrorResponse>();
                await OverlayProgressBar.Instance.HideAndDisplayErrorMessage(error.ErrorMessage);
            }
            else
            {
                var collection = responseData.Payload.ToObject<Counters>();
                Storage.Models.Organization organization = new Storage.Models.Organization
                {
                    Id= collection.OrganizationId,
                    Name= collection.OrganizationName,
                    City= collection.OrganizationCity
                };
                foreach(Counter counter in collection.counters)
                {
                    Counters.Add(new CounterItem(counter, organization));
                }
                System.Diagnostics.Debug.WriteLine("Counters: " + Counters.Count);
                await OverlayProgressBar.Instance.HideAndDisplayErrorMessage();
            }
        }

        public override void GoBack()
        {
            var navigationService = ServiceLocator.Current.GetInstance<NavigationService>();
            navigationService.GoBack();
        }

        public async override void Initialize(object param)
        {
            string organizationId = (string)param;
            await FetchCounters(organizationId);
        }
    }
}
