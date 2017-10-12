using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Repository.Interfaces
{
    public interface IAddress
    {
        int StreetNumber { get; }
        string StreetName { get; }
        string FullAddress {get; set; }
    }
}
