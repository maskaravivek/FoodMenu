using Food_Menu.Models;
using Food_Menu.Models.Request;
using Food_Menu.Models.Response;
using Food_Menu.Services;
using Food_Menu.Storage;
using Food_Menu.Utils;
using Food_Menu.ViewModel.Model;
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

namespace Food_Menu.ViewModel
{
    public class ViewMenuViewModel : BaseViewModel
    {
        public RelayCommand NextButtonCommand { get; set; }
        private bool _isMenuLoadingInitiated = false;
        private ObservableCollection<FoodMenuObject> _foodMenus;
        private FoodMenuObject _selectedMenu;

        public ViewMenuViewModel()
        {
            FoodMenus = new ObservableCollection<FoodMenuObject>();
            NextButtonCommand = new RelayCommand(() =>
            {
                var navigationService = ServiceLocator.Current.GetInstance<NavigationService>();
                navigationService.Navigate(FileUtils.DestinationType(typeof(Screens.Subscribe.ChooseOrganization)), null);
            });
        }

        public FoodMenuObject SelectedMenu
        {
            get { return _selectedMenu; }
            set
            {
                if (value != _selectedMenu)
                {
                    _selectedMenu = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ObservableCollection<FoodMenuObject> FoodMenus
        {
            get { return _foodMenus; }
            set
            {
                if (value != _foodMenus)
                {
                    _foodMenus = value;
                    RaisePropertyChanged();
                }
            }
        }

        public async Task FetchFreshMenu(int counter, int version)
        {
            ResponseData responseData = await ConnectionManager.SendRequestPacket<GetMenuRequest>("getMenu.php", new GetMenuRequest(counter, version));
            if (responseData.ResponseType.Equals(Constants.SuccessString))
            {
                var collection = responseData.Payload.ToObject<CounterMenu>();
                foreach(FoodMenu menu in collection.menu)
                {
                    await MenuStore.InsertMenu(new Storage.Models.Menu
                    {
                        MenuId = menu.MenuId,
                        MenuType= menu.MenuType,
                        CounterId= collection.CounterId,
                        Cost= menu.Cost,
                        Day= menu.Day,
                        StartTime= DateTime.Parse(menu.StartTime, System.Globalization.CultureInfo.CurrentCulture),
                        EndTime= DateTime.Parse(menu.StartTime, System.Globalization.CultureInfo.CurrentCulture),
                        Version = menu.Version
                    });

                    await ItemStore.DeleteItems(menu.MenuId);
                    
                    foreach(FoodItem item in menu.FoodItems)
                    {
                        await ItemStore.InsertItem(new Storage.Models.Item
                        {
                            MenuId= menu.MenuId,
                            Name= item.Name,
                            Type= item.Type
                        });
                    }
                }
            }
        }

        public async Task UpdateCounterMenus(List<Storage.Models.Counter> subscribedCounters)
        {
            foreach(Storage.Models.Counter counter in subscribedCounters)
            {
                await FetchFreshMenu(counter.Id, counter.MenuVersion);
            }
            await DisplayMenus(subscribedCounters);
        }

        public async Task DisplayMenus(List<Storage.Models.Counter> subscribedCounters)
        {
            FoodMenus.Clear();
            foreach (Storage.Models.Counter counter in subscribedCounters)
            {
                await DisplayMenu(counter);
            }
        }

        private async Task DisplayMenu(Storage.Models.Counter counter)
        {
            Storage.Models.Menu menu = await MenuStore.GetMenu(counter.Id);
            if (menu != null)
            {
                List<Storage.Models.Item> items = await ItemStore.GetItems(menu.MenuId);
                FoodMenus.Add(new FoodMenuObject(counter.CounterName, menu, items));
            }
        }

        public override void GoBack()
        {
            var navigationService = ServiceLocator.Current.GetInstance<NavigationService>();
            navigationService.GoBack();
        }

        public async override void Initialize(object param, Page basePage)
        {
            _isMenuLoadingInitiated = false;
            List<Storage.Models.Counter> subscribedCounters = await CounterStore.GetCounters();
            UpdateCounterMenus(subscribedCounters);
            DisplayMenus(subscribedCounters);
        }
    }
}
