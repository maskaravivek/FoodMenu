using Food_Menu.Services;
using Food_Menu.ViewModel.Subscribe;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Food_Menu.ViewModel
{
    public class ViewModelLocator
    {
        public ChooseCityViewModel City
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ChooseCityViewModel>();
            }
        }

        public ChooseOrganizationViewModel Organization
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ChooseOrganizationViewModel>();
            }
        }

        public CountersViewModel Counters
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CountersViewModel>();
            }
        }

        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<NavigationService>();
            SimpleIoc.Default.Register<ChooseCityViewModel>();
            SimpleIoc.Default.Register<ChooseOrganizationViewModel>();
            SimpleIoc.Default.Register<CountersViewModel>();
        }
    }

}
