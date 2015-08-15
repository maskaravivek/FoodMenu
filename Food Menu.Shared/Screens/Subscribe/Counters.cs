using Food_Menu.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Food_Menu.Screens.Subscribe
{
    public sealed partial class Counters : Page
    {
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            BaseViewModel vm = this.DataContext as BaseViewModel;
            if (vm != null)
            {
                vm.Initialize(e.Parameter);
            }
        }
    }
}
