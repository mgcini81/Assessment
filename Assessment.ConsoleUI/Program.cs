using Assessment.Repository.Data;
using Assessment.Repository.Entities;
using Assessment.Repository.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            IKernel _kernel = new StandardKernel();
            _kernel.Load(Assembly.GetExecutingAssembly());
            ICSVRepository<IPerson, IFrequency, IAddress> _repo = _kernel.Get<ICSVRepository<IPerson, IFrequency, IAddress>>();
            IOutputWritter _writter = _kernel.Get<IOutputWritter>();
            var frequencies = _repo.GetNameFrequency();
            var sortedAddresses = _repo.GetSortedAddresses();

            StringBuilder sbfr = new StringBuilder();
            StringBuilder sbsort = new StringBuilder();
            foreach (var fr in frequencies)
            {
                sbfr.AppendLine(String.Format("{0},{1}", fr.Value, fr.Count));
            }
            foreach (var adr in sortedAddresses)
            {
                sbsort.AppendLine(String.Format("{0}",adr.FullAddress));
            }
            ShowNameFrequency(frequencies);
            ShowSortedAddresses(sortedAddresses);
            _writter.WriteToOutPutFile(sbfr);
            _writter.WriteToOutPutFile(sbsort);
            Console.WriteLine("Opening Output Folder");
            Console.WriteLine("Press any key to exit and open the Output Folder....");           
            Console.ReadKey();
            _writter.OpenLogDirectory();

        }

        static void ShowNameFrequency(IEnumerable<IFrequency> frequency)
        {
            Console.WriteLine("Showing  the frequency of the first and last names ordered by frequency descending and then alphabetically ascending");
            foreach (var fr in frequency)
            {              
                Console.WriteLine("{0},{1}", fr.Value, fr.Count);
            }
            Console.WriteLine("-------------------------------------------------------");
        }

        static void ShowSortedAddresses(IEnumerable<IAddress> addresses )
        {
            Console.WriteLine("Showing  the addresses sorted alphabetically by street name. ");
            foreach (var adr in addresses)
            {               
                Console.WriteLine("{0}", adr.FullAddress); 
            }
            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine();
        }
    }

    public class NinjectBindings : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<IPerson>().To<Person>();
            Bind<IAddress>().To<Address>();
            Bind<IFrequency>().To<Frequency>();
            Bind(typeof(IFileDataProvider<>)).To(typeof(CSVPersonDataProvider));
            Bind(typeof(ICSVRepository<,,>)).To(typeof(CSVRepository));
            Bind<IOutputWritter>().To<OutputWritter>();
        }
    }
}
