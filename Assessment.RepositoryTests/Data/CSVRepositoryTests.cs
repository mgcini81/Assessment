using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Assessment.Repository.Entities;
using Assessment.Repository.Interfaces;
using Ninject;
using System.Reflection;

namespace Assessment.Repository.Data.Tests
{
    [TestClass()]
    public class CSVRepositoryTests
    {
        [TestMethod()]
        public void GetAllTest()
        {
            IKernel _kernel = new StandardKernel();
            _kernel.Load(Assembly.GetExecutingAssembly());
            ICSVRepository<IPerson, IFrequency, IAddress> _repo = _kernel.Get<ICSVRepository<IPerson, IFrequency, IAddress>>();

            var data = _repo.GetAll();

            Assert.IsTrue(data.Count() > 0);
        }

        [TestMethod()]
        public void GetNameFrequencyTest()
        {
            IKernel _kernel = new StandardKernel();
            _kernel.Load(Assembly.GetExecutingAssembly());
            ICSVRepository<IPerson, IFrequency, IAddress> _repo = _kernel.Get<ICSVRepository<IPerson, IFrequency, IAddress>>();

            var frequencies = _repo.GetNameFrequency();

            Assert.IsTrue((frequencies.Count() > 0) && frequencies.FirstOrDefault().Value == "Clive" && frequencies.FirstOrDefault().Count == 2);
        }

        [TestMethod()]
        public void GetSortedAddressesTest()
        {
            IKernel _kernel = new StandardKernel();
            _kernel.Load(Assembly.GetExecutingAssembly());
            ICSVRepository<IPerson, IFrequency, IAddress> _repo = _kernel.Get<ICSVRepository<IPerson, IFrequency, IAddress>>();

            var sortedAddresses = _repo.GetSortedAddresses();

            Assert.IsTrue((sortedAddresses.Count() > 0) && sortedAddresses.FirstOrDefault().FullAddress == "65 Ambling Way");
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