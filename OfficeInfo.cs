using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Exchange_Office_WPF
{
    public class OfficeInfo
    {
        public string nameOfTheOffice { get; set; }
        public double sellingRate { get; set; }
        public double purchaceRate { get; set; }
        public int numberOfSold { get; set; }
        public int numberOfPurchased { get; set; }
        public double totalSalesAmount { get; set; }

        public OfficeInfo(string nameOfTheOffice, double sellingRate,
            double purchaceRate, int numberOfSold, int numberOfPurchased)
        {
            this.nameOfTheOffice = nameOfTheOffice;
            this.sellingRate = sellingRate;
            this.purchaceRate = purchaceRate;
            this.numberOfSold = numberOfSold;
            this.numberOfPurchased = numberOfPurchased;

            totalSalesAmount = purchaceRate * numberOfSold;
        }

        private static void MakeListOffices(List<OfficeInfo> offices, string path)
        {
            try
            {
                string[] tempArray = File.ReadAllLines(path);
                string nameOffice = "", otherInfo = "";

                foreach (string str in tempArray)
                {
                    for (int i = 0; i < str.Length; i++)
                    {
                        if (Char.IsWhiteSpace(str[i]) && Char.IsDigit(str[i + 1]))
                        {
                            nameOffice = str.Substring(0, i);
                            otherInfo = str.Substring(i + 1);
                            break;
                        }
                    }

                    string[] otherInfoArray = otherInfo.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    offices.Add(new OfficeInfo(nameOffice.Trim(), double.Parse(otherInfoArray[0]), double.Parse(otherInfoArray[1]),
                        int.Parse(otherInfoArray[2]), int.Parse(otherInfoArray[3])));
                }
            }
            catch
            {
                throw new Exception("Данные в файле были некорректны");
            }
        }

        public static List<OfficeInfo> MakeListOfOffices(string path)
        {
            List<OfficeInfo> offices = new List<OfficeInfo>();

            if (File.Exists(path))
            {
               MakeListOffices(offices, path);
            }
            //else if (File.Exists("Info.txt"))
            //{
            //    MakeListOffices(offices, "Info.txt");
            //}

            return offices;
        }

        public static List<OfficeInfo> MakeDefaultListOfOffices()
        {
            List<OfficeInfo> offices = new List<OfficeInfo>();

            Random rnd = new Random();
            offices.Add(new OfficeInfo("SberBank", 101.5, 94.2, rnd.Next(5000, 100000), rnd.Next(5000, 100000)));
            offices.Add(new OfficeInfo("Tinkoff", 102.65, 88, rnd.Next(5000, 100000), rnd.Next(5000, 100000)));
            offices.Add(new OfficeInfo("VTB", 99.95, 93.35, rnd.Next(5000, 100000), rnd.Next(5000, 100000)));

            return offices;
        }

        public static void RewriteFileOfOffices(List<OfficeInfo> offices, string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);

                for (int i = 0; i < offices.Count; i++)
                {
                    File.AppendAllText(path, $"{offices[i]}\n");
                }
            }
            else
            {
                if(File.Exists("Info.txt")) File.Delete("Info.txt");

                for (int i = 0; i < offices.Count; i++)
                {
                    File.AppendAllText("Info.txt", $"{offices[i]}\n");
                }
            }
        }

        public override bool Equals(object obj)
        {
            return obj is OfficeInfo info &&
                   nameOfTheOffice == info.nameOfTheOffice &&
                   sellingRate == info.sellingRate &&
                   purchaceRate == info.purchaceRate &&
                   numberOfSold == info.numberOfSold &&
                   numberOfPurchased == info.numberOfPurchased;
        }

        public override int GetHashCode()
        {
            int hashCode = 892826532;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(nameOfTheOffice);
            hashCode = hashCode * -1521134295 + sellingRate.GetHashCode();
            hashCode = hashCode * -1521134295 + purchaceRate.GetHashCode();
            hashCode = hashCode * -1521134295 + numberOfSold.GetHashCode();
            hashCode = hashCode * -1521134295 + numberOfPurchased.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return $"{nameOfTheOffice} {sellingRate} {purchaceRate} {numberOfSold} {numberOfPurchased}";
        }
    }
}
