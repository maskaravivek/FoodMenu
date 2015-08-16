using Food_Menu.Models.Response;
using Food_Menu.Storage;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Food_Menu.ViewModel.Model
{
    public class MenuItem : INotifyPropertyChanged
    {
        private string _name;
        private string _type;
        public Storage.Models.Item MenuItemObject;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public string Type
        {
            get { return _type; }
            set
            {
                if (_type != value)
                {
                    _type = value;
                    OnPropertyChanged("Type");
                }
            }
        }

        public MenuItem(Storage.Models.Item item)
        {
            MenuItemObject = item;
            Name = item.Name;
            Type = item.Type;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
