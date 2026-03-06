using Microsoft.IdentityModel.Tokens;
using pract16.ModelsBD;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace pract16
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadBDInDataGrid();
        }
        void LoadBDInDataGrid()
        {
            using (StudentChoicesContext _db = new StudentChoicesContext())
            {
                int selectredIndex = dgStudent.SelectedIndex;
                dgStudent.ItemsSource = _db.Students.ToList();
                if (selectredIndex != -1)
                {
                    if (selectredIndex>= dgStudent.Items.Count) selectredIndex = dgStudent.Items.Count-1;
                    dgStudent.SelectedIndex = selectredIndex;
                    dgStudent.ScrollIntoView(dgStudent.SelectedItem);
                }
                dgStudent.Focus();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Data.student = null;
            WindowAddEditStudent f = new WindowAddEditStudent();
            f.Owner = this;
            f.ShowDialog();
            LoadBDInDataGrid();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgStudent.SelectedItem != null)
            {
                Data.student = (Student)dgStudent.SelectedItem;
                WindowAddEditStudent f = new WindowAddEditStudent();
                f.Owner = this;
                f.ShowDialog();
                LoadBDInDataGrid();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            result = MessageBox.Show("Удалить запись?", "Удаление записи", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    Student row = (Student)dgStudent.SelectedItem;
                    if (row != null)
                    {
                        using (StudentChoicesContext _db = new StudentChoicesContext())
                        {
                            _db.Students.Remove(row);
                            _db.SaveChanges();
                        }
                        LoadBDInDataGrid();
                    }
                }
                catch
                {
                    MessageBox.Show("Ошибка удаления!");
                }
            }
            else dgStudent.Focus();
        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            List <Student> listItem = (List <Student>)dgStudent.ItemsSource;
            var filtered = listItem.Where(p=> p.LastName.Contains(tbFind.Text));
            if (filtered.Count()>0)
            {
                var item = filtered.First();
                dgStudent.SelectedItem = item;
                dgStudent.ScrollIntoView(item);
                dgStudent.Focus();
            }
            tbFind.Clear();
        }

        private void btnFiltr_Click(object sender, RoutedEventArgs e)
        {
            if (tbFiltr.Text.IsNullOrEmpty() == false)
            {
                using (StudentChoicesContext _db = new StudentChoicesContext())
                {
                    var filtered = _db.Students.Where(p => p.Groupe.Contains(tbFiltr.Text));
                    dgStudent.ItemsSource = filtered.ToList();
                }
            }
            else 
            {
                LoadBDInDataGrid();
            }
        }
    }
}