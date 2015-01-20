using InterfaceLib;
using System;
using System.ComponentModel.Composition;

namespace MyModules
{
    [Export(typeof(IBodyOfModule))]
    class RandomNumber:IBodyOfModule
    {
        public string[] CommandList
        {
            get { return new string[] { "Число" }; }
        }

        public string DescrptionModule
        {
            get { return "Рандоайзер-выдаёт рандомное число"; }
        }

        public void Handleevent(string Command, string args, ISkypeData ClientData, out string Answer)
        {
         
            Random r = new Random();
            int Number = r.Next(101);
            Answer = ClientData.FromName + ":" + Number;
        }

    }
}
