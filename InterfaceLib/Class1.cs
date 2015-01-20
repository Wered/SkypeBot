using System;
using System.Collections.Generic;

namespace InterfaceLib
{
  public  interface IBodyOfModule 
    {
        string[] CommandList { get; }

        string DescrptionModule { get; }

        void Handleevent(string Command,string args, ISkypeData ClientData,out string Answer);   
    }
  public interface ISkypeData 
  {
      string FromName { get; set; }
     
  }
    

}
