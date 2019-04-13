using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USTC.Software.hanyizhao.NetSpeedMonitor.Model
{
    public class ModelHistory : INotifyPropertyChanged
    {
        

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private string monthdownload = "";
        private string monthupload = "";
        private string totaldownload = "";
        private string totalupload = "";
        private string daydownload = "";
        private string dayupload = "";
        public string Totaldownload { get => totaldownload; set { totaldownload = value; OnPropertyChanged("Totaldownload"); } }
        public string Totalupload { get => totalupload; set { totalupload = value; OnPropertyChanged("Totalupload"); } }

        public string Monthdownload { get => monthdownload; set { monthdownload = value; OnPropertyChanged("Monthdownload"); } }
        public string Monthupload { get => monthupload; set { monthupload = value; OnPropertyChanged("Monthupload"); } }
        public string Daydownload { get => daydownload; set { daydownload = value; OnPropertyChanged("Daydownload"); } }
        public string Dayupload { get => dayupload; set { dayupload = value; OnPropertyChanged("Dayupload"); } }


        public ModelHistory()
        {
            //display disinge
            Totaldownload = Tool.ToString(0);
            Totalupload = Tool.ToString(0);
            Daydownload = Tool.ToString(0);
            Dayupload = Tool.ToString(0);
            Monthdownload = Tool.ToString(0);
            Monthupload = Tool.ToString(0);
        }
        public void Initialize()
        {
            CallDay();
            CallMonth();
            App._History.ChengEvent += Chenge;
        }
        int index = 0;
        double dayd = 0;
        double dayu = 0;
        double monthd = 0;
        double monthu = 0;
        private void Chenge(object sender, string e)
        {
            Totaldownload = Tool.ToString(App._History.Data1.Total.Download);
            Totalupload = Tool.ToString(App._History.Data1.Total.Upload);
            if (index == 60)
            {
                CallDay();
            }
            else if(index==300)
            {
                CallMonth();
            }
            index++;
        }
        void CallDay()
        {
            int day = DateTime.Now.Day;
            dayd = 0; dayu = 0;
            var d = App._History.Data1.ListHistory.Where(x => x.Date.Day == day);
            d.Where(x => WhereDay(x, day));
            Daydownload = Tool.ToString(dayd);
            Dayupload = Tool.ToString(dayu);
        }
        void CallMonth()
        {
            index = -1;
            int month = DateTime.Now.Month;
            monthd = 0; monthu = 0;
            var m = App._History.Data1.ListHistory.Where(x => x.Date.Month == month);
            m.Where(x => WhereMonth(x, month));
            Monthdownload = Tool.ToString(monthd);
            Monthupload = Tool.ToString(monthu);
        }
        bool WhereDay (DataHistory Data, int day)
            {
            if (Data.Date.Day == day) { dayd+=Data.Download; dayu += Data.Upload; }
            return false;
            }
        bool WhereMonth(DataHistory Data, int Month)
        {
            if (Data.Date.Month == Month) { monthd += Data.Download; monthu += Data.Upload; }
            return false;
        }
    }
}
