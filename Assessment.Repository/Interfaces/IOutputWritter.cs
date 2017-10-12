using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Repository.Interfaces
{
    public interface IOutputWritter
    {
        void WriteToOutPutFile(StringBuilder message);
        void OpenLogDirectory();       
    }
}
