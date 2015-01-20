using InterfaceLib;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Text;

namespace SkypeBot
{
    class ManagmentModules
    {
       
        [ImportMany(typeof(IBodyOfModule))]
        public IEnumerable<IBodyOfModule> EventHandl { get; set; }
        public void LoadModules()
        {
            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new DirectoryCatalog("Modules"));
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }
        string[] SystemCommands = {"Модули","Помощь" };
        ISkypeData data = new SkypeData();
        public string Answer(string Command,string args,string Name) 
        {
            try
            {
                data.FromName = Name;
                string result = "";
                #region Проверка на СисКоманды
                foreach (var SysCommand in SystemCommands)
                {
                    if (SysCommand == Command) 
                    {
                        if (Command == "Модули")
                        {
                            result = DescriptionModules();
                        }
                        else
                        {
                            result = "*Здесь должна быть полезная инфа*";
                        }
                        return result;
                    }
                
                }
                #endregion
                #region Поиск по модулям
                foreach (var Module in EventHandl)
            {
                foreach (var _Command in Module.CommandList)
                {
                    if (_Command.ToLower()==Command.ToLower())
                    {
                        Module.Handleevent(_Command, args, data, out result);
                        return result;

                    }
               }
            }
                #endregion
                return result; }
            catch (Exception e) { Console.WriteLine(e.ToString()); return ""; }
           
        }
        private string DescriptionModules() 
        {
            StringBuilder result = new StringBuilder();
            foreach (var Module in EventHandl)
            {
                result.AppendLine(Module.DescrptionModule);
                 result.Append("Команды:");
                 foreach (string Command in Module.CommandList)
                 { result.Append("!"+Command+","); }
                 result[result.Length-1]='\n';
            }            
            return result.ToString(); ;
        }
    }

    struct SkypeData : ISkypeData 
    {
        public string FromName
        {
            get;
            set;
        }
    }
}
