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

namespace Food_Menu.ViewModel.Manage
{
    public class AddOrganizationViewModel : BaseViewModel
    {
        private Page _currentPage;
        public RelayCommand DoneCommand { get; set; }
        private string _city;
        private string _name;
        public AddOrganizationViewModel()
        {
            DoneCommand = new RelayCommand(() =>
            {
                AddEntry();
            });
        }

        public string City
        {
            get { return _city; }
            set
            {
                if (value != _city)
                {
                    _city = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    RaisePropertyChanged();
                }
            }
        }

        public async Task AddEntry()
        {
            await OverlayProgressBar.Instance.ShowAndHideAfterTimeOut("Adding organization...", _currentPage);
            ResponseData responseData = await ConnectionManager.SendRequestPacket<AddOrganizationRequest>("addOrganization.php", new AddOrganizationRequest(Name, City));
            if (responseData.ResponseType.Equals(Constants.ErrorString))
            {
                var error = responseData.Payload.ToObject<ErrorResponse>();
                await OverlayProgressBar.Instance.HideAndDisplayErrorMessage(error.ErrorMessage);
            }
            else
            {
                var collection = responseData.Payload.ToObject<Organization>();
                await OverlayProgressBar.Instance.HideAndDisplayErrorMessage();

                var navigationService = ServiceLocator.Current.GetInstance<NavigationService>();
                navigationService.Navigate(FileUtils.DestinationType(typeof(Screens.Manage.AddCounter)), collection);
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
