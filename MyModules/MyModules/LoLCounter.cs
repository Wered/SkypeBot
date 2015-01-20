using System;
using InterfaceLib;
using System.Net;
using System.IO;
using System.ComponentModel.Composition;

namespace MyModules
{
    [Export(typeof(IBodyOfModule))]
    class LoLCounter:IBodyOfModule
    {
        public string[] CommandList
        {
            get { return new string[] { "Контр", "К" }; }
        }
        public string DescrptionModule
        {
            get { return "LoLCounter-Модуль для нахождения КонтрПика"; }
        }
        public string GET(string URL)
        {
            string result = "";
            try
            {
                WebRequest req = WebRequest.Create(URL);
                WebResponse resp = req.GetResponse();
                Stream stream = resp.GetResponseStream();
                StreamReader sr = new StreamReader(stream);
                string Line = "";
                bool flag = false;
                int NumOfChamp = 0;
                string[] Champions = new string[5];
                while (Line != null)
                {
                    if (NumOfChamp == 5) { break; }
                    Line = sr.ReadLine();
                    switch (flag)
                    {
                        case false:
                            if (Line.Contains("is Weak Against</p>"))
                                flag = true;
                            break;
                        case true:
                            if (Line.Contains("<div class='name'>"))
                            {
                                Champions[NumOfChamp] = SearchChampion(Line);
                                NumOfChamp++;
                            }
                            break;
                        default:
                            break;

                    }
                }
                sr.Close();
                for (int i = 0; i < NumOfChamp; i++)
                {
                    result += (i + 1) + ":" + Champions[i] + "\n";
                }
            }
            catch (Exception e) { result = "Такого героя нет,если он всё же есть Значит в проге баг.Идите нафиг"; Console.WriteLine(e.Message); }
            return result;
        }
        private string SearchChampion(string Line)
        {
            int index1 = Line.IndexOf("<div class='name'>");
            int index2 = Line.IndexOf('>', index1);
            int index3 = Line.IndexOf('<', index2);
            string NameChamp = Line.Substring(index2 + 1, index3 - index2 - 1);
            return NameChamp;
        }
        public void Handleevent(string Command, string args, ISkypeData ClientData, out string Answer)
        {
            Answer = GET("http://www.lolcounter.com/champions/" + args);
        }
    }
}
