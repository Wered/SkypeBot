using System;
using System.Threading.Tasks;
using SKYPE4COMLib;
using System.Threading;
using MyModules;

namespace SkypeBot
{
    class Program
    {
        #region Объявление и инцилизация переменных      
        private static Skype skype = new Skype();       
        private static ManagmentModules Modules = new ManagmentModules();
        private static SkypeWork WinWorker = new SkypeWork();       
        #endregion
        static void Main(string[] args)
        {
            
            Modules.LoadModules();
            new Program().Run();
        }     
        private void Run()
        {
            #region Подключение к скайпу
            try
            {
                   skype.Attach(5,true);                   
                   Console.WriteLine("К Скайпу подрубился");
                   #region Очистка всех MissedMessages
        foreach (IChatMessage message in skype.MissedMessages)
        {
            Console.WriteLine(message.Body);
            message.Seen = true;
        }
                    #endregion
                        
            #endregion
            #region Работа с Сообщениями
                  while(true)
                      if(skype.MissedMessages.Count>0)
                      foreach (IChatMessage message in skype.MissedMessages)
                      {
                          if (message.FromDisplayName != skype.CurrentUser.FullName && message.Body != "")
                          {
                              string Answer = AnswerOnMessage(message.Body,message.Sender.Handle);
                              if (Answer != "")
                              {                                 
                              bool ChatIsAvaible = true;
                              Console.WriteLine(message.Sender.Handle + ":" + message.Body);
                              #region Проверка Чата
                                
                              try { string NotUse = message.Chat.FriendlyName; }
                              catch (System.Runtime.InteropServices.COMException e) { Console.WriteLine("ComException:" + e.Message); ChatIsAvaible = false; }
                              catch (Exception e) { Console.WriteLine("Error:" + e.Message+"/nРабота программы будет приостановлена"); break; }
                              #endregion                           
                              if (ChatIsAvaible)
                              #region Отпрвка Личного Сообщения
                                  {
                                      
                                      message.Chat.SendMessage(Answer);                                    

                                  }
                                  #endregion
                              #region Отправка Сообщение Коонференции
                                  else
                                  {
                                      message.Chat.OpenWindow();
                                      Thread.Sleep(300);
                                      WinWorker.Work(Answer);

                                  }
                                  #endregion

                              }

                          }
                          message.Seen = true;
                      }
             

         
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
            #endregion
           
        }
        private string AnswerOnMessage(string Message,string FromName)
        { 
        string Command;
        if (Message[0] != '!') return ""; else Command = Message.Substring(1);
        #region Отделение Аргументов от Команды
        String Args;
        if (Command.Contains(" "))
        {
            Args = Command.Substring(Command.IndexOf(' ')+1);
            Command = Command.Substring(0, Command.IndexOf(' '));
        }
        else
        {
            Args = "";
        }
        #endregion
        string Answer = Modules.Answer(Command, Args,FromName);
        if (Answer == "")
        {
            return "";
        }
        return Answer;
        }
   
      
    }
   
}
