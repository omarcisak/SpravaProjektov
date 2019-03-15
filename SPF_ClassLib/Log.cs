﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SPF_ClassLib
{
   

    [XmlRoot("logs")]
    public class Logs
    {
        [XmlElement("log")]
        public List<Log> LogList { get; set; }
    }

    public class Log
    {
        [XmlElement("datetime")]
        public DateTime CrtDate { get; set; }
        [XmlElement("action")]
        public string LogAction { get; set; }
        [XmlElement("username")]
        public string Username { get; set; }

        public Log()
        { }

        public Log(string logAction)
        {
            this.LogAction = logAction;
        }

        public static void WriteLog(Log log)
        {
            log.CrtDate = DateTime.Now;
            Helpers.WriteToXmlFile<Log>(Properties.Settings.Default.LogXML_path, log,true);
        }

    }
}