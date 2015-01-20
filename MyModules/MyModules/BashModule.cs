using System;
using InterfaceLib;
using System.Net;
using System.IO;
using System.ComponentModel.Composition;

namespace MyModules
{[Export(typeof(IBodyOfModule))]
    public class BashModule:IBodyOfModule
    {
        String[] Commands = { "Баш", "Таш" };
        public string[] CommandList
        {
            get { return Commands; }
        }
        public string DescrptionModule
        {
            get { return "Баш-Выдаёт Случайную Цитату с Баша(Не выдаёт)"; }
        }
        public string GET(string URL)
        {
            string result = "";
            try
            {
                WebRequest req = WebRequest.Create(URL);
                WebResponse resp = req.GetResponse();
                Stream stream = resp.GetResponseStream();
                StreamReader sr = new StreamReader(stream, System.Text.Encoding.GetEncoding("windows-1251"));
                string Line = "";
                while (Line != null)
                {
                    Line = sr.ReadLine();
                    if (Line.Contains("<div class=\"text\">"))
                    {
                        result = Line.Substring(Line.IndexOf('>') + 1, Line.LastIndexOf('<') - Line.IndexOf('>') - 1);
                        break;
                    }


                } sr.Close();
            }
            catch (Exception e) { result = "ОШибка!!!!!"; Console.WriteLine(e.Message); }
            result = result.Replace("<br>", "\n").
            Replace("<br />", "\n").
            Replace("&quot;", " ").Replace("&lt;", "").Replace("&gt;", "");
            return result;
        }
        public void Handleevent(string Command, string args, ISkypeData ClientData, out string Answer)
        {
            switch (Command)
            {
                case "Баш": Answer = GET("http://bash.im/random"); break;
                case "Таш": Answer = "Ты написал Таш"; break;
                default: Answer = "Ты написал какуето Ахинею"; break;

            }
        }
    }
}
