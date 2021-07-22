using System;
using System.Linq;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using BookStore.Model;


namespace BookStore.View
{

    public partial class Find : UserControl
    {
        public Find()
        {
            InitializeComponent();
        }

        #region ISwitchable Member

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region DataContext

        QuanLySachEntities context = new QuanLySachEntities();


        #endregion //DataContext

        private void btnSearch(object sender, System.Windows.RoutedEventArgs e)
        {
            var search = from b in context.SACH
                         where b.TenSach.Contains(tbSearch.Text)
                         select b;
            ObservableCollection<SACH> data = new ObservableCollection<SACH>(search);
            dataSearch.ItemsSource = data;
        }

        private void dataSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
