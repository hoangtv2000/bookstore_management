using System;
using System.Windows.Controls;
using BookStore.ViewModel;
using BookStore.Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace BookStore.View
{

    public partial class Books : UserControl
    {
        public Books()
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

        private ObservableCollection<SACH> getBooks()
        {
            return new ObservableCollection<SACH>(context.SACH);
        }

        private ObservableCollection<THELOAI> getTheLoai()
        {
            return new ObservableCollection<THELOAI>(context.THELOAI);
        }

        private ObservableCollection<NHAXUATBAN> getNhaXuatBan()
        {
            return new ObservableCollection<NHAXUATBAN>(context.NHAXUATBAN);
        }

        #endregion

        /// Set textbox Enabled

        private void setEnabled()
        {
            tbTenSach.IsEnabled = true;
            tbTacGia.IsEnabled = true;
            cbTheLoai.IsEnabled = true;
            cbNhaXuatBan.IsEnabled = true;
            tbGiaBan.IsEnabled = true;
        }

        /// Set textbox mutable

        private void setMutable()
        {
            tbMaSach.IsEnabled = false;
            tbTenSach.IsEnabled = false;
            tbTacGia.IsEnabled = false;
            cbTheLoai.IsEnabled = false;
            cbNhaXuatBan.IsEnabled = false;
            tbGiaBan.IsEnabled = false;
        }

        private void setClear()
        {
            tbMaSach.Clear();
            tbTenSach.Clear();
            tbTacGia.Clear();
            cbTheLoai.Text = "";
            cbNhaXuatBan.Text = "";
            tbGiaBan.Clear();
        }

        /// thêm sách

        private void addBook(object sender, RoutedEventArgs e)
        {
            dataBooks.SelectedItem = null;
            if (!tbTenSach.IsEnabled)
            {
                tbMaSach.IsEnabled = true;
                setClear();
                setEnabled();
                dataBooks.IsEnabled = false;
            }
            else
            {
                setMutable();
                dataBooks.IsEnabled = true;
            }
        }

        /// kiểm tra textbox null

        private bool isNull()
        {
            if (!String.IsNullOrEmpty(tbMaSach.Text)
                && !String.IsNullOrEmpty(tbTenSach.Text)
                && !String.IsNullOrEmpty(tbTacGia.Text)
                && !String.IsNullOrEmpty(cbTheLoai.Text)
                && !String.IsNullOrEmpty(cbNhaXuatBan.Text)
                && !String.IsNullOrEmpty(tbGiaBan.Text))
                return false;
            return true;
        }

   
        private void saveBook(object sender, RoutedEventArgs e)
        {
            SACH _sach = new SACH();
            if (!isNull())
            {
                if (tbMaSach.IsEnabled)
                {
                    var Confirm = MessageBox.Show("Bạn có chắc muốn thêm sách " + tbTenSach.Text + " không?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (Confirm == MessageBoxResult.Yes)
                    {
                        var _find = Convert.ToInt32(tbMaSach.Text);
                        if (context.SACH.Find(_find) == null)
                        {
                            _sach.MaSach = Convert.ToInt32(tbMaSach.Text);

                            _sach.TenSach = tbTenSach.Text;
                            _sach.TacGia = tbTacGia.Text;

                            _sach.THELOAI = cbTheLoai.SelectedValue as THELOAI;
                            _sach.NHAXUATBAN = cbNhaXuatBan.SelectedValue as NHAXUATBAN;

                            _sach.GiaBan = Convert.ToInt32(tbGiaBan.Text);

                            context.SACH.Add(_sach);
                            context.SaveChanges();

                            dataBooks.ItemsSource = getBooks();
                            setMutable();
                            dataBooks.IsEnabled = true;
                        }
                        else
                            MessageBox.Show("Mã sách không được trùng", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    var Confirm = MessageBox.Show("Bạn có chắc muốn sửa sách " + tbTenSach.Text + " không?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (Confirm == MessageBoxResult.Yes)
                    {
                        _sach = context.SACH.Find(Convert.ToInt32(tbMaSach.Text));

                        _sach.TenSach = tbTenSach.Text;
                        _sach.TacGia = tbTacGia.Text;

                        _sach.THELOAI = cbTheLoai.SelectedValue as THELOAI;
                        _sach.NHAXUATBAN = cbNhaXuatBan.SelectedValue as NHAXUATBAN;

                        _sach.GiaBan = Convert.ToInt32(tbGiaBan.Text);

                        context.SaveChanges();

                        dataBooks.ItemsSource = getBooks();

                        MessageBox.Show("Sửa sách thành công", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Warning);

                        setMutable();
                        dataBooks.IsEnabled = true;
                    }
                }
                setMutable();
            }
            else
                MessageBox.Show("Không được để trống!", "Sách", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// xóa sách
        private void deleteBook(object sender, RoutedEventArgs e)
        {
            Button seleted = (Button)sender;
            var item = seleted.DataContext as SACH;
            var DeleteRecord = MessageBox.Show("Bạn có chắc chắn muốn xóa sách " + item.TenSach + " không?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (DeleteRecord == MessageBoxResult.Yes)
            {
                context.SACH.Remove(item);
                context.SaveChanges();
                dataBooks.ItemsSource = getBooks();
            }
        }

        /// sửa sách

        private void updateBook(object sender, RoutedEventArgs e)
        {
            if (dataBooks.SelectedItem == null)
                MessageBox.Show("Hãy chọn sách để sửa!", "Sách", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
                setEnabled();
        }


        private void Books_Loaded(object sender, RoutedEventArgs e)
        {
            dataBooks.ItemsSource = getBooks();

            cbTheLoai.ItemsSource = getTheLoai();
            cbTheLoai.DisplayMemberPath = "TenTheLoai";

            cbNhaXuatBan.ItemsSource = getNhaXuatBan();
            cbNhaXuatBan.DisplayMemberPath = "TenNhaXuatBan";
        }

    }
}
