using System;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using BookStore.Model;
using System.Windows;

namespace BookStore.View
{

    public partial class Cash : UserControl
    {
        public Cash()
        {
            InitializeComponent();
        }

        #region ISwitchable Member

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        #endregion
        QuanLySachEntities context = new QuanLySachEntities();

        private ObservableCollection<KHACHHANG> getCustomers()
        {
            return new ObservableCollection<KHACHHANG>(context.KHACHHANG);
        }
        
        private ObservableCollection<PHIEUTHUTIEN> getCash()
        {
            return new ObservableCollection<PHIEUTHUTIEN>(context.PHIEUTHUTIEN);
        }

        private bool isNull()
        {
            if (!String.IsNullOrEmpty(tbMaPT.Text)
                && !String.IsNullOrEmpty(tbTienNo.Text))
                return false;
            return true;
        }

        private void btnDelete(object sender, System.Windows.RoutedEventArgs e)
        {
            Button seleted = (Button)sender;
            var item = seleted.DataContext as PHIEUTHUTIEN;
            var DeleteRecord = MessageBox.Show("Bạn có chắc chắn muốn xóa phiếu thu " + item.SoPT + " không?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (DeleteRecord == MessageBoxResult.Yes)
            {
                context.PHIEUTHUTIEN.Remove(item);
                context.SaveChanges();
                dataCash.ItemsSource = getCash();
            }
        }

        private void btnAdd(object sender, System.Windows.RoutedEventArgs e)
        {
            if (cbKhachHang.SelectedItem != null)
            {
                if (isNull())
                    MessageBox.Show("Không được để trống!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                else if (Convert.ToInt32(tbTienThu.Text) > Convert.ToInt32(tbTienNo.Text))
                    MessageBox.Show("Tiền thu không được lớn hơn tiền nợ!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    PHIEUTHUTIEN _phieu = new PHIEUTHUTIEN();
                    KHACHHANG _customer = cbKhachHang.SelectedItem as KHACHHANG;
                    KHACHHANG _find = context.KHACHHANG.Find(_customer.MaKhachHang);
                    if (context.PHIEUTHUTIEN.Find(Convert.ToInt32(tbMaPT.Text)) == null)
                    {
                        _phieu.SoPT = Convert.ToInt32(tbMaPT.Text);
                        _phieu.NgayLap = DateTime.Now;
                        _phieu.SoTienThu = Convert.ToInt32(tbTienThu.Text);
                        _find.SoTienNo -= Convert.ToInt32(tbTienThu.Text);
                        context.SaveChanges();

                        _phieu.MaKhachHang = _find.MaKhachHang;
                        _phieu.KHACHHANG = _find;

                        context.PHIEUTHUTIEN.Add(_phieu);
                        context.SaveChanges();

                        dataCash.ItemsSource = getCash();

                        MessageBox.Show("Thêm phiếu thu thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                        tbMaPT.Clear();
                        tbTienThu.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Mã phiếu thu không được trùng!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        tbMaPT.Clear();
                    }
                }
            }
            else
                MessageBox.Show("Vui lòng chọn khách hàng cần thu tiền!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void Cash_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            cbKhachHang.ItemsSource = getCustomers();
            cbKhachHang.DisplayMemberPath = "TenKhachHang";

            dataCash.ItemsSource = getCash();

        }

    }
}
