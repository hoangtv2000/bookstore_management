using System;
using System.Linq;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using BookStore.Model;

namespace BookStore.View
{
    public partial class SearchCustomer : UserControl
    {
        public SearchCustomer()
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
            var search = from b in context.KHACHHANG
                         where b.TenKhachHang.Contains(tbSearch.Text)
                         select b;
            ObservableCollection<KHACHHANG> data = new ObservableCollection<KHACHHANG>(search);
            dataSearch.ItemsSource = data;
        }
    }
}
