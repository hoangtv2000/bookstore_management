using System;
using System.Windows;
using System.Windows.Controls;
using BookStore.Model;

namespace BookStore.View
{

    public partial class Setting : UserControl
    {
        public Setting()
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

        private bool isNull()
        {
            if (!String.IsNullOrEmpty(tbQD1.Text)
                && !String.IsNullOrEmpty(tbQD2.Text)
                && !String.IsNullOrEmpty(tbQD3.Text)
                && !String.IsNullOrEmpty(tbQD4.Text))
                return false;
            return true;
        }

        private void btnChange(object sender, RoutedEventArgs e)
        {
            if (isNull())
                MessageBox.Show("Không được để trống quy định nào!","Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                THAMSO _add = new THAMSO();
                _add.SoLuongNhapToiThieu = Convert.ToInt32(tbQD1.Text);
                _add.SoLuongTonToiThieu = Convert.ToInt32(tbQD2.Text);
                _add.SoLuongTonToiDa = Convert.ToInt32(tbQD3.Text);
                _add.SoTienNoToiDa = Convert.ToInt32(tbQD4.Text);
                _add.ApDungQD4 = "Tùy chỉnh";
                context.THAMSO.Attach(_add);
                context.SaveChanges();
                MessageBox.Show("Thay đổi quy định thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnDefault(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Thay đổi các quy định về mặc định", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
