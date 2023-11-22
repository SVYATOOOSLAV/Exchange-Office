using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Exchange_Office_WPF
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        List<OfficeInfo> _offices = new List<OfficeInfo>();
        string _filePath = null;
        public Window2(List<OfficeInfo> offices, string filePath)
        {
            InitializeComponent();
            _offices = offices;
            _filePath = filePath;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool flag = true;
            try
            {
                if (textBox1.Text != "" && textBox2.Text != "" &&
                    textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "")
                {
                    double.Parse(textBox2.Text);
                    double.Parse(textBox3.Text);
                    int.Parse(textBox4.Text);
                    int.Parse(textBox5.Text);
                }
                else
                {
                    flag = false;
                    MessageBox.Show("Не все поля заполнены", "Ошибка", MessageBoxButton.OK);
                }
            }
            catch
            {
                flag = false;
                MessageBox.Show("Некорректные данные", "Ошибка", MessageBoxButton.OK);
            }

            if (flag)
            {
                OfficeInfo office = new OfficeInfo(textBox1.Text, double.Parse(textBox2.Text), double.Parse(textBox3.Text),
                    int.Parse(textBox4.Text), int.Parse(textBox5.Text));

                _offices.Add(office);

                OfficeInfo.RewriteFileOfOffices(_offices, _filePath);

                MessageBox.Show("Информация о банке успешно добавлена", "Подтверждение", MessageBoxButton.OK);

                Button_Click_1(sender, e);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
