using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Controls;

namespace Food_Menu.ViewModel
{
    public abstract class BaseViewModel : ViewModelBase
    {
        private bool isLoading;

        public bool IsLoading
        {
            get
            { return isLoading; }
            set
            {
                isLoading = value;
                RaisePropertyChanged();
            }
        }

        private bool isLoaded;

        public bool IsLoaded
        {
            get
            { return isLoaded; }
            set
            {
                isLoaded = value;
                RaisePropertyChanged();
            }
        }

        //public abstract void Initialize(object param);

        public abstract void Initialize(object param, Page basePage);

        public abstract void GoBack();
    }
}
