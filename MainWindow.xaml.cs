using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace Exchange_Office_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<OfficeInfo> offices = new List<OfficeInfo>();
        string path = null;

        public MainWindow()
        {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Random rnd = new Random();
            //offices.Add(new OfficeInfo("SberBank", 101.5, 94.2, rnd.Next(5000, 100000), rnd.Next(5000, 100000)));
            //offices.Add(new OfficeInfo("Tinkoff", 102.65, 88, rnd.Next(5000, 100000), rnd.Next(5000, 100000)));
            //offices.Add(new OfficeInfo("VTB", 99.95, 93.35, rnd.Next(5000, 100000), rnd.Next(5000, 100000)));

            //offices = OfficeInfo.MakeListOfOffices(path);

            //dataGrid1.ItemsSource = offices;

            combobox1.Items.Add("Названию");
            combobox1.Items.Add("Курсу продажи");
            combobox1.Items.Add("Курсу покупки");
            combobox1.Items.Add("Количеству проданных");
            combobox1.Items.Add("Количеству купленных");
            combobox2.Items.Add("Возрастанию");
            combobox2.Items.Add("Убыванию");

           combobox1.SelectedIndex = 0;
           combobox2.SelectedIndex = 0;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1(offices, path);
            if (offices.Count > 0)
            {
                window1.ShowDialog();
                dataGrid1.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Валютообменников нет", "Информация");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window2 window2 = new Window2(offices, path);
            window2.ShowDialog();
            dataGrid1.Items.Refresh();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (offices.Count == 0) {
                MessageBox.Show($"Максимальная разница между курсом продажи и покупки: {0}", "Информация", MessageBoxButton.OK);
                return;
            }

            double maximumDiff = getMaxDiffBetweenSellRateAndPurchaseRate(offices);

            MessageBox.Show($"Максимальная разница между курсом продажи и покупки: {maximumDiff}", "Информация", MessageBoxButton.OK);
        }

        public double getMaxDiffBetweenSellRateAndPurchaseRate(List<OfficeInfo> offices)
        {
            double maximumDiff = double.MinValue;
            foreach (OfficeInfo office in offices)
            {
                if (office.sellingRate - office.purchaceRate > maximumDiff)
                {
                    maximumDiff = office.sellingRate - office.purchaceRate;
                }
            }
            return maximumDiff;
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            if(offices.Count == 0)
            {
                MessageBox.Show("Валютообменников нет", "Информация");
                return;
            }

            double maximumDiff = double.MinValue;
            OfficeInfo deletedOffice = null;

            foreach (OfficeInfo office in offices)
            {
                if (office.sellingRate - office.purchaceRate > maximumDiff)
                {
                    maximumDiff = office.sellingRate - office.purchaceRate;
                    deletedOffice = office;
                }
            }

            offices.Remove(deletedOffice);

            OfficeInfo.RewriteFileOfOffices(offices, path);

            dataGrid1.Items.Refresh();
        }

        public int getNumberOfSold(List<OfficeInfo> offices)
        {
            int counter = 0;
            foreach (OfficeInfo office in offices)
            {
                counter += office.numberOfSold;
            }
            return counter;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            int counter = getNumberOfSold(offices);
            MessageBox.Show($"Количество проданных долларов: {counter}", "Информация", MessageBoxButton.OK);
        }

        public double getAllSumOfSellCurrency(List<OfficeInfo> offices)
        {
            double sum = 0;
            foreach (OfficeInfo office in offices)
            {
                sum += office.numberOfSold * office.sellingRate;
            }
            return sum;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            double sum = getAllSumOfSellCurrency(offices);
            MessageBox.Show($"Общая сумма на которую проданы доллары: {sum}", "Информация", MessageBoxButton.OK);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Window3 window3 = new Window3(offices);
            window3.ShowDialog();
        }

        public List<OfficeInfo> sortingListOfOffices(List<OfficeInfo> offices, int typeOfSort, int reverseOrder)
        {
            switch (typeOfSort)
            {
                case 0:
                    offices = offices.OrderBy(x => x.nameOfTheOffice).ToList();
                    //QuickSortName(0, offices.Count - 1);
                    break;
                case 1:
                    offices = offices.OrderBy(x => x.sellingRate).ThenBy(x => x.nameOfTheOffice).ToList();
                    //QuickSortSellingRate(0, offices.Count - 1);
                    break;
                case 2:
                    offices = offices.OrderBy(x => x.purchaceRate).ThenBy(x => x.nameOfTheOffice).ToList();
                    //QuickSortPurchaseRate(0, offices.Count - 1);
                    break;
                case 3:
                    offices = offices.OrderBy(x => x.numberOfSold).ThenBy(x => x.nameOfTheOffice).ToList();
                    //QuickSortNumberOfSold(0, offices.Count - 1);
                    break;
                case 4:
                    offices = offices.OrderBy(x => x.numberOfPurchased).ThenBy(x => x.nameOfTheOffice).ToList();
                    //QuickSortNumberOfPurchased(0, offices.Count - 1);
                    break;
                default:
                    MessageBox.Show("Параметр не найден", "Информация", MessageBoxButton.OK);
                    break;
            }

            if (reverseOrder == 1)
            {
                offices.Reverse();
            }

            return offices;
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            if (combobox1.Text != "" && combobox2.Text != "")
            {
                int typeOfSort = combobox1.SelectedIndex;
                int reverseOrder = combobox2.SelectedIndex;

                sortingListOfOffices(offices, typeOfSort, reverseOrder);

                dataGrid1.ItemsSource = offices;
            }
            else
            {
                MessageBox.Show("Выбраны не все параметры", "Информация");
            }
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            offices.Clear();

            offices = OfficeInfo.MakeDefaultListOfOffices();

            OfficeInfo.RewriteFileOfOffices(offices, "Info.txt");

            dataGrid1.ItemsSource = offices;
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Window_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
            }
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            string[] temp = (string[])e.Data.GetData(DataFormats.FileDrop);
            path = temp[0];

            offices = OfficeInfo.MakeListOfOffices(path);
            dataGrid1.ItemsSource = offices;
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Перетащите файл на рабочую обасть приложения\nПри нажатии кнопки \"Вернутся к шаблону\" создасться новый файл\nВсе изменения в таблице сохраняются в вашем файле", "Информация",MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            if(offices.Count == 0) 
            {
                MessageBox.Show("Таблица пустая", "Информация",MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "Текстовые файлы(*.txt)|*.txt|Все файлы(*.*)|*.*";
                save.InitialDirectory = @"D:\";

                string result;
                if (File.Exists(path)) result = File.ReadAllText(path);
                else result = File.ReadAllText("Info.txt");

                if (save.ShowDialog() == true)
                {
                    File.WriteAllText(save.FileName, result);
                }
            }
        }




        //public void QuickSortName(int low, int high)
        //{
        //    int i = low;
        //    int j = high;
        //    OfficeInfo x = offices[(low + high) >> 1];
        //    do
        //    {
        //        while (offices[i].nameOfTheOffice.CompareTo(x.nameOfTheOffice) == 1) ++i;
        //        while (offices[j].nameOfTheOffice.CompareTo(x.nameOfTheOffice) == -1) --j;
        //        if (i <= j)
        //        {
        //            OfficeInfo temp = offices[i];
        //            offices[i] = offices[j];
        //            offices[j] = temp;
        //            i++; j--;
        //        }
        //    } while (i < j);
        //    if (low < j) QuickSortName(low, j);
        //    if (i < high) QuickSortName(i, high);
        //}

        //public void QuickSortSellingRate(int low, int high)
        //{
        //    int i = low;
        //    int j = high;
        //    OfficeInfo x = offices[(low + high) >> 1];
        //    do
        //    {
        //        while (offices[i].sellingRate < x.sellingRate) ++i;
        //        while (offices[i].sellingRate > x.sellingRate) --j;
        //        if (i <= j)
        //        {
        //            OfficeInfo temp = offices[i];
        //            offices[i] = offices[j];
        //            offices[j] = temp;
        //            i++; j--;
        //        }
        //    } while (i < j);
        //    if (low < j) QuickSortSellingRate(low, j);
        //    if (i < high) QuickSortSellingRate(i, high);
        //}

        //public void QuickSortPurchaseRate(int low, int high)
        //{
        //    int i = low;
        //    int j = high;
        //    OfficeInfo x = offices[(low + high) >> 1];
        //    do
        //    {
        //        while (offices[i].purchaceRate < x.purchaceRate) ++i;
        //        while (offices[j].purchaceRate > x.purchaceRate) --j;
        //        if (i <= j)
        //        {
        //            OfficeInfo temp = offices[i];
        //            offices[i] = offices[j];
        //            offices[j] = temp;
        //            i++; j--;
        //        }
        //    } while (i < j);
        //    if (low < j) QuickSortPurchaseRate(low, j);
        //    if (i < high) QuickSortPurchaseRate(i, high);
        //}

        //public void QuickSortNumberOfSold(int low, int high)
        //{
        //    int i = low;
        //    int j = high;
        //    OfficeInfo x = offices[(low + high) >> 1];
        //    do
        //    {
        //        while (offices[i].numberOfSold < x.numberOfSold) ++i;
        //        while (offices[j].numberOfSold > x.numberOfSold) --j;
        //        if (i <= j)
        //        {
        //            OfficeInfo temp = offices[i];
        //            offices[i] = offices[j];
        //            offices[j] = temp;
        //            i++; j--;
        //        }
        //    } while (i < j);
        //    if (low < j) QuickSortNumberOfSold(low, j);
        //    if (i < high) QuickSortNumberOfSold(i, high);
        //}

        //public void QuickSortNumberOfPurchased(int low, int high)
        //{
        //    int i = low;
        //    int j = high;
        //    OfficeInfo x = offices[(low + high) >> 1];
        //    do
        //    {
        //        while (offices[i].numberOfPurchased < x.numberOfPurchased) ++i;
        //        while (offices[j].numberOfPurchased > x.numberOfPurchased) --j;
        //        if (i <= j)
        //        {
        //            OfficeInfo temp = offices[i];
        //            offices[i] = offices[j];
        //            offices[j] = temp;
        //            i++; j--;
        //        }
        //    } while (i < j);
        //    if (low < j) QuickSortNumberOfPurchased(low, j);
        //    if (i < high) QuickSortNumberOfPurchased(i, high);
        //}


    }
}
