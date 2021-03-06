﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace USTC.Software.hanyizhao.NetSpeedMonitor
{
    public class History
    {
        public Data Data1 = new Data();
        string PathFileData;
        DataHistory Now;
        public History()
        {
            PathFileData = Path.Combine(Environment.GetFolderPath(
    Environment.SpecialFolder.ApplicationData), typeof(App).Assembly.ManifestModule.Name.Replace(".exe",""));
            Directory.CreateDirectory(PathFileData);
            PathFileData = Path.Combine(PathFileData, "Data.xml");
            Data1 = Data.Load(PathFileData);
        }
        public EventHandler<string> ChengEvent;
        public void Add(UDStatistic statistics,DateTime date,bool Insert=false)
        {
            Data1.Total.Download+= statistics.download;
            Data1.Total.Upload += statistics.upload;
            if (Insert)
            {
                HowGetNew(date);
                Now.Download += statistics.download;
                Now.Upload += statistics.upload;
            }
            ChengEvent?.Invoke(this, "Total");
        }
        void HowGetNew(DateTime date)
        {
            Now =Data1.ListHistory.Find(x => x.Date.Month == date.Month && x.Date.Day == date.Day);
            if (Now == null)
            {
                Now = new DataHistory() { Date = DateTime.Now };
                Data1.ListHistory.Add(Now);
            }
        }
        internal void Save()
        {
            Data.Save(Data1, PathFileData);
        }
    }
    [XmlRoot("Data", Namespace = "hamerstandr",
IsNullable = false)]
    public class Data
    {
        [XmlAttribute]
        public DateTime Date;
        [XmlElement]
        public DataHistory Total;
        [XmlArray("ListHistory")]
        public List<DataHistory> ListHistory;
        public static void Save(object obj, string path)
        {
            XmlSerializer s = new XmlSerializer(obj.GetType());
            using (StreamWriter writer = new StreamWriter(path))
            {
                s.Serialize(writer, obj);
            }
        }

        public static Data Load(string path)
        {
            XmlSerializer s = new XmlSerializer(typeof(Data));
            if (File.Exists(path))
                using (StreamReader reader = new StreamReader(path))
                {
                    object obj = s.Deserialize(reader);
                    return (Data)obj;
                }
            else
                return new Data() { ListHistory = new List<DataHistory>(), Total = new DataHistory(), Date = DateTime.Now
        };

        }
        }
    public class DataHistory
    {
        [XmlAttribute]
        public DateTime Date;
        [XmlAttribute]
        public double Download;
        [XmlAttribute]
        public double Upload;
    }
}