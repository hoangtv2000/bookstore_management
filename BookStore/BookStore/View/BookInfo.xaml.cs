using System;
using System.Windows.Controls;
using BookStore.ViewModel;
using BookStore.Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace BookStore.View
{
    public partial class BookInfo : UserControl
    {
        QuanLySachEntities context = new QuanLySachEntities();
        bool isInsertMode = false;
        bool isBeingEdited = false;

        bool isInsertMode2 = false;
        bool isBeingEdited2 = false;

        public BookInfo()
        {
            InitializeComponent();
        }

        #region ISwitchable Member

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region Thể Loại

        private void theLoai_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            dataTheLoai.ItemsSource = getTheLoai();
        }

        private ObservableCollection<THELOAI> getTheLoai()
        {
            return new ObservableCollection<THELOAI>(context.THELOAI);
        }

        // Thêm
        private void theLoai_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            THELOAI _theloai = new THELOAI();
            THELOAI selected = e.Row.DataContext as THELOAI;
            if (isInsertMode)
            {
                var InsertRecord = MessageBox.Show("Bạn có chắc chắn muốn thêm thể loại " + selected.TenTheLoai + " không?", "Lưu ý mã thể loại phải khác nhau", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (InsertRecord == MessageBoxResult.Yes)
                {
                    _theloai.MaTheLoai = selected.MaTheLoai;
                    _theloai.TenTheLoai = selected.TenTheLoai;

                    if (context.THELOAI.Find(_theloai.MaTheLoai) == null)
                    {
                        context.THELOAI.Add(_theloai);
                        context.SaveChanges();
                        dataTheLoai.ItemsSource = getTheLoai();
                    }
                    else
                    {
                        MessageBox.Show("Mã thể loại không được trùng!", "Thể loại", MessageBoxButton.OK, MessageBoxImage.Warning);
                        dataTheLoai.ItemsSource = getTheLoai();
                    }
                }
                else
                    dataTheLoai.ItemsSource = getTheLoai();
            }
        }

        // Sửa
        private void theLoai_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var selected = e.Row.DataContext as THELOAI;
            var _edit = context.THELOAI.Find(selected.MaTheLoai);
            if (_edit != null)
            {
                var EditRecord = MessageBox.Show("Bạn có chắc chắn muốn sửa thành " + selected.TenTheLoai + " không?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (EditRecord == MessageBoxResult.Yes)
                {
                    if (!String.IsNullOrWhiteSpace(selected.TenTheLoai))
                    {
                        _edit.TenTheLoai = selected.TenTheLoai;
                        context.SaveChanges();
                        dataTheLoai.ItemsSource = getTheLoai();
                    }
                    else
                    {
                        MessageBox.Show("Tên thể loại không được để trống!", "Thể loại", MessageBoxButton.OK, MessageBoxImage.Warning);
                        dataTheLoai.ItemsSource = getTheLoai();
                    }
                }
                else
                    dataTheLoai.ItemsSource = getTheLoai();
            }
        }

        private void theLoai_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            isBeingEdited = true;
        }

        private void theLoai_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            isInsertMode = true;
        }

        // Xóa
        private void deleteTheLoai(object sender, RoutedEventArgs e)
        {
            Button seleted = (Button)sender;
            var item = seleted.DataContext as THELOAI;
            if (item != null)
            {
                var DeleteRecord = MessageBox.Show("Bạn có chắc chắn muốn xóa " + item.TenTheLoai + " không?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (DeleteRecord == MessageBoxResult.Yes)
                {
                    context.THELOAI.Remove(item);
                    context.SaveChanges();
                    dataTheLoai.ItemsSource = getTheLoai();
                }
            }
            else
                MessageBox.Show("Có gì đâu mà xóa!", "Thể loại", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        #endregion // Thể Loại



        #region Nhà Xuất Bản

        private void nhaXuatBan_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            dataNhaXuatBan.ItemsSource = getNhaXuatBan();
        }

        private ObservableCollection<NHAXUATBAN> getNhaXuatBan()
        {
            return new ObservableCollection<NHAXUATBAN>(context.NHAXUATBAN);
        }

        // Thêm nhà xuất bản
        private void nhaXuatBan_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            NHAXUATBAN _nhaXuatBan = new NHAXUATBAN();
            NHAXUATBAN selected = e.Row.DataContext as NHAXUATBAN;
            if (isInsertMode2)
            {
                var InsertRecord = MessageBox.Show("Bạn có chắc chắn muốn thêm nhà xuất bản " + selected.TenNhaXuatBan + " không?", "Lưu ý mã nhà xuất bản phải khác nhau", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (InsertRecord == MessageBoxResult.Yes)
                {
                    _nhaXuatBan.MaNhaXuatBan = selected.MaNhaXuatBan;
                    _nhaXuatBan.TenNhaXuatBan = selected.TenNhaXuatBan;


                    if (context.NHAXUATBAN.Find(_nhaXuatBan.MaNhaXuatBan) == null)
                    {
                        context.NHAXUATBAN.Add(_nhaXuatBan);
                        context.SaveChanges();
                        dataNhaXuatBan.ItemsSource = getNhaXuatBan();
                    }
                    else
                    {
                        MessageBox.Show("Mã nhà xuất bản không được trùng!", "Nhà xuất bản", MessageBoxButton.OK, MessageBoxImage.Warning);
                        dataNhaXuatBan.ItemsSource = getNhaXuatBan();
                    }
                }
                else
                    dataNhaXuatBan.ItemsSource = getNhaXuatBan();
            }
        }

        // Sửa nhà xuất bản
        private void nhaXuatBan_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var selected = e.Row.DataContext as NHAXUATBAN;
            var _edit = context.NHAXUATBAN.Find(selected.MaNhaXuatBan);
            if (_edit != null)
            {
                var EditRecord = MessageBox.Show("Bạn có chắc chắn muốn sửa thành " + selected.TenNhaXuatBan + " không?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (EditRecord == MessageBoxResult.Yes)
                {
                    if (!String.IsNullOrWhiteSpace(selected.TenNhaXuatBan))
                    {
                        _edit.TenNhaXuatBan = selected.TenNhaXuatBan;
                        context.SaveChanges();
                        dataNhaXuatBan.ItemsSource = getNhaXuatBan();
                    }
                    else
                    {
                        MessageBox.Show("Tên nhà xuất bản không được để trống!", "Nhà xuất bản", MessageBoxButton.OK, MessageBoxImage.Warning);
                        dataNhaXuatBan.ItemsSource = getNhaXuatBan();
                    }
                }
                else
                    dataNhaXuatBan.ItemsSource = getNhaXuatBan();

            }
        }

        private void nhaXuatBan_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            isBeingEdited2 = true;
        }
        private void nhaXuatBan_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            isInsertMode2 = true;
        }

        // Xóa
        private void deleteNhaXuatBan(object sender, RoutedEventArgs e)
        {
            Button seleted = (Button)sender;
            var item = seleted.DataContext as NHAXUATBAN;
            if (item != null)
            {
                var DeleteRecord = MessageBox.Show("Bạn có chắc chắn muốn xóa " + item.TenNhaXuatBan + " không?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (DeleteRecord == MessageBoxResult.Yes)
                {
                    context.NHAXUATBAN.Remove(item);
                    context.SaveChanges();
                    dataNhaXuatBan.ItemsSource = getNhaXuatBan();
                }
            }
            else
                MessageBox.Show("Có gì đâu mà xóa!", "Nhà Xuất Bản", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        #endregion // Nhà xuất bản




    }
}
