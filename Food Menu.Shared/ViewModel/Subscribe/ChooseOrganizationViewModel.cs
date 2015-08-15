using Food_Menu.Models;
using Food_Menu.Models.Request;
using Food_Menu.Models.Response;
using Food_Menu.Services;
using Food_Menu.Utils;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food_Menu.ViewModel.Subscribe
{
    public class ChooseOrganizationViewModel : BaseViewModel
    {
        private ObservableCollection<Organization> _organizations;
        private Organization _selectedOrganization;
        public RelayCommand NextButtonCommand { get; set; }

        public ChooseOrganizationViewModel()
        {
            Organizations = new ObservableCollection<Organization>();
            NextButtonCommand = new RelayCommand(() =>
            {
                var navigationService = ServiceLocator.Current.GetInstance<NavigationService>();
                navigationService.Navigate(FileUtils.DestinationType(typeof(Screens.Subscribe.Counters)), SelectedOrganization.Id);
            });
        }

        public Organization SelectedOrganization
        {
            get { return _selectedOrganization; }
            set
            {
                if (value != _selectedOrganization)
                {
                    _selectedOrganization = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ObservableCollection<Organization> Organizations
        {
            get { return _organizations; }
            set
            {
                if (value != _organizations)
                {
                    _organizations = value;
                    RaisePropertyChanged();
                }
            }
        }

        public async Task FetchOrganizations(string city)
        {
            ResponseData responseData = await ConnectionManager.SendRequestPacket<GetOrganizationsRequest>("getOrganizations.php", new GetOrganizationsRequest(city));
            if (responseData.ResponseType.Equals(Constants.ErrorString))
            {
                var error = responseData.Payload.ToObject<ErrorResponse>();
                await OverlayProgressBar.Instance.HideAndDisplayErrorMessage(error.ErrorMessage);
            }
            else
            {
                var collection = responseData.Payload.ToObject<Organizations>();
                foreach (Organization org in collection.organizations)
                {
                    Organizations.Add(org);
                }
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
            string city = (string)param;
            await FetchOrganizations(city);
        }
    }
}
