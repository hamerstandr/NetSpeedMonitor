using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USTC.Software.hanyizhao.NetSpeedMonitor.Medol
{
    public class MedolHistory
    {
        private string totaldownload ="";
        private string totalupload ="";

        public string Totaldownload { get => totaldownload; set => totaldownload = value; }
        public string Totalupload { get => totalupload; set => totalupload = value; }

        public MedolHistory()
        {
            
        }
        public void Initialize()
        {
            Totaldownload = Tool.ToString( App._History.Data1.Total.Download);
            Totalupload = Tool.ToString(App._History.Data1.Total.Upload);
        }
    }
}
