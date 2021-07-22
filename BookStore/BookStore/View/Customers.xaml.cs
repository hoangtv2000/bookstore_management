using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using BookStore.Model;
using System;

namespace BookStore.View
{
    public partial class Customers : UserControl
    {
        public Customers()
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

        private ObservableCollection<KHACHHANG> getCustomers()
        {
            return new ObservableCollection<KHACHHANG>(context.KHACHHANG);
        }

        #endregion //Datacontext

        private void setEnable()
        {
            tbTenKH.IsEnabled = true;
            tbDiaChi.IsEnabled = true;
            tbSDT.IsEnabled = true;
            tbEmail.IsEnabled = true;
            tbTienNo.IsEnabled = true;
        }

        private void setMutable()
        {
            tbMaKH.IsEnabled = false;
            tbTenKH.IsEnabled = false;
            tbDiaChi.IsEnabled = false;
            tbSDT.IsEnabled = false;
            tbTienNo.IsEnabled = false;
            tbEmail.IsEnabled = false;
        }

        private void setClear()
        {
            tbMaKH.Clear();
            tbTenKH.Clear();
            tbDiaChi.Clear();
            tbSDT.Clear();
            tbEmail.Clear();
            tbTienNo.Clear();
        }

        private bool isNull()
        {
            if (!String.IsNullOrEmpty(tbMaKH.Text)
                && !String.IsNullOrEmpty(tbTenKH.Text)
                && !String.IsNullOrEmpty(tbDiaChi.Text)
                && !String.IsNullOrEmpty(tbSDT.Text)
                && !String.IsNullOrEmpty(tbTienNo.Text)
                && !String.IsNullOrEmpty(tbEmail.Text))
                return false;
            return true;
        }

        private void btnAdd(object sender, RoutedEventArgs e)
        {
            dataCustomers.SelectedItem = null;
            if (!tbTenKH.IsEnabled)
            {
                tbMaKH.IsEnabled = true;
                setClear();
                setEnable();
                dataCustomers.IsEnabled = false;
            }
            else
            {
                setMutable();
                dataCustomers.IsEnabled = true;
            }
        }

        private void btnSave(object sender, RoutedEventArgs e)
        {
            KHACHHANG _customer = new KHACHHANG();
            if (!isNull())
            {
                if (tbMaKH.IsEnabled)
                {
                    var Confirm = MessageBox.Show("Bạn có chắc muốn thêm khách hàng " + tbTenKH.Text + " không?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (Confirm == MessageBoxResult.Yes)
                    {
                        var _find = Convert.ToInt32(tbMaKH.Text);
                        if (context.KHACHHANG.Find(_find) == null)
                        {
                            _customer.MaKhachHang = Convert.ToInt32(tbMaKH.Text);
                            _customer.SoTienNo = Convert.ToInt32(tbTienNo.Text);

                            _customer.TenKhachHang = tbTenKH.Text;
                            _customer.DiaChi = tbDiaChi.Text;
                            _customer.SDT = tbSDT.Text;
                            _customer.Email = tbEmail.Text;

                            context.KHACHHANG.Add(_customer);
                            context.SaveChanges();

                            dataCustomers.ItemsSource = getCustomers();
                        }
                        else
                            MessageBox.Show("Mã khách hàng không được trùng!","Thông Báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    var Confirm = MessageBox.Show("Bạn có chắc muốn sửa khách hàng " + tbTenKH.Text + " không?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (Confirm == MessageBoxResult.Yes)
                    {
                        _customer = context.KHACHHANG.Find(Convert.ToInt32(tbMaKH.Text));
                        _customer.SoTienNo = Convert.ToInt32(tbTienNo.Text);

                        _customer.TenKhachHang = tbTenKH.Text;
                        _customer.DiaChi = tbDiaChi.Text;
                        _customer.SDT = tbSDT.Text;
                        _customer.Email = tbEmail.Text;

                        context.SaveChanges();

                        dataCustomers.ItemsSource = getCustomers();
                    }
                }
                setMutable();
            }
            else
                MessageBox.Show("Không được để trống!", "Sách", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnUpdate(object sender, RoutedEventArgs e)
        {
            if (dataCustomers.SelectedItem == null)
                MessageBox.Show("Hãy chọn khách hàng để sửa!", "Khách hàng", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
                setEnable();
        }

        private void btnDelete(object sender, RoutedEventArgs e)
        {
            Button seleted = (Button)sender;
            var item = seleted.DataContext as KHACHHANG;
            var DeleteRecord = MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng " + item.TenKhachHang + " không?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (DeleteRecord == MessageBoxResult.Yes)
            {
                context.KHACHHANG.Remove(item);
                context.SaveChanges();
                dataCustomers.ItemsSource = getCustomers();
            }
        }

        private void Customer_Loaded(object sender, RoutedEventArgs e)
        {
            dataCustomers.ItemsSource = getCustomers();
        }
    }
}
