using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using Assessment.Repository.Interfaces;
using System.Reflection;
using Ninject;
using System.IO;

namespace Assessment.Repository.Data.Tests
{
    [TestClass()]
    public class OutputWritterTests
    {
        [TestMethod()]
        public void WriteToOutPutFileTest()
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
                sbsort.AppendLine(String.Format("{0}", adr.FullAddress));
            }

            _writter.WriteToOutPutFile(sbfr);
            _writter.WriteToOutPutFile(sbsort);

            Assert.IsTrue(Directory.GetFiles(Environment.CurrentDirectory + @"\Output", "Output*.csv").Length ==2);
        }
    }
}