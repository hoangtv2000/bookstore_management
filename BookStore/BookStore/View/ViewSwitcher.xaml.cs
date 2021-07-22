using System;
using System.Windows;
using System.Windows.Controls;
using BookStore.ViewModel;

namespace BookStore.View
{

    /// chuyển View 

    public partial class ViewSwitcher : UserControl
    {
        public ViewSwitcher()
        {
            InitializeComponent();
            Switcher.viewSwitcher = this;


            Switcher.Switch(new Test());
        }

        public void Navigate(UserControl nextPage)
        {
            this.Content = nextPage;
        }

        public void Navigate(UserControl nextPage, object state)
        {
            this.Content = nextPage;
            ISwitchable s = nextPage as ISwitchable;

            if (s != null)
                s.UtilizeState(state);
            else
                throw new ArgumentException("NextPage is not ISwitchable! "
                  + nextPage.Name.ToString());
        }
    }
}
