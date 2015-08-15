using Food_Menu.Models.Response;
using Food_Menu.Storage;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Food_Menu.ViewModel.Subscribe.Model
{
    public class CounterItem: INotifyPropertyChanged
    {
        public ICommand SubscriptionButtonCommand { get; set; }
        private string _counterName;
        private bool _isSubscribed;
        public Counter CounterObject;
        public Storage.Models.Organization Organization;
        public string CounterName
        {
            get { return _counterName; }
            set
            {
                if (_counterName != value)
                {
                    _counterName = value;
                    OnPropertyChanged("CounterName");
                }
            }
        }

        public bool IsSubscribed
        {
            get { return _isSubscribed; }
            set
            {
                if (_isSubscribed != value)
                {
                    _isSubscribed = value;
                    OnPropertyChanged("IsSubscribed");
                }
            }
        }

        public CounterItem(Counter counter, Storage.Models.Organization organization)
        {
            CounterObject = counter;
            Organization = organization;
            CounterName = counter.CounterName;
            SubscriptionButtonCommand = new SubscribeButtonClick();
            CheckSubscription();
        }

        public async void CheckSubscription()
        {
            IsSubscribed =  await CounterStore.CounterExists(CounterObject.CounterId);
        }

        public void HandleSubscription()
        {
            if (!IsSubscribed)
            {
                InsertCounter();
                IsSubscribed = true;
            }
            else
            {
                DeleteCounter();
                IsSubscribed = false;
            }
        }

        private async Task DeleteCounter()
        {
            await CounterStore.DeleteCounter(CounterObject.CounterId);
        }
        private async Task InsertCounter()
        {
            await OrganizationStore.InsertOrganization(Organization);
            await CounterStore.InsertCounter(new Storage.Models.Counter
            {
                Id = CounterObject.CounterId,
                CounterName = CounterObject.CounterName,
                MenuVersion = CounterObject.MenuVersion,
                OrganizationId = Organization.Id
            });
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
            ((CounterItem)parameter).HandleSubscription();
        }
    }
}
