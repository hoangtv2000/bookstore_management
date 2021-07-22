using System;
using System.Windows.Controls;
using BookStore.View;

namespace BookStore.ViewModel
{
    public static class Switcher
    {
        public static ViewSwitcher viewSwitcher;

        public static void Switch(UserControl newPage)
        {
            viewSwitcher.Navigate(newPage);
        }

        public static void Switch(UserControl newPage, object state)
        {
            viewSwitcher.Navigate(newPage, state);
        }
    }
}

