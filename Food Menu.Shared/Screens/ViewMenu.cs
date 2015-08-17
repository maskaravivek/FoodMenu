using Food_Menu.ViewModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Food_Menu.Screens
{
    public sealed partial class ViewMenu : Page
    {
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            BaseViewModel vm = this.DataContext as BaseViewModel;
            if (vm != null)
            {
                vm.Initialize(e.Parameter, this);
            }
        }
    }
}
