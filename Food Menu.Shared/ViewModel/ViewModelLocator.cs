using Food_Menu.Services;
using Food_Menu.ViewModel.Manage;
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

        public ViewMenuViewModel Menus
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ViewMenuViewModel>();
            }
        }

        public AddOrganizationViewModel AddOrg
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AddOrganizationViewModel>();
            }
        }

        public AddCounterViewModel AddCounter
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AddCounterViewModel>();
            }
        }

        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<NavigationService>();
            SimpleIoc.Default.Register<ChooseCityViewModel>();
            SimpleIoc.Default.Register<ChooseOrganizationViewModel>();
            SimpleIoc.Default.Register<CountersViewModel>();
            SimpleIoc.Default.Register<ViewMenuViewModel>();
            SimpleIoc.Default.Register<AddOrganizationViewModel>();
            SimpleIoc.Default.Register<AddCounterViewModel>();
        }
    }
}
