using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using BookStore.Model;
using System.Linq;

namespace BookStore.View
{
    /// <summary>
    /// Interaction logic for Sell.xaml
    /// </summary>
    public partial class Sell : UserControl
    {
        public Sell()
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

        private ObservableCollection<HOADON> getHoaDon()
        {
            return new ObservableCollection<HOADON>(context.HOADON);
        }

        private ObservableCollection<KHACHHANG> getCustomers()
        {
            return new ObservableCollection<KHACHHANG>(context.KHACHHANG);
        }

        private ObservableCollection<SACH> getBooks()
        {
            return new ObservableCollection<SACH>(context.SACH);
        }

        private HOADON _hoadon;
        private CT_HOADON _chitiet;
        private int _sum;

        #endregion //Datacontext

        private void setEnable()
        {
            pnlBooks.IsEnabled = true;
            pnlHoaDon.IsEnabled = true;
        }

        private void setMutable()
        {
            pnlBooks.IsEnabled = false;
            pnlHoaDon.IsEnabled = false;
        }


        private void btnDelete(object sender, RoutedEventArgs e)
        {
            {
                Button seleted = (Button)sender;
                var item = seleted.DataContext as HOADON;
                if (item != null)
                {
                    var DeleteRecord = MessageBox.Show("Bạn có chắc chắn muốn xóa hóa đơn số " + item.SoHD + " không?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (DeleteRecord == MessageBoxResult.Yes)
                    {
                        if (context.HOADON.Find(item.SoHD) != null)
                        {
                            //xóa Chi tiết.
                            var query = from b in context.CT_HOADON
                                        where b.SoHD.Equals(item.SoHD)
                                        select b;
                            foreach (var delete in query)
                            {
                                context.CT_HOADON.Remove(delete);
                            }
                            context.SaveChanges();

                            //xóa Phiếu
                            context.HOADON.Remove(item);
                            context.SaveChanges();
                            dataHoaDon.ItemsSource = getHoaDon();
                        }
                        else
                            dataHoaDon.ItemsSource = getHoaDon();
                    }
                }
                else
                    MessageBox.Show("Có gì đâu mà xóa!", "Phiếu nhập", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnAdd(object sender, RoutedEventArgs e)
        {
            if (cbKhachHang.SelectedItem != null)
                dataHoaDon.IsEnabled = true;
            else
                MessageBox.Show("Vui lòng chọn khách hàng trước khi tạo hóa đơn!", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void btnUpdate(object sender, RoutedEventArgs e)
        {
            if (dataHoaDon.SelectedItem == null)
            {
                MessageBox.Show("Hãy chọn hóa đơn để thêm sách!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                //sửa phiếu nhập
                setEnable();
                _hoadon = dataHoaDon.SelectedItem as HOADON;
                _sum = Convert.ToInt32(_hoadon.ThanhToan);
            }
        }

        private void btnSave(object sender, RoutedEventArgs e)
        {
            setMutable();
            var selected = dataHoaDon.SelectedItem as HOADON;
            if (selected != null)
            {
                _sum = 0;
                var query = from b in context.CT_HOADON
                            where b.SoHD.Equals(selected.SoHD)
                            select b;
                foreach (var sum in query)
                {
                    _sum += Convert.ToInt32(sum.ThanhTien);
                }
                var changed = context.HOADON.Find(selected.SoHD);
                changed.ThanhToan = _sum;
                context.SaveChanges();
                dataHoaDon.ItemsSource = getHoaDon();
                MessageBox.Show("Số tiền cần thanh toán là " + _sum, "Vui lòng Thanh Toán!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
                MessageBox.Show("Hãy chọn hóa đơn để thanh toán", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        bool isInsertMode = false;
        bool isBeingEdited = false;

        private void dataHoaDon_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            isInsertMode = true;
        }

        private void dataHoaDon_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            isBeingEdited = true;
        }

        private void dataHoaDon_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            HOADON _hoadon = new HOADON();
            HOADON selected = e.Row.DataContext as HOADON;
            if (isInsertMode)
            {
                var InsertRecord = MessageBox.Show("Bạn có chắc chắn muốn thêm hóa đơn mã " + selected.SoHD + " không?", "Lưu ý mã phiếu phải khác nhau", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (InsertRecord == MessageBoxResult.Yes)
                {
                    _hoadon.SoHD = selected.SoHD;
                    var customerID = cbKhachHang.SelectedValue as KHACHHANG;
                    _hoadon.MaKhachHang = customerID.MaKhachHang;
                    _hoadon.NgayLap = DateTime.Now;
                    _hoadon.ThanhToan = 0;

                    if (context.HOADON.Find(_hoadon.SoHD) == null)
                    {
                        context.HOADON.Add(_hoadon);
                        context.SaveChanges();
                        dataHoaDon.ItemsSource = getHoaDon();
                    }
                    else
                    {
                        MessageBox.Show("Mã hóa đơn không được trùng!", "Hóa Đơn", MessageBoxButton.OK, MessageBoxImage.Warning);
                        dataHoaDon.ItemsSource = getHoaDon();
                    }
                }
                else
                    dataHoaDon.ItemsSource = getHoaDon();
            }
        }

        private void dataHoaDon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = ((DataGrid)sender).SelectedItem as HOADON;
            if (selected != null)
            {
                //chọn chi tiết trùng với id
                var query = from b in context.CT_HOADON
                            where b.SoHD.Equals(selected.SoHD)
                            select b;
                ObservableCollection<CT_HOADON> data = new ObservableCollection<CT_HOADON>(query);
                dataCT.ItemsSource = data;
            }
        }

        private void btnAddBook(object sender, RoutedEventArgs e)
        {
            if (dataBooks.SelectedItem == null)
            {
                MessageBox.Show("Hãy chọn sách để thêm", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                if (String.IsNullOrEmpty(tbDonGia.Text) && String.IsNullOrEmpty(tbSoLuong.Text))
                {
                    MessageBox.Show("Hãy nhập số lượng và đơn giá", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    _chitiet = new CT_HOADON();
                    var _sach = dataBooks.SelectedItem as SACH;
                    _chitiet = context.CT_HOADON.Find(_sach.MaSach, _hoadon.SoHD);
                    if (_chitiet == null)
                    {
                        _chitiet = new CT_HOADON();
                        _chitiet.SoHD = _hoadon.SoHD;
                        _chitiet.SACH = _sach;
                        _chitiet.MaSach = _sach.MaSach;
                        _chitiet.SoLuong = Convert.ToInt32(tbSoLuong.Text);
                        _chitiet.DonGia = Convert.ToInt32(tbDonGia.Text);
                        _chitiet.ThanhTien = _chitiet.SoLuong * _chitiet.DonGia;

                        context.CT_HOADON.Add(_chitiet);
                        context.SaveChanges();

                        var query = from b in context.CT_HOADON
                                    where b.SoHD.Equals(_hoadon.SoHD)
                                    select b;
                        ObservableCollection<CT_HOADON> data = new ObservableCollection<CT_HOADON>(query);
                        dataCT.ItemsSource = data;
                    }
                    else
                    {
                        var InsertRecord = MessageBox.Show("Bạn có chắc chắn muốn sửa hóa đơn mã " + _hoadon.SoHD + " không?", "Thông Báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (InsertRecord == MessageBoxResult.Yes)
                        {
                            _chitiet.SoLuong = Convert.ToInt32(tbSoLuong.Text);
                            _chitiet.DonGia = Convert.ToInt32(tbDonGia.Text);
                            _chitiet.ThanhTien = _chitiet.SoLuong * _chitiet.DonGia;

                            context.SaveChanges();

                            var query = from b in context.CT_HOADON
                                        where b.SoHD.Equals(_hoadon.SoHD)
                                        select b;
                            ObservableCollection<CT_HOADON> data = new ObservableCollection<CT_HOADON>(query);
                            dataCT.ItemsSource = data;
                        }
                    }
                }
            }
        }

        private void btnDeleteBook(object sender, RoutedEventArgs e)
        {
            Button seleted = (Button)sender;
            var item = seleted.DataContext as CT_HOADON;
            var DeleteRecord = MessageBox.Show("Bạn có chắc chắn muốn xóa sách " + item.SACH.TenSach + " vừa nhập không?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (DeleteRecord == MessageBoxResult.Yes)
            {
                if (context.CT_HOADON.Find(item.SoHD) != null)
                {
                    //xóa chi tiết
                    context.CT_HOADON.Remove(item);
                    context.SaveChanges();
                    var query = from b in context.CT_HOADON
                                where b.SoHD.Equals(_hoadon.SoHD)
                                select b;
                    ObservableCollection<CT_HOADON> data = new ObservableCollection<CT_HOADON>(query);
                    dataCT.ItemsSource = data;
                }
            }
        }

        private void HoaDoa_Loaded(object sender, RoutedEventArgs e)
        {
            dataHoaDon.ItemsSource = getHoaDon();

            cbKhachHang.ItemsSource = getCustomers();
            cbKhachHang.DisplayMemberPath = "TenKhachHang";

            dataBooks.ItemsSource = getBooks();
        }
    }
}
