using System;
using System.Collections.Generic;
using InterfaceLib;
using System.IO;
using System.ComponentModel.Composition;

namespace MyModules
{
    [Export(typeof(IBodyOfModule))]
  public  class Reddles:IBodyOfModule
    {
             
        public string[] CommandList
        {
            get { return new string[] { "Загадка","Ответ"}; }
        }
        
        public string DescrptionModule
        {
            get { return "Reddles-Задаёт загадку"; }
        }
        public string ReadAnswer() 
        {

            using (StreamReader sr = new StreamReader("Riddles.txt"))
            {
                string Line = sr.ReadLine();
                string id = Line;
                sr.Close();
                return ReadAnswer(id);
            }     
        
        }
        public string ReadAnswer(string id)
        {
            using (StreamReader sr = new StreamReader("Riddles.txt"))
            { 
            string Line = sr.ReadLine();
            string Answer = "Ответ не найден Магия 0_0";
            while (Line != null)
            {
               
                if (Line.Length>id.Length && Line.Substring(0,id.Length) == id )
                {
                    return sr.ReadLine();
                }
                Line = sr.ReadLine();

            }
            sr.Close();
            return Answer;
            }          
        }
        public string ReadRiddle() 
        {
            using (StreamReader sr = new StreamReader("Riddles.txt"))
            {

                string id = sr.ReadLine();
                int i = Convert.ToInt32(id) + 1;
                sr.Close();
                return ReadRiddle(i.ToString());
            }
           
        }
        public string ReadRiddle(string id)
        {
            List<string> Text = new List<string>();
            string Answer = "Это была последняя загадка,либо в программе ошибка.";  
            using (StreamReader sr = new StreamReader("Riddles.txt")) {
                string Line = sr.ReadLine();
                 Line = sr.ReadLine();  
            bool FindLine = false;          
            Text.Add(id);
        while (Line != null)
        {
            Text.Add(Line);   
            if (Line.Length>id.Length && Line.Substring(0,id.Length) == id && !FindLine)
            {
                Answer=Line;
                FindLine = true;
            }
            Line = sr.ReadLine();
          
        }
        sr.Close();
        }

            using (StreamWriter wr = new StreamWriter("Riddles.txt", false))
            {
                for (int i = 0; i < Text.Count; i++)
                    wr.WriteLine(Text[i]);

                wr.Close();
            }

            return Answer;
        }
      

        public void Handleevent(string Command, string args, ISkypeData ClientData, out string Answer)
        {

            try
            {
                if (Command == "Загадка")
                {
                    if (args == "") Answer = ReadRiddle();
                    else Answer = ReadRiddle(args);
                }
                else
                {
                    if (args == "") Answer = ReadAnswer();
                    else Answer = ReadAnswer(args);
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.ToString());
                Answer = "Ошибка";
            }
        }
    }
}
