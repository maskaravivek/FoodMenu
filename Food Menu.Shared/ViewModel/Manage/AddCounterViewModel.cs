using Food_Menu.Models;
using Food_Menu.Models.Request;
using Food_Menu.Models.Response;
using Food_Menu.Services;
using Food_Menu.Utils;
using Food_Menu.ViewModel.Subscribe.Model;
using Food_Menu.ViewModel.Utils;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Food_Menu.ViewModel.Manage
{
    public class AddCounterViewModel : BaseViewModel
    {
        private Page _currentPage;
        public RelayCommand DoneCommand { get; set; }
        private string _organizationId;
        private string _organizationName;
        private string _counterName;
        private ObservableCollection<CounterItem> _counters;
        public AddCounterViewModel()
        {
            DoneCommand = new RelayCommand(() =>
            {
                AddEntry();
            });
        }

        public string OrganizationName
        {
            get { return _organizationName; }
            set
            {
                if (value != _organizationName)
                {
                    _organizationName = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string CounterName
        {
            get { return _counterName; }
            set
            {
                if (value != _counterName)
                {
                    _counterName = value;
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

        public async Task AddEntry()
        {
            await OverlayProgressBar.Instance.ShowAndHideAfterTimeOut("Adding counter...", _currentPage);
            ResponseData responseData = await ConnectionManager.SendRequestPacket<AddCounterRequest>("addCounter.php", new AddCounterRequest(CounterName, _organizationId));
            if (responseData.ResponseType.Equals(Constants.ErrorString))
            {
                var error = responseData.Payload.ToObject<ErrorResponse>();
                await OverlayProgressBar.Instance.HideAndDisplayErrorMessage(error.ErrorMessage);
            }
            else
            {
                var collection = responseData.Payload.ToObject<Counters>();
                Counters.Clear();
                Storage.Models.Organization organization = new Storage.Models.Organization
                {
                    Id = collection.OrganizationId,
                    Name = collection.OrganizationName,
                    City = collection.OrganizationCity
                };
                foreach (Counter counter in collection.counters)
                {
                    Counters.Add(new CounterItem(counter, organization));
                }
                await OverlayProgressBar.Instance.HideAndDisplayErrorMessage();
            }
        }

        public override void GoBack()
        {
            var navigationService = ServiceLocator.Current.GetInstance<NavigationService>();
            navigationService.GoBack();
        }

        public override void Initialize(object param, Page basePage)
        {
            _currentPage = basePage;
        }
    }
}
