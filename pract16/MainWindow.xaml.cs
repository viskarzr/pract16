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
    }
}