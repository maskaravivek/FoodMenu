using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Food_Menu
{
    public sealed partial class OverlayProgressBar : UserControl
    {
        public OverlayProgressBar()
        {
            this.InitializeComponent();
            LayoutRoot.Height = Window.Current.Bounds.Height;
            LayoutRoot.Width = Window.Current.Bounds.Width;
        }
        private static OverlayProgressBar _instance;
        private DispatcherTimer _timer;
        private Page _basePage;

        public static OverlayProgressBar Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new OverlayProgressBar();
                }
                return _instance;
            }
            private set { }
        }

        internal Popup ChildWindowPopup { get; private set; }

        public async Task ShowAndHideAfterTimeOut(string valueofoverlay, Page applicationPage, int hideAfterTimeInSeconds = 5)
        {
            Show(valueofoverlay, applicationPage);
            _timer = new DispatcherTimer();
            _timer.Tick += _timer_Tick;
            _timer.Interval = TimeSpan.FromSeconds(hideAfterTimeInSeconds);
            _timer.Start();
        }

        private void _timer_Tick(object sender, object e)
        {
            CoreDispatcher dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;
            dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                Hide();
            });
        }

        public void ChangeStatusText(string newText)
        {
            if (this.ChildWindowPopup != null && this.ChildWindowPopup.IsOpen)
            {
                this.textBlockStatus.Text = newText;
            }
        }
        /// <summary>
        /// Will hide application bar if current page reference is passged
        /// If you don't want that, pass null
        /// </summary>
        /// <param name="valueofoverlay"></param>
        /// <param name="basePage"></param>
        public void Show(string valueofoverlay, Page basePage)
        {
            if (basePage != null)
            {
                _basePage = basePage;
                if (_basePage != null && _basePage.BottomAppBar != null)
                    _basePage.BottomAppBar.IsOpen = false;
            }
            this.textBlockStatus.Text = valueofoverlay;
            if (this.ChildWindowPopup == null)
            {
                this.ChildWindowPopup = new Popup();

                try
                {
                    this.ChildWindowPopup.Child = this;
                }
                catch (ArgumentException)
                {
                    throw new InvalidOperationException("The control is already shown.");
                }
            }

            if (this.ChildWindowPopup != null)
            {
                // Show popup
                this.ChildWindowPopup.IsOpen = true;
            }
        }

        public async Task HideAndDisplayErrorMessage(string errorMessage=null)
        {
            if (errorMessage!=null)
            {
                MessageDialog dialog = new MessageDialog(errorMessage, "Indian Kahani");
                await dialog.ShowAsync();
            }
            await Hide();
        }

        public async Task Hide()
        {
            if (this.ChildWindowPopup != null && this.ChildWindowPopup.IsOpen)
            {
                CoreDispatcher dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;
                await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    this.ChildWindowPopup.IsOpen = false;
                    if (_basePage != null && _basePage.BottomAppBar != null)
                        _basePage.BottomAppBar.IsOpen = true;
                    _basePage = null;
                });
            }
        }
    }
}
