using System.Windows;
using System.Windows.Controls;
using BookStore.ViewModel;

namespace BookStore.View
{
 
    public partial class Menu : UserControl
    {
        public Menu()
        {
            InitializeComponent();
        }

        #region Menu Items

        private void Books(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Books());
        }

        private void Info(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new BookInfo());

        }
        private void Customer(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Customers());
        }

        private void ImportBook(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new ImportBook());
        }

        private void Sell(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Sell());
        }

        private void Cash(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Cash());
        }

        private void Find(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Find());
        }

        private void SearchCustomer(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new SearchCustomer());
        }

        private void Setting(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Setting());
        }



        #endregion //Menu Items

        private void DebtReport_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new DebtReport());
        }

        private void ExistReport_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new ExistReport());
        }
    }
}
