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
                AddEdit.Title = "Добавить записи";
                btnAdd.Content = "Добавить";
                _student = new Student();
            }
            else
            {
                AddEdit.Title = "Изменить записи";
                btnAdd.Content = "Изменить";
                _student = _db.Students.Find(Data.student.Id);
            }
            AddEdit.DataContext = _student;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (tbLastName.Text.Length == 0) errors.AppendLine("Введите Фамилию");
            if (tbFirstName.Text.Length == 0) errors.AppendLine("Введите Имя");
            if (tbNumber.Text.Length == 0) errors.AppendLine("Введите нормер зачетной книжки");
            if (cbLiveIn.Text != "True" && cbLiveIn.Text != "False") errors.AppendLine("Введите проживает ли студент в общежитии");
            if (cbMath.Text != "0" && cbMath.Text != "1") errors.AppendLine("Введите хочет ли студент изучать математику, если да введите 1, если нет - 0");
            if (cbProg.Text !="0" && cbProg.Text !="1") errors.AppendLine("Введите хочет ли студент изучать программирование, если да введите 1, если нет - 0");
            if (cbHistory.Text != "0" && cbHistory.Text != "1") errors.AppendLine("Введите хочет ли студент изучать историю, если да введите 1, если нет - 0");
            if (cbAnalitic.Text != "0" && cbAnalitic.Text != "1") errors.AppendLine("Введите хочет ли студент изучать аналитику, если да введите 1, если нет - 0");
            if (cbEng.Text != "0" && cbEng.Text != "1") errors.AppendLine("Введите хочет ли студент изучать английский, если да введите 1, если нет - 0");

            if (errors.Length>0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            try
            {
                if (Data.student == null)
                {
                    _db.Students.Add(_student);
                    _db.SaveChanges();
                }
                else
                {
                    _db.SaveChanges();
                }
                MessageBox.Show("Информация сохранена!");
                this.Close();
            }
            catch (Exception ex)  
            {
                _db.Students.Remove(_student);
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
