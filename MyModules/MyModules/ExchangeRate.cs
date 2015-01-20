using System;
using InterfaceLib;
using System.Net;
using System.IO;
using System.ComponentModel.Composition;

namespace MyModules
{
    [Export(typeof(IBodyOfModule))]
    class ExchangeRate:IBodyOfModule
    {
        public string[] CommandList
        {
            get { return new string[] { "Курс" }; }
        }
        public string DescrptionModule
        {
            get { return "ExchangeRate-Показывает Курс Рубля к Доллару и Евро"; }
        }
        public string Get(string URL)
        {
            string result = "";
            try
            {
                WebRequest req = WebRequest.Create(URL);
                WebResponse resp = req.GetResponse();
                Stream stream = resp.GetResponseStream();
                StreamReader sr = new StreamReader(stream);
                string Line = "";
                while (Line != null)
                {
                    Line = sr.ReadLine();
                    if (Line.Contains("var cursNow = {"))
                    {
                        break;
                    }

                }
                sr.Close();
                int a = Line.IndexOf(':');
                int b = Line.IndexOf(',');
                string EUR = Line.Substring(Line.IndexOf(':') + 1, Line.IndexOf(',') - Line.IndexOf(':') - 1);
                string USD = Line.Substring(Line.LastIndexOf(':') + 1, Line.LastIndexOf('}') - Line.LastIndexOf(':') - 1);
                result = "Евро:" + EUR + "руб,Доллар:" + USD + "руб";
            }
            catch (Exception e) { result = e.Message; Console.WriteLine(e.Message); }
            return result;
        }
        public void Handleevent(string Command, string args, ISkypeData ClientData, out string Answer)
        {
            Answer = Get("http://www.sberometer.ru/");
        }
    }
}
