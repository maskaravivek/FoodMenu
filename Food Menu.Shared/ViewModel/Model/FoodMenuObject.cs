using Food_Menu.Models.Response;
using Food_Menu.Storage;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Food_Menu.ViewModel.Model
{
    public class FoodMenuObject : INotifyPropertyChanged
    {
        //public ICommand SubscriptionButtonCommand { get; set; }
        public string CounterName { get; set; }
        public string MenuType { get; set; }
        public double Cost { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Day { get; set; }
        public string MenuItems { get; set; }
        //public ObservableCollection<MenuItem> MenuItems { get; set; }
        public Storage.Models.Menu FoodMenuModel;
        
        public FoodMenuObject(string counterName, Storage.Models.Menu menu, List<Storage.Models.Item> items)
        {
            FoodMenuModel = menu;
            CounterName = counterName;
            MenuType = menu.MenuType;
            Cost = menu.Cost;
            StartTime = menu.StartTime;
            EndTime = menu.EndTime;
            Day = menu.Day;
            List<string> menuItems = new List<string>();
            foreach(Storage.Models.Item item in items)
            {
                menuItems.Add(item.Name);
            }
            MenuItems = String.Join(", ", menuItems);
            //foreach(Storage.Models.Item item in items)
            //{
            //    MenuItems.Add(new MenuItem(item));
            //}
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class SubscribeButtonClick : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            //((CounterItem)parameter).HandleSubscription();
        }
    }
}
