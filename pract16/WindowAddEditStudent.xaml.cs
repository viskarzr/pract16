using pract16.ModelsBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace pract16
{
    /// <summary>
    /// Логика взаимодействия для WindowAddEditStudent.xaml
    /// </summary>
    public partial class WindowAddEditStudent : Window
    {
        public WindowAddEditStudent()
        {
            InitializeComponent();
        }

        StudentChoicesContext _db = new StudentChoicesContext();
        Student _student;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Data.student == null)
            {
                //WindowAddEditStudent.Title = ""; // не работает
                btnAdd.Content = "Добавить";
                _student = new Student();
            }
            else
            {
                //WindowAddEditStudent.Title = ""; // не работает
                btnAdd.Content = "Изменить";
            }
            //WindowAddEditStudent.DataContextProperty
        }
    }
}
