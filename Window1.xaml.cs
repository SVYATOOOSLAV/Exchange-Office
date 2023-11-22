using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        List<OfficeInfo> _offices = new List<OfficeInfo>();
        string _filePath = null;

        public Window1(List<OfficeInfo> offices, string filePath)
        {
            InitializeComponent();

            _offices = offices;

            _filePath = filePath;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (OfficeInfo office in _offices)
            {
                Combobox1.Items.Add(office.nameOfTheOffice);
            }
            Combobox1.SelectedIndex = 0;

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            bool flag = true;

            try
            {
                if (textBox1.Text != "" && textBox2.Text != "" &&
                    textBox3.Text != "" && textBox4.Text != "")
                {
                    double.Parse(textBox1.Text);
                    double.Parse(textBox2.Text);
                    int.Parse(textBox3.Text);
                    int.Parse(textBox4.Text);
                }
                else
                {
                    flag = false;
                    MessageBox.Show("Не все строки заполнены", "Ошибка", MessageBoxButton.OK);
                }

            }
            catch
            {
                flag = false;
                MessageBox.Show("Строки содержат неправильные значения", "Ошибка", MessageBoxButton.OK);
            }

            if (flag)
            {
                _offices[indexOfOffice].nameOfTheOffice = Combobox1.Text.ToString();
                _offices[indexOfOffice].sellingRate = double.Parse(textBox1.Text);
                _offices[indexOfOffice].purchaceRate = double.Parse(textBox2.Text);
                _offices[indexOfOffice].numberOfSold = int.Parse(textBox3.Text);
                _offices[indexOfOffice].numberOfPurchased = int.Parse(textBox4.Text);

                OfficeInfo.RewriteFileOfOffices(_offices, _filePath);

                MessageBox.Show("Информация о банке успешно обновлена", "Информация", MessageBoxButton.OK);

                Combobox1.Items.Clear();

                foreach (OfficeInfo office in _offices)
                {
                    Combobox1.Items.Add(office.nameOfTheOffice);
                }

                Combobox1.SelectedIndex = 0;
            }

        }

        int indexOfOffice;


        private void Combobox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Combobox1.SelectedIndex != -1)
            {
                indexOfOffice = Combobox1.SelectedIndex;
                ShowTheDetailsOfOffice();
            }
        }

        private void ShowTheDetailsOfOffice()
        {
            textBox1.Text = _offices[indexOfOffice].sellingRate.ToString();
            textBox2.Text = _offices[indexOfOffice].purchaceRate.ToString();
            textBox3.Text = _offices[indexOfOffice].numberOfSold.ToString();
            textBox4.Text = _offices[indexOfOffice].numberOfPurchased.ToString();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();

            Combobox1.Items.RemoveAt(indexOfOffice);

            _offices.Remove(_offices[indexOfOffice]);

            OfficeInfo.RewriteFileOfOffices(_offices, _filePath);

            Combobox1.SelectedIndex = 0;

            if (Combobox1.Items.Count == 0)
            {
                this.Close();
                return;
            }

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
