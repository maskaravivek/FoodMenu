using Food_Menu.Models;
using Food_Menu.Models.Request;
using Food_Menu.Models.Response;
using Food_Menu.Services;
using Food_Menu.Utils;
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

namespace Food_Menu.ViewModel.Subscribe
{
    public class ChooseCityViewModel: BaseViewModel
    {
        public RelayCommand NextButtonCommand { get; set; }
        private ObservableCollection<City> _cities;
        private City _selectedCity;

        public ChooseCityViewModel()
        {
            Cities = new ObservableCollection<City>();
            NextButtonCommand = new RelayCommand(() =>
            {
                var navigationService = ServiceLocator.Current.GetInstance<NavigationService>();
                navigationService.Navigate(FileUtils.DestinationType(typeof(Screens.Subscribe.ChooseOrganization)), SelectedCity.CityName);
            });
        }

        public City SelectedCity
        {
            get { return _selectedCity; }
            set
            {
                if (value != _selectedCity)
                {
                    _selectedCity = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ObservableCollection<City> Cities
        {
            get { return _cities; }
            set
            {
                if (value != _cities)
                {
                    _cities = value;
                    RaisePropertyChanged(); 
                }
            }
        }

        public async Task FetchCities()
        {
            ResponseData responseData = await ConnectionManager.SendRequestPacket<GetCitiesRequest>("getCities.php", new GetCitiesRequest("hello world"));
            if (responseData.ResponseType.Equals(Constants.ErrorString))
            {
                var error = responseData.Payload.ToObject<ErrorResponse>();
                await OverlayProgressBar.Instance.HideAndDisplayErrorMessage(error.ErrorMessage);
            }
            else
            {
                var collection = responseData.Payload.ToObject<Cities>();
                foreach (City city in collection.cities)
                {
                    Cities.Add(city);
                }
                await OverlayProgressBar.Instance.HideAndDisplayErrorMessage();
            }
        }

        public override void GoBack()
        {
            var navigationService = ServiceLocator.Current.GetInstance<NavigationService>();
            navigationService.GoBack();
        }

        public async override void Initialize(object param, Page basePage)
        {
            await OverlayProgressBar.Instance.ShowAndHideAfterTimeOut("Fetching cities...", basePage);
            await FetchCities();
        }
    }
}
